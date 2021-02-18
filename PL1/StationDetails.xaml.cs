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
        void initialize()
        {
            stationGrid.DataContext = station;
            var list =
            busLineDataGrid.DataContext = bl.GetBusLinesOfStation(station.Code);
        }
        public StationDetails(IBL bl1, BO.User user, bool manage, BO.BusStation bus1)
        {
            InitializeComponent();
            bl = bl1;
            User = user;
            station = bus1;
            if (!manage)
                Edit.Visibility = Visibility.Hidden;
        }
        private void editClick(object sender, RoutedEventArgs e)
        {
            if (namebox.Text != null)
            {
                try
                {
                    bl.UpdateBusStation(station.Code, namebox.Text);
                }
                catch (BO.StationNotFoundException ex)
                {
                    MessageBoxResult mb = MessageBox.Show("Something went wrong. we regret the error.");
                }
                initialize();
            }
        }
    }
}
