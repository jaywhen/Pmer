using Pmer.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pmer.Views
{
    /// <summary>
    /// WarningView.xaml 的交互逻辑
    /// </summary>
    public partial class WarningView : Window
    {
        public WarningView(string warningMsg)
        {
            InitializeComponent();
            this.MouseDown += (sender, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                    this.DragMove();
            };
            WarningViewModel warningViewModel = new WarningViewModel(warningMsg);
            this.DataContext = warningViewModel;
        }
    }
}
