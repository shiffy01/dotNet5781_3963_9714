using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_3963_9714
{
    class Bus_line
    {
        private List<Bus_line_stop> stops;
        private int line_number;

        public int Line_number
        {
            get { return line_number; }
            set { line_number = value; }
        }
        private Bus_line_stop first_stop;
       

        public Bus_line_stop First_stop
        {
            get { return first_stop; }
            set { first_stop = value; }
        }
        private Bus_line_stop last_stop;


        public Bus_line_stop Last_stop
        {
            get { return last_stop; }
            set { last_stop = value; }
        }
        private int myVar;

        public int MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }


    }
}
