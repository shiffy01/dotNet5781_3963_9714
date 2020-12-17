﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.ComponentModel;
using System.Runtime.CompilerServices;
//do converter then finish תהליכונים

namespace dotNet5781_01_3963_9714
{

    public class Bus : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public enum Status_ops
        {
            Ready,
            On_the_road,
            Filling_up,
            At_mechanic
        }

        public Status_ops Status { get; set; }
        public int License { get; set; }
        string licenseFull;
        public string LicenseFull { get { return PrintBus(); } }//full format with dashes
        public DateTime StartDate { get; set; }
        public DateTime Last_tune_up { get; set; }
        public int TotalMilage { get; set; }
        public int Milage { get; set; }
        public int Gas { get; set; }
        public bool Number_of_passengers40 { get; set; }
        public bool Number_of_passengers50 { get; set; }
        public bool Number_of_passengers60 { get; set; }
        public bool IsAccessible { get; set; }
        public bool HasWifi { get; set; }
        public bool HasDVD { get; set; }
        public string seconds;
        public string Seconds
        {
            get { return seconds; }
            set
            {
                seconds = value;
                if (PropertyChanged != null)
                {
                    
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("seconds"));
                }

            }
        }//messege of how much time until the bus is ready to drive
        public bool buttonVisibility;
        public bool ButtonVisibility
        {
            get { return buttonVisibility; }
            set
            {
                buttonVisibility = value;
                if (PropertyChanged != null)
                {
                    
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("buttonVisibility"));
                }
            }
        }//visibility of progressbar
        public int Time { get; set; }//real time: 1 second=10 minutes
        public int progressb;
        public int Progressb
        {
            get { return progressb; }
            set
            {
                progressb = value;
                if (PropertyChanged != null)
                {
                   
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("progressb"));
                }

            }
        }//percent of progress bar
        public bool canDrive;
        public bool CanDrive
        {
            get { return canDrive; }
            set
            {
                canDrive = value;
                if (PropertyChanged != null)
                {
                  
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("canDrive"));
                }
            }
        }//enable drive button
        public bool canGas;
        public bool CanGas
        {
            get { return canGas; }
            set
            {
                canGas = value;
                if (PropertyChanged != null)
                {
                   
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("canGas"));
                }
            }
        }//enable gas button
        public bool canTuneUp;
        public bool CanTuneUp
        {
            get {

                if ((DateTime.Now - Last_tune_up).Days > 356)
                    return false;
                else
                return canTuneUp; 
                }
            set
            {
                if ((DateTime.Now - Last_tune_up).Days > 356)
                    canTuneUp= false;
                else
                    canTuneUp = value;
                if (PropertyChanged != null)
                {  
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("canTuneUp"));
                }
            }
        }//enable tune up button

      
        public Bus(int licenseNumber, DateTime date, int curr_milage, int passengers, bool accessable, bool wifi, bool dvd)
        {
            License = licenseNumber;
            
            StartDate = date;
            Milage = 0;//busses go through tune_up when they arrive
            TotalMilage = curr_milage;//total milage
            Gas = 1200;//buses fill up the gas tank when they first arrive
            Last_tune_up = DateTime.Now;//busses go through tune_up when they arrive
            Status = Status_ops.Ready;//bus is ready to leave
            CanDrive = true;
            CanGas = true;
            CanTuneUp = true;
            Seconds = "Ready";
            progressb = 0;
            if (passengers == 50)
            {
                Number_of_passengers50 = true;
                Number_of_passengers60 = false;
                Number_of_passengers40 = false;
            }
            if (passengers == 40)
            {
                Number_of_passengers40 = true;
                Number_of_passengers60 = false;
                Number_of_passengers50 = false;
            }
            if (passengers == 60)
            {
                Number_of_passengers60 = true;
                Number_of_passengers50 = false;
                Number_of_passengers40 = false;
            }
            IsAccessible = accessable;
            HasWifi = wifi;
            HasDVD = dvd;
        }

        public string Send_bus(int distance)//checks if bus has enough gas, and if its safe to drive.
                                            //if it is, it updates the gas and milage, and returns true. otherwise it returns false and doesn't update anything
        {

            if (Milage + distance > 20000)//cant send a bus that is dangerous or will become dangerous durring the ride
                return "Bus needs a tune up in order to go that far";
            if (Gas - distance < 0)//cant send a bus that doesnt have enough gas
                return "Bus doesnt have enough gas to go that far";
            int diff = (DateTime.Now - Last_tune_up).Days;
            if (diff > 365)//bus needs tune up
                return "Cannot drive this bus, it needs a tune up";
            if (Status != Status_ops.Ready)//if bus is occupied 
                return "Bus is occupied";
            //otherwise, update gas and milage
            Milage += distance;
            TotalMilage += Milage;
            Gas -= distance;
            return "Bus sent";//bus was sent

        }
        public void Refill()//refill tank
        {
            Gas = 1200;
        }
        public void Tune_up()//set buses milage back to 0, and resets the date of the last tune_up to today
        {
            Milage = 0;
            Last_tune_up = DateTime.Now;
        }
      
       
        public string PrintBus()//this function returns a string of the license plate with the dashes
        {

            string finalLicense;
            if (License<10000000)//license plate hase 7 digits
            {
                int tmpLicense = License / 100000;//this gives us the first 2 digits of license
                finalLicense = (" "+tmpLicense + "-");
                tmpLicense = License % 100000;
                if (tmpLicense<100)// if it has 2 or less digits
                {
                    finalLicense += "000-";
                }
                else
                {
                    if (tmpLicense < 1000)//if it has 3 digits
                        finalLicense += "00";
                    if (tmpLicense < 10000 && tmpLicense >999)//if it has 4 digits
                        finalLicense += "0";
                    finalLicense += tmpLicense / 100;
                    finalLicense +="-";
                }

                tmpLicense = tmpLicense % 100;
                if (tmpLicense == 0)
                    finalLicense += "00";
                else
                {
                    if (tmpLicense < 10)//if it has one digit
                        finalLicense += "0";
                    finalLicense += tmpLicense;
                }
               
            }
            else
            //license plate has 8 digits
            {
                int tmpLicense = License / 100000;//this gives us the first 3 digits of license
                finalLicense = (tmpLicense + "-");
                tmpLicense = License % 100000;
                if(tmpLicense<1000)//if it has 3 digits or less
                {
                    finalLicense += "00-";
                }
                else
                {
                    if (tmpLicense < 10000)// if it has 4 digits
                        finalLicense += "0";
                    finalLicense +=( tmpLicense / 1000)+"-";
                    tmpLicense = tmpLicense % 1000;//gets last 3
                }
                if (tmpLicense == 0)
                    finalLicense += "000";
                else
                {
                    if (tmpLicense < 10)// has only 1 digit
                        finalLicense += "00";
                    if (tmpLicense < 100 && tmpLicense >9)//has only 2 digits
                        finalLicense += "0";
                    finalLicense += tmpLicense;
                }                             
            }
            finalLicense += " ";
            return finalLicense;
        }
       public int Num_of_passengers()
        {

            if (Number_of_passengers60 == true)
                return 60;
            if (Number_of_passengers50 == true)
                return 50;
            return 40;
        }
        public override string ToString()
        {
            string lp = PrintBus();
            int milageLeft = 20000 - Milage;
            string toString = @"   
                 License Plate: " + lp;
            toString += @"

                 Status: " + Status;
            toString += @"

                 Total mileage: " + TotalMilage;
            toString += @"

                 ";
            if ((DateTime.Now-Last_tune_up).Days > 356||milageLeft==0)
                toString += "THIS BUS NEEDS A TUNE-UP";
            else
                toString += milageLeft + " kilometers left till next tune up";
            toString += @"

                 Last tune up: " + Last_tune_up;
            toString += @"

                 Gas left: " + Gas;
            toString += @"

                 " + Num_of_passengers() + " seats";
            if (IsAccessible)
                toString += @"

                 *Accessible";
            if (HasWifi)
                toString += @"
                 *Wifi";
            if (HasDVD)
                toString += @"
                 *DVD player";
            return toString;
        }

    }

}
