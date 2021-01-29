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


namespace PL
{
    /// <summary>
    /// Interaction logic for ManagementWindow.xaml
    /// </summary>
    public partial class ManagementWindow : Window
    {
        public ManagementWindow()
        {
            InitializeComponent();

        }

        private void station_display_Click(object sender, RoutedEventArgs e)
        {
            DisplayStations displayStations = new DisplayStations();
            displayStations.ShowDialog();
        }
        private void line_display_Click(object sender, RoutedEventArgs e)
        {
            DisplayBusLines displayBusLines = new DisplayBusLines();
            displayBusLines.ShowDialog();
        }
        private void Buses_click(object sender, RoutedEventArgs e)
        {
            DisplayBuses displayBuses = new DisplayBuses();
            displayBuses.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
