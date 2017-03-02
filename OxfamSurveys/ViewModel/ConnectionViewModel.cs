using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;

namespace OxfamSurveys.ViewModel
{
    public class ConnectionViewModel : ViewModelBase
    {
        private readonly RelayCommand saveCommand;


        public ICommand SaveCommand => saveCommand;

        public string KoboLogin { get; set; }
        public string KoboPassword { get; set; }
        public string KoboUrl { get; set; }
        public string CTOLogin { get; set; }
        public string CTOPassword { get; set; }
        public string CTOUrl { get; set; }

        

        //Check for URL authenticity

        public ConnectionViewModel()
        {
            saveCommand = new RelayCommand(SaveSettings);
            string[] config;
            config = RetrieveConfig();
            KoboLogin = config[0];
            KoboPassword = config[1];
            KoboUrl = config[2];
            CTOLogin = config[3];
            CTOPassword = config[4];
            CTOUrl = config[5];
        }

        private void SaveSettings()
        {
            if (CTOLogin != null && CTOPassword != null && CTOUrl != null && KoboLogin != null && KoboPassword != null && KoboUrl != null)
            {
                string[] lines = { KoboLogin, KoboPassword, KoboUrl,CTOLogin, CTOPassword, CTOUrl };
                string filePath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + @"\config.txt";
                File.WriteAllLines(filePath, lines);
            }
        }

        private string[] RetrieveConfig()
        {
            string[] config = new string[6] {
                "empty",
                "empty",
                "empty",
                "empty",
                "empty",
                "empty"
            };
            string[] lines = config;

            string filePath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + @"\\config.txt";

            if(!File.Exists(filePath))
            {
                File.WriteAllLines(filePath, config);
            }else
            {
                lines = File.ReadAllLines(filePath);

                for (int i = 0; i < lines.Length; i++)
                {
                    config[i] = lines[i];
                }
            }

            return config;
        }
    }
}
