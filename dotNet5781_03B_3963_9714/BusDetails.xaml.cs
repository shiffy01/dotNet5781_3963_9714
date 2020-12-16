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
using dotNet5781_01_3963_9714;

namespace dotNet5781_03B_3963_9714
{
    /// <summary>
    /// Interaction logic for BusDetails.xaml
    /// </summary>
    
    public partial class BusDetails : Window
    {
        public bool Fill=false;
        public bool Tune=false;
        public Bus thisBus;
        public BusDetails(Bus b)
        {
            InitializeComponent();
            details.DataContext = b;
            thisBus = b;
         
        }

        private void Button_Click_tune(object sender, RoutedEventArgs e)
        {
            Tune = true;
            this.Close();
        }
        private void Button_Click_fill(object sender, RoutedEventArgs e)
        {
            Fill = true;
            this.Close();
        }
    }
}
