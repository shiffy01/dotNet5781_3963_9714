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
    /// Interaction logic for AddStation.xaml
    /// </summary>
    public partial class AddStation : Page
    {
        static IBL bl;
        BO.User User;
        public AddStation(IBL bl1, BO.User user)
        {
            InitializeComponent();
            bl = bl1;
            User = user;
        }
        private void addClick(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AddBusStation(double.Parse(latitudeTextBox.Text), double.Parse(longitudeTextBox.Text), nameTextBox.Text, addressTextBox.Text);
            }
            catch (BO.StationALreadyExistsException ex)
            {
                MessageBoxResult mb = MessageBox.Show(ex.Message);
            }
           
                BusStationsDisplay busStationssDispaly = new BusStationsDisplay(bl, User, true);
                NavigationService.Navigate(busStationssDispaly);
        }
        private bool canUpdate()
        {
            double latresult;
            double longresult;          
            if (string.IsNullOrWhiteSpace(latitudeTextBox.Text) || !(double.TryParse(latitudeTextBox.Text, out latresult)))
                return false;
            if (string.IsNullOrWhiteSpace(longitudeTextBox.Text) || !(double.TryParse(longitudeTextBox.Text, out longresult)))
                return false;
            if (string.IsNullOrWhiteSpace(nameTextBox.Text))
                return false;
            if (string.IsNullOrWhiteSpace(addressTextBox.Text))
                return false;    
            return true;
        }
        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            if (canUpdate())
                add.IsEnabled = true;
            else
                add.IsEnabled = false;
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
            if (text.Name == "latitudeTextBox" || text.Name == "longitudeTextBox")
                if (e.Key == Key.OemPeriod)
                {
                    if (!(text.Text).Contains("."))
                        return;//if there is not already a decimal point in the textbox, let this key be written in the textbox

                }

            //forbid letters and signs (#,$, %, ...)
            e.Handled = true; //ignore this key. mark event as handled, will not be routed to other controls
            return;
        }
    }

}

