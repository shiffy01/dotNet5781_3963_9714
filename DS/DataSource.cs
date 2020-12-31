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
        public static List<Bus> Buses
        {
            get => buses;
        }
        public static void initialize_buses()
        {

            Buses.Add(new Bus {
                Status = Bus.Status_ops.Ready,
                License = 12345678,
                StartDate = DateTime.Now,
                Last_tune_up = DateTime.Now,
                TotalMilage = 0,
                Milage = 0,
                Gas = 1200,
                HasDVD = true,
                HasWifi = true,
                IsAccessible = false
            });//12345678
            Buses.Add(new Bus {
                Status = Bus.Status_ops.Ready,
                License = 12375578,
                StartDate = new DateTime(2019, 02, 05),
                Last_tune_up = DateTime.Now,
                TotalMilage = 1200,
                Milage = 0,
                Gas = 1200,
                HasDVD = false,
                HasWifi = true,
                IsAccessible = false
            });//12375578
            Buses.Add(new Bus {
                Status = Bus.Status_ops.Ready,
                License = 11343098,
                StartDate = new DateTime(2020, 05, 14),
                Last_tune_up = DateTime.Now,
                TotalMilage = 13500,
                Milage = 0,
                Gas = 1200,
                HasDVD = true,
                HasWifi = true,
                IsAccessible = false
            });//11343098
            Buses.Add(new Bus {
                Status = Bus.Status_ops.Ready,
                License = 67544527,
                StartDate = new DateTime(2020, 10, 22),
                Last_tune_up = DateTime.Now,
                TotalMilage = 4098,
                Milage = 0,
                Gas = 1200,
                HasDVD = true,
                HasWifi = true,
                IsAccessible = true
            });//67544527
            Buses.Add(new Bus {
                Status = Bus.Status_ops.Ready,
                License = 76542223,
                StartDate = new DateTime(2018, 08, 30),
                Last_tune_up = DateTime.Now,
                TotalMilage = 1000000,
                Milage = 0,
                Gas = 1200,
                HasDVD = true,
                HasWifi = true,
                IsAccessible = false
            });//76542223

            Buses.Add(new Bus {
                Status = Bus.Status_ops.Ready,
                License = 1234567,
                StartDate = new DateTime(2015, 01, 01),
                Last_tune_up = DateTime.Now,
                TotalMilage = 4000000,
                Milage = 0,
                Gas = 1200,
                HasDVD = true,
                HasWifi = true,
                IsAccessible = true
            });//1234567
            Buses.Add(new Bus {
                Status = Bus.Status_ops.Ready,
                License = 7154110,
                StartDate = new DateTime(2016, 01, 01),
                Last_tune_up = DateTime.Now,
                TotalMilage = 905088,
                Milage = 0,
                Gas = 1200,
                HasDVD = true,
                HasWifi = false,
                IsAccessible = true
            });//7154110
            Buses.Add(new Bus {
                Status = Bus.Status_ops.Ready,
                License = 8765432,
                StartDate = new DateTime(2017, 02, 05),
                Last_tune_up = DateTime.Now,
                TotalMilage = 620000,
                Milage = 0,
                Gas = 1200,
                HasDVD = true,
                HasWifi = false,
                IsAccessible = true
            });//8765432
            Buses.Add(new Bus {
                Status = Bus.Status_ops.Ready,
                License = 7564325,
                StartDate = new DateTime(2012, 12, 27),
                Last_tune_up = DateTime.Now,
                TotalMilage = 10000000,
                Milage = 0,
                Gas = 1200,
                HasDVD = true,
                HasWifi = true,
                IsAccessible = false
            });//7564325
            Buses.Add(new Bus {
                Status = Bus.Status_ops.Ready,
                License = 67541527,
                StartDate = new DateTime(2020, 09, 18),
                Last_tune_up = DateTime.Now,
                TotalMilage = 40,
                Milage = 0,
                Gas = 1200,
                HasDVD = true,
                HasWifi = true,
                IsAccessible = true
            });//67541527

            Buses.Add(new Bus {
                Status = Bus.Status_ops.Ready,
                License = 76542823,
                StartDate = new DateTime(2018, 08, 30),
                Last_tune_up = DateTime.Now,
                TotalMilage = 3049330,
                Milage = 0,
                Gas = 1200,
                HasDVD = true,
                HasWifi = true,
                IsAccessible = false
            });//76542821
            Buses.Add(new Bus {
                Status = Bus.Status_ops.Ready,
                License = 2490171,
                StartDate = new DateTime(2015, 04, 21),
                Last_tune_up = DateTime.Now,
                TotalMilage = 4998778,
                Milage = 0,
                Gas = 1200,
                HasDVD = true,
                HasWifi = false,
                IsAccessible = true
            });//2490171
            Buses.Add(new Bus {
                Status = Bus.Status_ops.Ready,
                License = 47321385,
                StartDate = DateTime.Now,
                Last_tune_up = DateTime.Now,
                TotalMilage = 0,
                Milage = 0,
                Gas = 1200,
                HasDVD = true,
                HasWifi = true,
                IsAccessible = true
            });//47321385
            Buses.Add(new Bus {
                Status = Bus.Status_ops.Ready,
                License = 80368632,
                StartDate = new DateTime(2019, 02, 05),
                Last_tune_up = DateTime.Now,
                TotalMilage = 920098,
                Milage = 0,
                Gas = 1200,
                HasDVD = true,
                HasWifi = true,
                IsAccessible = true
            });//80368632
            Buses.Add(new Bus {
                Status = Bus.Status_ops.Ready,
                License = 21609875,
                StartDate = new DateTime(2020, 05, 14),
                Last_tune_up = DateTime.Now,
                TotalMilage = 13500,
                Milage = 0,
                Gas = 1200,
                HasDVD = true,
                HasWifi = true,
                IsAccessible = false
            });//21609875

            Buses.Add(new Bus {
                Status = Bus.Status_ops.Ready,
                License = 60544521,
                StartDate = DateTime.Now,
                Last_tune_up = DateTime.Now,
                TotalMilage = 0,
                Milage = 0,
                Gas = 1200,
                HasDVD = true,
                HasWifi = true,
                IsAccessible = true,
                Exists = true
            });//60544521
            Buses.Add(new Bus {
                Status = Bus.Status_ops.Ready,
                License = 36542873,
                StartDate = DateTime.Now,
                Last_tune_up = DateTime.Now,
                TotalMilage = 0,
                Milage = 0,
                Gas = 1200,
                HasDVD = true,
                HasWifi = true,
                IsAccessible = true,
                Exists = true
            });//36542873
            Buses.Add(new Bus {
                Status = Bus.Status_ops.Ready,
                License = 18932468,
                StartDate = DateTime.Now,
                Last_tune_up = DateTime.Now,
                TotalMilage = 0,
                Milage = 0,
                Gas = 1200,
                HasDVD = true,
                HasWifi = true,
                IsAccessible = true,
                Exists = true
            });//18932468
            Buses.Add(new Bus {
                Status = Bus.Status_ops.Ready,
                License = 93254329,
                StartDate = DateTime.Now,
                Last_tune_up = DateTime.Now,
                TotalMilage = 0,
                Milage = 0,
                Gas = 1200,
                HasDVD = true,
                HasWifi = true,
                IsAccessible = true,
                Exists = true
            });//93254329
            Buses.Add(new Bus {
                Status = Bus.Status_ops.Ready,
                License = 53281972,
                StartDate = DateTime.Now,
                Last_tune_up = DateTime.Now,
                TotalMilage = 0,
                Milage = 0,
                Gas = 1200,
                HasDVD = true,
                HasWifi = true,
                IsAccessible = true,
                Exists = true
            });//53281972

            Buses.Add(new Bus {
                Status = Bus.Status_ops.Ready,
                License = 84372803,
                StartDate = new DateTime(2021, 01, 01),
                Last_tune_up = DateTime.Now,
                TotalMilage = 0,
                Milage = 0,
                Gas = 1200,
                HasDVD = true,
                HasWifi = true,
                IsAccessible = false,
                Exists = true
            });//84372803
            Buses.Add(new Bus {
                Status = Bus.Status_ops.Ready,
                License = 95344034,
                StartDate = new DateTime(2020, 10, 22),
                Last_tune_up = new DateTime(2020, 10, 22),
                TotalMilage = 2000,
                Milage = 2000,
                Gas = 0,
                HasDVD = true,
                HasWifi = true,
                IsAccessible = true,
                Exists = true
            });//95344034
            Buses.Add(new Bus {
                Status = Bus.Status_ops.Ready,
                License = 21234567,
                StartDate = new DateTime(2017, 10, 30),
                Last_tune_up = new DateTime(2019, 10, 30),
                TotalMilage = 400000,
                Milage = 20030,
                Gas = 120,
                HasDVD = true,
                HasWifi = true,
                IsAccessible = true,
                Exists = true
            });//21234567
            Buses.Add(new Bus {
                Status = Bus.Status_ops.Ready,
                License = 48392412,
                StartDate = new DateTime(2019, 01, 10),
                Last_tune_up = new DateTime(2019, 01, 10),
                TotalMilage = 19005,
                Milage = 19005,
                Gas = 574,
                HasDVD = true,
                HasWifi = true,
                IsAccessible = true,
                Exists = true
            });//48392412

        }
    }
}