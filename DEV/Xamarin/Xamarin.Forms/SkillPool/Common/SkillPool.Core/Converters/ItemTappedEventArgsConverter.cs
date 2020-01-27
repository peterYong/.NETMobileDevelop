using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace SkillPool.Core.Converters
{
    public class ItemTappedEventArgsConverter : IValueConverter
    {
        private const string Message = "Expected TappedEventArgs as value";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is ItemTappedEventArgs eventArgs))
            {
                throw new ArgumentException(Message, nameof(value));
            }
            return eventArgs.Item; //ItemTapped的事件参数对象
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
