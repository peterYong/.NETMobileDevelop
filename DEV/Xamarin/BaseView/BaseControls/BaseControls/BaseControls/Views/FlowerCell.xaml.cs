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
    public partial class FlowerCell : ViewCell
    {
        public static readonly BindableProperty ItemProperty = BindableProperty.Create("Item", typeof(Flower), typeof(FlowerCell), default(Flower));

        public Flower Item
        {
            get { return (Flower)GetValue(ItemProperty); }
            set { SetValue(ItemProperty, value); }
        }


        public FlowerCell()
        {
            InitializeComponent();

            BindingContext = Item;
        }

        //protected override void OnBindingContextChanged()
        //{
        //    base.OnBindingContextChanged();

        //    if (BindingContext != null)
        //    {
        //        img.Source = Item.Picture;
        //        lblName.Text = Item.Name;
        //        lblGrowth.Text = Item.Growth;
        //        lblLocation.Text = Item.Location;
        //    }
        //}
    }
}