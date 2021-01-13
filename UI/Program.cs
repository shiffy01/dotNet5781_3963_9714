using System;
using BlApi;
using BO;
using System.Collections.Generic;
using System.Linq;


namespace PlConsole
{
    class Program
    {
        static IBL bl;
        static Random rnd = new Random(DateTime.Now.Millisecond);
        static void Main(string[] args)
        {
            bl = BlFactory.GetBl();
            void PrintLines(List<BO.BusLine> lines1)
            {

                for (int i = 0; i < lines1.Count; i++)
                {
                    Console.WriteLine(lines1[i].ToString());
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

            #region create and print lists
            List<BO.BusLine> busLines = bl.GetAllBusLines().ToList();
            List<BO.BusStation> busStations = bl.GetAllBusStations().ToList();
            Console.WriteLine("All Bus Lines:");
            PrintLines(busLines);
            Console.WriteLine("All Bus Stations:");
            PrintStations(busStations);
            #endregion
            //*********************

            #region ADD NEW LINE AND PRINT
            List<int> stationsOnLine = new List<int>();
            stationsOnLine.Add(73);
            stationsOnLine.Add(76);
            stationsOnLine.Add(77);
            List<String> neededDistances = new List<string>();
            try
            {
               neededDistances = bl.AddBusLine(35, stationsOnLine, new DateTime(2021, 1, 1, 8, 0, 0), new DateTime(2021, 1, 1, 22, 0, 0), new TimeSpan(1, 0, 0));
            }
            catch (FrequencyConflictException ex)
            {

                Console.WriteLine(ex.Message);
            }
            catch (BusLineAlreadyExistsException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (StationNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            List<BO.StationOnTheLine> stationsToAdd = new List<BO.StationOnTheLine>();
            neededDistances.ForEach(AddDistance);
            void AddDistance(string pairId)
            {
                //get both station codes, choose rand distance, and create a bus station w the distance and the staion code, and add to stationsToAd
            }
            BusLine tmpLine=new BusLine();
            #region create tmpline
            tmpLine.Bus_line_number = 35;
            tmpLine.Destination = bl.GetBusLine(35).Destination;
            tmpLine.BusID = bl.GetBusLine(35).BusID;
            tmpLine.First_bus = bl.GetBusLine(35).First_bus;
            tmpLine.Frequency = bl.GetBusLine(35).Frequency;
            tmpLine.InterCity = bl.GetBusLine(35).InterCity;
            tmpLine.Last_bus = bl.GetBusLine(35).Last_bus;
            tmpLine.Origin = bl.GetBusLine(35).Origin;
            tmpLine.Stations = stationsToAdd;
            #endregion
            bl.UpdateBusLine(tmpLine);
            Console.WriteLine("All Bus Lines:");
            PrintLines(busLines);

            #endregion//implement addDistance 

            //*********************


            #region Delete line
            bl.DeleteBusLine(32);
            bl.GetBusLine(32);
            #endregion
            //******************

            #region GET  BISLINES BY
            List<BusLine> interCityLines = (bl.GetBusLineBy(line => line.InterCity == true)).ToList();
            PrintLines(interCityLines);
            #endregion//NOT DONE!!





            #region BUS STATIONS
            bl.UpdateBusStation(89, "נחל דולב ב");
            #region ADDSTATION
                
            bl.AddBusStation(4321, 4.764, 50.43, "מרכז מסחרי", "נחל שורק 55", "בית שמש");
            #endregion
            #region gets
            Console.WriteLine("testing addBUSstation, and getBusStation "+bl.GetBusStation(4321));
            try
            {
                Console.WriteLine("testing getBusStationError: " + bl.GetBusStation(9876));
            }
            catch (StationNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("testing  getBusStationBy ");
            List<BusStation> lines= (bl.GetBusStationBy(station => (station.City=="ירושלים"))).ToList();
            #endregion
            #endregion
            Console.WriteLine("TESTING REMOVE STATION FROM LIST");
            bl.RemoveBusStationFromLine(77, 32);
            BusLine bline = bl.GetBusLine(32);
            List<StationOnTheLine> stationlist = bline.Stations.ToList();
            for (int i = 0; i< stationlist.Count; i++)
            {
                Console.WriteLine("station number"+i+":"+stationlist[i]);
            }

                    



        }
    }
}
