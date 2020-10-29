using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_3963_9714
{
    class Program
    {
        static void Main(string[] args)
        {
            drive
            List<Bus> buses;
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
                        Console.WriteLine("enter the license plate number:");
                       int li = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter year, month and day the bus started working");
                        int year = int.Parse(Console.ReadLine());
                        int month = int.Parse(Console.ReadLine());
                        int day = int.Parse(Console.ReadLine());
                        DateTime newDate=new DateTime(year, month, day);
                        Console.WriteLine("enter the mileage of the bus");
                        int mi = int.Parse(Console.ReadLine());
                        Bus bus1 = new Bus(li, newDate, mi);
                        buses.Add(bus1);
                        break;
                    case 2:
                        Console.WriteLine("enter the license plate number of the bus you want to use");
                        int lic = int.Parse(Console.ReadLine());

                        break;

                } 
            }
        }
    }
}
