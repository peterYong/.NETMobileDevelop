using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content.Res;
using System.IO;
using Android.Graphics;
using System;

namespace Practice
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        //参数Bundle：在绑定不为 null 时用于存储和传递状态信息和活动之间的对象的字典，这表示活动正在重新启动，应从上一个实例还原其状态
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            TestButton();
            TestEditView();
            TestAsset();
            TestFont();
            TestBundle(savedInstanceState);

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        #region 测试代码

        private void TestButton()
        {
            Button button = FindViewById<Button>(Resource.Id.saveButton);
            int count = 0;
            button.Click += (sender, e) =>
            {
                button.Text = string.Format("{0} clicks!", count++);
                button.AnnounceForAccessibility(button.Text);
            };
        }

        private void TestEditView()
        {
            TextView textView = FindViewById<TextView>(Resource.Id.someText);
            EditText editText = FindViewById<EditText>(Resource.Id.editFirstName);
            textView.LabelFor = Resource.Id.editFirstName;
        }

        private void TestAsset()
        {
            //读取asset
            //TextView textView1 = new TextView(this);
            string content;
            AssetManager asset = this.Assets;
            using (StreamReader sr = new StreamReader(asset.Open("read_asset.txt")))
            {
                content = sr.ReadToEnd();
            }
            //textView1.Text = content;
            //SetContentView(textView1);  //整个界面就是textView1的内容了
            TextView readAsset = FindViewById<TextView>(Resource.Id.readAsset);
            readAsset.Text = content;
        }

        private void TestFont()
        {
            //Font
            TextView textViewFont = FindViewById<TextView>(Resource.Id.textViewFont);
            //Android.Graphics.Typeface typeface = this.Resources.GetFont(Resource.Font.SourceSansPro_bold); //GetFont方法已经不能使用
            //textViewFont.Typeface = typeface;
            //textViewFont.Text = "Changed the font";

            var typeface = Typeface.Create("<FONT FAMILY NAME>", Android.Graphics.TypefaceStyle.Bold);
            textViewFont.Typeface = typeface;

            Button btnTransferFont = FindViewById<Button>(Resource.Id.btnTransferFont);
            int times = 0;
            btnTransferFont.Click += (sender, e) =>
            {
                times++;
                if (times % 2 == 0)
                {
                    typeface = Typeface.Create("<FONT FAMILY NAME>", Android.Graphics.TypefaceStyle.Bold);
                }
                else
                {
                    typeface = Typeface.Create("<FONT FAMILY NAME>", Android.Graphics.TypefaceStyle.Italic);
                }
                textViewFont.Typeface = typeface;
            };
        }

        int counter = 0;
        /// <summary>
        /// 测试保存实例状态
        /// </summary>
        private void TestBundle(Bundle bundle)
        {
            Button button = FindViewById<Button>(Resource.Id.testBundle);
            
            if (bundle != null)
            {
                counter = bundle.GetInt("counter", 0);
            }
            else
            {
                counter = 0;
            }
            string info = this.Resources.GetString(Resource.String.button_bundle_info);
            button.Text = $"{info},clicked {counter} times";

            button.Click += (sender, e) =>
            {
                button.Text = $"{info},clicked {++counter} times";
            };
        }

        #endregion

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutInt("counter", counter);
            base.OnSaveInstanceState(outState);
        }
    }
}