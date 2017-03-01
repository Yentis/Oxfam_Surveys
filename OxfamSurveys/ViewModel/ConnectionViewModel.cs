using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;

namespace OxfamSurveys.ViewModel
{
    public class ConnectionViewModel : ViewModelBase
    {
        private readonly RelayCommand connectCommand;

        public ICommand ConnectCommand => connectCommand;

        public ConnectionViewModel()
        {
            connectCommand = new RelayCommand(Connect);
        }

        private void Connect()
        {
            
        }
    }
}
