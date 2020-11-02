using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pmer.ViewModel
{
    public class PasswordItem: ViewModelBase
    {
        private string title;
        private string account;
        private string password;
        private string website;
        private string itemAvatar;

        public string Title
        {
            get { return title; }
            set { title = value; RaisePropertyChanged(); }
        }
        public string Account
        {
            get { return account; }
            set { account = value; RaisePropertyChanged(); }
        }
        public string Password
        {
            get { return password; }
            set { password = value; RaisePropertyChanged(); }
        }
        public string Website
        {
            get { return website; }
            set { website = value; RaisePropertyChanged(); }
        }
        public string ItemAvatar
        {
            get { return itemAvatar; }
            set { itemAvatar = value; RaisePropertyChanged(); }
        }

    }
}
