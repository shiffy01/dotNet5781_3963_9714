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
using dotNet5781_01_3963_9714;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;

//maybe in the xaml change selection to each cell figure out if that would help
namespace dotNet5781_03B_3963_9714
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Bus> buses = new ObservableCollection<Bus>();
        BackgroundWorker worker;
       
        void Initialize_bus_collection()
        {

            for (int i = 1; i < 11; i++)//add 10 buses to the list
            {

                switch (i % 3)
                {
                    case 0:
                        buses.Add(new Bus(12345008, DateTime.Now, i, 56, true, false, false));
                        break;
                    case 1:
                        buses.Add(new Bus(i * 1100002, DateTime.Now, i, 20, true, true, true));
                        break;
                    case 2:
                        buses.Add(new Bus(i * 1200003, DateTime.Now, i, 16, false, false, true));
                        break;
                };
            }
            //make one bus be passed the tune up date:
            DateTime date = new DateTime(2017, 01, 01);
            buses[0].Last_tune_up = date;
            //make one bus at least be close to the max milage allowed:
            buses[1].TotalMilage = 19990;
            //make one bus with very little gas:
            buses[3].Gas = 10;
        }


        public MainWindow()
        {
            InitializeComponent();
            Initialize_bus_collection();
            busDataGrid.DataContext = buses;
           // busDataGrid.IsReadOnly = true;
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool filled = true;

            Bus b1 = (busDataGrid.SelectedItem as Bus);
            if (b1.Gas == 1200)
                filled = false;
            
            if (filled)
            {
                MessageBox.Show("Gas tank was filled", " ", MessageBoxButton.OK, MessageBoxImage.Information);
                worker.RunWorkerAsync(12);
            }

           

        }

        private void Worker_DoWork(object sender,DoWorkEventArgs e)
        {
           
            int length = (int)e.Argument;
            for(int i=0; i<length; i++)
            {
                System.Threading.Thread.Sleep(1000);//one second
                worker.ReportProgress((length - i) * 10);//im not sure if i can give any number here or just percent. see if this works
            }
        }
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int time = e.ProgressPercentage;//check if this is only percent...
            Bus b1 = (busDataGrid.SelectedItem as Bus);
            string mes = time + "seconds till the bus can drive";
            b1.Seconds = mes;
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

           // c.Visibility = Visibility.Hidden;       
        }
       
        private void licenseButton(object sender, RoutedEventArgs e)
        {
            Bus b1 = (busDataGrid.SelectedItem as Bus);
            BusDetails bd = new BusDetails(b1);
            bd.ShowDialog();
        }
      
     
        private void Button_Click_add(object sender, RoutedEventArgs e)
        {
            AddBus add = new AddBus();
            add.Closed += AddBusWindow_Closed;
            add.Show();

        }
        private void AddBusWindow_Closed(object sender, EventArgs e)
        {
            Bus resultBus = (sender as AddBus).CurrentBus;
            if ((sender as AddBus).AddIt)
            {
                buses.Add(resultBus);
                //Button b = (Button)sender;
                //var s = b.Parent as Grid;
                //c = s.Children[1] as Button;
            }
          
        }
        private void Button_Click_Drive(object sender, RoutedEventArgs e)
        {
            Bus b1 = (busDataGrid.SelectedItem as Bus);
            DriveBus drive = new DriveBus(b1);
            drive.ShowDialog();

        }


        // private void busdatagrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    Bus b1 = (busDataGrid.SelectedItem as Bus);
        //    BusDetails bd = new BusDetails(b1);
        //    bd.ShowDialog();
        //}


    }
}

