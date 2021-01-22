using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class AdjacentStations
    {
       public string PairID{get; set;}
        public int Stop_1_code {get; set;}
        public int Stop_2_code {get; set;}
        public double Distance{get; set;}
        public TimeSpan Average_drive_time{get; set;}
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
