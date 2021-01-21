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
using System.Collections.ObjectModel;
using BO;
using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for AddLine.xaml
    /// </summary>
    public partial class AddLine : Window
    {
        static IBL bl;
        ObservableCollection<BusStation> all_stations;
        List<int> stationsToAdd;
        
        void initialize()
        {
            bl = BlFactory.GetBl();
            IEnumerable<BusStation> stationIenumerable = bl.GetAllBusStations();
            all_stations = new ObservableCollection<BusStation>(stationIenumerable);
            stationsToAdd = new List<int>();
        }
        public AddLine()
        {
            InitializeComponent();
            initialize();
            busStationDataGrid.DataContext = all_stations;
        }

        private bool canUpdate()
        {   
            if (string.IsNullOrWhiteSpace(line_number.Text))
                return false;
            if (stationsToAdd.Count < 2)
                return false;
            return true;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource busStationViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busStationViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // busStationViewSource.Source = [generic data source]
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
        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            if (canUpdate())
                add_button.IsEnabled = true;
            else
                add_button.IsEnabled = false;
        }
        private void got_checked(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    stationsToAdd.Add((row.DataContext as BusStation).Code);
                    if (canUpdate())
                        add_button.IsEnabled = true;
                    break;
                }
        }
        private void got_unchecked(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    stationsToAdd.Remove((row.DataContext as BusStation).Code);
                    if (!canUpdate())
                        add_button.IsEnabled = false;
                    break;
                }
        }
        //private void lost_focus_minutes(object sender, RoutedEventArgs e)
        //{
        //    TextBox text = sender as TextBox;
        //    if (int.Parse(text.Text) > 59)
        //        ; //change border to red
        //}
        //private void lost_focus_hours(object sender, RoutedEventArgs e)
        //{
        //    TextBox text = sender as TextBox;
        //    if (int.Parse(text.Text) > 23)
        //        ; //change border to red
        //}
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<string> needed_distances=null;
            try
            {
                TimeSpan frequency = new TimeSpan(freq.Value.Value.Hour, freq.Value.Value.Minute, 0);
                needed_distances=bl.AddBusLine(int.Parse(line_number.Text), stationsToAdd, first_bus.Value.Value, last_bus.Value.Value, frequency);
                MessageBoxResult mb = MessageBox.Show("The bus was added to the system");
                if (needed_distances == null)
                    this.Close();
                else
                {
                    AddDistances addDistances = new AddDistances(needed_distances);
                    addDistances.Show();
                    this.Close();
                }
            }
            catch (FrequencyConflictException ex)
            {
                System.Windows.MessageBoxResult mb = MessageBox.Show(ex.Message);
              
            }
            catch (BusLineAlreadyExistsException ex)
            {
                System.Windows.MessageBoxResult mb = MessageBox.Show(ex.Message);
            
            }
            catch (Exception)
            {
                System.Windows.MessageBoxResult mb = MessageBox.Show("Something has gone wrong. For an unknown reason, this busline cannot be added to the system. We regret the error");
                this.Close();
            }
           
           
        }

        private void busStationDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
