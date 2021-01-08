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
//FIGURE OUT WHAT ADAPTERS ARE!!!!!
namespace BL
{
    
    public class BlImp1 : IBL
    {
        static Random rnd = new Random(DateTime.Now.Millisecond);
        readonly IDAL dal = DalFactory.GetDal();
        public StationOnTheLine DOtoBOstationOnTheLine(DO.BusStation station)
        {
         
        }
        BusLine DOtoBOBusLineAdapter(DO.BusLine busLine)//NOT SURE IF THIS IS RIGHT
        {
            BusLine BbusLine=new BusLine();
            busLine.CopyPropertiesTo(BbusLine);
            //get stations
            BbusLine.Stations = from station in dal.GetAllBusLineStationsBy(station => station.BusLineNumber == BbusLine.BusID)
                                 let stop = dal.GetBusStation(station.pairID)
                                 select DOtoBOstationOnTheLine(stop);//will be switching this to call bus line stations
            //for each place in the list of all (distances by has two stops) select 
            StationOnTheLine first = BbusLine.Stations.FirstOrDefault(s => s.Number_on_route == 1);
            var sss = from station in BbusLine.Stations
                      let distance = (dal.GetTwoConsecutiveStops(first.StationID, station.StationID).Distance)
                      //not sure how to stick this in
            

        }

        public void AddBusLine(int line_number, List<int> stations, int first_bus_hour, int first_bus_minute, int last_bus_hour, int last_bus_minutes, TimeSpan frequency)
        {
            //check frequency:
            DateTime first_bus = new DateTime(0, 0, 0, first_bus_hour, first_bus_minute, 0);
            DateTime last_bus = new DateTime(0, 0, 0, last_bus_hour, last_bus_minutes, 0);
            TimeSpan totalTime = last_bus - first_bus;
            //does it need to be exact? like if first bus is 11, second bus is 12:30, frequencey is every hour is that an exception?
            //FIGURE THIS OUT AND THEN THROW AN EXCEPTION
            //deal with stations:
            //for each int get the station from the ds and add it to new list. how to do this in a linq?
            //then go over the list and add busline stops and two consecutive stops
            //deal with intercity, check by first and last stop
            //do origin & destination
            BO.BusLine line = new BO.BusLine {
                //do running number for id
                Bus_line_number = line_number,
                //add stations here
                //intercity
                //origin
                //destination
                Exists = true,
                First_bus=first_bus,
                Last_bus=last_bus,
                Frequency=frequency
                //adapt and send to ds   
            };
            //IS THERE A PLACE WHERE ALL THE BO STUFF IS SAVED? LIKE A DS FOR THIS LEVEL
        }
        // left to do: 
        //void UpdateBusLine(BusLine line);
        //void DeleteBusLine(BusLine line);
        //void PrintBusLine(BusLine line);
        BusLine GetBusLine(int lineID)
        {
            //DO.Student studentDO;
            //try
            //{
            //    studentDO = dl.GetStudent(id);
            //}
            //catch (DO.BadPersonIdException ex)
            //{
            //    throw new BO.BadStudentIdException("Person id does not exist or he is not a student", ex);
            //}
            //return studentDoBoAdapter(studentDO);

            //NONE OF THIS MAKES ANY SENSE AT ALL
        }
        //IEnumerable<BusLine> GetAllBusLines();
        //IEnumerable<BusLine> GetBusLineBy(Predicate<BusLine> predicate);
        //void AddBusStation(BusStation station);
        //void UpdateBusStation(BusStation station);
        //void DeleteBusStation(BusStation station);
        //void PrintBusStation(BusStation station);
        //BusStation GetBusStation(int stationID);
        //IEnumerable<BusStation> GetAllBusStations();
        //IEnumerable<BusStation> GetBusStationBy(Predicate<BusLine> predicate);


    }
}