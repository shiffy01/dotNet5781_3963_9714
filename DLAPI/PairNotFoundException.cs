using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLAPI
{
    public class PairNotFoundException:Exception
    {


        public PairNotFoundException() : base() { }
        public PairNotFoundException(string message) : base(message) { }
        public PairNotFoundException(string message, Exception inner) : base(message, inner) { }
        public override string ToString() => base.ToString() + "Pair not found in system";
    }
}
