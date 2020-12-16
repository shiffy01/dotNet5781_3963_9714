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

namespace dotNet5781_03B_3963_9714
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Bus> buses = new ObservableCollection<Bus>();
        BackgroundWorker worker;//displays seconds untill the bus can drive again
        BackgroundWorker worker2;//displays progress bar for progress of a drive/tune up/refill
        Bus user;//this is used to point to the bus currently in use in the program
       
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
            buses[0].CanDrive = false;
            //make one bus at least be close to the max milage allowed:
            buses[1].TotalMilage = 19990;
            //make one bus with very little gas:
            buses[3].Gas = 10;
        }

        public bool DoesExist(int license)//checks if a licensse plate exists in the collection already
        {
            bool exists = false;
            for (int i = 0; i < buses.Count; i++)
                if (buses[i].License == license)
                    exists = true;
            return exists;
        }
        public MainWindow()
        {
            InitializeComponent();
            Initialize_bus_collection();
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker2 = new BackgroundWorker();
            worker2.DoWork += Worker2_DoWork;
            worker2.ProgressChanged += Worker2_ProgressChanged;
            worker2.RunWorkerCompleted += Worker2_RunWorkerCompleted;
            worker2.WorkerReportsProgress = true;
            worker2.WorkerSupportsCancellation = true;
            busDataGrid.DataContext = buses;


        }
        private void Worker_DoWork(object sender,DoWorkEventArgs e)
        {
          
            Bus bb = (Bus)e.Argument;
            
            for(int i=0; i<bb.Time; i++)
            {
                
                System.Threading.Thread.Sleep(1000);//one second
                worker.ReportProgress((bb.Time - i) * 10);//sends the number of seconds untill the bus is ready to drive again
            }
        }
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int time = e.ProgressPercentage;
            string mes = time + " minutes till the bus can drive";
            user.Seconds = mes;   
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            user.ButtonVisibility = false;
            if (user.Status == Bus.Status_ops.At_mechanic || user.Status == Bus.Status_ops.Filling_up)
            { 
                user.Status = Bus.Status_ops.Ready;
                user.CanDrive = true;
            }
           
                if (user.Status == Bus.Status_ops.On_the_road)//the bus just came back from a drive
                {
                    if (user.Milage < 20000 && !((user.Last_tune_up-DateTime.Now).Days>356)&& user.Gas == 1200)//the bus does not need gas or a tune up
                        user.Status = Bus.Status_ops.Ready;
                    if (user.Milage >= 20000|| (user.Last_tune_up - DateTime.Now).Days > 356)//the bus needs a tune up
                        {
                        if (user.Gas < 1200)//if the bus needs gas too
                            user.Refill();
                        user.Status = Bus.Status_ops.At_mechanic;                          
                        MessageBox.Show("Bus being sent for a tune up...", " ", MessageBoxButton.OK, MessageBoxImage.Information);
                        user.Tune_up();
                        user.Refill();
                        user.Time = 144;//one day in real time
                        user.ButtonVisibility = true;
                        worker.RunWorkerAsync(user);
                        worker2.RunWorkerAsync(user);
                    }                           
                        if (user.Gas < 1200)
                        {
                            user.Status = Bus.Status_ops.Filling_up;
                            MessageBox.Show("Gas being filled...", " ", MessageBoxButton.OK, MessageBoxImage.Information);
                            user.Refill();
                            user.Time = 12;//2 hours in real time
                            user.ButtonVisibility = true;
                            worker.RunWorkerAsync(user);
                            worker2.RunWorkerAsync(user);
                        }
                }
            
        }
        private void Worker2_DoWork(object sender, DoWorkEventArgs e)
        {
            user = (Bus)e.Argument;
            int percent = 0;//percent of time that the bus cannot drive that passed
            for (int i = 0; i < user.Time; i++)
            {
                percent += i*(100/user.Time);
                System.Threading.Thread.Sleep(1000);//one second
                worker2.ReportProgress(percent);
            }
        }
        private void Worker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            user.Progressb= e.ProgressPercentage;
        }
        private void Worker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            user.ButtonVisibility = false;
        }
        private void licenseButton(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            Bus b1= (button.DataContext as Bus);
            BusDetails bd = new BusDetails(b1);
            bd.Closed += BusDetails_Closed;
            bd.ShowDialog();
        }
        private void BusDetails_Closed(object sender, EventArgs e)
        {
            if((sender as BusDetails).Fill)//user pressed refill button
            {     
                Bus b1 = (sender as BusDetails).thisBus;
                if (user.Gas == 1200)
                    MessageBox.Show("Gas tank was already full", " ", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                {
                    user.Status = Bus.Status_ops.Filling_up;
                    MessageBox.Show("Gas tank being refilled", " ", MessageBoxButton.OK, MessageBoxImage.Information);
                    b1.Refill();
                    b1.Time = 12;//2 hours in real time
                    b1.ButtonVisibility = true;
                    b1.CanDrive = false;
                    BackgroundWorker gas = new BackgroundWorker();
                    gas.DoWork += Worker_DoWork;
                    gas.ProgressChanged += Worker_ProgressChanged;
                    gas.RunWorkerCompleted += Worker_RunWorkerCompleted;
                    gas.WorkerReportsProgress = true;
                    gas.WorkerSupportsCancellation = true;
                    BackgroundWorker gas2 = new BackgroundWorker();
                    gas2.DoWork += Worker2_DoWork;
                    gas2.ProgressChanged += Worker2_ProgressChanged;
                    gas2.RunWorkerCompleted += Worker2_RunWorkerCompleted;
                    gas2.WorkerReportsProgress = true;
                    gas2.WorkerSupportsCancellation = true;
                    gas.RunWorkerAsync(b1);
                    gas2.RunWorkerAsync(b1);
                }
            }
            if ((sender as BusDetails).Tune)//user pressed tune up button
            {
                Bus b1 = (sender as BusDetails).thisBus;
                MessageBox.Show("Bus being sent for a tune up...", " ", MessageBoxButton.OK, MessageBoxImage.Information);
                b1.Tune_up();
                b1.CanDrive = false;
                b1.Status = Bus.Status_ops.At_mechanic;
                b1.Time = 144;//one day in real time
                b1.ButtonVisibility = true;
                BackgroundWorker tune = new BackgroundWorker();
                tune.DoWork += Worker_DoWork;
                tune.ProgressChanged += Worker_ProgressChanged;
                tune.RunWorkerCompleted += Worker_RunWorkerCompleted;
                tune.WorkerReportsProgress = true;
                tune.WorkerSupportsCancellation = true;
                BackgroundWorker tune2 = new BackgroundWorker();
                tune2.DoWork += Worker2_DoWork;
                tune2.ProgressChanged += Worker2_ProgressChanged;
                tune2.RunWorkerCompleted += Worker2_RunWorkerCompleted;
                tune2.WorkerReportsProgress = true;
                tune2.WorkerSupportsCancellation = true;
                tune.RunWorkerAsync(b1);
                tune2.RunWorkerAsync(b1);
            }
        }
        private void Button_Click_gas(object sender, RoutedEventArgs e)
        {
            bool filled = true;//asume the bus needs a refill
            var button = sender as Button;
            Bus b1 = (button.DataContext as Bus);
            if (b1.Gas == 1200)
                filled = false;//the bus does not need a refill
            if (filled)
            {
                if (b1.CanDrive == true)
                    b1.CanDrive = false;//the bus will not be in use while refilling
                MessageBox.Show("Gas tank being refilled...", " ", MessageBoxButton.OK, MessageBoxImage.Information);
                b1.Refill();
                b1.Time = 12;//12 seconds= 2 hours real time
                b1.ButtonVisibility = true;
                BackgroundWorker gas = new BackgroundWorker();
                gas.DoWork += Worker_DoWork;
                gas.ProgressChanged += Worker_ProgressChanged;
                gas.RunWorkerCompleted += Worker_RunWorkerCompleted;
                gas.WorkerReportsProgress = true;
                gas.WorkerSupportsCancellation = true;
                BackgroundWorker gas2 = new BackgroundWorker();
                gas2.DoWork += Worker2_DoWork;
                gas2.ProgressChanged += Worker2_ProgressChanged;
                gas2.RunWorkerCompleted += Worker2_RunWorkerCompleted;
                gas2.WorkerReportsProgress = true;
                gas2.WorkerSupportsCancellation = true;
                gas.RunWorkerAsync(b1);
                gas2.RunWorkerAsync(b1);
            }
            else
                MessageBox.Show("Gas tank was already full", " ", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void Button_Click_add(object sender, RoutedEventArgs e)
        {
            AddBus add = new AddBus();
            add.Closed += AddBusWindow_Closed;
            add.Show();

        }
        private void AddBusWindow_Closed(object sender, EventArgs e)
        {
            user = (sender as AddBus).CurrentBus;
            if (DoesExist(user.License))//if this bus is already in the collection
                (sender as AddBus).AddIt = false;
            if ((sender as AddBus).AddIt)//no input errors
                buses.Add(user);   
        }
        private void Button_Click_Drive(object sender, RoutedEventArgs e)
        {
            Bus b1 = (busDataGrid.SelectedItem as Bus);
            DriveBus drive = new DriveBus(b1);
            drive.Closed += DriveBusWindow_Closed;
            drive.ShowDialog();

        }

        private void DriveBusWindow_Closed(object sender, EventArgs e)
        {
            if ((sender as DriveBus).driven)//bus sent to be driven with no errors
            {
                Random rand = new Random(DateTime.Now.Millisecond);
                int kamash = rand.Next(20, 51);//random speed between 20 and 50 km per hour
                Bus b1 = (sender as DriveBus).CurrentBus;
                b1.Status = Bus.Status_ops.On_the_road;
                b1.Time=((((sender as DriveBus).curr_milage)/kamash)*6);  //זמן*מהירות=דרך
                b1.Status = Bus.Status_ops.On_the_road;
                b1.CanDrive = false;
                b1.ButtonVisibility = true;
                BackgroundWorker drive = new BackgroundWorker();
                drive.DoWork += Worker_DoWork;
                drive.ProgressChanged += Worker_ProgressChanged;
                drive.RunWorkerCompleted += Worker_RunWorkerCompleted;
                drive.WorkerReportsProgress = true;
                drive.WorkerSupportsCancellation = true;
                BackgroundWorker drive2 = new BackgroundWorker();
                drive2.DoWork += Worker2_DoWork;
                drive2.ProgressChanged += Worker2_ProgressChanged;
                drive2.RunWorkerCompleted += Worker2_RunWorkerCompleted;
                drive2.WorkerReportsProgress = true;
                drive2.WorkerSupportsCancellation = true;
                drive.RunWorkerAsync(b1);
                drive2.RunWorkerAsync(b1);
            }
        }
    }
}

