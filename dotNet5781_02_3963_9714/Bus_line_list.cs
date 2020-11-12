using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace dotNet5781_02_3963_9714
{
    class Bus_line_list:IEnumerable
    {
        List<Bus_line> busLines;
        private int count;//counts how many busses in list

        public int Count
        {
            get { return Count; }
            set { Count = value; }
        }

        public Bus_line_list()//constructor
        {
            busLines = new List<Bus_line>();
            count = 0;
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return busLines[i];
            }
        }

        public int is_in_list(Bus_line bus)//returns: 1 if list has the same line in the same direction
                                           //         2 if list has the same line in the opposite direction
                                           //         3 if list has same line in both directions
                                           //         4 if list has same line with different route
                                           //         5 if line is not in list at all
        {
            bool same_direction = false;
            bool opposite_direction = false;
            for (int i = 0; i < count; i++)
            {
                if (busLines[i].Line_number == bus.Line_number)//if this line is already in the collection
                {
                    if (busLines[i].First_stop == bus.First_stop && busLines[i].Last_stop == bus.Last_stop)// if it is the same direction
                    {
                        same_direction=true;
                    }

                    if (busLines[i].First_stop == bus.Last_stop &&busLines[i].Last_stop == bus.First_stop)//if they r the same line, but  in the opposite direction//check if its the opposite direction
                        opposite_direction = true;
                    if (!same_direction && !opposite_direction)//if line has same number but different route
                        return 4;
                }
                if (same_direction && opposite_direction)//if list has same line in both directions
                    return 3;
                
            }
            if (same_direction && !opposite_direction)//list has same direction
                return 1;
            if (!same_direction && opposite_direction)//list has opposite direction
                return 2;
            return 5;
        }

        public void add_line(Bus_line b_line)//function adds bus line to collection
        {
            int in_list = is_in_list(b_line);
            if (in_list == 2 || in_list == 5)//if line doesn't exist in list, or only exists in opposite direction,then add this line
            {
                busLines.Add(b_line);
                count++;//update counter
                Console.WriteLine("Line was added to the list");

                return;
            }

            if (in_list == 4)// if list has same line with different route
            {
               throw new ArgumentException("Cannot add this line. It is already in the list and has a different route.");
                
            }
            if (in_list == 1 || in_list == 3)//if line is already in the list
            {
                throw new ArgumentException("Cannot add this line, it is already in the list.");
            }
            
        }

        public void remove_line(int b_line, int first_stop)
        {
            for(int i=0;i<count;i++)
            {
                if(busLines[i].Line_number==b_line)//if it is the same line
                {
                    if(busLines[i].First_stop==first_stop)//if same line and direction
                    {
                        busLines.Remove(busLines[i]);//remove line from the list
                        Console.WriteLine("Line was removed from the list.");
                        count--;
                        return;
                    }
                }
            }
            throw new ArgumentException("Line was not found in list");//if we got to here, we went through the whole list, and didnt return, so we know the line wasn't found
        }

        public List<Bus_line> have_stop(int stop_code)//gets a code for a bus stop, and returns a list of all the lines that stop there
        { 
                List<Bus_line> lines_with_stop = new List<Bus_line>();
                for (int i = 0; i < count; i++)
                {
                    if (busLines[i].on_route(stop_code))
                        lines_with_stop.Add(busLines[i]);
                }
                if (lines_with_stop.Count == 0)
                    throw new ArgumentOutOfRangeException();
                return lines_with_stop;
            
           
        }

        public List<Bus_line> sorted_bus_lines()
        {
            List<Bus_line> sorted_lines = new List<Bus_line>(busLines);// builds new list with the same elements as bus list
            sorted_lines.Sort();//sort this list, based on icomparable interface
            return sorted_lines;
        }

        public Bus_line this[int line_number]//indexer. Returns the bus line with line numebr recieved
        {  
            
                get
                {


                     int index = -1;
                        for (int i = 0; i < count; i++)
                        {
                             if (busLines[i].Line_number == line_number)//if this is the right bus line
                               {
                                 index = i;//save  the index of line
                                 i = count;//leave loop

                               }
                               
                        }
                    
                        if(index==-1)//line wasnt found in loop
                            throw new ArgumentOutOfRangeException();
                  return busLines[index];
                        
                      
               
                    }
                set
                {
                bool found = false;
                    for (int i = 0; i < count; i++)
                    {
                        if (busLines[i].Line_number == line_number)//if this is the right bus line set it to value
                        {
                             busLines[i] = value;
                             found = true;
                    }
                        if(!found)
                            throw new ArgumentOutOfRangeException();
                }
            
        }
     

      


    }
    

    

