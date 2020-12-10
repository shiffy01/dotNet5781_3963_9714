using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_3963_9714
{
   public class Bus 
    {
      public enum Status_ops
        {
            Ready,
            On_the_road,
            Filling_up,
            At_mechanic
        }
        
        private Status_ops status;
        public Status_ops Status
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
        private int number_of_passengers;

        public int Number_of_passengers
        {
            get { return number_of_passengers; }
            set { number_of_passengers = value; }
        }

        private bool isAccessable;//האם האוטובוס נגיש לנכים
        public bool IsAccessable
        {
            get { return IsAccessable; }
            set { IsAccessable = value; }
        }
        private bool hasWifi;//האם יש באוטובוס וויי - פיי
        public bool HasWifi
        {
            get { return hasWifi; }
            set { hasWifi = value; }
        }
        private bool hasDVD;// dvd האם יש באוטובוס 
        public bool HasDVD
        {
            get { return hasDVD; }
            set { hasDVD= value; }
        }

        public Bus(int licenseNumber, DateTime date,  int curr_milage, int passengers, bool accessable, bool wifi, bool dvd )
        {
            license = licenseNumber;
            startDate = date;
            milage = 0;//busses go through tune_up when they arrive
            totalMilage = curr_milage;//total milage
            gas = 1200;//buses fill up the gas tank when they first arrive
            last_tune_up = DateTime.Now;//busses go through tune_up when they arrive
            status = Status_ops.Ready;//bus is ready to leave
            number_of_passengers = passengers;
            isAccessable = accessable;
            hasWifi = wifi;
            hasDVD = dvd;
        }
              
        public  string Send_bus(int distance)//checks if bus has enough gas, and if its safe to drive.
                                   //if it is, it updates the gas and milage, and returns true. otherwise it returns false and doesn't update anything
        {
           
            if (milage + distance > 20000)//cant send a bus that is dangerous or will become dangerous durring the ride
                return "The bus needs a tune up in order to go that far";
            if (gas - distance < 0)//cant send a bus that doesnt have enough gas
                return "Not enough gas";
            int diff = ( DateTime.Now- last_tune_up).Days;
            if (diff > 365)//bus needs tune up
                return "Bus needs a tune up";
            if (status != Status_ops.Ready)//if bus is occupied 
                return "Bus is occupied";
                //otherwise, update gas and milage
                milage += distance;
            totalMilage += milage;
            gas -= distance;
            return "Bus sent";//bus was sent
            
        }
        public void Refill()//refill tank
        {//change state in xaml
            gas = 1200;
        }
        public void Tune_up()//set buses milage back to 0, and resets the date of the last tune_up to today
        {//change state in xaml
            milage = 0;
            last_tune_up = DateTime.Now;
        }
        public string PrintBus()//this function returns a string of the license plate with the -
        {

            string finalLicense;
            if (startDate.Year < 2018)//license plates from before 2018 have 7 digits
            {
                int tmpLicense = license / 100000;//this gives us the first 2 digits of license
                finalLicense=(tmpLicense + "-");
                tmpLicense = (license - (tmpLicense * 100000))/100;//this gives us the next 3 digits
                finalLicense+=(tmpLicense + "-");
                tmpLicense = license % 100;//this gives us the last 2 digits
                finalLicense+=(tmpLicense);
            }
            else
            //license plates from after 2018 have 8 digits
            {
                int tmpLicense = license / 100000;//this gives us the first 3 digits of license
                finalLicense=(tmpLicense + "-");
                tmpLicense = (license - (tmpLicense * 100000)) / 1000;//this gives us the next 2 digits
                finalLicense+=(tmpLicense + "-");
                tmpLicense = license % 1000;//this gives us the last 2 digits
                finalLicense+=(tmpLicense);
            }
            return finalLicense;
        }
        public override string ToString()
        {
            string lp = PrintBus();
            int milageLeft = 20000 - milage;
            string toString = "License Plate: " + lp;
            toString += @"
                         Status: "+status;
            toString += @"
                         Total milage: " +totalMilage;
            toString += @"
                         " + milageLeft+ " kilometers left till next tune up" ;
            toString += @"
                         Last tune up: " + last_tune_up;
            toString += @"
                         Gas left: " + gas;
            toString += @"
                          " + number_of_passengers+ " seats";
            if(isAccessable)
                toString += @"
                             *Accessable";
            if (hasWifi)
                toString += @"
                             *Wifi";
            if (hasDVD)
                toString += @"
                             *DVD player";
            return toString;
        }

    }
    
}
