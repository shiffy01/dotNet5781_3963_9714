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
                    if (busLines[i].First_stop == bus.First_stop || busLines[i].Last_stop == bus.Last_stop)// if it is the same direction
                    {
                        same_direction=true;
                    }

                    if (busLines[i].First_stop != bus.Last_stop || busLines[i].Last_stop != bus.First_stop)//if they r the same line, and not in the opposite direction//check if its the opposite direction
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
                bool line_already_found = false;
                for (int i = 0; i < count; i++)
                {
                    if (busLines[i].Line_number == b_line.Line_number)//if this line is already in the collection
                    {
                        if (line_already_found)//if this line is in 
                            Console.WriteLine("Line already exists in the list:");


                        if (busLines[i].First_stop != b_line.Last_stop || busLines[i].Last_stop != b_line.First_stop)//if they r the same line, and not in the opposite direction
                        { Console.WriteLine("Line already exists in the list:");
                            return;//leave function
                        }
                        //if we got to here, they r the same line but opposite directions

                        line_already_found = true; ;//this indicates that the list already has both directions of this line

                    }

                }

            }
        
        public Bus_line_list()//constructor
        {
            count = 0;
        }

        public IEnumerator GetEnumerator()
        {
            for(int i=0; i<Count;i++)
            {
                yield return busLines[i];
            }
        }


    }
    

    
}
