using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class BusLineStation
    {
        public string pairID{get; set;}
        public int StationID {get; set;}
        public int BusLineNumber { get; set; }
        public int LineID{ get; set; }
        public int Number_on_route{ get; set; }
        public bool Exists{ get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
