using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pmer.ViewModel
{
    public class BaseViewModel : ViewModelBase
    {
        #region Property
        private string masterUserName = Environment.UserName;
        private string windowToolTip;
        private string passWord;

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
        public string WindowToolTip
        {
            get { return windowToolTip; }
            set { windowToolTip = value; RaisePropertyChanged(); }
        }
        public string PassWord
        {
            get { return passWord; }
            set { passWord = value; RaisePropertyChanged(); }
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
        public RelayCommand CloseCommand { get; set; }
        //public RelayCommand SetLoginSuccessCommand { get; set; }
        #endregion

        public void Close()
        {
            this.DialogResult = false;
        }

        public void SetLoginSuccess()
        {
            this.DialogResult = true;
        }


    }
}
