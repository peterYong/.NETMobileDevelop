using SkillPool.Core.ViewModels.IM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkillPool.Core.Views.IM
{
    /// <summary>
    /// 联系人
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactView : ContentPage
    {
        

        public ContactView()
        {
            InitializeComponent();

            BindingContext = new ContactViewModel();
        }
    }
}