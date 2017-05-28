using System.Windows.Controls;
using NC.Panel.ViewModels;

namespace NC.Panel.Panels
{
    /// <summary>
    /// Panel1.xaml 的交互逻辑
    /// </summary>
    public partial class SamplePanel : UserControl
    {
        private readonly SamplePanelViewModel _samplePanelViewModel;

        public SamplePanel()
        {
            InitializeComponent();
            _samplePanelViewModel = new SamplePanelViewModel();
            this.DataContext = _samplePanelViewModel;
        }
    }
}
