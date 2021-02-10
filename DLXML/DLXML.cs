﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DLAPI;
using DO;


namespace DL
{
    //should fix some delete functions that are written badly, then do update bus in bl
    sealed class DLXML:IDAL
    {
        #region singleton implementaion
        private readonly static IDAL dalInstance = new DLXML();

        private DLXML()
        {

           
        }

        static DLXML()
        {
        }

        public static IDAL Instance
        {

            get => dalInstance;
        }

        #endregion singleton

        #region XML FILES
        string busLinePath = @"busLineXml.xml";
        string busStationPath = @"busStationXml.xml";
        string busPath = @"busXml.xml";
        string adjacentStationsPath = @"twoConsecutiveStopsXml.xml";
        string busLineStationPath = @"busLineStationXml.xml";
        string badBusStationsPath = @"stops.xml";
        string userPath=@"userXml.xml";
        string stationSearchHistoryPath = @"stationSearchHistoryXml.xml";
        string routeSearchHistoryPath = @"routeSearchHistoryXml.xml";
        string lineSearchHistoryPath = @"lineSearchHistoryXml.xml";
        string lineFrequencyPath=@"lineFrequencyXml.xml";
        #endregion


        public void CreateStationsList()//get list of all the stations in the country from stops.xml,
                                    //convert them to our station type and save to new xml
        {
            XElement busStationsRootElem = XMLtools.LoadListFromXMLElement(busStationPath);
            if (busStationsRootElem.Elements().Count() == 0)
            {
                XElement badBusStationsRootElem = XMLtools.LoadListFromXMLElement(badBusStationsPath);
                IEnumerable<BusStation> stops = from stop in badBusStationsRootElem.Elements()
                                                       select new BusStation() {
                                                           Code = int.Parse(stop.Element("Code").Value),
                                                           Name= stop.Element("Name").Value,
                                                           Latitude=double.Parse(stop.Element("Latitude").Value),
                                                           Longitude = double.Parse(stop.Element("Longitude").Value),
                                                           Address = stop.Element("Address").Value  
                                                       };
                busStationsRootElem.Add(stops);
            XMLtools.SaveListToXMLElement(busStationsRootElem, busStationPath);
            }
           
        }
      

