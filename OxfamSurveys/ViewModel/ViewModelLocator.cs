using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace OxfamSurveys.ViewModel
{
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            
            SimpleIoc.Default.Register<ConnectionViewModel>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<FormCreationViewModel>();
            SimpleIoc.Default.Register<AnalyticsViewModel>();
            SimpleIoc.Default.Register<FormCreationViewModel>();
        }

        public MainViewModel MainViewModel => ServiceLocator.Current.GetInstance<MainViewModel>();
        public static FormCreationViewModel MenuViewModel => ServiceLocator.Current.GetInstance<FormCreationViewModel>();
        public ConnectionViewModel ConnectionViewModel => ServiceLocator.Current.GetInstance<ConnectionViewModel>();
        public AnalyticsViewModel AnalyticsViewModel => ServiceLocator.Current.GetInstance<AnalyticsViewModel>();
        public FormCreationViewModel FormCreationViewModel => ServiceLocator.Current.GetInstance<FormCreationViewModel>();

        public static void Cleanup()
        {
        }
    }
}