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
            BusLine bb=bl.GetBusLine(2000000);
            Console.WriteLine(bb.ToString());
            List<StationOnTheLine> list2 = bb.Stations.ToList();
            for (int j = 0; j < list2.Count; j++)
                Console.WriteLine(list2[j].ToString());
         
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
                 Console.WriteLine(bl.GetBusLine(2002).BusID);
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
            Console.WriteLine("ALL THE BUS LINE FUNCTIONS WORK. THE ONLY THINGS LEFT TO CHECK ARE:");

            Console.WriteLine("WHEN IT RETURNS DISTANCES FOR THE USER TO ENTER, AND IF IT CAN ACCESS STATIONS ON A LINE THAT IS ONE OF THE LINES OF A STATION WE HAVE");
            Console.WriteLine("NEED TO MAKE SURE EVERYTHING IN BUS STATION IS ALSO HERE");
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
