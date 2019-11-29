using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BaseControls
{
    public class MsgUtil
    {
        public static void ShowMsg(string msg)
        {
            Application.Current.MainPage.DisplayAlert("提示", msg, "确定");
        }
    }
}
