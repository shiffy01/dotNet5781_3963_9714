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
                    List<StationOnTheLine> list = lines1[i].Stations.ToList();
                    Console.WriteLine("stops:");
                    for (int j = 0; j < list.Count(); j++)
                    {
                        Console.WriteLine(list[j].ToString());
                    }
                    Console.WriteLine("               * * * *   ");
                }
            }

            void PrintStations(List<BO.BusStation> stations)
            {

                for (int i = 0; i < stations.Count; i++)
                {
                    Console.WriteLine(stations[i].ToString());
                    Console.WriteLine("               * * * *   ");
                    Console.WriteLine("lines:");
                    List<BusLine> list = stations[i].Lines.ToList();
                    for (int j = 0; j < list.Count; j++)
                        Console.WriteLine(list[j].ToString());
                }
            }

            #region create and print lists
            //try
            //{
            //    List<BO.BusLine> busLines = bl.GetAllBusLines().ToList();
            //    List<BO.BusStation> busStations = bl.GetAllBusStations().ToList();
            //    Console.WriteLine("All Bus Lines:");
            //    PrintLines(busLines);
            //    Console.WriteLine("All Bus Stations:");
            //    PrintStations(busStations);
            //}
            //catch (PairNotFoundException ex)
            //{
            //    Console.WriteLine(ex.InnerException.Message);
            //}
            Console.WriteLine("WORKS TILL HERE I CHECKED, EXCEPT THE RUNNING NUMBERS DONT WORK AND DESTINATION AND ORIGIN");
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
                 Console.WriteLine(bl.GetBusLine(2000011).BusID);
                List<BO.BusLine> busLines = bl.GetAllBusLines().ToList();
                PrintLines(busLines);
            }
            catch (BusLineNotFoundException ex)
            {
                Console.WriteLine(ex.Message); 
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
            int code1=0, code2=0;
            for (int i = 0; i < neededDistances.Count; i++)
            {
                string[] codes = neededDistances[i].Split('*');
                try
                {
                     code1= Int32.Parse(codes[0]);
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }
                try
                {
                     code2 = Int32.Parse(codes[1]);
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }
                bl.AddTwoConsecutiveStops(code1, code2, rnd.Next(), new TimeSpan(6, 0, 0));
            }


            #endregion
            Console.WriteLine("WORKS TILL HERE I CHECKED");

            #region update line, and getline
            try
            {
                bl.UpdateBusLine(new DateTime(2021, 1, 1, 6, 0, 0), new DateTime(2021, 1, 1, 12, 0, 0), new TimeSpan(1, 0, 0), 2000000);
            }
            catch (BO.FrequencyConflictException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (BO.BusLineNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("WORKS TILL HERE I CHECKED, btw all this with valid input so check after with wrong things to make sure the exceptions work");
            try
            {
                Console.WriteLine("Updated line:" + bl.GetBusLine(2000000).ToString());
            }
            catch (BusLineNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (PairNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            #endregion//implement addDistance 

            //*********************

            Console.WriteLine("WORKS TILL HERE I CHECKED");
            #region Delete line
            try
            {
                bl.GetBusLine(2000000);
                bl.DeleteBusLine(2000000);
                bl.GetBusLine(2000000);//should throw exception
            }
            catch (BusLineNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            #endregion
            //******************
            Console.WriteLine("WORKS TILL HERE I CHECKED");
            #region GET  BuSLINES BY
            try
            {
                List<BusLine> interCityLines = (bl.GetBusLineBy(line => line.InterCity == true)).ToList();
                PrintLines(interCityLines);
            }         
            catch (PairNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            #endregion//NOT DONE!!
            Console.WriteLine("check city in bus station cuz it doesnt show up when printing bus stations");
            Console.WriteLine("also check if when printing a station it can print the lines and the stations for each line");
            Console.WriteLine("otherwise works till here");


            #region BUS STATIONS
            try
            {
                bl.UpdateBusStation(89, "נחל דולב ב");
            }
            catch (StationNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            #region ADDSTATION
            try
            {
                bl.AddBusStation(4321, 4.764, 50.43, "מרכז מסחרי", "נחל שורק 55", "בית שמש");
            }
            catch (StationALreadyExistsException ex)
            {
                Console.WriteLine(ex.Message);
            }
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
            try
            {
                BusLine bline = bl.GetBusLine(2000002);

                List<StationOnTheLine> stationlist = bline.Stations.ToList();
                for (int i = 0; i < stationlist.Count; i++)
                {
                    Console.WriteLine("station number   " + i + ":   " + stationlist[i].Code);
                }
                bl.RemoveBusStationFromLine(91, 2000002);
                bl.RemoveBusStationFromLine(77, 32);
                
                BusLine bline2 = bl.GetBusLine(2000002);

                List<StationOnTheLine> stationlist2 = bline2.Stations.ToList();
                for (int i = 0; i < stationlist2.Count; i++)
                {
                    Console.WriteLine("station number   " + i + ":   " + stationlist2[i].Code);
                }
            }
            catch (StationNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (BusLineNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (PairNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();

        }
    }
}
