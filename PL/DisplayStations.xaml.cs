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
        static IBL bl;

        ObservableCollection<BusStation> stations;

            void initialize_station_collection()
        {
             bl = BlFactory.GetBl();
            IEnumerable<BusStation> stationIenumerable = bl.GetAllBusStations();
            stations = new ObservableCollection<BusStation>(stationIenumerable);
        }//shallow copies from Ienumerable to observable, so might be an issue
        public DisplayStations()
        {
            InitializeComponent();
            initialize_station_collection();
            busStationDataGrid.DataContext = stations;
        }

        private void add_station_Click(object sender, RoutedEventArgs e)
        {
            AddStation addStation = new AddStation();
            addStation.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource busStationViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busStationViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // busStationViewSource.Source = [generic data source]
        }
    }
}
;