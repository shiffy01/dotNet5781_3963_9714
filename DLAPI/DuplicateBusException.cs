using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DLAPI
{
    public class DuplicateBusException : Exception
    {
        // [Serializable]??

        public int License;
        public DuplicateBusException(int license) : base() => License = license;
        public DuplicateBusException(int license, string messege) : base(messege) => License = license;
        public DuplicateBusException(int license, string message, Exception inner) : base(message, inner) => License = license;
        public override string ToString() => base.ToString() + $", Bus with License number: {License} already exists in the system";

    }
}
