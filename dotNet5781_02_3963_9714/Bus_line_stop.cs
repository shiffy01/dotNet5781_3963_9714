using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace dotNet5781_02_3963_9714
{
    class Bus_line_stop:Bus_Stop
    {
        private double distance_from_last_stop;

        public double Distance_from_last_stop
        {
            get { return distance_from_last_stop; }
            set { distance_from_last_stop = value; }
        }
        private double time_since_last_stop;
        public double Time_since_last_stop
        {
            get { return time_since_last_stop; }
            set { time_since_last_stop = value; }
        }
        public Bus_line_stop(int code1, Bus_line_stop previous )
        {

        }
        //inherrits toString from Bus_stop...

    }
}
