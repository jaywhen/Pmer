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
        }

        // 只有当isWindowShow为True时，才进行数据绑定
        public MainWindow(bool isWindowShow)
        {
            InitializeComponent();
            if (isWindowShow)
            {
                MainWindowModel mainWindowModel = new MainWindowModel();
                this.DataContext = mainWindowModel;
            }

        }
    }
}
