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

        private DateTime beginningDate;
        private DateTime endingDate;
        private String formsName;

        public ICommand DownloadNutValCommand => downloadNutValCommand;
        public ICommand CreateCommand => createCommand;
        public DateTime BeginningDate
        {
            get { return beginningDate; }
            set
            {
                this.beginningDate = value;
            }
        }
        public DateTime EndingDate
        {
            get { return endingDate; }
            set
            {
                this.endingDate = value;
            }
        }
        public String FormsName
        {
            get { return formsName; }
            set
            {
                this.formsName = value;
            }
        }

        public MenuWindowViewModel()
        {
            downloadNutValCommand = new RelayCommand(DownloadNutVal);
            createCommand = new RelayCommand(CreateForm);
        }

        private void DownloadNutVal()
        {
            Console.WriteLine("blah");
        }

        private void CreateForm()
        {
            Console.WriteLine("blah");
        }
    }
}
