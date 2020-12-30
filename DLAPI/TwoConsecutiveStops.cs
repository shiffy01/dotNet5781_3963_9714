using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class TwoConsecutiveStops
    {
        public int Stop_1_code {get; set;}
        public int Stop_2_code {get; set;}
        public double Distance{get; set;}
        public TimeSpan Average_drive_time{get; set;}
        public bool Exists{ get; set; }//im not sure if we need this for this kind of class too or only for things like bus and bus stop. might erase this
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
