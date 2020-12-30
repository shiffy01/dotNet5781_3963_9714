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
            
            Buses.Add(new Bus 
            {
                Status = Bus.Status_ops.Ready,
                License = 12345678,
                StartDate = DateTime.Now,
                Last_tune_up = DateTime.Now,
                TotalMilage = 0,
                Milage = 0,
                Gas = 120,
                HasDVD = false,
                HasWifi = false,
                IsAccessible = false
            }) ;
            Buses.Add(new Bus {
                Status = Bus.Status_ops.Ready,
                License = 12375578,
                StartDate = new DateTime(2019, 02, 05),
                Last_tune_up = DateTime.Now,
                TotalMilage = 1200,
                Milage = 0,
                Gas = 120,
                HasDVD = false,
                HasWifi = true,
                IsAccessible = false
            });
            Buses.Add(new Bus {
                Status = Bus.Status_ops.Ready,
                License = 11343098,
                StartDate = new DateTime(2020, 05, 14),
                Last_tune_up = DateTime.Now,
                TotalMilage = 100,
                Milage = 0,
                Gas = 120,
                HasDVD = true,
                HasWifi = true,
                IsAccessible = false
            });
            Buses.Add(new Bus {
                Status = Bus.Status_ops.Ready,
                License = 67544527,
                StartDate = new DateTime(2020, 10, 22),
                Last_tune_up = DateTime.Now,
                TotalMilage = 40,
                Milage = 0,
                Gas = 120,
                HasDVD = true,
                HasWifi = true,
                IsAccessible = true
            });
            Buses.Add(new Bus {
                Status = Bus.Status_ops.Ready,
                License = 76542223,
                StartDate = new DateTime(2018, 08, 30),
                Last_tune_up = DateTime.Now,
                TotalMilage = 0,
                Milage = 0,
                Gas = 120,
                HasDVD = false,
                HasWifi = false,
                IsAccessible = false
            });



            Buses.Add(new Bus 
            {
                Status = Bus.Status_ops.Ready,
                License = 1234567,
                StartDate = new DateTime(2015, 01, 01),
                Last_tune_up = DateTime.Now,
                TotalMilage = 400,
                Milage = 0,
                Gas = 120,
                HasDVD = true,
                HasWifi = false,
                IsAccessible = true

            });
    }
}