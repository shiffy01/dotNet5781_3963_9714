using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class LineSearchHistory
    {
        public string ID{ get; set; }
        public string UserName{ get; set; }
        public int LineCode{ get; set; }
        public int SearchIndex{ get; set; }
        public bool IsStarred{ get; set; }
        public string NickName{ get; set; }
        public bool Exists { get; set; }
    }
}
