using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//bonus
namespace DO
{
   public class UserRide
    {
        public int RideID {get; set;}
        public string User_name {get; set;}
        public int LineID {get; set;}
        public int Boarding_station {get; set;}
        public TimeSpan Boarding {get; set;}
        public int Last_station {get; set;}//maybe change the name
        public TimeSpan Last {get; set;}
        public bool Exists{ get; set; }//also might not need this
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
