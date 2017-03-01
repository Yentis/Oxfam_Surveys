using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;

namespace OxfamSurveys.ViewModel
{
    public class MenuViewModel
    {
        private ICommand _UpdateNutval;

        private DateTime beginDate;
        private DateTime endDate;
        private String formsName;

        public ICommand UpdateNutval
        {
            get
            {
                return _UpdateNutval ?? (
                    _UpdateNutval = new RelayCommand(() =>
                    {

                    })
                );
            }
        }
        public DateTime BeginDate
        {
            get { return beginDate; }
            set
            {
                this.beginDate = value;
            }
        }
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                this.endDate = value;
            }
        }
        public String FormsName
        {
            get { return formsName; }
            set
            {
                this.formsName = value;
            }
        }

        public MenuViewModel()
        {
            endDate = DateTime.Now;
            beginDate = DateTime.Now.Subtract(new TimeSpan(168, 0, 0));
        }
    }
}
