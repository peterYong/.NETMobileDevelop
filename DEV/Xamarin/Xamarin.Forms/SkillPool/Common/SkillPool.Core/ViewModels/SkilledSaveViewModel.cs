using System;
using System.Collections.Generic;
using System.Text;
using SkillPool.Core.ViewModels.Base;
using SkillPool.Core.Services.Skilled;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using SkillPool.Core.Views;
using SkillPool.Model.Skilled;
using SkillPool.DataAccess.Skilled;

namespace SkillPool.Core.ViewModels
{
    public class SkilledSaveViewModel : ViewModelBase
    {
        private SkilledItem _skilledItem;
        private readonly ISkilledService _skilledService;
        private bool isAdd = false;

        public SkilledSaveViewModel(ISkilledService skilledService)
        {
            _skilledService = skilledService;
        }

        /// <summary>
        /// 绑定对象
        /// </summary>
        public SkilledItem SkilledItem
        {
            get { return _skilledItem; }
            set
            {
                _skilledItem = value;
                RaisePropertyChanged(() => SkilledItem);
            }
        }

        public bool IsAdd
        {
            get { return isAdd; }
            set
            {
                isAdd = value;
                RaisePropertyChanged(() => IsAdd);
            }
        } 

        public ICommand SkillSaveCommand => new Command(async () => await SkillSavelAsync());

        private async Task SkillSavelAsync()
        {
            if (isAdd)
            {
                await _skilledService.AddSkilledItemAsync(SkilledItem).ConfigureAwait(false);
            }
            else
            {
                await _skilledService.UpdateSkilledItemAsync(SkilledItem).ConfigureAwait(false);
            }

            //await NavigationService.NavigateToAsync<MainViewModel>(new TabParameter { TabIndex = 0 });
            await NavigationService.NavigateToAsync<SkilledSaveViewModel>();
            await NavigationService.RemoveBackStackAsync();

        }


        public bool IsNewSkill { get; } = true;

        public override async Task InitializeAsync(object navigationData)
        {
            if (navigationData == null)  //添加
            {
                SkilledItem = new SkilledItem();
                IsAdd = true;
            }
            if (navigationData is SkilledItem)  //修改
            {
                SkilledItem = navigationData as SkilledItem;
                IsAdd = false;
            }
        }

    }
}
