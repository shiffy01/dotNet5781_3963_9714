using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace BO
{
    public class LineTiming
    {
        //maybe make an id
        public TimeSpan TripStart{get; set;}
        public int LineID{get; set;}
        public int lineCode {get; set;}
        public string LastStation{get; set;}
        public TimeSpan Timing{get; set;}
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
