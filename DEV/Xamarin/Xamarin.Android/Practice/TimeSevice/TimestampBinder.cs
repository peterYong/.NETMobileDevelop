using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Practice.TimeSevice
{
    /// <summary>
    /// IBinder接口，最常用的方法
    /// 提供客户端和服务之间的信道，提供对绑定服务的引用
    /// </summary>
    public class TimestampBinder : Binder, IGetTimestamp
    {
        public TimestampService Service { get; private set; }
        public TimestampBinder(TimestampService service)
        {
            this.Service = service;
        }

        public string GetFormattedTimestamp()
        {
            return Service?.GetFormattedTimestamp();
        }
    }
}