//using Systems;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DS
{
    public class Class1
    {
        private static List<BusStation> stations = new List<BusStation>();
        public static List<BusStation> Stations { get => stations; }
        private static List<BusLine> lines = new List<BusLine>();
        public static List<BusLine> Lines { get => lines; }
        private static List<BusLineStation> line_stations = new List<BusLineStation>();
        public static List<BusLineStation> Line_stations{ get => line_stations; }
        private static List<TwoConsecutiveStops> two_stops = new List<TwoConsecutiveStops>();
        public static List<TwoConsecutiveStops> Two_stops{ get => two_stops; }
        public static void initialize_Stations()
        {
            Stations.Add(new BusStation 
            {
                Code = 73,
                Name = "שדרות גולדה מאיר/המשורר אצ''ג",
                Address = "רחוב:שדרות גולדה מאיר עיר: ירושלים ",
                Latitude = 31.825302,
                Longitude = 35.188624,
                Exists = true
            });//שדרות גולדה מאיר/המשורר אצ''ג
            Stations.Add(new BusStation 
            {
                Code = 76,
                Name = "בית ספר צור באהר בנות/אלמדינה אלמונוורה",
                Address = "רחוב:אל מדינה אל מונאוורה עיר: ירושלים",
                Latitude = 31.738425,
                Longitude = 35.228765,
                Exists = true
            });//בית ספר צור באהר בנות/אלמדינה אלמונוורה
            Stations.Add(new BusStation 
            {
            Code = 77,
                Name = "בית ספר אבן רשד/אלמדינה אלמונוורה",
                Address = "רחוב:אל מדינה אל מונאוורה עיר: ירושלים ",
                Latitude = 31.738676,
                Longitude = 35.226704,
                Exists = true
            });//בית ספר אבן רשד/אלמדינה אלמונוורה
            Stations.Add(new BusStation 
            {
                Code = 78,
                Name = "שרי ישראל/יפו",
                Address = "רחוב:שדרות שרי ישראל 15 עיר: ירושלים",
                Latitude = 31.789128,
                Longitude = 35.206146,
                Exists = true
            });//שרי ישראל/יפו
            Stations.Add(new BusStation
            {
                Code = 83,
                Name = "בטן אלהווא/חוש אל מרג",
                Address = "רחוב:בטן אל הווא עיר: ירושלים",
                Latitude = 31.766358,
                Longitude = 35.240417,
                Exists = true
            });//בטן אלהווא/חוש אל מרג
            Stations.Add(new BusStation 
            {
                Code = 84,
                Name = "מלכי ישראל/הטורים",
                Address = " רחוב:מלכי ישראל 77 עיר: ירושלים ",
                Latitude = 31.790758,
                Longitude = 35.209791,
                Exists = true
            });//מלכי ישראל/הטורים
            Stations.Add(new BusStation 
            {
                Code = 85,
                Name = "בית ספר לבנים/אלמדארס",
                Address = "רחוב:אלמדארס עיר: ירושלים",
                Latitude = 31.768643,
                Longitude = 35.238509,
                Exists = true
            });//בית ספר לבנים/אלמדארס
            Stations.Add(new BusStation 
            {
                Code = 86,
                Name = "מגרש כדורגל/אלמדארס",
                Address = "רחוב:אלמדארס עיר: ירושלים",
                Latitude = 31.769899,
                Longitude = 35.23973,
                Exists = true
            });//מגרש כדורגל/אלמדארס
            Stations.Add(new BusStation {
                Code = 88,
                Name = "בית ספר לבנות/בטן אלהוא",
                Address = " רחוב:בטן אל הווא עיר: ירושלים",
                Latitude = 31.767064,
                Longitude = 35.238443,
                Exists = true
            });//בית ספר לבנות/בטן אלהוא
            Stations.Add(new BusStation 
            {
                Code = 89,
                Name = "דרך בית לחם הישה/ואדי קדום",
                Address = " רחוב:דרך בית לחם הישנה עיר: ירושלים ",
                Latitude = 31.765863,
                Longitude = 35.247198,
                Exists = true
            });//דרך בית לחם הישה/ואדי קדום
            //10
            Stations.Add(new BusStation 
            {
                Code = 90,
                Name = "גולדה/הרטום",
                Address = "רחוב:דרך בית לחם הישנה עיר: ירושלים",
                Latitude = 31.799804,
                Longitude = 35.213021,
                Exists = true
            });//גולדה/הרטום
            Stations.Add(new BusStation 
            {
                Code = 91,
                Name = "דרך בית לחם הישה/ואדי קדום",
                Address = " רחוב:דרך בית לחם הישנה עיר: ירושלים ",
                Latitude = 31.765717,
                Longitude = 35.247102,
                Exists = true
            });//דרך בית לחם הישה/ואדי קדום
            Stations.Add(new BusStation 
            {
                Code = 93,
                Name = "חוש סלימה 1",
                Address = " רחוב:דרך בית לחם הישנה עיר: ירושלים",
                Latitude = 31.767265,
                Longitude = 35.246594,
                Exists = true
            });//חוש סלימה 
            Stations.Add(new BusStation 
            {
                Code = 94,
                Name = "דרך בית לחם הישנה ב",
                Address = " רחוב:דרך בית לחם הישנה עיר: ירושלים",
                Latitude = 31.767084,
                Longitude = 35.246655,
                Exists = true
            });//דרך בית לחם הישנה ב
            Stations.Add(new BusStation
            {
                Code = 95,
                Name = "דרך בית לחם הישנה א",
                Address = " רחוב:דרך בית לחם הישנה עיר: ירושלים",
                Latitude = 31.768759,
                Longitude = 31.768759,
                Exists = true
            });//דרך בית לחם הישנה א
            Stations.Add(new BusStation 
            {
                Code = 97,
                Name = "שכונת בזבז 2",
                Address = " רחוב:דרך בית לחם הישנה עיר: ירושלים",
                Latitude = 31.77002,
                Longitude = 35.24348,
                Exists = true
            });//שכונת בזבז 2
            Stations.Add(new BusStation 
            {
                Code = 102,
                Name = "גולדה/שלמה הלוי",
                Address = " רחוב:שדרות גולדה מאיר עיר: ירושלים",
                Latitude = 31.8003,
                Longitude = 35.208257,
                Exists = true
            });//גולדה/שלמה הלוי
            Stations.Add(new BusStation 
            {
                Code = 103,
                Name = "גולדה/הרטום",
                Address = " רחוב:שדרות גולדה מאיר עיר: ירושלים",
                Latitude = 31.8,
                Longitude = 35.214106,
                Exists = true
            });//גולדה/הרטום
            Stations.Add(new BusStation 
            {
                Code = 105,
                Name = "גבעת משה",
                Address = " רחוב:גבעת משה 2 עיר: ירושלים",
                Latitude = 31.797708,
                Longitude = 35.217133,
                Exists = true
            });//גבעת משה
            Stations.Add(new BusStation 
            {
                Code = 106,
                Name = "גבעת משה",
                Address = " רחוב:גבעת משה 3 עיר: ירושלים",
                Latitude = 31.797535,
                Longitude = 35.217057,
                Exists = true
            });//גבעת משה
            //20
            Stations.Add(new BusStation
            {
                Code = 108,
                Name = "עזרת תורה/עלי הכהן",
                Address = " רחוב:עזרת תורה 25 עיר: ירושלים",
                Latitude = 31.797535,
                Longitude = 35.213728,
                Exists = true
            });//עזרת תורה/עלי הכהן
            Stations.Add(new BusStation 
            {
                Code = 109,
                Name = "עזרת תורה/דורש טוב",
                Address = " רחוב:עזרת תורה 21 עיר: ירושלים ",
                Latitude = 31.796818,
                Longitude = 35.212936,
                Exists = true
            });//עזרת תורה/דורש טוב
            Stations.Add(new BusStation 
            {
                Code = 110,
                Name = "עזרת תורה/דורש טוב",
                Address = " רחוב:עזרת תורה 12 עיר: ירושלים",
                Latitude = 31.796129,
                Longitude = 35.212698,
                Exists = true
            });//עזרת תורה/דורש טוב
            Stations.Add(new BusStation 
            {
                Code = 111,
                Name = "יעקובזון/עזרת תורה",
                Address = " רחוב:יעקובזון 1 עיר: ירושלים",
                Latitude = 31.794631,
                Longitude = 35.21161,
                Exists = true
            });//יעקובזון/עזרת תורה
            Stations.Add(new BusStation 
            {
                Code = 112,
                Name = "יעקובזון/עזרת תורה",
                Address = " רחוב:יעקובזון עיר: ירושלים",
                Latitude = 31.79508,
                Longitude = 35.211684,
                Exists = true
            });//יעקובזון/עזרת תורה
            Stations.Add(new BusStation 
            {
                Code = 113,
                Name = "זית רענן/אוהל יהושע",
                Address = " רחוב:זית רענן 1 עיר: ירושלים",
                Latitude = 31.796255,
                Longitude = 35.211065,
                Exists = true
            });//זית רענן/אוהל יהושע
            Stations.Add(new BusStation 
            {
                Code = 115,
                Name = "זית רענן/תורת חסד",
                Address = " רחוב:זית רענן עיר: ירושלים",
                Latitude = 31.798423,
                Longitude = 35.209575,
                Exists = true
            });//זית רענן/תורת חסד
            Stations.Add(new BusStation
            {
                Code = 116,
                Name = "זית רענן/תורת חסד",
                Address = " רחוב:הרב סורוצקין 48 עיר: ירושלים ",
                Latitude = 31.798689,
                Longitude = 35.208878,
                Exists = true
            });//זית רענן/תורת חסד
            Stations.Add(new BusStation
            {
                Code = 117,
                Name = "קרית הילד/סורוצקין",
                Address = " רחוב:הרב סורוצקין עיר: ירושלים",
                Latitude = 31.799165,
                Longitude = 35.206918,
                Exists = true
            });//קרית הילד/סורוצקין
            Stations.Add(new BusStation 
            {
                Code = 119,
                Name = "סורוצקין/שנירר",
                Address = " רחוב:הרב סורוצקין 31 עיר: ירושלים",
                Latitude = 31.797829,
                Longitude = 35.205601,
                Exists = true
            });//סורוצקין/שנירר
            //30
            Stations.Add(new BusStation 
            {
                Code = 1485,
                Name = "שדרות נווה יעקוב/הרב פרדס ",
                Address = "רחוב: שדרות נווה יעקוב עיר:ירושלים ",
                Latitude = 31.840063,
                Longitude = 35.240062,
                Exists = true

            });//שדרות נווה יעקוב/הרב פרדס
            Stations.Add(new BusStation 
            {
                Code = 1486,
                Name = "מרכז קהילתי /שדרות נווה יעקוב",
                Address = "רחוב:שדרות נווה יעקוב ירושלים עיר:ירושלים ",
                Latitude = 31.838481,
                Longitude = 35.23972,
                Exists = true
            });//מרכז קהילתי /שדרות נווה יעקוב
            Stations.Add(new BusStation
            {
                Code = 1487,
                Name = " מסוף 700 /שדרות נווה יעקוב ",
                Address = "חוב:שדרות נווה יעקב 7 עיר: ירושלים ",
                Latitude = 31.837748,
                Longitude = 35.231598,
                Exists = true
            });//מסוף 700 /שדרות נווה יעקוב
            Stations.Add(new BusStation 
            {
                Code = 1488,
                Name = " הרב פרדס/אסטורהב ",
                Address = "רחוב:מעגלות הרב פרדס עיר: ירושלים רציף ",
                Latitude = 31.840279,
                Longitude = 35.246272,
                Exists = true
            });//הרב פרדס/אסטורהב
            Stations.Add(new BusStation 
            {
                Code = 1490,
                Name = "הרב פרדס/צוקרמן ",
                Address = "רחוב:מעגלות הרב פרדס 24 עיר: ירושלים ",
                Latitude = 31.843598,
                Longitude = 35.243639,
                Exists = true
            });//הרב פרדס/צוקרמן
            Stations.Add(new BusStation 
            {
                Code = 1491,
                Name = "ברזיל ",
                Address = "רחוב:ברזיל 14 עיר: ירושלים",
                Latitude = 31.766256,
                Longitude = 35.173,
                Exists = true
            });//ברזיל
            Stations.Add(new BusStation 
            {
                Code = 1492,
                Name = "בית וגן/הרב שאג ",
                Address = "רחוב:בית וגן 61 עיר: ירושלים ",
                Latitude = 31.76736,
                Longitude = 35.184771,
                Exists = true
            });//בית וגן/הרב שאג
            Stations.Add(new BusStation
            {
                Code = 1493,
                Name = "בית וגן/עוזיאל ",
                Address = "רחוב:בית וגן 21 עיר: ירושלים ",
                Latitude = 31.770543,
                Longitude = 35.183999,
                Exists = true
            });//בית וגן/עוזיאל
            Stations.Add(new BusStation
            {
                Code = 1494,
                Name = " קרית יובל/שמריהו לוין ",
                Address = "רחוב:ארתור הנטקה עיר: ירושלים ",
                Latitude = 31.768465,
                Longitude = 35.178701,
                Exists = true
            });//קרית יובל/שמריהו לוין
            Stations.Add(new BusStation 
            {
                Code = 1510,
                Name = " קורצ'אק / רינגלבלום ",
                Address = "רחוב:יאנוש קורצ'אק 7 עיר: ירושלים",
                Latitude = 31.759534,
                Longitude = 35.173688,
                Exists = true
            });//קורצ'אק / רינגלבלום
            //40
            Stations.Add(new BusStation 
            {
                Code = 1511,
                Name = " טהון/גולומב ",
                Address = "רחוב:יעקב טהון עיר: ירושלים ",
                Latitude = 31.761447,
                Longitude = 35.175929,
                Exists = true
            });//טהון/גולומב
            Stations.Add(new BusStation 
            {
                Code = 1512,
                Name = "הרב הרצוג/שח''ל ",
                Address = "רחוב:הרב הרצוג עיר: ירושלים רציף",
                Latitude = 31.761447,
                Longitude = 35.199936,
                Exists = true
            });//הרב הרצוג/שח''ל
            Stations.Add(new BusStation 
            {
                Code = 1514,
                Name = "פרץ ברנשטיין/נזר דוד ",
                Address = "רחוב:הרב הרצוג עיר: ירושלים רציף",
                Latitude = 31.759186,
                Longitude = 35.189336,
                Exists = true
            });//פרץ ברנשטיין/נזר דוד
            Stations.Add(new BusStation 
            {
                Code = 1518,
                Name = "פרץ ברנשטיין/נזר דוד",
                Address = " רחוב:פרץ ברנשטיין 56 עיר: ירושלים ",
                Latitude = 31.759121,
                Longitude = 35.189178,
                Exists = true
            });//פרץ ברנשטיין/נזר דוד
            Stations.Add(new BusStation
            {
                Code = 1522,
                Name = "מוזיאון ישראל/רופין",
                Address = " רחוב:דרך רופין עיר: ירושלים ",
                Latitude = 31.774484,
                Longitude = 35.204882,
                Exists = true
            });//מוזיאון ישראל/רופין
            Stations.Add(new BusStation 
            {
                Code = 1523,
                Name = "הרצוג/טשרניחובסקי",
                Address = " רחוב:הרב הרצוג עיר: ירושלים ",
                Latitude = 31.769652,
                Longitude = 35.208248,
                Exists = true
            });//הרצוג/טשרניחובסקי
            Stations.Add(new BusStation 
            {
                Code = 1524,
                Name = "רופין/שד' הזז",
                Address = " רחוב:הרב הרצוג עיר: ירושלים ",
                Latitude = 31.769652,
                Longitude = 35.208248,
                Exists = true
            });//רופין/שד' הזז
            Stations.Add(new BusStation 
            {
                Code = 121,
                Name = "מרכז סולם/סורוצקין ",
                Address = " רחוב:הרב סורוצקין 13 עיר: ירושלים",
                Latitude = 31.796033,
                Longitude = 35.206094,
                Exists = true
            });//מרכז סולם/סורוצקין
            Stations.Add(new BusStation
            {
                Code = 123,
                Name = "אוהל דוד/סורוצקין ",
                Address = " רחוב:הרב סורוצקין 9 עיר: ירושלים",
                Latitude = 31.794958,
                Longitude = 35.205216,
                Exists = true
            });//אוהל דוד/סורוצקין
            Stations.Add(new BusStation
            {
                Code = 122,
                Name = "מרכז סולם/סורוצקין ",
                Address = " רחוב:הרב סורוצקין 28 עיר: ירושלים",
                Latitude = 31.79617,
                Longitude = 35.206158,
                Exists = true
            });//מרכז סולם/סורוצקין   
            //50
        }
        public static void initialize_Lines()
        {
            Lines.Add(new BusLine 
            {
                BusID = 1,
                Bus_line_number = 32,
                Exists = true
            }); 
            Lines.Add(new BusLine 
            {
                BusID = 2,
                Bus_line_number = 45,
                Exists = true
            });
            Lines.Add(new BusLine 
            {
                BusID = 3,
                Bus_line_number = 36,
                Exists = true

            });
            Lines.Add(new BusLine 
            {
                BusID = 4,
                Bus_line_number=19,
                Exists = true
            });
            Lines.Add(new BusLine 
            {
                BusID = 5,
                Bus_line_number = 56,
                Exists = true
            });
            Lines.Add(new BusLine 
            {
                BusID = 6,
                Bus_line_number = 28,
                Exists = true
            });
            Lines.Add(new BusLine 
            {
                BusID = 7,
                Bus_line_number = 69,
                Exists = true
            });
            Lines.Add(new BusLine 
            {
                BusID = 8,
                Bus_line_number = 58,
                Exists = true
            });
            Lines.Add(new BusLine 
            {
                BusID = 9,
                Bus_line_number = 92,
                Exists = true
            });
            Lines.Add(new BusLine
            {
                BusID = 10,
                Bus_line_number = 55,
                Exists = true
            });

           
        }
        public static void initialize_Bus_line_stations()
        {
            
        }
        public static void initialize_two_consecutive_stations()
        {
        
        }
    }
}
