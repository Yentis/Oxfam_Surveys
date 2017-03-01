using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OxfamSurveys.ViewModel
{
    public class MenuViewModel
    {
        private ICommand _UpdateNutval;
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
    }
}
