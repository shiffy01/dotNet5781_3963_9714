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
                        Console.WriteLine("enter the license plate number of the bus you want to inspect/fill up with gas:");
                        int license = int.Parse(Console.ReadLine());
                        int i;
                        Bus bus2=new Bus(0, new DateTime(0, 0, 0), 0.0, new DateTime(0, 0, 0));
                        bool exists = false;
                        for (i = 0; i < buses.Count; i++;)
                         {
                            if (buses[i].getLicense() == license)
                            {
                                bus2 = buses[i];
                                exists = true;
                            }
                         }
                        if (!exists)
                            Console.WriteLine("this bus does not exist");                  //should there be an option to put the number in again...? (and then no else)
                        else
                        { 
                            Console.WriteLine("enter 1 for inspection and 2 for refill");
                            int number = int.Parse(Console.ReadLine());
                            if (number == 1)
                                bus2.refill();
                            if (number == 2)
                                bus2.inspection();
                        }
                        break;
                    case 4:
                        int j;
                        for (j = 0; j < buses.Count; j++;)
                        {
                            int licenseplate = buses[j].getLicense();
                            year = buses[j].getStartDate().Year;
                            if (year < 2018)
                                Console.WriteLine(year);
                             else
                                        //here print in 8 digits
                             buses[j].getMilage();
                            Console.WriteLine("mileage:"+buses[j].getMilage());
                        }
                        break;
                } 
            }
        }
    }
}
