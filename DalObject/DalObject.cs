using DLAPI;
using DO;
using DS;
using DL;
using System;
using System.Collections.Generic;
using System.Linq;
//DELETE BONUS????
namespace DAL
{
    public sealed class DalObject : IDAL
    {
        #region singleton implementaion
        private readonly static IDAL dalInstance = new DalObject();

        private DalObject()
        {
           
            DS.DataSource.initialize_buses();
            DS.DataSource.initialize_Stations();
            DS.DataSource.initialize_Lines();
            DS.DataSource.initialize_Bus_line_stations();
            DS.DataSource.initialize_two_consecutive_stations();
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
            if (DataSource.Buses.FirstOrDefault(tmpBus => tmpBus.License ==bus.License) != null)
            {
                throw new DuplicateBusException(bus.License, $", Bus with License number: {bus.License} already exists in the system"); 
            }
            DataSource.Buses.Add(bus.Clone());
        }//done
        public void UpdateBus(Bus bus)
        {
           
              Bus realBus= DataSource.Buses.FirstOrDefault(tmpBus => tmpBus.License== bus.License);

            if (realBus != null)
            {
                DataSource.Buses.Remove(realBus);
                DataSource.Buses.Add(bus.Clone());
            }
            else
                throw new BusNotFoundException("Bus wasn't found in the system");
        }//done
        public void DeleteBus(int license)
        {

            Bus realBus = DataSource.Buses.FirstOrDefault(tmpBus => tmpBus.License == license);

            if (realBus != null)
            {
                DataSource.Buses.Remove(realBus);
            }
            else
                throw new BusNotFoundException("Bus wasn't found in the system");
        }//done
        public Bus GetBus(int license)
        {
           Bus sameBus= DataSource.Buses.Find(tmpBus => tmpBus.License == license);

            if (sameBus != null)
                return sameBus.Clone();
            else
                throw new BusNotFoundException("Bus wasn't found in the system");
        } //done
        public IEnumerable<Bus> GetAllBusses()
        {
            return 
                from bus in DataSource.Buses
                   select bus.Clone();
        }//done!!
        public IEnumerable<Bus> GetAllBussesBy(Predicate<Bus> predicate)
        {
            return from bus in DataSource.Buses
                   where predicate(bus)
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
                BusID= BusLineRunningNumber,
                Bus_line_number =line_number,
                InterCity=inter_city,
                Destination=dest,
                Origin=org,
                First_bus=first,
                Last_bus=last,
                Frequency=freq,
                Exists=true
            };
            BusLineRunningNumber++;

