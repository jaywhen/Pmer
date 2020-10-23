using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Pmer.Db;
using System;

namespace Pmer.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {
            db = new DbCreator();
            db.createDbConnection();
            LoginCommand = new RelayCommand(Login);
            CloseCommand = new RelayCommand(Close);
        }
        DbCreator db;

        #region Command
        public RelayCommand LoginCommand { get; set; }
        #endregion

        public void Login()
        {
            if (string.IsNullOrEmpty(PassWord))
            {
                WindowToolTip = "Please enter your master password";
                return;
            }
            string relpw = db.getMasterPwFromMpTable();
            if(!string.Equals(PassWord, relpw))
            {
                WindowToolTip = "Incorrect password";
                return;
            }
            else
            {
                WindowToolTip = "";
            }
            SetLoginSuccess();

        }

    }
}
