using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace OxfamSurveys.ViewModel
{
    public class ConnectionViewModel : ViewModelBase
    {
        private RelayCommand connectCommand;

        public RelayCommand ConnectCommand => connectCommand;
    }
}
