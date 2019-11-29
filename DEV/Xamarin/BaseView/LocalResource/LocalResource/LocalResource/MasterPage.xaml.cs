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
    public partial class MasterPage : ContentPage
    {
        public ListView ListView;

        public MasterPage()
        {
            InitializeComponent();

            ListView = MasterListView;
            MasterListView.BindingContextChanged += MasterListView_BindingContextChanged;
            InitData();
        }

        private void MasterListView_BindingContextChanged(object sender, EventArgs e)
        {
            List<MasterPageItem> list = MasterListView.BindingContext as List<MasterPageItem>;
            //MasterListView.
        }

        public void InitData()
        {
            List<MasterPageItem> list = new List<MasterPageItem>()
            {
                new MasterPageItem(){ Title="InBox",Icon="received.png",PageType=typeof(DetailPage)},
                new MasterPageItem(){ Title="OutBox",Icon="send.png",PageType=typeof(Send)},
                new MasterPageItem(){ Title="Draft",Icon="draft.png",PageType=typeof(Draft)},
            };
            MasterListView.ItemsSource = list;
        }
    }
}