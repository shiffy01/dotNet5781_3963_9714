using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    class TwoConsecutiveStops
    {
        int Stop_1_code {get; set;}
        int Stop_2_code {get; set;}
        double Distance{get; set;}
        TimeSpan Average_drive_time{get; set;}
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
