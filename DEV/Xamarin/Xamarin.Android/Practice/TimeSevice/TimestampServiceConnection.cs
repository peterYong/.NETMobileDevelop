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

namespace Practice.TimeSevice
{
    /// <summary>
    /// 实现IServiceConnection。客户端连接/断开连接
    /// </summary>
    public class TimestampServiceConnection : Java.Lang.Object, IServiceConnection, IGetTimestamp
    {
        static readonly string TAG = typeof(TimestampServiceConnection).FullName;
        public bool IsConnected { get; private set; }
        public TimestampBinder Binder { get; private set; }

        TimestampActivity timestampActivity;
        /// <summary>
        /// 绑定客户端TimestampActivity
        /// </summary>
        /// <param name="activity"></param>
        public TimestampServiceConnection(TimestampActivity activity)
        {
            IsConnected = false;
            Binder = null;
            timestampActivity = activity;
        }


       
        /// <summary>
        /// 有客户端（某个Activity）连接上时会调用此方法
        /// </summary>
        /// <param name="name"></param>
        /// <param name="service"></param>
        public void OnServiceConnected(ComponentName name, IBinder service)
        {
            Binder = service as TimestampBinder;
            IsConnected = this.Binder != null;

            string message = "onServiceConnected - ";
            Log.Debug(TAG, $"OnServiceConnected {name.ClassName}");

            if (IsConnected)
            {
                message = message + " bound to service " + name.ClassName; //服务的名字
                timestampActivity.UpdateUiForBoundService();
            }
            else
            {
                message = message + " not bound to service " + name.ClassName;
                timestampActivity.UpdateUiForUnboundService();
            }

            Log.Info(TAG, message);
            timestampActivity.timestampMessageTextView.Text = message;
        }

        public void OnServiceDisconnected(ComponentName name)
        {
            Log.Debug(TAG, $"OnServiceDisconnected {name.ClassName}");
            IsConnected = false;
            Binder = null;
            timestampActivity.UpdateUiForUnboundService();
        }

        public string GetFormattedTimestamp()
        {
            if (!IsConnected)
            {
                return null;
            }

            return Binder?.GetFormattedTimestamp();
        }
    }
}