            //dont need to clone bec i built it here
            DataSource.Lines.Add(newBus);
            return newBus;
        }
       public  void DeleteBusLine(int busID)
        {
            DO.BusLine bus = DataSource.Lines.Find(b => (b.BusID == busID && b.Exists));

            if (bus != null)
            {
                bus.Exists = false;
            }
            else
                throw new DO.BusLineNotFoundException("The BusLine is not found in the system");
        }
       public void UpdateBusLine(BusLine busLine)
        {
            DO.BusLine bus = DataSource.Lines.Find(b => (b.BusID == busLine.BusID && b.Exists));

            if (bus != null)
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
            DO.BusLine bus = DataSource.Lines.Find(b => (b.BusID == busID && b.Exists));

            if (bus != null)
                return bus.Clone();
            else
                throw new DO.BusLineNotFoundException("Bus line is not in the system");
        }
        public IEnumerable<BusLine> GetAllBusLinesBy(Predicate<BusLine> predicate)
        {
            return from line in DataSource.Lines
                   where predicate(line)
                   select line.Clone();
        }   //done!!
        public IEnumerable<BusLine> GetBuslinesOfStation(int stationID)//gets all the bus lines with this station on the route
        {
            //go through all the bus line stations and for each one select that number. then for each number in the for each select get bus line(number)
            //i'll comment this better tomorrow night
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
        public void AddBusLineStation(int station_id, int line_id, int bus_line_number, int number_on_route)
        {
            if (DataSource.Line_stations.FirstOrDefault(b => (b.StationID == station_id && b.Exists)) != null)
                throw new DO.BusLineStationAlreadyExistsException("This bus line station is already in the system");
           
                DataSource.Line_stations.Add(new BusLineStation {
                    StationID=station_id,
                    LineID=line_id,
                    pairID=(station_id.ToString()+line_id.ToString()),
                    BusLineNumber=bus_line_number,
                    Number_on_route=number_on_route,
                    Exists=true
                });
        }
        public void UpdateBusLineStation(BusLineStation busLineStation)
        {
            DO.BusLineStation station = DataSource.Line_stations.Find(s => (s.pairID== busLineStation.pairID && s.Exists));

            if (station != null)
            {
                DataSource.Line_stations.Remove(station);
                DataSource.Line_stations.Add(busLineStation.Clone());
            }
            else
                throw new DO.BusLineStationNotFoundException(busLineStation.BusLineNumber, $"Line number: {busLineStation.BusLineNumber} doesn't stop at this station");
        }
        public void DeleteBusLineStation(string pairID)
        {
            DO.BusLineStation station = DataSource.Line_stations.Find(s => (s.pairID==pairID && s.Exists));

            if (station != null)
            {
                station.Exists = false;
            }
            else
                throw new DO.BusLineStationNotFoundException(station.StationID, "The BusLineStation is not found in the system");
        }
        public BusLineStation GetBusLineStation(string pairID)
        {
            DO.BusLineStation station = DataSource.Line_stations.Find(s => (s.pairID==pairID && s.Exists));

            if (station != null)
                return station.Clone();
            else
                throw new DO.BusLineStationNotFoundException(station.StationID, "Bus line station is not in the system");
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
                   orderby busStation.Number_on_route
                   where predicate(busStation)
                   select busStation.Clone();
        }
        #endregion   

        #region  BusStation implementation
        public void AddBusStation(int code, double latitude, double longitude, string name, string address, string city)
        {
            if (DataSource.Stations.FirstOrDefault(tmpBusStation => tmpBusStation.Code == code) != null)
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
                //Exists=true
            });
        }//done!!
        public void UpdateBusStation(BusStation busStation)
        {

            BusStation findBusStation = DataSource.Stations.FirstOrDefault(tmpBusStation => tmpBusStation.Code == busStation.Code);

            if (findBusStation != null)
            {
                DataSource.Stations.Remove(findBusStation);
                DataSource.Stations.Add(findBusStation.Clone());
            }
            else
                throw new StationNotFoundException(busStation.Code, $"Station :{busStation.Code} wasn't found in the system");
        }//done!!
        public void DeleteBusStation(int code)
        {

            BusStation busStation = DataSource.Stations.FirstOrDefault(tmpBusStation => tmpBusStation.Code==code);

            if (busStation != null)
            {
                DataSource.Stations.Remove(busStation);
            }
            else
                throw new StationNotFoundException(busStation.Code, $"Station :{code} wasn't found in the system");
        }//done!!
        public BusStation GetBusStation(int code)
        {
            BusStation findBusStation = DataSource.Stations.Find(tmpBusStation => tmpBusStation.Code == code);

            if (findBusStation != null)
                return findBusStation.Clone();
            else
                throw new StationNotFoundException(code, $"Station :{code} wasn't found in the system");
        }///done!!
        public IEnumerable<BusStation> GetAllBusStations()
        {
            return
                from BusStation in DataSource.Stations
                select BusStation.Clone();
        }//done!!
        public IEnumerable<BusStation> GetAllBusStationsBy(Predicate<BusStation> predicate)
        {
            return from busStation in DataSource.Stations
                   where predicate(busStation)
                   select busStation.Clone();
        }   //done!!
        #endregion
               
        #region TwoConsecutiveStops  implementation
       public  void AddTwoConsecutiveStops(int code_1, int code_2, double distance, TimeSpan drive_time)
        {
            if (DataSource.Two_stops.FirstOrDefault(tmpTwo_stops => tmpTwo_stops.PairID == (code_1.ToString()+code_2.ToString())) != null)
            {
                throw new PairAlreadyExitsException(" Pair already exists in the system");
            }

            DataSource.Two_stops.Add(new TwoConsecutiveStops {
                Stop_1_code = code_1,
                Stop_2_code = code_2,
                PairID = (code_1.ToString() + code_2.ToString()),
                Distance = distance,
                Average_drive_time = drive_time//CHANGE THIS!!FIGURE OUT HOW TO CALCULATE AVERAGE DRIVE TIME, SHOULD BE DONE IN BL UNLESS ITS A SET NUMBER!!!!!!!!
            }) ;
        }//done!!
        public void UpdateTwoConsecutiveStops(TwoConsecutiveStops twoConsecutiveStops)
        {

            TwoConsecutiveStops findTwoStops = DataSource.Two_stops.FirstOrDefault(tmpTwo_stops => tmpTwo_stops.PairID == twoConsecutiveStops.PairID);

            if (findTwoStops != null)
            {
                DataSource.Two_stops.Remove(findTwoStops);
                DataSource.Two_stops.Add(findTwoStops.Clone());
            }
            else
                throw new PairNotFoundException("Pair not found in system");
        }//done!!
        public void DeleteTwoConsecutiveStops(string pairID)
        {
            TwoConsecutiveStops findTwoStops = DataSource.Two_stops.FirstOrDefault(tmpTwo_stops => tmpTwo_stops.PairID == pairID);
            if (findTwoStops != null)
            {
                DataSource.Two_stops.Remove(findTwoStops);
            }
            else
                throw new PairNotFoundException("Pair not found in system");
        }//done!!
        public TwoConsecutiveStops GetTwoConsecutiveStops(string pairID)
        {
            string id = pairID;
            TwoConsecutiveStops findTwoStops = DataSource.Two_stops.FirstOrDefault(tmpTwo_stops => tmpTwo_stops.PairID == id);
            if (findTwoStops != null)
                return findTwoStops.Clone();
            else
                throw new PairNotFoundException("Pair not found in system");
        }///done!! 
        public IEnumerable<TwoConsecutiveStops> GetAllPairs()
        {
            return from pair in DataSource.Two_stops
                   select pair.Clone();
        }   //done!!
        public bool TwoConsecutiveStopsExists(string pairID)
        {
            try
            {
                GetTwoConsecutiveStops(pairID);
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


