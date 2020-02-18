using SkillPool.Core.ViewModels.IM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkillPool.Core.Views.IM
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessageView : ContentPage
    {
        public MessageView()
        {
            InitializeComponent();

            BindingContext = new MessageViewModel();
        }
    }
}