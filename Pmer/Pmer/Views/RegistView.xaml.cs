using Pmer.ViewModel;
using Microsoft.Xaml.Behaviors;
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
using System.Windows.Media.Animation;
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

            RegistViewModel registViewModel = new RegistViewModel();
            this.DataContext = registViewModel;
            
        }


        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

       
    }


    // 移至BaseHandler.cs中了

    //public static class PasswordBoxHelper
    //{
    //    public static readonly DependencyProperty PasswordProperty =
    //        DependencyProperty.RegisterAttached("Password",
    //        typeof(string), typeof(PasswordBoxHelper),
    //        new FrameworkPropertyMetadata(string.Empty, OnPasswordPropertyChanged));

    //    private static void OnPasswordPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    //    {
    //        PasswordBox passwordBox = sender as PasswordBox;
    //        string password = (string)e.NewValue;
    //        if (passwordBox != null && passwordBox.Password != password)
    //            passwordBox.Password = password;
    //    }

    //    public static string GetPassword(DependencyObject dp)
    //    {
    //        return (string)dp.GetValue(PasswordProperty);
    //    }

    //    public static void SetPassword(DependencyObject dp, string value)
    //    {
    //        dp.SetValue(PasswordProperty, value);
    //    }
    //}

    //public class PasswordBoxBehavior : Behavior<PasswordBox>
    //{
    //    protected override void OnAttached()
    //    {
    //        base.OnAttached();
    //        AssociatedObject.PasswordChanged += OnPasswordChanged;
    //    }

    //    private static void OnPasswordChanged(object sender, RoutedEventArgs e)
    //    {
    //        PasswordBox passwordBox = sender as PasswordBox;
    //        string password = PasswordBoxHelper.GetPassword(passwordBox);
    //        if (passwordBox != null && passwordBox.Password != password)
    //            PasswordBoxHelper.SetPassword(passwordBox, passwordBox.Password);
    //    }

    //    protected override void OnDetaching()
    //    {
    //        base.OnDetaching();
    //        AssociatedObject.PasswordChanged -= OnPasswordChanged;
    //    }
    //}

    //public static class DialogCloser
    //{
    //    public static readonly DependencyProperty DialogResultProperty =
    //        DependencyProperty.RegisterAttached(
    //            "DialogResult",
    //            typeof(bool?),
    //            typeof(DialogCloser),
    //            new PropertyMetadata(DialogResultChanged));

    //    private static void DialogResultChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    //    {
    //        var window = d as Window;
    //        if (window != null)
    //        {
    //            window.DialogResult = e.NewValue as bool?;
    //        }
    //    }

    //    public static void SetDialogResult(Window target, bool? value)
    //    {
    //        target.SetValue(DialogResultProperty, value);
    //    }
    //}
}
