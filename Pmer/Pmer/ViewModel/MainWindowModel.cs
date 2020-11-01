using GalaSoft.MvvmLight.Command;
using Pmer.Db;
using System;
using System.Windows.Controls;

namespace Pmer.ViewModel
{
    public class MainWindowModel : BaseViewModel
    {
        public MainWindowModel()
        {
            db = new DbCreator();
            db.createDbConnection();
            setUserFavicon();

            CloseCommand = new RelayCommand(Close);
            AddNewPwCommand = new RelayCommand(AddNewPw);
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
        #endregion

        #region Command
        public RelayCommand AddNewPwCommand { get; set; }

        #endregion

        public void AddNewPw()
        {
            //点击添加按钮，后台响应并创建表单
            DefaultVisibility = "Hidden";
            AddNewPwFormVisibility = "Visible";


        }
    }
}