        #region Bus implementation
        public int AddBus(bool access, bool wifi)
        {
            XElement BusRootElem = XMLtools.LoadListFromXMLElement(busPath);
            var biggest_license = from b in BusRootElem.Elements()
                                  where b.Element("Exists").Value == "true"
                                  select int.Parse(b.Element("License").Value);
            biggest_license.OrderByDescending(b => b);
            int newLicense = biggest_license.First()+1;


            XElement busElem = new XElement("Bus", new XElement("License", newLicense),
                                  new XElement("Status", Bus.Status_ops.Ready),
                                  new XElement("StartYear", DateTime.Now.Year),
                                  new XElement("StartMonth", DateTime.Now.Month),
                                  new XElement("StartDay", DateTime.Now.Day),
                                  new XElement("TuneYear", DateTime.Now.Year),
                                  new XElement("TuneMonth", DateTime.Now.Month),
                                  new XElement("TuneDay", DateTime.Now.Day),
                                  new XElement("TotalKiloMeterage", 0),
                                  new XElement("KiloMeterage", 0),
                                  new XElement("Gas", 1200),
                                  new XElement("Accessible", access),
                                  new XElement("Wifi", wifi),
                                  new XElement("Exists", true)
                                );

            BusRootElem.Add(busElem);
            XMLtools.SaveListToXMLElement(BusRootElem, busPath);
            return newLicense;
        }//done
        public void UpdateBus(int license, bool access, bool wifi)
        {
            XElement linesRootElem = XMLtools.LoadListFromXMLElement(busPath);

            XElement line = (from l in linesRootElem.Elements()
                             where int.Parse(l.Element("License").Value) == license && bool.Parse(l.Element("Exists").Value)
                             select l).FirstOrDefault();

            if (line != null)
            {
                line.Element("Access").Value = access + "";
                line.Element("Wifi").Value = wifi + "";


                XMLtools.SaveListToXMLElement(linesRootElem, busPath);
            }
            else
                throw new BusNotFoundException("This bus is not in the system");
        }
        public void UpdateBus(int license, Bus.Status_ops status, DateTime last_tune_up, int kilometerage, int totalkilometerage, int gas)
        {
            XElement linesRootElem = XMLtools.LoadListFromXMLElement(busPath);

            XElement line = (from l in linesRootElem.Elements()
                             where int.Parse(l.Element("License").Value) == license && bool.Parse(l.Element("Exists").Value)
                             select l).FirstOrDefault();

            if (line != null)
            {
                line.Element("Status").Value = ((int)status + "");
                line.Element("TuneYear").Value = last_tune_up.Year + "";
                line.Element("TuneMonth").Value = last_tune_up.Month + "";
                line.Element("TotalKiloMeterage").Value = kilometerage + "";
                line.Element("TotalKiloMeterage").Value = totalkilometerage + "";
                line.Element("Gas").Value = gas + "";

                XMLtools.SaveListToXMLElement(linesRootElem, busPath);
            }
            else
                throw new BusNotFoundException("This bus is not in the system");
        }
        public void DeleteBus(int license)
        {
            XElement busRootElem = XMLtools.LoadListFromXMLElement(busPath);

            XElement bus = (from b in busRootElem.Elements()
                                 where int.Parse(b.Element("License").Value) == license
                                 select b).FirstOrDefault();

            if (bus != null)
            {
                XElement newbusElem = new XElement("Bus", new XElement("License", bus.Element("License").Value),
                                  new XElement("StartYear", bus.Element("StartYear").Value),
                                  new XElement("StartMonth", bus.Element("StartMonth").Value),
                                  new XElement("StartDay", bus.Element("StartDay").Value),
                                  new XElement("TuneYear", bus.Element("TuneYear").Value),
                                  new XElement("TuneMonth", bus.Element("TuneMonth").Value),
                                  new XElement("TuneDay", bus.Element("TuneDay").Value),
                                  new XElement("TotalKiloMeterage", bus.Element("TotalKiloMeterage").Value),
                                  new XElement("KiloMeterage", bus.Element("KiloMeterage").Value),
                                  new XElement("Gas", bus.Element("Gas").Value),
                                  new XElement("Accessible", bus.Element("Accessible").Value),
                                  new XElement("Wifi", bus.Element("Wifi").Value),
                                  new XElement("Exists", false)
                                );
                bus.Remove();
                busRootElem.Add(newbusElem);
                XMLtools.SaveListToXMLElement(busRootElem, busPath);
            }
            else
                throw new BusNotFoundException("This bus is not in the system");
        }//done, with exists=false
        public Bus GetBus(int license)
        {
            XElement busRoot = XMLtools.LoadListFromXMLElement(busPath);
            Bus bus = (from b in busRoot.Elements()
                                      where int.Parse(b.Element("License").Value) == license&& bool.Parse(b.Element("Exists").Value)
                       select new Bus() {
                                         License=int.Parse(b.Element("License").Value),
                                         Status=(Bus.Status_ops)int.Parse(b.Element("Status").Value),//can it convert int to enum?
                                         StartDate=new DateTime(int.Parse(b.Element("StartYear").Value), int.Parse(b.Element("StartMonth").Value), int.Parse(b.Element("StartDay").Value)),
                                         Last_tune_up= new DateTime(int.Parse(b.Element("TuneYear").Value), int.Parse(b.Element("TuneMonth").Value), int.Parse(b.Element("TuneDay").Value)),
                                         Totalkilometerage=int.Parse(b.Element("TotalKiloMeterage").Value),
                                         Kilometerage=int.Parse(b.Element("KiloMeterage").Value),
                                         Gas=int.Parse(b.Element("Gas").Value),
                                         IsAccessible = bool.Parse(b.Element("Accessible").Value),
                                         HasWifi =bool.Parse(b.Element("Wifi").Value),
                                         Exists= bool.Parse(b.Element("Exists").Value)
                                      }).FirstOrDefault();
            if (bus == null)
                throw new BusNotFoundException("This bus is not in the system");
            return bus;
        } //done
        public IEnumerable<Bus> GetAllBuses()
        {
            XElement busesRoot = XMLtools.LoadListFromXMLElement(busPath);
            IEnumerable<Bus> buses = (from b in busesRoot.Elements()
                                      where bool.Parse(b.Element("Exists").Value)
                                                   select new Bus() {
                                                       License = int.Parse(b.Element("License").Value),
                                                       Status = (Bus.Status_ops)int.Parse(b.Element("Status").Value),//can it convert int to enum?
                                                       StartDate = new DateTime(int.Parse(b.Element("StartYear").Value), int.Parse(b.Element("StartMonth").Value), int.Parse(b.Element("StartDay").Value)),
                                                       Last_tune_up = new DateTime(int.Parse(b.Element("TuneYear").Value), int.Parse(b.Element("TuneMonth").Value), int.Parse(b.Element("TuneDay").Value)),
                                                       Totalkilometerage = int.Parse(b.Element("TotalKiloMeterage").Value),
                                                       Kilometerage = int.Parse(b.Element("KiloMeterage").Value),
                                                       Gas = int.Parse(b.Element("Gas").Value),
                                                       IsAccessible = bool.Parse(b.Element("Accessible").Value),
                                                       HasWifi = bool.Parse(b.Element("Wifi").Value),
                                                       Exists = bool.Parse(b.Element("Exists").Value)
                                                   });
            return buses;
        }//done!!
        public IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate)
        {
            return from bus in GetAllBuses()
                   where predicate(bus)
                   select bus;
        }    //Done!!

