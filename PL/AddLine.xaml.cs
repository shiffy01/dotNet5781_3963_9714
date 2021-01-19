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
    /// Interaction logic for AddLine.xaml
    /// </summary>
    public partial class AddLine : Window
    {
        static IBL bl;
        ObservableCollection<BusStation> all_stations;
        List<int> stationsToAdd;
        int number;
        DateTime first, last;
        TimeSpan frequency;
        void initialize()
        {
            bl = BlFactory.GetBl();
            IEnumerable<BusStation> stationIenumerable = bl.GetAllBusStations();
            all_stations = new ObservableCollection<BusStation>(stationIenumerable);
            stationsToAdd = new List<int>();
        }
        public AddLine()
        {
            InitializeComponent();
            initialize();  
        }

       
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource busStationViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busStationViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // busStationViewSource.Source = [generic data source]
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //  (int line_number, List<int> stations, DateTime first_bus, DateTime last_bus, TimeSpan frequency)
            bl.AddBusLine(number, stationsToAdd, first, last, frequency);
        }
    }
}
