using System;
using GalaSoft.MvvmLight.Command;
using Pmer.Db;
using Pmer.Encryption;

namespace Pmer.ViewModel
{
    public class RegistViewModel : BaseViewModel
    {
        public RegistViewModel()
        {
            // db = new DbCreator();
            // db.CreateDbConnection();

            RegistCommand = new RelayCommand(Regist);
            CloseCommand = new RelayCommand(Close);
        }

        // DbCreator db;

        #region Property
        private string masterUserName = Environment.UserName;
        private string rePassWord;
        private string regiBtnContent = "Set";
        private bool isRegiSuccess = false;

        // about exit command
        

        // regist button's content
        public string RegiBtnContent
        {
            get { return regiBtnContent; }
            set { regiBtnContent = value; RaisePropertyChanged(); }
        }

        public bool IsRegiSuccess
        {
            get { return isRegiSuccess; }
            set { isRegiSuccess = value; }
        }
        
        public string RePassWord
        {
            get { return rePassWord; }
            set { rePassWord = value; RaisePropertyChanged(); }
        }
        
   
        

        #endregion

        #region Command
        public RelayCommand RegistCommand { get; set; }
        #endregion

        //public void Close()
        //{
        //    this.DialogResult = false;
        //}
        public void Regist()
        {
            if (IsRegiSuccess)
            {
                SetLoginSuccess();
                return;
            }
            else
            {
                if (string.IsNullOrEmpty(PassWord))
                {
                    WindowToolTip = "Please input a password";
                    return;
                }
                if (string.IsNullOrEmpty(RePassWord))
                {
                    WindowToolTip = "Please confirm your password";
                    return;
                }
                if (!string.Equals(PassWord, RePassWord))
                {
                    WindowToolTip = "The two passwords entered are inconsistent";
                    return;
                }
                else
                {
                    WindowToolTip = "OK!";
                }

                string preSalt = Encryptor.GenerateSalt();
                string sufSalt = Encryptor.GenerateSalt();
                string hashedPassword = Encryptor.SHA512AddSalt(preSalt, RePassWord, sufSalt);
                DbHelper.InsertMainPassword(MasterUserName, hashedPassword, preSalt, sufSalt);
                // db.InsertMasterPw(MasterUserName, hashedPassword, preSalt, sufSalt);
                RegiBtnContent = "Close";
                IsRegiSuccess = true;
                return;
            }
        }

    }
}