        #endregion

       
        #region BusLine implementation finished!
        public BusLine AddBusLine(int line_number, string dest, string org)
        {
            IEnumerable<BusLine> ListLines = XMLtools.LoadListFromXMLSerializer<BusLine>(busLinePath).OrderBy(line => line.BusID);
            List<BusLine> list = ListLines.ToList();

            bool exists =
               ListLines.Any(p => p.Exists == true && p.BusID == line_number);
            if (exists)
                throw new BusLineAlreadyExistsException();//does it need to say something inside?

            BusLine newBus = new BusLine {
                BusID = ++list[list.Count()].BusID,
                Bus_line_number = line_number,
                Destination = dest,
                Origin = org,  
                Exists = true
            };

            list.Add(newBus);
            XMLtools.SaveListToXMLSerializer(list, busLinePath);
            return newBus;
        }
        public void DeleteBusLine(int busID)
        {
            List<BusLine> ListLines = XMLtools.LoadListFromXMLSerializer<BusLine>(busLinePath);
            BusLine line = ListLines.FirstOrDefault(b => (b.BusID == busID && b.Exists));

            if (line != default(BusLine))
            {
                line.Exists = false;
                XMLtools.SaveListToXMLSerializer(ListLines, busLinePath);
            }
            else
                throw new DO.BusLineNotFoundException("The BusLine is not found in the system");
        }
        public void UpdateBusLine(BusLine busLine)
        {
            List<BusLine> ListLines = XMLtools.LoadListFromXMLSerializer<BusLine>(busLinePath);
            DO.BusLine line = ListLines.FirstOrDefault(b => (b.BusID == busLine.BusID && b.Exists));

            if (line != default(BusLine))
            {  
                ListLines.Remove(line);
                ListLines.Add(busLine);
                XMLtools.SaveListToXMLSerializer(ListLines, busLinePath);
            }
            else
                throw new DO.BusLineNotFoundException("Bus line is not in the system");
        }
        public IEnumerable<BusLine> GetAllBuslines()
        {
            List<BusLine> ListLines = XMLtools.LoadListFromXMLSerializer<BusLine>(busLinePath);
            return from line in ListLines
                   where line.Exists
                   select line;
        }
        public BusLine GetBusLine(int busID)
        {
            List<BusLine> ListLines = XMLtools.LoadListFromXMLSerializer<BusLine>(busLinePath);
            BusLine line = ListLines.FirstOrDefault();
            if (line != default(BusLine))
                return line;
            else
                throw new BusLineNotFoundException("Bus line is not in the system");
        }
        public IEnumerable<BusLine> GetAllBusLinesBy(Predicate<BusLine> predicate)
        {
            List<BusLine> ListLines = XMLtools.LoadListFromXMLSerializer<BusLine>(busLinePath);
            return from line in ListLines
                   where line.Exists && predicate(line)
                   select line;
        }

        //public IEnumerable<BusLine> GetBuslinesOfStation(int stationID)//gets all the bus lines with this station on the route
        //{
        //    List<BusLine> ListLines = XMLtools.LoadListFromXMLSerializer<BusLine>(busLinePath);
        //    List<BusLineStation> bus_line_station_list = XMLtools.LoadListFromXMLSerializer<BusLineStation>(busLineStationPath);
        //    var list =
        //     from station in bus_line_station_list
        //     where (station.Exists && station.StationID == stationID)
        //     select (station.LineID);
        //    List<BusLine> returnList = new List<BusLine>();
        //    try
        //    {
        //        foreach (var item in list)
        //        {
        //            returnList.Add(GetBusLine(item));

        //        }
        //    }
        //    catch (BusLineNotFoundException ex)
        //    {
        //        throw ex;
        //    }
        //    return returnList;
        //}

        #endregion //finished! 

        #region LineFrequency
        void AddLineFrequency(int lineID, DateTime start, TimeSpan frequency, DateTime end)
        {
            XElement FrequencyRootElem = XMLtools.LoadListFromXMLElement(lineFrequencyPath);
   
            XElement freqElem = new XElement("LineFrequency", new XElement("ID", lineID+start.Hour.ToString()+start.Minute.ToString()),
                                  new XElement("LineID", lineID),
                                  new XElement("StartHour", start.Hour),
                                  new XElement("StartMinute", start.Minute),
                                  new XElement("EndHour", end.Hour),
                                  new XElement("EndMinute", end.Minute),
                                  new XElement("FrequencyHours", frequency.Hours),
                                  new XElement("FrequencyMinutes", frequency.Minutes),
                                  new XElement("Exists", true)
                                );

            FrequencyRootElem.Add(freqElem);
            XMLtools.SaveListToXMLElement(FrequencyRootElem, lineFrequencyPath);  
        }
        void UpdateLineFrequency(LineFrequency frequency)
        {
            XElement frequencyRootElem = XMLtools.LoadListFromXMLElement(lineFrequencyPath);

            XElement freq = (from l in frequencyRootElem.Elements()
                             where l.Element("ID").Value== frequency.ID && bool.Parse(l.Element("Exists").Value)
                             select l).FirstOrDefault();

            if (freq != null)
            {
               
                freq.Element("StartHour").Value = frequency.Start.Hour + "";
                freq.Element("StartMinute").Value = frequency.Start.Minute + "";
                freq.Element("EndHour").Value = frequency.End.Hour + "";
                freq.Element("EndMinute").Value = frequency.End.Minute + "";
                freq.Element("FrequencyHours").Value = frequency.Frequency.Hours + "";
                freq.Element("FrequencyMinutes").Value = frequency.Frequency.Minutes + "";



                XMLtools.SaveListToXMLElement(frequencyRootElem, busPath);
            }
            else
                throw new BusNotFoundException("This bus is not in the system");
        }
        void DeleteLineFrequency(string id);
        IEnumerable<LineFrequency> GetAllLineFrequency();
        LineFrequency GetLineFrequency(string id);
        IEnumerable<LineFrequency> GetAllLineFrequencyBy(Predicate<LineFrequency> predicate);
        #endregion

