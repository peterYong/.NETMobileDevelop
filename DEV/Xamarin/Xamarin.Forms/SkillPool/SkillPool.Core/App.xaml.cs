using SkillPool.Core.Navigation;
using SkillPool.Core.ViewModels.Base;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkillPool.Core
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
        }

        protected async override void OnStart()
        {
            // Handle when your app starts
            base.OnStart();

            await InitNavigation();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            base.OnResume();
        }

        /// <summary>
        /// 初始化导航
        /// </summary>
        /// <returns></returns>
        private Task InitNavigation()
        {
            var navigationService = ViewModelLocator.Resolve<INavigationService>();
            return navigationService.InitializeAsync();
        }
    }
}
