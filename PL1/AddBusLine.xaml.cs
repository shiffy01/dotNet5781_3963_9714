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
        void initialize()
        {
            bl = BlFactory.GetBl();
            IEnumerable<BusStation> stationIenumerable = bl.GetAllBusStations();
            all_stations = new ObservableCollection<BusStation>(stationIenumerable);
            stationsToAdd = new List<int>();
            LineTimes = new ObservableCollection<BO.BusLineTime>();
            LineTimes.Add(new BO.BusLineTime {
                Start = new DateTime(1, 1, 2000, 8, 00, 00),
                Frequency = new TimeSpan(1, 0, 0, 0),
                End = new DateTime(1, 1, 2000, 11, 00, 00)
            });
        }
        public AddBusLine()
        {
            InitializeComponent();
            initialize();
            busLineTimeDataGrid.DataContext = LineTimes;
        }

        private void TextBox_OnlyNumbers_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;

            //allow get out of the text box
            if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
                return;

            //allow list of system keys (add other key here if you want to allow)
            if (e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||
                  e.Key == Key.Home
             || e.Key == Key.End || e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right)
                return;

            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);

            //allow control system keys
            if (Char.IsControl(c)) return;

            //allow digits (without Shift or Alt)
            if (Char.IsDigit(c))
                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                    return; //let this key be written inside the textbox

            //forbid letters and signs (#,$, %, ...)
            e.Handled = true; //ignore this key. mark event as handled, will not be routed to other controls
            return;
        }
        private void busLineTimeDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
