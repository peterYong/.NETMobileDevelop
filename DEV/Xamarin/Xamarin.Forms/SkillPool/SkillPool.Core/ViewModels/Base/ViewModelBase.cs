using SkillPool.Core.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SkillPool.Core.ViewModels.Base
{
    public class ViewModelBase : ExtendedBindableObject
    {
        protected readonly INavigationService NavigationService;
        private bool _isBusy;

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }

            set
            {
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        public ViewModelBase()
        {
            NavigationService = ViewModelLocator.Resolve<INavigationService>(); //返回的NavigationService对象 同App.xaml.cs中创建的一样

            //var settingsService = ViewModelLocator.Resolve<ISettingsService>();

        }


        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }
    }
}
