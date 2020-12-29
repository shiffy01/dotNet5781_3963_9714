using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;



namespace BO
{
    class BusStation
    {
        int StationID {get; set;}
        public int Code{ get; set; }
        IEnumerable<BusLine> Lines{ get; set; }
        double Latitude{ get; set; }
        double Longitude{ get; set; }
        string Street{ get; set; }
        string City{ get; set; }
        public bool Exists{ get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
