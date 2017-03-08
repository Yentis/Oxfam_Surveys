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
        }
        private ICommand _WindowClosing;
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
    }
}
