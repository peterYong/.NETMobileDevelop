using BaseControls.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BaseControls.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class List_ListView : ContentPage
    {
        public List_ListView()
        {
            InitializeComponent();

            BindingContext = new List_ListViewViewModel();
            //  LoadData();
        }

        private void LoadData()
        {
            ObservableCollection<Flower> fl = new ObservableCollection<Flower>();
            List<Flower> flowers = new List<Flower>();
            flowers.Add(new Flower() { Id = 1, Name = "花名1", Growth = "3个月", Location = "生长地1", Picture = "flower.jpg" });
            flowers.Add(new Flower() { Id = 2, Name = "花名2", Growth = "5个月", Location = "生长地2", Picture = "flower.jpg" });
            flowers.Add(new Flower() { Id = 3, Name = "花名3", Growth = "6个月", Location = "生长地3", Picture = "flower.jpg" });
            flowers.Add(new Flower() { Id = 4, Name = "花名4", Growth = "8个月", Location = "生长地4", Picture = "flower.jpg" });
            flowers.Add(new Flower() { Id = 5, Name = "花名5", Growth = "2个月", Location = "生长地5", Picture = "flower.jpg" });
            flowers.Add(new Flower() { Id = 6, Name = "花名6", Growth = "1个月", Location = "生长地6", Picture = "flower.jpg" });

            lvFlowers.ItemsSource = flowers;
        }

        private async void lvFlowers_Refreshing(object sender, EventArgs e)
        {
            await Task.Delay(3000);
            lvFlowers.IsRefreshing = false;
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {

        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }

    public class List_ListViewViewModel : ViewModelBase
    {
        public ICommand RefleshCommand { get; set; }
        public ICommand ChangedCommand { get; set; }

        public List_ListViewViewModel()
        {
            RefleshCommand = new Command(Reflesh);
            ChangedCommand = new Command(Change);

            Flowers = new ObservableCollection<Flower>();
            Flowers.Add(new Flower() { Id = 1, Name = "花名1", Growth = "3个月", Location = "生长地1", Picture = "flower.jpg" });
            Flowers.Add(new Flower() { Id = 2, Name = "花名2", Growth = "5个月", Location = "生长地2", Picture = "flower.jpg" });
            Flowers.Add(new Flower() { Id = 3, Name = "花名3", Growth = "6个月", Location = "生长地3", Picture = "flower.jpg" });
            Flowers.Add(new Flower() { Id = 4, Name = "花名4", Growth = "8个月", Location = "生长地4", Picture = "flower.jpg" });
            Flowers.Add(new Flower() { Id = 5, Name = "花名5", Growth = "2个月", Location = "生长地5", Picture = "flower.jpg" });
            Flowers.Add(new Flower() { Id = 6, Name = "花名6", Growth = "1个月", Location = "生长地6", Picture = "flower.jpg" });
        }

        private ObservableCollection<Flower> flowers;

        public ObservableCollection<Flower> Flowers
        {
            get { return flowers; }
            set { SetProperty(ref flowers, value); }
        }

        private void Change(object obj)
        {
            Flowers[1].Name = "更改花名";
        }

        private void Reflesh(object obj)
        {

        }
    }

    public class Flower : ViewModelBase
    {
        public int Id { get; set; }
        private string name;

        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        public string Growth { get; set; }
        public string Location { get; set; }
        public string Picture { get; set; }
    }
}