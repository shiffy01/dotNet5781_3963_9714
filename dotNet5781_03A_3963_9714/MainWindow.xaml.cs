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
using dotNet5781_02_3963_9714;

namespace dotNet5781_03A_3963_9714
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Bus_line currentDisplayBusLine;
        private Bus_line_list bus = new Bus_line_list();
       private void add_lines()
        {
            //add 10 bus lines, each one has a first and last stop so that adds 20 different stops
            //although this code adds new lines, it is guaranteed that there will be no exceptions thrown because we used numbers that we know are not in the collection yet
            for (int i = 1; i <= 10; i++)
            { Random rand = new Random(DateTime.Now.Millisecond);
                int new_line_number = rand.Next(1, 100);
                int first_stop_number = rand.Next(1, 20);
                int last_stop_number = rand.Next(1, 20);
                while (last_stop_number == first_stop_number)
                { last_stop_number = rand.Next(1, 20); }
                int stop1_number = rand.Next(1, 20);
                while (stop1_number == first_stop_number||stop1_number==last_stop_number)
                { stop1_number = rand.Next(1, 20); }
                int stop2_number = rand.Next(1, 20);
                while (stop2_number == first_stop_number || stop2_number == last_stop_number|| stop2_number == stop1_number)
                { stop2_number = rand.Next(1, 20); }
                int stop3_number = rand.Next(1, 20);
                while (stop3_number == first_stop_number || stop3_number == last_stop_number || stop3_number == stop1_number|| stop3_number == stop2_number)
                { stop3_number = rand.Next(1, 20); }
                Bus_line_stop bus_stop_first = Bus_line_stop.make_bus_line_stop(first_stop_number);
                Bus_line_stop bus_stop_last = Bus_line_stop.make_bus_line_stop(last_stop_number);
                Bus_line_stop stop1 = Bus_line_stop.make_bus_line_stop(stop1_number);
                Bus_line_stop stop2 = Bus_line_stop.make_bus_line_stop(stop2_number);
                Bus_line_stop stop3 = Bus_line_stop.make_bus_line_stop(stop3_number);
                string region="";
               switch(i%4)
                {
                    case 0: region = "North";
                        break;
                    case 1: region = "South";
                        break;
                    case 2: region = "Jerusalem";
                        break;
                    case 3: region = "Central";
                        break; 
                }
                try
                {
                    bus.add_line(new Bus_line(new_line_number, region, bus_stop_first, bus_stop_last));
                    bus[new_line_number].add_stop(first_stop_number, stop1);
                    bus[new_line_number].add_stop(stop1_number, stop2);
                    bus[new_line_number].add_stop(stop2_number, stop3);

                }
                catch(ArgumentException)//if number of line already exists
                {
                    i--;//retry adding a line
                }
            }
        }
        void create_20_stops()//creates 20 stops, that are added to static list "stop_list" in bus_line_stops
        {
            for (int i = 1; i <= 20; i++)
            {
                //although this code uses functions that throw exceptions, it is guaranteed that there will be no exceptions thrown because we used numbers that we know are valid
                Bus_line_stop bus_stop = Bus_line_stop.make_bus_line_stop(i);
            }
        }
        public MainWindow()
        {
            create_20_stops();
            add_lines();    
            InitializeComponent();
            cbBusLines.ItemsSource = bus;
            cbBusLines.DisplayMemberPath = "Line_number";
            cbBusLines.SelectedIndex = 0;
        }
        private void ShowBusLine(int index)
        {
            currentDisplayBusLine = bus[index];
            UpGrid.DataContext = currentDisplayBusLine;
            lbBusLineStation.DataContext = currentDisplayBusLine.stops;
        }
        private void ___cbBusLines__SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowBusLine((cbBusLines.SelectedValue as Bus_line).Line_number);
        }

        
    }
}
