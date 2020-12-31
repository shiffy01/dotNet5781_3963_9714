using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DS
{
    public static class Config 
    {
        static int busDrivingCounter = 1000000;
        public static int BusDrivingCounter {
            get => ++busDrivingCounter;
        }

        static int busLineCounter = 2000010;
        public static int BusLineCounter => ++busLineCounter;

      
   }
}
