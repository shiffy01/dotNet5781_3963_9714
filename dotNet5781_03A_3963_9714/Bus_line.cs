using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime;
//done
namespace dotNet5781_02_3963_9714
{
    public class Bus_line : IComparable
    {
        public List<Bus_line_stop> stops;
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
        private string area;//General\North\South\Center\Jerusalem

        public string Area
        {
            get { return area; }
            set { area = value; }
        }
        public Bus_line(int number, string area1, Bus_line_stop first, Bus_line_stop last)//only valid codes for first and last stops are sent to this function
        {
            line_number = number;
            area = area1;
            first_stop = first.Code;
            last_stop = last.Code;
            stops = new List<Bus_line_stop>();
            //add the first and last stops to the list of stops
            stops.Add(first);
            stops.Add(last);
        }
        public override string ToString()
        {
            //ans contains all the information to be printed:
            string ans = "Line number:" + line_number + @"
           Route area:" + area + @"
           Bus stops:
           ";
            for (int i = 0; i < stops.Count; i++)//for every stop on the line
            {
                ans += stops[i].Code;//print the stop number
                ans += @"
           ";//enter line
            }
            return ans;
        }
        public void add_stop(int code, Bus_line_stop stop)//code=the code of the bus stop to add the new stop after. only a valid code for the bus stop STOP is sent to this function
        {
                if(!on_route(code)&&code!=0)//if the stop to add after is not on the bus route (and is not 0)
                throw new ArgumentException("cannot add after a stop that does not exist");
            if (on_route(stop.Code))//if the bus stop is already on the route
                {
                    Console.WriteLine("This stop is already on this route");
                    return;
                }
            if (code == 0)//if code is 0 it means that this stop should be added at the beginning of the route
            {
                stops.Insert(0, stop);
                first_stop = stop.Code;//update first stop
                return;
            }

                int i;
                for (i = 0; i < stops.Count; i++)//find the place to add after
                    if (stops[i].Code == code)
                        break;//leave the loop, i is saved and is the position to add to

                    stops.Insert(i + 1, stop);
                    if (stops[stops.Count-1].Code == code)//update last stop
                        last_stop = stop.Code;
        }
        public void remove_stop(int code)
        {
            if (!on_route(code))
                throw new ArgumentException("this bus stop is not on the route");
            int i;
            for (i = 0; i < stops.Count; i++)
                if (stops[i].Code == code)//if this is the place with the right code
                    break;//leave the loop, i is saved as the position to remove from
            stops.RemoveAt(i);//removes element in index i
        }
        public bool on_route(int code)
        {
            bool ans = false;//so far not found
            for (int i = 0; i < stops.Count; i++)
                if (stops[i].Code == code)//if found
                    ans = true;
            return ans;

        }
        public double distance(int code1, int code2)
        {
            //the search function will throw an exception if the codes are invalid, and the program will catch the exception in the main
                search(code1);
                search(code2);

            double dis = 0;//distance
            int i;
            for (i = 0; i < stops.Count; i++)
                if (stops[i].Code == code1)//when found
                    break;
            int first = i;//first stop in the sub route
            for (i = 0; i < stops.Count; i++)
                if (stops[i].Code == code2)//when found
                    break;
            int last = i;//last stop in the sub route

                for (i = first + 1; i < last; i++)
                    dis += stops[i].Distance_from_last_stop;//keep adding the distances between stops for all the stops in between them
                return dis;
        }
        public double travel_time(int code1, int code2)
        {
            //the time is equal to the distance because the distance is measured in km and the time is measured in minutes,
            //and the speed is 60km per hour-- 1km per minute
            return this.distance(code1, code2);        
        }
        public Bus_line sub_route(int code1, int code2)//fix this!
        {
            //the search function will throw an exception if the codes are invalid, and the program will catch the exception in the main
            Bus_line_stop first=search(code1);
            Bus_line_stop last=search(code2);
            Bus_line sub = new Bus_line(this.Line_number, this.Area, first, last);

            int i;
            for (i = 0; i < stops.Count; i++)
                if (stops[i].Code == code1)//when found
                    break;
            int first_index = i;//index of first stop in the sub route
            for (i = 0; i < stops.Count; i++)
                if (stops[i].Code == code2)//when found
                    break;
            int last_index = i;//index of last stop in the sub route
            if(first_index>last_index)//both codes were found but the route from one to the other is in the wrong direction
                throw new ArgumentException();
            //this code uses functions that throw exceptions, but it only sends valid codes so there is no need for try/catch
            //add all the stops in between first and last to the sub route:
            int most_recent = code1;//this is the stop to add the next one after
                for (i = first_index + 1; i < last_index; i++)
                {
                    sub.add_stop(most_recent, stops[i]);//add the stop
                    most_recent = stops[i].Code;//the next one will be added after this stop
                }
                return sub;
            

        }
        int IComparable.CompareTo(Object other)//1=bigger, 0=equal, -1=smaller
        {
            //only sends valid codes to the travel time function
            double time1 = this.travel_time(first_stop, last_stop);
            double time2 = this.travel_time(((Bus_line)other).first_stop, ((Bus_line)other).last_stop);
            if (time1 > time2)
                return 1;
            if (time1 < time2)
                return -1;
            return 0;
        }
        public Bus_line_stop search(int code)//returns bus stop with this code
        {
            for(int i=0; i<stops.Count; i++)
            {
                if (stops[i].Code == code)
                    return stops[i];
            }
            throw new ArgumentException();
        }

    }
    
}
