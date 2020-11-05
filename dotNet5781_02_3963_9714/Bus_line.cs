using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_3963_9714
{
    class Bus_line:IComparable
    {
        private List<Bus_line_stop> stops;
        private int line_number;

        public int Line_number
        {
            get { return line_number; }
            set { line_number = value; }
        }
        private int first_stop;
       

        public int First_stop
        {
            get { return first_stop; }
            set { first_stop = value; }
        }
        private int last_stop;


        public int Last_stop
        {
            get { return last_stop; }
            set { last_stop = value; }
        }
        private string area;

        public string Area
        {
            get { return area; }
            set { area = value; }
        }
        public Bus_line(int number, string area)//constructor
        {

        }
        public override string ToString()
        {
            return ("Line number:" + line_number + @"
           Route area:"+area+@"
           Bus stops:
           ");//print list
        }
        public void add_stop(int code)
        {
            //add bus to the route, check if stop1 exists, if its beginning or end of the route, update first/last
        }
        public void remove_stop(int code)
        {
            //remove bus stop from route
        }
        public bool on_route(int code)
        {
            //if bus stop with this code is on the route
        }
        public double distance(int code1, int code2)
        {
            //measure distance between two stops with these codes
        }
        public double travel_time(int code1, int code2)
        {
            //measure time between two stops with these codes
        }
        public Bus_line sub_route(int code1, int code2)
        {
            //returns new route in between these two stops
        }
        public int CompareTo(Bus_line other)//1=bigger, 0=equal, -1=smaller
        {
            
            double time1 = travel_time(first_stop, last_stop);
            double time2 = travel_time(other.first_stop, other.last_stop);
            if (time1 > time2)//
                return 1;


        }

    }
}
