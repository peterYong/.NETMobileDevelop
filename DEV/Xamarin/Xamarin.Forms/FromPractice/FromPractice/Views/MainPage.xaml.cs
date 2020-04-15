using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using FromPractice.Models;
using FromPractice.Views.DataBinding;
using FromPractice.Views.UserInterface;
using FromPractice.Views.Image;
using FromPractice.Views.Navigation;
using FromPractice.Views.Layout;

namespace FromPractice.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        //Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        readonly Dictionary<int, Page> MenuPages = new Dictionary<int, Page>();
        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemType.Browse, (NavigationPage)Detail);
        }

        /// <summary>
        /// 详情页的导航
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.Browse:
                        MenuPages.Add(id, new NavigationPage(new ItemsPage()));  //内容页
                        break;
                    case (int)MenuItemType.About:
                        MenuPages.Add(id, new NavigationPage(new AboutPage()));
                        break;
                    case (int)MenuItemType.DataBinding:
                        //MenuPages.Add(id, new NavigationPage(new BasicCodeBindingPage())); 
                        MenuPages.Add(id, new NavigationPage(new BindingPage()));  //跳一个 多个按钮的页面，单击按钮再进去二级页面
                        break;
                    case (int)MenuItemType.ActivityIndicatorPage:
                        MenuPages.Add(id, new NavigationPage(new ActivityIndicatorPage()));
                        break;
                    case (int)MenuItemType.DisplayPopUps:
                        //MenuPages.Add(id, new NavigationPage(new DisplayPopUps()));  //用这种方式 会多出一个顶部的导航栏
                        MenuPages.Add(id, new NavigationPage(new DisplayPopUps()));  //选项卡页面
                        break;
                    case (int)MenuItemType.Image:
                        MenuPages.Add(id, new NavigationPage(new ImageBasicTest()));  //选项卡页面
                        break;
                    case (int)MenuItemType.Navigation:
                        MenuPages.Add(id, new NavigationPage(new MainNavigationPage(new AboutPage())));
                        break;
                    case (int)MenuItemType.Layout:
                        MenuPages.Add(id, new NavigationPage(new LayoutTest()));
                        break;
                    case (int)MenuItemType.UserInterface:
                        MenuPages.Add(id, new NavigationPage(new UserInterfaceTest()));
                        break;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;   //将 详情页 显示为所选择的页面

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;  // 此属性控制是显示母版页还是详细信息页。 设置为 true 以显示母版页，设置为 false 以显示详细信息页。
            }
        }
    }
}