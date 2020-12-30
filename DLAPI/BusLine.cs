using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class BusLine
    {
        public int BusID {get; set;}
        public int Bus_line_number {get; set;}
        public bool Exists{ get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
