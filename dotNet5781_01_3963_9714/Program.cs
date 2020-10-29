using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

    namespace dotNet5781_01_3963_9714
    {
        class Program
        {
            public static bool drive(int license, List<Bus> buses)//drives the bus with this license
            {
                Bus bus1;
                bool can;
                for (int i = 0; i < buses.Count; i++)//for each bus in the company
                    {
                        if (buses[i].getLicense() == license)//if this is the bus the user wants to drive
                        { 
                            bus1 = buses[i];//save the bus in a paremeter
                            Random rand = new Random(DateTime.Now.Millisecond);
                            int num = rand.Next();//num is the length of the drive in km
                            can=bus1.send_bus(num);//sends to a function that drives the bus, and returns true if it was successful and false otherwise
                        if(!can)//if this bus could not drive
                            Console.WriteLine("This bus is unsafe or does not have enough gas and therefore cannot do the job");
                        return true;//the bus exists
                        }
                    }
                return false;//the bus does not exist
            }
                static void Main(string[] args)
            {
                List<Bus> buses = new List<Bus>();//list of buses in the company
                //options are printed out for the user
                Console.WriteLine("please choose one of the following options:");
                Console.WriteLine("1 to enter a new bus into the system");
                Console.WriteLine("2 to choose a bus for a ride");
                Console.WriteLine("3 to fill up gas or inspect");
                Console.WriteLine("4 to print the mileage of all the buses in the company");
                Console.WriteLine("5 to exit");
                int choice = int.Parse(Console.ReadLine());//get request from the user
                while (choice != 5)//if the user does not want to exit yet
                { 
                    switch (choice)
                    {
                        case 1://add a bus
                            Console.WriteLine("enter year, month and day the bus started working");
                            int year = int.Parse(Console.ReadLine());
                            int month = int.Parse(Console.ReadLine());
                            int day = int.Parse(Console.ReadLine());
                            DateTime date = new DateTime(year, month, day);//start date of the bus
                            Console.WriteLine("enter the license plate number:");
                           int li = int.Parse(Console.ReadLine());
                            if (date.Year < 2018)//if the bus bus made before 2018, its license must be 7 digits long
                                while (!(li > 9999999 || li < 1000000))//if license is invalid
                                {
                                    Console.WriteLine("Invalid. License must have 7 digits. Enter a new license plate number:");
                                    li = int.Parse(Console.ReadLine());
                                }
                            else//if the bus bus made after 2018, its license must be 8 digits
                                    while (!(li > 99999999 || li < 10000000))//license is invalid
                                { 
                                    Console.WriteLine("Invalid. License must have 8 digits. Enter a new license plate number:");
                                    li = int.Parse(Console.ReadLine());
                                }
                            Console.WriteLine("enter mileage:");
                            int mi = int.Parse(Console.ReadLine());
                            Console.WriteLine("enter year, month and day the bus had its last inspection");
                            int year1 = int.Parse(Console.ReadLine());
                            int month1 = int.Parse(Console.ReadLine());
                            int day1 = int.Parse(Console.ReadLine());
                            DateTime dateinspect = new DateTime(year, month, day);
                            Bus bus1 = new Bus(li, date, mi, dateinspect);
                            buses.Add(bus1);
                            break;
                        case 2:
                            Console.WriteLine("enter the license plate number of the bus you want to use");
                            int lic = int.Parse(Console.ReadLine());
                            bool worked = drive(lic, buses);
                            if(!worked)
                                Console.WriteLine("This bus does not exist in the company");
                            break;
                        case 3:
                            bool exists = false;
                            int license;
                            Bus bus2 = new Bus(0, new DateTime(0, 0, 0), 0.0, new DateTime(0, 0, 0));
                            do
                            {
                                Console.WriteLine("enter the license plate number of the bus you want to inspect/fill up with gas:");
                                license = int.Parse(Console.ReadLine());
                                for (int i = 0; i < buses.Count; i++)
                                {
                                    if (buses[i].getLicense() == license)
                                    {
                                        bus2 = buses[i];
                                        exists = true;
                                    }
                                }
                            }
                            while (!exists);
                        
                                Console.WriteLine("this bus does not exist. Please enter a new license plate number:");
                                license = int.Parse(Console.ReadLine());
                        
                        
                                Console.WriteLine("enter 1 for inspection and 2 for refill");
                                int number = int.Parse(Console.ReadLine());
                                if (number == 1)
                                    bus2.refill();
                                if (number == 2)
                                    bus2.inspection();
                        
                            break;
                        case 4:
                            for (int j = 0; j < buses.Count; j++)
                            {
                                buses[j].printBus();
                            }
                            break;
                    }
                    Console.WriteLine("please choose another option");
                    choice = int.Parse(Console.ReadLine());
                }
            }
        }
    }
