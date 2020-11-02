using GalaSoft.MvvmLight.Command;
using Pmer.Db;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Windows.Controls;
using System.Windows.Data;

namespace Pmer.ViewModel
{
    public class MainWindowModel : BaseViewModel
    {
        public MainWindowModel()
        {
            db = new DbCreator();
            db.createDbConnection();
            setUserFavicon();
            Testc();

            CloseCommand = new RelayCommand(Close);
            ShowAddNewPwFormCommand = new RelayCommand(ShowAddNewPwForm);
            AddNewPasswordCommand = new RelayCommand(AddNewPassword);
            FuncCommand = new RelayCommand<int>(t => Func(t));

        }

        DbCreator db;
        
        #region Property

        // 将FirstLetter与MainWindow中的TextBox的Text进行绑定
        private char firstLetter;
        private string addNewPwFormVisibility = "Hidden";
        private string defaultVisibility = "Visible";
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
            FirstLetter = db.getUserNameFromMpTable().ToUpper()[0];
        }

        // ----------
        private string title;
        private string account;
        private string addPassword;
        private string website;
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

        private int testIndex;
        public int TestIndex
        {
            get { return testIndex; }
            set { testIndex = value; RaisePropertyChanged(); }

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
        public RelayCommand<int> FuncCommand { get; set; }
        #endregion

        public void Func(int TestIndex)
        {
            PasswordLists[TestIndex].Title = "test";
        }

        public void ShowAddNewPwForm()
        {
            //点击添加按钮，后台响应并创建表单
            DefaultVisibility = "Hidden";
            AddNewPwFormVisibility = "Visible";
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
            db.InsertNewPw(Title, Account, AddPassword, Website);

        }


        public void Testc()
        {
            PasswordItem bilibili = new PasswordItem();
            bilibili.ItemAvatar = "asset/bilibili.png";
            bilibili.Title = "Bilibili";
            bilibili.Account = "Vincentiang";
            PasswordLists = new ObservableCollection<PasswordItem>();

            PasswordItem github = new PasswordItem();
            github.ItemAvatar = "asset/github.png";
            github.Title = "Github";
            github.Account = "Jaywhen";
            PasswordLists = new ObservableCollection<PasswordItem>();


            PasswordLists.Add(bilibili);
            PasswordLists.Add(github);
        }
    }
}
