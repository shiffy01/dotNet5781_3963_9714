using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace dotNet5781_01_3963_9714
{
    class Program
    {
        public static string drive(int license, List<Bus> buses)//drives a bus
        {
            Bus bus1;
            bool can;
            for (int i = 0; i < buses.Count; i++)
                {
                    if (buses[i].getLicense() == license)//if this is the bus the user wants to drive
                    { 
                        bus1 = buses[i];
                        Random rand = new Random(DateTime.Now.Millisecond);
                        int num = rand.Next(1, 1201);//num is the length of the drive measured in km, cannot exceed full gas tank
                        can=bus1.send_bus(num);//this function drives the bus and returns true, or returns false if the bus can't drive
                    if (can)
                        return "Bus was successfully driven";
                    else 
                        return "Bus needs insepction or refill";
                    }
                }
            return "Bus not found";//bus does not exist
        }
            static void Main(string[] args)
        {
            List<Bus> buses = new List<Bus>();//list of buses
            int choice;
            //options menu
            Console.WriteLine("Please choose one of the following options:");
            Console.WriteLine("1 to enter a new bus into the system");
            Console.WriteLine("2 to choose a bus for a drive");
            Console.WriteLine("3 to fill up gas or inspect");
            Console.WriteLine("4 to print the mileage of all the buses in the company");
            Console.WriteLine("5 to exit");
            //the user enters a number according to his/her choice
            int.TryParse(Console.ReadLine(), out choice);
            while (choice != 5)//the user didnt request to exit yet
            {
                int year, month, day, licensePlate, mileage, number;
                switch (choice)
                {
                    case 1://add a bus to the company
                        Console.WriteLine("Enter year, month and day the bus started working");
                        int.TryParse(Console.ReadLine(), out year);
                        int.TryParse(Console.ReadLine(), out month);
                        int.TryParse(Console.ReadLine(), out day);
                        DateTime date = new DateTime(year, month, day);//date the bus started driving
                        Console.WriteLine("Enter the license plate number:");
                        int.TryParse(Console.ReadLine(), out licensePlate);
                        bool exists = false, keepOn=false;
                        do
                        {
                            if (date.Year < 2018)//if the bus bus made before 2018, its license must be 7 digits
                            {
                                while ((licensePlate > 9999999 || licensePlate < 1000000))//license is invalid
                                {
                                    Console.WriteLine("Invalid. License must have 7 digits. Enter a new license plate number:");
                                    int.TryParse(Console.ReadLine(), out licensePlate);
                                }
                            }
                            else//if the bus bus made after 2018, its license must be 8 digits
                            {
                                while ((licensePlate > 99999999 || licensePlate < 10000000))//license is invalid
                                {
                                    Console.WriteLine("Invalid. License must have 8 digits. Enter a new license plate number:");
                                    int.TryParse(Console.ReadLine(), out licensePlate);
                                }
                            }
                            exists = false;//this bus was not yet found in the list of buses
                            for (int i = 0; i < buses.Count; i++)//go over the list and make sure this license plate is not taken already
                            {
                                if (buses[i].getLicense() == licensePlate)//if the bus exists already
                                {
                                    Console.WriteLine("This bus is already in the system. Enter a new license plate number or enter 0 to choose a new option");
                                    int.TryParse(Console.ReadLine(), out licensePlate);
                                    if (licensePlate != 0)//the user entered a new license plate number-- try again
                                        exists = true;
                                    else//the user entered 0--continue and offer a choice of choosing a new option
                                        keepOn = true;
                                }
                            }
                        } while (exists);//if this bus already is in the system, repeat
                        if(!keepOn)//if the user did not choose to pick a different option instead
                        { 
                        Console.WriteLine("Enter mileage:");
                        int.TryParse(Console.ReadLine(), out mileage);//mileage
                        Bus bus1 = new Bus(licensePlate, date, mileage);//make a new bus with all the new data
                        buses.Add(bus1);//and add it to the list of buses
                            //buses are automaticlly inspected and filled up with gas when they enter the system
                            Console.WriteLine("Bus was added successfully");
                        }
                        break;
                    case 2://drive a bus
                        Console.WriteLine("Enter the license plate number of the bus you want to use");
                        int.TryParse(Console.ReadLine(), out licensePlate);//get license plate number from the user
                        string worked = drive(licensePlate, buses);//this function finds the bus to drive and drives it, returning true, if it can't find the bus it returns false
                        while (worked== "Bus not found")//if the bus does not exist
                        {
                            Console.WriteLine("This bus does not exist in the company. Please enter a new license plate number or enter 0 to choose a different option");
                            int.TryParse(Console.ReadLine(), out licensePlate);
                            if (licensePlate == 0)//continue and let the user choose a different option
                                worked = "";//leave while
                            else
                            worked = drive(licensePlate, buses);//try to drive this bus
                        }
                        while(worked== "Bus needs insepction or refill")
                        {
                            Console.WriteLine("The bus needs an insepction or a refill. Please enter a new license plate number or enter 0 to choose a different option");
                            int.TryParse(Console.ReadLine(), out licensePlate);
                            if (licensePlate == 0)//continue and let the user choose a different option
                                worked = "";//leave while
                            else
                                worked = drive(licensePlate, buses);//try to drive this bus
                        }
                        if(worked== "Bus was successfully driven")
                            Console.WriteLine(worked);
                        break;
                    case 3://inspect/refill
                        Console.WriteLine("Enter the license plate number of the bus you want to inspect/fill up with gas:");
                        int.TryParse(Console.ReadLine(), out licensePlate);
                        Bus bus2 = new Bus(0, new DateTime(2000, 01, 01), 0);//empty bus
                        exists = false;//so far the bus is not found
                        do
                        {
                            for (int i = 0; i < buses.Count; i++)//go over the whole list of buses
                            {
                                if (buses[i].getLicense() == licensePlate)//if this is the bus to inspect/refill
                                {
                                    bus2 = buses[i];
                                    exists = true;//found the bus
                                }
                            }
                            if (!exists)
                            {
                                Console.WriteLine("This bus does not exist in the company. Please enter a new license plate number or enter 0 to choose a different option");
                                int.TryParse(Console.ReadLine(), out licensePlate);
                                if (licensePlate == 0)//continue and let the user choose a new option
                                    exists = true;
                            }
                        }
                        while (!exists);//redo if the license number was wrong and the user wants to try again
                        if (licensePlate != 0)//if the user does not want to choose a different option instead,
                        {
                            Console.WriteLine("Enter 1 for refill and 2 for inspection");
                            int.TryParse(Console.ReadLine(), out number);
                            if (number == 1)//refill
                            { 
                                bus2.refill();
                                Console.WriteLine("Bus refilled with a full tank of gas");
                            }
                            if (number == 2)//inspection
                            { 
                                bus2.inspection();
                                Console.WriteLine("Bus inspected and is now safe");
                            }
                        }
                        break;
                    case 4://print
                        for (int i = 0; i < buses.Count; i++)//for all the buses, print license plate number and mileage
                        {
                            buses[i].printBus();
                        }
                            break;
                }
                Console.WriteLine("Please choose another option");//get another option from the user
                int.TryParse(Console.ReadLine(), out choice);
            }
        }
    }
}
