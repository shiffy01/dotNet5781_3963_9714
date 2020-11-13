using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//done
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
        public static List<Bus_line_stop> stop_list = new List<Bus_line_stop>();//this list saves all the bus stops that exist
        public static Bus_line_stop make_bus_line_stop(int code)//checks if the stop already exists. if so it returns it, otherwise it builds a new one and returns it
        {
            for (int i=0; i< stop_list.Count; i++)//go through the list of stops
            {
                if (stop_list[i].Code == code)//if found
                    return stop_list[i];//return it
            }
            Bus_line_stop new_stop= new Bus_line_stop(code);//if not found in the whole list, build a new stop
            stop_list.Add(new_stop);
            return new_stop;
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
        //inherits toString from Bus_stop...

    }
}
