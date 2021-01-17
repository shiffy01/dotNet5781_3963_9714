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
         void initialize_line_collection()
        {
            bl = BlFactory.GetBl();
            IEnumerable<BusLine> stationIenumerable = bl.GetAllBusLines();
            var stations = new ObservableCollection<BusLine>(stationIenumerable);
        }//shallow copies from Ienumerable to observab
        public DisplayBusLines()
        {
            InitializeComponent();
            initialize_line_collection();
        }

        private void add_line_Click(object sender, RoutedEventArgs e)
        {
            AddLine addLine = new AddLine();
            addLine.Show();

        }
    }
}
