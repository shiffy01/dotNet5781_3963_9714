using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//done
namespace dotNet5781_02_3963_9714
{
    class Bus_Stop
    {
        protected int code;//code of bus stop

        public int Code
        {
            get { return code; }
            set { code = value; }
        }
        private double latitude;
       

        public double Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }
        private double longitude;

        public double Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }
        private string address;

        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        public Bus_Stop(int code1)//constructor
        {
            if (code1 > 999999 || code1 <= 0)//make sure code has 6 or less digits
                throw new ArgumentOutOfRangeException("Cannot add this stop because the bus stop code exceeds six digits");
            code = code1;
            //the longitude and latitude are raffled numbers inside the borders of Israel:
            //latitude[31, 33.3], longitude[34.3, 35.5]
            Random rand = new Random(DateTime.Now.Millisecond);
            latitude = rand.Next(310, 334);
            latitude = (double)longitude / 10;
            longitude = rand.Next(343, 356);
            longitude = (double)longitude / 10;

        }
        public override string ToString()
        {
            //return code and measurements for this bus stop
            string ans = "Bus Station Code: " + code + ", " + latitude + "°N " + longitude + "°E";
            return ans;
        }
    }
}
