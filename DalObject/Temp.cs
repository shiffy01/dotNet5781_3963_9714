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
               return 0; //throw exception!!!
           
           BusLine b = DataSource.Lines.FirstOrDefault(e => e.BusID == busLine.BusID && e.Exists == false);
            if (b != null)
            { b.Exists = true;
                return b.BusID;
            }

            BusLine bus = Clone(busLine);
            DataSource.Lines.Add(bus);
            return bus.BusID;
        }
    }
}
