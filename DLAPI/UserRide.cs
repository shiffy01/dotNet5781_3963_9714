using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    class UserRide
    {
         int RideID {get; set;}
        string User_name {get; set;}
        int LineID {get; set;}
        int Boarding_station {get; set;}
        TimeSpan Boarding {get; set;}
         int Last_station {get; set;}//maybe change the name
        TimeSpan Last {get; set;}
        public bool Exists{ get; set; }//also might not need this
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
