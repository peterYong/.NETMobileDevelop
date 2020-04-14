using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FromPractice.Controls
{
   public class MySwithCell: ViewCell
    {
        public event EventHandler<ToggledEventArgs> OnChanged;

        private MySwitch swi;

        public string Text { get; set; }
        public bool On { get; set; }

        public MySwithCell(string label, bool on,string detail, bool isEnabled = true)
        {
            this.On = on;
            Text = label;
            Label lab = new Label
            {
                Text = Text,
                FontSize = 14,
                HorizontalOptions = LayoutOptions.Start,
                VerticalTextAlignment = TextAlignment.Center,
            };

            swi = new MySwitch
            {
                WidthRequest = 53,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.EndAndExpand,
            };
            swi.Stagechange = Statechange;
            swi.on = on;
            Label l2 = new Label
            {
                Text = detail,
                FontSize = 14,
                HorizontalOptions = LayoutOptions.Start,
                VerticalTextAlignment = TextAlignment.Center,
            };

            StackLayout s1 = new StackLayout()
            {
                Padding = new Thickness(10, 0, 0, 0),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
                Orientation = StackOrientation.Horizontal,
                Children = { lab, swi }
            };

            View = new StackLayout
            {
                Children = { s1,l2 }
            };
        }

        private void Statechange(bool isOn)
        {
            this.On = isOn;
            OnChanged?.Invoke(this, new ToggledEventArgs(isOn));
        }
    }

    public class MySwitch : View
    {
        public bool on { get; set; }
        public Action<bool> Stagechange;

    }
}
