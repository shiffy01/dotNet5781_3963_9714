using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using dotNet5781_01_3963_9714;

namespace dotNet5781_03B_3963_9714
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class BusCompany : Window
    {
        private ObservableCollection<Bus> buses = new ObservableCollection<Bus>();//collection of buses

       
       void Initialize_bus_collection()
        {

            for (int i = 1; i < 11; i++)//add 10 buses to the list
            {

                switch (i % 3)
                {
                    case '0':
                        buses.Add(new Bus(i, DateTime.Now, i, 56, true, false, false));
                        break;
                    case '1':
                        buses.Add(new Bus(i, DateTime.Now, i, 20, true, true, true));
                        break;
                    case '2':
                        buses.Add(new Bus(i, DateTime.Now, i, 16, false, false, true));
                        break;
                };
                //make one bus be passed the tune up date:
                DateTime date = new DateTime(01, 01, 2017);
                buses[0].Last_tune_up = date;
                //make one bus at least be close to the max milage allowed:
                buses[1].TotalMilage = 19990;
                //make one bus with very little gas:
                buses[3].Gas = 10;
            }
        }

       




    public BusCompany()
        {
           
            InitializeComponent();
            lb_buses.DataContext = buses;
            Initialize_bus_collection();//add busses to the collection
           

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BusDetails busDetails = new BusDetails();
            busDetails.ShowDialog();

        }
        private void Click_drive(object sender, RoutedEventArgs e)
        {
            //open drive window!
        }
        private void Click_gas(object sender, RoutedEventArgs e)
        {
          
        }
        private void lb_buses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          //figure out what to do
        }
        private void Click_add(object sender, RoutedEventArgs e)
        {
            //not sure how to add an object of data template type
        }
    }
}
