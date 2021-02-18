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
using System.Collections.ObjectModel;
using BO;
using BlApi;

namespace PL1
{
    /// <summary>
    /// Interaction logic for AddBusLine.xaml
    /// </summary>
    public partial class AddBusLine : Page
    {
        ObservableCollection<BO.BusLineTime> LineTimes;
        static IBL bl;
        ObservableCollection<BusStation> all_stations;
        List<int> stationsToAdd;
       
        public AddBusLine()
        {
            InitializeComponent();
            LineTimes = new ObservableCollection<BO.BusLineTime>();
            LineTimes.Add(new BO.BusLineTime {Start= new DateTime(1, 1, 2000, 8, 00, 00),
                                                        Frequency= new TimeSpan(1, 0, 0, 0), 
                                                        End= new DateTime(1, 1, 2000, 11, 00, 00) });
            busLineTimeDataGrid.DataContext = LineTimes;
        }

        private void busLineTimeDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