        #region BusLineStation CRUD finished!
        public void AddBusLineStation(int station_id, int line_id, int number_on_route)
        {
            List<BusLineStation> ListLineStations = XMLtools.LoadListFromXMLSerializer<BusLineStation>(busLineStationPath);
            if (ListLineStations.FirstOrDefault(b => (b.BusLineStationID == (station_id.ToString() + line_id.ToString()) && b.Exists)) != default(BusLineStation))
                throw new DO.BusLineStationAlreadyExistsException("This bus line station is already in the system");

            ListLineStations.Add(new BusLineStation {
                StationID = station_id,
                LineID = line_id,
                BusLineStationID = (station_id.ToString() + line_id.ToString()),
                Number_on_route = number_on_route,
                Exists = true
            });
            XMLtools.SaveListToXMLSerializer(ListLineStations, busLineStationPath);
        }
        public void UpdateBusLineStation(BusLineStation busLineStation)
        {
            List<BusLineStation> ListLineStations = XMLtools.LoadListFromXMLSerializer<BusLineStation>(busLineStationPath);
            BusLineStation station = ListLineStations.FirstOrDefault(s => (s.BusLineStationID == busLineStation.BusLineStationID && s.Exists));

            if (station != default(BusLineStation))
            {
                //dont need add remove ?
                ListLineStations.Remove(station);
                ListLineStations.Add(busLineStation);
                XMLtools.SaveListToXMLSerializer(ListLineStations, busLineStationPath);
            }
            else
                throw new DO.BusLineStationNotFoundException(busLineStation.BusLineStationID, $"ID number: {busLineStation.LineID} doesn't stop at this station");
        }
        public void DeleteBusLineStation(string ID)
        {
            List<BusLineStation> ListLineStations = XMLtools.LoadListFromXMLSerializer<BusLineStation>(busLineStationPath);
            DO.BusLineStation station = ListLineStations.FirstOrDefault(s => (s.BusLineStationID == ID && s.Exists));

            if (station != default(BusLineStation))
            {
                station.Exists = false;
                XMLtools.SaveListToXMLSerializer(ListLineStations, busLineStationPath);
            }
            else
                throw new DO.BusLineStationNotFoundException(station.BusLineStationID, "The BusLineStation is not found in the system");
        }
        public BusLineStation GetBusLineStation(string ID)
        {
            List<BusLineStation> ListLineStations = XMLtools.LoadListFromXMLSerializer<BusLineStation>(busLineStationPath);
            BusLineStation station = ListLineStations.FirstOrDefault(s => (s.BusLineStationID == ID && s.Exists));

            if (station != default(BusLineStation))
                return station;
            else
                throw new BusLineStationNotFoundException(ID, "Bus line station is not in the system");
        }
        public IEnumerable<BusLineStation> GetAllBusLineStations()
        {
            List<BusLineStation> ListLineStations = XMLtools.LoadListFromXMLSerializer<BusLineStation>(busLineStationPath);
            var list =
            from station in ListLineStations
            where (station.Exists)
            select (station);
            return list;
        }
        public IEnumerable<BusLineStation> GetAllBusLineStationsBy(Predicate<BusLineStation> predicate)
        {
            List<BusLineStation> ListLineStations = XMLtools.LoadListFromXMLSerializer<BusLineStation>(busLineStationPath);
            return from busStation in ListLineStations
                   where predicate(busStation) && busStation.Exists
                   orderby busStation.Number_on_route
                   select busStation;
        }
        #endregion   

