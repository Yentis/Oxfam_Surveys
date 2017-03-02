using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System.Windows;

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

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}
            SimpleIoc.Default.Register<ConnectionViewModel>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<MenuViewModel>();
        }

        public MainViewModel MainViewModel => ServiceLocator.Current.GetInstance<MainViewModel>();
        public static MenuViewModel MenuViewModel => ServiceLocator.Current.GetInstance<MenuViewModel>();
        public ConnectionViewModel ConnectionViewModel => ServiceLocator.Current.GetInstance<ConnectionViewModel>();
        public SummaryViewModel SummaryViewModel => ServiceLocator.Current.GetInstance<SummaryViewModel>();

        public static void Cleanup()
        {
            MenuViewModel.ExcelApp.Quit();
        }
    }
}