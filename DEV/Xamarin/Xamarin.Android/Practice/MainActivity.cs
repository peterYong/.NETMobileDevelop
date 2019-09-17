using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;

namespace Practice
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            TextView textView = FindViewById<TextView>(Resource.Id.someText);
            EditText editText = FindViewById<EditText>(Resource.Id.editFirstName);
            textView.LabelFor = Resource.Id.editFirstName;

            Button button = FindViewById<Button>(Resource.Id.saveButton);
            int count = 0;
            button.Click += (sender, e) =>
            {
                button.Text = string.Format("{0} clicks!", count++);
                button.AnnounceForAccessibility(button.Text);
            };


        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}