        #region  BusStation implementation finished!
        public void AddBusStation(int code, double latitude, double longitude, string name, string address)
        {
            List<BusStation> ListStations = XMLtools.LoadListFromXMLSerializer<BusStation>(busStationPath);
            if (ListStations.FirstOrDefault(tmpBusStation => tmpBusStation.Code == code) != default(BusStation))
            {
                throw new StationAlreadyExistsException(code, $", Bus with License number: {code} already exists in the system");

            }

            ListStations.Add(new BusStation {
                Code = code,
                Latitude = latitude,
                Longitude = longitude,
                Name = name,
                Address = address,
             
               
            });
            XMLtools.SaveListToXMLSerializer(ListStations, busStationPath);
        }//done!!
        public void UpdateBusStation(BusStation busStation)
        {
            List<BusStation> ListStations = XMLtools.LoadListFromXMLSerializer<BusStation>(busStationPath);
            BusStation findBusStation = ListStations.FirstOrDefault(tmpBusStation => tmpBusStation.Code == busStation.Code);

            if (findBusStation != default(BusStation))
            {
                //dont need add/remove but leaving it anyway... easier this way
                ListStations.Remove(findBusStation);
                ListStations.Add(busStation);
                XMLtools.SaveListToXMLSerializer(ListStations, busStationPath);
            }
            else
                throw new StationNotFoundException(busStation.Code, $"Station :{busStation.Code} wasn't found in the system");
        }//done!!
        public void DeleteBusStation(int code)
        {
            List<BusStation> ListStations = XMLtools.LoadListFromXMLSerializer<BusStation>(busStationPath);
            BusStation busStation = ListStations.FirstOrDefault(tmpBusStation => tmpBusStation.Code == code );

            if (busStation != default(BusStation))
            {

             
                XMLtools.SaveListToXMLSerializer(ListStations, busStationPath);
            }
            else
                throw new StationNotFoundException(busStation.Code, $"Station :{code} wasn't found in the system");
        }//done!!
        public BusStation GetBusStation(int code)
        {
            List<BusStation> ListStations = XMLtools.LoadListFromXMLSerializer<BusStation>(busStationPath);
            BusStation findBusStation = ListStations.Find(tmpBusStation => tmpBusStation.Code == code);

            if (findBusStation != default(BusStation))
                return findBusStation;
            else
                throw new StationNotFoundException(code, $"Station :{code} wasn't found in the system");
        }///done!!
        public IEnumerable<BusStation> GetAllBusStations()
        {
            List<BusStation> ListStations = XMLtools.LoadListFromXMLSerializer<BusStation>(busStationPath);
            return
                from BusStation in ListStations
                orderby BusStation.Code
                select BusStation;
        }//done!!
        public IEnumerable<BusStation> GetAllBusStationsBy(Predicate<BusStation> predicate)
        {
            List<BusStation> ListStations = XMLtools.LoadListFromXMLSerializer<BusStation>(busStationPath);
            return from busStation in ListStations
                   where predicate(busStation)
                   select busStation;
        }   //done!!

        #endregion

        #region AdjacentStations  implementation
        public void AddAdjacentStations(int code_1, int code_2, double distance, TimeSpan drive_time)
        {

            XElement StationsRootElem = XMLtools.LoadListFromXMLElement(adjacentStationsPath);

            XElement station1 = (from s in StationsRootElem.Elements()
                             where s.Element("PairID").Value == code_1.ToString()+code_2.ToString()
                             select s).FirstOrDefault();

            if (station1 != null)
                throw new PairAlreadyExitsException(" Pair already exists in the system");

            XElement stationElem = new XElement("AdjacentStations", new XElement("PairID", code_1.ToString() + code_2.ToString()),
                                  new XElement("Stop_1_code", code_1),
                                  new XElement("Stop_2_code", code_2),
                                  new XElement("Distance", distance),
                                  new XElement("Average_drive_time", (int)drive_time.TotalMinutes)
                                );

            StationsRootElem.Add(stationElem);

            XMLtools.SaveListToXMLElement(StationsRootElem, adjacentStationsPath);
        }//done!!
        public void UpdateAdjacentStations(AdjacentStations adjacentStations)
        {

            XElement stationsRootElem = XMLtools.LoadListFromXMLElement(adjacentStationsPath);

            XElement stations = (from s in stationsRootElem.Elements()
                            where s.Element("PairID").Value== adjacentStations.PairID
                            select s).FirstOrDefault();

            if (stations != null)
            {
                stations.Element("PairID").Value = adjacentStations.PairID;
                stations.Element("Stop_1_code").Value = adjacentStations.Stop_1_code.ToString();
                stations.Element("Stop_2_code").Value = adjacentStations.Stop_2_code.ToString();
                stations.Element("Distance").Value = adjacentStations.Distance.ToString();
                stations.Element("Average_drive_time").Value = ((int)(adjacentStations.Average_drive_time.TotalMinutes)).ToString();

                XMLtools.SaveListToXMLElement(stationsRootElem, adjacentStationsPath);
            }
            else
                throw new PairNotFoundException("Pair not found in system");
        }//done!!
        public void DeleteAdjacentStations(string pairID)
        {
            XElement stationsRootElem = XMLtools.LoadListFromXMLElement(adjacentStationsPath);

            XElement stations = (from s in stationsRootElem.Elements()
                            where s.Element("PairID").Value == pairID
                            select s).FirstOrDefault();

            if (stations != null)
            {
                stations.Remove(); 

                XMLtools.SaveListToXMLElement(stationsRootElem, adjacentStationsPath);
            }
            else
                throw new PairNotFoundException("Pair not found in system");
        }//done!!
        public AdjacentStations GetAdjacentStations(string pairID)
        {
           
            XElement stopsRoot = XMLtools.LoadListFromXMLElement(adjacentStationsPath);
            AdjacentStations stops = (from stop in stopsRoot.Elements()
                                      where stop.Element("PairID").Value == pairID
                                      select new AdjacentStations() {
                                          PairID=stop.Element("PairID").Value,
                                          Stop_1_code=int.Parse(stop.Element("Stop_1_code").Value),
                                          Stop_2_code = int.Parse(stop.Element("Stop_2_code").Value),
                                          Distance=double.Parse(stop.Element("Distance").Value),
                                          Average_drive_time = new TimeSpan((int.Parse(stop.Element("Average_drive_time").Value) / 60), (int.Parse(stop.Element("Average_drive_time").Value) % 60), 0)

                                      }).FirstOrDefault();
            if(stops==null)
                throw new PairNotFoundException(pairID + "  Pair not found in system");
            return stops;
        }///done!! 
        public IEnumerable<AdjacentStations> GetAllPairs()
        {
            XElement stopsRoot = XMLtools.LoadListFromXMLElement(adjacentStationsPath);
            IEnumerable<AdjacentStations> stops = (from stop in stopsRoot.Elements()
                                      select new AdjacentStations() {
                                          PairID = stop.Element("PairID").Value,
                                          Stop_1_code = int.Parse(stop.Element("Stop_1_code").Value),
                                          Stop_2_code = int.Parse(stop.Element("Stop_2_code").Value),
                                          Distance = double.Parse(stop.Element("Distance").Value),
                                         Average_drive_time=new TimeSpan((int.Parse(stop.Element("Average_drive_time").Value)/60), (int.Parse(stop.Element("Average_drive_time").Value) % 60), 0)

                                      });
            return stops;
            
        }   //done!! 
        public bool AdjacentStationsExists(string pairID)
        {
            try
            {
                GetAdjacentStations(pairID);
                return true;
            }
            catch (PairNotFoundException)
            {
                return false;
            }
        }//done
        #endregion

