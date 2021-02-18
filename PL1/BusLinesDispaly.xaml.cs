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
    /// Interaction logic for BusLinesDispaly.xaml
    /// </summary>
    public partial class BusLinesDispaly : Page
    {
        IBL bl;
        BO.User User;
        public BusLinesDispaly(IBL bl1, BO.User user, bool manage)
        {
            InitializeComponent();
            bl = bl1;
            User = user;
            if (!manage)
                addButton.Visibility = Visibility.Hidden;
        }
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            BusLineDetails line = new BusLineDetails(bl,  (row.DataContext as BO.BusLine));
            NavigationService.Navigate(line);

        }
        private void AddClick(object sender, RoutedEventArgs e)
        {
            AddBusLine add = new AddBusLine(bl, User);
            NavigationService.Navigate(add);
        }
        private void DeleteLineButton_Click(object sender, RoutedEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            try
            {
                bl.DeleteBusLine((row.DataContext as BO.BusLine).BusID);
            }
            catch (BO.BusLineNotFoundException ex)
            {
                System.Windows.MessageBoxResult mb = MessageBox.Show(ex.Message);
            }
        }
        private void UpdateLineButton_Click(object sender, RoutedEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            Updateline updateline = new Updateline(bl, User, (row.DataContext as BO.BusLine));
            NavigationService.Navigate(updateline);
        }

    }
}
