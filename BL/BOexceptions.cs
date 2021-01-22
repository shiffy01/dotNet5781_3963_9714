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
    public class StationALreadyExistsException:Exception
    {
        public int Code;
        public StationALreadyExistsException(int code) : base() => Code = code;
        public StationALreadyExistsException(int code, string message) : base(message) => Code = code;
        public StationALreadyExistsException(int code, string message, Exception inner) : base(message, inner) => Code = code;
        public override string ToString() => base.ToString() + $",StationCode: {Code} is already in the system";
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
    [Serializable]
    public class StationAlreadyExistsOnTheLinexception : Exception
    {

        public int Code;
        public StationAlreadyExistsOnTheLinexception(int code, int stationcode) : base() => Code = code;
        public StationAlreadyExistsOnTheLinexception(int code, int stationcode, string message) : base(message) => Code = code;
        public StationAlreadyExistsOnTheLinexception(int code, int stationcode, string message, Exception inner) : base(message, inner) => Code = code;
        public override string ToString() => base.ToString() + $",Code number: {Code} already goes through that stop";
    }
    [Serializable]
    public class StationDoesNotExistOnTheLinexception : Exception
    {

        public int Code;
        public StationDoesNotExistOnTheLinexception(int code, int line) : base() => Code = code;
        public StationDoesNotExistOnTheLinexception(int code, int line, string message) : base(message) => Code = code;
        public StationDoesNotExistOnTheLinexception(int code, int line, string message, Exception inner) : base(message, inner) => Code = code;
        public override string ToString() => base.ToString() + $",Code number: {Code} already goes through that stop";
    }
    [Serializable]
    public class NeedDistanceException : Exception
    {

        public int CodeA;
        public int CodeB;
        public int CodeC;
        public bool FirstPair;
        public bool SecondPair;
        public NeedDistanceException(int codeA, int codeB, int codeC, bool first, bool second) : base()
        {
            CodeA = codeA;
            CodeB = codeB;
            CodeC = codeC;
            FirstPair = first;
            SecondPair = second;
        }
        public NeedDistanceException(int codeA, int codeB, int codeC, bool first, bool second, string message) : base(message)
        {
            CodeA = codeA;
            CodeB = codeB;
            CodeC = codeC;
            FirstPair = first;
            SecondPair = second;
        }
        public NeedDistanceException(int codeA, int codeB, int codeC, bool first, bool second, string message, Exception inner) : base(message, inner)
        {
            CodeA = codeA;
            CodeB = codeB;
            CodeC = codeC;
            FirstPair = first;
            SecondPair = second;
        }
        public override string ToString() => base.ToString() + $",Codes: {CodeA} and {CodeB} already have a distance between them";
    }
    [Serializable]
    public class PairAlreadyExistsException : Exception
    {
        public PairAlreadyExistsException() : base() { }
        public PairAlreadyExistsException(string message) : base(message) { }
        public PairAlreadyExistsException(string message, Exception inner) : base(message, inner) { }
        public override string ToString() => base.ToString() + " Pair already exists in the system";
    }
    [Serializable]
    public class PairNotFoundException : Exception
    {
        public PairNotFoundException() : base() { }
        public PairNotFoundException(string message) : base(message) { }
        public PairNotFoundException(string message, Exception inner) : base(message, inner) { }
        public override string ToString() => base.ToString() + " Pair not found in the system";
    }
    [Serializable]
    public class InvalidPlaceException : Exception
    {
        public InvalidPlaceException()
        {
        }
        public InvalidPlaceException(string messege) : base(messege)
        {

        }
        public InvalidPlaceException(string message, Exception inner) : base(message, inner)
        {
        }
        public override string ToString() => base.ToString() + " ";
    }


}