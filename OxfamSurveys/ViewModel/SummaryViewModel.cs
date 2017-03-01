using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace OxfamSurveys.ViewModel
{
    public class SummaryViewModel : ViewModelBase
    {
        private RelayCommand seeNutValCommand;

        public RelayCommand SeeNutValCommand => seeNutValCommand;
    }
}
