using GalaSoft.MvvmLight.Command;
using Pmer.Db;
using Pmer.Encryption;
using Pmer.Models;

namespace Pmer.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login);
            CloseCommand = new RelayCommand(Close);
        }
        // DbCreator db;

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
            MainPassword mainPasswordItem = DbHelper.GetMainPasswordItem();

            string hashedPassword = Encryptor.SHA512AddSalt(mainPasswordItem.PreSalt, PassWord, mainPasswordItem.SufSalt);

            if(!string.Equals(mainPasswordItem.Password, hashedPassword))
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
