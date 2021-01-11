using System;
using System.Linq;
using BlApi;
using DLAPI;
//using DL;
using BO;
//using DO;
//FIGURE OUT WHAT ADAPTERS ARE!!!!!
namespace BL
{
    partial class BlImp1 : IBL
    {
        static Random rnd = new Random(DateTime.Now.Millisecond);

        readonly IDAL dal = DalFactory.GetDal();


        //void AddBusLine(BO.BusLine line)
        //{

        //}
        //// left to do: 

        //BO.BusStation ConvertStationDOtoBO(DO.BusStation DOstation)
        //{
        //    BO.BusStation BOstation = new BO.BusStation();
        //    int StationCode = DOstation.Code;
        //    DOstation.CopyPropertiesTo(BOstation);
        //   // BOstation.Lines 
        //        var a= from line in dal.GetBuslinesOfStation(StationCode)
        //                      select DOtoBOBusLineAdapter(line);
        //    return BOstation;
        //}//figure out convert!!!!
        //DO.BusStation ConvertStationBOtoDO(BO.BusStation BOstation)
        //{
        //    DO.BusStation DOstation = new DO.BusStation();
        //    BOstation.CopyPropertiesTo(DOstation);
        //    return DOstation;
        //}

        //void AddBusStation(BusStation station)
        //{
        //    try
        //    {
        //        dal.AddBusStation(ConvertStationBOtoDO(station));
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //}//done!
        public void RemoveBusStationFromLine(StationOnTheLine station, BusLine line)
        {
            var stationlist = (from stop in line.Stations
                               select stop).ToList();
            StationOnTheLine stationToRemove= stationlist.Find(s => s.Code == station.Code);
            for (int i = stationToRemove.Number_on_route; i < stationlist.Count; i++)
            {
                stationlist[i].Number_on_route--;
            }
            stationlist.Remove(stationToRemove);
            line.Stations = stationlist;
        }//add exception and fix 
    }


    





    //;
    //void DeleteBusStation(BusStation station);
    //void PrintBusStation(BusStation station);
    //IEnumerable<BusStation> GetAllBusStations();
    //IEnumerable<BusStation> GetBusStationBy(Predicate<BusLine> predicate);
}


