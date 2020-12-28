using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    class BusDeparted
    {
         int BusID {get; set;}
        DateTime Start {get; set;}
        int Frequency{get; set;}
        DateTime End {get; set;}
        public override string ToString()
        {
            return this.ToStringProperty();
        }

    }
}
