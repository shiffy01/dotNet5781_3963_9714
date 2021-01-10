using System;
using BlApi;
using DLAPI;
//using DL;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BO;
//using DO;

namespace BL
{
    
    public class BlImp1 : IBL
    {
        static Random rnd = new Random(DateTime.Now.Millisecond);
        readonly IDAL dal = DalFactory.GetDal();
        BO.BusStation ConvertStationDOtoBO(DO.BusStation DOstation)//ADINA'S, COPIED IT HERE TO USE
        {
            BO.BusStation BOstation = new BO.BusStation();
            int StationCode = DOstation.Code;
            DOstation.CopyPropertiesTo(BOstation);
            BOstation.Lines = from line in dal.GetBuslinesOfStation(StationCode)
                              select DOtoBOBusLineAdapter(line);
            return BOstation;
        }//figure out convert!!!!
        DO.BusStation ConvertStationBOtoDO(BO.BusStation BOstation)//ADINA'S, COPIED IT HERE TO USE
        {
            DO.BusStation DOstation = new DO.BusStation();
            BOstation.CopyPropertiesTo(DOstation);
            return DOstation;
        }
        StationOnTheLine DOtoBOstationOnTheLine(DO.BusStation DOstation)
        {
            StationOnTheLine BOstation = new StationOnTheLine();
            DOstation.CopyPropertiesTo(BOstation);
            //this function is missing number on route and distance from last stop
            //those will have to be filled in somewhere else
            //otherwise i think its fine
            return BOstation;
        }
        //if we need to convert station on the line to anything in do add it here, im not sure we do
        BusLine DOtoBOBusLineAdapter(DO.BusLine DObusLine)//done :)
        {
            BusLine BObusLine = new BusLine();
            DObusLine.CopyPropertiesTo(BObusLine);
            List<StationOnTheLine> list = (from station in dal.GetAllBusLineStationsBy(station => station.BusLineNumber == BObusLine.BusID)
                                          let stop = dal.GetBusStation(station.StationID)
                                          select DOtoBOstationOnTheLine(stop)).ToList();
            for (int i = 0; i < list.Count; i++)//add number on route for each stop
                list[i].Number_on_route = i + 1;
            for (int i = 0; i < (list.Count - 1); i++)//add distance to the next stop for each stop
                list[i].Distance_to_the_next_stop = dal.GetTwoConsecutiveStops(list[i].ToString() + list[i + 1].ToString()).Distance;
            BObusLine.Stations = list;
            return BObusLine;
        }
        DO.BusLine BOtoDOBusLineAdapter(BusLine BObusline)//i think done
        {
            DO.BusLine DObusline = new DO.BusLine();
            BObusline.CopyPropertiesTo(DObusline);
            return DObusline;
        }

        public void AddBusLine(int line_number, List<int> stations, int first_bus_hour, int first_bus_minute, int last_bus_hour, int last_bus_minutes, TimeSpan frequency)
        {
            
            DateTime first_bus = new DateTime(0, 0, 0, first_bus_hour, first_bus_minute, 0);
            DateTime last_bus = new DateTime(0, 0, 0, last_bus_hour, last_bus_minutes, 0);
            TimeSpan totalTime = last_bus - first_bus;
            //does it need to be exact? like if first bus is 11, second bus is 12:30, frequencey is every hour is that an exception?
            //FIGURE THIS OUT AND THEN THROW AN EXCEPTION

            //stations can't be incorrect bec the user has to choose them directly from the list of existing stations
            try
            {
                foreach (var item in stations)
                {
                    dal.GetBusStation(item);//throws an exception if this bus station doesn't exist

                }
            }
            catch(Exception ex)//CHANGE THIS TO "ONE OF THE BUS STOPS DOESNT EXIST" EXCEPTION BUT FIRST PUT ALL THE EXCEPTIONS TOGETHER IN ONE FILE
            {
                throw ex;
            }
            
           // dal.AddBusLine(line_number, )
           
       
          
        }
        // left to do: 
        void UpdateBusLine(BusLine line)
        {
            DO.BusLine DOline;
            DOline=BOtoDOBusLineAdapter(line);
            try
            {
                dal.UpdateBusLine(DOline);
            }
            catch (DO.BusLineNotFoundException ex)
            {
                throw new BusLineNotFoundException("The bus line cannot be deleted because it is not in the system", ex);
            }
        }
        void DeleteBusLine(int lineID)
        {
            try
            {
                dal.DeleteBusLine(lineID);
            }
            catch (DO.BusLineNotFoundException ex)
            {
                throw new BusLineNotFoundException("The bus line cannot be deleted because it is not in the system", ex);
            }
        }//done
        //void PrintBusLine(int lineID);
        BusLine GetBusLine(int lineID)
        {
            DO.BusLine DObusline;
            try
            {
                DObusline = dal.GetBusLine(lineID);
            }
            catch (DO.BusLineNotFoundException ex)
            {
                throw new BusLineNotFoundException("The bus line is not in the system", ex);
            }
            return DOtoBOBusLineAdapter(DObusline);

            //NONE OF THIS MAKES ANY SENSE AT ALL
        }//done
        IEnumerable<BusLine> GetAllBusLines()
        {
            var list =
            from bus in dal.GetAllBuslines()
            select (DOtoBOBusLineAdapter(bus));
            return list;
        }//done
        IEnumerable<BusLine> GetBusLineBy(Predicate<BusLine> predicate)
        {
            return from line in dal.GetAllBuslines()
                   let BOLine= DOtoBOBusLineAdapter(line)
                   where predicate(BOLine)
                   select BOLine;
        }//done
        //void AddBusStation(BusStation station);
        void UpdateBusStation(BusStation station)
        {
            DO.BusStation DOstation;
            DOstation = ConvertStationBOtoDO(station);
            try
            {
                dal.UpdateBusStation(DOstation);
            }
            catch (DO.StationNotFoundException ex)
            {
                throw new StationNotFoundException(station.Code, $"Station :{station.Code} wasn't found in the system", ex);
            }
        }
        void DeleteBusStation(int stationID)
        {
            try
            {
                dal.DeleteBusStation(stationID);
            }
            catch (DO.StationNotFoundException ex)
            {
                throw new StationNotFoundException(stationID, $"Station :{stationID} wasn't found in the system", ex);
            }
        }//done
        //void PrintBusStation(int stationID);
        BusStation GetBusStation(int stationID)
        {
            DO.BusStation DObusStation;
            try
            {
                DObusStation = dal.GetBusStation(stationID);
            }
            catch (DO.StationNotFoundException ex)
            {
                throw new StationNotFoundException(stationID, $"Station :{stationID} wasn't found in the system", ex);
            }
            return ConvertStationDOtoBO(DObusStation);
        }//done
        IEnumerable<BusStation> GetAllBusStations()
        {
            var list =
           from bus in dal.GetAllBusStations()
           select (ConvertStationDOtoBO(bus));
            return list;
        }//done
        IEnumerable<BusStation> GetBusStationBy(Predicate<BusStation> predicate)
        {
            return from station in dal.GetAllBusStations()
                   let BOStation = ConvertStationDOtoBO(station)
                   where predicate(BOStation)
                   select BOStation;
        }//done


    }
}