﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DLAPI

{

    public interface IDAL
        //ADD GET FILTERS!!!!!! also should the functions be public? if so change the implementation in dalobject too.
        //maybe also late make some of the functions return id numbers if its useful
    {
        #region Bus  definition
        void AddBus(Bus bus);
        void UpdateBus(Bus bus);
        void DeleteBus(int license);
        IEnumerable<Bus> GetAllBusses();
        Bus GetBus(int license);
        IEnumerable<Bus> GetAllBussesBy(Predicate<Bus> predicate);
        #endregion

        #region BusLine  definition
        void AddBusLine(BusLine busLine);
        void UpdateBusLine(BusLine busLine);
        void DeleteBusLine(int busID);
        IEnumerable<BusLine> GetAllBuslines();
        BusLine GetBusLine(int busID);
        IEnumerable<BusLine> GetAllBusLinesBy(Predicate<BusLine> predicate);
        IEnumerable<BusLine> GetBuslinesOfStation(int stationID);
        #endregion

        #region BusLineStation  definition
        void AddBusLineStation(BusLineStation busLineStation);
        void UpdateBusLineStation(BusLineStation busLineStation);
        void DeleteBusLineStation(string pairID);
        BusLineStation GetBusLineStation(string pairID);
        //  IEnumerable<BusLineStation> GetStationsOfBusLine(int lineID);
        IEnumerable<BusLineStation> GetAllBusLineStationsBy(Predicate<BusLineStation> predicate);
        IEnumerable<BusLineStation> GetAllBusLineStations();
        #endregion

        #region BusStation  definition
        void AddBusStation(BusStation busStation);
        void UpdateBusStation(BusStation busStation);
        void DeleteBusStation(int code);
        BusStation GetBusStation(int code);
        IEnumerable<BusStation> GetAllBusStationsBy(Predicate<BusStation> predicate);
        IEnumerable<BusStation> GetAllBusStations();
        #endregion

        #region TwoConsecutiveStops  definition
        void AddTwoConsecutiveStops(TwoConsecutiveStops twoConsecutiveStops);
        void UpdateTwoConsecutiveStops(TwoConsecutiveStops twoConsecutiveStops);
        void DeleteTwoConsecutiveStops(int stop1_code, int stop2_code);
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