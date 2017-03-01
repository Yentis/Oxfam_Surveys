namespace OxfamSurveys.ViewModel
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel => new MainViewModel();
        public ConnectionViewModel ConnectionViewModel => new ConnectionViewModel();
        public SummaryViewModel SummaryViewModel => new SummaryViewModel();
        public MenuWindowViewModel MenuWindowViewModel => new MenuWindowViewModel();
    }
}