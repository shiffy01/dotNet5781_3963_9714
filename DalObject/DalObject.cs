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
            //try
          //  {
                DS.DataSource.initialize_buses();

          //  }
            //catch (BusException be)
            //{
            //    //TODO
            //}
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



        #region Bus implementation
        public bool AddBus(Bus bus)
        {
            if (DataSource.Busses.FirstOrDefault(tmpBus => tmpBus.License ==bus.License) != null)
            {
                throw new DuplicateBusException(bus.License, $", Bus with License number: {bus.License} already exists in the system");
                
            }

            DataSource.Busses.Add(bus.Clone());
            return true;
        }//done

        public bool UpdateBus(Bus bus)
        {
           
              Bus realBus= DataSource.Busses.FirstOrDefault(tmpBus => tmpBus.License== bus.License);

            if (realBus != null)
            {
                DataSource.Busses.Remove(realBus);
                DataSource.Busses.Add(bus.Clone());
            }
            else
                throw new BusNotFoundException("Bus wasn't found in the system");
            return true;
        }//done

        public bool DeleteBus(int license)
        {

            Bus realBus = DataSource.Buses.FirstOrDefault(tmpBus => tmpBus.License == license);

            if (realBus != null)
            {
                DataSource.Buses.Remove(realBus);
            }
            else
                throw new BusNotFoundException("Bus wasn't found in the system");
            return true;
        }//done
        public Bus GetBus(int license)
        {
           Bus sameBus= DataSource.Busses.Find(tmpBus => tmpBus.License == license);

            if (sameBus != null)
                return sameBus.Clone();
            else
                throw new BusNotFoundException("Bus wasn't found in the system");
        } //done
        public IEnumerable<Bus> GetAllBusses()
        {
            return 
                from bus in DataSource.Busses
                   select bus.Clone();
        }//done!!
        public IEnumerable<Bus> GetAllBussesBy(Predicate<Bus> predicate)
        {
            return from bus in DataSource.Busses
                   where predicate(bus)
                   select bus.Clone();
        }    //Done!!

        //public void UpdatePerson(int license, Action<Bus> update)
        //{
        //    throw new NotImplementedException();
        //}//figure out..

        #endregion

        #region BusLine implementation
       public int AddBusLine(BusLine busLine)
        {
            bool exists =
               DataSource.Lines.Any(p => p.Exists == true && p.BusID == busLine.BusID);
            if (exists)
                throw new BusLineAlreadyExistsException();//does it need to say something inside?

            BusLine b = DataSource.Lines.FirstOrDefault(e => e.BusID == busLine.BusID && e.Exists == false);
            if (b != null)
            {
                b.Exists = true;
                return b.BusID;
            }

            BusLine bus = Cloning.Clone(busLine);
            DataSource.Lines.Add(bus);
            return bus.BusID;
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
       public bool UpdateBusLine(BusLine busLine)
        {
            DO.BusLine bus = DataSource.Lines.Find(b => (b.BusID == busLine.BusID && b.Exists));

            if (bus != null)
            {
                DataSource.Lines.Remove(bus);
                DataSource.Lines.Add(busLine.Clone());
                return true;
            }
            else
                throw new DO.BusLineNotFoundException("Bus line is not in the system");
        }//there's something wrong with this. either it should be bool and return false if it cant update,
        //or it should throw an exception. not both. not sure which to do yet
       public  IEnumerable<BusLine> GetBuslines()
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
        public bool AddBusLineStation(BusLineStation busLineStation)
        {
            if (DataSource.Line_stations.FirstOrDefault(b => (b.StationID == busLineStation.StationID && b.Exists)) != null)
                throw new DO.BusLineStationAlreadyExistsException("This bus line station is already in the system");
            var station = DataSource.Line_stations.FirstOrDefault(b => (b.pairID == busLineStation.pairID && b.Exists == false));
            if (station != null)
                station.Exists = true;
            else
                DataSource.Line_stations.Add(busLineStation.Clone());
            return true;
        }
       public bool UpdateBusLineStation(BusLineStation busLineStation)
        {
            DO.BusLineStation station = DataSource.Line_stations.Find(s => (s.pairID== busLineStation.pairID && s.Exists));

            if (station != null)
            {
                DataSource.Line_stations.Remove(station);
                DataSource.Line_stations.Add(busLineStation.Clone());
                return true;
            }
            else
                throw new DO.BusLineStationNotFoundException(busLineStation.BusLineNumber, $"Line number: {busLineStation.BusLineNumber} doesn't stop at this station");
        }
     public   bool DeleteBusLineStation(string pairID)
        {
            DO.BusLineStation station = DataSource.Line_stations.Find(s => (s.pairID==pairID && s.Exists));

            if (station != null)
            {
                station.Exists = false;
                return true;
            }
            else
                throw new DO.BusLineStationNotFoundException(station.StationID, "The BusLineStation is not found in the system");
        }
        BusLineStation GetBusLineStation(string pairID)
        {
            DO.BusLineStation station = DataSource.Line_stations.Find(s => (s.pairID==pairID && s.Exists));

            if (station != null)
                return station.Clone();
            else
                throw new DO.BusLineStationNotFoundException(station.StationID, "Bus line station is not in the system");
        }
        IEnumerable<BusLineStation> GetBusLineStations()
        {
            var list =
            from station in DataSource.Line_stations
            where (station.Exists)
            select (station.Clone());
            return list;
        }
        #endregion   //need to check line too??
        //need to check line too??
        #region  BusStation implementation
        public bool AddBusStation(BusStation busStation)
        {
            if (DataSource.Stations.FirstOrDefault(tmpBusStation => tmpBusStation.Code == busStation.Code) != null)
            {
                throw new StationAlreadyExistsException(busStation.Code, $", Bus with License number: {busStation.Code} already exists in the system");

            }
           
            DataSource.Stations.Add(busStation.Clone());
            return true;
        }//done!!

        public bool UpdateBusStation(BusStation busStation)
        {

            BusStation findBusStation = DataSource.Stations.FirstOrDefault(tmpBusStation => tmpBusStation.Code == busStation.Code);

            if (findBusStation != null)
            {
                DataSource.Stations.Remove(findBusStation);
                DataSource.Stations.Add(findBusStation.Clone());
            }
            else
                throw new StationNotFoundException(busStation.Code, $"Station :{busStation.Code} wasn't found in the system");
            return true;
        }//done!!

        public bool DeleteBusStation(int code)
        {

            BusStation busStation = DataSource.Stations.FirstOrDefault(tmpBusStation => tmpBusStation.Code==code);

            if (busStation != null)
            {
                DataSource.Stations.Remove(busStation);
            }
            else
                throw new StationNotFoundException(busStation.Code, $"Station :{code} wasn't found in the system");
            return true;
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
      public  bool AddTwoConsecutiveStops(TwoConsecutiveStops twoConsecutiveStops)
        {
            if (DataSource.Two_stops.FirstOrDefault(tmpTwo_stops => tmpTwo_stops.PairID == twoConsecutiveStops.PairID) != null)
            {
                throw new PairAlreadyExitsException(" Pair already exists in the system");

            }
            string id = twoConsecutiveStops.Stop_1_code.ToString() + twoConsecutiveStops.Stop_2_code.ToString();

            twoConsecutiveStops.PairID = id;
            DataSource.Two_stops.Add(twoConsecutiveStops.Clone());
            return true;
        }//done!!

        public bool UpdateTwoConsecutiveStops(TwoConsecutiveStops twoConsecutiveStops)
        {

            TwoConsecutiveStops findTwoStops = DataSource.Two_stops.FirstOrDefault(tmpTwo_stops => tmpTwo_stops.PairID == twoConsecutiveStops.PairID);

            if (findTwoStops != null)
            {
                DataSource.Two_stops.Remove(findTwoStops);
                DataSource.Two_stops.Add(findTwoStops.Clone());
            }
            else
                throw new PairNotFoundException("Pair not found in system");
            return true;
        }//done!!

        public bool DeleteTwoConsecutiveStops(int stop1_code, int stop2_code)
        {
            string id = stop1_code.ToString() + stop2_code.ToString();
            TwoConsecutiveStops findTwoStops = DataSource.Two_stops.FirstOrDefault(tmpTwo_stops => tmpTwo_stops.PairID == id);


            if (findTwoStops != null)
            {
                DataSource.Two_stops.Remove(findTwoStops);
            }
            else
                throw new PairNotFoundException("Pair not found in system");
            return true;
        }//done!!
        public TwoConsecutiveStops GetTwoConsecutiveStops(int stop1_code, int stop2_code)
        {
            string id = stop1_code.ToString() + stop2_code.ToString();
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
        #endregion

        // #region BuLine implementation

        // public int AddBusLine(BusLine busLine)
        // {
        //     BusLine findLine = DataSource.Lines.FirstOrDefault(tmpBusLine => tmpBusLine.Bus_line_number == busLine.Bus_line_number);
        //     if (findLine != null)
        //         if(findLine.firstStation==busLine.firstStation && findLine.lastStation == busLine.lastStation)
        //              throw new BusLineAlreadyExistsException(" Bus line already exists in the system");

        //     busLine.BusID = DS.Config.BusDrivingCounter;
        //     DataSource.Lines.Add(busLine.Clone());
        //     return busLine.BusID;
        // }//done!!

        // public bool UpdateBusLine(BusLine busLine)
        // {
        //     BusLine findLine = DataSource.Lines.FirstOrDefault(tmpBusLine => tmpBusLine.Bus_line_number == busLine.Bus_line_number);
        //     if (findLine != null && findLine.firstStation == busLine.firstStation && findLine.lastStation == busLine.lastStation)
        //         {
        //             DataSource.Lines.Remove(findLine);
        //             DataSource.Lines.Add(busLine.Clone());
        //         }
        //         else
        //         throw new BusLineNotFoundException("Bus line wasn't found in the system");
        //     return true;
        // }//done//!!

        //public void DeleteBusLine(int busID)
        // {

        //     BusLine findLine = DataSource.Lines.FirstOrDefault((tmpBusLine => tmpBusLine.BusID== busID);

        //     if (findLine!= null)
        //     {
        //         DataSource.Lines.Remove(findLine);
        //     }
        //     else
        //         throw new BusLineNotFoundException("Bus line wasn't found in the system");

        // }//done!!

        // public IEnumerable<BusLine> GetAllBuslines()
        // {
        //     return
        //             from BusLine in DataSource.Lines
        //             select BusLine.Clone();
        // }//done!!
        // public BusLine GetBusLine(int busID)
        // {
        //     BusLine findLine = DataSource.Lines.FirstOrDefault(tmpLine => tmpLine.BusID == busID);

        //     if (findLine != null)
        //         return findLine.Clone();
        //     else
        //         throw new BusLineStationAlreadyExistsException(" Bus line already exists in the system");
        // }//done!!

        // IEnumerable<BusLine> GetBuslinesOfStation(int stationID)
        // {

        //     var lineList = from station in DataSource.Line_stations
        //                       where station.StationID == stationID
        //                       select station.Clone();
        //     return lineList;
        // }//done!!!

        // #endregion

        // #region BuLineStation implementation

        //     public bool AddBusLineStation(BusLineStation busLineStation)
        // {
        //     BusLineStation findLineStation= DataSource.Line_stations.FirstOrDefault(tmpBusLineStation => tmpBusLineStation.StationID == busLineStation.StationID&& tmpBusLineStation.LineID == busLineStation.LineID);
        //     if (findLineStation != null&& findLineStation.LineID==busLineStation.LineID)
        //             throw new BusLineStationAlreadyExistsException(" Bus line already exists at this station");
        //     DataSource.Line_stations.Add(findLineStation.Clone());
        //     return true;
        // }//update place ob route for all other stops on line

        // public bool UpdateBusLineStation(BusLineStation busLineStation)
        // {
        //     BusLineStation findLineStation = DataSource.Line_stations.FirstOrDefault(tmpBusLineStation => tmpBusLineStation.StationID == busLineStation.StationID);
        //     if (findLineStation != null && findLineStation.Bus_line_number == busLineStation.Bus_line_number)
        //     {
        //         DataSource.Line_stations.Remove(findLineStation);
        //         DataSource.Line_stations.Add(busLineStation.Clone());
        //     }
        //     else
        //         throw new BusLineStationNotFoundException(busLineStation.Bus_line_number, $"Line number: {busLineStation.Bus_line_number} doesn't stop at this station" );
        //     return true;
        // }//update place ob route for all other stops on line

        // void DeleteBusLineStation(int StationID, int lineNumber)
        // {

        //     BusLine findLine = DataSource.Lines.FirstOrDefault((tmpBusLine => tmpBusLine.BusID == busID);

        //     if (findLine != null)
        //     {
        //         DataSource.Lines.Remove(findLine);
        //     }
        //     else
        //         throw new BusLineNotFoundException("Bus line wasn't found in the system");

        // }
        // IEnumerable<BusLine> GetAllBuslines()
        // {
        //     return
        //             from BusLine in DataSource.Lines
        //             select BusLine.Clone();
        // }
        // BusLine GetBusLine(int busID)
        // {
        //     BusLine findLine = DataSource.Lines.FirstOrDefault(tmpLine => tmpLine.BusID == busID);

        //     if (findLine != null)
        //         return findLine.Clone();
        //     else
        //         throw new BusLineStationAlreadyExistsException(" Bus line already exists in the system");
        // }

        // IEnumerable<BusLineStation> GetStationsOfBusLine(int lineID)
        // {

        //     var stationList = from station in DataSource.Line_stations
        //                    where station.LineID == lineID
        //                    select station.Clone();
        //     return stationList;
        // }//done!!!

        // #endregion




    }
}


