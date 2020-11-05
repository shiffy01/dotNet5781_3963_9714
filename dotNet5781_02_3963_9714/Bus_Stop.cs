using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_3963_9714
{
    class Bus_Stop
    {
        protected int code;//code of bus stop

        public int Code
        {
            get { return code; }
            set { code = value; }
        }
        private double latitude;
       

        public double Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }
        private double longitude;

        public double Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }
        private string address;

        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        public Bus_Stop(int code1)//constructor
        {
            if(code1>999999)//make sure code has 6 or less digits
            code = code1;
            //use rand for long and latitude

        }
        public override string ToString()
        {
            //return base.ToString();
        }



    }
}
