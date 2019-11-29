using LocalResource.Resources.Language;
using LocalResource.Resources.Style;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LocalResource
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
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

        public static void Localize(string lang = "en")
        {
            var lan = GetCurrentByType("Language");
            Application.Current.Resources.MergedDictionaries.Remove(lan);

            switch (lang)
            {
                case "zh":
                    lan = new zh();
                    break;
                default:
                    lan = new en();
                    break;
            }
            Application.Current.Resources.MergedDictionaries.Add(lan);
       
        }

        public static void Theme(string theme = "Light")
        {
            var lan = GetCurrentByType("Style");
            Application.Current.Resources.MergedDictionaries.Remove(lan);

            switch (theme)
            {
                case "Light":
                    lan = new Light();
                    break;
                default:
                    lan = new Dark();
                    break;
            }
            Application.Current.Resources.MergedDictionaries.Add(lan);
        }

        public static ResourceDictionary GetCurrentByType(string type)
        {
            ResourceDictionary lan = null;
            foreach (ResourceDictionary item in Application.Current.Resources.MergedDictionaries)
            {
                if (item.GetType().FullName.Contains(type) || item.Source?.OriginalString.Contains(type) == true)
                {
                    lan = item;
                }
            }
            return lan;
        }
    }
}
