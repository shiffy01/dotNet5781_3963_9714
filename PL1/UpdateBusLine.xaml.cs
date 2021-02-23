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
    /// Interaction logic for UpdateBusLine.xaml
    /// </summary>
    public partial class UpdateBusLine : Page
    {
        List<BO.BusLineTime> LineTimes = new List<BO.BusLineTime>();
        static IBL bl;
        BO.User User;
        BO.BusLine line;
        List<BO.BusStation> stationsToAdd = new List<BO.BusStation>();
        int Index = 0;
        int Code1;
        int Code2;
        List<string> distances=new List<string>();
        void initialize()
        {
            foreach (var item in line.Times)
            {
                LineTimes.Add(item);
            }
            timesText.Text = bl.printTimes(LineTimes);
            busStationDataGrid.DataContext = bl.GetAllBusStations();
            foreach (var item in line.Stations)
            {
                stationsToAdd.Add(bl.GetBusStation(item.Code));
            }
            stationOnTheLineDataGrid.DataContext = line.Stations;
            Line_tb.Text = line.Bus_line_number.ToString();
            //Index = 0;
        }
        public UpdateBusLine(IBL bl1, BO.User user, int lineID)
        {
            InitializeComponent();
            bl = bl1;
            line = bl.GetBusLine(lineID);
            User = user;
            initialize();
        }
        private void TextBox_OnlyNumbers_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;

            //allow get out of the text box
            if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
                return;

            //allow list of system keys (add other key here if you want to allow)
            if (e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||
                  e.Key == Key.Home
             || e.Key == Key.End || e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right)
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
        private bool IsAllFilled()
        {
            if (string.IsNullOrEmpty(Line_tb.Text))
                return false;
            if (LineTimes.Count() == 0)
                return false;
            if (stationsToAdd.Count() < 2)
                return false;

            return true;
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

        private void createDialogeContent()
        {
            if (Index < distances.Count())
            {
                splitStringTOTwoInts(distances[Index], ref Code1, ref Code2, '*');
                Index++;
                ids.Text = Code1 + "and" + Code2;
            }
            else
            {
                BusLinesDispaly busLineDisplay = new BusLinesDispaly(bl, User, true);
                NavigationService.Navigate(busLineDisplay);
            }


        }
        private void saveClick(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.UpdateBusLine(line.BusID, int.Parse(Line_tb.Text), LineTimes);
                foreach (var item in line.Stations)
                {
                    bl.RemoveBusStationFromLine(item.Code, line.BusID);
                }
                int i = 0;
                foreach (var item in stationsToAdd)
                {
                    List<string> d=bl.AddStationToBusLine(line.BusID, item.Code, i);
                    if (d != null && d.Count > 0)
                    {
                        foreach (var dis in d)
                        {
                            distances.Add(dis);
                        }
                    }
                        i++;
                }
                if (distances.Count != 0)
                {
                    createDialogeContent();
                    distance.IsOpen = true;
                }
                else
                {
                    BusLinesDispaly display = new BusLinesDispaly(bl, User, true);
                    NavigationService.Navigate(display);
                }

            }
            catch (BO.BusLineNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
                BusLinesDispaly display = new BusLinesDispaly(bl, User, true);
                NavigationService.Navigate(display);
            }
            catch (BO.StationDoesNotExistOnTheLinexception ex)
            {
                MessageBox.Show(ex.Message);
                BusLinesDispaly display = new BusLinesDispaly(bl, User, true);
                NavigationService.Navigate(display);
            }
            catch (BO.StationNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
                BusLinesDispaly display = new BusLinesDispaly(bl, User, true);
                NavigationService.Navigate(display);
            }
            catch (BO.InvalidPlaceException ex)
            {
                MessageBox.Show(ex.Message);
                BusLinesDispaly display = new BusLinesDispaly(bl, User, true);
                NavigationService.Navigate(display);
            }
        }
        private void removeStation(object sender, RoutedEventArgs e)
        {
            object ID = ((CheckBox)sender).CommandParameter;
            List<BO.BusStation> list = new List<BO.BusStation>();
            foreach (var item in stationsToAdd)
            {
                if (item.Code != (int)ID)
                    list.Add(item);
            }
            stationsToAdd = list;
            var stat = from s in stationsToAdd
                       select s;
            stationOnTheLineDataGrid.DataContext = stat;
            if (IsAllFilled())
                saveButton.IsEnabled = true;
            else
                saveButton.IsEnabled = false;
        }
        private void addStation(object sender, RoutedEventArgs e)
        {
            (sender as CheckBox).IsChecked = false;
            object ID = ((CheckBox)sender).CommandParameter;
            stationsToAdd.Add(bl.GetBusStation((int)ID));
            var stat = from s in stationsToAdd
                       select s;
            stationOnTheLineDataGrid.DataContext = stat;
            if (IsAllFilled())
                saveButton.IsEnabled = true;
            else
                saveButton.IsEnabled = false;

        }     
        private void AddTime(object sender, RoutedEventArgs e)
        {
            addTimeDialog.IsOpen = true;
        }
        private void accept(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(distance_tb.Text) || !(DriveTimePicker.SelectedTime.HasValue))
                return;


            bl.AddAdjacentStations(Code1, Code2, double.Parse(distance_tb.Text), new TimeSpan(DriveTimePicker.SelectedTime.Value.Hour, DriveTimePicker.SelectedTime.Value.Minute, 0));
            createDialogeContent();
        }
        private void cancel(object sender, RoutedEventArgs e)
        {

            MessageBoxResult result = System.Windows.MessageBox.Show(@"             Are you sure you want to cancel?
                                             By canceling, the remaining distances will be set to random values.", " Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {

                for (int i = Index - 1; i < distances.Count; i++)
                {
                    splitStringTOTwoInts(distances[i], ref Code1, ref Code2, '*');
                    try
                    {
                        bl.AddAdjacentStations(Code1, Code2, 200, new TimeSpan(00, 12, 00));
                    }
                    catch (BO.PairAlreadyExistsException ex)
                    {
                        MessageBoxResult result2 = System.Windows.MessageBox.Show(ex.Message, " Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                BusLinesDispaly busLineDisplay = new BusLinesDispaly(bl, User, true);
                NavigationService.Navigate(busLineDisplay);
            }



        }
        private void refreshClick(object sender, RoutedEventArgs e)
        {
            timesText.Text = "";
            LineTimes = new List<BO.BusLineTime>();
            saveButton.IsEnabled = false;
        }
        private void AddTimeClick(object sender, RoutedEventArgs e)
        {
            if (!(startTimePicker.SelectedTime.HasValue && endTimePicker.SelectedTime.HasValue && freqTimePicker.SelectedTime.HasValue))
                return;
            LineTimes.Add(new BO.BusLineTime {
                Start = new DateTime(2000, 01, 01, startTimePicker.SelectedTime.Value.Hour, startTimePicker.SelectedTime.Value.Minute, 0),
                End = new DateTime(2000, 01, 01, endTimePicker.SelectedTime.Value.Hour, endTimePicker.SelectedTime.Value.Minute, 0),
                Frequency = new TimeSpan(freqTimePicker.SelectedTime.Value.Hour, freqTimePicker.SelectedTime.Value.Minute, 0)
            });
            addTimeDialog.IsOpen = false;
            timesText.Text = bl.printTimes(LineTimes);
            if (IsAllFilled())
                saveButton.IsEnabled = true;
            else
                saveButton.IsEnabled = false;
        }
        private void CancelTimeClick(object sender, RoutedEventArgs e)
        {
            addTimeDialog.IsOpen = false;
        }
        private void lineNumberTextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsAllFilled())
                saveButton.IsEnabled = true;
            else
                saveButton.IsEnabled = false;
        }
    }
}

