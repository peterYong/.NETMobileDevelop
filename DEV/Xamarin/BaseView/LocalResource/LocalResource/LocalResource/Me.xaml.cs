using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LocalResource
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Me : ContentPage
    {
        public Me()
        {
            InitializeComponent();
        }
    }
}