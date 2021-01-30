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
    /// Interaction logic for AddBus.xaml
    /// </summary>
    public partial class AddBus : Window
    {
        bool wifi=false, access = false;
        static IBL bl;
        public AddBus()
        {
            bl = BlFactory.GetBl();
            InitializeComponent();
        }
        private void wifiChecked(object sender, RoutedEventArgs e)
        {
            wifi = true;
        }
        private void wifiUnchecked(object sender, RoutedEventArgs e)
        {
            wifi = false;
        }
        private void accessChecked(object sender, RoutedEventArgs e)
        {
            access = true;
        }

        private void AddButton(object sender, RoutedEventArgs e)
        {
            string license=bl.AddBus(access, wifi);
            MessageBoxResult mb = MessageBox.Show("Bus number "+ license+" was added to the system!");
            this.Close();
        }

        private void accessUnchecked(object sender, RoutedEventArgs e)
        {
            access = false;
        }
    }
}
