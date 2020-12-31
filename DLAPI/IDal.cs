﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DLAPI

{

    public interface IDAL
        //ADD GET FILTERS!!!!!!
    {
        #region Bus  definition
        bool AddBus(Bus bus);
        bool UpdateBus(Bus bus);
        void DeleteBus(int license);
        IEnumerable<Bus> GetBusses();
        #endregion

        #region BusDeparted  definition
        bool AddBusDeparted(BusDeparted busDeparted);
        bool UpdateBusDeparted(BusDeparted busDeparted);
        void DeleteBusDeparted(int license);
        IEnumerable<Bus> GetBusBusDeparted();
        BusDeparted GetBusDeparted(int busID);
        #endregion

        #region BusDriving  definition
        bool AddBusDriving(BusDriving busDriving);
        bool UpdateBusDriving(BusDriving busDriving);
        void DeleteBusDriving(int busID);
        IEnumerable<Bus> GetBussesDriving();
        BusDriving GetBusDriving(int BusID);
        #endregion

        #region BusLine  definition
        bool AddBusLine(BusLine busLine);
        bool UpdateBusLine(BusLine busLine);
        void DeleteBusLine(int busID);
        IEnumerable<Bus> GetBuslines();
        BusLine GetBusLine(int busID);
        #endregion

        #region BusLineStation  definition
        bool AddBusLineStation(BusLineStation busLineStation);
        bool UpdateBusLineStation(BusLineStation busLineStation);
        void DeleteBusLineStation(int license);
        BusLineStation GetBusLineStation(int stationID);
        IEnumerable<Bus> GetBusLineStations();
        #endregion

        #region BusStation  definition
        bool AddBusStation(BusStation busStation);
        bool UpdateBusStation(BusStation busStation);
        void DeleteBusStation(int stationID);
        BusStation GetBusStation(int stationID);
        IEnumerable<Bus> GetBusStations();
        #endregion

        #region TwoConsecutiveStops  definition
        bool AddTwoConsecutiveStops(TwoConsecutiveStops twoConsecutiveStops);
        bool UpdateTwoConsecutiveStops(TwoConsecutiveStops twoConsecutiveStops);
        void DeleteTwoConsecutiveStops(int stop1_code, int stop2_code);
        TwoConsecutiveStops GetwoConsecutiveStops(int stop1_code, int stop2_code);
        IEnumerable<Bus> GetTwoConsecutiveStopss();
        #endregion

        #region User  definition
        bool AddUser(User user);
        bool UpdateUser(User user);
        void DeleteUser(string UserName, string password);
        IEnumerable<Bus> GetUsers();
        User GetUser(string userName, string password);
        #endregion

        #region UserRide  definition
        bool AddUserRide(UserRide userRide);
        bool UpdateUserRide(UserRide userRide);
        void DeleteUserRide(int rideID);
        UserRide GetUserRide(int rideID);

        IEnumerable<Bus> GetUserRides();
        #endregion





    }
}