using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_3963_9714
{
    class Bus
    {
        readonly int license;//מספר רישויpr
         DateTime startDate;//תאריך תחילת הפעילות
        double milage;//נסועה
        double gas;//כמות דלק
        bool dangerous;//אמת עם צריך טיפול, ושקר אחרת
        public Bus(int licenceNumber, DateTime date)
        {
               
        }
        public int getLicense()
        {
            return license;
        }
        public DateTime getStartDate()
        {
            return startDate;
        }
        public double getMilage()
        {
            return milage;
        }
        public void setMilage(double newMilage)
        {
            if(newMilage>milage)//milage can only be increased

        }
        public double getGas()
        {
            return gas;
        }
    }
}
