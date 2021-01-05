using System;
using BlApi;
using DLAPI;
//using DL;
using BO;
using DO;
//FIGURE OUT WHAT ADAPTERS ARE!!!!!
namespace BL
{
    public class BlImp1 : IBL
    {
        static Random rnd = new Random(DateTime.Now.Millisecond);

        readonly IDAL dal = DalFactory.GetDal();


        void AddBusLine(BO.BusLine line)
        {
            //trying to figure out what this busline is. it said in wpf "add bus line" so it makes an empty one and sends it here. then
            //how does it get the rest of the information... i think it should get a full one and then check, if anythings wrong
            //throw an exception and otherwise send to the do.
        }
       // left to do: 
        //void UpdateBusLine(BusLine line);
        //void DeleteBusLine(BusLine line);
        //void PrintBusLine(BusLine line);
        //IEnumerable<BusLine> GetAllBusLines();
        //IEnumerable<BusLine> GetBusLineBy(Predicate<BusLine> predicate);
        //void AddBusStation(BusStation station);
        //void UpdateBusStation(BusStation station);
        //void DeleteBusStation(BusStation station);
        //void PrintBusStation(BusStation station);
        //IEnumerable<BusStation> GetAllBusStations();
        //IEnumerable<BusStation> GetBusStationBy(Predicate<BusLine> predicate);


    }
}