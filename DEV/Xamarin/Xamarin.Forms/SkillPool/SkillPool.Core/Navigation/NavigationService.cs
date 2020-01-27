using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SkillPool.Core.ViewModels;
using SkillPool.Core.ViewModels.Base;
using SkillPool.Core.Views;
using SkillPool.Server.Settings;
using Xamarin.Forms;

namespace SkillPool.Core.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly ISettingsService _settingsService;

        public ViewModelBase PreviousPageViewModel
        {
            get
            {
                var mainPage = Application.Current.MainPage as CustomNavigationView;
                var viewModel = mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2].BindingContext;
                return viewModel as ViewModelBase;
            }
        }


        public NavigationService(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        /// <returns></returns>
        public Task InitializeAsync()
        {
            if (string.IsNullOrEmpty(_settingsService.AuthAccessToken))
            {
                return NavigateToAsync<LoginViewModel>();
            }
            else
            {
                return NavigateToAsync<MainViewModel>();
            }
        }


        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="parameter">给InitializeAsync用</param>
        /// <returns></returns>
        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        /// <summary>
        /// 通过viewModel 转换为Page并导航到页面
        /// </summary>
        /// <param name="viewModelType"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            //需要导航到的页面
            Page page = CreatePage(viewModelType);
            if (page is LoginView)
            {
                Application.Current.MainPage = new CustomNavigationView(page);
            }
            else
            {
                if (Application.Current.MainPage is CustomNavigationView navigationPage)
                {
                    await navigationPage.PushAsync(page).ConfigureAwait(false);
                }
                else
                {
                    Application.Current.MainPage = new CustomNavigationView(page);
                }
            }

            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter).ConfigureAwait(false);
        }
        private Page CreatePage(Type viewModelType)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }
            //实例化视图时，该视图与对应的视图模型会关联（ViewModelLocator中的AutoWireViewModel）
            Page page = Activator.CreateInstance(pageType) as Page;   
            return page;
        }
        /// <summary>
        /// 查找与视图模型类型对应的视图，基于一些约定
        /// </summary>
        /// <param name="viewModelType"></param>
        /// <returns></returns>
        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            var viewName = viewModelType.FullName.Replace("Model", string.Empty);
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }

        public Task RemoveLastFromBackStackAsync()
        {
            if (Application.Current.MainPage is CustomNavigationView mainPage)
            {
                mainPage.Navigation.RemovePage(
                    mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2]);
            }
            //Application.Current.MainPage = mainPage;
            return Task.FromResult(true);
        }

        public Task RemoveBackStackAsync()
        {
            if (Application.Current.MainPage is CustomNavigationView mainPage)
            {
                for (int i = mainPage.Navigation.NavigationStack.Count - 1; i > 0; i--)
                {
                    var page = mainPage.Navigation.NavigationStack[i];

                    mainPage.Navigation.RemovePage(page);
                    //mainPage.Navigation.PopAsync();
                    //Application.Current.MainPage = new CustomNavigationView(page);
                }
                //int i = mainPage.Navigation.NavigationStack.Count - 1;
                //var page = mainPage.Navigation.NavigationStack[i];

                //Application.Current.MainPage = mainPage.RootPage ;

            }

            return Task.FromResult(true);
        }
    }
}
