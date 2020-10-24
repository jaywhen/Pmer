using Pmer.ViewModel;
using System.Windows;
using Pmer.Views;

namespace Pmer
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowModel mainWindowModel = new MainWindowModel();
            this.DataContext = mainWindowModel;
        }
    }
}
