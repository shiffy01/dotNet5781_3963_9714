using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace BO
{
    public class User
    {
        public string UserName{ get; set; }
        public string Password{ get; set; }
        public bool IsManager{ get; set; }
        public IEnumerable<Route> RouteSearches{ get; set; }
        public IEnumerable<int> LineSearches{ get; set; }
        public IEnumerable<int> StationSearches{ get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
