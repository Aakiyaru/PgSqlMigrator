using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PgSqlMigrator_Core
{
    //TODO разобраться с сохранением и выводом данных
    
    public class ProgramData
    {
        public int Value1 { get; set; }
        public int Value2 { get; set; }

        public override string ToString() => $"{Value1}, {Value2}";

        private static readonly XmlSerializer _xmlSerializer =
                                    new XmlSerializer(typeof(ProgramData));

        public static ProgramData GetProgramData(string file)
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

        public static void SetNewValues(ProgramData progData)
        {
            Console.WriteLine("число 1");
            if (int.TryParse(Console.ReadLine(), out int value1))
            {
                progData.Value1 = value1;
            }
            Console.WriteLine("число 2");
            if (int.TryParse(Console.ReadLine(), out int value2))
            {
                progData.Value2 = value2;
            }

        }

        public static void SaveProgData(ProgramData progData, string file)
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
