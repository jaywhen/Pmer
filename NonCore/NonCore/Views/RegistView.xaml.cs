using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NonCore.Views
{
    /// <summary>
    /// RegistView.xaml 的交互逻辑
    /// </summary>
    public partial class RegistView : Window
    {
        public RegistView()
        {
            InitializeComponent();
        }

        private string pw;
        public string Pw 
        {
            get
            {
                return pw;
            }
          
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // 输入验证 {测试}
            if (string.IsNullOrEmpty(regiPwBox.Password))
            {
                SetInformationString("请输入密码");
                regiPwBox.Focus();
                return;
            }
            if (string.IsNullOrEmpty(repeatPwBox.Password))
            {
                SetInformationString("请重复输入密码");
                repeatPwBox.Focus();
                return;
            }
            if(!string.Equals(regiPwBox.Password, repeatPwBox.Password))
            {
                SetInformationString("两次输入的密码不一致");
            }
            else
            {
                SetInformationString("");
            }

            // 验证over

            this.pw = regiPwBox.Password;
            this.Close();

        }

        private void SetInformationString(string str)
        {
            if (WindowToolTip.Opacity == 1)
            {
                DoubleAnimation hidden = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(100));
                hidden.Completed += delegate
                {
                    DoubleAnimation show = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(100));
                    WindowToolTip.Text = str;
                    WindowToolTip.BeginAnimation(OpacityProperty, show);
                };
                WindowToolTip.BeginAnimation(OpacityProperty, hidden);
            }
            else
            {
                DoubleAnimation show = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(100));
                WindowToolTip.Text = str;
                WindowToolTip.BeginAnimation(OpacityProperty, show);
            }
        }
    }
}
