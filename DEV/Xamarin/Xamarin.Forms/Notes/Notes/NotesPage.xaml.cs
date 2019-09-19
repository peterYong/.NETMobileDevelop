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

        /// <summary>
        /// 显示该页时, OnAppearing将执行方法，检索本地文件数据，填充到listView
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            var notes = new List<Note>();
            var files = Directory.EnumerateFiles(App.FolderPath, "*.notes.txt");
            foreach (var filename in files)
            {
                notes.Add(new Note
                {
                    Filename = filename,
                    Text = File.ReadAllText(filename),
                    Date = File.GetCreationTime(filename),
                });
            }
            listView.ItemsSource = notes.OrderBy(d => d.Date).ToList();
        }

        async void OnNoteAddedClicked(object sender, EventArgs e)
        {
            //导航到NoteEntryPage，并将NoteEntryPage的BindingContext设置为新Note实例
            await Navigation.PushAsync(new NoteEntryPage
            {
                BindingContext = new Note()
            });
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                //导航到NoteEntryPage，并将NoteEntryPage的BindingContext设置为所选中的Note实例
                await Navigation.PushAsync(new NoteEntryPage
                {
                    BindingContext = e.SelectedItem as Note
                });
            }
        }
    }
}