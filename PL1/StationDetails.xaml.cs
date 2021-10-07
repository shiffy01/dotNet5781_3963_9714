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
using BlApi;

namespace PL1
{
    /// <summary>
    /// Interaction logic for StationDetails.xaml
    /// </summary>
    public partial class StationDetails : Page
    {
        static IBL bl;
        BO.User User;
        BO.BusStation station;
        bool Manage;
        void initialize()
        {
            stationGrid.DataContext = bl.GetBusStation(station.Code);
            busLineDataGrid.DataContext = station.Lines;
        }
        public StationDetails(IBL bl1, BO.User user, bool manage, BO.BusStation bus1)
        {
            InitializeComponent();
            bl = bl1;
            User = user;
            station = bus1;
            Manage = manage;
            if (!manage)
                Edit.Visibility = Visibility.Hidden;
            initialize();
        }
        private void editNameClick(object sender, RoutedEventArgs e)
        {
            popUp.IsOpen = true;
        }
        private void editClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(namebox.Text))
                    bl.UpdateBusStation(station.Code, namebox.Text);
                popUp.IsOpen = false;
                initialize();
            }
            catch (BO.StationNotFoundException)
            {
                MessageBox.Show("Something went wrong. We regret the error.");
            }
        }
    }
}
