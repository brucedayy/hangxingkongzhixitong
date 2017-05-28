using System.Windows;
using System.Windows.Input;

namespace NC.Panel.Panels
{
    /// <summary>
    /// 优先使用
    /// </summary>
    public partial class SamplePanel
    {
        private bool _buttonTwoIsEnabled = true;

        /// <summary>
        ///     打开窗口1命令。
        /// </summary>
        public static readonly RoutedCommand OpenWindowOneCommand = new RoutedCommand("OpenWindowOneCommand",
            typeof(SamplePanel));

        /// <summary>
        ///     绑定命令。
        /// </summary>
        private void BindCommand()
        {
            CommandBindings.Add(new CommandBinding(OpenWindowOneCommand, ExecutedOpenWindowOneCommand,
                CanExecuteOpenWindowOneCommand));
        }

        private void CanExecuteOpenWindowOneCommand(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _buttonTwoIsEnabled;
        }

        private void ExecutedOpenWindowOneCommand(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show(Application.Current.MainWindow, "该控件已禁用", "提示", MessageBoxButton.OK);
            _buttonTwoIsEnabled = false;
        }
    }
}