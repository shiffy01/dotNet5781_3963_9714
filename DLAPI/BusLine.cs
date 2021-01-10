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
        public bool Exists{ get; set; }
        //public string Area { get; set; }
        //i added some more things here: i think theyre useful but check again
        public bool InterCity {get; set;}
        public string Destination {get; set;}
        public string Origin {get; set;}
        public DateTime First_bus{get; set;}
        public DateTime Last_bus{get; set;}
        public TimeSpan Frequency{get; set;}//time between each bus
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
