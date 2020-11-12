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
        private List<Bus_line> busLines;
        private int count;//counts how many busses in list
        public int Count
        {
            get { return Count; }
            set { Count = value; }
        }
        public string is_in_list(Bus_line bus)
        {
           
            for (int i = 0; i < count; i++)
            {
                if (busLines[i].Line_number == bus.Line_number)//if this line is already in the collection
                {
                    if (busLines[i].First_stop == bus.First_stop || busLines[i].Last_stop == bus.Last_stop)// if it is the same direction
                    {
                        return "Line is al;
                    }
                    if (busLines[i].First_stop != bus.Last_stop || busLines[i].Last_stop != bus.First_stop)//if they r the same line, and not in the opposite direction//check if its the opposite direction

                }
            }
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
        }

        public Bus_line_list()//constructor
        {
            this.Count = 0;
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
