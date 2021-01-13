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
        #region BusLine
        List<string> AddBusLine(int line_number, List<int> stations, DateTime first_bus, DateTime last_bus, TimeSpan frequency);
        void UpdateBusLine(BusLine line);
        void DeleteBusLine(int lineID);
        BusLine GetBusLine(int lineID);
        IEnumerable<BusLine> GetAllBusLines();
        IEnumerable<BusLine> GetBusLineBy(Predicate<BusLine> predicate);
        #endregion

        #region BusStation
        void AddBusStation(int code, double latitude, double longitude, string name, string address, string city);//DO WE NEED THIS SINCE THEY CANT ADD BUS STATIONS???
        void UpdateBusStation(int code, string name);
        void DeleteBusStation(int stationID); 
        BusStation GetBusStation(int stationID);
        IEnumerable<BusStation> GetAllBusStations();
        IEnumerable<BusStation> GetBusStationBy(Predicate<BusStation> predicate);
        void RemoveBusStationFromLine(int stationCode, int lineNumber);
        #endregion
    }
}