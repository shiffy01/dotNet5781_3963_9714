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
      private int curr_milage;
        private string message;
        public Bus CurrentBus { get; set; }
        public DriveBus(Bus bus)
        {
            CurrentBus = bus;
            
            InitializeComponent();
          //drive_grid.DataContext = CurrentBus;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           // tbText = distance_tb.Text;
           
        }
        private void drive_grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                 curr_milage = int.Parse(distance_tb.Text);
                 message= CurrentBus.Send_bus(curr_milage);
                MessageBoxResult mbResult = MessageBox.Show(message);


            }
           
        }
    }
}