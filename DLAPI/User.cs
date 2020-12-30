using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class User
    {
        public string User_name{ get; set; }
        public string Password{ get; set; }
        public bool Access_level{ get; set; }//might change this to enum or something...
        public bool Exists{ get; set; }//also here might not need this
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
