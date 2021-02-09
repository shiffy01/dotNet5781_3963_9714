using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    [Serializable]
    public class BusLineAlreadyExistsException : Exception
    {
        public BusLineAlreadyExistsException()
        {
        }
        public BusLineAlreadyExistsException(string messege) : base(messege)
        {

        }
        public BusLineAlreadyExistsException(string message, Exception inner) : base(message, inner)
        {
        }
        public override string ToString() => base.ToString() + "Bus line is already in the system";
    }
    [Serializable]
    public class BusLineNotFoundException : Exception
    {
        public BusLineNotFoundException()
        {
        }
        public BusLineNotFoundException(string messege) : base(messege)
        {

        }
        public BusLineNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }
        public override string ToString() => base.ToString() + "Bus line doesn't exist in the system";
    }
    [Serializable]
    public class BusLineStationAlreadyExistsException : Exception
    {
        public BusLineStationAlreadyExistsException()
        {
        }
        public BusLineStationAlreadyExistsException(string messege) : base(messege)
        {

        }
        public BusLineStationAlreadyExistsException(string message, Exception inner) : base(message, inner)
        {
        }
        public override string ToString() => base.ToString() + "Bus line is already in the system";
    }
    [Serializable]
    public class BusLineStationNotFoundException : Exception
    {
        public string ID;
        public BusLineStationNotFoundException(string id) : base() => ID = id;

        public BusLineStationNotFoundException(string id, string messege) : base(messege) => ID = id;

        public BusLineStationNotFoundException(string id, string message, Exception inner) : base(message, inner) => ID = id;

        public override string ToString() => base.ToString() + $"BusLineStation number: {ID} doesn't stop at this station";

    }
   
    [Serializable]
    public class DuplicateBusException : Exception
    {
        public int License;
        public DuplicateBusException(int license) : base() => License = license;
        public DuplicateBusException(int license, string messege) : base(messege) => License = license;
        public DuplicateBusException(int license, string message, Exception inner) : base(message, inner) => License = license;
        public override string ToString() => base.ToString() + $", Bus with License number: {License} already exists in the system";
    }
    [Serializable]
    public class PairAlreadyExitsException : Exception
    {
        public PairAlreadyExitsException() : base() { }
        public PairAlreadyExitsException(string message) : base(message) { }
        public PairAlreadyExitsException(string message, Exception inner) : base(message, inner) { }
        public override string ToString() => base.ToString() + " Pair already exists in the system";
    }
    [Serializable]
    public class PairNotFoundException : Exception
    {
        public PairNotFoundException() : base() { }
        public PairNotFoundException(string message) : base(message) { }
        public PairNotFoundException(string message, Exception inner) : base(message, inner) { }
        public override string ToString() => base.ToString() + "Pair not found in system";
    }
    [Serializable]
    public class StationAlreadyExistsException : Exception
    {
        public int Code;
        public StationAlreadyExistsException(int code) : base() => Code = code;
        public StationAlreadyExistsException(int code, string message) : base(message) => Code = code;
        public StationAlreadyExistsException(int code, string message, Exception inner) : base(message, inner) => Code = code;
        public override string ToString() => base.ToString() + $",Station with code: {Code} already exists in the system";
    }
    [Serializable]
    public class StationNotFoundException : Exception
    {
        public int Code;
        public StationNotFoundException(int code) : base() => Code = code;
        public StationNotFoundException(int code, string message) : base(message) => Code = code;
        public StationNotFoundException(int code, string message, Exception inner) : base(message, inner) => Code = code;
        public override string ToString() => base.ToString() + $",Station number: {Code} wasn't found in the system";
    }
    [Serializable]
    public class XMLFileLoadCreateException : Exception
    {
        string Filepath;
        public XMLFileLoadCreateException(string filepath, string messege, Exception inner) : base(messege, inner) => Filepath = filepath;
    }
    [Serializable]
    public class BusNotFoundException : Exception
    {
        public BusNotFoundException()
        {
        }
        public BusNotFoundException(string messege) : base(messege)
        {

        }
        public BusNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }
        public override string ToString() => base.ToString() + "Bus doesn't exist in the system";
    }
    [Serializable]
    public class UserNameAlreadyExistsException : Exception
    {
        public UserNameAlreadyExistsException()
        {
        }
        public UserNameAlreadyExistsException(string messege) : base(messege)
        {

        }
        public UserNameAlreadyExistsException(string message, Exception inner) : base(message, inner)
        {
        }
        public override string ToString() => base.ToString() + "User name is already used in the system";
    }
    [Serializable]
    public class PasswordAlreadyExistsException : Exception
    {
        public PasswordAlreadyExistsException()
        {
        }
        public PasswordAlreadyExistsException(string messege) : base(messege)
        {

        }
        public PasswordAlreadyExistsException(string message, Exception inner) : base(message, inner)
        {
        }
        public override string ToString() => base.ToString() + "Password is already used in the system";
    }
    [Serializable]
    public class UserDoesNotExistException : Exception
    {
        public UserDoesNotExistException()
        {
        }
        public UserDoesNotExistException(string messege) : base(messege)
        {

        }
        public UserDoesNotExistException(string message, Exception inner) : base(message, inner)
        {
        }
        public override string ToString() => base.ToString() + "User is not in the system";
    }
    [Serializable]
    public class StationSearchHistoryAlreadyExistsException : Exception
    {
        public StationSearchHistoryAlreadyExistsException()
        {
        }
        public StationSearchHistoryAlreadyExistsException(string messege) : base(messege)
        {

        }
        public StationSearchHistoryAlreadyExistsException(string message, Exception inner) : base(message, inner)
        {
        }
        public override string ToString() => base.ToString() + "This search is already saved in the system";
    }
    [Serializable]
    public class StationSearchDoesNotExistException : Exception
    {
        public StationSearchDoesNotExistException()
        {
        }
        public StationSearchDoesNotExistException(string messege) : base(messege)
        {

        }
        public StationSearchDoesNotExistException(string message, Exception inner) : base(message, inner)
        {
        }
        public override string ToString() => base.ToString() + "This search is already saved in the system";
    }
}
