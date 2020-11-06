using GalaSoft.MvvmLight.Command;
using Pmer.Db;

namespace Pmer.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {
            db = new DbCreator();
            db.CreateDbConnection();
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
            string relpw = db.GetMasterPwFromMpTable();
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
