using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BaseControls.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class List_Picker : ContentPage
    {
        public List_Picker()
        {
            InitializeComponent();

            List<string> items = new List<string>() { "选项1", "选项2", "选项3", "选项4", "选项5", "选项6" };
            picker.ItemsSource = items;
            picker.SelectedIndex = 2;
        }
    }
}