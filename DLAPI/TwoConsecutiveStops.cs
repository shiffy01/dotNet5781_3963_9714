using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class TwoConsecutiveStops
    {
        int Stop_1_code {get; set;}
        int Stop_2_code {get; set;}
        double Distance{get; set;}
        TimeSpan Average_drive_time{get; set;}
        public bool Exists{ get; set; }//im not sure if we need this for this kind of class too or only for things like bus and bus stop. might erase this
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
