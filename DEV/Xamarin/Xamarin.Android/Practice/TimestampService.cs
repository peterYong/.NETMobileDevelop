using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Practice.TimeSevice;

namespace Practice
{
    /// <summary>
    /// 扩展Service类以实现将活动绑定到服务
    /// 绑定服务具有一个非常简单的 API，该 API 只包含GetFormattedTimestamp一种方法，该方法返回一个字符串，该字符串告知用户服务启动的时间和运行时间。
    /// 还允许用户手动取消绑定并绑定到服务。
    /// </summary>
    [Service(Name = "com.xamarin.TimestampService")]
    public class TimestampService : Service ,IGetTimestamp
    {
        static readonly string TAG = typeof(TimestampService).FullName;
        IGetTimestamp timestamp;
        public IBinder Binder
        {
            get;
            private set;
        }

      

        /// <summary>
        /// 【可选的】
        /// 实例化服务。 它用于初始化服务在生存期内所需的任何变量或对象。
        /// </summary>
        public override void OnCreate()
        {
            base.OnCreate();

            Log.Debug(TAG, "OnCreate");
            timestamp = new UtcTimestamper();
        }

        /// <summary>
        /// 必须的
        /// 它在第一个客户端尝试连接到该服务时调用。 
        /// 它将返回一个实例IBinder ，以便客户端可以与该服务进行交互。 只要服务正在运行， IBinder就会使用对象来完成以后要绑定到服务的客户端请求。
        /// </summary>
        /// <param name="intent"></param>
        /// <returns></returns>
        public override IBinder OnBind(Intent intent)
        {
            Log.Debug(TAG, "OnBind");
            this.Binder = new TimestampBinder(this);
            return this.Binder;
        }

        /// <summary>
        /// 【可选的】
        /// 当所有绑定的客户端都未绑定时，将调用此方法。
        /// 
        /// </summary>
        /// <param name="intent"></param>
        /// <returns></returns>
        public override bool OnUnbind(Intent intent)
        {
            Log.Debug(TAG, "OnUnbind");
            return base.OnUnbind(intent);
        }

        /// <summary>
        /// 【可选的】
        /// 当 Android 销毁服务时，将调用此方法。 在此方法中，应执行任何必要的清理（如释放资源）
        /// </summary>
        public override void OnDestroy()
        {
            Log.Debug(TAG, "OnDestroy");
            Binder = null;
            timestamp = null;
            base.OnDestroy();
        }

        /// <summary>
        /// This method will return a formatted timestamp to the client.
        /// </summary>
        /// <returns>A string that details what time the service started and how long it has been running.</returns>
        public string GetFormattedTimestamp()
        {
            return timestamp?.GetFormattedTimestamp();
        }
    }
}