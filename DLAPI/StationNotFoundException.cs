using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLAPI
{
   public class StationNotFoundException: Exception
    {


        public int Code;
        public StationNotFoundException(int code) : base() => Code = code;
        public StationNotFoundException(int code, string message) : base(message) => Code = code;
        public StationNotFoundException(int code, string message, Exception inner) : base(message, inner) => Code = code;
        public override string ToString() => base.ToString() + $",Station number: {Code} wasn't found in the system";
    }
}
