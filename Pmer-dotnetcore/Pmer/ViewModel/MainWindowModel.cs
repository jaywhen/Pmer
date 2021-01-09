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
using Microsoft.Win32;
using Pmer.Handler;
using Pmer.Helper;

namespace Pmer.ViewModel
{
    public class MainWindowModel : BaseViewModel
    {
        public MainWindowModel(string key)
        {
            Switcher = new ComponentSwitcher();
            PasswordForm = new PasswordItem();
            KeyPassword = Encryptor.HashedKeyByMD5(key);
            SetUserFavicon();
            UpdateTagList();

            // Commands
            CloseCommand = new RelayCommand(Close);
            ShowAddNewPwFormCommand = new RelayCommand(ShowAddNewPwForm);
            AddNewPasswordCommand = new RelayCommand(AddNewPassword);
            SelectPasswordItemCommand = new RelayCommand<PasswordItem>(t =>  SelectPasswordItem(t));
            CopyCommand = new RelayCommand<string>(t => Copy(t));
            OpenWeblinkCommand = new RelayCommand(OpenWeblink);
            UpdateCommand = new RelayCommand(Update);
            UpdateOKCommand = new RelayCommand(UpdateOK);
            DeletePasswordItemCommand = new RelayCommand(DeletePasswordItem);
            SearchCommand = new RelayCommand<string>(t => Search(t));
            AboutCommand = new RelayCommand(About);
            OpenCSVFileCommand = new RelayCommand(OpenCSVFile);
        }

        #region Property
        public ComponentSwitcher Switcher { get; set; }
        
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

