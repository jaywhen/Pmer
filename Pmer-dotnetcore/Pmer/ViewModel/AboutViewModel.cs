using GalaSoft.MvvmLight.Command;
using System.Configuration;
using System.Diagnostics;

namespace Pmer.ViewModel
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            CloseCommand = new RelayCommand(Close);
            OpenCodeReposCommand = new RelayCommand(OpenCodeRepos);
        }

        public RelayCommand OpenCodeReposCommand { get; set; }

        public void OpenCodeRepos()
        {
            var url = ConfigurationManager.AppSettings["CodeRepos"];
            var psi = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };
            Process.Start(psi);
        }

    }
}
