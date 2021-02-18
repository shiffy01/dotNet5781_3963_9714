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
using BlApi;

namespace PL1
{
    /// <summary>
    /// Interaction logic for BusStationsDisplay.xaml
    /// </summary>
    public partial class BusStationsDisplay : Page
    {

        static IBL bl;
        BO.User User;
        bool Manage;
        ObservableCollection<BO.BusStation> stations;
        void initialize()
        {
            busStationDataGrid.DataContext = bl.GetAllBusStations();
            CitiesBox.DataContext = bl.GetByCities();
        }
        public BusStationsDisplay(IBL bl1, BO.User user, bool manage)
        {
            InitializeComponent();
            bl = bl1;
            User = user;
            Manage = manage;
            if (!Manage)
                addButton.Visibility = Visibility.Hidden;
            initialize();
        }
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            StationDetails station = new StationDetails(bl, User, Manage,(row.DataContext as BO.BusStation));
            NavigationService.Navigate(station);

        }
        private void AddStationClick(object sender, RoutedEventArgs e)
        {
            AddStation addStation = new AddStation(bl, User);
            NavigationService.Navigate(addStation);
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
            if (text.Name == "latitudeTextBox" || text.Name == "longitudeTextBox")
                if (e.Key == Key.OemPeriod)
                {
                    if (!(text.Text).Contains("."))
                        return;//if there is not already a decimal point in the textbox, let this key be written in the textbox

                }

            //forbid letters and signs (#,$, %, ...)
            e.Handled = true; //ignore this key. mark event as handled, will not be routed to other controls
            return;
        }
        private void searchChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                busStationDataGrid.DataContext = bl.GetAllBusStationsBy(b => b.Code == int.Parse(searchBox.Text));
            }
            catch (BO.StationNotFoundException)
            {
            //no results..
            }
        }
        private void allStationsClick(object sender, RoutedEventArgs e)
        {
            initialize();
        }
        private void cityChanged(object sender, RoutedEventArgs e)
        {
            busStationDataGrid.DataContext = bl.GetAllBusStationsBy(b => b.City == CitiesBox.Text);
        }


    }
}
