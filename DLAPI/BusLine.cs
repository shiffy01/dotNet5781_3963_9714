using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class BusLine
    {
        public int BusID {get; set;}
        public int Bus_line_number {get; set;}
        //address of first and last stops:
        public string Destination {get; set;}
        public string Origin {get; set;}
        //public DateTime First_bus{get; set;}
        //public DateTime Last_bus{get; set;}
        //public TimeSpan Frequency{get; set;}//time between each bus
         public bool Exists{ get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
