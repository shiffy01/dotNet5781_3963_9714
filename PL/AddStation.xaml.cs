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
using System.Windows.Shapes;
using BO;
using BlApi;
namespace PL
{
    /// <summary>
    /// Interaction logic for AddStation.xaml
    /// </summary>
    public partial class AddStation : Window
    {
        private bool canUpdate()
        {
            double latresult;
            double longresult;
            int intresult;
            
            if (string.IsNullOrWhiteSpace(codeTextBox.Text) || !(int.TryParse(codeTextBox.Text, out intresult)))
                return false;
            if (string.IsNullOrWhiteSpace(latitudeTextBox.Text)|| !(double.TryParse(latitudeTextBox.Text, out latresult)))
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
        static IBL bl;
        public BusStation station;
      
        public AddStation()
        {
            InitializeComponent();
            bl = BlFactory.GetBl();
            //this.KeyDown += new KeyEventHandler(drive_grid_KeyDown);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource busStationViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busStationViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // busStationViewSource.Source = [generic data source]
        }
       
       

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            if (canUpdate())
                add.IsEnabled = true;
            else
                add.IsEnabled = false;
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AddBusStation(int.Parse(codeTextBox.Text), double.Parse(latitudeTextBox.Text), double.Parse(longitudeTextBox.Text), nameTextBox.Text, addressTextBox.Text, cityTextBox.Text);
                MessageBoxResult mb = MessageBox.Show("The station was successfully added to the system");
               
                
            }
            catch (StationALreadyExistsException ex)
            {
                MessageBoxResult mb = MessageBox.Show(ex.Message);
               
            }
           
            this.Close();
        }
        //private void drive_grid_KeyDown(object sender, KeyEventArgs e)
        //{

        //    e.Handled = true;
        //    if (canUpdate())
        //        update.IsEnabled = true;

        //}
    }
}
