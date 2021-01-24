﻿using System;
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
        List<string> getCities();
        string RemoveBusStationFromLine(int stationCode, int lineNumber);
        #endregion
         void AddAdjacentStations(int codeA, int codeB, double distance, TimeSpan drive_time);
    }
}