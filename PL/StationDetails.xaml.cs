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
    /// Interaction logic for StationDetails.xaml
    /// </summary>
    public partial class StationDetails : Window
    {
        static IBL bl;
        BusStation station;
        public StationDetails(BusStation busstation)
        {
            InitializeComponent();
            bl = BlFactory.GetBl();
            station = busstation;
            BusLineDataGrid.DataContext = busstation.Lines;
            Station_details.DataContext = busstation;
            
        }

        private void update_station_Click(object sender, RoutedEventArgs e)
        {
            UpdateStation updateStation = new UpdateStation(station);
            updateStation.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource busStationViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busStationViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // busStationViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource busLineViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busLineViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // busLineViewSource.Source = [generic data source]
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            nameTextBox.IsReadOnly = false;
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void drive_grid_KeyDown(object sender, KeyEventArgs e)
        {
           
                e.Handled = true;
  
            if (e.Key == Key.Return)
            {
                try
                {
                    bl.UpdateBusStation(station.Code, nameTextBox.Text);
                    MessageBoxResult mb = MessageBox.Show("This station's name was changed successfully");
                }
                catch (StationNotFoundException ex)
                {
                    MessageBoxResult mb = MessageBox.Show(ex.Message);
                }
                
                
            }

        }
    }
};
