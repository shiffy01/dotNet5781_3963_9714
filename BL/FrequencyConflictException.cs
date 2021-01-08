using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    class FrequencyConflictException:Exception
    {
        public FrequencyConflictException()
        {
        }
        public FrequencyConflictException(string messege) : base(messege)
        {

        }
        public FrequencyConflictException(string message, Exception inner) : base(message, inner)
        {
        }
        public override string ToString() => base.ToString() + " ";
    }
}
