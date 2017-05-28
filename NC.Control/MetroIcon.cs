using System.Windows;
using System.Windows.Media;

namespace NC.Control
{
    public sealed class MetroIcon : System.Windows.Controls.Control
    {
        public Visual Visual
        {
            get { return (Visual)GetValue(VisualProperty); }
            set { SetValue(VisualProperty, value); }
        }

        public static readonly DependencyProperty VisualProperty =
            DependencyProperty.Register("Visual", typeof(Visual), typeof(MetroIcon), new PropertyMetadata(default(Visual)));
        
        public Brush Fill
        {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        public static readonly DependencyProperty FillProperty =
            DependencyProperty.Register("Fill", typeof(Brush), typeof(MetroIcon), new PropertyMetadata(default(Brush)));

    }
}