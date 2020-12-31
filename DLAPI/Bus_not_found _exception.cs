using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    class Bus_not_found_exception:Exception
    {
        public Bus_not_found_exception()
        {
        }
        public Bus_not_found_exception(string messege) : base(message)
        {
        }
    }
}
