using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AwesomeApp.MyEntry
{
    /// <summary>
    /// 写邮件页面的编辑器（隐藏下划线、监听keyPress事件...）
    /// </summary>
    public class WriteEmailEditor : Editor
    {
        public delegate void OnKeyDownHandler();
        public event OnKeyDownHandler KeyDown;
        public void OnKeyDown()
        {
            KeyDown?.Invoke();
        }

        

    }
}
