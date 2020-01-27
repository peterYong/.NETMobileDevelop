using SkillPool.Core.ViewModels.Base;
using SkillPool.DataAccess.Skilled;
using SkillPool.Model.Skilled;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using SkillPool.Common.Extensions;

namespace SkillPool.Core.ViewModels
{
    public class SkilledQueryViewModel : ViewModelBase
    {
        private readonly ISkilledService _skilledService;
        private ObservableCollection<SkilledItem> _skilledItems;
        private string _queryName;
        public SkilledQueryViewModel(ISkilledService skilledService)
        {
            _skilledService = skilledService;
        }

        public ObservableCollection<SkilledItem> SkilledItems
        {
            get { return _skilledItems; }
            set
            {
                _skilledItems = value;
                RaisePropertyChanged(() => SkilledItems);
            }
        }

       
        public string QueryName
        {
            get { return _queryName; }
            set
            {
                _queryName = value;
                RaisePropertyChanged(() => QueryName);
            }
        }

        public override async Task InitializeAsync(object navigationData)
        {
            
        }
        public ICommand SkillDetailCommand => new Command<SkilledItem>(async (skilledItem) => await SkillDetailAsync(skilledItem));
        public ICommand SkillQueryBtnCommand => new Command(async () => await SkillQueryBtnAsync());

        private async Task SkillDetailAsync(SkilledItem skilledItem)
        {
            await NavigationService.NavigateToAsync<SkilledDetailViewModel>(skilledItem).ConfigureAwait(false);
        }
       
        private async Task SkillQueryBtnAsync()
        {
            List<SkilledItem> skilledItems = await _skilledService.GetSkilledItemByNameAsync(QueryName);
            SkilledItems = skilledItems.ToObservableCollection();
        }
      
    }
}
