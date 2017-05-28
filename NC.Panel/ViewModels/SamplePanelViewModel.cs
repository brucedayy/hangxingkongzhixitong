#region

using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

#endregion

namespace NC.Panel.ViewModels
{
    public class SamplePanelViewModel : ViewModelBase
    {
        private bool _buttonOneIsEnabled = true;

        public SamplePanelViewModel()
        {
            ButtonOneCommand = new RelayCommand(ExecuteButtonOneCommand, CanExecuteButtonOneCommand);
        }

        #region Commands

        /// <summary>
        ///     <remarks>注：应引用命名空间GalaSoft.MvvmLight.CommandWpf中的RelayCommand，否则有CanExecute不能刷新界面bug。</remarks>
        /// </summary>
        public RelayCommand ButtonOneCommand { get; set; }

        #endregion

        private bool CanExecuteButtonOneCommand()
        {
            return _buttonOneIsEnabled;
        }

        private void ExecuteButtonOneCommand()
        {
            MessageBox.Show(Application.Current.MainWindow, "该控件已禁用", "提示", MessageBoxButton.OK);
            _buttonOneIsEnabled = false;
        }
    }
}