using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace AndroidDevice.Droid
{
    [Activity(Label = "AndroidDevice", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            System.Diagnostics.Debug.WriteLine(TAG + "OnCreate");

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            //测试签名
            SignCheck signCheck = new SignCheck(this, "27:19:6E:38:6B:87:5E:76:AD:F7:00:E7:EA:84:E4:C6:EE:E3:3D:FA");
            if (signCheck.check())
            {
                //TODO 签名正常
            }
            else
            {
                //TODO 签名不正确
                Toast.MakeText(this, "请前往官方渠道下载正版 app", ToastLength.Long);
            }

            LoadApplication(new App(signCheck.cer));
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        static readonly string TAG = "Debug:" + typeof(MainActivity).Name + ":";



        protected override void OnStart()
        {
            base.OnStart();
            System.Diagnostics.Debug.WriteLine(TAG + "OnStart");
        }

        protected override void OnResume()
        {
            base.OnResume();
            System.Diagnostics.Debug.WriteLine(TAG + "OnResume");
        }

        protected override void OnRestart()
        {
            base.OnRestart();
            System.Diagnostics.Debug.WriteLine(TAG + "OnRestart");
        }

        protected override void OnPause()
        {
            base.OnPause();
            System.Diagnostics.Debug.WriteLine(TAG + "OnPause");
        }

        protected override void OnStop()
        {
            base.OnStop();
            System.Diagnostics.Debug.WriteLine(TAG + "OnStop");
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            System.Diagnostics.Debug.WriteLine(TAG + "OnDestroy");
        }



    }
}