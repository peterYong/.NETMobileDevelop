using SkillPool.Core.ViewModels.Base;
using SkillPool.DataAccess.Skilled;
using SkillPool.Model.Skilled;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SkillPool.Core.ViewModels
{
    public class SkilledDetailViewModel : ViewModelBase
    {
        private readonly ISkilledService _skilledService;
        private SkilledItem _skilledItem;

        public SkilledDetailViewModel(ISkilledService skilledService)
        {
            _skilledService = skilledService;
        }

        public SkilledItem SkilledItem
        {
            get { return _skilledItem; }
            set
            {
                _skilledItem = value;
                RaisePropertyChanged(() => SkilledItem);
            }
        }

        public override async Task InitializeAsync(object navigationData)
        {
            if (navigationData is SkilledItem)
            {
                IsBusy = true;
                var item = navigationData as SkilledItem;
                SkilledItem = await _skilledService.GetSkilledItemByIdAsync(item.Id).ConfigureAwait(false);
                IsBusy = false;
            }
        }

        public ICommand UpdateSkillCommand => new Command(async () => await SkillUpdateAsync());

        private async Task SkillUpdateAsync()
        {
            //详情进入保存页面是要修改
            await NavigationService.NavigateToAsync<SkilledSaveViewModel>(SkilledItem).ConfigureAwait(false);
        }


    }
}
