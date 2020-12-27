using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace DL
{
    static class Cloning
    {
      
     public  static WindDirection Clone(this WindDirection original) //דרך ראשונה
        {
            WindDirection target = new WindDirection();
            target.direction = original.direction;
            return target;
        }
    }
}