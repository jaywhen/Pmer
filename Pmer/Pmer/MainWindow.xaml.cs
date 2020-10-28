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

            if (IsMainWindowShow)
            {
                MainWindowModel mainWindowModel = new MainWindowModel();
                this.DataContext = mainWindowModel;
            }
            
        }
        public MainWindow(bool IsM)
        {
            InitializeComponent();

            if (IsM)
            {
                MainWindowModel mainWindowModel = new MainWindowModel();
                this.DataContext = mainWindowModel;
            }

        }


        private bool isMainWindowShow = false;
        public bool IsMainWindowShow
        {
            get { return isMainWindowShow; }
            set { 
                isMainWindowShow = value;
            }
        }



    }
}
