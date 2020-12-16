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
        BackgroundWorker worker2;
        Bus user;
       
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

        public bool DoesExist(int license)
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
        private void Button_Click_gas(object sender, RoutedEventArgs e)
        {
            bool filled = true;
            var button = sender as Button;
            var b1 = (button.DataContext as Bus);
            user = b1;
            if (b1.Gas == 1200)
                filled = false;
            user = b1;
            if (filled)
            {
                if (user.CanDrive == true)
                    user.CanDrive = false;
                MessageBox.Show("Gas tank was filled", " ", MessageBoxButton.OK, MessageBoxImage.Information);
                b1.Refill();
                b1.Time = 12;
                b1.ButtonVisibility = true;
              //  b1.Seconds = "at least";
               worker.RunWorkerAsync(b1);
                worker2.RunWorkerAsync(b1);
            }
        }

        private void Worker_DoWork(object sender,DoWorkEventArgs e)
        {
          
            Bus bb = (Bus)e.Argument;
            
            for(int i=0; i<bb.Time; i++)
            {
                
                System.Threading.Thread.Sleep(1000);//one second
                worker.ReportProgress((bb.Time - i) * 10);//im not sure if i can give any number here or just percent. see if this works
            }
        }
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int time = e.ProgressPercentage;//check if this is only percent...
           
            string mes = time + " minutes till the bus can drive";
            user.Seconds = mes;
            //OnPropertyChanged(user.Seconds);
            
            //     this.Invoke( {someLabel.Text = newText;
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            user.ButtonVisibility = false;
            if (user.Status == Bus.Status_ops.At_mechanic || user.Status == Bus.Status_ops.Filling_up)
            { 
                user.Status = Bus.Status_ops.Ready;
                user.CanDrive = true;
            }
            else
            {
                if (user.Status == Bus.Status_ops.On_the_road)
                {
                    if (user.Milage < 20000 && user.Gas == 1200)
                        user.Status = Bus.Status_ops.Ready;
                        if (user.Milage >= 20000 && user.Gas < 1200)
                    {
                        user.Status = Bus.Status_ops.At_mechanic;
                        user.Status = Bus.Status_ops.At_mechanic;
                        Bus b1 = (sender as BusDetails).thisBus;
                        bool tune = true;
                        if (b1.Milage == 0)
                            tune = false;
                        if (tune)
                        {
                            MessageBox.Show("Bus was tuned up", " ", MessageBoxButton.OK, MessageBoxImage.Information);
                            b1.Tune_up();
                            b1.Refill();
                            user = b1;
                            b1.Time = 144;
                            b1.ButtonVisibility = true;
                            //  b1.Seconds = "at least";
                            worker.RunWorkerAsync(b1);
                            worker2.RunWorkerAsync(b1);
                        }

                    }
                    else
                    {
                        if (user.Milage >= 20000)
                        {
                            user.Status = Bus.Status_ops.At_mechanic;
                            Bus b1 = (sender as BusDetails).thisBus;
                            bool tune = true;
                            if (b1.Milage == 0)
                                tune = false;
                            if (tune)
                            {
                                MessageBox.Show("Bus was tuned up", " ", MessageBoxButton.OK, MessageBoxImage.Information);
                                b1.Tune_up();
                                user = b1;
                                b1.Time = 144;
                                b1.ButtonVisibility = true;
                                //  b1.Seconds = "at least";
                                worker.RunWorkerAsync(b1);
                                worker2.RunWorkerAsync(b1);
                            }
                        }
                        if (user.Gas < 1200)
                        {
                            user.Status = Bus.Status_ops.Filling_up;
                            bool filled = true;
                            var button = sender as Button;
                            var b1 = (button.DataContext as Bus);
                            user = b1;
                            if (b1.Gas == 1200)
                                filled = false;

                            if (filled)
                            {
                                MessageBox.Show("Gas tank was filled", " ", MessageBoxButton.OK, MessageBoxImage.Information);
                                b1.Refill();
                                b1.Time = 12;
                                b1.ButtonVisibility = true;
                                user = b1;
                                worker.RunWorkerAsync(b1);
                                worker2.RunWorkerAsync(b1);
                            }
                        }
                    }
                }
            }
            
        }
        private void Worker2_DoWork(object sender, DoWorkEventArgs e)
        {

            Bus bb = (Bus)e.Argument;
            int percent = 0;
            for (int i = 0; i < bb.Time; i++)
            {
                percent += i;
                System.Threading.Thread.Sleep(1000);//one second
                worker2.ReportProgress(percent);//im not sure if i can give any number here or just percent. see if this works
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
            var b1 = (button.DataContext as Bus);
            user = b1;
            BusDetails bd = new BusDetails(b1);
            bd.Closed += BusDetails_Closed;
            bd.ShowDialog();
        }
        private void BusDetails_Closed(object sender, EventArgs e)
        {
            if((sender as BusDetails).Fill)
            {
                bool filled = true;

                Bus b1 = (sender as BusDetails).thisBus;
              
                if (b1.Gas == 1200)
                    filled = false;

                if (filled)
                {
                    b1.Status = Bus.Status_ops.Filling_up;
                    MessageBox.Show("Gas tank was filled", " ", MessageBoxButton.OK, MessageBoxImage.Information);
                    b1.Refill();
                    b1.Time = 12;
                    user = b1;
                    b1.ButtonVisibility = true;
                    user.CanDrive = false;
                    worker.RunWorkerAsync(b1);
                    worker2.RunWorkerAsync(b1);
                }
            }
            if ((sender as BusDetails).Tune)
            {
                Bus b1 = (sender as BusDetails).thisBus;
                bool tune = true;
                if (b1.Milage == 0)
                    tune = false;
                if(tune)
                {
                    
                    MessageBox.Show("Bus was tuned up", " ", MessageBoxButton.OK, MessageBoxImage.Information);
                    b1.Tune_up();
                    user = b1;
                    user.CanDrive = false;
                    b1.Status = Bus.Status_ops.At_mechanic;
                    b1.Time = 144;
                    b1.ButtonVisibility = true;
                    //  b1.Seconds = "at least";
                    worker.RunWorkerAsync(b1);
                    worker2.RunWorkerAsync(b1);
                }
            }
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
            if (DoesExist(resultBus.License))
                (sender as AddBus).AddIt = false;
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
            drive.Closed += DriveBusWindow_Closed;
            drive.ShowDialog();

        }

        private void DriveBusWindow_Closed(object sender, EventArgs e)
        {
            if ((sender as DriveBus).driven)
            {
                Random rand = new Random(DateTime.Now.Millisecond);
                int kamash = rand.Next(20, 51);
                //זמן*מהירות=דרך
                user = (sender as DriveBus).CurrentBus;
                user.Status = Bus.Status_ops.On_the_road;
                user.Time=((((sender as DriveBus).curr_milage)/kamash)*6);
                user.Status = Bus.Status_ops.On_the_road;
                user.CanDrive = false;
                worker.RunWorkerAsync(user);
                worker2.RunWorkerAsync(user);
            }
        }
    }
}

