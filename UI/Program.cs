﻿using System;
using BlApi;
using BO;
using System.Collections.Generic;
using System.Linq;


namespace PlConsole
{
    class Program
    {
        static IBL bl;
       
        static void Main(string[] args)
        {
            bl = BlFactory.GetBl();
            void PrintLines(List<BO.BusLine> lines)
            {

                for (int i = 0; i < lines.Count; i++)
                {
                    Console.WriteLine(lines[i].ToString());
                    Console.WriteLine("               * * * *   ");
                }
            }

            void PrintStations(List<BO.BusStation> stations)
            {

                for (int i = 0; i < stations.Count; i++)
                {
                    Console.WriteLine(stations[i].ToString());
                    Console.WriteLine("               * * * *   ");
                }
            }


            List<BO.BusLine> busLines = bl.GetAllBusLines().ToList();
            List<BO.BusStation> busStations = bl.GetAllBusStations().ToList();
            Console.WriteLine("All Bus Lines:");
            PrintLines(busLines);
            Console.WriteLine("All Bus Stations:");
            PrintStations(busStations);
            List<int> stationsOnLine = new List<int>();
            try
            {
                stationsOnLine.Add(73);
            }
            catch (StationNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (StationAlreadyExistsEOnTheLinexception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                stationsOnLine.Add(76);
            }
            catch (StationNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (StationAlreadyExistsEOnTheLinexception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                stationsOnLine.Add(77);
            }
            catch (StationNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (StationAlreadyExistsEOnTheLinexception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                bl.AddBusLine(35, stationsOnLine, 8, 0, 22, 0, new TimeSpan(1, 0, 0));
            }
            catch (BusLineAlreadyExistsException ex)
            {
                Console.WriteLine(ex.Message); 
            }
            Console.WriteLine("All Bus Lines:");
            PrintLines(busLines);
            Console.WriteLine("line 35:" + bl.GetBusLine(35));



        }
    }
}
//public static void initialize_Bus_line_stations()
//{
//    #region line number 1 //this one's a route that makes sense
//    Line_stations.Add(new BusLineStation {
//        StationID = 1522,
//        BusLineNumber = 1,
//        Number_on_route = 1,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 1523,
//        BusLineNumber = 1,
//        Number_on_route = 2,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 1512,
//        BusLineNumber = 3,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 1511,
//        BusLineNumber = 1,
//        Number_on_route = 4,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 1510,
//        BusLineNumber = 1,
//        Number_on_route = 5,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 1491,
//        BusLineNumber = 1,
//        Number_on_route = 6,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 1494,
//        BusLineNumber = 1,
//        Number_on_route = 7,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 1493,
//        BusLineNumber = 1,
//        Number_on_route = 8,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 1492,
//        BusLineNumber = 1,
//        Number_on_route = 9,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 1514,
//        BusLineNumber = 1,
//        Number_on_route = 10,
//        Exists = true
//    });
//    #endregion
//    #region line number 2
//    Line_stations.Add(new BusLineStation {
//        StationID = 73,
//        BusLineNumber = 2,
//        Number_on_route = 1,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 76,
//        BusLineNumber = 2,
//        Number_on_route = 2,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 77,
//        BusLineNumber = 2,
//        Number_on_route = 3,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 78,
//        BusLineNumber = 2,
//        Number_on_route = 4,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 83,
//        BusLineNumber = 2,
//        Number_on_route = 5,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 84,
//        BusLineNumber = 2,
//        Number_on_route = 6,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 85,
//        BusLineNumber = 2,
//        Number_on_route = 7,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 86,
//        BusLineNumber = 2,
//        Number_on_route = 8,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 88,
//        BusLineNumber = 2,
//        Number_on_route = 9,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 89,
//        BusLineNumber = 2,
//        Number_on_route = 10,
//        Exists = true
//    });
//    #endregion
//    #region line number 3
//    Line_stations.Add(new BusLineStation {
//        StationID = 91,
//        BusLineNumber = 3,
//        Number_on_route = 1,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 93,
//        BusLineNumber = 3,
//        Number_on_route = 2,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 84,
//        BusLineNumber = 3,
//        Number_on_route = 3,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 85,
//        BusLineNumber = 3,
//        Number_on_route = 4,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 86,
//        BusLineNumber = 3,
//        Number_on_route = 5,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 88,
//        BusLineNumber = 3,
//        Number_on_route = 6,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 89,
//        BusLineNumber = 3,
//        Number_on_route = 7,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 108,
//        BusLineNumber = 3,
//        Number_on_route = 8,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 109,
//        BusLineNumber = 3,
//        Number_on_route = 9,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 110,
//        BusLineNumber = 3,
//        Number_on_route = 10,
//        Exists = true
//    });
//    #endregion
//    #region line number 4
//    Line_stations.Add(new BusLineStation {
//        StationID = 108,
//        BusLineNumber = 4,
//        Number_on_route = 1,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 109,
//        BusLineNumber = 4,
//        Number_on_route = 2,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 110,
//        BusLineNumber = 4,
//        Number_on_route = 3,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 1485,
//        BusLineNumber = 4,
//        Number_on_route = 4,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 1486,
//        BusLineNumber = 4,
//        Number_on_route = 5,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 1487,
//        BusLineNumber = 4,
//        Number_on_route = 6,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 1488,
//        BusLineNumber = 4,
//        Number_on_route = 7,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 1490,
//        BusLineNumber = 4,
//        Number_on_route = 8,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 1491,
//        BusLineNumber = 4,
//        Number_on_route = 9,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 90,
//        BusLineNumber = 4,
//        Number_on_route = 10,
//        Exists = true
//    });
//    #endregion
//    #region line number 5
//    Line_stations.Add(new BusLineStation {
//        StationID = 1485,
//        BusLineNumber = 5,
//        Number_on_route = 1,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 1486,
//        BusLineNumber = 5,
//        Number_on_route = 2,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 1487,
//        BusLineNumber = 5,
//        Number_on_route = 3,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 90,
//        BusLineNumber = 5,
//        Number_on_route = 4,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 105,
//        BusLineNumber = 5,
//        Number_on_route = 5,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 84,
//        BusLineNumber = 5,
//        Number_on_route = 6,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 78,
//        BusLineNumber = 5,
//        Number_on_route = 7,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 115,
//        BusLineNumber = 5,
//        Number_on_route = 8,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 117,
//        BusLineNumber = 5,
//        Number_on_route = 9,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 119,
//        BusLineNumber = 5,
//        Number_on_route = 10,
//        Exists = true
//    });
//    #endregion
//    #region line number 6
//    Line_stations.Add(new BusLineStation {
//        StationID = 1485,
//        BusLineNumber = 6,
//        Number_on_route = 1,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 1486,
//        BusLineNumber = 6,
//        Number_on_route = 2,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 1487,
//        BusLineNumber = 6,
//        Number_on_route = 3,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 1488,
//        BusLineNumber = 6,
//        Number_on_route = 4,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 1490,
//        BusLineNumber = 6,
//        Number_on_route = 5,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 1510,
//        BusLineNumber = 6,
//        Number_on_route = 6,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 1491,
//        BusLineNumber = 6,
//        Number_on_route = 7,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 1493,
//        BusLineNumber = 6,
//        Number_on_route = 8,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 1492,
//        BusLineNumber = 6,
//        Number_on_route = 9,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 1511,
//        BusLineNumber = 6,
//        Number_on_route = 10,
//        Exists = true
//    });
//    #endregion
//    #region line number 7
//    Line_stations.Add(new BusLineStation {
//        StationID = 1491,
//        BusLineNumber = 7,
//        Number_on_route = 1,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 1494,
//        BusLineNumber = 7,
//        Number_on_route = 2,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 78,
//        BusLineNumber = 7,
//        Number_on_route = 3,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 123,
//        BusLineNumber = 7,
//        Number_on_route = 4,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 119,
//        BusLineNumber = 7,
//        Number_on_route = 5,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 122,
//        BusLineNumber = 7,
//        Number_on_route = 6,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 1524,
//        BusLineNumber = 7,
//        Number_on_route = 7,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 1493,
//        BusLineNumber = 7,
//        Number_on_route = 8,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 1522,
//        BusLineNumber = 7,
//        Number_on_route = 9,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 1512,
//        BusLineNumber = 7,
//        Number_on_route = 10,
//        Exists = true
//    });
//    #endregion
//    #region line number 8
//    Line_stations.Add(new BusLineStation {
//        StationID = 90,
//        BusLineNumber = 8,
//        Number_on_route = 1,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 91,
//        BusLineNumber = 8,
//        Number_on_route = 2,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 92,
//        BusLineNumber = 8,
//        Number_on_route = 3,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 93,
//        BusLineNumber = 8,
//        Number_on_route = 4,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 94,
//        BusLineNumber = 8,
//        Number_on_route = 5,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 95,
//        BusLineNumber = 8,
//        Number_on_route = 6,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 97,
//        BusLineNumber = 8,
//        Number_on_route = 7,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 96,
//        BusLineNumber = 8,
//        Number_on_route = 8,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 83,
//        BusLineNumber = 8,
//        Number_on_route = 9,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 85,
//        BusLineNumber = 8,
//        Number_on_route = 10,
//        Exists = true
//    });
//    #endregion
//    #region line number 9
//    Line_stations.Add(new BusLineStation {
//        StationID = 91,
//        BusLineNumber = 9,
//        Number_on_route = 1,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 93,
//        BusLineNumber = 9,
//        Number_on_route = 2,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 84,
//        BusLineNumber = 9,
//        Number_on_route = 3,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 85,
//        BusLineNumber = 9,
//        Number_on_route = 4,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 86,
//        BusLineNumber = 9,
//        Number_on_route = 5,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 1488,
//        BusLineNumber = 9,
//        Number_on_route = 6,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 1490,
//        BusLineNumber = 9,
//        Number_on_route = 7,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 1510,
//        BusLineNumber = 9,
//        Number_on_route = 8,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 1491,
//        BusLineNumber = 9,
//        Number_on_route = 9,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 1493,
//        BusLineNumber = 9,
//        Number_on_route = 10,
//        Exists = true
//    });
//    #endregion
//    #region line number 10
//    Line_stations.Add(new BusLineStation {
//        StationID = 1491,
//        BusLineNumber = 10,
//        Number_on_route = 1,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 1494,
//        BusLineNumber = 10,
//        Number_on_route = 2,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 78,
//        BusLineNumber = 10,
//        Number_on_route = 3,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 123,
//        BusLineNumber = 10,
//        Number_on_route = 4,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 119,
//        BusLineNumber = 10,
//        Number_on_route = 5,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 84,
//        BusLineNumber = 10,
//        Number_on_route = 6,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 78,
//        BusLineNumber = 10,
//        Number_on_route = 7,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 115,
//        BusLineNumber = 10,
//        Number_on_route = 8,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 117,
//        BusLineNumber = 10,
//        Number_on_route = 9,
//        Exists = true
//    });
//    Line_stations.Add(new BusLineStation {
//        StationID = 119,
//        BusLineNumber = 10,
//        Number_on_route = 10,
//        Exists = true
//    });
//    #endregion
//}