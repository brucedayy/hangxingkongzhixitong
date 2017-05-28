using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using NC.Control.ControlzEx;

namespace NC.Control.IconPacks
{
    /// <summary>
    ///     Enum PackIconFlipOrientation for the Flip property of the PackIcon
    /// </summary>
    public enum PackIconFlipOrientation
    {
        /// <summary>
        ///     No flip
        /// </summary>
        Normal,

        /// <summary>
        ///     Flip the icon horizontal
        /// </summary>
        Horizontal,

        /// <summary>
        ///     Flip the icon vertical
        /// </summary>
        Vertical,

        /// <summary>
        ///     Flip the icon vertical and horizontal
        /// </summary>
        Both
    }

    /// <summary>
    ///     Class PackIcon which is the custom base class of MahApps.Metro.IconPacks.
    /// </summary>
    /// <typeparam name="TKind">The type of the enum kind.</typeparam>
    /// <seealso cref="PackIconBase{TKind}" />
    public class PackIcon<TKind> : PackIconBase<TKind>
    {
        /// <summary>
        ///     Identifies the Flip dependency property.
        /// </summary>
        public static readonly DependencyProperty FlipProperty
            = DependencyProperty.Register(
                "Flip",
                typeof(PackIconFlipOrientation),
                typeof(PackIcon<TKind>),
                new PropertyMetadata(PackIconFlipOrientation.Normal));

        /// <summary>
        ///     Identifies the Rotation dependency property.
        /// </summary>
        public static readonly DependencyProperty RotationProperty
            = DependencyProperty.Register(
                "Rotation",
                typeof(double),
                typeof(PackIcon<TKind>),
                new PropertyMetadata(0d, null, RotationPropertyCoerceValueCallback));

        /// <summary>
        ///     Identifies the Spin dependency property.
        /// </summary>
        public static readonly DependencyProperty SpinProperty
            = DependencyProperty.Register(
                "Spin",
                typeof(bool),
                typeof(PackIcon<TKind>),
                new PropertyMetadata(default(bool), SpinPropertyChangedCallback, SpinPropertyCoerceValueCallback));

        private static readonly string SpinnerStoryBoardName = string.Format("{0}SpinnerStoryBoard",
            typeof(PackIcon<TKind>).Name); //$"{typeof(PackIcon<TKind>).Name}SpinnerStoryBoard";

        /// <summary>
        ///     Identifies the SpinDuration dependency property.
        /// </summary>
        public static readonly DependencyProperty SpinDurationProperty
            = DependencyProperty.Register("SpinDuration",
                typeof(double),
                typeof(PackIcon<TKind>),
                new PropertyMetadata(1d, SpinDurationPropertyChangedCallback, SpinDurationCoerceValueCallback));

        /// <summary>
        ///     Identifies the SpinEasingFunction dependency property.
        /// </summary>
        public static readonly DependencyProperty SpinEasingFunctionProperty
            = DependencyProperty.Register("SpinEasingFunction",
                typeof(IEasingFunction),
                typeof(PackIcon<TKind>),
                new PropertyMetadata(null, SpinEasingFunctionPropertyChangedCallback));

        /// <summary>
        ///     Identifies the SpinAutoReverse dependency property.
        /// </summary>
        public static readonly DependencyProperty SpinAutoReverseProperty
            = DependencyProperty.Register("SpinAutoReverse",
                typeof(bool),
                typeof(PackIcon<TKind>),
                new PropertyMetadata(default(bool), SpinAutoReversePropertyChangedCallback));

        // private static readonly string SpinnerStoryBoardName = $"{typeof(PackIcon<TKind>).Name}SpinnerStoryBoard";

        private FrameworkElement _innerGrid;


        static PackIcon()
        {
            OpacityProperty.OverrideMetadata(typeof(PackIcon<TKind>),
                new UIPropertyMetadata(1d, (d, e) => { d.CoerceValue(SpinProperty); }));
            VisibilityProperty.OverrideMetadata(typeof(PackIcon<TKind>),
                new UIPropertyMetadata(Visibility.Visible, (d, e) => { d.CoerceValue(SpinProperty); }));
        }

        public PackIcon(Func<IDictionary<TKind, string>> dataIndexFactory)
            : base(dataIndexFactory)
        {
        }

