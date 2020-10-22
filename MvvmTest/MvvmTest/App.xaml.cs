using MvvmTest.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MvvmTest
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow mw = new MainWindow();
            bool? ret = mw.ShowDialog();
            if(ret.Value == false)
            {
                
                Window1 w1 = new Window1();
                w1.ShowDialog();
            }

            
        }
    }
}
