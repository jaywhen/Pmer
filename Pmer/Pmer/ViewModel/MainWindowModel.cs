using GalaSoft.MvvmLight.Command;
using Pmer.Db;
using System;

namespace Pmer.ViewModel
{
    public class MainWindowModel : BaseViewModel
    {
        public MainWindowModel()
        {
            db = new DbCreator();
            db.createDbConnection();
            setUserFavicon();

            CloseCommand = new RelayCommand(Close);
        }

        DbCreator db;
        #region Property

        // 将FirstLetter与MainWindow中的TextBox的Text进行绑定
        private char firstLetter;
        public char FirstLetter
        {
            get { return firstLetter; }
            set { firstLetter = value; RaisePropertyChanged(); }
        }
        public void setUserFavicon()
        {
            // 从数据库中获取用户名，截取首字母大写作为头像
            FirstLetter = db.getUserNameFromMpTable().ToUpper()[0];
        }
        #endregion
    }
}
