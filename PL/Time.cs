using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public class Time
    {
        public List<string> Hours()
        {
            List<string> list = new List<string>();
            for (int i = 23; i >= 0; i--)
            {
                string str="";
                if (i < 10)
                    str += "0";
                str += i;
                list.Add(str);
            }
            return list;
        }
        public List<string> Minutes()
        {
            List<string> list = new List<string>();
            for (int i = 59; i >= 0; i--)
            {
                string str = "";
                if (i < 10)
                    str += "0";
                str += i;
                list.Add(str);
            }
            return list;
        }
    }
}
