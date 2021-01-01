using DLAPI;
using DO;
using DS;
using System;
using System.Collections.Generic;
using System.Linq;
//ADD EXCEPTIONS!!!
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

            Bus realBus = DataSource.Buses.Find(tmpBus => tmpBus.License == bus.License);

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
        }//done
        public IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate)
        {
            throw new NotImplementedException();
        }//not sure abt this
        #endregion


        //public void UpdatePerson(int license, Action<Bus> update)
        //{
        //    throw new NotImplementedException();
        //}









        #endregion
    }
}


