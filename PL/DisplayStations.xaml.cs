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
using System.Collections.ObjectModel;
using BO;
using BlApi;


namespace PL
{
    /// <summary>
    /// Interaction logic for DisplayStations.xaml
    /// </summary>
    
    public partial class DisplayStations : Window
    {
        static IBL bl = BlFactory.GetBl();

        ObservableCollection<BusStation> stations;

            void initialize_station_collection()
        {
             
            IEnumerable<BusStation> stationIenumerable = bl.GetAllBusStations();
            stations = new ObservableCollection<BusStation>(stationIenumerable);
            busStationDataGrid.DataContext = stations;
           
        }
        public DisplayStations()
        {
            InitializeComponent();
            initialize_station_collection();
            
        }

        private void add_station_Click(object sender, RoutedEventArgs e)
        {
            AddStation addStation = new AddStation();
            addStation.ShowDialog();
            addStation.Closed += AddStationWindow_Closed;
        }
        private void All_click(object sender, RoutedEventArgs e)
        {
            busStationDataGrid.DataContext = bl.GetAllBusStations();
        }
        private void AddStationWindow_Closed(object sender, EventArgs e)
        {

            initialize_station_collection();
        }
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            StationDetails station = new StationDetails(row.DataContext as BusStation);
            station.ShowDialog();
            station.Closed += stationWindowClosed;
        }
        private void stationWindowClosed(object sender, EventArgs e)
        {
            initialize_station_collection();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource busStationViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busStationViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // busStationViewSource.Source = [generic data source]
        }
        private void Cities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {          
            busStationDataGrid.DataContext = bl.GetAllBusStationsBy(station => station.City ==Cities.SelectedValue.ToString());
        }

        private void busStationDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
};