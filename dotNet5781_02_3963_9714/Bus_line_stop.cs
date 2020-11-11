using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace dotNet5781_02_3963_9714
{
    class Bus_line_stop:Bus_Stop
    {
        private double distance_from_last_stop;//measured in km

        public double Distance_from_last_stop
        {
            get { return distance_from_last_stop; }
            set { distance_from_last_stop = value; }
        }
        private double time_since_last_stop;//measured in minutes
        public double Time_since_last_stop
        {
            get { return time_since_last_stop; }
            set { time_since_last_stop = value; }
        }
        public Bus_line_stop(int code1):base(code1)
        {
            //distance from the last stop is in the range (0.1, 200)km
            Random rand = new Random(DateTime.Now.Millisecond);
            distance_from_last_stop = rand.Next(1, 2001);
            distance_from_last_stop = (double)distance_from_last_stop / 10;
            //the bus's speed is estimated at 1km a minute
            time_since_last_stop = distance_from_last_stop;
        }
        //inherrits toString from Bus_stop...

    }
}
