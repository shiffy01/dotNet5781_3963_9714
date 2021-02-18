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
using System.ComponentModel;
using BlApi;

namespace PL1
{
    /// <summary>
    /// Interaction logic for BusesDisplay.xaml
    /// </summary>
    public partial class BusesDisplay : Page
    {
        static IBL bl;
        BO.User User;
        

        public BusesDisplay(IBL bl1, BO.User user)
        {
            InitializeComponent();
            bl = bl1;
            User = user;
            
        }
       
        private void Worker2_DoWork(object sender, DoWorkEventArgs e)
        {
            BO.Bus bus = (BO.Bus)e.Argument;
            double percent = 0;//percent of time that the bus cannot drive that passed
            for (int i = 0; i < 12; i++)
            {
                percent = i * (100 / (double)12);
                System.Threading.Thread.Sleep(1000);//one second

                (sender as BackgroundWorker).ReportProgress((int)percent, bus);
            }
            e.Result = bus;
        }
        private void Worker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BO.Bus b = (e.UserState as BO.Bus);
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
            {
               // = e.ProgressPercentage;//?why wont it recognize value
            }), null);
        }
        private void Worker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BO.Bus bus = (e.Result as BO.Bus);
            progressbar.
        }
        private void DriveClick(object sender, RoutedEventArgs e)
        {
            drive
            BO.Bus b1 = (busDataGrid.SelectedItem as BO.Bus);
            try
            {
                bl.drive(b1, );
            }
            catch (BO.BusNotFoundException)
            {
                MessageBoxResult mb = MessageBox.Show("There was an problem loading the data. We regret the error");
                return;
            }
        }
        private void RefillClick(object sender, RoutedEventArgs e)
        {
            BO.Bus b1 = (busDataGrid.SelectedItem as BO.Bus);
            try
            {
                bl.refill(b1);
            }
            catch (BO.BusNotFoundException)
            {
                MessageBoxResult mb = MessageBox.Show("There was an problem loading the data. We regret the error");
                return;
            }

        }
        private void TuneUpClick(object sender, RoutedEventArgs e)
        {
            BO.Bus b1 = (busDataGrid.SelectedItem as BO.Bus);
            try
            {
                bl.tuneUp(b1);
            }
            catch (BO.BusNotFoundException)
            {
                MessageBoxResult mb = MessageBox.Show("There was an problem loading the data. We regret the error");
                return;
            }
        }

    }
}
