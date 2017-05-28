using System.Windows;
using System.Windows.Controls;

namespace NC.Control
{
    /// <summary>
    /// User a colour zone to easily switch the background and foreground colours, whilst still remaining within the selected Material Design palette.
    /// </summary>
    public class ColorZone : ContentControl
    {
        static ColorZone()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorZone), new FrameworkPropertyMetadata(typeof(ColorZone)));
        }

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            "CornerRadius", typeof(CornerRadius), typeof(ColorZone), new PropertyMetadata(default(CornerRadius)));

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius) GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
    }
}
