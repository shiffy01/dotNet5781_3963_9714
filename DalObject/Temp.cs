using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using DO;
using DS;
namespace DalObject
{
    class Temp
    {
        //var arr2 = 
        // arr1.Where(i => i % 2 == 0).Select(item => item* 2);
        // bool unvaccinated =
        //pets.Any(p => p.Age > 1 && p.Vaccinated == false);
        public DalObject()
        {
            DS.DataSource.initialize_buses();
        }
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
    }
}