        #region User inplementation
        public void AddUser(string userName, string password, bool manager)
        {
            List<User> userList = XMLtools.LoadListFromXMLSerializer<User>(userPath);
            if (userList.FirstOrDefault(u => (u.UserName == (userName) && u.Exists)) != default(User))
                throw new UserNameAlreadyExistsException("There is already a user with the same name");
        
            userList.Add(new User {
               UserName=userName,
               Password=password,
               IsManager=manager,
                Exists = true
            });
            XMLtools.SaveListToXMLSerializer(userList, userPath);
        }
        public void UpdateUser(User user)
        {
            List<User> userList = XMLtools.LoadListFromXMLSerializer<User>(userPath);
            User oldUser = userList.FirstOrDefault(s => (s.UserName == user.UserName && s.Exists&&s.Password==user.Password));

            if (oldUser != default(User))
            {
            
                userList.Remove(oldUser);
                userList.Add(user);
                XMLtools.SaveListToXMLSerializer(userList, userPath);
            }
            else
                throw new DO.UserDoesNotExistException("This user is not in the system");
        }
        public void DeleteUser(string userName, string password)
        {
            List<User> userList = XMLtools.LoadListFromXMLSerializer<User>(userPath);
            User oldUser = userList.FirstOrDefault(s => (s.UserName == userName && s.Exists && s.Password == password));

            if (oldUser != default(User))
            {
                oldUser.Exists = false;
                XMLtools.SaveListToXMLSerializer(userList, userPath);
            }
            else
                throw new UserDoesNotExistException("Cannot delete a user that does not exist");
        }
        public IEnumerable<User> GetAllUsers()
        {
            List<User> userList = XMLtools.LoadListFromXMLSerializer<User>(userPath);
            return
            from user in userList
            where (user.Exists)
            select (user);
          
        }
        public User GetUser(string userName, string password)
        {
            List<User> userList = XMLtools.LoadListFromXMLSerializer<User>(userPath);
            User user = userList.FirstOrDefault(u => (u.UserName == userName && u.Exists&&u.Password==password));

            if (user != default(User))
                return user;
            throw new UserDoesNotExistException("This user with this password is not in the system");
        }
        public IEnumerable<User> GetAllUsersBy(Predicate<User> predicate)
        {
            List<User> userList = XMLtools.LoadListFromXMLSerializer<User>(userPath);
            return from user in userList
                   where predicate(user) && user.Exists
                   select user;
        }
        #endregion

