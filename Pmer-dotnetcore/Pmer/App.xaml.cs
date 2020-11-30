using Pmer.Db;
using Pmer.Views;
using System.Windows;
using System.Configuration;

namespace Pmer
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// 配置数据库文件所在位置以及所在文件夹位置
        /// </summary>
        public void Config()
        {
            if (ConfigurationManager.AppSettings["LocalPath"] == null)
            {
                // 若应用程序第一次启动， 设置appdata文件夹的位置
                Configuration cf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                string localPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
                cf.AppSettings.Settings.Add("LocalPath", localPath);

                // 添加数据库文件所在的文件夹位置
                string dbDir = localPath + ConfigurationManager.AppSettings["DataBaseDirName"];
                cf.AppSettings.Settings.Add("DataBaseDirNamePath", dbDir);
                // 添加数据库文件所在的位置
                string dbFile = dbDir + ConfigurationManager.AppSettings["DataBaseFileName"];
                cf.AppSettings.Settings.Add("DataBaseFileNamePath", dbFile);

                cf.Save();
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Config();

            MainWindow mv = new MainWindow();
            this.MainWindow = mv;
            this.ShutdownMode = ShutdownMode.OnMainWindowClose;
            // 检测是否具备弹出登录窗口的条件
            bool isLogin = DbHelper.IfRegister();

            if (!isLogin)
            {
                RegistView registView = new RegistView();
                bool? result = registView.ShowDialog();
                // 如果注册成功，显示登录界面
                if (result == true)
                {
                    LoginView lw = new LoginView();
                    bool? ret = lw.ShowDialog();
                    string key = lw.PasswordBox.Password;
                    if (ret.Value == true)
                    {
                        mv = new MainWindow(true, key);
                        this.MainWindow = mv;
                        MainWindow.ShowDialog();
                    }
                } // 若用户关闭注册窗口，则退出进程
                else
                {
                    MainWindow.Close();
                }
            }
            else
            {
                LoginView loginView = new LoginView();
                bool? resultLogin = loginView.ShowDialog();
                string key = loginView.PasswordBox.Password;
                // 登录成功，进入主界面
                if (resultLogin.Value == true)
                {
                    mv = new MainWindow(true, key);
                    this.MainWindow = mv;
                    MainWindow.ShowDialog();
                } // 否则，退出进程
                else
                {
                    // 需要一个确认退出提示
                    MainWindow.Close();
                }
            }
        }
    }
}