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

namespace dotNet5781_03B_3963_9714
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Bus> buses = new ObservableCollection<Bus>();
        void Initialize_bus_collection()
        {

            for (int i = 1; i < 11; i++)//add 10 buses to the list
            {

                switch (i % 3)
                {
                    case 0:
                        buses.Add(new Bus(i*1000001, DateTime.Now, i, 56, true, false, false));
                        break;
                    case 1:
                        buses.Add(new Bus(i*1100002, DateTime.Now, i, 20, true, true, true));
                        break;
                    case 2:
                        buses.Add(new Bus(i*1200003, DateTime.Now, i, 16, false, false, true));
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
            busDataGrid.IsReadOnly = true;
        }
      
private void busDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Bus b1 = (busDataGrid.SelectedItem as Bus);
            BusDetails bd = new BusDetails(b1);
            bd.ShowDialog();

        }
    
        // private void busdatagrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    Bus b1 = (busDataGrid.SelectedItem as Bus);
        //    BusDetails bd = new BusDetails(b1);
        //    bd.ShowDialog();
        //}


    }
}
