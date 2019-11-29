using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FromPractice.Views.Navigation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainNavigationPage : NavigationPage
    {
        public MainNavigationPage()
        {
            InitializeComponent();
        }

        public MainNavigationPage(Page root):base(root)
        {
            InitializeComponent();

            Page page = Application.Current.MainPage;
           int count= Navigation.NavigationStack.Count;
        }
    }
}