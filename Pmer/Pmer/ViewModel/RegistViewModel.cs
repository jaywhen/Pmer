using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Pmer.Views;

namespace Pmer.ViewModel
{
    public class RegistViewModel : ViewModelBase
    {


        #region Property
        private string masterUserName = Environment.UserName;
        private string passWord;
        public string PassWord
        {
            get { return passWord; }
            set { passWord = value;RaisePropertyChanged(); }
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
        #endregion




    }
}
