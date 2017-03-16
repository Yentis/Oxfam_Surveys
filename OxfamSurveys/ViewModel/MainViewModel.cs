using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace OxfamSurveys.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            ChosenView = "AnalyticsWindow";
        }
        private ICommand _WindowClosing;
        private ICommand _ToFrameCommand;
        private string _ChosenView;


        public ICommand WindowClosing
        {
            get
            {
                return _WindowClosing ?? (
                    _WindowClosing = new RelayCommand<CancelEventArgs>(args =>
                    {
                        args.Cancel = true;
                        ViewModelLocator.Cleanup();
                        Application.Current.Shutdown();
                    })
                );
            }
        }
        public ICommand ToFrameCommand
        {
            get
            {
                return _ToFrameCommand ?? (
                    _ToFrameCommand = new RelayCommand<string>(view =>
                    {
                        ChosenView = view;
                    })
                );
            }
        }

        public string ChosenView {
            get { return _ChosenView; }
            set {
                if (value != _ChosenView)
                {
                    if (!value.EndsWith(".xaml"))
                    {
                        value = value + ".xaml";
                    }
                    _ChosenView = value;
                    RaisePropertyChanged();
                }
            }
        }
    }
}
