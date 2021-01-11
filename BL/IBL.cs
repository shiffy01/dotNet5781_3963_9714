using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi
{
    public interface IBL
    {
        //void AddBus(Bus bus);
        //void UpdateBus(Bus bus);
        //void DeleteBus(int license);
        void AddBusLine(int line_number, List<int> stations, DateTime first_bus, DateTime last_bus, TimeSpan frequency);
        void UpdateBusLine(BusLine line);
        void DeleteBusLine(int lineID);
   
        BusLine GetBusLine(int lineID);
        IEnumerable<BusLine> GetAllBusLines();
        IEnumerable<BusLine> GetBusLineBy(Predicate<BusLine> predicate);
        void AddBusStation(BusStation station);//DO WE NEED THIS SINCE THEY CANT ADD BUS STATIONS???
        void UpdateBusStation(BusStation station);
        void DeleteBusStation(int stationID);
     
        BusStation GetBusStation(int stationID);
        IEnumerable<BusStation> GetAllBusStations();
        IEnumerable<BusStation> GetBusStationBy(Predicate<BusStation> predicate);
        void RemoveBusStationFromLine(StationOnTheLine station, BusLine line);
    }
}