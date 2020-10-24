using GalaSoft.MvvmLight.Command;

namespace Pmer.ViewModel
{
    public class MainWindowModel : BaseViewModel
    {
        public MainWindowModel()
        {
            CloseCommand = new RelayCommand(Close);
        }

    }
}
