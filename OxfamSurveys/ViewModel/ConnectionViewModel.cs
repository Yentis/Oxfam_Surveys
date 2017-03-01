using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;

namespace OxfamSurveys.ViewModel
{
    public class ConnectionViewModel : ViewModelBase
    {
        private readonly RelayCommand addAccountCommand;

        private String login;
        private String password;
        private String url;

        public ICommand AddAccountCommand => addAccountCommand;
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
        public String URL
        {
            get { return url; }
            set
            {
                this.url = value;
            }
        }

        public ConnectionViewModel()
        {
            addAccountCommand = new RelayCommand(AddAccount);
            login = "Login";
            password = "Password";
            url = "URL";
        }

        private void AddAccount()
        {
            
        }
    }
}
