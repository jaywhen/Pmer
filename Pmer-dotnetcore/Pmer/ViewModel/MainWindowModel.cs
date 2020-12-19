using GalaSoft.MvvmLight.Command;
using Pmer.Db;
using Pmer.Encryption;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using Pmer.Models;
using Pmer.Views;
namespace Pmer.ViewModel
{
    public class MainWindowModel : BaseViewModel
    {
        public MainWindowModel(string key)
        {
            KeyPassword = Encryptor.HashedKeyByMD5(key);
            InitAvatarHashTable();
            SetUserFavicon();
            Query();

            CloseCommand = new RelayCommand(Close);
            ShowAddNewPwFormCommand = new RelayCommand(ShowAddNewPwForm);
            AddNewPasswordCommand = new RelayCommand(AddNewPassword);
            SelectPasswordItemCommand = new RelayCommand<int>(t =>  SelectPasswordItem(t));
            CopyCommand = new RelayCommand<string>(t => Copy(t));
            OpenWeblinkCommand = new RelayCommand(OpenWeblink);
            UpdateCommand = new RelayCommand(Update);
            UpdateOKCommand = new RelayCommand(UpdateOK);
            DeletePasswordItemCommand = new RelayCommand(DeletePasswordItem);
            SearchCommand = new RelayCommand<string>(t => Search(t));
            AboutCommand = new RelayCommand(About);
        }

        #region Visible
        private string deleteVisible;
        private string updateVisible;
        private string updateOKVisible;
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
            set { defaultVisibility = value; RaisePropertyChanged(); }
        }
        public string AddNewPwFormVisibility
        {
            get { return addNewPwFormVisibility; }
            set { addNewPwFormVisibility = value; RaisePropertyChanged(); }
        }

        public string DeleteVisible
        {
            get { return deleteVisible; }
            set { deleteVisible = value; RaisePropertyChanged(); }
        }

        public string UpdateVisible
        {
            get { return updateVisible; }
            set { updateVisible = value; RaisePropertyChanged(); }
        }

        public string UpdateOKVisible
        {
            get { return updateOKVisible; }
            set { updateOKVisible = value; RaisePropertyChanged(); }
        }
        #endregion


        #region Property

        // 用于AES加密解密的key
        private byte[] keyPassword;
        public byte[] KeyPassword
        {
            get { return keyPassword; }
            set { keyPassword = value; RaisePropertyChanged(); }
        }

