#region

using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

#endregion

namespace NC.Panel.Infrastructure.MarkupExtensions
{
    /// <summary>
    ///     延迟绑定扩展。
    /// </summary>
    [MarkupExtensionReturnType(typeof(object))]
    public class LazyBindingExtension : MarkupExtension
    {
        #region Members

        /// <summary>
        ///     绑定的目标元素。
        /// </summary>
        private FrameworkElement _target;

        /// <summary>
        ///     绑定的依赖属性。
        /// </summary>
        private DependencyProperty _property;

        #endregion Members

        #region Properties

        /// <summary>
        ///     获取或设置转换器。
        /// </summary>
        public IValueConverter Converter { get; set; }

        /// <summary>
        ///     获取或设置转换器参数。
        /// </summary>
        public object ConverterParameter { get; set; }

        /// <summary>
        ///     获取或设置元素名称。
        /// </summary>
        public string ElementName { get; set; }

        /// <summary>
        ///     获取或设置属性路径。
        /// </summary>
        public PropertyPath Path { get; set; }

        /// <summary>
        ///     获取或设置绑定源的相对位置。
        /// </summary>
        public RelativeSource RelativeSource { get; set; }

        /// <summary>
        ///     获取或设置用作绑定源的对象。
        /// </summary>
        public object Source { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        ///     无参构造函数。
        /// </summary>
        public LazyBindingExtension()
        {
        }

        /// <summary>
        ///     有参构造函数。
        /// </summary>
        /// <param name="path">路径标记字符串。</param>
        public LazyBindingExtension(string path)
        {
            Path = new PropertyPath(path);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        ///     创建绑定类型实例。
        /// </summary>
        /// <returns></returns>
        private Binding CreateBinding()
        {
            var binding = new Binding(Path.Path);
            if (Source != null)
                binding.Source = Source;
            if (RelativeSource != null)
                binding.RelativeSource = RelativeSource;
            if (ElementName != null)
                binding.ElementName = ElementName;
            binding.Converter = Converter;
            binding.ConverterParameter = ConverterParameter;
            return binding;
        }

        /// <summary>
        ///     显隐状态改变事件处理函数。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // 添加绑定。
            var binding = CreateBinding();
            BindingOperations.SetBinding(_target, _property, binding);
        }

        /// <summary>
        ///     返回一个对象，此对象是作为此标记扩展的目标属性的值提供的。
        /// </summary>
        /// <param name="serviceProvider">服务提供者。</param>
        /// <returns>要在应用扩展的属性上设置的对象值。</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var service = serviceProvider.GetService
                (typeof(IProvideValueTarget)) as IProvideValueTarget;
            if (service == null)
                return null;

            _target = service.TargetObject as FrameworkElement;
            _property = service.TargetProperty as DependencyProperty;
            if (_target != null && _property != null)
            {
                // 侦听IsVisible属性的更改，以在界面元素显示时通过OnIsVisibleChanged函数添加绑定。
                _target.IsVisibleChanged += OnIsVisibleChanged;
                return null;
            }
            var binding = CreateBinding();
            return binding.ProvideValue(serviceProvider);
        }

        #endregion Methods
    }
}