        /// <summary>
        ///     Gets or sets the flip orientation.
        /// </summary>
        public PackIconFlipOrientation Flip
        {
            get { return (PackIconFlipOrientation) GetValue(FlipProperty); }
            set { SetValue(FlipProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the rotation (angle).
        /// </summary>
        /// <value>The rotation.</value>
        public double Rotation
        {
            get { return (double) GetValue(RotationProperty); }
            set { SetValue(RotationProperty, value); }
        }

        private FrameworkElement InnerGrid
        {
            get { return _innerGrid ?? (_innerGrid = GetTemplateChild("PART_InnerGrid") as FrameworkElement); }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the inner icon is spinning.
        /// </summary>
        /// <value><c>true</c> if spin; otherwise, <c>false</c>.</value>
        public bool Spin
        {
            get { return (bool) GetValue(SpinProperty); }
            set { SetValue(SpinProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the duration of the spinning animation (in seconds). This will also restart the spin animation.
        /// </summary>
        /// <value>The duration of the spin in seconds.</value>
        public double SpinDuration
        {
            get { return (double) GetValue(SpinDurationProperty); }
            set { SetValue(SpinDurationProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the EasingFunction of the spinning animation. This will also restart the spin animation.
        /// </summary>
        /// <value>The spin easing function.</value>
        public IEasingFunction SpinEasingFunction
        {
            get { return (IEasingFunction) GetValue(SpinEasingFunctionProperty); }
            set { SetValue(SpinEasingFunctionProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the AutoReverse of the spinning animation. This will also restart the spin animation.
        /// </summary>
        /// <value><c>true</c> if [spin automatic reverse]; otherwise, <c>false</c>.</value>
        public bool SpinAutoReverse
        {
            get { return (bool) GetValue(SpinAutoReverseProperty); }
            set { SetValue(SpinAutoReverseProperty, value); }
        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            CoerceValue(SpinProperty);
        }

        private static object RotationPropertyCoerceValueCallback(DependencyObject dependencyObject, object value)
        {
            var val = (double) value;
            return val < 0 ? 0d : (val > 360 ? 360d : value);
        }

        private static object SpinPropertyCoerceValueCallback(DependencyObject dependencyObject, object value)
        {
            var packIcon = dependencyObject as PackIcon<TKind>;
            if (packIcon != null && (!packIcon.IsVisible || packIcon.Opacity <= 0 || packIcon.SpinDuration <= 0.0))
                return false;
            return value;
        }

        private static void SpinPropertyChangedCallback(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs e)
        {
            var packIcon = dependencyObject as PackIcon<TKind>;
            if (packIcon != null && e.OldValue != e.NewValue && e.NewValue is bool)
            {
                var spin = (bool) e.NewValue;
                if (spin)
                    packIcon.BeginSpinAnimation();
                else
                    packIcon.StopSpinAnimation();
            }
        }

        private void BeginSpinAnimation()
        {
            var element = InnerGrid;
            if (null == element)
                return;
            var transformGroup = element.RenderTransform as TransformGroup ?? new TransformGroup();
            var rotateTransform = transformGroup.Children.OfType<RotateTransform>().LastOrDefault();

            if (rotateTransform != null)
            {
                rotateTransform.Angle = 0;
            }
            else
            {
                transformGroup.Children.Add(new RotateTransform());
                element.RenderTransform = transformGroup;
            }

            var storyboard = new Storyboard();
            var animation = new DoubleAnimation
            {
                From = 0,
                To = 360,
                AutoReverse = SpinAutoReverse,
                EasingFunction = SpinEasingFunction,
                RepeatBehavior = RepeatBehavior.Forever,
                Duration = new Duration(TimeSpan.FromSeconds(SpinDuration))
            };
            storyboard.Children.Add(animation);

            Storyboard.SetTarget(animation, element);

            Storyboard.SetTargetProperty(animation,
                new PropertyPath("(0).(1)[2].(2)", RenderTransformProperty, TransformGroup.ChildrenProperty,
                    RotateTransform.AngleProperty));

            element.Resources.Add(SpinnerStoryBoardName, storyboard);
            storyboard.Begin();
        }

        private void StopSpinAnimation()
        {
            var element = InnerGrid;
            if (null == element)
                return;
            var storyboard = element.Resources[SpinnerStoryBoardName] as Storyboard;
            if (storyboard != null)
            {
                storyboard.Stop();
                element.Resources.Remove(SpinnerStoryBoardName);
            }
        }


        private static void SpinDurationPropertyChangedCallback(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs e)
        {
            var packIcon = dependencyObject as PackIcon<TKind>;
            if (packIcon != null && e.OldValue != e.NewValue && packIcon.Spin && e.NewValue is double)
            {
                packIcon.StopSpinAnimation();
                packIcon.BeginSpinAnimation();
            }
        }

        private static object SpinDurationCoerceValueCallback(DependencyObject dependencyObject, object value)
        {
            var val = (double) value;
            return val < 0 ? 0d : value;
        }

        private static void SpinEasingFunctionPropertyChangedCallback(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs e)
        {
            var packIcon = dependencyObject as PackIcon<TKind>;
            if (packIcon != null && e.OldValue != e.NewValue && packIcon.Spin)
            {
                packIcon.StopSpinAnimation();
                packIcon.BeginSpinAnimation();
            }
        }

        private static void SpinAutoReversePropertyChangedCallback(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs e)
        {
            var packIcon = dependencyObject as PackIcon<TKind>;
            if (packIcon != null && e.OldValue != e.NewValue && packIcon.Spin && e.NewValue is bool)
            {
                packIcon.StopSpinAnimation();
                packIcon.BeginSpinAnimation();
            }
        }
    }
}