using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DLAPI
{
    public class LicenseExistsException : Exception
    {
        // [Serializable]??

        public int License;
        public LicenseExistsException(int license) : base() => License = license;
        public LicenseExistsException(int license, string messege) : base(messege) => License = license;
        public LicenseExistsException(int license, string message, Exception inner) : base(message, inner) => License = license;
        public override string ToString() => base.ToString() + $", License number: {License} already exists in the system";

    }
}
