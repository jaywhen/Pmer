﻿using Pmer.Db;
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

            UserModel usermodel = new UserModel();
            this.DataContext = usermodel;

            DbCreator db = new DbCreator();
            db.createDbConnection();
            bool isLogin = db.Init();
            if (!isLogin)
            {
                RegistView registView = new RegistView();
                registView.ShowDialog();
            }
        }
        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
