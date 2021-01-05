using Pmer.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Pmer.Views
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
        public MainWindow(bool isWindowShow, string key)
        {
            InitializeComponent();
            this.MouseDown += (sender, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                    this.DragMove();
            };
            if (isWindowShow)
            {
                MainWindowModel mainWindowModel = new MainWindowModel(key);
                this.DataContext = mainWindowModel;                                
            }
        }

        private void ListBox_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!e.Handled)
            {
                // 内层ListBox拦截鼠标滚轮事件
                e.Handled = true;

                // 激发一个鼠标滚轮事件，冒泡给外层ListBox接收到
                var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
                eventArg.RoutedEvent = UIElement.MouseWheelEvent;
                eventArg.Source = sender;
                var parent = ((Control)sender).Parent as UIElement;
                parent.RaiseEvent(eventArg);
            }

        }
    }
}
