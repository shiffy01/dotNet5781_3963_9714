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
        void DeleteBusLine(BusLine line);
        void PrintBusLine(BusLine line);
        BusLine GetBusLine(int lineID);
        IEnumerable<BusLine> GetAllBusLines();
        IEnumerable<BusLine> GetBusLineBy(Predicate<BusLine> predicate);
        BO.BusStation ConvertStationDOtoBO(DO.BusStation DOstation);
        DO.BusStation ConvertStationBOtoDO(BO.BusStation BOstation);
        void AddBusStation(BusStation station);
        void UpdateBusStation(BusStation station);
        void DeleteBusStation(BusStation station);
        void PrintBusStation(BusStation station);
        BusStation GetBusStation(int stationID);
        IEnumerable<BusStation> GetAllBusStations();
        IEnumerable<BusStation> GetBusStationBy(Predicate<BusLine> predicate);
    }
}