using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_3963_9714
{
   public class Bus 
    {
      public enum status_ops
        {
            Ready,
            On_the_road,
            Filling_up,
            At_mechanic
        }
        public enum reason
        {
            Not_enough_gas,
            Too_far,// bus will need tune up in the middle of the ride
            Needs_tune_up,//bus needs tune up
            Occupied,//buses status isnt ready
            Sent// bus was sent succesfully
        }
        private status_ops status;
        public status_ops Status
        {
            get { return status; }
            set { status = value; }
        }

        readonly int license;//מספר רישוי
       private DateTime startDate;//תאריך תחילת הפעילות
       
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

       private DateTime last_tune_up;//
      
        public DateTime Last_tune_up
        {
            get { return last_tune_up; }
            set { last_tune_up = value; }
        }

       private int totalMilage;//נסועה כוללת
        public int TotalMilage
        {
            get { return totalMilage; }
            set { totalMilage = value; }
        }
        private int milage;//נסועה מאז הטיפול האחרון    
        public int Milage
        {
            get { return milage; }
            set { milage = value; }
        }
        private int gas;//כמות דלק
        public int Gas 
        { 
            get { return gas; }
            set { gas = value; }
        }

        public Bus(int licenseNumber, DateTime date,  int curr_milage )
        {
            license = licenseNumber;
            startDate = date;
            milage = 0;//busses go through tune_up when they arrive
            totalMilage = curr_milage;//total milage
            gas = 1200;//buses fill up the gas tank when they first arrive
            last_tune_up = DateTime.Now;//busses go through tune_up when they arrive
            status = status_ops.Ready;//bus is ready to leave
        }
              
        public  reason send_bus(int distance)//checks if bus has enough gas, and if its safe to drive.
                                   //if it is, it updates the gas and milage, and returns true. otherwise it returns false and doesn't update anything
        {
           
            if (milage + distance > 20000)//cant send a bus that is dangerous or will become dangerous durring the ride
                return reason.Too_far;
            if (gas - distance < 0)//cant send a bus that doesnt have enough gas
                return reason.Not_enough_gas;
            int diff = ( DateTime.Now- last_tune_up).Days;
            if (diff > 365)//bus needs tune up
                return reason.Needs_tune_up;
            if (status != status_ops.Ready)//if bus is occupied 
                return reason.Occupied;
                //otherwise, update gas and milage
                milage += distance;
            totalMilage += milage;
            gas -= distance;
            return reason.Sent;//bus was sent
            //change state in xaml
        }
        public void refill()//refill tank
        {//change state in xaml
            gas = 1200;
        }
        public void tune_up()//set buses milage back to 0, and resets the date of the last tune_up to today
        {//change state in xaml
            milage = 0;
            last_tune_up = DateTime.Now;
        }
        public void printBus()//prints license plate number
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
            {
                int tmpLicense = license / 100000;//this gives us the first 3 digits of license
                Console.Write(tmpLicense + "-");
                tmpLicense = (license - (tmpLicense * 100000)) / 1000;//this gives us the next 2 digits
                Console.Write(tmpLicense + "-");
                tmpLicense = license % 1000;//this gives us the last 2 digits
                Console.Write(tmpLicense);
            }
           
        }

    }
    
}
