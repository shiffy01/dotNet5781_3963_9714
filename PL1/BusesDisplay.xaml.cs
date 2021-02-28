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
        void initialize()
        {
            busDataGrid.DataContext = bl.GetAllBuses();
        }

        public BusesDisplay(IBL bl1, BO.User user)
        {
            InitializeComponent();
            bl = bl1;
            User = user;
            initialize();
        }
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            BusDetails line = new BusDetails(bl, (row.DataContext as BO.Bus));
            NavigationService.Navigate(line);

        }
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
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
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BO.Bus b = (e.UserState as BO.Bus);
            b.Percent = e.ProgressPercentage;
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BO.Bus b = (e.UserState as BO.Bus);
            b.Percent = 0;
            try
            {
                bl.UpdateBus(b.License, BO.Bus.Status_ops.Ready, b.Last_tune_up, b.kilometerage, b.Totalkilometerage, b.Gas);
                initialize();
            }
            catch (BO.BusNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DriveClick(object sender, RoutedEventArgs e)
        {
            BO.Bus b1 = (busDataGrid.SelectedItem as BO.Bus);
            if (b1.kilometerage+int.Parse(distance.Text) > 200000||b1.Gas< int.Parse(distance.Text))
                return;
            else
            {

                try
                {
                    bl.UpdateBus(b1.License, BO.Bus.Status_ops.Not_ready, b1.Last_tune_up, b1.kilometerage+ int.Parse(distance.Text), b1.Totalkilometerage+int.Parse(distance.Text), b1.Gas- int.Parse(distance.Text));
                }
                catch (BO.BusNotFoundException ex)
                {
                    MessageBox.Show(ex.Message);
                }

                BackgroundWorker gas = new BackgroundWorker();
                gas.DoWork += Worker_DoWork;
                gas.ProgressChanged += Worker_ProgressChanged;
                gas.RunWorkerCompleted += Worker_RunWorkerCompleted;
                gas.WorkerReportsProgress = true;
                gas.WorkerSupportsCancellation = true;

                gas.RunWorkerAsync(b1);
                dialog.IsOpen = false;

            }
        }
        private void RefillClick(object sender, RoutedEventArgs e)
        {
            
            BO.Bus b1 = (busDataGrid.SelectedItem as BO.Bus);
            if (b1.Status != BO.Bus.Status_ops.Ready)
                return;
            if (b1.Gas == 1200)
                MessageBox.Show("Gas tank was already full", " ", MessageBoxButton.OK, MessageBoxImage.Information);//the bus does not need a refill
            else
            {

                try
                {
                    bl.UpdateBus(b1.License, BO.Bus.Status_ops.Not_ready, b1.Last_tune_up, b1.kilometerage, b1.Totalkilometerage, 1200);
                }
                catch (BO.BusNotFoundException ex)
                {
                    MessageBox.Show(ex.Message);
                }
      
                BackgroundWorker gas = new BackgroundWorker();
                gas.DoWork += Worker_DoWork;
                gas.ProgressChanged += Worker_ProgressChanged;
                gas.RunWorkerCompleted += Worker_RunWorkerCompleted;
                gas.WorkerReportsProgress = true;
                gas.WorkerSupportsCancellation = true;
               
                gas.RunWorkerAsync(b1);
               
               
            }
            //BO.Bus b1 = (busDataGrid.SelectedItem as BO.Bus);
            //try
            //{
            //    bl.refill(b1);
            //}
            //catch (BO.BusNotFoundException)
            //{
            //    MessageBoxResult mb = MessageBox.Show("There was an problem loading the data. We regret the error");
            //    return;
            //}

        }
        private void TuneUpClick(object sender, RoutedEventArgs e)
        {
            BO.Bus b1 = (busDataGrid.SelectedItem as BO.Bus);
            if (b1.Status != BO.Bus.Status_ops.Ready)
                return;
            
              
                    try
                    {
                        bl.UpdateBus(b1.License, BO.Bus.Status_ops.Not_ready, DateTime.Now, 0, b1.Totalkilometerage, b1.Gas);
                    }
                    catch (BO.BusNotFoundException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    BackgroundWorker gas = new BackgroundWorker();
                    gas.DoWork += Worker_DoWork;
                    gas.ProgressChanged += Worker_ProgressChanged;
                    gas.RunWorkerCompleted += Worker_RunWorkerCompleted;
                    gas.WorkerReportsProgress = true;
                    gas.WorkerSupportsCancellation = true;

                    gas.RunWorkerAsync(b1);


                
        }
        private void driveButtonClick(object sender, RoutedEventArgs e)
        {
            dialog.IsOpen = true;
        }
        private void cancelclick(object sender, RoutedEventArgs e)
        {
            dialog.IsOpen = false;
        }
        private void deleteClick(object sender, RoutedEventArgs e)
        {
            BO.Bus b1 = (busDataGrid.SelectedItem as BO.Bus);
            try
            {
                bl.DeleteBus(b1.License);
            }
            catch (BO.BusNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
            initialize();
        }
        private void addClick(object sender, RoutedEventArgs e)
        {
            addDialog.IsOpen = true;
        }
        private void canceladdclick(object sender, RoutedEventArgs e)
        {
            addDialog.IsOpen = false;
        }
        private void addbusClick(object sender, RoutedEventArgs e)
        {
            addDialog.IsOpen = false;
            bl.AddBus(startText.SelectedDate.Value, int.Parse(tkTextBox.Text));
            initialize();
        }
    }
}
