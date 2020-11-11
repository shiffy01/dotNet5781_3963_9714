﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime;
//finished this except the exceptions.
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
        public Bus_line(int number, string area1)//constructor
        {
            line_number = number;
            area = area1;
            first_stop = 0;//no stops yet
            last_stop = 0;//no stops yet
            stops = new List<Bus_line_stop>();
        }
        public override string ToString()
        {
            //ans contains all the information to be printed:
            string ans="Line number:" + line_number + @"
           Route area:"+area+@"
           Bus stops:
           ";
            for (int i = 0; i < stops.Count; i++)//for every stop on the line
            {
                ans+=stops[i].Code;//print the stop number
                ans += @"
                ";//enter line
            }
            return ans;
        }
        public void add_stop(int code, Bus_line_stop stop)//code=the code of the bus stop to add the new stop after
        {
            //add bus to the route, check if stop1 exists, if its beginning or end of the route, update first/last
            if (code == 0)//if code is 0 it means that this stop should be added at the beginning of the route
            { 
                stops.Insert(0, stop);
                first_stop = stop.Code;//update first stop
            }
            else
            {
                    int i;
                    for (i = 0; i < stops.Count; i++)//find the place to add after
                        if (stops[i].Code == code)//if this is the place with the right code
                            break;//leave the loop, i is saved and is the position to add to
                    if (stops.Count == i && !(stops[stops.Count].Code == code))//if the loop got to the end and the last place is not the place to add to
                        Console.WriteLine("error");//needs an exception!!

                    else
                    {
                        stops.Insert(i + 1, stop);//maybe print success messege (or return true and do that in the main)
                        if (stops[stops.Count].Code == code)//update last stop
                            last_stop = stop.Code;
                    }
            }
        }
        public void remove_stop(int code)
        {
            int i;
            for(i=0; i<stops.Count; i++)
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
            //returns the distance between the two stops with these codes
            double dis = 0;//distance
            int i;
            for (i = 0; i < stops.Count; i++)
                if (stops[i].Code == code1)//if found
                    break;
            int first = i;//first stop in the sub route
            for (i = 0; i < stops.Count; i++)
                if (stops[i].Code == code2)//if found
                    break;
            int last = i;//last stop in the sub route
            if (last == stops.Count || first == stops.Count)//if either first or last reached the end of the list
            {
                if ((last == stops.Count && first == stops.Count) && (stops[stops.Count].Code != code1 || stops[stops.Count].Code != code2))
                {//if both of them reached the end of the list but they are not both equal to the code in the last place
                    Console.WriteLine("error");//exception?
                }
                if (stops[stops.Count].Code != code1 && stops[stops.Count].Code != code2)//if one of them reached the end of the list but neither of the codes equal the on eat the end of the list
                    Console.WriteLine("error");//exception?
                return -1;//what to return if there is an error?
            }
            else//if code1 and code2 are valid
            {
                for (i = first + 1; i < last; i++)
                    dis += stops[i].Distance_from_last_stop;//keep adding the distances between stops for all the stops in between them
                return dis;
            }
        }
        public double travel_time(int code1, int code2)
        {
            //returns the time it takes to get from one stop to the other
            double time = 0;//distance
            int i;
            for (i = 0; i < stops.Count; i++)
                if (stops[i].Code == code1)//if found
                    break;
            int first = i;//first stop in the sub route
            for (i = 0; i < stops.Count; i++)
                if (stops[i].Code == code2)//if found
                    break;
            int last = i;//last stop in the sub route
            if (last == stops.Count || first == stops.Count)//if either first or last reached the end of the list
            {
                if ((last == stops.Count && first == stops.Count) && (stops[stops.Count].Code != code1 || stops[stops.Count].Code != code2))
                {//if both of them reached the end of the list but they are not both equal to the code in the last place
                    Console.WriteLine("error");//exception?
                }
                if (stops[stops.Count].Code != code1 && stops[stops.Count].Code != code2)//if one of them reached the end of the list but neither of the codes equal the on eat the end of the list
                    Console.WriteLine("error");//exception?
                return -1;//what to return if there is an error?
            }
            else//if code1 and code2 are valid
            {
                for (i = first + 1; i < last; i++)
                    time += stops[i].Distance_from_last_stop;//keep adding the time between stops for all the stops in between them
                return time;
            }
        }
        public Bus_line sub_route(int code1, int code2)
        {
            int i;
            Bus_line sub = new Bus_line(line_number, area);//is the bus line number same as the whole route's number...? might not be
            for (i = 0; i < stops.Count; i++)
                if (stops[i].Code == code1)//if found
                    break;
            int first = i;//first stop in the sub route
            for (i = 0; i < stops.Count; i++)
                if (stops[i].Code == code2)//if found
                    break;
            int last = i;//last stop in the sub route
            if (last == stops.Count || first == stops.Count)//if either first or last reached the end of the list
            {
                if ((last == stops.Count && first == stops.Count) && (stops[stops.Count].Code != code1 || stops[stops.Count].Code != code2))
                {//if both of them reached the end of the list but they are not both equal to the code in the last place
                    Console.WriteLine("error");//exception?
                }
                if (stops[stops.Count].Code != code1 && stops[stops.Count].Code != code2)//if one of them reached the end of the list but neither of the codes equal the on eat the end of the list
                    Console.WriteLine("error");//exception?
                return sub;//what to return if one of the codes is invalid
            }
            else
            {
                sub.add_stop(0, stops[first]);//add first stop
                int most_recent = stops[first].Code;//this is the stop to add the next one after
                for (i = first + 1; i <= last; i++)
                {
                    sub.add_stop(most_recent, stops[i]);//addd the stop
                    most_recent = stops[i].Code;//the next one will add after this stop
                }
                return sub;
            }
          
        }
         int IComparable.CompareTo(Object other)//1=bigger, 0=equal, -1=smaller
        {
            
            double time1 = travel_time(first_stop, last_stop);
            double time2 = travel_time(((Bus_line)other).first_stop, ((Bus_line)other).last_stop);
            if (time1 > time2)
                return 1;
            if (time1 < time2)
                return -1;
            return 0;
        }

    }
}
