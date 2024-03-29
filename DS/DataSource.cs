﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
//need to fix ids here.. to start from millions. bus line station->bus line number is unique
//running numbers!!
namespace DS
{
    public  static partial class DataSource
    {
       
        private static List<Bus> buses = new List<Bus>();//does this need to be an observerable collection? it does errors if i try..
        public static List<Bus> Buses{ get => buses; }
      
         private static List<BusStation> stations = new List<BusStation>();
        public static List<BusStation> Stations { get => stations; }
        private static List<BusLine> lines = new List<BusLine>();
        public static List<BusLine> Lines { get => lines; }
        private static List<BusLineStation> line_stations = new List<BusLineStation>();
        public static List<BusLineStation> Line_stations{ get => line_stations; }
        private static List<AdjacentStations> two_stops = new List<AdjacentStations>();
        public static List<AdjacentStations> Two_stops{ get => two_stops; }
        //private static List<BusDeparted> departed = new List<BusDeparted>();
        //public static List<BusDeparted> Departed{ get => departed; }
        public static double Distance_Between_Two_Stops(BusStation station1, BusStation station2)
        {
            if (station1 == null || station2 == null)
            {
                return 0;

            }
            return Math.Sqrt((station1.Latitude - station2.Latitude) * (station1.Latitude - station2.Latitude) +
                             (station1.Longitude - station2.Longitude) * (station1.Longitude - station2.Longitude));
        }
        public static void initialize_Stations()
        {
            Stations.Add(new BusStation {
                Code = 73,
                Name = "שדרות גולדה מאיר/המשורר אצ''ג",
                Address = "רחוב:שדרות גולדה מאיר עיר: ירושלים ",
                
                Latitude = 31.825302,
                Longitude = 35.188624            
            }) ;//שדרות גולדה מאיר/המשורר אצ''ג 73
            Stations.Add(new BusStation 
            {
                Code = 76,
                Name = "בית ספר צור באהר בנות/אלמדינה אלמונוורה",
                Address = "רחוב:אל מדינה אל מונאוורה עיר: ירושלים",
                
                Latitude = 31.738425,
                Longitude = 35.228765
            });//76בית ספר צור באהר בנות/אלמדינה אלמונוורה
            Stations.Add(new BusStation 
            {
            Code = 77,
                Name = "בית ספר אבן רשד/אלמדינה אלמונוורה",
                Address = "רחוב:אל מדינה אל מונאוורה עיר: ירושלים ",
               
                Latitude = 31.738676,
                Longitude = 35.226704
            });// 77 בית ספר אבן רשד/אלמדינה אלמונוורה
            Stations.Add(new BusStation 
            {
                Code = 78,
                Name = "שרי ישראל/יפו",
                Address = "רחוב:שדרות שרי ישראל 15 עיר: ירושלים",
                
                Latitude = 31.789128,
                Longitude = 35.206146
            });//שרי ישראל/יפו 78
            Stations.Add(new BusStation
            {
                Code = 83,
                Name = "בטן אלהווא/חוש אל מרג",
                Address = "רחוב:בטן אל הווא עיר: ירושלים",
              
                Latitude = 31.766358,
                Longitude = 35.240417
            });//83 בטן אלהווא/חוש אל מרג
            Stations.Add(new BusStation 
            {
                Code = 84,
                Name = "מלכי ישראל/הטורים",
                Address = " רחוב:מלכי ישראל 77 עיר: ירושלים ",
           
                Latitude = 31.790758,
                Longitude = 35.209791
            });//84 מלכי ישראל/הטורים
            Stations.Add(new BusStation 
            {
                Code = 85,
                Name = "בית ספר לבנים/אלמדארס",
                Address = "רחוב:אלמדארס עיר: ירושלים",
            
                Latitude = 31.768643,
                Longitude = 35.238509
            });//בית ספר לבנים/אלמדארס 85
            Stations.Add(new BusStation 
            {
                Code = 86,
                Name = "מגרש כדורגל/אלמדארס",
                Address = "רחוב:אלמדארס עיר: ירושלים",
              
                Latitude = 31.769899,
                Longitude = 35.23973
            });//מגרש כדורגל/אלמדארס 86
            Stations.Add(new BusStation {
                Code = 88,
                Name = "בית ספר לבנות/בטן אלהוא",
                Address = " רחוב:בטן אל הווא עיר: ירושלים",
        
                Latitude = 31.767064,
                Longitude = 35.238443
            });//בית ספר לבנות/בטן אלהוא 88
            Stations.Add(new BusStation 
            {
                Code = 89,
                Name = "דרך בית לחם הישה/ואדי קדום",
                Address = " רחוב:דרך בית לחם הישנה עיר: ירושלים ",
             
                Latitude = 31.765863,
                Longitude = 35.247198
            });//דרך בית לחם הישה/ואדי קדום89 
            //10
            Stations.Add(new BusStation 
            {
                Code = 90,
                Name = "גולדה/הרטום",
                Address = "רחוב:דרך בית לחם הישנה עיר: ירושלים",
           
                Latitude = 31.799804,
                Longitude = 35.213021
            });//90 גולדה/הרטום
            Stations.Add(new BusStation 
            {
                Code = 91,
                Name = "דרך בית לחם הישה/ואדי קדום",
                Address = " רחוב:דרך בית לחם הישנה עיר: ירושלים ",
         
                Latitude = 31.765717,
                Longitude = 35.247102
            });//91 דרך בית לחם הישה/ואדי קדום
            Stations.Add(new BusStation 
            {
                Code = 93,
                Name = "חוש סלימה 1",
                Address = " רחוב:דרך בית לחם הישנה עיר: ירושלים",
          
                Latitude = 31.767265,
                Longitude = 35.246594
            });//93 חוש סלימה 
            Stations.Add(new BusStation 
            {
                Code = 94,
                Name = "דרך בית לחם הישנה ב",
                Address = " רחוב:דרך בית לחם הישנה עיר: ירושלים",
           
                Latitude = 31.767084,
                Longitude = 35.246655
            });//94 דרך בית לחם הישנה ב
            Stations.Add(new BusStation
            {
                Code = 95,
                Name = "דרך בית לחם הישנה א",
                Address = " רחוב:דרך בית לחם הישנה עיר: ירושלים",
             
                Latitude = 31.768759,
                Longitude = 31.768759
            });//95 דרך בית לחם הישנה א
            Stations.Add(new BusStation 
            {
                Code = 97,
                Name = "שכונת בזבז 2",
                Address = " רחוב:דרך בית לחם הישנה עיר: ירושלים",
             
                Latitude = 31.77002,
                Longitude = 35.24348
            });//97 שכונת בזבז 2
            Stations.Add(new BusStation 
            {
                Code = 102,
                Name = "גולדה/שלמה הלוי",
                Address = " רחוב:שדרות גולדה מאיר עיר: ירושלים",
              
                Latitude = 31.8003,
                Longitude = 35.208257
            });//גולדה/שלמה הלוי 102
            Stations.Add(new BusStation 
            {
                Code = 103,
                Name = "גולדה/הרטום",
                Address = " רחוב:שדרות גולדה מאיר עיר: ירושלים",
                
                Latitude = 31.8,
                Longitude = 35.214106
            });//גולדה/הרטום 103 
            Stations.Add(new BusStation 
            {
                Code = 105,
                Name = "גבעת משה",
                Address = " רחוב:גבעת משה 2 עיר: ירושלים",
                
                Latitude = 31.797708,
                Longitude = 35.217133
            });// 105 גבעת משה
            Stations.Add(new BusStation 
            {
                Code = 106,
                Name = "גבעת משה",
                Address = " רחוב:גבעת משה 3 עיר: ירושלים",
             
                Latitude = 31.797535,
                Longitude = 35.217057
            });// 106 גבעת משה
            //20
            Stations.Add(new BusStation
            {
                Code = 108,
                Name = "עזרת תורה/עלי הכהן",
                Address = " רחוב:עזרת תורה 25 עיר: ירושלים",
           
                Latitude = 31.797535,
                Longitude = 35.213728
            });//עזרת תורה/עלי הכהן108 
            Stations.Add(new BusStation 
            {
                Code = 109,
                Name = "עזרת תורה/דורש טוב",
                Address = " רחוב:עזרת תורה 21 עיר: ירושלים ",
              
                Latitude = 31.796818,
                Longitude = 35.212936
            });//עזרת תורה/דורש טוב 109
            Stations.Add(new BusStation 
            {
                Code = 110,
                Name = "עזרת תורה/דורש טוב",
                Address = " רחוב:עזרת תורה 12 עיר: ירושלים",
                
                Latitude = 31.796129,
                Longitude = 35.212698
            });//עזרת תורה/דורש טוב110
            Stations.Add(new BusStation 
            {
                Code = 111,
                Name = "יעקובזון/עזרת תורה",
                Address = " רחוב:יעקובזון 1 עיר: ירושלים",
                
                Latitude = 31.794631,
                Longitude = 35.21161
            });//111 יעקובזון/עזרת תורה
            Stations.Add(new BusStation 
            {
                Code = 112,
                Name = "יעקובזון/עזרת תורה",
                Address = " רחוב:יעקובזון עיר: ירושלים",
           
                Latitude = 31.79508,
                Longitude = 35.211684
            });//112יעקובזון/עזרת תורה
            Stations.Add(new BusStation 
            {
                Code = 113,
                Name = "זית רענן/אוהל יהושע",
                Address = " רחוב:זית רענן 1 עיר: ירושלים",
           
                Latitude = 31.796255,
                Longitude = 35.211065
            });// 113 זית רענן/אוהל יהושע
            Stations.Add(new BusStation 
            {
                Code = 115,
                Name = "זית רענן/תורת חסד",
                Address = " רחוב:זית רענן עיר: ירושלים",
                
                Latitude = 31.798423,
                Longitude = 35.209575
            });//זית רענן/תורת חסד 115
            Stations.Add(new BusStation
            {
                Code = 116,
                Name = "זית רענן/תורת חסד",
                Address = " רחוב:הרב סורוצקין 48 עיר: ירושלים ",
               
                Latitude = 31.798689,
                Longitude = 35.208878
            });// 116 זית רענן/תורת חסד
            Stations.Add(new BusStation
            {
                Code = 117,
                Name = "קרית הילד/סורוצקין",
                Address = " רחוב:הרב סורוצקין עיר: ירושלים",
                
                Latitude = 31.799165,
                Longitude = 35.206918
            });//117 קרית הילד/סורוצקין
            Stations.Add(new BusStation 
            {
                Code = 119,
                Name = "סורוצקין/שנירר",
                Address = " רחוב:הרב סורוצקין 31 עיר: ירושלים",
            
                Latitude = 31.797829,
                Longitude = 35.205601
            });//סורוצקין/שנירר119
            //30
            Stations.Add(new BusStation 
            {
                Code = 1485,
                Name = "שדרות נווה יעקוב/הרב פרדס ",
                Address = "רחוב: שדרות נווה יעקוב עיר:ירושלים ",
           
                Latitude = 31.840063,
                Longitude = 35.240062

            });//1485 שדרות נווה יעקוב/הרב פרדס
            Stations.Add(new BusStation 
            {
                Code = 1486,
                Name = "מרכז קהילתי /שדרות נווה יעקוב",
                Address = "רחוב:שדרות נווה יעקוב ירושלים עיר:ירושלים ",
            
                Latitude = 31.838481,
                Longitude = 35.23972
            });//מרכז קהילתי /שדרות נווה יעקוב 1486
            Stations.Add(new BusStation
            {
                Code = 1487,
                Name = " מסוף 700 /שדרות נווה יעקוב ",
                Address = "חוב:שדרות נווה יעקב 7 עיר: ירושלים ",
             
                Latitude = 31.837748,
                Longitude = 35.231598
            });// 1487 מסוף 700 /שדרות נווה יעקוב
            Stations.Add(new BusStation 
            {
                Code = 1488,
                Name = " הרב פרדס/אסטורהב ",
                Address = "רחוב:מעגלות הרב פרדס עיר: ירושלים רציף ",
          
                Latitude = 31.840279,
                Longitude = 35.246272
            });//הרב פרדס/אסטורה 1488
            Stations.Add(new BusStation 
            {
                Code = 1490,
                Name = "הרב פרדס/צוקרמן ",
                Address = "רחוב:מעגלות הרב פרדס 24 עיר: ירושלים ",
              
                Latitude = 31.843598,
                Longitude = 35.243639
            });//הרב פרדס/צוקרמן1490 
            Stations.Add(new BusStation 
            {
                Code = 1491,
                Name = "ברזיל ",
                Address = "רחוב:ברזיל 14 עיר: ירושלים",
           
                Latitude = 31.766256,
                Longitude = 35.173
            });//ברזיל 1491 
            Stations.Add(new BusStation 
            {
                Code = 1492,
                Name = "בית וגן/הרב שאג ",
                Address = "רחוב:בית וגן 61 עיר: ירושלים ",
             
                Latitude = 31.76736,
                Longitude = 35.184771
            });//1492 בית וגן/הרב שאג
            Stations.Add(new BusStation
            {
                Code = 1493,
                Name = "בית וגן/עוזיאל ",
                Address = "רחוב:בית וגן 21 עיר: ירושלים ",
               
                Latitude = 31.770543,
                Longitude = 35.183999
            });//בית וגן/עוזיאל 1493 
            Stations.Add(new BusStation
            {
                Code = 1494,
                Name = " קרית יובל/שמריהו לוין ",
                Address = "רחוב:ארתור הנטקה עיר: ירושלים ",
               
                Latitude = 31.768465,
                Longitude = 35.178701
            });//קרית יובל/שמריהו לוין 1494
            Stations.Add(new BusStation 
            {
                Code = 1510,
                Name = " קורצ'אק / רינגלבלום ",
                Address = "רחוב:יאנוש קורצ'אק 7 עיר: ירושלים",
               
                Latitude = 31.759534,
                Longitude = 35.173688
            });//1510 קורצ'אק / רינגלבלום
            //40
            Stations.Add(new BusStation 
            {
                Code = 1511,
                Name = " טהון/גולומב ",
                Address = "רחוב:יעקב טהון עיר: ירושלים ",
              
                Latitude = 31.761447,
                Longitude = 35.175929
            });//טהון/גולומב 1511 
            Stations.Add(new BusStation 
            {
                Code = 1512,
                Name = "הרב הרצוג/שח''ל ",
                Address = "רחוב:הרב הרצוג עיר: ירושלים רציף",
            
                Latitude = 31.761447,
                Longitude = 35.199936
            });//הרב הרצוג/שח''ל 1512
            Stations.Add(new BusStation 
            {
                Code = 1514,
                Name = "פרץ ברנשטיין/נזר דוד ",
                Address = " רחוב:פרץ ברנשטיין 37 עיר: ירושלים",
             
                Latitude = 31.759186,
                Longitude = 35.189336
            });//פרץ ברנשטיין/נזר דוד 1514
            Stations.Add(new BusStation 
            {
                Code = 1518,
                Name = "פרץ ברנשטיין/נזר דוד",
                Address = " רחוב:פרץ ברנשטיין 56 עיר: ירושלים ",
           
                Latitude = 31.759121,
                Longitude = 35.189178
            });// 1518 פרץ ברנשטיין/נזר דוד
            Stations.Add(new BusStation
            {
                Code = 1522,
                Name = "מוזיאון ישראל/רופין",
                Address = " רחוב:דרך רופין עיר: ירושלים ",
              
                Latitude = 31.774484,
                Longitude = 35.204882
            });//מוזיאון ישראל/רופין 1522
            Stations.Add(new BusStation 
            {
                Code = 1523,
                Name = "הרצוג/טשרניחובסקי",
                Address = " רחוב:הרב הרצוג עיר: ירושלים ",
                
                Latitude = 31.769652,
                Longitude = 35.208248
            });//הרצוג/טשרניחובסקי 1523  
            Stations.Add(new BusStation 
            {
                Code = 1524,
                Name = "רופין/שד' הזז",
                Address = " רחוב:הרב הרצוג עיר: ירושלים ",
               
                Latitude = 31.769652,
                Longitude = 35.208248
            });//רופין/שד' הזז 1524
            Stations.Add(new BusStation 
            {
                Code = 121,
                Name = "מרכז סולם/סורוצקין ",
                Address = " רחוב:הרב סורוצקין 13 עיר: ירושלים",
            
                Latitude = 31.796033,
                Longitude = 35.206094
            });// 121 מרכז סולם/סורוצקין
            Stations.Add(new BusStation
            {
                Code = 123,
                Name = "אוהל דוד/סורוצקין ",
                Address = " רחוב:הרב סורוצקין 9 עיר: ירושלים",
              
                Latitude = 31.794958,
                Longitude = 35.205216
            });//אוהל דוד/סורוצקין 123 
            Stations.Add(new BusStation
            {
                Code = 122,
                Name = "מרכז סולם/סורוצקין ",
                Address = " רחוב:הרב סורוצקין 28 עיר: ירושלים",
               
                Latitude = 31.79617,
                Longitude = 35.206158
            });//מרכז סולם/סורוצקין 122   
            //50
        }
        public static void initialize_Lines()
        {
            Lines.Add(new BusLine 
            {
                BusID = 2000000,
                Bus_line_number = 32,
                Exists = true
            }); 
            Lines.Add(new BusLine 
            {
                BusID = 2000001,
                Bus_line_number = 45,
                Exists = true
            });
            Lines.Add(new BusLine 
            {
                BusID = 2000002,
                Bus_line_number = 36,
                Exists = true

            });
            Lines.Add(new BusLine 
            {
                BusID = 2000003,
                Bus_line_number=19,
                Exists = true
            });
            Lines.Add(new BusLine 
            {
                BusID = 2000004,
                Bus_line_number = 56,
                Exists = true
            });
            Lines.Add(new BusLine 
            {
                BusID = 2000005,
                Bus_line_number = 28,
                Exists = true
            });
            Lines.Add(new BusLine 
            {
                BusID = 2000006,
                Bus_line_number = 69,
                Exists = true
            });
            Lines.Add(new BusLine 
            {
                BusID = 2000007,
                Bus_line_number = 58,
                Exists = true
            });
            Lines.Add(new BusLine 
            {
                BusID = 2000008,
                Bus_line_number = 92,
                Exists = true
            });
            Lines.Add(new BusLine
            {
                BusID = 2000009,
                Bus_line_number = 55,
                Exists = true
            });      
        }
        public static void initialize_Bus_line_stations()
        {
            #region line number 1 //this one's a route that makes sense
            Line_stations.Add(new BusLineStation {
                StationID = 1522,
                LineID = 2000000,
                BusLineStationID = 1522.ToString()+ 2000000.ToString(),
                Number_on_route = 1,
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 1523,
                LineID = 2000000,
                BusLineStationID = 1523.ToString() + 2000000.ToString(),
                Number_on_route = 2,
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 1512,
                LineID = 2000000,
                Number_on_route=3,
                BusLineStationID = 1512.ToString() + 2000000.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 1511,
                LineID = 2000000,
                Number_on_route = 4,
                BusLineStationID = 1511.ToString() + 2000000.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 1510,
                LineID = 2000000,
                Number_on_route = 5,
                BusLineStationID = 1510.ToString() + 2000000.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 1491,
                LineID = 2000000,
                Number_on_route = 6,
                BusLineStationID = 1491.ToString() + 2000000.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 1494,
                LineID = 2000000,
                Number_on_route = 7,
                BusLineStationID = 1494.ToString() + 2000000.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 1493,
                LineID = 2000000,
                Number_on_route = 8,
                BusLineStationID = 1493.ToString() + 2000000.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 1492,
                LineID = 2000000,
                Number_on_route = 9,
                BusLineStationID = 1492.ToString() + 2000000.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 1514,
                LineID = 2000000,
                Number_on_route = 10,
                BusLineStationID = 1514.ToString() + 2000000.ToString(),
                Exists = true
            });
            #endregion
            #region line number 2
            Line_stations.Add(new BusLineStation {
                StationID = 73,
                LineID = 2000001,
                Number_on_route = 1,
                BusLineStationID = 73.ToString() + 2000001.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 76,
                LineID = 2000001,
                Number_on_route = 2,
                BusLineStationID = 76.ToString() + 2000001.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 77,
                LineID = 2000001,
                Number_on_route = 3,
                BusLineStationID = 77.ToString() + 2000001.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 78,
                LineID = 2000001,
                Number_on_route = 4,
                BusLineStationID = 78.ToString() + 2000001.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 83,
                LineID = 2000001,
                Number_on_route = 5,
                BusLineStationID = 83.ToString() + 2000001.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 84,
                LineID = 2000001,
                Number_on_route = 6,
                BusLineStationID = 84.ToString() + 2000001.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 85,
                LineID = 2000001,
                Number_on_route = 7,
                BusLineStationID = 85.ToString() + 2000001.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 86,
                LineID = 2000001,
                Number_on_route = 8,
                BusLineStationID = 86.ToString() + 2000001.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 88,
                LineID = 2000001,
                Number_on_route = 9,
                BusLineStationID = 88.ToString() + 2000001.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 89,
                LineID = 2000001,
                Number_on_route = 10,
                BusLineStationID = 89.ToString() + 2000001.ToString(),
                Exists = true
            });
            #endregion
            #region line number 3
            Line_stations.Add(new BusLineStation {
                StationID = 91,
                LineID = 2000002,
                Number_on_route = 1,
                BusLineStationID = 91.ToString() + 2000002.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 93,
                LineID = 2000002,
                Number_on_route = 2,
                BusLineStationID = 93.ToString() + 2000002.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 84,
                LineID = 2000002,
                Number_on_route = 3,
                BusLineStationID = 84.ToString() + 2000002.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 85,
                LineID = 2000002,
                Number_on_route = 4,
                BusLineStationID = 85.ToString() + 2000002.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 86,
                LineID = 2000002,
                Number_on_route = 5,
                BusLineStationID = 86.ToString() + 2000002.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 88,
                LineID = 2000002,
                Number_on_route = 6,
                BusLineStationID = 88.ToString() + 2000002.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 89,
                LineID = 2000002,
                Number_on_route = 7,
                BusLineStationID = 89.ToString() + 2000002.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 108,
                LineID = 2000002,
                Number_on_route = 8,
                BusLineStationID = 108.ToString() + 2000002.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 109,
                LineID = 2000002,
                Number_on_route = 9,
                BusLineStationID = 109.ToString() + 2000002.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 110,
                LineID = 2000002,
                Number_on_route = 10,
                BusLineStationID = 110.ToString() + 2000002.ToString(),
                Exists = true
            });
            #endregion
            #region line number 4
            Line_stations.Add(new BusLineStation {
                StationID = 108,
                LineID = 2000003,
                Number_on_route = 1,
                BusLineStationID = 108.ToString() + 2000003.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 109,
                LineID = 2000003,
                Number_on_route = 2,
                BusLineStationID = 109.ToString() + 2000003.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 110,
                LineID = 2000003,
                Number_on_route = 3,
                BusLineStationID = 110.ToString() + 2000003.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 1485,
                LineID = 2000003,
                Number_on_route = 4,
                BusLineStationID = 1485.ToString() + 2000003.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 1486,
                LineID = 2000003,
                Number_on_route = 5,
                BusLineStationID = 1486.ToString() + 2000003.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 1487,
                LineID = 2000003,
                Number_on_route = 6,
                BusLineStationID = 1487.ToString() + 2000003.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 1488,
                LineID = 2000003,
                Number_on_route = 7,
                BusLineStationID = 1488.ToString() + 2000003.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 1490,
                LineID = 2000003,
                Number_on_route = 8,
                BusLineStationID = 1490.ToString() + 2000003.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 1491,
                LineID = 2000003,
                Number_on_route = 9,
                BusLineStationID = 1491.ToString() + 2000003.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 90,
                LineID = 2000003,
                Number_on_route = 10,
                BusLineStationID = 90.ToString() + 2000003.ToString(),
                Exists = true
            });
            #endregion
            #region line number 5
            Line_stations.Add(new BusLineStation {
                StationID = 1485,
                LineID = 2000004,
                Number_on_route = 1,
                BusLineStationID = 1485.ToString() + 2000004.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 1486,
                LineID = 2000004,
                Number_on_route = 2,
                BusLineStationID = 1486.ToString() + 2000004.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 1487,
                LineID = 2000004,
                Number_on_route = 3,
                BusLineStationID = 1487.ToString() + 2000004.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 90,
                LineID = 2000004,
                Number_on_route = 4,
                BusLineStationID = 90.ToString() + 2000004.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 105,
                LineID = 2000004,
                Number_on_route = 5,
                BusLineStationID = 105.ToString() + 2000004.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 84,
                LineID = 2000004,
                Number_on_route = 6,
                BusLineStationID = 84.ToString() + 2000004.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 78,
                LineID = 2000004,
                Number_on_route = 7,
                BusLineStationID = 78.ToString() + 2000004.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 115,
                LineID = 2000004,
                Number_on_route = 8,
                BusLineStationID = 115.ToString() + 2000004.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 117,
                LineID = 2000004,
                Number_on_route = 9,
                BusLineStationID = 117.ToString() + 2000004.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 119,
                LineID = 2000004,
                Number_on_route = 10,
                BusLineStationID = 119.ToString() + 5.ToString(),
                Exists = true
            });
            #endregion
            #region line number 6
            Line_stations.Add(new BusLineStation {
                StationID = 1485,
                LineID = 2000005,
                Number_on_route = 1,
                BusLineStationID = 1485.ToString() + 2000005.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 1486,
                LineID = 2000005,
                Number_on_route = 2,
                BusLineStationID = 1486.ToString() + 2000005.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 1487,
                LineID = 2000005,
                Number_on_route = 3,
                BusLineStationID = 1487.ToString() + 2000005.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 1488,
                LineID = 2000005,
                Number_on_route = 4,
                BusLineStationID = 1488.ToString() + 2000005.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 1490,
                LineID = 2000005,
                Number_on_route = 5,
                BusLineStationID = 1490.ToString() + 2000005.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 1510,
                LineID = 2000005,
                Number_on_route = 6,
                BusLineStationID = 1510.ToString() + 2000005.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 1491,
                LineID = 2000005,
                Number_on_route = 7,
                BusLineStationID = 1491.ToString() + 2000005.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 1493,
                LineID = 2000005,
                Number_on_route = 8,
                BusLineStationID = 1493.ToString() + 2000005.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 1492,
                LineID = 2000005,
                Number_on_route = 9,
                BusLineStationID = 1492.ToString() + 2000005.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 1511,
                LineID = 2000005,
                Number_on_route = 10,
                BusLineStationID = 1511.ToString() + 2000005.ToString(),
                Exists = true
            });
            #endregion
            #region line number 7
            Line_stations.Add(new BusLineStation {
                StationID = 1491,
                LineID = 2000006,
                Number_on_route = 1,
                BusLineStationID = 1491.ToString() + 2000006.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 1494,
                LineID = 2000006,
                Number_on_route = 2,
                BusLineStationID = 1494.ToString() + 2000006.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 78,
                LineID = 2000006,
                Number_on_route = 3,
                BusLineStationID = 78.ToString() + 2000006.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 123,
                LineID = 2000006,
                Number_on_route = 4,
                BusLineStationID = 123.ToString() + 2000006.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 119,
                LineID = 2000006,
                Number_on_route = 5,
                BusLineStationID = 119.ToString() + 2000006.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 122,
                LineID = 2000006,
                Number_on_route = 6,
                BusLineStationID = 122.ToString() + 2000006.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 1524,
                LineID = 2000006,
                Number_on_route = 7,
                BusLineStationID = 1524.ToString() + 2000006.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 1493,
                LineID = 2000006,
                Number_on_route = 8,
                BusLineStationID = 1493.ToString() + 2000006.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 1522,
                LineID = 2000006,
                Number_on_route = 9,
                BusLineStationID = 1522.ToString() + 2000006.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 1512,
                LineID = 2000006,
                Number_on_route = 10,
                BusLineStationID = 1512.ToString() + 2000006.ToString(),
                Exists = true
            });
            #endregion
            #region line number 8
            Line_stations.Add(new BusLineStation {
                StationID = 90,
                LineID = 2000007,
                Number_on_route = 1,
                BusLineStationID = 90.ToString() + 2000007.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 91,
                LineID = 2000007,
                Number_on_route = 2,
                BusLineStationID = 91.ToString() + 2000007.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 1485,
                LineID = 2000007,
                Number_on_route = 3,
                BusLineStationID = 92.ToString() + 2000007.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 93,
                LineID = 2000007,
                Number_on_route = 4,
                BusLineStationID = 93.ToString() + 2000007.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 94,
                LineID = 2000007,
                Number_on_route = 5,
                BusLineStationID = 94.ToString() + 2000007.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 95,
                LineID = 2000007,
                Number_on_route = 6,
                BusLineStationID = 95.ToString() + 2000007.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 97,
                LineID = 2000007,
                Number_on_route = 7,
                BusLineStationID = 97.ToString() + 2000007.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 76,
                LineID = 2000007,
                Number_on_route = 8,
                BusLineStationID = 96.ToString() + 2000007.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 83,
                LineID = 2000007,
                Number_on_route = 9,
                BusLineStationID = 83.ToString()+ 2000007.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 85,
                LineID = 2000007,
                Number_on_route = 10,
                BusLineStationID = 85.ToString() + 2000007.ToString(),
                Exists = true
            });
            #endregion
            #region line number 9
            Line_stations.Add(new BusLineStation {
                StationID = 91,
                LineID = 2000008,
                Number_on_route = 1,
                BusLineStationID = 91.ToString() + 2000008.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 93,
                LineID = 2000008,
                Number_on_route = 2,
                BusLineStationID = 93.ToString() + 2000008.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 84,
                LineID = 2000008,
                Number_on_route = 3,
                BusLineStationID = 84.ToString() + 2000008.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 85,
                LineID = 2000008,
                Number_on_route = 4,
                BusLineStationID = 85.ToString() + 2000008.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 86,
                LineID = 2000008,
                Number_on_route = 5,
                BusLineStationID = 86.ToString() + 2000008.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 1488,
                LineID = 2000008,
                Number_on_route = 6,
                BusLineStationID = 1488.ToString() + 2000008.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 1490,
                LineID = 2000008,
                Number_on_route = 7,
                BusLineStationID = 1490.ToString() + 2000008.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 1510,
                LineID = 2000008,
                Number_on_route = 8,
                BusLineStationID = 1510.ToString() + 2000008.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 1491,
                LineID = 2000008,
                Number_on_route = 9,
                BusLineStationID = 1491.ToString() + 2000008.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 1493,
                LineID = 2000008,
                Number_on_route = 10,
                BusLineStationID = 1493.ToString() + 2000008.ToString(),
                Exists = true
            });
            #endregion
            #region line number 10
            Line_stations.Add(new BusLineStation {
                StationID = 1491,
                LineID = 2000009,
                Number_on_route = 1,
                BusLineStationID = 1491.ToString() + 2000009.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 1494,
                LineID = 2000009,
                Number_on_route = 2,
                BusLineStationID = 1494.ToString() + 2000009.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 78,
                LineID = 2000009,
                Number_on_route = 3,
                BusLineStationID = 78.ToString() + 2000009.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 123,
                LineID = 2000009,
                Number_on_route = 4,
                BusLineStationID = 123.ToString() + 2000009.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 119,
                LineID = 2000009,
                Number_on_route = 5,
                BusLineStationID = 119.ToString() + 2000009.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 84,
                LineID = 2000009,
                Number_on_route = 6,
                BusLineStationID = 84.ToString() + 2000009.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 78,
                LineID = 2000009,
                Number_on_route = 7,
                BusLineStationID = 78.ToString() + 2000009.ToString(),
                Exists = true
            }) ;
            Line_stations.Add(new BusLineStation {
                StationID = 115,
                LineID = 2000009,
                Number_on_route = 8,
                BusLineStationID = 115.ToString() + 2000009.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 117,
                LineID = 2000009,
                Number_on_route = 9,
                BusLineStationID = 117.ToString() + 2000009.ToString(),
                Exists = true
            });
            Line_stations.Add(new BusLineStation {
                StationID = 119,
                LineID = 2000009,
                Number_on_route = 10,
                BusLineStationID = 119.ToString() + 2000009.ToString(),
                Exists = true
            });
            #endregion
        }
        public static BusStation search(int code)//does this need linq?
        {
            for (int i = 0; i < stations.Count; i++)
            {
                if (code == stations[i].Code)
                    return (stations[i]);
            }
            return null;
        }
        public static void initialize_two_adjacent_stations()
        {   
            for (int i = 0; i < 10; i++)
            {
                for (int j = i*10; j < i*10+9; j++)
                {
                    double distance = Distance_Between_Two_Stops(search(Line_stations[j].StationID), search(Line_stations[j + 1].StationID));
                    if (distance == 0)
                        distance = 1;
                    int minutes = (int)(6 * distance) / 5;//average speed is 50 km per hour
                    Two_stops.Add(new AdjacentStations 
                    {
                        
                        Stop_1_code = Line_stations[j].StationID,
                        Stop_2_code= Line_stations[j + 1].StationID,
                        PairID = (Line_stations[j].StationID).ToString() +(Line_stations[j + 1].StationID).ToString(),
                        Distance = distance,
                        Average_drive_time=new TimeSpan(minutes/60, minutes%60, 0),
                        
                    });
                    
                   
                }
            }
        }
    }
}        //public static void initialize_buses()
         //{
         //    Buses.Add(new Bus {
         //        Status = Bus.Status_ops.Ready,
         //        License = 12345678,
         //        StartDate = DateTime.Now,
         //        Last_tune_up = DateTime.Now,
         //        Totalkilometerage = 0,
         //        kilometerage = 0,
         //        Gas = 1200,
         //        HasDVD = true,
         //        HasWifi = true,
         //        IsAccessible = false
         //    });//12345678
         //    Buses.Add(new Bus {
         //        Status = Bus.Status_ops.Ready,
         //        License = 12375578,
         //        StartDate = new DateTime(2019, 02, 05),
         //        Last_tune_up = DateTime.Now,
         //        Totalkilometerage = 1200,
         //        kilometerage = 0,
         //        Gas = 1200,
         //        HasDVD = false,
         //        HasWifi = true,
         //        IsAccessible = false
         //    });//12375578
         //    Buses.Add(new Bus {
         //        Status = Bus.Status_ops.Ready,
         //        License = 11343098,
         //        StartDate = new DateTime(2020, 05, 14),
         //        Last_tune_up = DateTime.Now,
         //        Totalkilometerage = 13500,
         //        kilometerage = 0,
         //        Gas = 1200,
         //        HasDVD = true,
         //        HasWifi = true,
         //        IsAccessible = false
         //    });//11343098
         //    Buses.Add(new Bus {
         //        Status = Bus.Status_ops.Ready,
         //        License = 67544527,
         //        StartDate = new DateTime(2020, 10, 22),
         //        Last_tune_up = DateTime.Now,
         //        Totalkilometerage = 4098,
         //        kilometerage = 0,
         //        Gas = 1200,
         //        HasDVD = true,
         //        HasWifi = true,
         //        IsAccessible = true
         //    });//67544527
         //    Buses.Add(new Bus {
         //        Status = Bus.Status_ops.Ready,
         //        License = 76542223,
         //        StartDate = new DateTime(2018, 08, 30),
         //        Last_tune_up = DateTime.Now,
         //        Totalkilometerage = 1000000,
         //        kilometerage = 0,
         //        Gas = 1200,
         //        HasDVD = true,
         //        HasWifi = true,
         //        IsAccessible = false
         //    });//76542223
         //    //5
         //    Buses.Add(new Bus {
         //        Status = Bus.Status_ops.Ready,
         //        License = 1234567,
         //        StartDate = new DateTime(2015, 01, 01),
         //        Last_tune_up = DateTime.Now,
         //        Totalkilometerage = 4000000,
         //        kilometerage = 0,
         //        Gas = 1200,
         //        HasDVD = true,
         //        HasWifi = true,
         //        IsAccessible = true
         //    });//1234567
         //    Buses.Add(new Bus {
         //        Status = Bus.Status_ops.Ready,
         //        License = 7154110,
         //        StartDate = new DateTime(2016, 01, 01),
         //        Last_tune_up = DateTime.Now,
         //        Totalkilometerage = 905088,
         //        kilometerage = 0,
         //        Gas = 1200,
         //        HasDVD = true,
         //        HasWifi = false,
         //        IsAccessible = true
         //    });//7154110
         //    Buses.Add(new Bus {
         //        Status = Bus.Status_ops.Ready,
         //        License = 8765432,
         //        StartDate = new DateTime(2017, 02, 05),
         //        Last_tune_up = DateTime.Now,
         //        Totalkilometerage = 620000,
         //        kilometerage = 0,
         //        Gas = 1200,
         //        HasDVD = true,
         //        HasWifi = false,
         //        IsAccessible = true
         //    });//8765432
         //    Buses.Add(new Bus {
         //        Status = Bus.Status_ops.Ready,
         //        License = 7564325,
         //        StartDate = new DateTime(2012, 12, 27),
         //        Last_tune_up = DateTime.Now,
         //        Totalkilometerage = 10000000,
         //        kilometerage = 0,
         //        Gas = 1200,
         //        HasDVD = true,
         //        HasWifi = true,
         //        IsAccessible = false
         //    });//7564325
         //    Buses.Add(new Bus {
         //        Status = Bus.Status_ops.Ready,
         //        License = 67541527,
         //        StartDate = new DateTime(2020, 09, 18),
         //        Last_tune_up = DateTime.Now,
         //        Totalkilometerage = 40,
         //        kilometerage = 0,
         //        Gas = 1200,
         //        HasDVD = true,
         //        HasWifi = true,
         //        IsAccessible = true
         //    });//67541527
         //    //10
         //    Buses.Add(new Bus {
         //        Status = Bus.Status_ops.Ready,
         //        License = 76542823,
         //        StartDate = new DateTime(2018, 08, 30),
         //        Last_tune_up = DateTime.Now,
         //        Totalkilometerage = 3049330,
         //        kilometerage = 0,
         //        Gas = 1200,
         //        HasDVD = true,
         //        HasWifi = true,
         //        IsAccessible = false
         //    });//76542821
         //    Buses.Add(new Bus {
         //        Status = Bus.Status_ops.Ready,
         //        License = 2490171,
         //        StartDate = new DateTime(2015, 04, 21),
         //        Last_tune_up = DateTime.Now,
         //        Totalkilometerage = 4998778,
         //        kilometerage = 0,
         //        Gas = 1200,
         //        HasDVD = true,
         //        HasWifi = false,
         //        IsAccessible = true
         //    });//2490171
         //    Buses.Add(new Bus {
         //        Status = Bus.Status_ops.Ready,
         //        License = 47321385,
         //        StartDate = DateTime.Now,
         //        Last_tune_up = DateTime.Now,
         //        Totalkilometerage = 0,
         //        kilometerage = 0,
         //        Gas = 1200,
         //        HasDVD = true,
         //        HasWifi = true,
         //        IsAccessible = true
         //    });//47321385
         //    Buses.Add(new Bus {
         //        Status = Bus.Status_ops.Ready,
         //        License = 80368632,
         //        StartDate = new DateTime(2019, 02, 05),
         //        Last_tune_up = DateTime.Now,
         //        Totalkilometerage = 920098,
         //        kilometerage = 0,
         //        Gas = 1200,
         //        HasDVD = true,
         //        HasWifi = true,
         //        IsAccessible = true
         //    });//80368632
         //    Buses.Add(new Bus {
         //        Status = Bus.Status_ops.Ready,
         //        License = 21609875,
         //        StartDate = new DateTime(2020, 05, 14),
         //        Last_tune_up = DateTime.Now,
         //        Totalkilometerage = 13500,
         //        kilometerage = 0,
         //        Gas = 1200,
         //        HasDVD = true,
         //        HasWifi = true,
         //        IsAccessible = false
         //    });//21609875
         //    //15
         //    Buses.Add(new Bus {
         //        Status = Bus.Status_ops.Ready,
         //        License = 60544521,
         //        StartDate = DateTime.Now,
         //        Last_tune_up = DateTime.Now,
         //        Totalkilometerage = 0,
         //        kilometerage = 0,
         //        Gas = 1200,
         //        HasDVD = true,
         //        HasWifi = true,
         //        IsAccessible = true,
         //        Exists = true
         //    });//60544521
         //    Buses.Add(new Bus {
         //        Status = Bus.Status_ops.Ready,
         //        License = 36542873,
         //        StartDate = DateTime.Now,
         //        Last_tune_up = DateTime.Now,
         //        Totalkilometerage = 0,
         //        kilometerage = 0,
         //        Gas = 1200,
         //        HasDVD = true,
         //        HasWifi = true,
         //        IsAccessible = true,
         //        Exists = true
         //    });//36542873
         //    Buses.Add(new Bus {
         //        Status = Bus.Status_ops.Ready,
         //        License = 18932468,
         //        StartDate = DateTime.Now,
         //        Last_tune_up = DateTime.Now,
         //        Totalkilometerage = 0,
         //        kilometerage = 0,
         //        Gas = 1200,
         //        HasDVD = true,
         //        HasWifi = true,
         //        IsAccessible = true,
         //        Exists = true
         //    });//18932468
         //    Buses.Add(new Bus {
         //        Status = Bus.Status_ops.Ready,
         //        License = 93254329,
         //        StartDate = DateTime.Now,
         //        Last_tune_up = DateTime.Now,
         //        Totalkilometerage = 0,
         //        kilometerage = 0,
         //        Gas = 1200,
         //        HasDVD = true,
         //        HasWifi = true,
         //        IsAccessible = true,
         //        Exists = true
         //    });//93254329
         //    Buses.Add(new Bus {
         //        Status = Bus.Status_ops.Ready,
         //        License = 53281972,
         //        StartDate = DateTime.Now,
         //        Last_tune_up = DateTime.Now,
         //        Totalkilometerage = 0,
         //        kilometerage = 0,
         //        Gas = 1200,
         //        HasDVD = true,
         //        HasWifi = true,
         //        IsAccessible = true,
         //        Exists = true
         //    });//53281972
         //    //20
         //    Buses.Add(new Bus {
         //        Status = Bus.Status_ops.Ready,
         //        License = 84372803,
         //        StartDate = new DateTime(2021, 01, 01),
         //        Last_tune_up = DateTime.Now,
         //        Totalkilometerage = 0,
         //        kilometerage = 0,
         //        Gas = 1200,
         //        HasDVD = true,
         //        HasWifi = true,
         //        IsAccessible = false,
         //        Exists = true
         //    });//84372803
         //    Buses.Add(new Bus {
         //        Status = Bus.Status_ops.Ready,
         //        License = 95344034,
         //        StartDate = new DateTime(2020, 10, 22),
         //        Last_tune_up = new DateTime(2020, 10, 22),
         //        Totalkilometerage = 2000,
         //        kilometerage = 2000,
         //        Gas = 0,
         //        HasDVD = true,
         //        HasWifi = true,
         //        IsAccessible = true,
         //        Exists = true
         //    });//95344034
         //    Buses.Add(new Bus {
         //        Status = Bus.Status_ops.Ready,
         //        License = 21234567,
         //        StartDate = new DateTime(2017, 10, 30),
         //        Last_tune_up = new DateTime(2019, 10, 30),
         //        Totalkilometerage = 400000,
         //        kilometerage = 20030,
         //        Gas = 120,
         //        HasDVD = true,
         //        HasWifi = true,
         //        IsAccessible = true,
         //        Exists = true
         //    });//21234567
         //    Buses.Add(new Bus {
         //        Status = Bus.Status_ops.Ready,
         //        License = 48392412,
         //        StartDate = new DateTime(2019, 01, 10),
         //        Last_tune_up = new DateTime(2019, 01, 10),
         //        Totalkilometerage = 19005,
         //        kilometerage = 19005,
         //        Gas = 574,
         //        HasDVD = true,
         //        HasWifi = true,
         //        IsAccessible = true,
         //        Exists = true
         //    });//48392412
         //    //24
         //}