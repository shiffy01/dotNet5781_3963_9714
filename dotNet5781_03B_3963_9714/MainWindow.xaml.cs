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

namespace dotNet5781_03B_3963_9714
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       private List<Bus> buses = new List<Bus>();//list of buses
       void Initialize_bus_list()
        {
            for (int i = 1; i < 11; i++)//add 10 buses to the list
            {
                buses.Add(new Bus(i, DateTime.Now, i));
            }
            //make one bus be passed the tune up date:
            DateTime date = new DateTime(01, 01, 2017);
            buses[0].Last_tune_up = date;
            //make one bus at least be close to the max milage allowed:
            buses[1].Total_milage = 19990;
            //make one bus with very little gas:
            buses[3].Gas = 10;
        }

       




    public MainWindow()
        {
           
            InitializeComponent();
        }
    }
}
