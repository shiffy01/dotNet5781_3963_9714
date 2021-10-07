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
        bool Manage;
        

        public MainWindow(IBL bl1, BO.User user, bool manage)
        {
            InitializeComponent();
            bl = bl1;
            User = user;
            Manage = manage;
            if (manage)
            {
                icon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Bus;
                text.Text = "Buses";
                Main.Navigate(new BusesDisplay(bl, User));
            }
            else
            {
                icon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Routes;
                text.Text = "Routes";
                Main.Navigate(new RouteSearch(bl, Manage, User));
            }
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
             GridMain.Children.Clear();
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {

                case "ChangingItem":
                    if (Manage)
                    {
                        BusesDisplay busesDisplay = new BusesDisplay(bl, User);
                        this.Main.NavigationService.Navigate(busesDisplay);
                    }
                    else
                    {
                        RouteSearch routeSearch = new RouteSearch(bl, Manage, User);
                        this.Main.NavigationService.Navigate(routeSearch);
                    }
                    break;
                case "BusLines":
                    BusLinesDispaly busLinesDispaly = new BusLinesDispaly(bl, User, Manage);
                    this.Main.NavigationService.Navigate(busLinesDispaly);
                    break;
                case "BusStations":
                    BusStationsDisplay busStationssDispaly = new BusStationsDisplay(bl, User, Manage);
                    this.Main.NavigationService.Navigate(busStationssDispaly);
                    break;
                case "Settings":
                    SettingsPage settings = new SettingsPage(bl, User, Manage);
                    this.Main.NavigationService.Navigate(settings);
                    break;
                case "LogOut":
                    this.Close();
                    break;
                default:
                    break;
            }
        }
    }
}