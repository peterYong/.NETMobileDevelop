using RedisHelper;
using SkillPool.Core.Navigation;
using SkillPool.Core.Services.Skilled;
using SkillPool.Core.ViewModels.IM;
using SkillPool.DataAccess.Skilled;
using SkillPool.Server.Settings;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using TinyIoC;
using Xamarin.Forms;

namespace SkillPool.Core.ViewModels.Base
{
    /// <summary>
    /// 视图模型定位器
    /// </summary>
    public static class ViewModelLocator
    {
        /// <summary>
        /// 依赖注入容器，，注册、解析
        /// </summary>
        private static readonly TinyIoCContainer _container;

        static ViewModelLocator()
        {
            _container = new TinyIoCContainer();

            // View models - by default, TinyIoC will register concrete classes as multi-instance.
            _container.Register<LoginViewModel>();  //直接注册具体类型，当解析LoginViewModel类型时，容器将注入其所需的依赖项（ISettingsService，其具体类型是下面注册的）
            _container.Register<SettingsViewModel>();
            _container.Register<MainViewModel>();
            _container.Register<SkilledViewModel>();
            _container.Register<SkilledDetailViewModel>();
            _container.Register<SkilledSaveViewModel>();
            _container.Register<ChatViewModel>();
            //_container.Register<RedisCacheHelper>();

            // Services - by default, TinyIoC will register interface registrations as singletons.(TinyIoC默认将接口映射注册为单例)
            _container.Register<INavigationService, NavigationService>().AsSingleton();
            _container.Register<ISettingsService, SettingsService>();
            _container.Register<ISkilledService, SkilledSqliteMockService>();
        }

        public static T Resolve<T>() where T : class
        {
            return _container.Resolve<T>();
        }

        #region Connecting View Models to Views

        /// <summary>
        /// view自动绑定到ViewModel。default(bool)=False
        /// </summary>
        public static readonly BindableProperty AutoWireViewModelProperty =
            BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), default(bool), propertyChanged: OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            if(bindable!=null)
            {
                return (bool)bindable.GetValue(ViewModelLocator.AutoWireViewModelProperty);
            }
            else
            {
                return false;
            }
        }

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            if (bindable != null)
            {
                bindable.SetValue(ViewModelLocator.AutoWireViewModelProperty, value);
            }
        }

        /// <summary>
        /// 根据View名称来生成viewModel名称，并设置View的BindingContext=具体的ViewModel。必须要保证以下几个条件：
        ///View models are in the same assembly as view types.
        ///Views are in a.Views child namespace.
        ///View models are in a.ViewModels child namespace.
        ///View model names correspond with view names and end with "ViewModel".
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is Element view))
            {
                return;
            }

            var viewType = view.GetType();
            var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);

            var viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null)
            {
                return;
            }
            var viewModel = _container.Resolve(viewModelType);
            view.BindingContext = viewModel;
        }

        #endregion

    }
}
