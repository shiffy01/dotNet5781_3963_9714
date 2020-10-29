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
        DateTime last_inspection;//
        double milage;//נסועה
        double gas;//כמות דלק
        bool dangerous;//אמת עם צריך טיפול, ושקר אחרת
        public Bus(int licenseNumber, DateTime date,  double curr_milage )
        {
            /*if (date.Year < 2018)//if the bus bus made before 2018, its license must be 7 digits
                if (licenseNumber > 9999999 || licenseNumber < 1000000)//license is valid
                    license = licenseNumber;// then set it
                else//license invalid
                    Console.WriteLine("Invalid. License must have 7 digits");
            //save the new license
            else//if the bus bus made after 2018, its license must be 8 digits
            {
                if (licenseNumber > 99999999 || licenseNumber < 10000000)//license is valid
                    license = licenseNumber;// then set it
                else//license invalid
                    Console.WriteLine("Invalid. License must have 8 digits");
                //save the new license
            }*/
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
