using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLAPI
{
   public class PairAlreadyExitsException:Exception
    {
        public PairAlreadyExitsException() : base() { }
        public PairAlreadyExitsException(string message) : base(message) { }
        public PairAlreadyExitsException(string message, Exception inner) : base(message, inner) { }
        public override string ToString() => base.ToString() + " Pair already exists in the system";
    }
}
