using GalaSoft.MvvmLight;
using System;

namespace Pmer.ViewModel
{
    public class PasswordItem: ViewModelBase
    {
        public PasswordItem(string title, string account, string password, string website, string avatar)
        {
            this.Title = title;
            this.Account = account;
            this.Password = password;
            this.Website = website;
            this.Avatar = avatar;
        }

        public PasswordItem(){}
        private Int64 id;
        private string title;
        private string account;
        private string password;
        private string website;
        private string avatar;

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
        public string Avatar
        {
            get { return avatar; }
            set { avatar = value; RaisePropertyChanged(); }
        }
        public Int64 Id
        {
            get { return id; }
            set { id = value;RaisePropertyChanged(); }
        }

    }
}
