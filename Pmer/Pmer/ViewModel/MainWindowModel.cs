using GalaSoft.MvvmLight.Command;
using Pmer.Db;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;

namespace Pmer.ViewModel
{
    public class MainWindowModel : BaseViewModel
    {
        public MainWindowModel()
        {
            db = new DbCreator();
            db.CreateDbConnection();
            InitAvatarHashTable();
            setUserFavicon();
            Query();

            CloseCommand = new RelayCommand(Close);
            ShowAddNewPwFormCommand = new RelayCommand(ShowAddNewPwForm);
            AddNewPasswordCommand = new RelayCommand(AddNewPassword);
            SelectPasswordItemCommand = new RelayCommand<int>(t => SelectPasswordItem(t));
            CopyCommand = new RelayCommand<string>(t => Copy(t));
            OpenWeblinkCommand = new RelayCommand(OpenWeblink);
        }

        DbCreator db;
        
        #region Property

        // 将FirstLetter与MainWindow中的TextBox的Text进行绑定
        private char firstLetter;
        private string addNewPwFormVisibility = "Hidden";
        private string defaultVisibility = "Visible";
        private string pwItemDetailVisibility = "Hidden";
        
        public string PwItemDetailVisibility
        {
            get { return pwItemDetailVisibility; }
            set { pwItemDetailVisibility = value; RaisePropertyChanged(); }
        }
        public string DefaultVisibility
        {
            get { return defaultVisibility; }
            set { defaultVisibility = value;RaisePropertyChanged(); }
        }
        public string AddNewPwFormVisibility
        {
            get { return addNewPwFormVisibility; }
            set { addNewPwFormVisibility = value; RaisePropertyChanged(); }
        }
        public char FirstLetter
        {
            get { return firstLetter; }
            set { firstLetter = value; RaisePropertyChanged(); }
        }
        public void setUserFavicon()
        {
            // 从数据库中获取用户名，截取首字母大写作为头像
            FirstLetter = db.GetUserNameFromMpTable().ToUpper()[0];
        }

        // ----------
        private string title;
        private string account;
        private string addPassword;
        private string website;
        private string avatar;
        private Hashtable avatarHashTable;
        public Hashtable AvatarHashTable
        {
            get { return avatarHashTable; }
            set { avatarHashTable = value; RaisePropertyChanged(); }
        }
        public string Title
        {
            get { return title; }
            set { title = value;RaisePropertyChanged(); }
        }
        public string Account
        {
            get { return account; }
            set { account = value; RaisePropertyChanged(); }
        }
        public string AddPassword
        {
            get { return addPassword; }
            set { addPassword = value; RaisePropertyChanged(); }
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

        // --- now selected items ---
        private string nowSelectedTitle;
        private string nowSelectedAccount;
        private string nowSelectedAvatar;
        private string nowSelectedPassword;
        private string nowSelectedWebsite;

        public string NowSelectedAvatar
        {
            get { return nowSelectedAvatar; }
            set { nowSelectedAvatar = value; RaisePropertyChanged(); }
        }
        public string NowSelectedTitle
        {
            get { return nowSelectedTitle; }
            set { nowSelectedTitle = value; RaisePropertyChanged(); }
        }
        public string NowSelectedAccount
        {
            get { return nowSelectedAccount; }
            set { nowSelectedAccount = value; RaisePropertyChanged(); }
        }
        public string NowSelectedPassword
        {
            get { return nowSelectedPassword; }
            set { nowSelectedPassword = value; RaisePropertyChanged(); }
        }
        public string NowSelectedWebsite
        {
            get { return nowSelectedWebsite; }
            set { nowSelectedWebsite = value;RaisePropertyChanged(); }
            
        }



        // for test
        private int index;
        public int Index
        {
            get { return index; }
            set { index = value; RaisePropertyChanged(); }

        }

        // -----  -----
        private ObservableCollection<PasswordItem> passwordLists;
        public ObservableCollection<PasswordItem> PasswordLists
        {
            get { return passwordLists; }
            set { passwordLists = value; RaisePropertyChanged(); }
        }
 
        #endregion


        #region Command
        public RelayCommand ShowAddNewPwFormCommand { get; set; }
        public RelayCommand AddNewPasswordCommand { get; set; }
        public RelayCommand<int> SelectPasswordItemCommand { get; set; }
        public RelayCommand<string> CopyCommand { get; set; }
        public RelayCommand OpenWeblinkCommand { get; set; }
        #endregion

        public void SelectPasswordItem(int Index)
        {
            this.Index = Index;
            DefaultVisibility = "Hidden";
            AddNewPwFormVisibility = "Hidden";

            SetNowSelectedItems();
            PwItemDetailVisibility = "Visible";

        }

        public void ShowAddNewPwForm()
        {
            //点击添加按钮，后台响应并创建表单
            PwItemDetailVisibility = "Hidden";
            DefaultVisibility = "Hidden";
            AddNewPwFormVisibility = "Visible";
        }

        public void InitAvatarHashTable()
        {
            AvatarHashTable = new Hashtable();
            AvatarHashTable.Add("github", "asset/github.png");
            AvatarHashTable.Add("bilibili", "asset/bilibili.png");
            AvatarHashTable.Add("qq", "asset/qq.png");
            AvatarHashTable.Add("wechat", "asset/wechat.png");
            AvatarHashTable.Add("weibo", "asset/weibo.png");
            AvatarHashTable.Add("zhihu", "asset/zhihu.png");
        }
        public void AddNewPassword()
        {
            if (string.IsNullOrEmpty(Title))
            {
                WindowToolTip = "Please fill the title";
                return;
            }
            if (string.IsNullOrEmpty(AddPassword))
            {
                WindowToolTip = "Please enter the password!";
            }
            if (!Website.StartsWith("https://"))
            {
                Website = "https://" + Website;
            }
            if (AvatarHashTable.ContainsKey(Title.ToLower()))
            {
                Avatar = (string)AvatarHashTable[Title.ToLower()];
            }
            else
            {
                Avatar = "asset/default.png";
            }
            db.InsertNewPw(Title, Account, AddPassword, Website, Avatar);
            PasswordItem passwordItem = new PasswordItem(Title, Account, AddPassword, Website, Avatar);
            AddAPwItemToPwList(passwordItem);
           
            ClearAddNewPwForm();
            DefaultVisibility = "Visible";
            AddNewPwFormVisibility = "Hidden";

        }
        // curd
        public void AddAPwItemToPwList(PasswordItem item)
        {
            PasswordLists.Add(item);
        }

        // 从数据库获取数据，并展示
        public void Query()
        {
            List<PasswordItem> passwordItems = new List<PasswordItem>();
            passwordItems = db.GetPasswordItems();
            PasswordLists = new ObservableCollection<PasswordItem>();
            foreach(PasswordItem item in passwordItems)
            {
                PasswordLists.Add(item);
            }

        }

        // --- tools
        public void ClearAddNewPwForm()
        {
            Title = "";
            Account = "";
            AddPassword = "";
            Website = "";
            Avatar = "";
            WindowToolTip = "";
        }

        public void SetNowSelectedItems()
        {
            NowSelectedTitle = PasswordLists[Index].Title;
            NowSelectedAvatar = "../" + PasswordLists[Index].Avatar;
            NowSelectedAccount = PasswordLists[Index].Account;
            NowSelectedPassword = PasswordLists[Index].Password;
            NowSelectedWebsite = PasswordLists[Index].Website;
        }

        public static void Copy(string content)
        {
            Clipboard.SetText(content);
            
        }

        public void OpenWeblink()
        {
            Process.Start(NowSelectedWebsite);
        }
    }
}
