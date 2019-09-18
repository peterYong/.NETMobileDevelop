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
using Practice.TimeSevice;

namespace Practice
{
    /// <summary>
    /// 时间戳Activity【客户端】
    /// </summary>
    [Activity(Label = "TimestampActivity", MainLauncher = true)]
    public class TimestampActivity : Activity
    {
        Button timestampButton;
        Button stopServiceButton;
        Button restartServiceButton;
        internal TextView timestampMessageTextView;

        TimestampServiceConnection serviceConnection;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Timestamp);

            timestampButton = FindViewById<Button>(Resource.Id.get_timestamp_button);
            timestampButton.Click += GetTimestampButton_Click;

            stopServiceButton = FindViewById<Button>(Resource.Id.stop_timestamp_service_button);
            stopServiceButton.Click += StopServiceButton_Click;

            restartServiceButton = FindViewById<Button>(Resource.Id.restart_timestamp_service_button);
            restartServiceButton.Click += RestartServiceButton_Click;

            timestampMessageTextView = FindViewById<TextView>(Resource.Id.message_textview);
        }

        protected override void OnStart()
        {
            base.OnStart();
            if (serviceConnection == null)
            {
                serviceConnection = new TimestampServiceConnection(this); 
            }
            DoBindService();//客户端启动并连接服务【或者说服务绑定客户端】
        }

        protected override void OnResume()
        {
            base.OnResume();
            if (serviceConnection.IsConnected)
            {
                UpdateUiForBoundService();
            }
            else
            {
                UpdateUiForUnboundService();
            }
        }

        protected override void OnPause()
        {
            timestampButton.Click -= GetTimestampButton_Click;
            stopServiceButton.Click -= StopServiceButton_Click;
            restartServiceButton.Click -= RestartServiceButton_Click;

            base.OnPause();
        }

        protected override void OnStop()
        {
            DoUnBindService();
            base.OnStop();
        }

        /// <summary>
        /// 绑定时 更新ui
        /// </summary>
        internal void UpdateUiForBoundService()
        {
            timestampButton.Enabled = true;
            stopServiceButton.Enabled = true;
            restartServiceButton.Enabled = false;

        }

        /// <summary>
        /// 解除绑定时 更新ui
        /// </summary>
        internal void UpdateUiForUnboundService()
        {
            timestampButton.Enabled = false;
            stopServiceButton.Enabled = false;
            restartServiceButton.Enabled = true;
        }

        /// <summary>
        /// 绑定服务
        /// </summary>
        void DoBindService()
        {
            Intent serviceToStart = new Intent(this, typeof(TimestampService));
            //BindService是对 Android 操作系统的请求，用于启动服务并将客户端绑定到它。
            BindService(serviceToStart, serviceConnection, Bind.AutoCreate);
            timestampMessageTextView.Text = "";
        }

        void DoUnBindService()
        {
            UnbindService(serviceConnection);
            restartServiceButton.Enabled = true;
            timestampMessageTextView.Text = "";
        }

        void GetTimestampButton_Click(object sender, System.EventArgs e)
        {
            if (serviceConnection.IsConnected)
            {
                timestampMessageTextView.Text = serviceConnection.Binder.Service.GetFormattedTimestamp();
            }
            else
            {
                timestampMessageTextView.SetText(Resource.String.service_not_connected);
            }
        }

        void StopServiceButton_Click(object sender, System.EventArgs e)
        {
            DoUnBindService();
            UpdateUiForUnboundService();
        }

        void RestartServiceButton_Click(object sender, System.EventArgs e)
        {
            DoBindService();
            UpdateUiForBoundService();
        }
    }
}