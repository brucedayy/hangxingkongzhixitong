using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace NC.Control.ControlzEx
{
    public abstract class PackIconBase : System.Windows.Controls.Control
    {
        internal abstract void UpdateData();
    }

    /// <summary>
    ///     Base class for creating an icon control for icon packs.
    /// </summary>
    /// <typeparam name="TKind"></typeparam>
    public abstract class PackIconBase<TKind> : PackIconBase
    {
        private static Lazy<IDictionary<TKind, string>> _dataIndex;

        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register("Kind", typeof(TKind), typeof(PackIconBase<TKind>),
                new PropertyMetadata(default(TKind), KindPropertyChangedCallback));

        private static readonly DependencyPropertyKey DataPropertyKey
            = DependencyProperty.RegisterReadOnly("Data", typeof(string), typeof(PackIconBase<TKind>),
                new PropertyMetadata(""));

        // ReSharper disable once StaticMemberInGenericType
        public static readonly DependencyProperty DataProperty = DataPropertyKey.DependencyProperty;

        /// <param name="dataIndexFactory">
        ///     Inheritors should provide a factory for setting up the path data index (per icon kind).
        ///     The factory will only be utilised once, across all closed instances (first instantiation wins).
        /// </param>
        protected PackIconBase(Func<IDictionary<TKind, string>> dataIndexFactory)
        {
            if (dataIndexFactory == null) throw new ArgumentNullException("dataIndexFactory");

            if (_dataIndex == null)
                _dataIndex = new Lazy<IDictionary<TKind, string>>(dataIndexFactory);
        }

        /// <summary>
        ///     Gets or sets the icon to display.
        /// </summary>
        public TKind Kind
        {
            get { return (TKind) GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

        /// <summary>
        ///     Gets the icon path data for the current <see cref="Kind" />.
        /// </summary>
        [TypeConverter(typeof(GeometryConverter))]
        public string Data
        {
            get { return (string) GetValue(DataProperty); }
            private set { SetValue(DataPropertyKey, value); }
        }

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ((PackIconBase) dependencyObject).UpdateData();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            UpdateData();
        }

        internal override void UpdateData()
        {
            string data = null;
            if (_dataIndex.Value != null)
                _dataIndex.Value.TryGetValue(Kind, out data);
            Data = data;
        }
    }
}