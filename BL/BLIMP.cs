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
        //  static Random rnd = new Random(DateTime.Now.Millisecond);
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
        public void AddBusLine(int line_number, List<int> stations, DateTime first_bus, DateTime last_bus, TimeSpan frequency)
        {

            //DateTime first_bus = new DateTime(0, 0, 0, first_bus_hour, first_bus_minute, 0);
            //DateTime last_bus = new DateTime(0, 0, 0, last_bus_hour, last_bus_minutes, 0);
            TimeSpan totalTime = last_bus - first_bus;
            if (frequency > totalTime)
                throw new FrequencyConflictException("The bus doesn't come frequently enough");
            DateTime total_last_bus = first_bus;
            while (total_last_bus < last_bus)
                total_last_bus += frequency;

            //stations can't be incorrect bec the user has to choose them directly from the list of existing stations
            //if we change that, need to add some try/catches

            bool in_city = (dal.GetBusStation(stations[0]).City == dal.GetBusStation(stations[stations.Count - 1]).City);
            DO.BusLine newBus;
            try
            {
                newBus = dal.AddBusLine(line_number, in_city, dal.GetBusStation(stations[stations.Count - 1]).Address, dal.GetBusStation(stations[0]).Address, first_bus, total_last_bus, frequency);
            }
            catch (DO.BusLineAlreadyExistsException ex)
            {
                throw new BusLineAlreadyExistsException(line_number, "Cannot add the bus line because it is already in the system", ex);
            }
            //stations can't be incorrect bec the user has to choose them directly from the list of existing stations
            try
            {
                for (int i = 0; i < stations.Count; i++)
                {
                    dal.GetBusStation(stations[i]);//throws an exception if this bus station doesn't exist
                    dal.AddBusLineStation(stations[i], newBus.BusID, line_number, i);
                    if (i != 0)
                        if (!dal.TwoConsecutiveStopsExists(stations[i - 1].ToString() + stations[i].ToString()))
                            dal.AddTwoConsecutiveStops(stations[i - 1], stations[i]);
                }
            }
            catch (Exception ex)//CHANGE THIS TO "ONE OF THE BUS STOPS DOESNT EXIST" EXCEPTION BUT FIRST PUT ALL THE EXCEPTIONS TOGETHER IN ONE FILE
            {
                throw ex;
            }

            // dal.AddBusLine(line_number, )
            if (total_last_bus > last_bus)
                throw new FrequencyConflictException("The time of the last bus has been changed to match the frequency");

        }//not done        
        public void UpdateBusLine(BusLine line)
        {
            DO.BusLine DOline;
            DOline = BOtoDOBusLineAdapter(line);
            try
            {
                dal.UpdateBusLine(DOline);
            }
            catch (DO.BusLineNotFoundException ex)
            {
                throw new BusLineNotFoundException("The bus line cannot be deleted because it is not in the system", ex);
            }
        }//find out what ur allowed to update and maybe change this!!!!!!!!!!!!!!!!
        public void DeleteBusLine(int lineID)
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
        public BusLine GetBusLine(int lineID)
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
        public IEnumerable<BusLine> GetAllBusLines()
        {
            var list =
            from bus in dal.GetAllBuslines()
            select (DOtoBOBusLineAdapter(bus));
            return list;
        }//done
        public IEnumerable<BusLine> GetBusLineBy(Predicate<BusLine> predicate)
        {
            return from line in dal.GetAllBuslines()
                   let BOLine = DOtoBOBusLineAdapter(line)
                   where predicate(BOLine)
                   select BOLine;
        }//done
        public void UpdateBusStation(int code, string name)
        {
            DO.BusStation DOstation;
            DOstation =dal.GetBusStation(code);
            DOstation.Name = name;
            try
            {
                dal.UpdateBusStation(DOstation);
            }
            catch (DO.StationNotFoundException ex)
            {
                throw new StationNotFoundException(code, $"Station :{code} wasn't found in the system", ex);
            }
        }
        public BusStation GetBusStation(int stationID)//check why the red in this function
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
        public IEnumerable<BusStation> GetAllBusStations()
        {
            var list =
           from bus in dal.GetAllBusStations()
           select (ConvertStationDOtoBO(bus));
            return list;
        }//done
        public IEnumerable<BusStation> GetBusStationBy(Predicate<BusStation> predicate)
        {
            return from station in dal.GetAllBusStations()
                   let BOStation = ConvertStationDOtoBO(station)
                   where predicate(BOStation)
                   select BOStation;
        }//done
        public void AddStationToBusLine(int bus_number, int code, int place)//done
        {
            try
            {
                dal.GetBusLine(bus_number);
                dal.GetBusStation(code);
            }
            catch (DO.BusLineNotFoundException ex)
            {
                throw new BusLineNotFoundException("The bus line is not in the system", ex);
            }
            catch (DO.StationNotFoundException ex)
            {
                throw new StationNotFoundException(ex.Code, "The Station is not in the system", ex);
            }
            try
            {
                dal.AddBusLineStation(code, bus_number, dal.GetBusLine(bus_number).Bus_line_number, place);
            }
            catch (DO.BusLineStationAlreadyExistsException ex)
            {
                throw new StationAlreadyExistsEOnTheLinexception(bus_number, code);//fill this in when i make the class
            }
            var list = dal.GetAllBusLineStationsBy(station => (station.BusLineNumber == bus_number && station.Number_on_route >= place));
            foreach (var item in list)
            {
                item.Number_on_route++;
                dal.UpdateBusLineStation(item);
            }
            //doesnt need exception because it just got it from the ds, its for sure there        

            //add distances between the stop and the ones next to it if they dont exist already
            int code_before = (dal.GetAllBusLineStationsBy(station => (station.LineID == bus_number)).FirstOrDefault(ss => (ss.Number_on_route == place - 1))).StationID;
            int code_after = (dal.GetAllBusLineStationsBy(station => (station.LineID == bus_number)).FirstOrDefault(ss => (ss.Number_on_route == place + 1))).StationID;
            //two stops with before and after stops if not exist
            bool add_before=false, add_after = false;
            if (!(dal.TwoConsecutiveStopsExists(code_before.ToString() + code.ToString())))
                add_before = true;
            if (!(dal.TwoConsecutiveStopsExists(code.ToString() + code_after.ToString())))
                add_after = true;
            if (add_before || add_after)
                throw new NeedDistanceException(code_before, code, code_after, add_before, add_after);
            //basiclly i threw and exception to get the distance and the pl will send to the AddTwoConsecutiveStops function
            //when it catches the exception. bec i cant ask for distance in middle of the function
        }
        public void RemoveBusStationFromLine(int stationCode, int lineNumber)
        {
            string id = stationCode.ToString() + lineNumber.ToString();
            DO.BusLineStation busLineStation;
            try
            {
                busLineStation = dal.GetBusLineStation(id);
            }
            catch (DO.BusLineStationNotFoundException ex)
            {
                throw new StationNotFoundException(stationCode, $",station number: {stationCode} is not on this route", ex);
            }
            var lineStations = from lineStation in dal.GetAllBusLineStationsBy(l => l.BusLineNumber == busLineStation.BusLineNumber)
                               select lineStation;
            List<DO.BusLineStation> busLineStationList = (lineStations.OrderBy(lineStation => lineStation.Number_on_route)).ToList();
            string pairIDPreviousStop = busLineStation.StationID.ToString() + busLineStationList[busLineStation.Number_on_route - 1].StationID.ToString();
            dal.DeleteTwoConsecutiveStops(pairIDPreviousStop);
            string pairIDNextStop = busLineStationList[busLineStation.Number_on_route + 1].StationID.ToString() + busLineStation.StationID.ToString();
            dal.DeleteTwoConsecutiveStops(pairIDNextStop);

            dal.AddTwoConsecutiveStops(busLineStationList[busLineStation.Number_on_route - 1].StationID, busLineStationList[busLineStation.Number_on_route + 1].StationID);
            for (int i = busLineStation.Number_on_route + 1; i < busLineStationList.Count; i++)
            {
                busLineStationList[i].Number_on_route--;
                dal.UpdateBusLineStation(busLineStationList[i]);
            }

        }
        public void AddBusStation(BusStation station)
        {
            //bonus
        }
        public void AddTwoConsecutiveStops(int codeA, int codeB, double distance, TimeSpan drive_time)
        {
            try
            {
                dal.AddTwoConsecutiveStops(codeA, codeB, distance, drive_time);
            }
            catch (DO.PairAlreadyExitsException ex)
            {
                throw new PairAlreadyExitsException("the pair already exists in the system", ex);
            }
        }

        public void DeleteBusStation(int stationID)//BONUS, not implemented
        {
            throw new NotImplementedException();
        }
    }      
    
}
