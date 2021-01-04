using GalaSoft.MvvmLight;
using System.Collections.Generic;


namespace Pmer.Models
{
    public class Tag : ViewModelBase
    {
        #region Property
        private int tagId;
        private string tagName;
        private List<PasswordItem> passwordItems;

        public string TagName
        {
            get { return tagName; }
            set { tagName = value; RaisePropertyChanged(); }
        }
        public int TagId
        {
            get { return tagId; }
            set { tagId = value; RaisePropertyChanged(); }
        }
        public List<PasswordItem> PasswordItems
        {
            get { return passwordItems; }
            set { passwordItems = value; RaisePropertyChanged(); }
        }
        #endregion
    }
}
