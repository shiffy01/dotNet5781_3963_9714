using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
   public class BusNotFoundException:Exception
    {
        public BusNotFoundException()
        {
        }
        public BusNotFoundException(string messege) : base(messege)
        {

        }
        public BusNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }
        public override string ToString() => base.ToString() + "Bus doesn't exist in the system";
    }
}