        #region StationSearchHistory implementation
        void AddStationSearchHistory(string userName, int code, bool starred, string nickname)
        {
            List<StationSearchHistory> historyList = XMLtools.LoadListFromXMLSerializer<StationSearchHistory>(stationSearchHistoryPath);
            if (historyList.FirstOrDefault(u => (u.ID==userName+code.ToString()&& u.Exists)) != default(StationSearchHistory))
                throw new StationSearchHistoryAlreadyExistsException("This search is already in the system");
            historyList = (historyList.OrderBy(h => h.SearchIndex)).ToList();

            historyList.Add(new StationSearchHistory {
                ID=userName+code.ToString(),
                UserName = userName,
                StationCode = code,
                IsStarred = starred,
                NickName = nickname,
                SearchIndex = historyList[historyList.Count()].SearchIndex+1,
                Exists = true
            }) ;
            XMLtools.SaveListToXMLSerializer(historyList, stationSearchHistoryPath);
        }
        void UpdateStationSearchHistory(StationSearchHistory search)
        {
            List<StationSearchHistory> historyList = XMLtools.LoadListFromXMLSerializer<StationSearchHistory>(stationSearchHistoryPath);
            StationSearchHistory oldSearch = historyList.FirstOrDefault(s => (s.ID == search.ID && s.Exists));

            if (oldSearch != default(StationSearchHistory))
            {

                historyList.Remove(oldSearch);
                historyList.Add(search);
                XMLtools.SaveListToXMLSerializer(historyList, stationSearchHistoryPath);
            }
            else
                throw new StationSearchDoesNotExistException("This search is not in the system");
        }
        void DeleteStationSearchHistory(string id)
        {
            List<StationSearchHistory> historyList = XMLtools.LoadListFromXMLSerializer<StationSearchHistory>(stationSearchHistoryPath);
            StationSearchHistory oldSearch = historyList.FirstOrDefault(s => (s.ID == id && s.Exists));

            if (oldSearch != default(StationSearchHistory))
            {
                oldSearch.Exists = false;
                XMLtools.SaveListToXMLSerializer(historyList, stationSearchHistoryPath);
            }
            else
                throw new StationSearchDoesNotExistException("Cannot delete a search that does not exist");
        }
        IEnumerable<StationSearchHistory> GetAllStationSearchHistory()
        {
            List<StationSearchHistory> historyList = XMLtools.LoadListFromXMLSerializer<StationSearchHistory>(stationSearchHistoryPath);
            return
            from search in historyList
            where search.Exists
            select search;
        }
        StationSearchHistory GetStationSearchHistory(string id)
        {
            List<StationSearchHistory> historyList = XMLtools.LoadListFromXMLSerializer<StationSearchHistory>(stationSearchHistoryPath);
            StationSearchHistory search = historyList.FirstOrDefault(s => (s.ID == id && s.Exists));

            if (search != default(StationSearchHistory))
                return search;
            throw new StationSearchDoesNotExistException("This search is not saved in the system");
        }
        IEnumerable<StationSearchHistory> GetAllStationSearchHistoryBy(Predicate<StationSearchHistory> predicate)
        {
            List<StationSearchHistory> historyList = XMLtools.LoadListFromXMLSerializer<StationSearchHistory>(stationSearchHistoryPath);
            return from search in historyList
                   where predicate(search) && search.Exists
                   select search;
        }
        #endregion

        #region RouteSearchHistory implementation
        void AddRouteSearchHistory(string userName, int code1, int code2, bool starred, string nickname)
        {
            List<RouteSearchHistory> historyList = XMLtools.LoadListFromXMLSerializer<RouteSearchHistory>(routeSearchHistoryPath);
            if (historyList.FirstOrDefault(u => (u.ID == userName + code1.ToString()+code2.ToString() && u.Exists)) != default(RouteSearchHistory))
                throw new RouteSearchHistoryAlreadyExistsException("This search is already in the system");
            historyList = (historyList.OrderBy(h => h.SearchIndex)).ToList();

            historyList.Add(new RouteSearchHistory {
                ID = userName + code1.ToString()+code2.ToString(),
                UserName = userName,
                Station1Code=code1,
                Station2Code=code2,
                IsStarred = starred,
                NickName = nickname,
                SearchIndex = historyList[historyList.Count()].SearchIndex + 1,
                Exists = true
            });
            XMLtools.SaveListToXMLSerializer(historyList, stationSearchHistoryPath);
        }
        void UpdateRouteSearchHistory(RouteSearchHistory search)
        {
            List<RouteSearchHistory> historyList = XMLtools.LoadListFromXMLSerializer<RouteSearchHistory>(routeSearchHistoryPath);
            RouteSearchHistory oldSearch = historyList.FirstOrDefault(s => (s.ID == search.ID && s.Exists));

            if (oldSearch != default(RouteSearchHistory))
            {

                historyList.Remove(oldSearch);
                historyList.Add(search);
                XMLtools.SaveListToXMLSerializer(historyList, routeSearchHistoryPath);
            }
            else
                throw new RouteSearchDoesNotExistException("This search is not in the system");
        }
        void DeleteRouteSearchHistory(string id)
        {
            List<RouteSearchHistory> historyList = XMLtools.LoadListFromXMLSerializer<RouteSearchHistory>(routeSearchHistoryPath);
            RouteSearchHistory oldSearch = historyList.FirstOrDefault(s => (s.ID == id && s.Exists));

            if (oldSearch != default(RouteSearchHistory))
            {
                oldSearch.Exists = false;
                XMLtools.SaveListToXMLSerializer(historyList, routeSearchHistoryPath);
            }
            else
                throw new RouteSearchDoesNotExistException("Cannot delete a search that does not exist");
        }
        IEnumerable<RouteSearchHistory> GetAllRouteSearchHistory()
        {
            List<RouteSearchHistory> historyList = XMLtools.LoadListFromXMLSerializer<RouteSearchHistory>(routeSearchHistoryPath);
            return
            from search in historyList
            where search.Exists
            select search;
        }
        RouteSearchHistory GetRouteSearchHistory(string id)
        {
            List<RouteSearchHistory> historyList = XMLtools.LoadListFromXMLSerializer<RouteSearchHistory>(routeSearchHistoryPath);
            RouteSearchHistory search = historyList.FirstOrDefault(s => (s.ID == id && s.Exists));

            if (search != default(RouteSearchHistory))
                return search;
            throw new RouteSearchDoesNotExistException("This search is not saved in the system");
        }
        IEnumerable<RouteSearchHistory> GetAllRouteSearchHistoryBy(Predicate<RouteSearchHistory> predicate)
        {
            List<RouteSearchHistory> historyList = XMLtools.LoadListFromXMLSerializer<RouteSearchHistory>(routeSearchHistoryPath);
            return from search in historyList
                   where predicate(search) && search.Exists
                   select search;
        }
        #endregion

