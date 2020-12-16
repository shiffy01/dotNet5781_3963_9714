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
    /// Interaction logic for DriveBus.xaml
    /// </summary>
    public partial class DriveBus : Window
    {
        public int curr_milage;
        private string message;
        public bool driven = false;
        public Bus CurrentBus { get; set; }
        public DriveBus(Bus bus)
        {
            CurrentBus = bus;
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void drive_grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key < Key.D0 || e.Key > Key.D9) //restricing input to numbers only
            {
                e.Handled = true;
            }
            if (e.Key == Key.Return)
            {
                 curr_milage = int.Parse(distance_tb.Text);
                 message= CurrentBus.Send_bus(curr_milage);
                if (message == "Bus sent")//need to drive the bus when the window closes
                    driven = true;
                MessageBoxResult mbResult = MessageBox.Show(message);
                this.Close();
            }
           
        }
    }
}