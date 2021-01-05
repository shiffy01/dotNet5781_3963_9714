using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;



namespace BO
{
    public class BusStation
    {
        public int StationID {get; set;}
        public int Code{ get; set; }
        public IEnumerable<BusLine> Lines{ get; set; }
        public double Latitude{ get; set; }
        public double Longitude{ get; set; }
        public string Street{ get; set; }
        public string City{ get; set; }
        public bool Exists{ get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
