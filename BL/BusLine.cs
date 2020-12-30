using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;


namespace BO
{
    class BusLine
    {
        int BusID {get; set;}
        int Bus_line_number {get; set;}
        IEnumerable<BusStation> Stations  {get; set;}
        bool InterCity {get; set;}
        string Destination {get; set;}
        string Origin {get; set;}
        public bool Exists{ get; set; }
        DateTime First_bus{get; set;}
        DateTime Last_bus{get; set;}
        TimeSpan Frequency{get; set;}//time between each bus
        public override string ToString()
        {
            return this.ToStringProperty();
        }


    }
}
