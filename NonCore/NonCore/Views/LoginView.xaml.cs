﻿using NonCore.Db;
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

namespace NonCore.Views
{
    /// <summary>
    /// LoginView.xaml 的交互逻辑
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();

            DbCreator db = new DbCreator();
            db.createDbConnection();
            bool isLogin = db.Init();
            // db.createDbConnection();
            if (!isLogin)
            {
                RegistView registView = new RegistView();
                registView.ShowDialog();
                string res = registView.Pw;

                // 插值
                db.insertMasterPw(res);
            } 
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
