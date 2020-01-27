using SkillPool.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using SkillPool.Common.Extensions;
using System.Linq;

namespace SkillPool.Core.ViewModels
{
    class FindingViewModel : ViewModelBase
    {
        #region  Properties
        ObservableCollection<string> _items;
        public ObservableCollection<string> Items
        {
            get => _items;
            set { _items = value; OnPropertyChanged(); }
        }

        string _text;
        public string Text
        {
            get => _text;
            set { _text = value; OnPropertyChanged(); }
        }

        string _selectedItem;
        public string SelectedItem
        {
            get => _selectedItem;
            set { _selectedItem = value; OnPropertyChanged(); }
        }

        List<string> dataSource;

        #endregion


        public async override Task InitializeAsync(object navigationData)
        {
            Items = new ObservableCollection<string>();

            // This simulates a data source that can be obtained by web service or read from database.
            // It is advisable
            dataSource = new List<string>();
            dataSource.Add("A");
            dataSource.Add("B");
            dataSource.Add("C");
            dataSource.Add("D");
            dataSource.Add("E");

            Items = dataSource.ToObservableCollection<string>();

            await Task.Delay(100);
        }

        #region Commands
      

        public ICommand SerchCommand
        {
            get
            {
                return new Command((o) =>
                {
                    Items.Clear();
                    if (!string.IsNullOrEmpty(Text))
                    {
                        IEnumerable<string> temp = dataSource.Where(x => x == Text.ToLower() || x == Text.ToUpper());
                        Items = temp.ToObservableCollection<string>();
                    }
                    else
                    {
                        Items = dataSource.ToObservableCollection<string>();
                    }
                       
                });
            }
        }

        public ICommand SelectItemCommand
        {
            get
            {
                return new Command((o) =>
                {
                    if (SelectedItem != null)
                    {
                        App.Current.MainPage.DisplayAlert("Test", $"You have selected {SelectedItem} item", "Ok");

                        // this deselects the item in the list.
                        SelectedItem = null;
                    }
                });
            }
        }
        #endregion
    }
}
