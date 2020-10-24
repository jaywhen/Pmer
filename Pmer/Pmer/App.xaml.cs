using MaterialDesignThemes.Wpf;
using Pmer.Db;
using Pmer.Views;
using System.Windows;
using System.Windows.Input;

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

            MainWindow mv = new MainWindow();
            this.MainWindow = mv;
            this.ShutdownMode = ShutdownMode.OnMainWindowClose;

            // 检测是否具备弹出登录窗口的条件
            DbCreator db = new DbCreator();
            db.createDbConnection();
            bool isLogin = db.Init();

            if (!isLogin)
            {
                RegistView registView = new RegistView();
                bool? result =  registView.ShowDialog();
                if (result == true)
                {
                    LoginView lw = new LoginView();
                    bool? ret = lw.ShowDialog();
                    if(ret.Value == true)
                    {
                        MainWindow.ShowDialog();
                    }
                }
            }
            else
            {
                LoginView loginView = new LoginView();
                bool? resultLogin = loginView.ShowDialog();
                if(resultLogin.Value == true)
                {
                    MainWindow.ShowDialog();
                }
            }
        }

    }
}
