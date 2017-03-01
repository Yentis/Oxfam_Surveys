using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;

namespace OxfamSurveys.ViewModel
{
    public class MenuWindowViewModel : ViewModelBase
    {
        private readonly RelayCommand downloadNutValCommand;
        private readonly RelayCommand createCommand;

        public ICommand DownloadNutValCommand => downloadNutValCommand;
        public ICommand CreateCommand => createCommand;

        public MenuWindowViewModel()
        {
            downloadNutValCommand = new RelayCommand(DownloadNutVal);
            createCommand = new RelayCommand(CreateForm);
        }

        private void DownloadNutVal()
        {
            Console.WriteLine("File download");
        }

        private void CreateForm()
        {
            Console.WriteLine("Create form");
        }
    }
}
