using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    class StationSearchHistory
    {
         public string UserName{ get; set; }
        public int StationCode{ get; set; }
        public int SearchIndex{ get; set; }
        public bool IsStarred{ get; set; }
        public bool NickName{ get; set; }
    }
}
