using Notes.Data;
using Notes.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes
{
    /// <summary>
    /// 多页程序的第一页
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesPage : ContentPage
    {
        public NotesPage()
        {
            InitializeComponent();
        }

        #region 数据保存到txt中

        /// <summary>
        /// 显示该页时, OnAppearing将执行方法，检索本地文件数据，填充到listView
        /// </summary>
        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();

        //    //从txt文件中读取数据
        //    var notes = new List<Note>();
        //    var files = Directory.EnumerateFiles(App.FolderPath, "*.notes.txt");
        //    foreach (var filename in files)
        //    {
        //        notes.Add(new Note
        //        {
        //            Filename = filename,
        //            Text = File.ReadAllText(filename),
        //            Date = File.GetCreationTime(filename),
        //        });
        //    }
        //    listView.ItemsSource = notes.OrderBy(d => d.Date).ToList();
        //}


        //async void OnNoteAddedClicked(object sender, EventArgs e)
        //{
        //    //导航到NoteEntryPage，并将NoteEntryPage的BindingContext设置为新Note实例
        //    await Navigation.PushAsync(new NoteEntryPage
        //    {
        //        BindingContext = new Note()
        //    });
        //}

        //async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    if (e.SelectedItem != null)
        //    {
        //        //导航到NoteEntryPage，并将NoteEntryPage的BindingContext设置为所选中的Note实例
        //        await Navigation.PushAsync(new NoteEntryPage
        //        {
        //            BindingContext = e.SelectedItem as Note
        //        });
        //    }
        //}

        #endregion

        #region 数据保存到sqlite中


        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext != null)
            {
                listView.ItemsSource = (List<NoteData>)BindingContext;
            }
            else
            {
                listView.ItemsSource = await App.Database.GetNotesAsync();
            }
        }


        async void OnNoteAddedClicked(object sender, EventArgs e)
        {
            //导航到NoteEntryPage，并将NoteEntryPage的BindingContext设置为新Note实例
            await Navigation.PushAsync(new NoteEntryPageFromSQLite
            {
                BindingContext = new NoteData()
            });
        }

        /// <summary>
        /// 选中某一条记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                //导航到NoteEntryPage，并将NoteEntryPage的BindingContext设置为所选中的Note实例
                await Navigation.PushAsync(new NoteEntryPageFromSQLite
                {
                    BindingContext = e.SelectedItem as NoteData
                });
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnQuery_Clicked(object sender, EventArgs e)
        {
            string text = text_Query.Text;
            if (!string.IsNullOrEmpty(text))
            {
                //这种实现页面会刷新
                //await Navigation.PushAsync(new NotesPage
                //{
                //    BindingContext = App.Database.GetNoteAsyncByString(text).Result
                //}); 

                listView.ItemsSource = App.Database.GetNoteAsyncByString(text).Result;
            }
            else
            {
                listView.ItemsSource = App.Database.GetNotesAsync().Result;
            }
        }
        #endregion




    }
}