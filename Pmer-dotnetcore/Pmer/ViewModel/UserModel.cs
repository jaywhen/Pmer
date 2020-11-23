using GalaSoft.MvvmLight;
using System;

namespace Pmer.ViewModel
{
    public class UserModel : ViewModelBase
    {
        private string username = Environment.UserName;
        public string UserName
        {
            get
            { return username; }
            set
            { username = value; RaisePropertyChanged(); }
        }
    }
}
