﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class BusDeparted
    {
        public int BusID {get; set;}
        public DateTime Start {get; set;}
        public int Frequency{get; set;}
        public DateTime End {get; set;}
        public bool Exists{ get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }

    }
}