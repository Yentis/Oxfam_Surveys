using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using IniParser.Model;
using OxfamSurveys.Messages;
using OxfamSurveys.Models;
using System;
using System.Threading;
using System.Windows.Input;
using static OxfamSurveys.Models.ApiConfig;

namespace OxfamSurveys.ViewModel
{
    public class ConnectionViewModel : ViewModelBase
    {
        private readonly ApiConfig apiConfig = new ApiConfig();

        private string _SaveContent = "Save";
        private bool _SaveEnabled = true;

        // TODO: Make passwords PasswordBox instead of TextBox
        #region Public Attributes
        public string KoboLogin { get; set; }
        public string KoboPassword { get; set; }
        public string KoboUrl { get; set; }
        public string CTOLogin { get; set; }
        public string CTOPassword { get; set; }
        public string CTOUrl { get; set; }

        public string SaveContent
        {
            get
            {
                return _SaveContent;
            }

            set
            {
                _SaveContent = value;
                RaisePropertyChanged();
            }
        }

        public bool SaveEnabled
        {
            get
            {
                return _SaveEnabled;
            }

            set
            {
                _SaveEnabled = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        private void SaveInfo()
        {
            SaveEnabled = false;
            SaveContent = "Saving...";
            apiConfig.Set(Apis.KoBoCollect, KoboLogin, KoboPassword, KoboUrl);
            apiConfig.Set(Apis.SurveyCTO, CTOLogin, CTOPassword, CTOUrl);
            MessengerInstance.Send(new FormsChanged());
            SaveEnabled = true;
            SaveContent = "Save";
        }

        #region Commands
        private ICommand _SaveCommand;
        public ICommand SaveCommand
        {
            get
            {
                return _SaveCommand ?? (
                    _SaveCommand = new RelayCommand(() =>
                    {
                        Thread newThread = new Thread(SaveInfo);
                        newThread.Start();
                    })
                );
            }
        }
        #endregion

        public ConnectionViewModel()
        {
            try
            {
                Config koboConfig = apiConfig.Get(Apis.KoBoCollect);
                KoboLogin = koboConfig.Username;
                KoboPassword = koboConfig.Password;
                KoboUrl = koboConfig.Server;
            }
            catch (Exception)
            {
                Console.WriteLine("API config file couldn't be read");
            }
            
            try
            {
                Config CTOConfig = apiConfig.Get(Apis.SurveyCTO);
                CTOLogin = CTOConfig.Username;
                CTOPassword = CTOConfig.Password;
                CTOUrl = CTOConfig.Server;
            }
            catch (Exception)
            {
                Console.WriteLine("API config file couldn't be read");
            }
        }
    }
}
