using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PgSqlMigrator_Core
{
    public class DataSaver
    {
        private static readonly XmlSerializer _xmlSerializer =
                                    new XmlSerializer(typeof(ProgramData));

        private static readonly string file = @"..\programdata.xml";

        public static ProgramData GetProgramData()
        {
            ProgramData progData = new ProgramData();
            if (File.Exists(file))
            {
                try
                {
                    using (var fs = File.OpenRead(file))
                    {
                        progData = (ProgramData)_xmlSerializer.Deserialize(fs);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return progData;
        }

        public static void SaveProgData(ProgramData progData)
        {
            try
            {
                using (var fs = File.OpenWrite(file))
                {
                    _xmlSerializer.Serialize(fs, progData);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
