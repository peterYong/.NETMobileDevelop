using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AndroidDevice
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            System.Diagnostics.Debug.WriteLine(TAG + "App()");

            MainPage = new MainPage();
        }
        public App(string text)
        {
            InitializeComponent();
            System.Diagnostics.Debug.WriteLine(TAG + "App()");

            MainPage = new MainPage("应用的签名：" + text);
        }


        static readonly string TAG = "Debug:" + typeof(App).Name + ":";

        protected override void OnStart()
        {
            // Handle when your app starts
            System.Diagnostics.Debug.WriteLine(TAG + "OnStart");
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            System.Diagnostics.Debug.WriteLine(TAG + "OnSleep");
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            System.Diagnostics.Debug.WriteLine(TAG + "OnResume");
        }

    }
}
