using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void add_line(Bus_line b_line)//function adds bus line to collection
        {
            int count_lines;
            count_lines = 0;
            for(int i=0; i<count; i++)
            {
                if(busLines[i].Line_number==b_line.Line_number)//if this line is already in the collection
                {
                  
                    if(busLines[i].First_stop!=b_line.Last_stop||busLines[i].Last_stop != b_line.First_stop)//if they r the same line, and not in the opposite direction
                    { Console.WriteLine("Line already exists in the list:");
                        return;//leave function
                    }
                    //if we got to here, they r the same line but opposite directions
                    count_lines = 1; ;//this indicates that the list already has both directions of this line
              
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
