using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
   public class Bus
    {
        
        public enum Status_ops
        {
            Ready,
            Not_ready
        }
        public Status_ops Status { get; set; }
        public int License { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Last_tune_up { get; set; }
        public int Totalkilometerage { get; set; }     
        public int Kilometerage { get; set; }
        public int Gas { get; set; }
        public bool Exists{ get; set; }

        public override string ToString()
        {
            return this.ToStringProperty();
        }



    }
}
