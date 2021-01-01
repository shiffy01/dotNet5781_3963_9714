using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
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
}
