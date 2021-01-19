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
using BO;
using BlApi;


namespace PL
{
    /// <summary>
    /// Interaction logic for UpdateLine.xaml
    /// </summary>
 
    
    public partial class UpdateLine : Window
    {
        static IBL bl;
        BusLine Line;
        DateTime first;
        DateTime last;
        TimeSpan frequency;
        int hr, min;
        List<Time> hrs;
        List<Time> mins;
        public UpdateLine(BusLine line)
        {
            bl = BlFactory.GetBl();
            InitializeComponent();
            Line = line;
            bus_line_numberTextBox.DataContext = Line.Bus_line_number;
            mins = new List<Time>();
            hrs= new List<Time>();
            first = Line.First_bus.Date;
            last = Line.Last_bus.Date;
            frequency = new TimeSpan();
            

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource busLineViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busLineViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // busLineViewSource.Source = [generic data source]
        }

      
        private void bus_line_number_previewKeyDown(object sender, KeyEventArgs e)
        {
             TextBox text = sender as TextBox;
                if (text == null) return;
                if (e == null) return;

               
                if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
                    return;

                //allow list of system keys (add other key here if you want to allow)
                if (e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete 
                     || e.Key == Key.LeftShift || e.Key == Key.End || 
                     e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right)
                    return;

                char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);

                //allow control system keys
                if (Char.IsControl(c)) return;

                //allow digits (without Shift or Alt)
                if (Char.IsDigit(c))
                    if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                        return; //let this key be written inside the textbox

                //forbid letters and signs (#,$, %, ...)
                e.Handled = true; //ignore this key. mark event as handled, will not be routed to other controls
                return;
            

        }
        private void bus_line_numberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            updateButton.IsEnabled = true;
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
           // bl.UpdateBusLine()
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
