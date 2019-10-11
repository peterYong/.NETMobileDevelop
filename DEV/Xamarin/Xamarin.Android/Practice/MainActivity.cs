using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content.Res;
using System.IO;
using Android.Graphics;
using System;
using Android.Content;

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
            TestService();

            TestAutoComplete();
        }

        private void TestAutoComplete()
        {
             string[] COUNTRIES = new string[] {
  "Afghanistan", "Albania", "Algeria", "American Samoa", "Andorra",
  "Angola", "Anguilla", "Antarctica", "Antigua and Barbuda", "Argentina",
  "Armenia", "Aruba", "Australia", "Austria", "Azerbaijan",
  "Bahrain", "Bangladesh", "Barbados", "Belarus", "Belgium",
  "Belize", "Benin", "Bermuda", "Bhutan", "Bolivia",
  "Bosnia and Herzegovina", "Botswana", "Bouvet Island", "Brazil", "British Indian Ocean Territory",
  "British Virgin Islands", "Brunei", "Bulgaria", "Burkina Faso", "Burundi",
  "Cote d'Ivoire", "Cambodia", "Cameroon", "Canada", "Cape Verde",
  "Cayman Islands", "Central African Republic", "Chad", "Chile", "China",
  "Christmas Island", "Cocos (Keeling) Islands", "Colombia", "Comoros", "Congo",
  "Cook Islands", "Costa Rica", "Croatia", "Cuba", "Cyprus", "Czech Republic",
  "Democratic Republic of the Congo", "Denmark", "Djibouti", "Dominica", "Dominican Republic",
  "East Timor", "Ecuador", "Egypt", "El Salvador", "Equatorial Guinea", "Eritrea",
  "Estonia", "Ethiopia", "Faeroe Islands", "Falkland Islands", "Fiji", "Finland",
  "Former Yugoslav Republic of Macedonia", "France", "French Guiana", "French Polynesia",
  "French Southern Territories", "Gabon", "Georgia", "Germany", "Ghana", "Gibraltar",
  "Greece", "Greenland", "Grenada", "Guadeloupe", "Guam", "Guatemala", "Guinea", "Guinea-Bissau",
  "Guyana", "Haiti", "Heard Island and McDonald Islands", "Honduras", "Hong Kong", "Hungary",
  "Iceland", "India", "Indonesia", "Iran", "Iraq", "Ireland", "Israel", "Italy", "Jamaica",
  "Japan", "Jordan", "Kazakhstan", "Kenya", "Kiribati", "Kuwait", "Kyrgyzstan", "Laos",
  "Latvia", "Lebanon", "Lesotho", "Liberia", "Libya", "Liechtenstein", "Lithuania", "Luxembourg",
  "Macau", "Madagascar", "Malawi", "Malaysia", "Maldives", "Mali", "Malta", "Marshall Islands",
  "Martinique", "Mauritania", "Mauritius", "Mayotte", "Mexico", "Micronesia", "Moldova",
  "Monaco", "Mongolia", "Montserrat", "Morocco", "Mozambique", "Myanmar", "Namibia",
  "Nauru", "Nepal", "Netherlands", "Netherlands Antilles", "New Caledonia", "New Zealand",
  "Nicaragua", "Niger", "Nigeria", "Niue", "Norfolk Island", "North Korea", "Northern Marianas",
  "Norway", "Oman", "Pakistan", "Palau", "Panama", "Papua New Guinea", "Paraguay", "Peru",
  "Philippines", "Pitcairn Islands", "Poland", "Portugal", "Puerto Rico", "Qatar",
  "Reunion", "Romania", "Russia", "Rwanda", "Sqo Tome and Principe", "Saint Helena",
  "Saint Kitts and Nevis", "Saint Lucia", "Saint Pierre and Miquelon",
  "Saint Vincent and the Grenadines", "Samoa", "San Marino", "Saudi Arabia", "Senegal",
  "Seychelles", "Sierra Leone", "Singapore", "Slovakia", "Slovenia", "Solomon Islands",
  "Somalia", "South Africa", "South Georgia and the South Sandwich Islands", "South Korea",
  "Spain", "Sri Lanka", "Sudan", "Suriname", "Svalbard and Jan Mayen", "Swaziland", "Sweden",
  "Switzerland", "Syria", "Taiwan", "Tajikistan", "Tanzania", "Thailand", "The Bahamas",
  "The Gambia", "Togo", "Tokelau", "Tonga", "Trinidad and Tobago", "Tunisia", "Turkey",
  "Turkmenistan", "Turks and Caicos Islands", "Tuvalu", "Virgin Islands", "Uganda",
  "Ukraine", "United Arab Emirates", "United Kingdom",
  "United States", "United States Minor Outlying Islands", "Uruguay", "Uzbekistan",
  "Vanuatu", "Vatican City", "Venezuela", "Vietnam", "Wallis and Futuna", "Western Sahara",
  "Yemen", "Yugoslavia", "Zambia", "Zimbabwe"
};

            AutoCompleteTextView textView = FindViewById<AutoCompleteTextView>(Resource.Id.autocomplete_country);
            var adapter = new ArrayAdapter<string>(this, Resource.Layout.HelloAutoComplete, COUNTRIES);

            textView.Adapter = adapter;
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
            //EditText editText = FindViewById<EditText>(Resource.Id.editFirstName);
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

        /// <summary>
        /// 测试Android服务
        /// </summary>
        /// <param name="bundle"></param>
        private void TestService()
        {
            Button testServiceButton = FindViewById<Button>(Resource.Id.testService);

            testServiceButton.Click += (sender, e) =>
            {
                //创建一个Intent，传入当前上下文（this，用于引用当前上下文）以及你所查找的应用程序块的类型 (TimestampActivity)：
                var intent = new Intent(this, typeof(TimestampActivity));
                StartActivity(intent);
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