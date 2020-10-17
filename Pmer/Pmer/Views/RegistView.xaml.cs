using Pmer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// RegistView.xaml 的交互逻辑
    /// </summary>
    public partial class RegistView : Window
    {
        public RegistView()
        {
            InitializeComponent();
            this.MouseDown += (sender, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                    this.DragMove();
            };

            UserModel userModel = new UserModel();
            this.DataContext = userModel;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SetBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
