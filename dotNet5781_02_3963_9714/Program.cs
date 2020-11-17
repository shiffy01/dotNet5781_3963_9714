﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
//final version--everything works
namespace dotNet5781_02_3963_9714
{
    class Program
    {
        static void Main(string[] args)
        {
            Bus_line_list buses = new Bus_line_list();//this is our collection of buses
            //add 10 bus lines, each one has a first and last stop so that adds 20 different stops
            //although this code adds new lines, it is guaranteed that there will be no exceptions thrown because we used numbers that we know are not in the collection yet
                for (int i = 1; i <= 10; i++)
                    buses.add_line(new Bus_line(i, "Central", Bus_line_stop.make_bus_line_stop(i), Bus_line_stop.make_bus_line_stop(i + 10)));
            //add another 20 stops to any two of the lines at random 
            Random rand = new Random(DateTime.Now.Millisecond);
            for (int i=21; i<=40; i++)
            {
                //although this code uses functions that throw exceptions, it is guaranteed that there will be no exceptions thrown because we used numbers that we know are valid
                Bus_line_stop b =Bus_line_stop.make_bus_line_stop(i);
                //randomly select two lines to add to, making sure they are different
                int place = rand.Next(1, 11);//first bus line to add to
                int other_place;
                //second place to add to gets a different number
                if (place == 10)
                    other_place = 1;
                else other_place = place + 1;
                buses[place].add_stop(0, b);
                buses[other_place].add_stop(0, b);
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
                        try
                        {
                            if (add == 1)//add a new bus line
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
                                        Console.WriteLine("Error! Please enter one of the following options: (with a capital letter)");
                                } while (area != "General" && area != "North" && area != "South" && area != "Center" && area != "Jerusalem");
                                Console.WriteLine("Enter the codes for the first and last stops in the new route");
                                int first = int.Parse(Console.ReadLine());
                                int last = int.Parse(Console.ReadLine());
                                try
                                {
                                    Bus_line_stop first_stop = Bus_line_stop.make_bus_line_stop(first);
                                    Bus_line_stop last_stop = Bus_line_stop.make_bus_line_stop(last);
                                    Bus_line b = new Bus_line(num, area, first_stop, last_stop);
                                    buses.add_line(b);//add the bus line to the collection
                                    Console.WriteLine("The line was successfully added to the list");
                                }
                                catch (ArgumentOutOfRangeException)//if code is greater than 999999
                                {
                                    Console.WriteLine("Error! Cannot add this bus because the bus stop code exceeds six digits");
                                }
                                catch (ArgumentException ex)//if the bus line is already on the route
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }
                            if (add == 2)//add a new stop to an existing bus line
                            {
                                //get all the information and add the stop:
                                Console.WriteLine("Enter the number of the Line to add to");
                                int line_number = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter the code of the new stop");
                                int stop_number = int.Parse(Console.ReadLine());
                                try
                                {
                                    Bus_line_stop b = Bus_line_stop.make_bus_line_stop(stop_number);


                                    Console.WriteLine("Enter the number of the stop you want the new stop to come after. To add at the beginning enter 0");
                                    int code = int.Parse(Console.ReadLine());


                                    buses[line_number].add_stop(code, b);
                                    Console.WriteLine("The stop was successfully added to the bus line");
                                }
                                catch (ArgumentOutOfRangeException ex)//either the line number does not exist or the stop number has more than 6 digits
                                {
                                    Console.WriteLine(ex.ParamName);
                                }
                                catch (ArgumentException ex)//if the stop to add after wasnt on the route
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }
                            if (add != 1 && add != 2)
                                throw new ArgumentException();
                        }
                        catch(ArgumentException)
                        {
                            Console.WriteLine("Error! Cannot complete the action");
                        }
                        break;
                    case 2://erase
                        Console.WriteLine("Please enter 1 to remove a bus line and 2 to remove a stop from an existing line");
                        int erase = int.Parse(Console.ReadLine());
                        try
                        {
                            if (erase == 1)//remove bus line
                            {
                                Console.WriteLine("Enter the number of the bus line to remove");
                                int code = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter the number of the first stop of the bus line to remove");//to now which it is because there can be two of each bus line
                                int code_first = int.Parse(Console.ReadLine());
                                try
                                {
                                    buses.remove_line(code, code_first);
                                }
                                catch (ArgumentException ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }
                            if (erase == 2)//remove a stop from one of the bus lines
                            {
                                Console.WriteLine("Enter the number of the bus line to remove from");
                                int code_bus = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter the number of the stop to remove");
                                int code_stop = int.Parse(Console.ReadLine());
                                try
                                {
                                    buses[code_bus].remove_stop(code_stop);
                                    Console.WriteLine("Stop was successfully removed");
                                }
                                catch (ArgumentOutOfRangeException)//bus line doesn't exist
                                {
                                    Console.WriteLine("The bus line does not exist");
                                }
                                catch (ArgumentException ex)//bus stop is not on the bus route
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }
                            if (erase != 1 && erase != 2)
                                throw new ArgumentException();
                        }
                        catch (ArgumentException)
                        {
                            Console.WriteLine("Error! Cannot complete the action");
                        }
                        break;
                    case 3://search
                        Console.WriteLine("Please enter 1 to search for lines that go through a certain bus stop and 2 to search the shortest route from one stop to another");
                        int search = int.Parse(Console.ReadLine());
                        try
                        {
                            if (search == 1)//searches for all bus lines that go through a certain bus stop
                            {
                                Console.WriteLine("Please enter the code of the bus stop");
                                int code = int.Parse(Console.ReadLine());
                                try
                                {
                                    List<Bus_line> b = buses.has_stop(code);
                                    for (int i = 0; i < b.Count; i++)
                                        Console.WriteLine(b[i]);
                                }
                                catch (ArgumentOutOfRangeException)//if no buses go through that stop
                                {
                                    Console.WriteLine("No buses pass through this stop");
                                }
                            }
                            if (search == 2)//prints list of routes between two stops
                            {
                                Console.WriteLine("Please enter the code of the the first stop");
                                int code1 = int.Parse(Console.ReadLine());
                                Console.WriteLine("Please enter the code of the second stop");
                                int code2 = int.Parse(Console.ReadLine());
                                try
                                {
                                    List<Bus_line> short_routes = buses.shortest_routes(code1, code2);//gets a list of the sorted routes
                                    for (int i = 0; i < short_routes.Count; i++)
                                    {
                                        //print the line numbers and travel times in order, shortest to longest
                                        //only sending valid codes to travel time, so it wont throw an exception
                                        Console.WriteLine("Bus #" + short_routes[i].Line_number + ": " + short_routes[i].travel_time(short_routes[i].First_stop, short_routes[i].Last_stop) + " minutes");
                                    }
                                }
                                catch (ArgumentException ex)//there were no routes between the two stops
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }
                            if (search != 1 && search != 2)
                                throw new ArgumentException();
                        }
                        catch (ArgumentException)
                        {
                            Console.WriteLine("Error! Cannot complete the action");
                        }
                        break;
                    case 4:
                        Console.WriteLine("Please enter 1 to print all the bus lines in the system and two to print each bus stop with the numbers of bus lines that go through it" );
                        int print = int.Parse(Console.ReadLine());
                        try
                        {
                            if (print == 1)//print all the bus lines
                            {
                                for (int i = 0; i < buses.Count; i++)
                                    Console.WriteLine(buses.busLines[i]);
                            }
                            if (print == 2)//print the bus stops with the lines that go through them
                            {
                                for (int i = 0; i < Bus_line_stop.stop_list.Count; i++)//go through the saved list of stops
                                {
                                    try
                                    {
                                        List<Bus_line> b = buses.has_stop(Bus_line_stop.stop_list[i].Code);
                                        Console.WriteLine("Bus stop #" + Bus_line_stop.stop_list[i].Code + ":");//print the stop
                                        for (int j = 0; j < b.Count; j++)//and the buses that come to this stop
                                            Console.WriteLine(b[j]);
                                    }
                                    catch (ArgumentOutOfRangeException)//if there are no buses that go through one of the stops
                                    {
                                        //leave the catch and continue printing the rest of the stops
                                    }
                                }
                            }
                            if (print != 1 && print != 2)
                                throw new ArgumentException();
                        }
                        catch (ArgumentException)
                        {
                            Console.WriteLine("Error! Cannot complete the action");
                        }
                        break;
                    default:
                        Console.WriteLine("Error! Cannot complete the action");
                        break;
                }
                Console.WriteLine("Please choose another option:");
                Console.WriteLine("1 to add");
                Console.WriteLine("2 to erase");
                Console.WriteLine("3 to search");
                Console.WriteLine("4 to print");
                Console.WriteLine("5 to exit");
                choice = int.Parse(Console.ReadLine());
            }
            Console.WriteLine("Thank you for using our program! We hope to see you again soon.");
        }
    }
}
