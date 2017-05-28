using System.Windows.Media;

namespace NC.Panel.Infrastructure.Extensions
{
    /// <summary>
    /// 颜色类型转换帮助类。
    /// </summary>
    public static class ColorExt
    {
        public static Brush ConvertToBrush(this string htmlColor)
        {
            BrushConverter brushConverter = new BrushConverter();
            Brush brush = (Brush)brushConverter.ConvertFromString(htmlColor);
            return brush;
        }

        public static Brush ConvertToBrush(this Color color)
        {
            return new SolidColorBrush(color);
        }

        public static Color ConvertToColor(this string htmlColor)
        {
            Color color = (Color)ColorConverter.ConvertFromString(htmlColor);
            return color;
        }

        public static Color ConvertToColor(this Brush brush)
        {
            Color color = ((SolidColorBrush)brush).Color;
            return color;
        }
    }
}