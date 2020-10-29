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
        double totalMilage;//נסועה כוללת
        double milage;//נסועה מאז הטיפול האחרון       
        double gas;//כמות דלק
        bool dangerous;//אמת עם צריך טיפול, ושקר אחרת
        public Bus(int licenseNumber, DateTime date,  double curr_milage, DateTime inspection )
        {
            license = licenseNumber;
            startDate = date;
            milage = curr_milage;
            totalMilage = curr_milage;
            gas = 1200;//buses fill up the gas tank when they first arrive
            last_inspection = inspection;
            DateTime today = DateTime.Now;
            TimeSpan t = today - last_inspection;//amount of time that passed since last inspection
            if (t.TotalDays >= 365)//if a year has passed
                dangerous = true;//the bus needs to be inspected
            else//less then a year passed
                dangerous = false;//doesnt need inspection yet

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
       
        public double getGas()
        {
            return gas;
        }
        /* void increase_milage(int amount)//increases mileage by amount
        {
            milage += amount;
        }*/
         public  bool send_bus(int distance)//checks if bus has enough gas, and if its safe to drive.
                                   //if it is, it updates the gas and milage, and returns true. otherwise it returns false and doesnt update anything
        {
            if (milage + distance > 20000)//cant send a bus that is dangerous or become dangerous durring the ride
                return false;
            if (gas - distance < 0)//cant send a bus that doesnt have enough gas
                return false;
            //otherwise, update gas and milage
            milage += distance;
            totalMilage += milage;
            gas -= distance;
            if (milage == 20000)//if this ride will cause the milage to go up, then its now danegerous, and needs to be taken in
                dangerous = true;
            return true;//bus was sent
        }
     public void refill()//refill tank
        {
            gas = 1200;
        }
        public void inspection()//set buses milage back to 0, and resets the date of the last inspection to today
        {
            milage = 0;
            DateTime today = DateTime.Now;
            last_inspection = today;
        }
        public void printBus()
        {

           
            if (startDate.Year < 2018)//license plates from before 2018 have 7 digits
            {
                int tmpLicense = license / 100000;//this gives us the first 2 digits of license
                Console.Write(tmpLicense + "-");
                tmpLicense = (license - (tmpLicense * 100000))/100;//this gives us the next 3 digits
                Console.Write(tmpLicense + "-");
                tmpLicense = license % 100;//this gives us the last 2 digits
                Console.Write(tmpLicense);
            }
            else
            //license plates from after 2018 have 8 digits
            {//change to 8 digit!!!!!!!
                int tmpLicense = license / 100000;//this gives us the first 2 digits of license
                Console.Write(tmpLicense + "-");
                tmpLicense = (license - (tmpLicense * 100000)) / 100;//this gives us the next 3 digits
                Console.Write(tmpLicense + "-");
                tmpLicense = license % 100;//this gives us the last 2 digits
                Console.Write(tmpLicense);
            }
            Console.WriteLine("mileage:" + milage);
        }

    }
    
}
