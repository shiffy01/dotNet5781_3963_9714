﻿using System;
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
        public int Line_Number;

        public BusLineStationNotFoundException(int line_number) : base() => Line_Number = line_number;

        public BusLineStationNotFoundException(int line_number, string messege) : base(messege) => Line_Number = line_number;

        public BusLineStationNotFoundException(int line_number, string message, Exception inner) : base(message, inner) => Line_Number = line_number;

        public override string ToString() => base.ToString() + $"Line number: {Line_Number} doesn't stop at this station";

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

}