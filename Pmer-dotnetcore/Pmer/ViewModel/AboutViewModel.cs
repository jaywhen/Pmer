using GalaSoft.MvvmLight.Command;
namespace Pmer.ViewModel
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            CloseCommand = new RelayCommand(Close);
        }
    }
}
