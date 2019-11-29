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
    public partial class List_ListView_Group : ContentPage
    {
        public List_ListView_Group()
        {
            InitializeComponent();

            BindingContext = new List_ListView_GroupViewModel();
        }
    }

    public class List_ListView_GroupViewModel : ViewModelBase
    {
        public ICommand RefleshCommand { get; set; }
        public ICommand ChangedCommand { get; set; }

        public List_ListView_GroupViewModel()
        {
            RefleshCommand = new Command(Reflesh);
            ChangedCommand = new Command(Change);

            List<Flower> flowers = new List<Flower>();
            flowers.Add(new Flower() { Id = 1, Name = "花名1", Growth = "3个月", Location = "生长地1", Picture = "flower.jpg" });
            flowers.Add(new Flower() { Id = 2, Name = "花名2", Growth = "5个月", Location = "生长地2", Picture = "flower.jpg" });
            flowers.Add(new Flower() { Id = 3, Name = "花名3", Growth = "6个月", Location = "生长地3", Picture = "flower.jpg" });
            flowers.Add(new Flower() { Id = 4, Name = "花名4", Growth = "8个月", Location = "生长地4", Picture = "flower.jpg" });
            flowers.Add(new Flower() { Id = 5, Name = "花名5", Growth = "2个月", Location = "生长地5", Picture = "flower.jpg" });
            flowers.Add(new Flower() { Id = 6, Name = "花名6", Growth = "1个月", Location = "生长地6", Picture = "flower.jpg" });
            Flowers = new ObservableCollection<FlowerGroup>();

            FlowerGroup group1 = new FlowerGroup("春季花", "C");
            group1.AddRange(flowers.Take(2));

            FlowerGroup group2 = new FlowerGroup("夏季花", "X");
            group2.AddRange(flowers.Skip(2).Take(2));

            FlowerGroup group3 = new FlowerGroup("秋季花", "Q");
            group3.AddRange(flowers.Skip(4).Take(1));

            FlowerGroup group4 = new FlowerGroup("冬季花", "D");
            group4.AddRange(flowers.Skip(5).Take(1));
            
            Flowers.Add(group1);
            Flowers.Add(group2);
            Flowers.Add(group3);
            Flowers.Add(group4);
        }

        private ObservableCollection<FlowerGroup> flowers;

        public ObservableCollection<FlowerGroup> Flowers
        {
            get { return flowers; }
            set { SetProperty(ref flowers, value); }
        }

        private void Change(object obj)
        {
            Flowers[1][0].Name = "更改名称";
        }

        private void Reflesh(object obj)
        {

        }
    }

    public class FlowerGroup : List<Flower>
    {
        public string Title { get; set; }
        public string ShortName { get; set; }
        public string SubTitle { get; set; }
        public FlowerGroup(string title, string shortName)
        {
            Title = title;
            ShortName = shortName;
        }
        public IList<FlowerGroup> All { private get; set; }
    }
}