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
using System.Linq;
using Microsoft.VisualBasic;

using BO;
using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for AddStationToLine.xaml
    /// </summary>
    public partial class AddStationToLine : Window
    {
        static IBL bl=BlFactory.GetBl();
        public BusLine Line;
        public BusStation station_to_add;
        int place;
        private void Initialize()
        {
           List<BusStation> stations =( bl.GetAllBusStations()).ToList();
           
            for (int i = 0; i < Line.Stations.Count(); i++)
            {
                BusStation station_to_remove=(bl.GetBusStation(((Line.Stations).ToList())[i].Code));
               BusStation find_station= stations.Find(station => station.Code == station_to_remove.Code);
                stations.Remove(find_station);
            }
            allbusStationsDataGrid.DataContext = stations;
            stationOnTheLineDataGrid.DataContext = Line.Stations.OrderBy(station=> station.Number_on_route);
        }
        public AddStationToLine(BusLine busLine)
        {
            Line = busLine;
            InitializeComponent();
            Initialize();
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource busStationViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busStationViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // busStationViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource stationOnTheLineViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("stationOnTheLineViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // stationOnTheLineViewSource.Source = [generic data source]
        }
        private void selection_changed(object sender, RoutedEventArgs e)
        {
            station_to_add = ((allbusStationsDataGrid.SelectedValue as BusStation));
            select.IsEnabled = true;
        }

        private void select_Click(object sender, RoutedEventArgs e)
        {
            stationOnTheLineDataGrid.IsEnabled = true;

        }
        private void select_place_changed(object sender, SelectionChangedEventArgs e)
        {
            StationOnTheLine station_place = (((DataGrid)sender).SelectedItem as StationOnTheLine);
            place = station_place.Number_on_route+1;
            add_button.IsEnabled = true;

        }

        private void add_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<string> distances = bl.AddStationToBusLine(Line.BusID, station_to_add.Code, place);
                if (distances != null)
                {
                    AddDistances addDistances = new AddDistances(distances);
                    addDistances.ShowDialog();
                    this.Close();
                }
            }
            catch (BusLineNotFoundException ex)
            {
                MessageBoxResult mb = MessageBox.Show(ex.Message);
            }
            catch (StationNotFoundException ex)
            {
                MessageBoxResult mb = MessageBox.Show(ex.Message);
            }
            catch (StationAlreadyExistsOnTheLinexception ex)
            {
                MessageBoxResult mb = MessageBox.Show(ex.Message);
            }
            catch (InvalidPlaceException ex)
            {
                MessageBoxResult mb = MessageBox.Show(ex.Message);
            }
            this.Close();
        }
    }
}
