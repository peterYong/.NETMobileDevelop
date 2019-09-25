using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FromPractice.Services;
using FromPractice.Views;
using System.Diagnostics;
using FromPractice.Views.DataBinding;
using FromPractice.ViewModels;

namespace FromPractice
{
    public partial class App : Application
    {
        public SampleSettingsViewModel Settings { private set; get; }
        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            
            MainPage = new MainPage();
            //MainPage = new BasicCodeBindingPage();  //加到菜单中

            Settings = new SampleSettingsViewModel(Application.Current.Properties); //初始没有属性值，进度页面 设置后，会绑定到Current.Properties，并且OnSleep时保存
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

            Settings.SaveState(Current.Properties);
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            Debug.WriteLine("OnResume");
        }
    }
}
