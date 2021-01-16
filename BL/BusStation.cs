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
        public int Code{ get; set; }        
        public double Latitude{ get; set; }
        public double Longitude{ get; set; }
         public string Name{get; set;}       
        public string Address{ get; set; }
         public string City{get; set;}
        public IEnumerable<BusLine> Lines{ get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
