using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class BusLineStationNotFoundException : Exception
    {
        public BusLineStationNotFoundException()
        {
        }
        public BusLineStationNotFoundException(string messege) : base(messege)
        {

        }
        public BusLineStationNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }
        public override string ToString() => base.ToString() + "Bus line doesn't exist in the system";
    }
}