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
using BlApi;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for LineDetails.xaml
    /// </summary>
    public partial class LineDetails : Window
    {
        static IBL bl;
      
       
            BusLine Line;
        public LineDetails(BusLine line)
        {
            bl = BlFactory.GetBl();
            InitializeComponent();
            Line = line;
            lineGrid.DataContext = Line;
           // busStationDataGrid.DataContext = Line.Stations;
            first_busTextBlock.DataContext = Line.First_bus.TimeOfDay;
            last_busTextBlock.DataContext = Line.Last_bus.TimeOfDay;
        }

        private void update_line_Click(object sender, RoutedEventArgs e)
        {
            UpdateLine updateLine = new UpdateLine(Line);
            updateLine.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource busLineViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busLineViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // busLineViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource busStationViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busStationViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // busStationViewSource.Source = [generic data source]
        }

        private void delete_line_click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this line?", " Alert", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                //BusLine line = sender as BusLine;
                try
                {
                    bl.DeleteBusLine(Line.BusID);
                }
                catch (BusLineNotFoundException ex)
                {
                    MessageBoxResult msgBox2 = MessageBox.Show(ex.Message, " Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                this.Close();
            }
        }
    }
}
