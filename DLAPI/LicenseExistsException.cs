using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DLAPI
{
  public  class LicenseExistsException:Exception
    {
       // [Serializable]??
        
            public int License;
        public LicenseExistsException(int license) : base() => License = license;
        {
            
        }
           

            public override string ToString() => base.ToString() + $", License already exists in the system: {lic}";
        

        public class BadPersonIdCourseIDException : Exception
        {
            public int personID;
            public int courseID;
            public BadPersonIdCourseIDException(int perID, int crsID) : base() { personID = perID; courseID = crsID; }
            public BadPersonIdCourseIDException(int perID, int crsID, string message) :
                base(message)
            {
                personID = perID; courseID = crsID;
            }
            public BadPersonIdCourseIDException(int perID, int crsID, string message, Exception innerException) :
                base(message, innerException)
            {
                personID = perID; courseID = crsID;
            }

            public override string ToString() => base.ToString() + $", bad person id: {personID} and course id: {courseID}";
        }
    }
}
