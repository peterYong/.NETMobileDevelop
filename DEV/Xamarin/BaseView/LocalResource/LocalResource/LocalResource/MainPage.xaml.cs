using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LocalResource
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();

            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }



        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if (item == null)
            {
                return;
            }
            //string assName = Assembly.GetExecutingAssembly().GetName().Name;
            //string typeName = string.Format("{0}.{1},{0}", assName, item.PageType);
            //Type type = Type.GetType(typeName);
            var page = (Page)Activator.CreateInstance(item.PageType);
            page.Title = item.Title;
            //设置详情页内容
            Detail = new NavigationPage(page);
            //关闭Master页，显示Detail页
            IsPresented = false;
            //设置Master页中的ListViewItem为非选中状态，去除默认选中样式
            MasterPage.ListView.SelectedItem = null;
        }
    }
}
