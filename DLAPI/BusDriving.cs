using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    class BusDriving
    {
          int BusID {get; set;}
          int Bus_line_number {get; set;}
        //להוסיף
        //מזהה קו שבביצוע
        DateTime DepartureTime{get; set;}
        DateTime TimeDeparted{get; set;}
        int Previous_stop{get; set;}
        TimeSpan Time_at_previous_stop{get; set;}
        DateTime Arrival_at_next_stop{get; set;}
        public bool Exists{ get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }

    }
}
