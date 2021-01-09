using GalaSoft.MvvmLight.Command;

namespace Pmer.ViewModel
{
    public class WarningViewModel : BaseViewModel
    {
        private string warningMsg;
        public string WarningMsg { get { return warningMsg; } set { warningMsg = value; RaisePropertyChanged(); } }

        public WarningViewModel(string warningMsg)
        {
            CloseCommand = new RelayCommand(Close);
            WarningMsg = warningMsg;
        }

    }
}
