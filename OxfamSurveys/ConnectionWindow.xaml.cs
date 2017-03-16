using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OxfamSurveys
{
    /// <summary>
    /// Interaction logic for Connection.xaml
    /// </summary>
    public partial class Connection : Page
    {
        public Connection()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            //Cast the 'sender' to a PasswordBox
            PasswordBox pBox = sender as PasswordBox;

            //Set this "EncryptedPassword" dependency property to the "SecurePassword"
            //of the PasswordBox.
            OxfamSurveysAttachedProperties.SetEncryptedPassword(pBox, pBox.SecurePassword);
        }
    }
}
