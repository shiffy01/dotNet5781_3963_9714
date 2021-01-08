using System;
using System.Linq;
using BlApi;
using DLAPI;
//using DL;
using BO;
using DO;
//FIGURE OUT WHAT ADAPTERS ARE!!!!!
namespace BL
{
    public class TmpBlimp : IBL
    {
        static Random rnd = new Random(DateTime.Now.Millisecond);

        readonly IDAL dal = DalFactory.GetDal();


        //void AddBusLine(BO.BusLine line)
        //{
       
        //}
        // left to do: 

        BO.BusStation ConvertStationDoBo(DO.BusStation DOstation)
        {
            BO.BusStation BOstation = new BO.BusStation();
            int StationCode = DOstation.Code;
            DOstation.CopyPropertiesTo(BOstation);
            BOstation.Lines = from line in dal.GetBuslinesOfStation(StationCode)
                                let tmp = line
                                      select tmp.CopyToStudentCourse(line);

            //new BO.StudentCourse()
            //{
            //    ID = course.ID,
            //    Number = course.Number,
            //    Name = course.Name,
            //    Year = course.Year,
            //    Semester = (BO.Semester)(int)course.Semester,
            //    Grade = sic.Grade
            //};

            return studentBO;
        }

    }
    //void UpdateBusLine(BusLine line);
    //void DeleteBusLine(BusLine line);
    //void PrintBusLine(BusLine line);
    //IEnumerable<BusLine> GetAllBusLines();
    //IEnumerable<BusLine> GetBusLineBy(Predicate<BusLine> predicate);
    //void AddBusStation(BusStation station);
    //void UpdateBusStation(BusStation station);
    //void DeleteBusStation(BusStation station);
    //void PrintBusStation(BusStation station);
    //IEnumerable<BusStation> GetAllBusStations();
    //IEnumerable<BusStation> GetBusStationBy(Predicate<BusLine> predicate);



}
}