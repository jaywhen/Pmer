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
        public App()
        {
            // 指定mw为主窗口，只有mw关闭后，进程才结束
            MainWindow mw = new MainWindow();
            this.MainWindow = mw;
            this.ShutdownMode = ShutdownMode.OnMainWindowClose;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

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
            }
        }

    }
}
