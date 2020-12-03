using GalaSoft.MvvmLight;

namespace Pmer.Models
{
    /// <summary>
    /// 密码项Model
    /// </summary>
    public class PasswordItem : ViewModelBase
    {
        public PasswordItem() { }
        public PasswordItem(int id, string title, string account, string password, string website, string avatar)
        {
            this.Id = id;
            this.Title = title;
            this.Account = account;
            this.Password = password;
            this.Website = website;
            this.Avatar = avatar;
        }
        #region Property
        private int id;
        private string title;
        private string account;
        private string password;
        private string website;
        private string avatar;

        public int Id 
        {
            get { return id; }
            set { id = value; RaisePropertyChanged(); } 
        }
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
        #endregion
    }
}
