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
        void AddBusLine(BusLine line);
        void UpdateBusLine(BusLine line);
        void DeleteBusLine(BusLine line);
        void PrintBusLine(BusLine line);
        void AddBusStation(BusStation station);
        void UpdateBusStation(BusStation station);
        void DeleteBusStation(BusStation station);
        void PrintBusStation(BusStation station);
    }
}