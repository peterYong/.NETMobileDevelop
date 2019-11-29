using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BaseControls.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Layout_Flex : ContentPage
    {
        int count = 6;
        List<Color> colors = new List<Color>() { Color.Red, Color.Blue, Color.Green, Color.Gray, Color.LightBlue, Color.LightGreen };

        public Layout_Flex()
        {
            InitializeComponent();

            stepper.Value = count;
            CreateItems(count);
        }

        private void stepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            CreateItems((int)stepper.Value);
        }

        private void pkDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            ItemChanged(typeof(FlexDirection), pkDirection, FlexLayout.DirectionProperty);
        }

        private void pkWrap_SelectedIndexChanged(object sender, EventArgs e)
        {
            ItemChanged(typeof(FlexWrap), pkWrap, FlexLayout.WrapProperty);
        }

        private void pkJustifyContent_SelectedIndexChanged(object sender, EventArgs e)
        {
            ItemChanged(typeof(FlexJustify), pkJustifyContent, FlexLayout.JustifyContentProperty);
        }

        private void pkAlignContent_SelectedIndexChanged(object sender, EventArgs e)
        {
            ItemChanged(typeof(FlexAlignContent), pkAlignContent, FlexLayout.AlignContentProperty);
        }

        private void pkAlignItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            ItemChanged(typeof(FlexAlignItems), pkAlignItems, FlexLayout.AlignItemsProperty);
        }

        private void ItemChanged(Type type, Picker picker, BindableProperty property)
        {
            string value = picker.SelectedItem.ToString();
            var obj = Enum.Parse(type, value);
            ChangeProperty(property, obj);
        }

        private void CreateItems(int count)
        {
            flLayout.Children.Clear();
            for (int i = 0; i < count; i++)
            {
                BoxView bv = new BoxView();
                bv.WidthRequest = 100;
                bv.HeightRequest = 100;
                int index = i % colors.Count;
                bv.BackgroundColor = colors[index];

                flLayout.Children.Add(bv);
            }
            lblCount.Text = count.ToString();
        }

        private void ChangeProperty(BindableProperty property, object value)
        {
            try
            {
                flLayout.SetValue(property, value);
            }
            catch (Exception ex)
            {

            }
        }

    }
}