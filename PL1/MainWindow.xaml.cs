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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BO.User User;
        static IBL bl;
        

        public MainWindow(IBL bl1, BO.User user)
        {
            InitializeComponent();
            Main.Content = new HomePage();
            bl = bl1;
            User = user;
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl usc = null;
            GridMain.Children.Clear();
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {

                case "Buses":
                    Search searchPage = new Search(bl, User);
                    this.Main.NavigationService.Navigate(searchPage);
                    break;
                case "BusLines":
                    BusLinesDispaly busLinesDispaly = new BusLinesDispaly(bl, User);
                    this.Main.NavigationService.Navigate(busLinesDispaly);

                    break;
                case "BusStops":

                    break;
                case "Routes":

                    break;
                case "SearchHistory":

                    break;
                case "Settings":

                    break;
                case "LogOut":

                    break;
                default:
                    break;
            }
        }
    }
}