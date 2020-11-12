using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_3963_9714
{
    class Program
    {
        static void Main(string[] args)
        {
            Bus_line_list buses = new Bus_line_list();//this is our collection of buses
            //add 10 bus lines, each one has a first and last stop so that adds 20 different stops
            for(int i=1; i<=10; i++)
                buses.add_line(new Bus_line(i, "Central", Bus_line_stop.make_bus_line_stop(i), Bus_line_stop.make_bus_line_stop(i+10)));
            //add another 20 stops to any two of the lines at random 
            for(int i=21; i<=40; i++)
            {
                Bus_line_stop b=Bus_line_stop.make_bus_line_stop(i);
                //randomly select two lines to add to
                Random rand = new Random(DateTime.Now.Millisecond);
                buses[rand.Next(1, 11)].add_stop(i, b);
                buses[rand.Next(1, 11)].add_stop(i, b);
            }

            Console.WriteLine("Please choose one of the following options:");
            Console.WriteLine("1 to add");
            Console.WriteLine("2 to erase");
            Console.WriteLine("3 to search");
            Console.WriteLine("4 to print");
            Console.WriteLine("5 to exit");
            int choice = int.Parse(Console.ReadLine());
            while (choice != 5)
            {
                switch (choice)
                {
                    case 1://add
                        Console.WriteLine("Please enter 1 to add a new bus line and 2 to add a new stop to an existing line");
                        int add = int.Parse(Console.ReadLine());
                        if (add==1)
                            Console.WriteLine("Enter the number of the Line to add");
                        //send to a function that checks if the number exists already in the collection
                      //  if (area1 != "General" && area1 != "North" && area1 != "South" && area1 != "Center" && area1 != "Jerusalem")
                     //then cout error, maybe tell them what the options are and that its only capital
                            if (add==2)
                            Console.WriteLine("Enter the number of the Line to add to");
                        if (add!=1&&add!=2)
                                    Console.WriteLine("Error! Cannot complete the action");
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    default:
                        break;
                }
                Console.WriteLine("Please choose another option");
            }
            Console.WriteLine("Thank you for using our program! We hope to see you again soon.");
        }
    }
}
