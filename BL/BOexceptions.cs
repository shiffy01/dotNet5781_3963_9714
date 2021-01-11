using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
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
    public class FrequencyConflictException : Exception
    {
        public FrequencyConflictException()
        {
        }
        public FrequencyConflictException(string messege) : base(messege)
        {

        }
        public FrequencyConflictException(string message, Exception inner) : base(message, inner)
        {
        }
        public override string ToString() => base.ToString() + " ";
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
    public class BusLineAlreadyExistsException : Exception
    {

        public int LineNumber;
        public BusLineAlreadyExistsException(int lineNumber) : base() => LineNumber = lineNumber;
        public BusLineAlreadyExistsException(int lineNumber, string message) : base(message) => LineNumber = lineNumber;
        public BusLineAlreadyExistsException(int lineNumber, string message, Exception inner) : base(message, inner) => LineNumber = lineNumber;
        public override string ToString() => base.ToString() + $",Line number: {LineNumber} is already in the system";
    }
    public class StationAlreadyExistsEOnTheLinexception : Exception
    {

        public int LineNumber;
        public StationAlreadyExistsEOnTheLinexception(int lineNumber, int stationcode) : base() => LineNumber = lineNumber;
        public StationAlreadyExistsEOnTheLinexception(int lineNumber, int stationcode, string message) : base(message) => LineNumber = lineNumber;
        public StationAlreadyExistsEOnTheLinexception(int lineNumber, int stationcode, string message, Exception inner) : base(message, inner) => LineNumber = lineNumber;
        public override string ToString() => base.ToString() + $",Line number: {LineNumber} is already in the system";
    }

}