using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace NC.Panel.Infrastructure.Converters
{
    /// <summary>
    /// 复合转换器，通过一个Converter将多个子Converter组合起来。
    /// </summary>
    [ContentProperty("Converters")]
    public class CompositeConverter : IValueConverter
    {
        /// <summary>
        /// 正向转换。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            foreach (IValueConverter converter in Converters)
            {
                value = converter.Convert(value, targetType, parameter, culture);
                if (value == DependencyProperty.UnsetValue || value == Binding.DoNothing)
                    break;
            }
            return value;
        }

        /// <summary>
        /// 反向转换。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            for (int index = Converters.Count - 1; index >= 0; index--)
            {
                value = Converters[index].ConvertBack(value, targetType, parameter, culture);
                if (value == DependencyProperty.UnsetValue || value == Binding.DoNothing)
                    break;
            }
            return value;
        }

        /// <summary>
        /// 获取子转换器集合。
        /// </summary>
        public List<IValueConverter> Converters
        {
            get { return _converters; }
        }

        private List<IValueConverter> _converters = new List<IValueConverter>();
    }
}