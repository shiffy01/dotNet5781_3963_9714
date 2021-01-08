using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    class Address
    {
        string City{get; set;}
        string Street{get; set;}
        int Street_number{get; set;}
        public override string ToString()
        {
            return Street + " " + Street_number + ", " + City;
        }
    }
}
