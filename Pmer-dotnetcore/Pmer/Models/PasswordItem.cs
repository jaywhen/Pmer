using GalaSoft.MvvmLight;

namespace Pmer.Models
{
    /// <summary>
    /// 密码项Model
    /// </summary>
    public class PasswordItem : ViewModelBase
    {
        #region Property
        private int passwordItemId;
        private string title;
        private string account;
        private string password;
        private string website;
        private string avatar;
        private int tagId; // 外键，引用自 tag 表
        private Tag tag;

        
        public int PasswordItemId
        {
            get { return passwordItemId; }
            set { passwordItemId = value; RaisePropertyChanged(); } 
        }
        public int TagId
        {
            get { return tagId; }
            set { tagId = value; RaisePropertyChanged(); }
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
        public Tag Tag
        {
            get { return tag; }
            set { tag = value; RaisePropertyChanged(); }
        }
        #endregion

        public void Clean()
        {
            Title = "";
            Account = "";
            Password = "";
            Website = "";
            Avatar = "";
            TagId = 0;
            PasswordItemId = 0;
        }
    }
}
