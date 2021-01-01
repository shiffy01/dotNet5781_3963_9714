using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using DO;
using DS;
using DL;
namespace DalObject
{
    class Temp
    {
       
       
        //bool AddBusDeparted(BusDeparted busDeparted)
        //{
        //    //if the bus is driving it cant depart. meaning if there is an object of bus driving with the same id this function should throw an exception
        //    //but it said not to do any calculations so prob that is only dealt with in the bl
        //    bool exists =
        //        DataSource.Departed.Any(p => p.Exists == true && p.BusID == busDeparted.BusID);
        //    var erased =
        //        DataSource.Departed.Any(p => p.Exists == false && p.BusID == busDeparted.BusID);
        //    //if(exists)
        //    //    throw some sort of exception
        //    if(erased)

        //    BusDeparted bus = Clone(busDeparted);

        //}

        int AddBusLine(BusLine busLine)
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
        void DeleteBusLine(int busID)
        {
            DO.BusLine bus = DataSource.Lines.Find(b =>( b.BusID == busID&&b.Exists));

            if (bus != null)
            {
                bus.Exists = false;
            }
            else
                throw new DO.BusLineNotFoundException("The BusLine is not found in the system");
        }
        public bool UpdateBusLine(BusLine busLine)
        {
            DO.BusLine bus = DataSource.Lines.Find(b => (b.BusID == busLine.BusID&&b.Exists));

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
        IEnumerable<BusLine> GetBuslines()
        {
            var list =
             from bus in DataSource.Lines
             where (bus.Exists)
             select (bus.Clone());
            return list;
                
        }
        BusLine GetBusLine(int busID)
        {
            DO.BusLine bus = DataSource.Lines.Find(b => (b.BusID == busID&&b.Exists));

            if (bus != null)
                return bus.Clone();
            else
                throw new DO.BusLineNotFoundException("this bus line is not in the system");
        }
    }
}
