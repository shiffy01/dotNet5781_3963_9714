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
using BlApi;

namespace PL1
{
    /// <summary>
    /// Interaction logic for AddStation.xaml
    /// </summary>
    public partial class AddStation : Page
    {
        static IBL bl;
        public AddStation(IBL bl1)
        {
            InitializeComponent();
            bl = bl1;
        }
        private void addClick(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AddBusStation(int.Parse(addressTextBox.Text), int.Parse(latitudeTextBox.Text), int.Parse(longitudeTextBox.Text), nameTextBox.Text, addressTextBox.Text, cityTextBox.Text);
            }
            catch (BO.StationALreadyExistsException ex)
            {
                MessageBoxResult mb = MessageBox.Show(ex.Message);
            }
            nav
        }
        private bool canUpdate()
        {
            double latresult;
            double longresult;
            int intresult;

            if (string.IsNullOrWhiteSpace(codeTextBox.Text) || !(int.TryParse(codeTextBox.Text, out intresult)))
                return false;
            if (string.IsNullOrWhiteSpace(latitudeTextBox.Text) || !(double.TryParse(latitudeTextBox.Text, out latresult)))
                return false;
            if (string.IsNullOrWhiteSpace(longitudeTextBox.Text) || !(double.TryParse(longitudeTextBox.Text, out longresult)))
                return false;
            if (string.IsNullOrWhiteSpace(nameTextBox.Text))
                return false;
            if (string.IsNullOrWhiteSpace(addressTextBox.Text))
                return false;
            if (string.IsNullOrWhiteSpace(cityTextBox.Text))
                return false;
            return true;
        }
        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            if (canUpdate())
                add.IsEnabled = true;
            else
                add.IsEnabled = false;
        }
    }

    
}
