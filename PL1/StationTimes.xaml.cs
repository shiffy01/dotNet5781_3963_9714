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
    /// Interaction logic for StationTimes.xaml
    /// </summary>
    public partial class StationTimes : Page
    {
        public StationTimes(IBL bl1)
        {
            InitializeComponent();
        }
        private void comboChange(object sender, SelectionChangedEventArgs e)
        {
        
        }
    }
}
