using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class LineFrequency
        //since the frequency of the line can change throughout the day, we need a separate class to keep track of each time interval for the same line.
    {
        public string ID{get; set;}  //ID of the time interval
        public int LineID {get; set;}
        public DateTime Start {get; set;}
        public DateTime End {get; set;}
        public TimeSpan Frequency{get; set;} //frequency of the bus during this interval
        public bool Exists{ get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }

    }
}
