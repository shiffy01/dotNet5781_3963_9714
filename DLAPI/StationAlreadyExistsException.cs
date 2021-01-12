using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class StationAlreadyExistsException: Exception
    {
        public int Code;
        public StationAlreadyExistsException(int code) : base() => Code = code;
        public StationAlreadyExistsException(int code, string message) : base(message) => Code = code;
        public StationAlreadyExistsException(int code, string message, Exception inner) : base(message, inner) => Code = code;
        public override string ToString() => base.ToString() + $",Station with code: {Code} already exists in the system";
    }
}