        // 将FirstLetter与MainWindow中的TextBox的Text进行绑定
        private char firstLetter;
        public char FirstLetter
        {
            get { return firstLetter; }
            set { firstLetter = value; RaisePropertyChanged(); }
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
        private int nowSelectedId;
        private string nowSelectedTitle;
        private string nowSelectedAccount;
        private string nowSelectedAvatar;
        private string nowSelectedPassword;
        private string nowSelectedWebsite;

        public int NowSelectedId
        {
            get { return nowSelectedId; }
            set { nowSelectedId = value;RaisePropertyChanged(); }
        }
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

        // password item detail
        private string accountBoxIsReadOnly;
        private string passwordBoxIsEnable;
        private string websiteIsReadOnly;

        public string AccountBoxIsReadOnly
        {
            get { return accountBoxIsReadOnly; }
            set { accountBoxIsReadOnly = value;RaisePropertyChanged(); }
        }
        public string PasswordBoxIsEnable
        {
            get { return passwordBoxIsEnable; }
            set { passwordBoxIsEnable = value; RaisePropertyChanged(); }
            
        }
        public string WebsiteIsReadOnly
        {
            get { return websiteIsReadOnly; }
            set { websiteIsReadOnly = value;RaisePropertyChanged(); }
        }

        // SelectedIndex
        private int index;
        public int Index
        {
            get { return index; }
            set { index = value; RaisePropertyChanged(); }
        }

        // ----- 视图层ListBox的ItemsSource-----
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
        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand UpdateOKCommand { get; set; }
        public RelayCommand DeletePasswordItemCommand { get; set; }
        public RelayCommand<string> SearchCommand { get; set; }
        public RelayCommand AboutCommand { get; set; }
        #endregion

        public void UpdateOK()
        {
            // issue
            string password = Encryptor.AESEncrypt(nowSelectedPassword, KeyPassword);
            PasswordItem passwordItem = new PasswordItem(
                NowSelectedId,
                NowSelectedTitle, 
                NowSelectedAccount, 
                password, 
                NowSelectedWebsite, 
                NowSelectedAvatar);
            DbHelper.UpdatePasswordItem(passwordItem);

            PasswordLists[Index].Account = NowSelectedAccount;
            PasswordLists[Index].Password = NowSelectedPassword;
            PasswordLists[Index].Website = NowSelectedWebsite;
            DeleteVisible = "Visible";
            UpdateVisible = "Visible";
            UpdateOKVisible = "Hidden";
        }

        public void Update()
        {
            UpdateVisible = "Hidden";
            DeleteVisible = "Hidden";
            UpdateOKVisible = "Visible";

            AccountBoxIsReadOnly = "False";
            PasswordBoxIsEnable = "True";
            WebsiteIsReadOnly = "False";
        }

        public void DeletePasswordItem()
        {
            DbHelper.DeletePasswordItem(NowSelectedId);
            PwItemDetailVisibility = "Hidden";
            DefaultVisibility = "Visible";
            PasswordLists.Remove(PasswordLists[Index]);
        }

        public void SelectPasswordItem(int Index)
        {
            if(Index == -1)
            {
                // 代表未选中任何一项
                // 则不对其进行绑定
                // do nothing
                // Index = 0;
            }
            else
            {
                this.Index = Index;
                DefaultVisibility = "Hidden";
                AddNewPwFormVisibility = "Hidden";

                SetNowSelectedItems();
                PwItemDetailVisibility = "Visible";

                AccountBoxIsReadOnly = "True";
                PasswordBoxIsEnable = "False";
                WebsiteIsReadOnly = "True";

                DeleteVisible = "Visible";
                UpdateVisible = "Visible";
                UpdateOKVisible = "Hidden";
            }
            
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
            AvatarHashTable.Add("coursera", "coursera.png");
            AvatarHashTable.Add("github", "github.png");
            AvatarHashTable.Add("gitee", "gitee.png");
            AvatarHashTable.Add("bilibili", "bilibili.png");
            AvatarHashTable.Add("qq", "qq.png");
            AvatarHashTable.Add("wechat", "wechat.png");
            AvatarHashTable.Add("weibo", "weibo.png");
            AvatarHashTable.Add("zhihu", "zhihu.png");
            AvatarHashTable.Add("paypal", "paypal.png");
            AvatarHashTable.Add("facebook", "Facebook.png");
            AvatarHashTable.Add("gmail", "gmail.png");
            AvatarHashTable.Add("instagram", "instagram.png");
            AvatarHashTable.Add("jetbrains", "jetbrains.png");
            AvatarHashTable.Add("leetcode", "leetcode.png");
            AvatarHashTable.Add("mysql", "mysql.png");
            AvatarHashTable.Add("twitter", "twitter.png");
            AvatarHashTable.Add("unity", "unity.png");
            AvatarHashTable.Add("amazon", "amazon.png");
            AvatarHashTable.Add("dribbble", "Dribbble.png");
            AvatarHashTable.Add("dingding", "dingding.png");
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
                if (Title.Equals("v2ex") || Title.Equals("1password"))
                {
                    Avatar = Title + ".png";
                } 
                else
                {
                    Avatar = "default.png";
                }
            }

            //对密码加密
            // 构造密码项一处应为函数

            string encryptedPassword = Encryptor.AESEncrypt(AddPassword, KeyPassword);
            PasswordItem passwordItem = new PasswordItem
            {
                Account = Account,
                Avatar = "../assets/" + Avatar,
                Password = encryptedPassword,
                Website = Website,
                Title = Title
            };
            DbHelper.InsertPasswordItem(passwordItem);

            passwordItem.Password = AddPassword;
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
            passwordItems = DbHelper.GetAllPasswordItems(KeyPassword);

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
            NowSelectedId = PasswordLists[Index].Id;
            NowSelectedTitle = PasswordLists[Index].Title;
            NowSelectedAvatar = PasswordLists[Index].Avatar;
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
            var url = NowSelectedWebsite;
            // .netcore 中默认UseShellExecute为false，此处需将其设置为true、
            // 否则将报错
            var psi = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        public void SetUserFavicon()
        {
            FirstLetter = System.Environment.UserName.ToUpper()[0];
        }
        public void Search(string queryStr)
        {
            foreach(PasswordItem it in PasswordLists)
            {
                if (it.Title == queryStr)
                {
                    Index = PasswordLists.IndexOf(it);
                    SetNowSelectedItems();
                    DefaultVisibility = "Hidden";
                    PwItemDetailVisibility = "Visible";
                    break;
                }
            }
        }
        public void About()
        {
            AboutView ab = new AboutView();
            ab.ShowDialog();
        }
    }
}
