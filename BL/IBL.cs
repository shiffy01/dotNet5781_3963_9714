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
        #region Bus
      //  i dont know what the user should be allowed to decide for a bus. status is automaticly ready, license is a running number, startdate
      //is now, last tune up is now bec its tuned right when it comes in, total km on a new bus is 0, gas is full.
      //only the last three things i guess... but they're kind of useless on public transportation anyway, the buses just come and 
      //no one gets to have special requests. so fill in add and update functions later.

        string AddBus(bool access, bool wifi);//
        void UpdateBus(int license, bool access, bool wifi);
        void DeleteBus(int license);
        Bus GetBus(int license);
        IEnumerable<Bus> GetAllBuses();
        IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate);
        #endregion

        #region BusLine
        List<string> AddBusLine(int line_number, List<int> stations, DateTime first_bus, DateTime last_bus, TimeSpan frequency);
        void UpdateBusLine( DateTime firstBus,  DateTime lastBus, TimeSpan frequency, int busID, int lineNumber = 0);
        void DeleteBusLine(int lineID);
        BusLine GetBusLine(int lineID);
        IEnumerable<BusLine> GetAllBusLines();
        IEnumerable<BusLine> GetAllBusLinesBy(Predicate<BusLine> predicate);
        #endregion

        #region BusStation
        void AddBusStation(int code, double latitude, double longitude, string name, string address, string city);
        void UpdateBusStation(int code, string name);
        void DeleteBusStation(int stationID); 
        BusStation GetBusStation(int stationID);
        IEnumerable<BusStation> GetAllBusStations();
        IEnumerable<BusStation> GetAllBusStationsBy(Predicate<BusStation> predicate);
        List<string> AddStationToBusLine(int bus_number, int code, int place);
       
        string RemoveBusStationFromLine(int stationCode, int lineNumber);
        #endregion
         void AddAdjacentStations(int codeA, int codeB, double distance, TimeSpan drive_time);
    }
}