        #region discard
        private string tagNameAdded;
        private string title;
        private string account;
        private string addPassword;
        private string website;
        private string avatar;
        public string TagNameAdded
        {
            get { return tagNameAdded; }
            set { tagNameAdded = value; RaisePropertyChanged(); }
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
        private int index;
        public int Index
        {
            get { return index; }
            set { index = value; RaisePropertyChanged(); }
        }
        #endregion

        // above will be comment if this passing
        private PasswordItem passwordForm;
        private PasswordItem selectedPassword;
        public PasswordItem PasswordForm
        {
            get { return passwordForm; }
            set { passwordForm = value; RaisePropertyChanged(); }
        }
        public PasswordItem SelectedPassword
        {
            get { return selectedPassword; }
            set { selectedPassword = value; RaisePropertyChanged(); }
        }


        #region ViewItemSource
        // 视图层ListView\组件AddNewPw表单的ItemsSource
        private ObservableCollection<PasswordItem> passwordLists;
        
        public ObservableCollection<PasswordItem> PasswordLists
        {
            get { return passwordLists; }
            set { passwordLists = value; RaisePropertyChanged(); }
        }
        

        // 存放所有的 tag : pwlist 对
        private ObservableCollection<string> tagNames;
        private ObservableCollection<Tag> tagList;
        public ObservableCollection<Tag> TagList
        {
            get { return tagList; }
            set { tagList = value; RaisePropertyChanged(); }
        }
        public ObservableCollection<string> TagNames
        {
            get { return tagNames; }
            set { tagNames = value; RaisePropertyChanged(); }
        }
        #endregion

        #endregion


        #region Command
        public RelayCommand ShowAddNewPwFormCommand { get; set; }
        public RelayCommand AddNewPasswordCommand { get; set; }
        public RelayCommand<PasswordItem> SelectPasswordItemCommand { get; set; }
        public RelayCommand<string> CopyCommand { get; set; }
        public RelayCommand OpenWeblinkCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand UpdateOKCommand { get; set; }
        public RelayCommand DeletePasswordItemCommand { get; set; }
        public RelayCommand<string> SearchCommand { get; set; }
        public RelayCommand AboutCommand { get; set; }

        public RelayCommand OpenCSVFileCommand { get; set; }
        #endregion

        #region Command Function

        public void UpdateOK()
        {
            // waiting for test
            // issue
            string originPassword = SelectedPassword.Password;
            SelectedPassword.Password = Encryptor.AESEncrypt(SelectedPassword.Password, KeyPassword);
            DbHelper.UpdatePasswordItem(SelectedPassword);

            SelectedPassword.Password = originPassword;
            UpdateTagList();
            Switcher.UpdateOK();
        }

        public void Search(string queryStr)
        {
            PasswordItem passwordItem = DbHelper.SearchPasswordByName(queryStr);
            if(passwordItem == null)
            {
                // null 需要提示窗口
                WarningView wv = new WarningView("have no this guys");
                wv.ShowDialog();
                
            }
            else
            {
                passwordItem.Password = Encryptor.AESDecrypt(passwordItem.Password, KeyPassword);
                SelectPasswordItem(passwordItem);
            }
        }

        public void Update()
        {
            Switcher.Update();
        }

        // bug & waiting for optimization
        public void DeletePasswordItem()
        {
            
            DbHelper.DeletePasswordItem(SelectedPassword.PasswordItemId);
            Switcher.DeletePasswordItem();
            // PasswordLists.Remove(PasswordLists[Index]);
            // remove passwordItem from TagList
            //foreach(Tag tg in TagList)
            //{
            //    if(tg.TagId == SelectedPassword.TagId)
            //    {
            //       tg.PasswordItems.Remove(SelectedPassword);
            //        break;
            //    }
            //}
            UpdateTagList();
        }

        // passing
        public void SelectPasswordItem(PasswordItem selectedItem)
        {
            // 获取到了当前被选择的 PasswordItem
            SelectedPassword = new PasswordItem();
            SelectedPassword = selectedItem;
            Switcher.SelectPasswordItem();
        }

        // passing
        public void ShowAddNewPwForm()
        {
            //点击添加按钮，后台响应并创建表单
            Switcher.ShowAddNewPwForm();
        }

        // passing
        public void AddNewPassword()
        {
            
            // 表单验证
            if (string.IsNullOrEmpty(TagNameAdded))
            {
                WindowToolTip = "Please choose or add a tag for the password";
                return;
            }
            if (string.IsNullOrEmpty(PasswordForm.Title))
            {
                WindowToolTip = "Please fill the title";
                return;
            }
            if (string.IsNullOrEmpty(PasswordForm.Password))
            {
                WindowToolTip = "Please enter the password!";
            }
            if (!PasswordForm.Website.StartsWith("https://"))
            {
                PasswordForm.Website = "https://" + PasswordForm.Website;
            }

            if(DbHelper.IfTagedPasswordListContain(TagList, TagNameAdded))
            {
                // 如果该 tag 在列表中已存在
                int tagId = DbHelper.GetTagId(TagNameAdded);
                // 保留明文密码
                string password = PasswordForm.Password;
                string encryptedPassword = Encryptor.AESEncrypt(PasswordForm.Password, KeyPassword);
                PasswordForm.TagId = tagId;
                PasswordForm.Avatar = AvatarDictionary.GetAvatarPath(PasswordForm.Title);
                PasswordForm.Password = encryptedPassword;
                DbHelper.InsertPasswordItem(PasswordForm);
                // 处理好后将密码明文添加进TagList
                PasswordForm.Password = password;
                AddPasswordToTagList(PasswordForm, TagNameAdded);
            }
            else
            {
                // tag 不存在, 将新来的 tag 入表
                // 插入新 tag 返回 tag Id
                int tagId = DbHelper.InsertTagName(TagNameAdded);
                // 重复，待优化
                string password = PasswordForm.Password;
                string encryptedPassword = Encryptor.AESEncrypt(PasswordForm.Password, KeyPassword);
                PasswordForm.TagId = tagId;
                PasswordForm.Avatar = AvatarDictionary.GetAvatarPath(PasswordForm.Title);
                PasswordForm.Password = encryptedPassword;
                DbHelper.InsertPasswordItem(PasswordForm);
                // 处理好后将密码明文添加进TagList
                PasswordForm.Password = password;
                AddPasswordToTagList(PasswordForm, TagNameAdded);
            }
            UpdateTagList();

            PasswordForm.Clean();
            Switcher.AddNewPassword();
        }

        // some functions contains discard

        // passing
        /// <summary>
        /// 将添加的密码项添加到已存在的 Tag 中去，此时该密码项
        /// 的密码已在库中密文存储，在列表中明文存在
        /// </summary>
        /// <param name="passwordItem"></param>
        /// <param name="tagName"></param>
        public void AddPasswordToTagList(PasswordItem passwordItem, string tagName)
        {
            foreach(Tag td in TagList)
            {
                if (td.TagName.Equals(tagName))
                {
                    td.PasswordItems.Add(passwordItem);
                }
            }

        }

        // passing
        public void UpdateTagList()
        {
            // get all tags
            var tags = DbHelper.GetAllTags();
            TagList = new ObservableCollection<Tag>();
            foreach (Tag tg in tags)
            {
                // 拿回明文密码
                List<PasswordItem> passwordItems = DbHelper.GetAllPasswordItemFromTag(tg.TagId, KeyPassword);
                //Tag tagedPassword = new Tag
                //{
                //    TagName = tn,
                //    PasswordItems = passwordItems
                //};
                tg.PasswordItems = passwordItems;
                TagList.Add(tg);
            }
        }

        // discard
        public void AddAPwItemToPwList(PasswordItem item)
        {
            PasswordLists.Add(item);
        }

        // discard 从数据库获取数据，并展示
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

        // passing
        public static void Copy(string content)
        {
            Clipboard.SetText(content);
        }
        public void OpenWeblink()
        {
            var url = SelectedPassword.Website;
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
        public void About()
        {
            AboutView ab = new AboutView();
            ab.ShowDialog();
        }

        // developing
        public void OpenCSVFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog() 
            { 
                Filter = "chrome 密码文件(*.csv)|*.csv",
                Title = "choose a .csv file",
            };
            bool? response = openFileDialog.ShowDialog();
            var path = openFileDialog.FileName;

            // test: if passing will add a view for user setting
            // 返回的 tag 中包含的密码为密文
            Tag csvTag = FileHelper.ChromeCSVToTag(path, openFileDialog.SafeFileName, KeyPassword);
            DbHelper.InsertTag(csvTag);
            UpdateTagList();
        }
        #endregion
    }
}
