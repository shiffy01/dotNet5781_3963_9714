using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class BusDriving
    {
        public int BusID {get; set;}
        public int Bus_line_number {get; set;}
        //להוסיף
        //מזהה קו שבביצוע
        //i think what this means is that BusID is the id of this bus ride,  and then bus_line_number is the number bus line and "מזהה קו" is the id of the physical bus thats being driven
        public DateTime DepartureTime{get; set;}
        public DateTime TimeDeparted{get; set;}
        public int Previous_stop{get; set;}
        public TimeSpan Time_at_previous_stop{get; set;}
        public DateTime Arrival_at_next_stop{get; set;}
        public bool Exists{ get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }

    }
}
