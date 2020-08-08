using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AwesomeApp.MyEntry
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AutoComplete : ContentView
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(AutoComplete), default(string), BindingMode.TwoWay, null, propertyChanged: OnTextChanged);

        private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            throw new NotImplementedException();
        }

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(AutoComplete), default(string), BindingMode.TwoWay, null, propertyChanged: OnPlaceholderChanged);
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        void OnTextChanged(object _, TextChangedEventArgs e)
        {
            Text = e.NewTextValue;
            //SearchCommand?.Execute(Text);
        }
        private static void OnPlaceholderChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is AutoComplete AutoCompleteView)
            {
                AutoCompleteView.Placeholder = (string)newValue;
                //AutoCompleteView.SearchInput.Placeholder = AutoCompleteView.Placeholder;
            }
        }



        public AutoComplete()
        {
            InitializeComponent();
        }

        private void editor_KeyDown()
        {

        }
    }
}