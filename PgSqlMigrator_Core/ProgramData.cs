using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PgSqlMigrator_Core
{
    public class ProgramData
    {
        public string inAddress { get; set; }
        public string inLogin { get; set; }
        public string inPass { get; set; }
        public string inDB { get; set; }
        public string outAddress { get; set; }
        public string outLogin { get; set; }
        public string outPass { get; set; }
        public string outDB { get; set; }
        public string key { get; set; }
    }
}
