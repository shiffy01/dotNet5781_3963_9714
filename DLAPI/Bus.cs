﻿using System;
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
            On_the_road,
            Filling_up,
            At_mechanic
        }
        public Status_ops Status { get; set; }
        public int License { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Last_tune_up { get; set; }
        public int Totalkilometerage
        { get; set; }
        public int kilometerage { get; set; }
        public int Gas { get; set; }
        public bool IsAccessible { get; set; }
        public bool HasWifi { get; set; }
        public bool HasDVD { get; set; }
        public bool Exists{ get; set; }

        public override string ToString()
        {
            return this.ToStringProperty();
        }



    }
}
