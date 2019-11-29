using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BaseControls.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page3 : ContentPage
    {
        public Page3()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            ShowPage(sender, e);
        }

        private void ShowPage(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            var name = btn?.CommandParameter?.ToString();
            if (!string.IsNullOrEmpty(name))
            {
                string assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
                string typeName = string.Format("{0}.Views.{1},{0}", assemblyName, name);
                Type type = Type.GetType(typeName);
                Page page = (Page)Activator.CreateInstance(type);
                Navigation.PushAsync(page);
            }

        }
    }
}