﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_3963_9714
{
    class Program
    {
        public static bool drive(int license, List<Bus> buses)
        {
            Bus bus1;
            int i;
            bool can;
            for (i = 0; i < buses.Count; i++;)
                {
                    if (buses[i].getLicense() == license)
                    { 
                        bus1 = buses[i];
                        Random rand = new Random(DateTime.Now.Millisecond);
                        int num = rand.Next();
                        can=bus1.SendTheBus(num);
                    if(!can)
                        Console.WriteLine("This bus is unsafe and therefore cannot do the job");
                    return true;
                    }
                }
            return false;
        }
            static void Main(string[] args)
        {
            List<Bus> buses = new List<Bus>();
            int num;
            Console.WriteLine("please choose one of the following options:");
            Console.WriteLine("1 to enter a new bus into the system");
            Console.WriteLine("2 to choose a bus for a ride");
            Console.WriteLine("3 to fill up gas or inspect");
            Console.WriteLine("4 to print the mileage of all the buses in the company");
            Console.WriteLine("5 to exit");
            num = int.Parse(Console.ReadLine());
            while (num != 5)
            { 
                switch (num)
                {
                    case 1:
                        Console.WriteLine("enter year, month and day the bus started working");
                        int year = int.Parse(Console.ReadLine());
                        int month = int.Parse(Console.ReadLine());
                        int day = int.Parse(Console.ReadLine());
                        DateTime date = new DateTime(year, month, day);
                        Console.WriteLine("enter the license plate number:");
                       int li = int.Parse(Console.ReadLine());
                        if (date.Year < 2018)//if the bus bus made before 2018, its license must be 7 digits
                            if (!(li > 9999999 || li < 1000000))//license is invalid
                            {
                                Console.WriteLine("Invalid. License must have 7 digits. Enter a new license plate number:");
                                li = int.Parse(Console.ReadLine());
                            }
                            else//if the bus bus made after 2018, its license must be 8 digits
                            {
                                if (!(li > 99999999 || li < 10000000))//license is invalid
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

                } 
            }
        }
    }
}
