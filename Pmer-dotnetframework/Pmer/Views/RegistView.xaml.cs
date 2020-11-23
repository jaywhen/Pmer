using Pmer.ViewModel;
using System.Windows;
using System.Windows.Input;


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

            RegistViewModel registViewModel = new RegistViewModel();
            this.DataContext = registViewModel;
            
        }



        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

       
    }
}
