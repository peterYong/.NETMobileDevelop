using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace SkillPool.Core.Converters
{
    /// <summary>
    /// 反向的 值到bool转换
    /// </summary>
    public class InverseBoolConverter : IValueConverter
    {
        private const string Message = "The target must be a boolean";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(!(value is bool))
            {
                throw new InvalidOperationException(Message);
            }
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw null;
        }
    }
}