        #region LineSearchHistory implementation
        void AddLineSearchHistory(string userName, int code, bool starred, string nickname)
        {
            List<LineSearchHistory> historyList = XMLtools.LoadListFromXMLSerializer<LineSearchHistory>(lineSearchHistoryPath);
            if (historyList.FirstOrDefault(u => (u.ID == userName + code.ToString() && u.Exists)) != default(LineSearchHistory))
                throw new LineSearchHistoryAlreadyExistsException("This search is already in the system");
            historyList = (historyList.OrderBy(h => h.SearchIndex)).ToList();

            historyList.Add(new LineSearchHistory {
                ID = userName + code.ToString(),
                UserName = userName,
                LineCode = code,
                IsStarred = starred,
                NickName = nickname,
                SearchIndex = historyList[historyList.Count()].SearchIndex + 1,
                Exists = true
            });
            XMLtools.SaveListToXMLSerializer(historyList, lineSearchHistoryPath);
        }
        void UpdateLineSearchHistory(LineSearchHistory search)
        {
            List<LineSearchHistory> historyList = XMLtools.LoadListFromXMLSerializer<LineSearchHistory>(lineSearchHistoryPath);
            LineSearchHistory oldSearch = historyList.FirstOrDefault(s => (s.ID == search.ID && s.Exists));

            if (oldSearch != default(LineSearchHistory))
            {

                historyList.Remove(oldSearch);
                historyList.Add(search);
                XMLtools.SaveListToXMLSerializer(historyList, lineSearchHistoryPath);
            }
            else
                throw new LineSearchDoesNotExistException("This search is not in the system");
        }
        void DeleteLineSearchHistory(string id)
        {
            List<LineSearchHistory> historyList = XMLtools.LoadListFromXMLSerializer<LineSearchHistory>(lineSearchHistoryPath);
            LineSearchHistory oldSearch = historyList.FirstOrDefault(s => (s.ID == id && s.Exists));

            if (oldSearch != default(LineSearchHistory))
            {
                oldSearch.Exists = false;
                XMLtools.SaveListToXMLSerializer(historyList, lineSearchHistoryPath);
            }
            else
                throw new LineSearchDoesNotExistException("Cannot delete a search that does not exist");
        }
        IEnumerable<LineSearchHistory> GetAllLineSearchHistory()
        {
            List<LineSearchHistory> historyList = XMLtools.LoadListFromXMLSerializer<LineSearchHistory>(lineSearchHistoryPath);
            return
            from search in historyList
            where search.Exists
            select search;
        }
        LineSearchHistory GetLineSearchHistory(string id)
        {
            List<LineSearchHistory> historyList = XMLtools.LoadListFromXMLSerializer<LineSearchHistory>(lineSearchHistoryPath);
            LineSearchHistory search = historyList.FirstOrDefault(s => (s.ID == id && s.Exists));

            if (search != default(LineSearchHistory))
                return search;
            throw new LineSearchDoesNotExistException("This search is not saved in the system");
        }
        IEnumerable<LineSearchHistory> GetAllLineSearchHistoryBy(Predicate<LineSearchHistory> predicate)
        {
            List<LineSearchHistory> historyList = XMLtools.LoadListFromXMLSerializer<LineSearchHistory>(lineSearchHistoryPath);
            return from search in historyList
                   where predicate(search) && search.Exists
                   select search;
        }
        #endregion
    }
}
