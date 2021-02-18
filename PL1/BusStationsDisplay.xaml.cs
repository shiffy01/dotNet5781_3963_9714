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
        }
        public BusStationsDisplay(IBL bl1, BO.User user, bool manage)
        {
            InitializeComponent();
            bl = bl1;
            User = user;
            Manage = manage;
            initialize()
        }
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            StationDetails station = new StationDetails(bl, User, Manage,(row.DataContext as BO.Bus));
            NavigationService.Navigate(station);

            
        }
    }
}
