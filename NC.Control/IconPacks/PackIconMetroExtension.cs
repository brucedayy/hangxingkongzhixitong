using System.Windows.Markup;

namespace NC.Control.IconPacks
{
    [MarkupExtensionReturnType(typeof(PackIconMetro))]
    public class PackIconMetroExtension : PackIconExtension<PackIconMetro, PackIconMetroKind>
    {
        public PackIconMetroExtension()
        {
        }

        public PackIconMetroExtension(PackIconMetroKind kind) : base(kind)
        {
        }
    }
}