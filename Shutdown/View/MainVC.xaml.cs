using Shutdown.ViewModel;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using Xceed.Wpf.Toolkit;

namespace Shutdown.View
{
    /// <summary>
    /// Interaction logic for MainVC.xaml
    /// </summary>
    public partial class MainVC : UserControl
    {
        private MainVM _viewModel;
        public MainVC()
        {
            InitializeComponent();
            this._viewModel = new MainVM();
            this.DataContext = this._viewModel;
        }
    }
}
