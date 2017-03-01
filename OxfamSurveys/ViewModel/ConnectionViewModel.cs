using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;

namespace OxfamSurveys.ViewModel
{
    public class ConnectionViewModel : ViewModelBase
    {
        private readonly RelayCommand connectCommand;

        private String login;
        private String password;

        public ICommand ConnectCommand => connectCommand;
        public String Login
        {
            get { return login; }
            set
            {
                this.login = value;
            }
        }
        public String Password
        {
            get { return password; }
            set
            {
                this.password = value;
            }
        }

        public ConnectionViewModel()
        {
            connectCommand = new RelayCommand(Connect);
            login = "Login";
            password = "Password";
        }

        private void Connect()
        {
            
        }
    }
}
