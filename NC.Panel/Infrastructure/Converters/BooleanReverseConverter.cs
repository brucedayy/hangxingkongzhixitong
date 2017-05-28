using System;
using System.Windows;
using System.Windows.Data;

namespace NC.Panel.Infrastructure.Converters
{
    /// <summary>
    /// 布尔类型值取反转换器。
    /// </summary>
    public class BooleanReverseConverter : IValueConverter
    {
        /// <summary>
        /// 正向转换。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool result = System.Convert.ToBoolean(value);
            return !result;
        }

        /// <summary>
        /// 反向转换。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
