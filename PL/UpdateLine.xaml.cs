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
        //DateTime first;
        //DateTime last;
        TimeSpan Frequency;
        bool First_MinTxtChange, First_HourTxtChange;
        int First_hr;
        int First_min;
        int Last_hr;
        int Last_min;
        int Frequency_hr;
        int Frequency_min;
        //List<Time> hrs;
        //List<Time> mins;
        public UpdateLine(BusLine line)
        {
            bl = BlFactory.GetBl();
            InitializeComponent();
            Line = line;
            bus_line_numberTextBox.DataContext = Line.Bus_line_number;

            first_bus_hrs_tb.DataContext = Line.First_bus.Hour;
            first_bus_min_tb.DataContext = Line.First_bus.Minute;
            frequency_hr_tb.DataContext = Line.Frequency.Hours;
            frequency_min_tb.DataContext = Line.Frequency.Minutes;

            First_MinTxtChange = false;
            First_HourTxtChange = false;
            //mins = new List<Time>();
            //hrs= new List<Time>();
            //first = Line.First_bus.Date;
            //last = Line.Last_bus.Date;
            //frequency = new TimeSpan();
            //first_busHr.ItemsSource = hrs;
            //first_busHr.DisplayMemberPath = "First_Bus_hour";
            //first_busHr.SelectedIndex = 0;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource busLineViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busLineViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // busLineViewSource.Source = [generic data source]
        }

      
        private void bus_line_previewKeyDown(object sender, KeyEventArgs e)
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
          
        }

        private void first_bus_hrs_tb_TextChanged(object sender, TextChangedEventArgs e)
        {


            
        }
        private void First_hrs_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;

            string stringHrs = sender as string;
            if (Int32.TryParse(stringHrs, out int hrs))
            {
                if (hrs > 0 && hrs <= 23)
                {
                    First_hr = hrs;
                    updateButton.IsEnabled = true;
                }
                else
                {
                    first_bus_hrs_tb.Text.Replace(stringHrs, Line.First_bus.Hour.ToString());
                }

            }
        }
        private void First_min_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;

            string stringMin = sender as string;
            if (Int32.TryParse(stringMin, out int minutes))
            {
                if (minutes > 0 && minutes <= 59)
                {
                    First_min = minutes;
                    updateButton.IsEnabled = true;
                }
                else
                {
                    first_bus_min_tb.Text.Replace(stringMin, Line.First_bus.Minute.ToString());
                }

            }

        }


        private void Frequency_hrs_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;

            string stringHrs = sender as string;
            if (Int32.TryParse(stringHrs, out int hrs))
            {
                if (hrs > 0 && hrs <= 23)
                {
                    Frequency_hr = hrs;
                    updateButton.IsEnabled = true;
                }
                else
                {
                frequency_hr_tb.Text.Replace(stringHrs, Line.Frequency.Hours.ToString());
                }

            }
        }
        private void Frequency_min_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;

            string stringMin = sender as string;
            if (Int32.TryParse(stringMin, out int minutes))
            {
                if (minutes > 0 && minutes <= 59)
                {
                    Frequency_min = minutes;
                    updateButton.IsEnabled = true;
                }
                else
                {
                    frequency_min_tb.Text.Replace(stringMin, Line.Frequency.Minutes.ToString());
                }

            }

        }
        private void first_bus_min_tb_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void frequency_hr_tb_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
           // bl.UpdateBusLine()
        }
        

    }
}
