using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Pmer.Db;


namespace Pmer.ViewModel
{
    public class RegistViewModel : ViewModelBase
    {
        public RegistViewModel()
        {
            db = new DbCreator();
            db.createDbConnection();

            RegistCommand = new RelayCommand(Regist);
            CloseCommand = new RelayCommand(Close);
        }

        DbCreator db;

        #region Property
        private string masterUserName = Environment.UserName;
        private string passWord;
        private string rePassWord;
        private string windowToolTip;
        private bool shouldCloseWindow = false;
        private string regiBtnContent = "Set";

        // about exit command
        private bool? dialogResult;
        public bool? DialogResult
        {
            get { return this.dialogResult; }
            set
            {
                this.dialogResult = value;
                RaisePropertyChanged("DialogResult");
            }
        }

        // regist button's content
        public string RegiBtnContent
        {
            get { return regiBtnContent; }
            set { regiBtnContent = value; RaisePropertyChanged(); }
        }

        public string PassWord
        {
            get { return passWord; }
            set { passWord = value;RaisePropertyChanged(); }
        }
        public string RePassWord
        {
            get { return rePassWord; }
            set { rePassWord = value; RaisePropertyChanged(); }
        }
        public string WindowToolTip
        {
            get { return windowToolTip; }
            set { windowToolTip = value;RaisePropertyChanged(); }
        }
   
        public string MasterUserName
        {
            get
            { return masterUserName; }
            set
            {
                masterUserName = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Command
        public RelayCommand RegistCommand { get; set; }
        public RelayCommand CloseCommand { get; set; }
        #endregion

        public void Close()
        {
            this.DialogResult = true;
        }
        public void Regist()
        {
            if (shouldCloseWindow)
            {
                Close();
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
                db.insertMasterPw(rePassWord);
                shouldCloseWindow = true;
                RegiBtnContent = "Close";
                return;
            }
        }

    }
}
