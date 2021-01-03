using System;
using BlApi;
using BO;


namespace PlConsole
{
    class Program
    {
        static IBL bl;

        static void Main(string[] args)
        {
            bl = BlFactory.GetBl();

            Console.Write("Please enter how many days back: ");
            int days = int.Parse(Console.ReadLine());
            for (int d = days; d >= 0; --d)
            {
                Weather w = bl.GetWeather(d);
                Console.WriteLine($"{d} days before - Feeling was: {w.Feeling} Celsius degrees");
            }

        }
    }
}
public static void initialize_Bus_line_stations()
{
    #region line number 1 //this one's a route that makes sense
    Line_stations.Add(new BusLineStation {
        StationID = 1522,
        BusLineNumber = 1,
        Number_on_route = 1,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 1523,
        BusLineNumber = 1,
        Number_on_route = 2,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 1512,
        BusLineNumber = 3,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 1511,
        BusLineNumber = 1,
        Number_on_route = 4,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 1510,
        BusLineNumber = 1,
        Number_on_route = 5,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 1491,
        BusLineNumber = 1,
        Number_on_route = 6,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 1494,
        BusLineNumber = 1,
        Number_on_route = 7,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 1493,
        BusLineNumber = 1,
        Number_on_route = 8,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 1492,
        BusLineNumber = 1,
        Number_on_route = 9,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 1514,
        BusLineNumber = 1,
        Number_on_route = 10,
        Exists = true
    });
    #endregion
    #region line number 2
    Line_stations.Add(new BusLineStation {
        StationID = 73,
        BusLineNumber = 2,
        Number_on_route = 1,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 76,
        BusLineNumber = 2,
        Number_on_route = 2,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 77,
        BusLineNumber = 2,
        Number_on_route = 3,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 78,
        BusLineNumber = 2,
        Number_on_route = 4,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 83,
        BusLineNumber = 2,
        Number_on_route = 5,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 84,
        BusLineNumber = 2,
        Number_on_route = 6,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 85,
        BusLineNumber = 2,
        Number_on_route = 7,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 86,
        BusLineNumber = 2,
        Number_on_route = 8,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 88,
        BusLineNumber = 2,
        Number_on_route = 9,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 89,
        BusLineNumber = 2,
        Number_on_route = 10,
        Exists = true
    });
    #endregion
    #region line number 3
    Line_stations.Add(new BusLineStation {
        StationID = 91,
        BusLineNumber = 3,
        Number_on_route = 1,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 93,
        BusLineNumber = 3,
        Number_on_route = 2,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 84,
        BusLineNumber = 3,
        Number_on_route = 3,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 85,
        BusLineNumber = 3,
        Number_on_route = 4,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 86,
        BusLineNumber = 3,
        Number_on_route = 5,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 88,
        BusLineNumber = 3,
        Number_on_route = 6,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 89,
        BusLineNumber = 3,
        Number_on_route = 7,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 108,
        BusLineNumber = 3,
        Number_on_route = 8,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 109,
        BusLineNumber = 3,
        Number_on_route = 9,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 110,
        BusLineNumber = 3,
        Number_on_route = 10,
        Exists = true
    });
    #endregion
    #region line number 4
    Line_stations.Add(new BusLineStation {
        StationID = 108,
        BusLineNumber = 4,
        Number_on_route = 1,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 109,
        BusLineNumber = 4,
        Number_on_route = 2,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 110,
        BusLineNumber = 4,
        Number_on_route = 3,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 1485,
        BusLineNumber = 4,
        Number_on_route = 4,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 1486,
        BusLineNumber = 4,
        Number_on_route = 5,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 1487,
        BusLineNumber = 4,
        Number_on_route = 6,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 1488,
        BusLineNumber = 4,
        Number_on_route = 7,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 1490,
        BusLineNumber = 4,
        Number_on_route = 8,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 1491,
        BusLineNumber = 4,
        Number_on_route = 9,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 90,
        BusLineNumber = 4,
        Number_on_route = 10,
        Exists = true
    });
    #endregion
    #region line number 5
    Line_stations.Add(new BusLineStation {
        StationID = 1485,
        BusLineNumber = 5,
        Number_on_route = 1,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 1486,
        BusLineNumber = 5,
        Number_on_route = 2,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 1487,
        BusLineNumber = 5,
        Number_on_route = 3,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 90,
        BusLineNumber = 5,
        Number_on_route = 4,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 105,
        BusLineNumber = 5,
        Number_on_route = 5,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 84,
        BusLineNumber = 5,
        Number_on_route = 6,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 78,
        BusLineNumber = 5,
        Number_on_route = 7,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 115,
        BusLineNumber = 5,
        Number_on_route = 8,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 117,
        BusLineNumber = 5,
        Number_on_route = 9,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 119,
        BusLineNumber = 5,
        Number_on_route = 10,
        Exists = true
    });
    #endregion
    #region line number 6
    Line_stations.Add(new BusLineStation {
        StationID = 1485,
        BusLineNumber = 6,
        Number_on_route = 1,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 1486,
        BusLineNumber = 6,
        Number_on_route = 2,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 1487,
        BusLineNumber = 6,
        Number_on_route = 3,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 1488,
        BusLineNumber = 6,
        Number_on_route = 4,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 1490,
        BusLineNumber = 6,
        Number_on_route = 5,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 1510,
        BusLineNumber = 6,
        Number_on_route = 6,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 1491,
        BusLineNumber = 6,
        Number_on_route = 7,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 1493,
        BusLineNumber = 6,
        Number_on_route = 8,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 1492,
        BusLineNumber = 6,
        Number_on_route = 9,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 1511,
        BusLineNumber = 6,
        Number_on_route = 10,
        Exists = true
    });
    #endregion
    #region line number 7
    Line_stations.Add(new BusLineStation {
        StationID = 1491,
        BusLineNumber = 7,
        Number_on_route = 1,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 1494,
        BusLineNumber = 7,
        Number_on_route = 2,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 78,
        BusLineNumber = 7,
        Number_on_route = 3,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 123,
        BusLineNumber = 7,
        Number_on_route = 4,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 119,
        BusLineNumber = 7,
        Number_on_route = 5,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 122,
        BusLineNumber = 7,
        Number_on_route = 6,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 1524,
        BusLineNumber = 7,
        Number_on_route = 7,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 1493,
        BusLineNumber = 7,
        Number_on_route = 8,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 1522,
        BusLineNumber = 7,
        Number_on_route = 9,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 1512,
        BusLineNumber = 7,
        Number_on_route = 10,
        Exists = true
    });
    #endregion
    #region line number 8
    Line_stations.Add(new BusLineStation {
        StationID = 90,
        BusLineNumber = 8,
        Number_on_route = 1,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 91,
        BusLineNumber = 8,
        Number_on_route = 2,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 92,
        BusLineNumber = 8,
        Number_on_route = 3,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 93,
        BusLineNumber = 8,
        Number_on_route = 4,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 94,
        BusLineNumber = 8,
        Number_on_route = 5,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 95,
        BusLineNumber = 8,
        Number_on_route = 6,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 97,
        BusLineNumber = 8,
        Number_on_route = 7,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 96,
        BusLineNumber = 8,
        Number_on_route = 8,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 83,
        BusLineNumber = 8,
        Number_on_route = 9,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 85,
        BusLineNumber = 8,
        Number_on_route = 10,
        Exists = true
    });
    #endregion
    #region line number 9
    Line_stations.Add(new BusLineStation {
        StationID = 91,
        BusLineNumber = 9,
        Number_on_route = 1,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 93,
        BusLineNumber = 9,
        Number_on_route = 2,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 84,
        BusLineNumber = 9,
        Number_on_route = 3,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 85,
        BusLineNumber = 9,
        Number_on_route = 4,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 86,
        BusLineNumber = 9,
        Number_on_route = 5,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 1488,
        BusLineNumber = 9,
        Number_on_route = 6,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 1490,
        BusLineNumber = 9,
        Number_on_route = 7,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 1510,
        BusLineNumber = 9,
        Number_on_route = 8,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 1491,
        BusLineNumber = 9,
        Number_on_route = 9,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 1493,
        BusLineNumber = 9,
        Number_on_route = 10,
        Exists = true
    });
    #endregion
    #region line number 10
    Line_stations.Add(new BusLineStation {
        StationID = 1491,
        BusLineNumber = 10,
        Number_on_route = 1,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 1494,
        BusLineNumber = 10,
        Number_on_route = 2,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 78,
        BusLineNumber = 10,
        Number_on_route = 3,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 123,
        BusLineNumber = 10,
        Number_on_route = 4,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 119,
        BusLineNumber = 10,
        Number_on_route = 5,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 84,
        BusLineNumber = 10,
        Number_on_route = 6,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 78,
        BusLineNumber = 10,
        Number_on_route = 7,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 115,
        BusLineNumber = 10,
        Number_on_route = 8,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 117,
        BusLineNumber = 10,
        Number_on_route = 9,
        Exists = true
    });
    Line_stations.Add(new BusLineStation {
        StationID = 119,
        BusLineNumber = 10,
        Number_on_route = 10,
        Exists = true
    });
    #endregion
}