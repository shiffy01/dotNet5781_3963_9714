using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DLAPI;
using DO;


namespace DL
{
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
        #endregion
        #region Bus implementation finished
        public void AddBus(bool access, bool wifi)
        {
            IEnumerable<Bus> Listbus = XMLtools.LoadListFromXMLSerializer<Bus>(busPath).OrderBy(bus=>bus.License);
            List<Bus> list = Listbus.ToList();
            list.Add(new Bus {
                Status = Bus.Status_ops.Ready,
                License = ++list[list.Count()].License,
                StartDate = DateTime.Now,
                Last_tune_up = DateTime.Now,
                Totalkilometerage = 0,
                kilometerage = 0,
                Gas = 1200,
                IsAccessible = access,
                HasWifi = wifi,
                Exists = true

            });
            XMLtools.SaveListToXMLSerializer(Listbus, busPath);
        }//done
        public void UpdateBus(int license, bool access, bool wifi)
        {
            List<Bus> Listbus = XMLtools.LoadListFromXMLSerializer<Bus>(busPath);
            Bus realBus = Listbus.FirstOrDefault(tmpBus => tmpBus.License == license && tmpBus.Exists);

            if (realBus != default(Bus))
            {
                Bus newBus = new Bus {
                    Status = realBus.Status,
                    License = realBus.License,
                    StartDate = realBus.StartDate,
                    Last_tune_up = realBus.Last_tune_up,
                    Totalkilometerage = realBus.Totalkilometerage,
                    kilometerage = realBus.kilometerage,
                    Gas = realBus.Gas,
                    IsAccessible = access,
                    HasWifi = wifi,
                    Exists = true
                };
                Listbus.Remove(realBus);
                Listbus.Add(newBus);
                XMLtools.SaveListToXMLSerializer(Listbus, busPath);
            }
            else
                throw new BusNotFoundException("Bus wasn't found in the system");
        }//done
        public void DeleteBus(int license)
        {
            List<Bus> Listbus = XMLtools.LoadListFromXMLSerializer<Bus>(busPath);
            Bus realBus = Listbus.FirstOrDefault(tmpBus => tmpBus.License == license && tmpBus.Exists);

            if (realBus != default(Bus))
            {
                realBus.Exists = false;
                XMLtools.SaveListToXMLSerializer(Listbus, busPath);
            }
            else
                throw new BusNotFoundException("Bus wasn't found in the system");
        }//done
        public Bus GetBus(int license)
        {
            List<Bus> Listbus = XMLtools.LoadListFromXMLSerializer<Bus>(busPath);
            Bus sameBus = Listbus.FirstOrDefault(tmpBus => tmpBus.License == license && tmpBus.Exists);

            if (sameBus != default(Bus))
                return sameBus;
            else
                throw new BusNotFoundException("Bus wasn't found in the system");
        } //done
        public IEnumerable<Bus> GetAllBuses()
        {
            List<Bus> Listbus = XMLtools.LoadListFromXMLSerializer<Bus>(busPath);
            return
                from bus in Listbus
                where (bus.Exists)
                select bus;
        }//done!!
        public IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate)
        {
            List<Bus> Listbus = XMLtools.LoadListFromXMLSerializer<Bus>(busPath);
            return from bus in Listbus
                   where (predicate(bus) && bus.Exists)
                   select bus;
        }    //Done!!

        #endregion

        #region BusLine implementation finished!
        public BusLine AddBusLine(int line_number, string dest, string org, DateTime first, DateTime last, TimeSpan freq)
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
                First_bus = first,
                Last_bus = last,
                Frequency = freq,
                Exists = true
            };


            //dont need to clone bec i built it here
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
                //we dont really need the remove/add now... because it loads new copies from the file anyway 
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
                throw new DO.BusLineNotFoundException("Bus line is not in the system");
        }
        public IEnumerable<BusLine> GetAllBusLinesBy(Predicate<BusLine> predicate)
        {
            List<BusLine> ListLines = XMLtools.LoadListFromXMLSerializer<BusLine>(busLinePath);
            return from line in ListLines
                   where line.Exists&&predicate(line)
                   select line;
        }  
        public IEnumerable<BusLine> GetBuslinesOfStation(int stationID)//gets all the bus lines with this station on the route
        {
             List<BusLine> ListLines = XMLtools.LoadListFromXMLSerializer<BusLine>(busLinePath);
             List<BusLineStation> bus_line_station_list=XMLtools.LoadListFromXMLSerializer<BusLineStation>(busLineStationPath);
            var list =
             from station in bus_line_station_list
             where (station.Exists && station.StationID == stationID)
             select (station.LineID);
            List<BusLine> returnList = new List<BusLine>();
            try
            {
                foreach (var item in list)
                {
                    returnList.Add(GetBusLine(item));

                }
            }
            catch (BusLineNotFoundException ex)
            {
                throw ex;
            }
            return returnList;
        }
        #endregion //finished! 

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
        public void AddBusStation(int code, double latitude, double longitude, string name, string address, string city)
        {
            List<BusStation> ListStations = XMLtools.LoadListFromXMLSerializer<BusStation>(busStationPath);
            if (ListStations.FirstOrDefault(tmpBusStation => tmpBusStation.Code == code && tmpBusStation.Exists) != default(BusStation))
            {
                throw new StationAlreadyExistsException(code, $", Bus with License number: {code} already exists in the system");

            }

            ListStations.Add(new BusStation {
                Code = code,
                Latitude = latitude,
                Longitude = longitude,
                Name = name,
                Address = address,
                City = city,
                Exists = true
            });
            XMLtools.SaveListToXMLSerializer(ListStations, busStationPath);
        }//done!!
        public void UpdateBusStation(BusStation busStation)
        {
            List<BusStation> ListStations = XMLtools.LoadListFromXMLSerializer<BusStation>(busStationPath);
            BusStation findBusStation = ListStations.FirstOrDefault(tmpBusStation => tmpBusStation.Code == busStation.Code && tmpBusStation.Exists);

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
            BusStation busStation = ListStations.FirstOrDefault(tmpBusStation => tmpBusStation.Code == code && tmpBusStation.Exists);

            if (busStation != default(BusStation))
            {

                busStation.Exists = false;
                XMLtools.SaveListToXMLSerializer(ListStations, busStationPath);
            }
            else
                throw new StationNotFoundException(busStation.Code, $"Station :{code} wasn't found in the system");
        }//done!!
        public BusStation GetBusStation(int code)
        {
            List<BusStation> ListStations = XMLtools.LoadListFromXMLSerializer<BusStation>(busStationPath);
            BusStation findBusStation = ListStations.Find(tmpBusStation => tmpBusStation.Code == code && tmpBusStation.Exists);

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
                where (BusStation.Exists)
                orderby BusStation.Code
                select BusStation;
        }//done!!
        public IEnumerable<BusStation> GetAllBusStationsBy(Predicate<BusStation> predicate)
        {
            List<BusStation> ListStations = XMLtools.LoadListFromXMLSerializer<BusStation>(busStationPath);
            return from busStation in ListStations
                   where predicate(busStation) && busStation.Exists
                   select busStation;
        }   //done!!
        public IEnumerable<IGrouping<string, BusStation>> getStationsByCity()
        {
            List<BusStation> ListStations = XMLtools.LoadListFromXMLSerializer<BusStation>(busStationPath);
            var list =
                  from station in ListStations
                  where station.Exists
                  group station by station.City;
            return list;
        }
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
    }
}
