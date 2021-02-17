using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DLAPI

{

    public interface IDAL
       
    {
        #region Bus  definition
        int AddBus(DateTime start, int totalk);
        void UpdateBus(int license, Bus.Status_ops status, DateTime last_tune_up, int kilometerage, int totalkilometerage, int gas);     
        void DeleteBus(int license);
        IEnumerable<Bus> GetAllBuses();
        Bus GetBus(int license);
        IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate);
        #endregion

        #region BusLine  definition
        BusLine AddBusLine(int line_number, string dest, string org);
        void UpdateBusLine(BusLine busLine);
        void DeleteBusLine(int busID);
        IEnumerable<BusLine> GetAllBuslines();
        BusLine GetBusLine(int busID);
        IEnumerable<BusLine> GetAllBusLinesBy(Predicate<BusLine> predicate);
        //IEnumerable<BusLine> GetBuslinesOfStation(int stationID);
        #endregion

        #region LineFrequency
        void AddLineFrequency(int lineID, DateTime start, TimeSpan frequency, DateTime end);
        void UpdateLineFrequency(LineFrequency frequency);
        void DeleteLineFrequency(string id);
        IEnumerable<LineFrequency> GetAllLineFrequency();
        LineFrequency GetLineFrequency(string id);
        IEnumerable<LineFrequency> GetAllLineFrequencyBy(Predicate<LineFrequency> predicate);
        #endregion

        #region User  definition
        void AddUser(string userName, string password, bool manager);
        void UpdateUser(User user);
        void DeleteUser(string userName, string password);
        IEnumerable<User> GetAllUsers();
        User GetUser(string userName, string password);
        IEnumerable<User> GetAllUsersBy(Predicate<User> predicate);

        #endregion

        #region StationSearchHistory  definition
        void AddStationSearchHistory(string userName, int code, bool starred, string nickname);
        void UpdateStationSearchHistory(StationSearchHistory search);
        void DeleteStationSearchHistory(string id);
        IEnumerable<StationSearchHistory> GetAllStationSearchHistory();
        StationSearchHistory GetStationSearchHistory(string id);
        IEnumerable<StationSearchHistory> GetAllStationSearchHistoryBy(Predicate<StationSearchHistory> predicate);

        #endregion

        #region RouteSearchHistory  definition
        void AddRouteSearchHistory(string userName, int code1, int code2, bool starred, string nickname);
        void UpdateRouteSearchHistory(RouteSearchHistory search);
        void DeleteRouteSearchHistory(string id);
        IEnumerable<RouteSearchHistory> GetAllRouteSearchHistory();
        RouteSearchHistory GetRouteSearchHistory(string id);
        IEnumerable<RouteSearchHistory> GetAllRouteSearchHistoryBy(Predicate<RouteSearchHistory> predicate);

        #endregion

        #region LineSearchHistory  definition
        void AddLineSearchHistory(string userName, int code, bool starred, string nickname);
        void UpdateLineSearchHistory(LineSearchHistory search);
        void DeleteLineSearchHistory(string id);
        IEnumerable<LineSearchHistory> GetAllLineSearchHistory();
        LineSearchHistory GetLineSearchHistory(string id);
        IEnumerable<LineSearchHistory> GetAllLineSearchHistoryBy(Predicate<LineSearchHistory> predicate);

        #endregion

        #region BusLineStation  definition
        void AddBusLineStation(int station_id, int line_id, int number_on_route);
        void UpdateBusLineStation(BusLineStation busLineStation);
        void DeleteBusLineStation(string ID);
        BusLineStation GetBusLineStation(string ID);
        //  IEnumerable<BusLineStation> GetStationsOfBusLine(int lineID);
        IEnumerable<BusLineStation> GetAllBusLineStationsBy(Predicate<BusLineStation> predicate);
        IEnumerable<BusLineStation> GetAllBusLineStations();
        #endregion

        #region BusStation  definition
        void AddBusStation(int code, double latitude, double longitude, string name, string address);
        void UpdateBusStation(BusStation busStation);
        void DeleteBusStation(int code);
        BusStation GetBusStation(int code);
        IEnumerable<BusStation> GetAllBusStationsBy(Predicate<BusStation> predicate);
        IEnumerable<BusStation> GetAllBusStations();
      
        #endregion

        #region AdjacentStations  definition
        void AddAdjacentStations(int code_1, int code_2, double distance, TimeSpan drive_time);
        void UpdateAdjacentStations(AdjacentStations twoConsecutiveStops);
        void DeleteAdjacentStations(string pairID);
        AdjacentStations GetAdjacentStations(string pairID);
        bool AdjacentStationsExists(string pairID);
        IEnumerable<AdjacentStations> GetAllPairs();
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

        void CreateStationsList();




    }
}