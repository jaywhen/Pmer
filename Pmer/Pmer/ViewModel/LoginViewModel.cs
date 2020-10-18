using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pmer.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private string masterUserName = Environment.UserName;
        public string MasterUserName
        {
            get
            {
                return masterUserName;
            }
            set
            {
                masterUserName = value;
                RaisePropertyChanged();
            }
        }
    }
}
