using BaseControls.Utils;
using BaseControls.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BaseControls.ViewModels
{
    public class MasterPageViewModel : ViewModelBase
    {
        private ObservableCollection<MasterPageItem> _menuItems;

        public ObservableCollection<MasterPageItem> MenuItems
        {
            get { return _menuItems; }
            set { SetProperty(ref _menuItems, value); }
        }

        public MasterPageViewModel()
        {
            InitMenu();
        }

        private void InitMenu()
        {
            MenuItems = new ObservableCollection<MasterPageItem>()
            {
                new MasterPageItem(){ Title="Page1",PageType=typeof(Page1)},
                new MasterPageItem(){ Title="Page2",PageType=typeof(Page2)},
                new MasterPageItem(){ Title="Page3",PageType=typeof(Page3)}
            };
        }
    }
}
