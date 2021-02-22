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
using System.Collections.ObjectModel;
using BlApi;

namespace PL1
{
    /// <summary>
    /// Interaction logic for AddBusLine.xaml
    /// </summary>
    public partial class AddBusLine : Page
    {
        List<BO.BusLineTime> LineTimes=new List<BO.BusLineTime>();
        static IBL bl;
        BO.User User;
        List<BO.BusStation> stationsToAdd=new List<BO.BusStation>();
        int Index=0;
        int Code1;
        int Code2;
        List<string> distances;
        void initialize()
        {
            timesText.Text = bl.printTimes(LineTimes);
            busStationDataGrid.DataContext = bl.GetAllBusStations();
            stationOnTheLineDataGrid.DataContext = stationsToAdd;
            Index = 0;
           
        }
        public AddBusLine(IBL bl1, BO.User user)
        {
            bl = bl1;
            InitializeComponent();
            initialize();
            
            
            User = user;
           
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
        //private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        //{

            //if (Line_tb.)
            //{
            //    if (!correctTimeFormat())
            //        return;
            //    try
            //    {
            //        bl.AddAdjacentStations(Code1, Code2, double.Parse(distanceMTB.Text), new TimeSpan(Hours, Minutes, 00));
            //    }
            //    catch (BO.PairAlreadyExistsException ex)
            //    {
            //        MessageBoxResult result = System.Windows.MessageBox.Show(ex.Message, " Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //    }
            //    Index++;
            //    if (Index == PairIds.Count)
            //    {
            //        this.DialogResult = true;
            //        return;

                //    }
                //    createDialogeContent();


                //}
                //else
                //{

                //    //throw trigger to change text to red
                //    errorLabel.Visibility = Visibility.Visible;
                //    return;
                //}

        
        private void addLine_Click(object sender, RoutedEventArgs e)
        {
            
                List<string> needed_distances = null;
                List<int> stationNumbers = new List<int>();
                foreach (var item in stationsToAdd)
                {
                    stationNumbers.Add(item.Code);
                }
                try
                {
                    needed_distances = bl.AddBusLine(int.Parse(Line_tb.Text), stationNumbers, (LineTimes).ToList());
                    MessageBoxResult mb = MessageBox.Show("The bus was added to the system");
                    if (needed_distances == null || needed_distances.Count == 0)
                    {
                        BusLinesDispaly busLinesDispaly = new BusLinesDispaly(bl, User, true);
                        NavigationService.Navigate(busLinesDispaly);
                    }

                    else
                    {
                        distances = needed_distances;
                        distance.IsOpen = true;
                        createDialogeContent();
                       

                    }
                }
                catch (BO.FrequencyConflictException ex)
                {
                    System.Windows.MessageBoxResult mb = MessageBox.Show(ex.Message);

                }

                catch (BO.BusLineAlreadyExistsException ex)
                {
                    System.Windows.MessageBoxResult mb = MessageBox.Show(ex.Message);
                }
            
        } 
        private void addStation(object sender, RoutedEventArgs e)
        {
            //DataGridRow row = sender as DataGridRow;
            object ID = ((CheckBox)sender).CommandParameter;
            stationsToAdd.Add(bl.GetBusStation((int)ID));
            var stat = from s in stationsToAdd
                       select s;
            stationOnTheLineDataGrid.DataContext = stat;
            if (IsAllFilled())
                addLineButton.IsEnabled = true;
            else
                addLineButton.IsEnabled = false;

        }
        private void removeStation(object sender, RoutedEventArgs e)
        {
            object ID = ((CheckBox)sender).CommandParameter;
            List<BO.BusStation> list= new List<BO.BusStation>();
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
                addLineButton.IsEnabled = true;
            else
                addLineButton.IsEnabled = false;
        }
        private void AddTime(object sender, RoutedEventArgs e)
        {
            addTimeDialog.IsOpen = true;
        }       
        private void accept(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(distance_tb.Text)|| !(DriveTimePicker.SelectedTime.HasValue))
                return;   
                

                bl.AddAdjacentStations(Code1, Code2, double.Parse(distance_tb.Text), new TimeSpan(DriveTimePicker.SelectedTime.Value.Hour, DriveTimePicker.SelectedTime.Value.Minute,0));
            createDialogeContent();
        }
        private void cancel(object sender, RoutedEventArgs e)
        {

            MessageBoxResult result = System.Windows.MessageBox.Show(@"             Are you sure you want to cancel?
                                             By canceling, the remaining distances will be set to random values.", " Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {

                for (int i = Index-1; i < distances.Count; i++)
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
            addLineButton.IsEnabled = false;
        }
        private void AddTimeClick(object sender, RoutedEventArgs e)
        {
            if (!(startTimePicker.SelectedTime.HasValue && endTimePicker.SelectedTime.HasValue && freqTimePicker.SelectedTime.HasValue))
                return;
            LineTimes.Add(new BO.BusLineTime {
                Start = new DateTime(2000, 01, 01, startTimePicker.SelectedTime.Value.Hour, startTimePicker.SelectedTime.Value.Minute, 0),
                End= new DateTime(2000, 01, 01, endTimePicker.SelectedTime.Value.Hour, endTimePicker.SelectedTime.Value.Minute, 0),
                Frequency= new TimeSpan(freqTimePicker.SelectedTime.Value.Hour, freqTimePicker.SelectedTime.Value.Minute, 0)
            }) ;
            addTimeDialog.IsOpen = false;
            timesText.Text = bl.printTimes(LineTimes);
            if (IsAllFilled())
                addLineButton.IsEnabled = true;
            else
                addLineButton.IsEnabled = false;
        }
        private void CancelTimeClick(object sender, RoutedEventArgs e)
        {
            addTimeDialog.IsOpen = false;
        }
        private void lineNumberTextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsAllFilled())
                addLineButton.IsEnabled = true;
            else
                addLineButton.IsEnabled = false;
        }


    }
}
        
        