using BaseControls.Utils;
using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace BaseControls.Views
{
    public class MasterPageItem : ViewModelBase
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        public Type PageType { get; set; }
        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }
        public Color Color { get; set; } = Color.Black;
        public Color SelectedColor { get; set; } = Color.Red;
    }
}
