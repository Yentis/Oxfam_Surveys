using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using IniParser.Model;
using OxfamSurveys.Messages;
using OxfamSurveys.Models;
using System;
using System.Windows.Input;
using static OxfamSurveys.Models.ApiConfig;

namespace OxfamSurveys.ViewModel
{
    public class ConnectionViewModel : ViewModelBase
    {
        private readonly ApiConfig apiConfig = new ApiConfig();

        // TODO: Make passwords PasswordBox instead of TextBox
        #region Public Attributes
        public string KoboLogin { get; set; }
        public string KoboPassword { get; set; }
        public string KoboUrl { get; set; }
        public string CTOLogin { get; set; }
        public string CTOPassword { get; set; }
        public string CTOUrl { get; set; }
        #endregion

        #region Commands
        private ICommand _SaveCommand;
        public ICommand SaveCommand
        {
            get
            {
                return _SaveCommand ?? (
                    _SaveCommand = new RelayCommand(() =>
                    {
                        apiConfig.Set(Apis.KoBoCollect, KoboLogin, KoboPassword, KoboUrl);
                        apiConfig.Set(Apis.SurveyCTO, CTOLogin, CTOPassword, CTOUrl);
                        MessengerInstance.Send(new FormsChanged());
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
