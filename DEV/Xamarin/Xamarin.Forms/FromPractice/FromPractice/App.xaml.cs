using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FromPractice.Services;
using FromPractice.Views;
using System.Diagnostics;
using FromPractice.Views.DataBinding;

namespace FromPractice
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            
            MainPage = new MainPage();
            //MainPage = new BasicCodeBindingPage();  //加到菜单中
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            Debug.WriteLine("OnStart");
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            Debug.WriteLine("OnSleep");
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            Debug.WriteLine("OnResume");
        }
    }
}
