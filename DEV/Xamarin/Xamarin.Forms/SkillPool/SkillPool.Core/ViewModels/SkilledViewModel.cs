using SkillPool.Core.ViewModels.Base;
using SkillPool.Core.Views;
using SkillPool.DataAccess.Skilled;
using SkillPool.Model.Skilled;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using SkillPool.Common.Extensions;

namespace SkillPool.Core.ViewModels
{
    public class SkilledViewModel : ViewModelBase
    {
        private readonly ISkilledService _skilledService;
        private ObservableCollection<SkilledItem> _skilledItems;
        private bool _isQuery;
        private string _queryName;
        public SkilledViewModel(ISkilledService skilledService)
        {
            _skilledService = skilledService;
            Time = DateTime.Now.ToString(System.Globalization.CultureInfo.InvariantCulture); //测试用
        }

        public ObservableCollection<SkilledItem> SkilledItems
        {
            get { return _skilledItems; }
            set
            {
                _skilledItems = value;
                OnPropertyChanged();
            }
        }

        public bool IsQuery
        {
            get { return _isQuery; }
            set
            {
                _isQuery = value;
                RaisePropertyChanged(() => IsQuery);
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
        private string time;
        public string Time
        {
            get { return time; }
            set
            {
                time = value;
                RaisePropertyChanged(() => Time);
            }
        }

        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;

            List<SkilledItem> skilledItems = await _skilledService.GetAllSkilledItemsAsync();
            SkilledItems = skilledItems.ToObservableCollection();

            IsBusy = false;
        }
        public ICommand SkillAddCommand => new Command(async () => await SkillAddAsync());
        public ICommand SkillDetailCommand => new Command<SkilledItem>(async (skilledItem) => await SkillDetailAsync(skilledItem));
        //public ICommand SkillDeleteCommand => new Command<SkilledItem>(async (skilledItem) => await SkillDeleteAsync(skilledItem));
        public ICommand SkillQueryCommand => new Command(async () => await SkillQueryAsync());
        public ICommand SkillQueryBtnCommand => new Command(async () => await SkillQueryBtnAsync());

        private async Task SkillDetailAsync(SkilledItem skilledItem)
        {
            //SkilledItem skilled = (SkilledItem)BindingContext;
            await NavigationService.NavigateToAsync<SkilledDetailViewModel>(skilledItem).ConfigureAwait(false);
        }
        private async Task SkillAddAsync()
        {
            //列表页进入保存页面是要增加
            await NavigationService.NavigateToAsync<SkilledSaveViewModel>().ConfigureAwait(false);
        }
        private async Task SkillQueryAsync()
        {
            //IsQuery = true;
            await NavigationService.NavigateToAsync<SkilledQueryViewModel>().ConfigureAwait(false);
        }
        private async Task SkillQueryBtnAsync()
        {
            List<SkilledItem> skilledItems = await _skilledService.GetSkilledItemByNameAsync(QueryName);
            SkilledItems = skilledItems.ToObservableCollection();
        }

        //private async Task SkillDeleteAsync(SkilledItem skilledItem)
        //{
        //    await _skilledService.RemoveSkilledItemAsync(skilledItem).ConfigureAwait(false);
        //    //NavigationService.RemoveLastFromBackStackAsync();
        //}

    }
}
