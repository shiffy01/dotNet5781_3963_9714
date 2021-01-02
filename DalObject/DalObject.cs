using DLAPI;
using DO;
using DS;
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
            if (DataSource.Buses.FirstOrDefault(tmpBus => tmpBus.License ==bus.License) != null)
            {
                throw new DuplicateBusException(bus.License, $", Bus with License number: {bus.License} already exists in the system");
                
            }

            DataSource.Buses.Add(bus.Clone());
            return true;
        }//done

        public bool updateBus(Bus bus)
        {
           
              Bus realBus= DataSource.Buses.Find(tmpBus => tmpBus.License== bus.License);

            if (realBus != null)
            {
                DataSource.Buses.Remove(realBus);
                DataSource.Buses.Add(bus.Clone());
            }
            else
                throw new BusNotFoundException("Bus wasn't found in the system");
            return true;
        }//done

        public bool DeleteBus(Bus bus)
        {

            Bus realBus = DataSource.Buses.FirstOrDefault(tmpBus => tmpBus.License == bus.License);

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
           Bus sameBus= DataSource.Buses.Find(tmpBus => tmpBus.License == license);

            if (sameBus != null)
                return sameBus.Clone();
            else
                throw new BusNotFoundException("Bus wasn't found in the system");
        } //done
        public IEnumerable<Bus> GetAllBuses()
        {
            return 
                from Bus in DataSource.Buses
                   select Bus.Clone();
        }//done!!
        public IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate)
        {
            return from bus in DataSource.Buses
                   where predicate(bus)
                   select bus.Clone();
        }    //Done!!

        //public void UpdatePerson(int license, Action<Bus> update)
        //{
        //    throw new NotImplementedException();
        //}//figure out..

        #endregion

        public int AddBusLine(BusLine busLine)
        {
            BusLine findLine = DataSource.Lines.FirstOrDefault(tmpBusLine => tmpBusLine.Bus_line_number == busLine.Bus_line_number);
            if (findLine != null)
                if(findLine.firstStation==busLine.firstStation && findLine.lastStation == busLine.lastStation)
                     throw new BusLineStationAlreadyExistsException(" Bus line already exists in the system");

            busLine.BusID = DS.Config.BusDrivingCounter;
            DataSource.Lines.Add(busLine.Clone());
            return busLine.BusID;
        }//done!!

        bool UpdateBusLine(BusLine busLine)
        {
            BusLine findLine = DataSource.Lines.FirstOrDefault(tmpBusLine => tmpBusLine.Bus_line_number == busLine.Bus_line_number);
            if (findLine != null && findLine.firstStation == busLine.firstStation && findLine.lastStation == busLine.lastStation)
                {
                    DataSource.Lines.Remove(findLine);
                    DataSource.Lines.Add(busLine.Clone());
                }
                else
                throw new BusLineNotFoundException("Bus line wasn't found in the system");
            return true;
        }//done//!!

        void DeleteBusLine(int busID)
        {

            BusLine findLine = DataSource.Lines.FirstOrDefault((tmpBusLine => tmpBusLine.BusID== busID);

            if (findLine!= null)
            {
                DataSource.Lines.Remove(findLine);
            }
            else
                throw new BusLineNotFoundException("Bus line wasn't found in the system");
            
        }//done!!

        IEnumerable<BusLine> GetAllBuslines()
        {
            return
                    from BusLine in DataSource.Lines
                    select BusLine.Clone();
        }//done!!
        BusLine GetBusLine(int busID)
        {
            BusLine findLine = DataSource.Lines.FirstOrDefault(tmpLine => tmpLine.BusID == busID);

            if (findLine != null)
                return findLine.Clone();
            else
                throw new BusLineStationAlreadyExistsException(" Bus line already exists in the system");
        }//done!!

        IEnumerable<BusLine> GetBuslinesOfStation(int stationID)
        {

            var lineList = from station in DataSource.Line_stations
                              where station.StationID == stationID
                              select station.Clone();
            return lineList;
        }//done!!!

        #endregion










#endregion
    }
}


