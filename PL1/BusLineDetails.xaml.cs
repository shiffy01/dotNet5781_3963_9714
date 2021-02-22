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
    /// Interaction logic for BusLineDetails.xaml
    /// </summary>
    public partial class BusLineDetails : Page
    {
        IBL bl;
        BO.BusLine line;
        void initialize()
        {
            stationsGrid.DataContext = line.Stations.OrderBy(b=>b.Number_on_route);
            timesText.Text = bl.printTimes(line.Times.ToList());
            lineGrid.DataContext = line;
        }
        public BusLineDetails(IBL bl1, BO.BusLine line1)
        {
            InitializeComponent();
            bl = bl1;
            line = line1;
            initialize();
        }

    }
}
