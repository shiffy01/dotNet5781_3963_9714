using DLAPI;
using DO;
using DS;
using DL;
using System;
using System.Collections.Generic;
using System.Linq;
//DELETE BONUS????
namespace DL
{
    public sealed class DalObject : IDAL
    {
        #region singleton implementaion
        private readonly static IDAL dalInstance = new DalObject();

        private DalObject()
        {
           
           // DS.DataSource.initialize_buses();
            DS.DataSource.initialize_Stations();
            DS.DataSource.initialize_Lines();
            DS.DataSource.initialize_Bus_line_stations();
            DS.DataSource.initialize_two_adjacent_stations();
        }

        static DalObject()
        {
        }

        public static IDAL Instance
        {
        
            get => dalInstance;
        }

        #endregion singleton

        #region IDAL implementation

        // static Random rnd = new Random(DateTime.Now.Millisecond);
       // int BusLineRunningNumber=2000010;

        #region Bus implementation
        public void AddBus(Bus bus)
        {
            if (DataSource.Buses.FirstOrDefault(tmpBus => tmpBus.License ==bus.License&&tmpBus.Exists) != default(Bus))
            {
                throw new DuplicateBusException(bus.License, $", Bus with License number: {bus.License} already exists in the system"); 
            }
            DataSource.Buses.Add(bus.Clone());
        }//done
        public void UpdateBus(Bus bus)
        {
           
              Bus realBus= DataSource.Buses.FirstOrDefault(tmpBus => tmpBus.License== bus.License&&tmpBus.Exists);

            if (realBus!= default(Bus))
            {
                DataSource.Buses.Remove(realBus);
                DataSource.Buses.Add(bus.Clone());
            }
            else
                throw new BusNotFoundException("Bus wasn't found in the system");
        }//done
        public void DeleteBus(int license)
        {

            Bus realBus = DataSource.Buses.FirstOrDefault(tmpBus => tmpBus.License == license&&tmpBus.Exists);

            if (realBus != default(Bus))
            {
               
                    realBus.Exists = false;
                   
            }
            else
                throw new BusNotFoundException("Bus wasn't found in the system");
        }//done
        public Bus GetBus(int license)
        {
           Bus sameBus= DataSource.Buses.FirstOrDefault(tmpBus => tmpBus.License == license&&tmpBus.Exists);

            if (sameBus!= default(Bus))
                return sameBus.Clone();
            else
                throw new BusNotFoundException("Bus wasn't found in the system");
        } //done
        public IEnumerable<Bus> GetAllBusses()
        {
            return 
                from bus in DataSource.Buses
                where(bus.Exists)
                   select bus.Clone();
        }//done!!
        public IEnumerable<Bus> GetAllBussesBy(Predicate<Bus> predicate)
        {
            return from bus in DataSource.Buses
                   where (predicate(bus)&&bus.Exists)
                   select bus.Clone();
        }    //Done!!

        #endregion

