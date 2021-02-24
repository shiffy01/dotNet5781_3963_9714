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
    /// Interaction logic for RouteSearch.xaml
    /// </summary>
    public partial class RouteSearch : Page
    {
        static IBL bl;
        BO.User User;
        void initialize()
        {
            noResultsLabel.Visibility = Visibility.Hidden;
            busLineDataGrid.Visibility = Visibility.Hidden;
        }
        public RouteSearch(IBL bl1, BO.User user)
        {
            InitializeComponent();
            bl = bl1;
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
        private void textChanged(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(firstCodeBox.Text) || string.IsNullOrEmpty(secondCodeBox.Text))
                searchButton.IsEnabled = false;
            else
                searchButton.IsEnabled = true;
        }
        private void searchClick(object sender, RoutedEventArgs e)
        {
            initialize();
           
           
                var lines = bl.SearchRoute(int.Parse(firstCodeBox.Text), int.Parse(secondCodeBox.Text));
            if (lines.Count() == 0)
                noResultsLabel.Visibility = Visibility.Visible;
            else
            {
                busLineDataGrid.DataContext = lines;
                busLineDataGrid.Visibility = Visibility.Visible;
                //try
                //{
                    
                //}
            }
        }



    }
}
