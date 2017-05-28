﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Effects;

namespace NC.Control.Converters
{
    public class ShadowConverter : IValueConverter
    {
        private static readonly IDictionary<ShadowDepth, DropShadowEffect> ShadowsDictionary;
        public static readonly ShadowConverter Instance = new ShadowConverter();

        static ShadowConverter()
        {
            var resourceDictionary = new ResourceDictionary { Source = new Uri("pack://application:,,,/NC.Control;component/Themes/Shadows.xaml", UriKind.Absolute) };

            ShadowsDictionary = new Dictionary<ShadowDepth, DropShadowEffect>
            {
                { ShadowDepth.Depth0, null },
                { ShadowDepth.Depth1, (DropShadowEffect)resourceDictionary["ShadowDepth1"] },
                { ShadowDepth.Depth2, (DropShadowEffect)resourceDictionary["ShadowDepth2"] },
                { ShadowDepth.Depth3, (DropShadowEffect)resourceDictionary["ShadowDepth3"] },
                { ShadowDepth.Depth4, (DropShadowEffect)resourceDictionary["ShadowDepth4"] },
                { ShadowDepth.Depth5, (DropShadowEffect)resourceDictionary["ShadowDepth5"] },
            };
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is ShadowDepth)) return null;
            return ShadowsDictionary[(ShadowDepth)value];
            // return Clone(ShadowsDictionary[(ShadowDepth)value]);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static DropShadowEffect Clone(DropShadowEffect dropShadowEffect)
        {
            if (dropShadowEffect == null) return null;
            return new DropShadowEffect()
            {
                BlurRadius = dropShadowEffect.BlurRadius,
                Color = dropShadowEffect.Color,
                Direction = dropShadowEffect.Direction,
                Opacity = dropShadowEffect.Opacity,
                RenderingBias = dropShadowEffect.RenderingBias,
                ShadowDepth = dropShadowEffect.ShadowDepth
            };
        }
    }
}