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
        #region Bus CRUD
        string AddBus(bool access, bool wifi);//
        void UpdateBus(int license, Bus.Status_ops status, DateTime last_tune_up, int kilometerage, int totalkilometerage, int gas);
        void UpdateBus(int license, bool access, bool wifi);
        void DeleteBus(int license);
        Bus GetBus(int license);
        IEnumerable<Bus> GetAllBuses();
        IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate);
        #endregion

        #region Bus functions
        void refill(Bus bus);
        void tuneUp(Bus bus);
        void drive(Bus bus);
        bool canDrive(Bus bus);
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