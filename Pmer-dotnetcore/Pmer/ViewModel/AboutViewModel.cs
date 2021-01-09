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
            SubmitNewIssueCommand = new RelayCommand(SubmitNewIssue);
        }

        public RelayCommand OpenCodeReposCommand { get; set; }
        public RelayCommand SubmitNewIssueCommand { get; set; }
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
        public void SubmitNewIssue()
        {
            var url = ConfigurationManager.AppSettings["NewIssue"];
            var psi = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
    }
}
