using System;
using BlApi;
using DLAPI;
//using DL;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;


//DO BUS BACKGROUND WORKERS!!!!

using BO;
//using DO;

namespace BL
{

    public class Blimp1 : IBL
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

        private readonly IDAL dal = DalFactory.GetDal();
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

            int first_index = address.IndexOf("עיר") + 3;
            int last_index = address.IndexOf("רציף");
            int length;
            if (last_index == -1)
            {
                string ans = address.Substring(first_index);
                ans = ans.Replace(@" ", "");
                return ans.Replace(@":", "");
            }
            length = last_index - first_index;
            string answer = address.Substring(first_index, length);
            answer = answer.Replace(@" ", "");
            return answer.Replace(@":", "");

        }
        void FrequencyCheck(List<BusLineTime> times)
        {
            foreach (var t in times)
            {
                TimeSpan totalTime = t.End - t.Start;
                if (t.Frequency > totalTime)
                    throw new FrequencyConflictException("The frequency doesn't match the time frame");
                if (t.Frequency.Ticks == 0 && t.End != t.Start)
                    throw new FrequencyConflictException("The frequency cannot be zero unless the bus only comes once");
                if (totalTime.Ticks % t.Frequency.Ticks != 0)
                    throw new FrequencyConflictException("Frequencey doesn't match time frame");
            }
            for (int i = 0; i < times.Count() - 1; i++)
                if (times[i].End > times[i + 1].Start)
                    throw new FrequencyConflictException("The times cannot overlap");
        }

        #region convert functions     
        BusStation ConvertStationDOtoBO(DO.BusStation DOstation)
        {
            BO.BusStation BOstation = new BO.BusStation();     
            DOstation.CopyPropertiesTo(BOstation);
            try
            {
                var list = from line in dal.GetAllBusLineStationsBy(s=>s.StationID==DOstation.Code)
                                  select line.LineID;
                List<BusLine> line_list = new List<BusLine>();
                foreach (var item in list)
                {
                    line_list.Add(DOtoBOBusLineAdapter(dal.GetBusLine(item)));
                }
                BOstation.Lines = line_list;
            }
            catch (DataErrorException ex)
            {
                throw ex;
            }
            BOstation.City = GetCityFromAddress(BOstation.Address);
            return BOstation;
        }
        DO.BusStation ConvertStationBOtoDO(BusStation BOstation)
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
        BusLine DOtoBOBusLineAdapter(DO.BusLine DObusLine)
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
                throw new DataErrorException("There was an error loading the data", ex);
            }
            BObusLine.Stations = list;
            BObusLine.Origin = list[0].Address;
            BObusLine.Destination = list[list.Count - 1].Address;
            BObusLine.InterCity = (list[0].City == list[list.Count - 1].City);

            BObusLine.Times = from time in dal.GetAllLineFrequencyBy(f => f.LineID == BObusLine.BusID).OrderBy(t=>t.Start)
                              select new BusLineTime {
                                  Start = time.Start,
                                  End=time.End,
                                  Frequency=time.Frequency
                              };
            return BObusLine;
        }
        DO.BusLine BOtoDOBusLineAdapter(BusLine BObusline)//i think done
        {
            DO.BusLine DObusline = new DO.BusLine();
            BObusline.CopyPropertiesTo(DObusline);
            DObusline.Exists = true;
            return DObusline;
        }
        DO.User BOtoDOUser(User BOuser)
        {
            DO.User DOuser = new DO.User();
            BOuser.CopyPropertiesTo(DOuser);
            DOuser.Exists = true;
            return DOuser;
        }
        User DOtoBOUser(DO.User DOuser)
        {
            User BOuser = new User();
            DOuser.CopyPropertiesTo(BOuser);
            BOuser.RouteSearches = from search in dal.GetAllRouteSearchHistoryBy(s => s.UserName == DOuser.UserName)
                                   select new Route {
                                       OriginCode=search.Station1Code,
                                       DestinationCode=search.Station2Code
                                   };
            BOuser.LineSearches = from search in dal.GetAllLineSearchHistoryBy(s => s.UserName == DOuser.UserName)
                                  select search.LineCode;
            BOuser.StationSearches = from search in dal.GetAllStationSearchHistoryBy(s => s.UserName == DOuser.UserName)
                                     select search.StationCode;
            return BOuser;
        }
        #endregion
       
        #region BusLine functions
        public List<string> AddBusLine(int line_number, List<int> stations, List<BusLineTime> times)
        {
            try
            {
                FrequencyCheck(times);
            }
            catch (FrequencyConflictException ex)
            {
                throw new FrequencyConflictException("This bus cannot be added, because there is a conflict with the times ", ex);
            }//check frequency
           
            DO.BusLine newBus;

            try
            {
                newBus = dal.AddBusLine(line_number, dal.GetBusStation(stations[stations.Count - 1]).Address, dal.GetBusStation(stations[0]).Address);
            }
            catch (DO.BusLineAlreadyExistsException ex)
            {
                throw new BusLineAlreadyExistsException(line_number, "This bus cannot be added because a busline with this number is already in the system", ex);
            }//add bus

            try
            {
                foreach (var t in times)
                {
                    dal.AddLineFrequency(newBus.BusID, t.Start, t.Frequency, t.End);
                }
            }
            catch (DO.LineFrequencyAlreadyExistsException ex)
            {
                throw new DataErrorException("There was an error pushing the data", ex);
            }//add times

            List<string> needed_distances = new List<string>();
            try
            {

                for (int i = 0; i < stations.Count; i++)
                {
                    dal.GetBusStation(stations[i]);//throws an exception if this bus station doesn't exist
                    dal.AddBusLineStation(stations[i], newBus.BusID, i);
                    if (i != 0)
                        if (!dal.AdjacentStationsExists(stations[i - 1].ToString() + stations[i].ToString()))
                            needed_distances.Add(stations[i - 1] + "*" + stations[i]);
                }
            }
            catch (DO.StationNotFoundException ex)
            {
                throw new StationNotFoundException(ex.Code, "One of the stops is not in the system");
            }//add stations on this line
            return needed_distances;
        } //returns all the pair IDs of distances we need to make
        public void UpdateBusLine(int busID, int lineNumber, List<BusLineTime> times)
        {
            DO.BusLine busToUpdate;
            try
            {
                busToUpdate = dal.GetBusLine(busID);
            }
            catch (DO.BusLineNotFoundException ex)
            {
                throw new BusLineNotFoundException("cannot update a bus line that is not in the system", ex);
            }//get bus
           
            busToUpdate.Bus_line_number = lineNumber; 
            try
            {
                dal.UpdateBusLine(busToUpdate);
            }
            catch (DO.BusLineNotFoundException ex)
            {
                throw new BusLineNotFoundException("The bus line was not found in the system", ex);
            }//update bus

            try
            {
                FrequencyCheck(times);
            }
            catch (FrequencyConflictException ex)
            {
                throw new FrequencyConflictException("This bus cannot be added, because there is a conflict with the times ", ex);
            }//check times

            try
            {
                foreach (var t in times)
                {
                    dal.AddLineFrequency(busToUpdate.BusID, t.Start, t.Frequency, t.End);
                }
            }
            catch (DO.LineFrequencyAlreadyExistsException ex)
            {
                throw new DataErrorException("There was an error pushing the data", ex);
            }//update times
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
            BusLine answer = new BusLine();
            try
            {
                answer = DOtoBOBusLineAdapter(DObusline);
            }
            catch (DataErrorException ex)
            {
                throw ex;
            }
            return answer;

        }//done
        public IEnumerable<BusLine> GetAllBusLines()
        {
            IEnumerable<DO.BusLine> list;

                list =
            from bus in dal.GetAllBuslines()
            select bus;

            
            List<BusLine> list2 = new List<BusLine>();
            try
            {
                foreach (var item in list)
                {
                    list2.Add(DOtoBOBusLineAdapter(item));
                }
            }
            catch (DataErrorException ex)
            {
                throw ex;
            }
            return list2;
        }//done
        public IEnumerable<BusLine> GetAllBusLinesBy(Predicate<BusLine> predicate)
        {
            return from b in GetAllBusLines()
                   where predicate(b)
                   select b;

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
            catch (DO.StationNotFoundException ex)
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
            catch (DataErrorException ex)
            {
                throw ex;
            }
        }//done

        public IEnumerable<BusStation> GetAllBusStationsBy(Predicate<BusStation> predicate)
        {
            try
            {
                return from station in dal.GetAllBusStations()
                       let BOStation = ConvertStationDOtoBO(station)
                       where predicate(BOStation)
                       select BOStation;
            }
            catch (DataErrorException ex)
            {
                throw ex;
            }
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
            DO.BusLineStation busLineStation = new DO.BusLineStation();
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
            if (!(dal.AdjacentStationsExists(busLineStationList[busLineStation.Number_on_route - 1].StationID.ToString() + busLineStationList[busLineStation.Number_on_route + 1].StationID.ToString())))
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

        #region Bus CRUD
       
        public string AddBus(bool access, bool wifi)
        {
            int license = dal.AddBus(access, wifi);
            return license_format(license);
        }
        public void UpdateBus(int license, Bus.Status_ops status, DateTime last_tune_up, int kilometerage, int totalkilometerage, int gas)
        {
            //converting BO status to DO status:
            int number = (int)status;
            DO.Bus.Status_ops DOstatus = (DO.Bus.Status_ops)number;
            try
            {
          //      dal.UpdateBus(license, DOstatus, last_tune_up, kilometerage, totalkilometerage, gas);
            }
            catch (DO.BusNotFoundException ex)
            {
                throw new BusNotFoundException("This bus is not in the system", ex);
            }
        }

        public void UpdateBus(int license, bool access, bool wifi)
        {
            try
            {
           //     dal.UpdateBus(license, access, wifi);
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

        #region Bus functions 

        //private void Worker_DoWork(object sender, DoWorkEventArgs e)
        //{

        //    Bus b = (Bus)e.Argument;

        //    for (int i = 0; i < b.Time; i++)
        //    {

        //        System.Threading.Thread.Sleep(1000);//one second

        //        (sender as BackgroundWorker).ReportProgress((b.Time - i) * 10, b);//sends the number of seconds untill the bus is ready to drive again
        //    }
        //    e.Result = b;
        //}
        //private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    Bus b = (e.UserState as Bus);
        //    int time = e.ProgressPercentage;
        //    string mes = "";
        //    if (time >= 60)
        //    {
        //        mes += (time / 60) + " hour";
        //        if (time / 60 > 1)
        //            mes += "s";
        //        if (time % 60 != 0)
        //            mes += " and " + time % 60 + " minutes";

        //    }
        //    else
        //        mes += time + " minutes";
        //    mes += " untill the bus can drive";
        //    b.Seconds = mes;
        //}
        //private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    Bus bus = (e.Result as Bus);
        //    bus.ButtonVisibility = false;
        //    bus.Seconds = "Ready";
        //    bus.Progressb = 0;
        //    if (bus.Status == Bus.Status_ops.At_mechanic || bus.Status == Bus.Status_ops.Filling_up)
        //    {
        //        bus.Status = Bus.Status_ops.Ready;
        //        bus.CanDrive = true;
        //        bus.CanGas = true;
        //        bus.CanTuneUp = true;
        //    }
        //    //the bus will be refilled automaticly if there is less than 40 in the gas tank and will be tuned up if less than 2000 km left to drive (or passed the date)
        //    if (bus.Status == Bus.Status_ops.On_the_road)//the bus just came back from a drive
        //    {
        //        if (bus.Milage < 18000 && !((DateTime.Now - bus.Last_tune_up).Days > 356) && bus.Gas >= 40)//the bus does not need gas or a tune up
        //        {
        //            bus.Status = Bus.Status_ops.Ready;
        //            bus.CanDrive = true;
        //            bus.CanGas = true;
        //            bus.CanTuneUp = true;
        //        }
        //        if (bus.Milage > 18000 || (bus.Last_tune_up - DateTime.Now).Days > 356)//the bus needs a tune up
        //        {
        //            if (bus.Gas < 40)//if the bus needs gas too
        //                bus.Refill();
        //            Tuneup(bus);
        //        }
        //        if (bus.Gas < 40)
        //        {
        //            Refill(bus);
        //        }
        //    }

        //}

        //BackgroundWorker gas = new BackgroundWorker();
        //gas.DoWork += Worker_DoWork;
        //        gas.ProgressChanged += Worker_ProgressChanged;
        //        gas.RunWorkerCompleted += Worker_RunWorkerCompleted;
        //        gas.WorkerReportsProgress = true;
        //        gas.WorkerSupportsCancellation = true;
        //gas.RunWorkerAsync(b1);
        public void refill(Bus bus)
        {
            
            try
            {
                UpdateBus(bus.License, BO.Bus.Status_ops.Filling_up, bus.Last_tune_up, bus.kilometerage, bus.Totalkilometerage, 1200);//1200 is a full tank
                //DO תהליכונים!!!!!!!!
        
            }
            catch (BusNotFoundException ex)
            {
                throw ex;
            }
            BackgroundWorker gas = new BackgroundWorker();
            //gas.DoWork += Worker_DoWork;
            //gas.ProgressChanged += Worker_ProgressChanged;
            //gas.RunWorkerCompleted += Worker_RunWorkerCompleted;
            //gas.WorkerReportsProgress = true;
            //gas.WorkerSupportsCancellation = true;
            //gas.RunWorkerAsync(bus);
        }
        public void tuneUp(Bus bus)
        {
            try
            {
                UpdateBus(bus.License, BO.Bus.Status_ops.At_mechanic, DateTime.Now, bus.kilometerage, bus.Totalkilometerage, 1200);//1200 is a full tank, 
                //the bus gets filled up at the end of a tune up
                //DO תהליכונים!!!!!!!!
            }
            catch (BusNotFoundException ex)
            {
                throw ex;
            }
        }
        public void drive(Bus bus, double distance)
        {
            
            try
            {
                UpdateBus(bus.License, BO.Bus.Status_ops.On_the_road, bus.Last_tune_up, (int)(bus.kilometerage+distance), (int)(bus.Totalkilometerage+distance), bus.Gas-(int)distance);
                //DO תהליכונים!!!!!!!!
               
            }
            catch (BusNotFoundException ex)
            {
                throw ex;
            }
        }
        public bool canDrive(Bus bus)
        {
            if(bus.Status==Bus.Status_ops.Ready)
                 return true;
            return false;
        }

        #endregion

        #region User functions
        public void AddUser(string userName, string password, bool manager)
        {
            try
            {
                dal.AddUser(userName, password, manager);
            }
            catch (DO.UserNameAlreadyExistsException ex)
            {
                throw new UserNameAlreadyExistsException("There is already a user with this name in the system", ex);
            }
        }
        public void UpdateUser(string userName, string password, bool manager)
        {
            try
            {
                dal.UpdateUser(BOtoDOUser(new User {
                    UserName = userName,
                    Password = password,
                    IsManager = manager
                }));
            }
            catch (DO.UserDoesNotExistException ex)
            {
                throw new UserDoesNotExistException("This user is not saved in the system", ex);
            }
        }
        public void DeleteUser(string userName, string password)
        {
            try
            {
                dal.DeleteUser(userName, password);
            }
            catch (DO.UserDoesNotExistException ex)
            {
                throw new UserDoesNotExistException("This user is not saved in the system", ex);
            }
        }
        public IEnumerable<User> GetAllUsers()
        {
            return from user in dal.GetAllUsers()
                   select (DOtoBOUser(user));
        }
        public User GetUser(string userName, string password)
        {
            try
            {
                return DOtoBOUser(dal.GetUser(userName, password));
            }
            catch (DO.UserDoesNotExistException ex)
            {
                throw new UserDoesNotExistException("This user is not in the system", ex);
            }
        }
        public IEnumerable<User> GetAllUsersBy(Predicate<User> predicate)
        {
            return from user in dal.GetAllUsers()
                   let u = DOtoBOUser(user)
                   where predicate(u)
                   select u;
        }
        public void DeleteAllHistory(String username, string password)
        {
            User user = new User();
            try
            {
                user = GetUser(username, password);
                foreach (var search in user.LineSearches)
                    dal.DeleteLineSearchHistory(user.UserName + search.ToString());
                foreach (var search in user.StationSearches)
                    dal.DeleteStationSearchHistory(user.UserName + search.ToString());
                foreach (var search in user.RouteSearches)
                    dal.DeleteLineSearchHistory(user.UserName + search.OriginCode.ToString() + search.DestinationCode.ToString());
            }
            catch (UserDoesNotExistException ex)
            {
                throw ex;
            }
            catch (Exception ex)//any of the searchDoesNotExist exceptions
            {
                throw new DataErrorException("There was an error loading the data", ex);
            }

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
