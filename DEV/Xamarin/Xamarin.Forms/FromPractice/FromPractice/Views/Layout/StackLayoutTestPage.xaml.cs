using FromPractice.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FromPractice.Views.Layout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StackLayoutTestPage : ContentPage
    {
        public StackLayoutTestPage()
        {
            InitializeComponent();

            string afd = "";
            if (Device.RuntimePlatform == Device.iOS)
            {
                afd = "设备锁" + "\r\n(您的设备未设置指纹或密码)";
            }
            else
            {
                afd = "设备锁\r\n" + "\r\n(您的设备未设置指纹或密码)";
            }
            duohang.Text = afd;

            MySwithCell BackgroundRefleshSetting = new MySwithCell("后台刷新", true, "xiangxi") { On = true };
            section.Add(BackgroundRefleshSetting);

            time.Text = DateTime.Now.ToString();
            time2.Text = DateTime.UtcNow.ToString();
        }

        private void OnViewCellTapped(object sender, EventArgs e)
        {
            SwitchCell switchCell = sender as SwitchCell;
            //_target.IsVisible = !_target.IsVisible;
            switchCell.ForceUpdateSize();
        }

        private void duohang_Tapped(object sender, EventArgs e)
        {
            SwitchCell switchCell = sender as SwitchCell;
            //_target.IsVisible = !_target.IsVisible;
            switchCell.ForceUpdateSize();
        }
    }
}