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

namespace PL1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       // BO.User user = new BO.User();

        public MainWindow()
        {
            InitializeComponent();
            Main.Content = new Login();
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
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "Buses":
                    Search searchPage = new Search(bl, user);
                    NavigationService.Navigate(searchPage);
                    break;
                case "BusLines":
                    BusLinesDispaly display = new BusLinesDispaly(bl, user);
                    NavigationService.Navigate(searchPage);
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