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
        void AddBusLine(int line_number, List<int> stations, int first_bus_hour, int first_bus_minute, int last_bus_hour, int last_bus_minutes, TimeSpan frequency);
        void UpdateBusLine(BusLine line);
        void DeleteBusLine(int lineID);
        void PrintBusLine(int lineID);
        BusLine GetBusLine(int lineID);
        IEnumerable<BusLine> GetAllBusLines();
        IEnumerable<BusLine> GetBusLineBy(Predicate<BusLine> predicate);
        void AddBusStation(BusStation station);//DO WE NEED THIS SINCE THEY CANT ADD BUS STATIONS???
        void UpdateBusStation(BusStation station);
        void DeleteBusStation(int stationID);
        void PrintBusStation(int StationID);
        BusStation GetBusStation(int stationID);
        IEnumerable<BusStation> GetAllBusStations();
        IEnumerable<BusStation> GetBusStationBy(Predicate<BusLine> predicate);
        void AddStationToBusLine(int bus_number, int code, int place);
    }
}