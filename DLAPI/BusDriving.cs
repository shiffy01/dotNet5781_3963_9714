using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class BusDriving
    {
        public int BusID {get; set;} //ID of the physical bus that was sent
        public int Bus_line_number {get; set;}
        public DateTime DepartureTime{get; set;} //official departure time
        public DateTime TimeDeparted{get; set;} //time bus actually left
        public int Previous_stop{get; set;} //last stop the bus was at
        public TimeSpan Time_at_previous_stop{get; set;} //time the bus arrived at the last station
        public DateTime Arrival_at_next_stop{get; set;} //time the bus will arrive at the next station
        public bool Exists{ get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }

    }
}
