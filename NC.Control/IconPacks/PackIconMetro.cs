
using System.Windows;


namespace NC.Control.IconPacks
{
    /// <summary>
    /// Icons from the Material Design Icons project.
    /// </summary>
    public class PackIconMetro : PackIcon<PackIconMetroKind>
    {
        static PackIconMetro()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconMetro), new FrameworkPropertyMetadata(typeof(PackIconMetro)));
        }

        public PackIconMetro() : base(PackIconMetroDataFactory.Create)
        {
        }
    }
}