using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LocalResource.Resources.Language
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class en : ResourceDictionary
    {
        public en()
        {
            InitializeComponent();
        }
    }
}