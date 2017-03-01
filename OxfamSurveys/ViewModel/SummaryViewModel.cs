using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;

namespace OxfamSurveys.ViewModel
{
    public class SummaryViewModel : ViewModelBase
    {
        private readonly RelayCommand seeNutValCommand;

        public ICommand SeeNutValCommand => seeNutValCommand;

        public SummaryViewModel()
        {
            seeNutValCommand = new RelayCommand(SeeNutVal);
        }

        private void SeeNutVal()
        {
            Console.WriteLine("blah");
        }
    }
}
