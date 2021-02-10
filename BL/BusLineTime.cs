using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class BusLineTime
    {
        public DateTime Start { get; set; }
        public DateTime End{ get; set; }
        public TimeSpan Frequency{ get; set; }
    }
}
