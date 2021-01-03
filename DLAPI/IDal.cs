using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DLAPI

{

    public interface IDAL
        //ADD GET FILTERS!!!!!! also should the functions be public? if so change the implementation in dalobject too.
    {
        #region Bus  definition
        bool AddBus(Bus bus);
        bool UpdateBus(Bus bus);
     //   void UpdatePerson(int license, Action<Bus> update);
        bool DeleteBus(int license);
        IEnumerable<Bus> GetAllBusses();
        Bus GetBus(int license);
        IEnumerable<Bus> GetAllBussesBy(Predicate<Bus> predicate);
        #endregion

        #region BusLine  definition
        int AddBusLine(BusLine busLine);
        bool UpdateBusLine(BusLine busLine);
        void DeleteBusLine(int busID);
        IEnumerable<BusLine> GetAllBuslines();
        BusLine GetBusLine(int busID);
        IEnumerable<BusLine> GetBuslinesOfStation(int stationID);
        #endregion

        #region BusLineStation  definition
        bool AddBusLineStation(BusLineStation busLineStation);
        bool UpdateBusLineStation(BusLineStation busLineStation);
        bool DeleteBusLineStation(int StationCode, int lineID);
        BusLineStation GetBusLineStation(int stationID);
      //  IEnumerable<BusLineStation> GetStationsOfBusLine(int lineID);
        IEnumerable<BusLineStation> GetAllBusLineStations();
        #endregion

        #region BusStation  definition
        bool AddBusStation(BusStation busStation);
        bool UpdateBusStation(BusStation busStation);
        bool DeleteBusStation(int code);
        BusStation GetBusStation(int code);
        IEnumerable<BusStation> GetAllBusStationsBy(Predicate<BusStation> predicate);
        IEnumerable<BusStation> GetAllBusStations();
        #endregion

        #region TwoConsecutiveStops  definition
        bool AddTwoConsecutiveStops(TwoConsecutiveStops twoConsecutiveStops);
        bool UpdateTwoConsecutiveStops(TwoConsecutiveStops twoConsecutiveStops);
        bool DeleteTwoConsecutiveStops(int stop1_code, int stop2_code);
        TwoConsecutiveStops GetTwoConsecutiveStops(int stop1_code, int stop2_code);
        IEnumerable<TwoConsecutiveStops> GetAllPairs();
        #endregion
        //#region BusDeparted  definition
        //bool AddBusDeparted(BusDeparted busDeparted);
        //bool UpdateBusDeparted(BusDeparted busDeparted);
        //void DeleteBusDeparted(int license);
        //IEnumerable<Bus> GetBusBusDeparted();
        //BusDeparted GetBusDeparted(int busID);
        //#endregion

        //#region BusDriving  definition
        //bool AddBusDriving(BusDriving busDriving);
        //bool UpdateBusDriving(BusDriving busDriving);
        //void DeleteBusDriving(int busID);
        //IEnumerable<Bus> GetBussesDriving();
        //BusDriving GetBusDriving(int BusID);
       // #endregion

        //#region User  definition
        //bool AddUser(User user);
        //bool UpdateUser(User user);
        //void DeleteUser(string UserName, string password);
        //IEnumerable<Bus> GetUsers();
        //User GetUser(string userName, string password);
        //#endregion

        //#region UserRide  definition
        //bool AddUserRide(UserRide userRide);
        //bool UpdateUserRide(UserRide userRide);
        //void DeleteUserRide(int rideID);
        //UserRide GetUserRide(int rideID);

        //IEnumerable<Bus> GetUserRides();
        //#endregion





    }
}