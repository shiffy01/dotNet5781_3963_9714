﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class BusStation
    {
        public int StationID {get; set;}
        public int Code{ get; set; }
        public double Latitude{get; set;}

        public double Longitude{get; set;}
       
        public string Address{get; set;}
        public bool Exists{ get; set; }

        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
