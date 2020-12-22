using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pmer.Handler
{
    // 封装了一些组件调换的方法
    public class ComponentSwitcher : ViewModelBase
    {
        #region Property
        private string deleteVisible;
        private string updateVisible;
        private string updateOKVisible;
        private string addNewPwFormVisibility;
        private string defaultVisibility;
        private string pwItemDetailVisibility;

        private string accountBoxIsReadOnly;
        private string passwordBoxIsEnable;
        private string websiteIsReadOnly;

        public string AccountBoxIsReadOnly
        {
            get { return accountBoxIsReadOnly; }
            set { accountBoxIsReadOnly = value; RaisePropertyChanged(); }
        }
        public string PasswordBoxIsEnable
        {
            get { return passwordBoxIsEnable; }
            set { passwordBoxIsEnable = value; RaisePropertyChanged(); }

        }
        public string WebsiteIsReadOnly
        {
            get { return websiteIsReadOnly; }
            set { websiteIsReadOnly = value; RaisePropertyChanged(); }
        }
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

        public ComponentSwitcher()
        {
            AddNewPwFormVisibility = "Hidden";
            DefaultVisibility = "Visible";
            PwItemDetailVisibility = "Hidden";
        }

        public void UpdateOK()
        {
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
            PwItemDetailVisibility = "Hidden";
            DefaultVisibility = "Visible";
        }

        public void SelectPasswordItem()
        {
            DefaultVisibility = "Hidden";
            AddNewPwFormVisibility = "Hidden";
            PwItemDetailVisibility = "Visible";
            AccountBoxIsReadOnly = "True";
            PasswordBoxIsEnable = "False";
            WebsiteIsReadOnly = "True";
            DeleteVisible = "Visible";
            UpdateVisible = "Visible";
            UpdateOKVisible = "Hidden";
        }

        public void ShowAddNewPwForm()
        {
            PwItemDetailVisibility = "Hidden";
            DefaultVisibility = "Hidden";
            AddNewPwFormVisibility = "Visible";
        }

        public void AddNewPassword()
        {
            DefaultVisibility = "Visible";
            AddNewPwFormVisibility = "Hidden";
        }

        public void Search()
        {
            DefaultVisibility = "Hidden";
            PwItemDetailVisibility = "Visible";
        }






    }
}
