using MaterialDesignThemes.Wpf;
using Pmer.Db;
using Pmer.Views;
using System.Windows;

namespace Pmer
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            DbCreator db = new DbCreator();
            db.createDbConnection();
            bool isLogin = db.Init();
            if (!isLogin)
            {
                RegistView registView = new RegistView();
                bool? result =  registView.ShowDialog();
                if (result.Value == true)
                {
                    MainWindow mw = new MainWindow();
                    MainWindow = mw;
                    mw.ShowDialog();

                }
                
            }
            else
            {
                MainWindow mw = new MainWindow();
                mw.ShowDialog();
                //LoginView loginView = new LoginView();
                //bool? resultLogin = loginView.ShowDialog();
            }
        }

    }
}
