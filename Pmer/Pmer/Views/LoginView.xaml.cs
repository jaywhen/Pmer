using Pmer.ViewModel;
using System.Windows;
using System.Windows.Input;


namespace Pmer.Views
{
    /// <summary>
    /// LoginView.xaml 的交互逻辑
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();

            this.MouseDown += (sender, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                    this.DragMove();
            };

            LoginViewModel loginViewModel = new LoginViewModel();
            this.DataContext = loginViewModel;
        }

    }

}
