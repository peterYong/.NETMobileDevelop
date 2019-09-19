using Notes.Data;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes
{
    public partial class App : Application
    {
        public static string FolderPath { get; private set; }
        public App()
        {
            InitializeComponent();

            //1、single page
            //MainPage = new MainPage();

            //2、multi pages，数据保存在txt文件
            //存储数据的路径。。调试发现路径=/data/user/0/com.companyname.notes/files/.local/share  【在手机上打开files程序】
            //FolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            //MainPage = new NavigationPage(new NotesPage());

            //3、multi pages，数据保存在sqlite数据库中
            MainPage = new NavigationPage(new NotesPage());
        }

        static NoteDatabase database;
        public static NoteDatabase Database
        {
            get
            {
                if(database==null)
                {
                    database= new NoteDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notes.db3"));
                }
                return database;
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
