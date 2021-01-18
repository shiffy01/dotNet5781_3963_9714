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
using System.Collections.ObjectModel;
using BO;
using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for DisplayBusLines.xaml
    /// </summary>
    public partial class DisplayBusLines : Window
    {

        static IBL bl;
        ObservableCollection<BusLine> Lines;
         void initialize_line_collection()
        {
            bl = BlFactory.GetBl();
            IEnumerable<BusLine> lineIenumerable = bl.GetAllBusLines();
             Lines =new ObservableCollection<BusLine>(lineIenumerable);
        }//shallow copies from Ienumerable to observab
        public DisplayBusLines()
        {
            InitializeComponent();
            initialize_line_collection();
            busLineDataGrid.DataContext = Lines;
        }

        private void add_line_Click(object sender, RoutedEventArgs e)
        {
            AddLine addLine = new AddLine();
            addLine.Show();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource busLineViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busLineViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // busLineViewSource.Source = [generic data source]
        }

        private void busLineDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