        #region BusLine implementation
        public BusLine AddBusLine(int line_number, bool inter_city, string dest, string org, DateTime first, DateTime last, TimeSpan freq)
        {
            bool exists =
               DataSource.Lines.Any(p => p.Exists == true && p.BusID == line_number);
            if (exists)
                throw new BusLineAlreadyExistsException();//does it need to say something inside?

            BusLine newBus = new BusLine {
                BusID = DS.Config.BusLineCounter,
                Bus_line_number =line_number,
                InterCity=inter_city,
                Destination=dest,
                Origin=org,
                First_bus=first,
                Last_bus=last,
                Frequency=freq,
                Exists=true
            };
            

            //dont need to clone bec i built it here
            DataSource.Lines.Add(newBus);
            return newBus;
        }
        public  void DeleteBusLine(int busID)
        {
            BusLine bus = DataSource.Lines.FirstOrDefault(b => (b.BusID == busID && b.Exists));

            if (bus != default(BusLine))
            {
                
                    bus.Exists = false;
            }
            else
                throw new DO.BusLineNotFoundException("The BusLine is not found in the system");
        }
        public void UpdateBusLine(BusLine busLine)
        {
            DO.BusLine bus = DataSource.Lines.FirstOrDefault(b => (b.BusID == busLine.BusID && b.Exists));

            if (bus != default(BusLine))
            {
                DataSource.Lines.Remove(bus);
                DataSource.Lines.Add(busLine.Clone());
            }
            else
                throw new DO.BusLineNotFoundException("Bus line is not in the system");
        }
        public  IEnumerable<BusLine> GetAllBuslines()
        {
            var list =
             from bus in DataSource.Lines
             where (bus.Exists)
             select (bus.Clone());
            return list;

        }
        public BusLine GetBusLine(int busID)
        {
            BusLine bus = DataSource.Lines.FirstOrDefault(b => (b.BusID == busID && b.Exists));

            if (bus != default(BusLine))
                return bus.Clone();
            else
                throw new DO.BusLineNotFoundException("Bus line is not in the system");
        }
        public IEnumerable<BusLine> GetAllBusLinesBy(Predicate<BusLine> predicate)
        {
            return from line in DataSource.Lines
                   where (predicate(line)&&line.Exists)
                   select line.Clone();
        }   //done!!
        public IEnumerable<BusLine> GetBuslinesOfStation(int stationID)//gets all the bus lines with this station on the route
        {
            var list =
             from station in DataSource.Line_stations
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
        #endregion

        #region BusLineStation CRUD 
        public void AddBusLineStation(int station_id, int line_id, int number_on_route)
        {
            if (DataSource.Line_stations.FirstOrDefault(b => (b.BusLineStationID == (station_id.ToString()+line_id.ToString()) && b.Exists)) != default(BusLineStation))
                throw new DO.BusLineStationAlreadyExistsException("This bus line station is already in the system");
           
                DataSource.Line_stations.Add(new BusLineStation {
                    StationID=station_id,
                    LineID=line_id,
                    BusLineStationID = (station_id.ToString()+line_id.ToString()),                   
                    Number_on_route=number_on_route,
                    Exists=true
                });
        }
        public void UpdateBusLineStation(BusLineStation busLineStation)
        {
            DO.BusLineStation station = DataSource.Line_stations.FirstOrDefault(s => (s.BusLineStationID == busLineStation.BusLineStationID && s.Exists));

            if (station != default(BusLineStation))
            {
                DataSource.Line_stations.Remove(station);
                DataSource.Line_stations.Add(busLineStation.Clone());
            }
            else
                throw new DO.BusLineStationNotFoundException(busLineStation.BusLineStationID, $"ID number: {busLineStation.LineID} doesn't stop at this station");
        }
        public void DeleteBusLineStation(string ID)
        {
            DO.BusLineStation station = DataSource.Line_stations.FirstOrDefault(s => (s.BusLineStationID == ID && s.Exists));

            if (station != default(BusLineStation))
            {
                station.Exists = false;
               
            }
            else
                throw new DO.BusLineStationNotFoundException(station.BusLineStationID, "The BusLineStation is not found in the system");
        }
        public BusLineStation GetBusLineStation(string ID)
        {
            BusLineStation station = DataSource.Line_stations.FirstOrDefault(s => (s.BusLineStationID == ID && s.Exists));

            if (station!= default(BusLineStation))
                return station.Clone();
            else
                throw new BusLineStationNotFoundException(ID, "Bus line station is not in the system");
        }
        public IEnumerable<BusLineStation> GetAllBusLineStations()
        {
            var list =
            from station in DataSource.Line_stations
            where (station.Exists)
            select (station.Clone());
            return list;
        }
        public IEnumerable<BusLineStation> GetAllBusLineStationsBy(Predicate<BusLineStation> predicate)
        {
            return from busStation in DataSource.Line_stations          
                   where predicate(busStation)&&busStation.Exists
                   orderby busStation.Number_on_route
                   select busStation.Clone();
        }
        #endregion   

        #region  BusStation implementation
        public void AddBusStation(int code, double latitude, double longitude, string name, string address, string city)
        {
            if (DataSource.Stations.FirstOrDefault(tmpBusStation => tmpBusStation.Code == code&&tmpBusStation.Exists) !=default(BusStation))
            {
                throw new StationAlreadyExistsException(code, $", Bus with License number: {code} already exists in the system");

            }
           
            DataSource.Stations.Add(new BusStation {
                Code=code,
                Latitude=latitude,
                Longitude=longitude,
                Name=name,
                Address=address,
                City=city,
                Exists=true
            });
        }//done!!
        public void UpdateBusStation(BusStation busStation)
        {

            BusStation findBusStation = DataSource.Stations.FirstOrDefault(tmpBusStation => tmpBusStation.Code == busStation.Code&&tmpBusStation.Exists);

            if (findBusStation != default(BusStation))
            {
                DataSource.Stations.Remove(findBusStation);
                DataSource.Stations.Add(busStation.Clone());
            }
            else
                throw new StationNotFoundException(busStation.Code, $"Station :{busStation.Code} wasn't found in the system");
        }//done!!
        public void DeleteBusStation(int code)
        {

            BusStation busStation = DataSource.Stations.FirstOrDefault(tmpBusStation => tmpBusStation.Code==code&&tmpBusStation.Exists);

            if (busStation != default(BusStation))
            {
               
                    busStation.Exists = false;
                  
            }
            else
                throw new StationNotFoundException(busStation.Code, $"Station :{code} wasn't found in the system");
        }//done!!
        public BusStation GetBusStation(int code)
        {
            BusStation findBusStation = DataSource.Stations.Find(tmpBusStation => tmpBusStation.Code == code&&tmpBusStation.Exists);

            if (findBusStation != default(BusStation))
                return findBusStation.Clone();
            else
                throw new StationNotFoundException(code, $"Station :{code} wasn't found in the system");
        }///done!!
        public IEnumerable<BusStation> GetAllBusStations()
        {
            return
                from BusStation in DataSource.Stations
                where (BusStation.Exists)
                orderby BusStation.Code
                select BusStation.Clone();
        }//done!!
        public IEnumerable<BusStation> GetAllBusStationsBy(Predicate<BusStation> predicate)
        {
            return from busStation in DataSource.Stations
                   where predicate(busStation)&&busStation.Exists
                   select busStation.Clone();
        }   //done!!
        #endregion
               
        #region AdjacentStations  implementation
        public  void AddAdjacentStations(int code_1, int code_2, double distance, TimeSpan drive_time)
        {
            if (DataSource.Two_stops.FirstOrDefault(tmpTwo_stops => (tmpTwo_stops.PairID == (code_1.ToString()+code_2.ToString()))) != default(AdjacentStations))
            {
                throw new PairAlreadyExitsException(" Pair already exists in the system");
            }

            DataSource.Two_stops.Add(new AdjacentStations {
                Stop_1_code = code_1,
                Stop_2_code = code_2,
                PairID = (code_1.ToString() + code_2.ToString()),
                Distance = distance,
                Average_drive_time = drive_time,
               
            }) ;
        }//done!!
        public void UpdateAdjacentStations(AdjacentStations twoConsecutiveStops)
        {

            AdjacentStations findTwoStops = DataSource.Two_stops.FirstOrDefault(tmpTwo_stops => tmpTwo_stops.PairID == twoConsecutiveStops.PairID);

            if (findTwoStops != default(AdjacentStations))
            {
                DataSource.Two_stops.Remove(findTwoStops);
                DataSource.Two_stops.Add(twoConsecutiveStops.Clone());
            }
            else
                throw new PairNotFoundException("Pair not found in system");
        }//done!!
        public void DeleteAdjacentStations(string pairID)
        {
            AdjacentStations findTwoStops = DataSource.Two_stops.FirstOrDefault(tmpTwo_stops => tmpTwo_stops.PairID == pairID);
            if (findTwoStops != default(AdjacentStations))
            {
                DataSource.Two_stops.Remove(findTwoStops);
            }
            else
                throw new PairNotFoundException("Pair not found in system");
        }//done!!
        public AdjacentStations GetAdjacentStations(string pairID)
        {
            string id = pairID;
            AdjacentStations findTwoStops = DataSource.Two_stops.FirstOrDefault(tmpTwo_stops => tmpTwo_stops.PairID == id);
            if (findTwoStops != default(AdjacentStations))
                return findTwoStops.Clone();
            else
                throw new PairNotFoundException(pairID+"  Pair not found in system");
        }///done!! 
        public IEnumerable<AdjacentStations> GetAllPairs()
        {
            return from pair in DataSource.Two_stops
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

    

        #endregion


    }
}


