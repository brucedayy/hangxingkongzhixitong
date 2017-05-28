#region

using System;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using NC.Control.ControlzEx;

#endregion

namespace NC.Control.IconPacks
{
    [MarkupExtensionReturnType(typeof(PackIconBase))]
    public class PackIconExtension : MarkupExtension
    {
        public PackIconExtension()
        {
        }

        public PackIconExtension(Enum kind)
        {
            Kind = kind;
        }

        [ConstructorArgument("kind")]
        public Enum Kind { get; set; }

        public double? Width { get; set; }
        public double? Height { get; set; }
        public PackIconFlipOrientation? Flip { get; set; }
        public double? Rotation { get; set; }
        public bool? Spin { get; set; }
        public bool? SpinAutoReverse { get; set; }
        public IEasingFunction SpinEasingFunction { get; set; }
        public double? SpinDuration { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (this.Kind is PackIconMetroKind)
            {
                return this.GetPackIcon<PackIconMetro, PackIconMetroKind>((PackIconMetroKind)this.Kind);
            }
            return null;
        }

        private PackIcon<TKind> GetPackIcon<TPack, TKind>(TKind kind) where TPack : PackIcon<TKind>, new()
        {
            var packIcon = new TPack { Kind = kind };
            if (Width != null)
                packIcon.Width = Width.Value;
            if (Height != null)
                packIcon.Height = Height.Value;
            if (Flip != null)
                packIcon.Flip = Flip.Value;
            if (Rotation != null)
                packIcon.Rotation = Rotation.Value;
            if (Spin != null)
                packIcon.Spin = Spin.Value;
            if (SpinAutoReverse != null)
                packIcon.SpinAutoReverse = SpinAutoReverse.Value;
            if (SpinEasingFunction != null)
                packIcon.SpinEasingFunction = SpinEasingFunction;
            if (SpinDuration != null)
                packIcon.SpinDuration = SpinDuration.Value;
            return packIcon;
        }
    }

    [MarkupExtensionReturnType(typeof(PackIconBase))]
    public class PackIconExtension<TPack, TKind> : MarkupExtension where TPack : PackIcon<TKind>, new()
    {
        public PackIconExtension()
        {
        }

        public PackIconExtension(TKind kind)
        {
            Kind = kind;
        }

        [ConstructorArgument("kind")]
        public TKind Kind { get; set; }

        public double? Width { get; set; }
        public double? Height { get; set; }
        public PackIconFlipOrientation? Flip { get; set; }
        public double? Rotation { get; set; }
        public bool? Spin { get; set; }
        public bool? SpinAutoReverse { get; set; }
        public IEasingFunction SpinEasingFunction { get; set; }
        public double? SpinDuration { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var packIcon = new TPack { Kind = Kind };

            if (Width != null)
                packIcon.Width = Width.Value;
            if (Height != null)
                packIcon.Height = Height.Value;
            if (Flip != null)
                packIcon.Flip = Flip.Value;
            if (Rotation != null)
                packIcon.Rotation = Rotation.Value;
            if (Spin != null)
                packIcon.Spin = Spin.Value;
            if (SpinAutoReverse != null)
                packIcon.SpinAutoReverse = SpinAutoReverse.Value;
            if (SpinEasingFunction != null)
                packIcon.SpinEasingFunction = SpinEasingFunction;
            if (SpinDuration != null)
                packIcon.SpinDuration = SpinDuration.Value;
            return packIcon;
        }
    }
}