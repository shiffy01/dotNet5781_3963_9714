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
using System.ComponentModel;
using System.Collections.ObjectModel;
using BlApi;

namespace PL1
{
    /// <summary>
    /// Interaction logic for StationTimes.xaml
    /// </summary>
    public partial class StationTimes : Page
    {
        IBL bl;
        bool stopped = false;
        // ObservableCollection<BO.LineTiming> lineTimes = new ObservableCollection<BO.LineTiming>();
        public StationTimes(IBL bl1)
        {
            InitializeComponent();
            bl = bl1;
            combo.ItemsSource = bl.GetAllBusStations();
        }
        private void comboChange(object sender, SelectionChangedEventArgs e)
        {
            start.IsEnabled = true;
        }
        private void startClick(object sender, RoutedEventArgs e)
        {
            stop.IsEnabled = true;
            start.IsEnabled = false;
            combo.IsEnabled = false;
            BackgroundWorker bgw = new BackgroundWorker();
            bgw.DoWork += Worker_DoWork;
            bgw.ProgressChanged += Worker_ProgressChanged;
            bgw.RunWorkerCompleted += Worker_RunWorkerCompleted;
            bgw.WorkerReportsProgress = true;
            bgw.WorkerSupportsCancellation = true;
            bgw.RunWorkerAsync((combo.SelectedItem as BO.BusStation).Code);
        }
        private void stopClick(object sender, RoutedEventArgs e)
        {
            stop.IsEnabled = false;
            stopped = true;
            combo.IsEnabled = true;
           
        }

        //private void splitStringTOTwoInts(string str, ref int num1, ref int num2, char splitHere)
        //{
        //    string[] codes = str.Split(splitHere);
        //    try
        //    {
        //        num1 = Int32.Parse(codes[0]);
        //    }
        //    catch (FormatException e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }
        //    try
        //    {
        //        num2 = Int32.Parse(codes[1]);
        //    }
        //    catch (FormatException e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }
        //}
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            int code = (int)e.Argument;
            int hour=8, min=0;
           
            //double percent = 0;//percent of time that the bus cannot drive that passed
            //for (int i = 0; i < 12; i++)
            //{
            //    percent = i * (100 / (double)12);
            //    System.Threading.Thread.Sleep(1000);//one second

            //    (sender as BackgroundWorker).ReportProgress((int)percent, bus);
            //}
            //e.Result = bus;
           
            while (!stopped)
            {
                System.Threading.Thread.Sleep(1000);//one second      
                min++;
                if (min > 59)
                {
                    min = 0;
                    hour++;
                }

                string time= hour + ":";
                if (min < 10)
                    time += "0";
                time += min;
                this.Dispatcher.Invoke(() => {
                    timer.Text = time;
                });
                TimeSpan tt = (new TimeSpan(hour, min, 0) - new TimeSpan(8, 0, 0));
                this.Dispatcher.Invoke(() => {
                    lineTimingDataGrid.DataContext = bl.getLineTimings(code, tt);
                });
                (sender as BackgroundWorker).ReportProgress(code);
               
                
            }
        }
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //int code = (e.UserState as int);
            //TimeSpan tt = (new TimeSpan(hour, min, 0) - new TimeSpan(8, 0, 0));
            //lineTimingDataGrid.DataContext = bl.getLineTimings(code, tt);
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //BO.Bus b = (e.UserState as BO.Bus);
            //try
            //{
            //    bl.UpdateBus(b.License, BO.Bus.Status_ops.Ready, b.Last_tune_up, b.kilometerage, b.Totalkilometerage, b.Gas);
            //    initialize();
            //}
            //catch (BO.BusNotFoundException ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            
        }
    }
}
