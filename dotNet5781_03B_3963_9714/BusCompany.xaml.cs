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
    /// Interaction logic for BusCompany.xaml
    /// </summary>
    public partial class BusCompany : Window
    {
        ObservableCollection<Bus> buses = new ObservableCollection<Bus>();
        BackgroundWorker worker;//displays seconds untill the bus can drive again
        BackgroundWorker worker2;//displays progress bar for progress of a drive/tune up/refill
        
       
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
        public BusCompany()
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
          
            Bus b = (Bus)e.Argument;
            
            for(int i=0; i<b.Time; i++)
            {
                
                System.Threading.Thread.Sleep(1000);//one second
                
                (sender as BackgroundWorker).ReportProgress((b.Time - i) * 10, b);//sends the number of seconds untill the bus is ready to drive again
            }
            e.Result = b;
        }
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Bus b = (e.UserState as Bus);
            int time = e.ProgressPercentage;
            string mes = ""; 
            if (time >= 60)
            {
                mes += (time / 60) + " hour";
                if (time / 60 > 1)
                    mes += "s";
                if (time % 60 != 0)
                    mes += " and " + time % 60 + " minutes";
               
            }
            else
                mes += time + " minutes";
            mes += " untill the bus can drive";
            b.Seconds = mes;   
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Bus bus=(e.Result as Bus);
            bus.ButtonVisibility = false;
            bus.Seconds = "Ready";
            bus.Progressb = 0;
            if (bus.Status == Bus.Status_ops.At_mechanic || bus.Status == Bus.Status_ops.Filling_up)
            { 
                bus.Status = Bus.Status_ops.Ready;
                bus.CanDrive = true;
                bus.CanGas = true;
                bus.CanTuneUp = true;
            }
           //the bus will be refilled automaticly if there is less than 40 in the gas tank and will be tuned up if less than 2000 km left to drive (or passed the date)
                if (bus.Status == Bus.Status_ops.On_the_road)//the bus just came back from a drive
                {
                if (bus.Milage < 18000 && !((DateTime.Now - bus.Last_tune_up).Days > 356) && bus.Gas >= 40)//the bus does not need gas or a tune up
                {
                    bus.Status = Bus.Status_ops.Ready;
                    bus.CanDrive = true;
                    bus.CanGas = true;
                    bus.CanTuneUp = true;
                }
                    if (bus.Milage > 18000 || (bus.Last_tune_up - DateTime.Now).Days > 356)//the bus needs a tune up
                     {
                        if (bus.Gas < 40)//if the bus needs gas too
                            bus.Refill();
                        Tuneup(bus);
                    }                           
                    if (bus.Gas < 40)
                    {
                         Refill(bus);
                    }
                }
            
        }
        private void Worker2_DoWork(object sender, DoWorkEventArgs e)
        {
            Bus bus= (Bus)e.Argument;
            double percent = 0;//percent of time that the bus cannot drive that passed
            for (int i = 0; i < bus.Time; i++)
            {
                percent = i*(100/(double)bus.Time);
                System.Threading.Thread.Sleep(1000);//one second
               
                (sender as BackgroundWorker).ReportProgress((int)percent, bus);
            }
            e.Result = bus;
        }
        private void Worker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Bus b = (e.UserState as Bus);
            b.Progressb= e.ProgressPercentage;
        }
        private void Worker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Bus bus = (e.Result as Bus);
            bus.ButtonVisibility = false;
            bus.Seconds = "Ready";
            bus.Progressb = 0;
        }
        private void Refill(Bus b1)
        {
            b1.Status = Bus.Status_ops.Filling_up;
            if (b1.Gas == 1200)
                MessageBox.Show("Gas tank was already full", " ", MessageBoxButton.OK, MessageBoxImage.Information);//the bus does not need a refill
            else
            {
                
                b1.CanDrive = false;//the bus will not be in use while refilling
                b1.CanGas = false;
                b1.CanTuneUp = false;
                MessageBox.Show("Gas tank being refilled...", " ", MessageBoxButton.OK, MessageBoxImage.Information);
                b1.Refill();
                b1.Status = Bus.Status_ops.Filling_up;
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
        }
        private void Tuneup(Bus b1)
        {
            MessageBox.Show("Bus being sent for a tune up...", " ", MessageBoxButton.OK, MessageBoxImage.Information);
            b1.Tune_up();
            b1.CanDrive = false;
            b1.CanGas = false;
            b1.CanTuneUp = false;
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
        private void Drive(Bus b1, int distance)
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            int kamash = rand.Next(20, 51);//random speed between 20 and 50 km per hour 
            b1.Status = Bus.Status_ops.On_the_road;
            b1.Time = ((distance *6) /kamash);  //זמן*מהירות=דרך
            b1.CanDrive = false;
            b1.CanGas = false;
            b1.CanTuneUp = false;
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
                Refill(b1);
            }
            if ((sender as BusDetails).Tune)//user pressed tune up button
            {
                Bus b1 = (sender as BusDetails).thisBus;
                Tuneup(b1);
            }
        }
        private void Button_Click_gas(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            Bus b1 = (button.DataContext as Bus);
            Refill(b1);
        }
        private void Button_Click_add(object sender, RoutedEventArgs e)
        {
            AddBus add = new AddBus();
            add.Closed += AddBusWindow_Closed;
            add.Show();

        }
        private void AddBusWindow_Closed(object sender, EventArgs e)
        {
            Bus b1 = (sender as AddBus).CurrentBus;
            if (DoesExist(b1.License))//if this bus is already in the collection
                (sender as AddBus).AddIt = false;
            if ((sender as AddBus).AddIt)//no input errors
                buses.Add(b1);   
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
                Bus b1 = (sender as DriveBus).CurrentBus;
                Drive(b1, (sender as DriveBus).curr_milage);     
            }
        }
    }
}

