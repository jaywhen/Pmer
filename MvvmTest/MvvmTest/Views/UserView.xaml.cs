using MvvmTest.Models;
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

namespace MvvmTest.Views
{
    /// <summary>
    /// UserView.xaml 的交互逻辑
    /// </summary>
    public partial class UserView : Window
    {
        public UserView(Student stu)
        {
            InitializeComponent();
            this.DataContext = new
            {
                Model = stu
            };
        }

        private void btnSave(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void btnCancel(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
