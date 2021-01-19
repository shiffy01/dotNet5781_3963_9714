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
        bool first_bus_changed=false;
        bool last_bus_changed=false;
       
        public UpdateLine(BusLine line)
        {
            bl = BlFactory.GetBl();
            InitializeComponent();
            Line = line;
            bus_line_numberTextBox.DataContext = Line.Bus_line_number;
            first_bus_hrs_tb.DataContext = Line.First_bus.Hour;
            first_bus_min_tb.DataContext = Line.First_bus.Minute;
            lastBus_hours.DataContext = Line.Last_bus.Hour;
            lastBus_minutes.DataContext = Line.Last_bus.Minute;
            frequency_hr_tb.DataContext = Line.Frequency.Hours;
            frequency_min_tb.DataContext = Line.Frequency.Minutes;
            busStationDataGrid.DataContext = Line.Stations;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource busLineViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busLineViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // busLineViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource busStationViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busStationViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // busStationViewSource.Source = [generic data source]
        }


        private void TextBox_OnlyNumbers_PreviewKeyDown(object sender, KeyEventArgs e)
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
        //private void bus_line_numberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        //{

        //}

        //private void first_bus_hrs_tb_TextChanged(object sender, TextChangedEventArgs e)
        //{




        //}
        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((sender as TextBox).Name == "first_bus_hrs_tb" || (sender as TextBox).Name == "first_bus_min_tb")
                first_bus_changed = true;
            if ((sender as TextBox).Name == "lastBus_hours"|| (sender as TextBox).Name == "lastBus_minutes")
                last_bus_changed = true;
            updateButton.IsEnabled = true;
        }
        private void HoursLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;

            if (int.Parse(text.Text) > 23)
            {
                updateButton.IsEnabled = false;
                text.Text.Replace(text.Text, text.DataContext.ToString());
            }
        }
        private void MinutesLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;

            if (int.Parse(text.Text) > 23)
            {
                updateButton.IsEnabled = false;
                text.Text.Replace(text.Text, text.DataContext.ToString());
            }
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            int first_year=2021, first_month=01, first_day=01, last_year= 2021, last_month = 01, last_day = 01;

            if (first_bus_changed || !last_bus_changed)
            {
                first_year = Line.Last_bus.Year;
                first_month = Line.Last_bus.Month;
                first_day = Line.Last_bus.Day;
            }
            if (!first_bus_changed || last_bus_changed)
            {
                last_year = Line.First_bus.Year;
                last_month = Line.First_bus.Month;
                last_day = Line.First_bus.Day;
            }
            try
            {

                bl.UpdateBusLine(new DateTime(first_year, first_month, first_day, int.Parse(first_bus_hrs_tb.Text), int.Parse(first_bus_min_tb.Text), 0), new DateTime(last_year, last_month, last_day, int.Parse(lastBus_hours.Text), int.Parse(lastBus_minutes.Text), 0), new TimeSpan(int.Parse(frequency_hr_tb.Text), int.Parse(frequency_min_tb.Text), 0), Line.BusID, int.Parse(bus_line_numberTextBox.Text));
                MessageBoxResult mb = MessageBox.Show("The bus line was updated successfully");
                updateButton.IsEnabled = false;
            }
            catch (FrequencyConflictException ex)
            {
                MessageBoxResult mb = MessageBox.Show(ex.Message);
            }
            catch (BusLineNotFoundException)
            {
                MessageBoxResult mb = MessageBox.Show("Something has gone wrong. For an unknown reason, this busline cannot be added to the system. We regret the error");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddStationToLine addStationToLine = new AddStationToLine(Line);
            addStationToLine.Show();
        }

        //private void Frequency_hrs_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    TextBox text = sender as TextBox;
        //    if (text == null) return;
        //    if (e == null) return;

        //    string stringHrs = sender as string;
        //    if (Int32.TryParse(stringHrs, out int hrs))
        //    {
        //        if (hrs > 0 && hrs <= 23)
        //        {
        //            Frequency_hr = hrs;
        //            updateButton.IsEnabled = true;
        //        }
        //        else
        //        {
        //        frequency_hr_tb.Text.Replace(stringHrs, Line.Frequency.Hours.ToString());
        //        }

        //    }
        //}
        //private void Frequency_min_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    TextBox text = sender as TextBox;
        //    if (text == null) return;
        //    if (e == null) return;

        //    string stringMin = sender as string;
        //    if (Int32.TryParse(stringMin, out int minutes))
        //    {
        //        if (minutes > 0 && minutes <= 59)
        //        {
        //            Frequency_min = minutes;
        //            updateButton.IsEnabled = true;
        //        }
        //        else
        //        {
        //            frequency_min_tb.Text.Replace(stringMin, Line.Frequency.Minutes.ToString());
        //        }

        //    }

    }
    //private void first_bus_min_tb_TextChanged(object sender, TextChangedEventArgs e)
    //{

    //}

    //private void frequency_hr_tb_TextChanged(object sender, TextChangedEventArgs e)
    //{

    //}

    

}
  
