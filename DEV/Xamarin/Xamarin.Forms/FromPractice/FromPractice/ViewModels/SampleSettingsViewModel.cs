using FromPractice.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace FromPractice.ViewModels
{
    public class SampleSettingsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        string name;
        DateTime birthDate;
        bool codesInCSharp;
        double numberOfCopies;
        NamedColor backgroundNamedColor;


        public SampleSettingsViewModel(IDictionary<string, object> dictionary)
        {
            Name = GetDictionaryEntry<string>(dictionary, "Name");
            BirthDate = GetDictionaryEntry(dictionary, "BirthDate", new DateTime(1980, 1, 1));
            CodesInCSharp = GetDictionaryEntry<bool>(dictionary, "CodesInCSharp");
            NumberOfCopies = GetDictionaryEntry(dictionary, "NumberOfCopies", 1.0);
            BackgroundNamedColor = NamedColor.Find(GetDictionaryEntry(dictionary, "BackgroundNamedColor", "White"));
        }

        public string Name
        {
            set { SetProperty(ref name, value); }
            get { return name; }
        }

        public DateTime BirthDate
        {
            set { SetProperty(ref birthDate, value); }
            get { return birthDate; }
        }

        public bool CodesInCSharp
        {
            set { SetProperty(ref codesInCSharp, value); }
            get { return codesInCSharp; }
        }

        public double NumberOfCopies
        {
            set { SetProperty(ref numberOfCopies, value); }
            get { return numberOfCopies; }
        }

        public NamedColor BackgroundNamedColor
        {
            set
            {
                if (SetProperty(ref backgroundNamedColor, value))
                {
                    OnPropertyChanged("BackgroundColor");
                }
            }
            get { return backgroundNamedColor; }
        }

        public Color BackgroundColor
        {
            get { return BackgroundNamedColor?.Color ?? Color.White; }
        }

        public void SaveState(IDictionary<string, object> dictionary)
        {
            dictionary["Name"] = Name;
            dictionary["BirthDate"] = BirthDate;
            dictionary["CodesInCSharp"] = CodesInCSharp;
            dictionary["NumberOfCopies"] = NumberOfCopies;
            dictionary["BackgroundNamedColor"] = BackgroundNamedColor.Name;
        }

        T GetDictionaryEntry<T>(IDictionary<string, object> dictionary, string key, T defaultValue = default(T))
        {
            return dictionary.ContainsKey(key) ? (T)dictionary[key] : defaultValue;
        }

        bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// 属性改变事件，设置为调用属性的可选参数。，这可以避免在将属性的名称指定为字符串时出现拼写错误。
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
