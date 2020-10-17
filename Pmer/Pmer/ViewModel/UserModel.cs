using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pmer.ViewModel
{
    public class UserModel : ViewModelBase
    {
        private string username = Environment.UserName;
        public string UserName
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                RaisePropertyChanged();
            }
        }
    }
}
