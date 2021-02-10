using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;


namespace BO
{
    public class BusLine
    {
        public int BusID {get; set;}
        public int Bus_line_number {get; set;}
        public IEnumerable<StationOnTheLine> Stations  {get; set;}
        public bool InterCity {get; set;}
        public string Destination {get; set;}
        public string Origin {get; set;}
        public IEnumerable<BusLineTime> Times  {get; set;}
        public override string ToString()
        {
            return this.ToStringProperty();
        }


    }
}
