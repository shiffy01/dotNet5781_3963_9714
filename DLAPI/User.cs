using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    class User
    {
        string User_name{ get; set; }
        string Password{ get; set; }
        bool Access_level{ get; set; }//might change this to enum or something...
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
