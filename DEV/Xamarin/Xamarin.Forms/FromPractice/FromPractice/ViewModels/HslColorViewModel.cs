using FromPractice.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace FromPractice.ViewModels
{
    /// <summary>
    /// （颜色改变）视图模型
    /// 五个属性：Hue、Saturation、Luminosity 和 Color 属性是相互关联的。 当这三个颜色组件中的任何一个更改值时，都将重新计算 Color 属性，
    /// 并为所有四个属性触发 PropertyChanged 事件
    /// </summary>
    public class HslColorViewModel : INotifyPropertyChanged
    {
        //用于显示颜色
        Color color;
        //用于显示颜色值String
        string name;
        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            private set
            {
                if (name != value)
                {
                    name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
                }
            }
            get
            {
                return name;
            }
        }

        public Color Color
        {
            set
            {
                if(color!=value)
                {
                    color = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Hue"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Saturation"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Luminosity"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Color"));

                    Name = NamedColor.GetNearestColorName(color);
                }
            }
            get
            {
                return color; //必须得有，否则控件获取不到颜色 而显示成一片空白
            }
        }

        /// <summary>
        /// 颜色的色调
        /// </summary>
        public double Hue
        {
            get
            {
                return color.Hue;
            }
            set
            {
                if(color.Hue!=value)   //属性值更改时，Color属性将重新计算
                {
                    Color = Color.FromHsla(value, color.Saturation, color.Luminosity);
                }
            }
        }
        /// <summary>
        /// 颜色的饱和度
        /// </summary>
        public double Saturation
        {
            set
            {
                if (color.Saturation != value)
                {
                    Color = Color.FromHsla(color.Hue, value, color.Luminosity);
                }
            }
            get
            {
                return color.Saturation;
            }
        }
        /// <summary>
        /// 颜色的亮度
        /// </summary>
        public double Luminosity
        {
            set
            {
                if (color.Luminosity != value)
                {
                    Color = Color.FromHsla(color.Hue, color.Saturation, value);
                }
            }
            get
            {
                return color.Luminosity;
            }
        }

    }
}
