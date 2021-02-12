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
//using Xceed.Wpf.Toolkit;


namespace PL
{
    /// <summary>
    /// Interaction logic for UpdateLine.xaml
    /// </summary>


    public partial class UpdateLine : Window
    {
        static IBL bl;
        BusLine Line;
        int Hours;
        int Minutes;

        private void initialize()
        {
            bus_line_numberTextBox.DataContext = Line.Bus_line_number;
            Line = bl.GetBusLine(Line.BusID);
            stationOnTheLineDataGrid.DataContext = Line.Stations.OrderBy(station => station.Number_on_route);
            //first_bus.DefaultValue = Line.First_bus;
            //last_bus.DefaultValue = Line.Last_bus;
            ////frequencyPicker.
        }
        public UpdateLine(BusLine line)
        {
            bl = BlFactory.GetBl();
            InitializeComponent();
            Line = line;
            initialize();

        }
        private void splitStringTOTwoInts(string str, ref int num1, ref int num2, char splitHere)
        {
            string[] codes = str.Split(splitHere);
            try
            {
                num1 = Int32.Parse(codes[0]);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            try
            {
                num2 = Int32.Parse(codes[1]);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource busLineViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busLineViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // busLineViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource busStationViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busStationViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // busStationViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource stationOnTheLineViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("stationOnTheLineViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // stationOnTheLineViewSource.Source = [generic data source]
        }

        private void change(object sender, EventArgs e)
        {
            splitStringTOTwoInts(freq.Text, ref Hours, ref Minutes, ':');

            if (Minutes <= 59||(Minutes==0&&Hours==0))
            {
                updateButton.IsEnabled = true;
            }
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
     
      
     
       
       
        private void Add_Stations_Button_Click(object sender, RoutedEventArgs e)
        {
            AddStationToLine addStationToLine = new AddStationToLine(Line);
            addStationToLine.ShowDialog();
            addStationToLine.Closed += AddStationToLine_Closed;
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                splitStringTOTwoInts(freq.Text, ref Hours, ref Minutes, ':');
               // bl.UpdateBusLine(first_bus.Value.Value, last_bus.Value.Value, new TimeSpan(Hours, Minutes, 0), Line.BusID, int.Parse(bus_line_numberTextBox.Text));
                System.Windows.MessageBoxResult mb = MessageBox.Show("Bus updated successfully");
            }
            catch (FrequencyConflictException ex) 
            {
                System.Windows.MessageBoxResult mb = MessageBox.Show("Cannot update, frequency is not valid");
            }
        }
        private void AddStationToLine_Closed(object sender, EventArgs e)
        {
            initialize();
        }

        private void busStationDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void remove_station_from_line_buttonClick(Object sender, RoutedEventArgs e)
        {
            string distance = null;
            
            MessageBoxResult result = MessageBox.Show("Are you sure you want to remove this station?", " Alert", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                    if (vis is DataGridRow)
                    {
                        var row = (DataGridRow)vis;
                        
                        try
                        {
                            distance = bl.RemoveBusStationFromLine((row.DataContext as StationOnTheLine).Code, Line.BusID);
                        }
                        catch (StationDoesNotExistOnTheLinexception ex)
                        {
                            MessageBoxResult msgBox2 = MessageBox.Show(ex.Message, " Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        if (distance != null)
                        {
                            List<string> distances = new List<string>();
                            distances.Add(distance);
                            AddDistances addDistances = new AddDistances(distances);
                            addDistances.ShowDialog();
                        }
                        break;
                    }

            }

          

        }

        private void stationOnTheLineDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
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
//private void first_bus_min_tb_TextChanged(object sender, TextChangedEventArgs e)
//{

//}

//private void frequency_hr_tb_TextChanged(object sender, TextChangedEventArgs e)
//{

//}