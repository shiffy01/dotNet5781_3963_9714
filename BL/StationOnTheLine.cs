using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace BO
{
    public class StationOnTheLine
    {
         public int StationID {get; set;}
        public int Code{ get; set; }
        public double Latitude{ get; set; }
        public double Longitude{ get; set; }
        public string Address{ get; set; }
        public int Number_on_route{ get; set; }
        public double Distance_from_last_stop{ get; set; }
        public bool Exists{ get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
