using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//need to finish case3, do case 4 and default
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
                        if (add==1)//add a new bus line
                        {  
                            //get all the info and build a new bus line:
                            Console.WriteLine("Enter the number of the Line to add");
                            int num = int.Parse(Console.ReadLine());
                            Console.WriteLine("Which part of the country are the bus's routes in? Please choose one of the following:");
                            string area;
                            do//keep asking for a valid area untill recieving one
                            {
                                Console.WriteLine("General, North, South, Center, Jerusalem");
                                area = Console.ReadLine();
                                if (area != "General" && area != "North" && area != "South" && area != "Center" && area != "Jerusalem")
                                    Console.WriteLine("Error! Please enter one of the following options: (with a capital letter)");//EXCEPTION??
                            } while (area != "General" && area != "North" && area != "South" && area != "Center" && area != "Jerusalem");
                            Console.WriteLine("enter the codes for the first and last stops in the new route");
                            int first = int.Parse(Console.ReadLine());
                            int last = int.Parse(Console.ReadLine());
                            Bus_line_stop first_stop = Bus_line_stop.make_bus_line_stop(first);
                            Bus_line_stop last_stop = Bus_line_stop.make_bus_line_stop(last);
                            Bus_line b = new Bus_line(num, area, first_stop, last_stop);
                            buses.add_line(b);//add the bus line to the collection
                        }
                        if (add == 2)//add a new stop to an existing bus line
                        { 
                            //get all the information and add the stop:
                            Console.WriteLine("Enter the number of the Line to add to");
                            int line_number = int.Parse(Console.ReadLine());
                            Console.WriteLine("Enter the number of the stop you want the new stop to come after");
                            int stop_number = int.Parse(Console.ReadLine());
                            Bus_line_stop b = Bus_line_stop.make_bus_line_stop(stop_number);
                            Console.WriteLine("Enter the code of the new stop");
                            int code = int.Parse(Console.ReadLine());
                            buses[line_number].add_stop(code, b);
                        }
                        if (add!=1&&add!=2)
                                    Console.WriteLine("Error! Cannot complete the action");//EXCEPTION???
                        break;
                    case 2://erase
                        Console.WriteLine("Please enter 1 to remove a bus line and 2 to remove a stop from an existing line");
                        int erase = int.Parse(Console.ReadLine());
                        if (erase == 1)//remove bus line
                        {
                            Console.WriteLine("Enter the number of the bus line to remove");
                            int code = int.Parse(Console.ReadLine());
                            Console.WriteLine("Enter the number of the first stop of the bus line to remove");
                            int code_first = int.Parse(Console.ReadLine());
                            buses.remove_line(code, code_first);
                        }
                        if (erase == 2)//remove a stop from one of the bus lines
                        {
                            Console.WriteLine("Enter the number of the bus line to remove from");
                            int code_bus = int.Parse(Console.ReadLine());
                            Console.WriteLine("Enter the number of the stop to remove");
                            int code_stop = int.Parse(Console.ReadLine());
                            buses[code_bus].remove_stop(code_stop);
                        }
                        if (erase != 1 && erase != 2)
                        {
                            Console.WriteLine("error! Cannot complete action");//EXCEPTION!!
                        }
                        break;
                    case 3://search
                        Console.WriteLine("Please enter 1 to search for lines that go through a certain bus stop and 2 to search the shortest route from one stop to another");
                        int search = int.Parse(Console.ReadLine());
                        if (search == 1)
                        {
                            Console.WriteLine("Please enter the code of the bus stop");
                            int code = int.Parse(Console.ReadLine());
                            List<Bus_line> b = buses.has_stop(code);
                            for(int i=0; i<b.Count; i++)
                                Console.WriteLine(b[i]);
                        }
                        if (search == 2)
                        {
                            Console.WriteLine("Please enter the code of the the first stop");
                            int code1 = int.Parse(Console.ReadLine());
                            Console.WriteLine("Please enter the code of the second stop");
                            int code2 = int.Parse(Console.ReadLine());
                            buses.searchroute(code1, code2);//will be called something else, also i dont know what it returns.
                            //i think its supposed to return bus line numbers in order
                            //like a list with bus lines sorted from the one that has the shortest route between the stops to the longest
                        }
                        if(search!=1&&search!=2)
                            Console.WriteLine("error! cannot complete action!");//EXCEPTION!!
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
