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
            SimpleIoc.Default.Register<AnalyticsViewModel>();
        }

        public MainViewModel MainViewModel => ServiceLocator.Current.GetInstance<MainViewModel>();
        public static MenuViewModel MenuViewModel => ServiceLocator.Current.GetInstance<MenuViewModel>();
        public ConnectionViewModel ConnectionViewModel => ServiceLocator.Current.GetInstance<ConnectionViewModel>();
        public AnalyticsViewModel AnalyticsViewModel => ServiceLocator.Current.GetInstance<AnalyticsViewModel>();

        public static void Cleanup()
        {
            /*
            if(MenuViewModel.ExcelFile.Workbook != null)
            {
                try
                {
                    MenuViewModel.ExcelFile.Workbook.Close(false);
                } catch (System.Runtime.InteropServices.COMException)
                {}
            }
            MenuViewModel.ExcelFile.ExcelApp.Quit();
            */
        }
    }
}