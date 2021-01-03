using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class BusLineStationNotFoundException : Exception
    {
        public int Line_Number;

        public BusLineStationNotFoundException(int line_number) : base() => Line_Number = line_number;
        
        public BusLineStationNotFoundException(int line_number, string messege) : base(messege) => Line_Number = line_number;
      
        public BusLineStationNotFoundException(int line_number, string message, Exception inner) : base(message, inner) => Line_Number = line_number;
      
        public override string ToString() => base.ToString() + $"Line number: {Line_Number} doesn't stop at this station";
  
    }
   
}