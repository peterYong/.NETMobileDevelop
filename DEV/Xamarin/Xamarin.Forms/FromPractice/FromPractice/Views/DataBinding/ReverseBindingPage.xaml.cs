using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FromPractice.Views.DataBinding
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReverseBindingPage : ContentPage
    {
        /// <summary>
        /// 反向绑定，当 Slider（而非 Label）是绑定目标时，数据绑定效果更好
        /// </summary>
        public ReverseBindingPage()
        {
            InitializeComponent();
        }
    }
}