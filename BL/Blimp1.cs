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

    public class Blimp1: IBL
    {
        #region singleton implementaion
        private static readonly Blimp1 blInstance = new Blimp1();
        private Blimp1()
        {
        }

        static Blimp1()
        {
        }

        public static Blimp1 Instance
        {

            get => blInstance;
        }
        #endregion

        string license_format(int license)
        {
            string finalLicense;
            if (license < 10000000)//license plate hase 7 digits
            {
                int tmpLicense = license / 100000;//this gives us the first 2 digits of license
                finalLicense = (" " + tmpLicense + "-");
                tmpLicense = license % 100000;
                if (tmpLicense < 100)// if it has 2 or less digits
                {
                    finalLicense += "000-";
                }
                else
                {
                    if (tmpLicense < 1000)//if it has 3 digits
                        finalLicense += "00";
                    if (tmpLicense < 10000 && tmpLicense > 999)//if it has 4 digits
                        finalLicense += "0";
                    finalLicense += tmpLicense / 100;
                    finalLicense += "-";
                }

                tmpLicense = tmpLicense % 100;
                if (tmpLicense == 0)
                    finalLicense += "00";
                else
                {
                    if (tmpLicense < 10)//if it has one digit
                        finalLicense += "0";
                    finalLicense += tmpLicense;
                }

            }
            else
            //license plate has 8 digits
            {
                int tmpLicense = license / 100000;//this gives us the first 3 digits of license
                finalLicense = (tmpLicense + "-");
                tmpLicense = license % 100000;
                if (tmpLicense < 1000)//if it has 3 digits or less
                {
                    finalLicense += "00-";
                }
                else
                {
                    if (tmpLicense < 10000)// if it has 4 digits
                        finalLicense += "0";
                    finalLicense += (tmpLicense / 1000) + "-";
                    tmpLicense = tmpLicense % 1000;//gets last 3
                }
                if (tmpLicense == 0)
                    finalLicense += "000";
                else
                {
                    if (tmpLicense < 10)// has only 1 digit
                        finalLicense += "00";
                    if (tmpLicense < 100 && tmpLicense > 9)//has only 2 digits
                        finalLicense += "0";
                    finalLicense += tmpLicense;
                }
            }
            finalLicense += " ";
            return finalLicense;
        }
        string GetCityFromAddress(string address)
        {
            int first_index = address.IndexOf(" :עיר")+5;
            int last_index = address.IndexOf("רציף");
            int length = last_index - first_index;
            return address.Substring(first_index, length);
            
        }

        #region convert functions
        readonly IDAL dal = DalFactory.GetDal();
        BO.BusStation ConvertStationDOtoBO(DO.BusStation DOstation)
        {
            BO.BusStation BOstation = new BO.BusStation();
            int StationCode = DOstation.Code;
            DOstation.CopyPropertiesTo(BOstation);
            try
            {
                BOstation.Lines = from line in dal.GetBuslinesOfStation(StationCode)
                                  select DOtoBOBusLineAdapter(line);
            }
            catch (PairNotFoundException ex)
            {
                throw ex;
            }
            BOstation.City = GetCityFromAddress(BOstation.Address);
            return BOstation;
        }//figure out convert!!!!
        DO.BusStation ConvertStationBOtoDO(BO.BusStation BOstation)
        {
            DO.BusStation DOstation = new DO.BusStation();
            BOstation.CopyPropertiesTo(DOstation);
            return DOstation;
        }
        StationOnTheLine DOtoBOstationOnTheLine(DO.BusStation DOstation)
        {
            StationOnTheLine BOstation = new StationOnTheLine();
            DOstation.CopyPropertiesTo(BOstation);
            BOstation.City = GetCityFromAddress(BOstation.Address);
            return BOstation;
        }
        Bus ConvertDOtoBOBus(DO.Bus DOBus)
        {
            Bus BOBus = new Bus();
            DOBus.CopyPropertiesTo(BOBus);
            BOBus.LicensePlate = license_format(DOBus.License);
            return BOBus;
        }
        BusLine DOtoBOBusLineAdapter(DO.BusLine DObusLine)//done :)
        {
            BusLine BObusLine = new BusLine();
            DObusLine.CopyPropertiesTo(BObusLine);
            List<StationOnTheLine> list = (from station in dal.GetAllBusLineStationsBy(station => station.LineID == BObusLine.BusID)
                                           let stop = dal.GetBusStation(station.StationID)
                                           select DOtoBOstationOnTheLine(stop)).ToList();
            
            for (int i = 0; i < list.Count; i++)//add number on route for each stop
                list[i].Number_on_route = i + 1;
            try
            {
                for (int i = 0; i < (list.Count - 1); i++)//add distance to the next stop for each stop
                    list[i].Distance_to_the_next_stop = dal.GetAdjacentStations(list[i].Code.ToString() + list[i + 1].Code.ToString()).Distance;
            }
            catch (DO.PairNotFoundException ex)
            {
                throw new PairNotFoundException("data error, all the pairs should be here", ex);
            }
            BObusLine.Stations = list;
            BObusLine.Origin = list[0].Address;
            BObusLine.Destination = list[list.Count - 1].Address;
            BObusLine.InterCity = (list[0].City == list[list.Count - 1].City);

            return BObusLine;
        }
        DO.BusLine BOtoDOBusLineAdapter(BusLine BObusline)//i think done
        {
            DO.BusLine DObusline = new DO.BusLine();
            BObusline.CopyPropertiesTo(DObusline);
            return DObusline;
        }
        #endregion
        void FrequencyCheck(DateTime firstBus, DateTime lastBus, TimeSpan frequency)
        {
            TimeSpan totalTime = lastBus - firstBus;
            if (frequency > totalTime)
                throw new FrequencyConflictException("The bus doesn't come frequently enough");
            if(frequency.Ticks != 0)
              if(totalTime.Ticks%frequency.Ticks!=0)
                throw new FrequencyConflictException("Frequencey doesnt match time frame");          
        }
        #region BusLine functions
        public List<string> AddBusLine(int line_number, List<int> stations, DateTime first_bus, DateTime last_bus, TimeSpan frequency)
        {
            try
            {
                FrequencyCheck(first_bus, last_bus, frequency);
            }
            catch (FrequencyConflictException ex)
            {
                throw new FrequencyConflictException("This bus cannot be added, because the frequency does not match the times of the fist and last buses", ex);
            }
            bool in_city;
            try
            {
                in_city = dal.GetBusStation(stations[0]).City == dal.GetBusStation(stations[stations.Count - 1]).City;
            }
            catch (DO.StationNotFoundException ex)
            {
                throw new StationNotFoundException(ex.Code, "One of the bus station codes sent is invalid");
            }
            DO.BusLine newBus;
            try
            { 
                newBus = dal.AddBusLine(line_number, dal.GetBusStation(stations[stations.Count - 1]).Address, dal.GetBusStation(stations[0]).Address, first_bus, last_bus, frequency);
            }
            catch (DO.BusLineAlreadyExistsException ex)
            {
                throw new BusLineAlreadyExistsException(line_number, "This bus cannot be added because a busline with this number is already in the system", ex);
            }
            List<string> needed_distances=new List<string>();
            try
            {

                for (int i = 0; i < stations.Count; i++)
                {
                    dal.GetBusStation(stations[i]);//throws an exception if this bus station doesn't exist
                    dal.AddBusLineStation(stations[i], newBus.BusID, i);
                    if (i != 0)
                        if (!dal.AdjacentStationsExists(stations[i - 1].ToString() + stations[i].ToString()))
                            needed_distances.Add(stations[i - 1] +"*" +stations[i]);
                }
            }
            catch (DO.StationNotFoundException ex)
            {
                throw new StationNotFoundException(ex.Code, "One of the stops is not in the system");
            }
            return needed_distances;
        } //returns all the pair IDs of distances we need to make
        public  void UpdateBusLine(DateTime firstBus, DateTime lastBus, TimeSpan frequency, int busID, int lineNumber = 0)
        {
            DO.BusLine busToUpdate;
            try
            {
                busToUpdate = dal.GetBusLine(busID);
            }
            catch (DO.BusLineNotFoundException ex)
            {
                throw new BusLineNotFoundException("cannot update a bus line that is not in the system",  ex);
            }
            DateTime tmpFirstbus=busToUpdate.First_bus, tmpLastbus=busToUpdate.Last_bus;
            TimeSpan tmpFrequency=busToUpdate.Frequency;

            #region check frequency
            if ( firstBus!= null)
            {
               tmpFirstbus= firstBus;
            }
            if ( lastBus != null)
            {
                tmpLastbus = lastBus;
            }
            if ( frequency != null)
            {
                tmpFrequency = frequency;
            }
            try
            {
                FrequencyCheck(tmpFirstbus, tmpLastbus, tmpFrequency);
            }
            catch (FrequencyConflictException ex)
            {
                throw ex;
            }
            #endregion
            if (lineNumber != 0)
                busToUpdate.Bus_line_number = lineNumber;
            busToUpdate.First_bus = tmpFirstbus;
            busToUpdate.Last_bus = tmpLastbus;
            busToUpdate.Frequency = tmpFrequency;
            try
            {
                dal.UpdateBusLine(busToUpdate);
            }
            catch (DO.BusLineNotFoundException ex)
            {
                throw new BusLineNotFoundException("The bus line was not found in the system", ex);
            }
        }
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
            //deleting all the bus line stations of this line:
            var BusLineStationsToDelete = dal.GetAllBusLineStationsBy(id => (id.LineID == lineID));
            foreach (var item in BusLineStationsToDelete)
            {
                dal.DeleteBusLineStation(item.BusLineStationID);
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
            BusLine answer=new BusLine();
            try
            {
                 answer = DOtoBOBusLineAdapter(DObusline);
            }
            catch (PairNotFoundException ex)
            {
              throw ex;
            }
            return answer;

        }//done
        public IEnumerable<BusLine> GetAllBusLines()
        {
            IEnumerable<DO.BusLine> list;
            try
            {
                list =
            from bus in dal.GetAllBuslines()
            select bus;
                
            }
            catch (PairNotFoundException ex)
            {
                throw ex;
            }
            List<BusLine> list2 = new List<BusLine>();
            foreach (var item in list)
            {
                list2.Add(DOtoBOBusLineAdapter(item));
            }
            return list2;
        }//done
        public IEnumerable<BusLine> GetAllBusLinesBy(Predicate<BusLine> predicate)
        {
            IEnumerable<BusLine> answer;
            try
            {
                answer = from line in dal.GetAllBuslines()
                         let BOLine = DOtoBOBusLineAdapter(line)
                         where predicate(BOLine)
                         select BOLine;
            }
            catch (PairNotFoundException ex)
            {
                throw ex;
            }
            return answer;

        }//done
        #endregion

        #region BusStation functions
        public void UpdateBusStation(int code, string name)
        {
            DO.BusStation DOstation;
            try
            {
                DOstation = dal.GetBusStation(code);
            }
            catch(DO.StationNotFoundException ex)
            {
                throw new StationNotFoundException(code, $"Station :{code} wasn't found in the system", ex);
            }
            DOstation.Name = name;    
                dal.UpdateBusStation(DOstation);  
        }
        public BusStation GetBusStation(int stationID)
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
           
                
            try
            {
                var list =
               (from bus in dal.GetAllBusStations()
               select (ConvertStationDOtoBO(bus)));
                return list;
            }
            catch (PairNotFoundException ex)
            {
                throw ex;
            }
        }//done
      
        public IEnumerable<BusStation> GetAllBusStationsBy(Predicate<BusStation> predicate)
        {
            return from station in dal.GetAllBusStations()
                   let BOStation = ConvertStationDOtoBO(station)
                   where predicate(BOStation)
                   select BOStation;
        }//done
        public List<string> AddStationToBusLine(int bus_number, int code, int place)//done
        {
            try//make sure the line and the station exist:
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
            
         
            var list = dal.GetAllBusLineStationsBy(station => (station.LineID == bus_number && station.Number_on_route >= place));
            if (place > list.Count() + 1)
                throw new InvalidPlaceException("Place number is too high");
            if (place < 1)
                throw new InvalidPlaceException("Place number cannot be lower than 1");
            
          
            foreach (var item in list)
            {
                item.Number_on_route++;
                dal.UpdateBusLineStation(item);
            }
            try
            {
                dal.AddBusLineStation(code, bus_number, place);
            }
            catch (DO.BusLineStationAlreadyExistsException ex)
            {
                throw new StationAlreadyExistsOnTheLinexception(bus_number, code, "The station is already on the line", ex);
            }
            //doesnt need exception because it just got it from the ds, its for sure there        

            //return a list of distances that need to be added:
            List<string> needed_distances = new List<string>();
            int code_before = (dal.GetAllBusLineStationsBy(station => (station.LineID == bus_number)).FirstOrDefault(ss => (ss.Number_on_route == place - 1))).StationID;
            int code_after = (dal.GetAllBusLineStationsBy(station => (station.LineID == bus_number)).FirstOrDefault(ss => (ss.Number_on_route == place + 1))).StationID;
            
            if (!(dal.AdjacentStationsExists(code_before.ToString() + code.ToString())))
                needed_distances.Add(code_before + "*" + code);
            if (!(dal.AdjacentStationsExists(code.ToString() + code_after.ToString())))
                needed_distances.Add(code + "*" + code_after);
            return needed_distances;
        
        }
        public string RemoveBusStationFromLine(int stationCode, int lineNumber)
        {
            string id = stationCode.ToString() + lineNumber.ToString();
            DO.BusLineStation busLineStation=new DO.BusLineStation();
            try
            {
                busLineStation = dal.GetBusLineStation(id);
            }
            catch (DO.BusLineStationNotFoundException ex)
            {
                throw new StationDoesNotExistOnTheLinexception(stationCode, lineNumber, $",station number: {stationCode} is not on this route", ex);
            }
            var lineStations = from lineStation in dal.GetAllBusLineStationsBy(l => l.LineID == busLineStation.LineID)
                               select lineStation;
            List<DO.BusLineStation> busLineStationList = (lineStations.OrderBy(lineStation => lineStation.Number_on_route)).ToList();
            for (int i = busLineStation.Number_on_route + 1; i < busLineStationList.Count; i++)
            {
                busLineStationList[i].Number_on_route--;
                try
                {
                    dal.UpdateBusLineStation(busLineStationList[i]);
                }
                catch (DO.BusLineStationNotFoundException ex)
                {
                    throw new StationDoesNotExistOnTheLinexception(stationCode, lineNumber, $",station number: {stationCode} is not on this route", ex);
                }
            }
            try
            {
                dal.DeleteBusLineStation(id);
            }
            catch (DO.BusLineStationNotFoundException ex)
            {
                throw new StationDoesNotExistOnTheLinexception(stationCode, lineNumber, $",station number: {stationCode} is not on this route", ex);
            }
            if (! (dal.AdjacentStationsExists(busLineStationList[busLineStation.Number_on_route - 1].StationID.ToString()+ busLineStationList[busLineStation.Number_on_route + 1].StationID.ToString())))
            {
                return busLineStationList[busLineStation.Number_on_route - 2].StationID + "*" + busLineStationList[busLineStation.Number_on_route].StationID;
            }
            return null;
        }
        public void AddBusStation(int code, double latitude, double longitude, string name, string address, string city)
        {
            try
            {
                dal.AddBusStation(code, latitude, longitude, name, address);
            }
            catch (DO.StationAlreadyExistsException ex)
            {
                throw new StationALreadyExistsException(code, "You can't add a bus station that is already in the system", ex);
            }
        }
        public void DeleteBusStation(int stationID)//BONUS, not implemented
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Bus functions
        //TO THIS DELETE FUNCTION ADD: IF THE BUS IS IN MIDDLE OF DRIVING, SEND A MESSEGE AND ONLY DELETE IT WHEN ITS FINISHED DRIVING
        // (IF WE'RE DOING THE תהליכון )
        public string AddBus(bool access, bool wifi)
        {
            int license=dal.AddBus(access, wifi);
            return license_format(license);
        }
        public void UpdateBus(int license, bool access, bool wifi)
        {
            try
            {
                dal.UpdateBus(license, access, wifi);
            }
            catch (DO.BusNotFoundException ex)
            {
                throw new BusNotFoundException("You cannot update a bus that doesn't exist", ex);
            }
        }
        public void DeleteBus(int license)
        {

            try
            {
                dal.DeleteBus(license);
            }
            catch (DO.BusNotFoundException ex)
            {
                throw new BusNotFoundException("The bus line cannot be deleted because it is not in the system", ex);
            }
           
           
        }
        public Bus GetBus(int license)
        {
            try
            {
                return ConvertDOtoBOBus(dal.GetBus(license));
            }
            catch (DO.BusNotFoundException ex)
            {
                throw new BusNotFoundException("This bus is not in the system", ex);
            }
        }
        public IEnumerable<Bus> GetAllBuses()
        {
                return
                from bus in dal.GetAllBuses()
                select (ConvertDOtoBOBus(bus));
        }
        public IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate)
        {  
            return
               from bus in dal.GetAllBuses()
               let BOBus=ConvertDOtoBOBus(bus)
               where predicate(BOBus)
               select (ConvertDOtoBOBus(bus));
        }
        #endregion
        public void AddAdjacentStations(int codeA, int codeB, double distance, TimeSpan drive_time)
        {
            try
            {
                dal.AddAdjacentStations(codeA, codeB, distance, drive_time);
            }
            catch (DO.PairAlreadyExitsException ex)
            {
                throw new PairAlreadyExistsException("the pair already exists in the system", ex);
            }
        }

        
    }
   

}
