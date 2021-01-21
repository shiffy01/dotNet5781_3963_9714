using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            // DS.DataSource.initialize_buses();
            //DS.DataSource.initialize_Stations();
            //DS.DataSource.initialize_Lines();
            //DS.DataSource.initialize_Bus_line_stations();
            //DS.DataSource.initialize_two_consecutive_stations();
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
        string twoConsecutiveStopsPath = @"twoConsecutiveStopsXml.xml";
        string busLineStationPath = @"busLineStationXml.xml";
        #endregion
        #region Bus implementation
        public void AddBus(Bus bus)
        {
            if (DataSource.Buses.FirstOrDefault(tmpBus => tmpBus.License == bus.License && tmpBus.Exists) != default(Bus))
            {
                throw new DuplicateBusException(bus.License, $", Bus with License number: {bus.License} already exists in the system");
            }
            DataSource.Buses.Add(bus.Clone());
        }//done
        public void UpdateBus(Bus bus)
        {

            Bus realBus = DataSource.Buses.FirstOrDefault(tmpBus => tmpBus.License == bus.License && tmpBus.Exists);

            if (realBus != default(Bus))
            {
                DataSource.Buses.Remove(realBus);
                DataSource.Buses.Add(bus.Clone());
            }
            else
                throw new BusNotFoundException("Bus wasn't found in the system");
        }//done
        public void DeleteBus(int license)
        {

            Bus realBus = DataSource.Buses.FirstOrDefault(tmpBus => tmpBus.License == license && tmpBus.Exists);

            if (realBus != default(Bus))
            {

                realBus.Exists = false;

            }
            else
                throw new BusNotFoundException("Bus wasn't found in the system");
        }//done
        public Bus GetBus(int license)
        {
            Bus sameBus = DataSource.Buses.FirstOrDefault(tmpBus => tmpBus.License == license && tmpBus.Exists);

            if (sameBus != default(Bus))
                return sameBus.Clone();
            else
                throw new BusNotFoundException("Bus wasn't found in the system");
        } //done
        public IEnumerable<Bus> GetAllBusses()
        {
            return
                from bus in DataSource.Buses
                where (bus.Exists)
                select bus.Clone();
        }//done!!
        public IEnumerable<Bus> GetAllBussesBy(Predicate<Bus> predicate)
        {
            return from bus in DataSource.Buses
                   where (predicate(bus) && bus.Exists)
                   select bus.Clone();
        }    //Done!!

        #endregion

        #region BusLine implementation finished!
        public BusLine AddBusLine(int line_number, bool inter_city, string dest, string org, DateTime first, DateTime last, TimeSpan freq)
        {
            List<BusLine> ListLines = XMLtools.LoadListFromXMLSerializer<BusLine>(busLinePath);
            bool exists =
               ListLines.Any(p => p.Exists == true && p.BusID == line_number);
            if (exists)
                throw new BusLineAlreadyExistsException();//does it need to say something inside?

            BusLine newBus = new BusLine {
                BusID = DS.Config.BusLineCounter,
                Bus_line_number = line_number,
                InterCity = inter_city,
                Destination = dest,
                Origin = org,
                First_bus = first,
                Last_bus = last,
                Frequency = freq,
                Exists = true
            };


            //dont need to clone bec i built it here
            ListLines.Add(newBus);
            XMLtools.SaveListToXMLSerializer(ListLines, busLinePath);
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
        #endregion

        #region AdjacentStations  implementation
        public void AddAdjacentStations(int code_1, int code_2, double distance, TimeSpan drive_time)
        {
            if (DataSource.Two_stops.FirstOrDefault(tmpTwo_stops => (tmpTwo_stops.PairID == (code_1.ToString() + code_2.ToString())) && tmpTwo_stops.Exists) != default(AdjacentStations))
            {
                throw new PairAlreadyExitsException(" Pair already exists in the system");
            }

            DataSource.Two_stops.Add(new AdjacentStations {
                Stop_1_code = code_1,
                Stop_2_code = code_2,
                PairID = (code_1.ToString() + code_2.ToString()),
                Distance = distance,
                Average_drive_time = drive_time,
                Exists = true
            });
        }//done!!
        public void UpdateAdjacentStations(AdjacentStations AdjacentStations)
        {

            AdjacentStations findTwoStops = DataSource.Two_stops.FirstOrDefault(tmpTwo_stops => tmpTwo_stops.PairID == AdjacentStations.PairID && tmpTwo_stops.Exists);

            if (findTwoStops != default(AdjacentStations))
            {
                DataSource.Two_stops.Remove(findTwoStops);
                DataSource.Two_stops.Add(AdjacentStations.Clone());
            }
            else
                throw new PairNotFoundException("Pair not found in system");
        }//done!!
        public void DeleteAdjacentStations(string pairID)
        {
            AdjacentStations findTwoStops = DataSource.Two_stops.FirstOrDefault(tmpTwo_stops => tmpTwo_stops.PairID == pairID && tmpTwo_stops.Exists);
            if (findTwoStops != default(AdjacentStations))
            {

                findTwoStops.Exists = false;

            }
            else
                throw new PairNotFoundException("Pair not found in system");
        }//done!!
        public AdjacentStations GetAdjacentStations(string pairID)
        {
            string id = pairID;
            AdjacentStations findTwoStops = DataSource.Two_stops.FirstOrDefault(tmpTwo_stops => tmpTwo_stops.PairID == id && tmpTwo_stops.Exists);
            if (findTwoStops != default(AdjacentStations))
                return findTwoStops.Clone();
            else
                throw new PairNotFoundException(pairID + "  Pair not found in system");
        }///done!! 
        public IEnumerable<AdjacentStations> GetAllPairs()
        {
            return from pair in DataSource.Two_stops
                   where pair.Exists
                   select pair.Clone();
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
        }
        #endregion
    }
}
