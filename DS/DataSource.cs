using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;


namespace DS
{
    public static class DataSource
    {
        private static List<Bus> buses = new List<Bus>();//does this need to be an observerable collection? it does errors if i try..
        public static List<Bus> Buses { get => buses; }
        public static void initialize_buses()
        {
            
                    Buses.Add(new Bus {
                    Status = Bus.Status_ops.Ready,
                    License = 12345678,
                    StartDate = DateTime.Now,
                    Last_tune_up = DateTime.Now,
                    TotalMilage = 0,
                    Milage = 0,
                    Gas = 0,
                    HasDVD = false,
                    HasWifi = false,
                    IsAccessible = false



                }) ;
            Buses.Add(new Bus {
                Status = Bus.Status_ops.Ready,
                License = 1234567,
                StartDate = DateTime.Now,
                Last_tune_up = DateTime.Now,
                TotalMilage = 0,
                Milage = 0,
                Gas = 0,
                HasDVD = true,
                HasWifi = false,
                IsAccessible = true

            }
    }
}