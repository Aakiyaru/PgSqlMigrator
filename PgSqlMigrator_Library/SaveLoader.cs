using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace PgSqlMigrator_Library
{
    /// <summary>
    /// Класс сохранения данных
    /// </summary>
    public class SaveLoader
    {
        private static readonly XmlSerializer _xmlSerializer =
                                    new XmlSerializer(typeof(ProgramData));

        private static string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments, Environment.SpecialFolderOption.Create);
        private static readonly string file = Path.Combine(docFolder, "programdata.xml");

        /// <summary>
        /// Загрузка данных программы из файла сохранения
        /// </summary>
        /// <returns>Сохранённые данные</returns>
        public static ProgramData Load()
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

        /// <summary>
        /// Сохранение данных программы в файл
        /// </summary>
        /// <param name="progData"></param>
        public static void Save(ProgramData progData)
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

        public static void Delete()
        {
            File.Delete(file);
        }

        public static void WriteToFile(string[,] array, string file)
        {
            StringBuilder output = new StringBuilder();
            for (int row = 0; row < array.GetLength(0); row++)
            {
                for (int column = 0; column < array.GetLength(1); column++)
                {
                    output.Append($"{array[row, column]}|");

                }
                output.Append(Environment.NewLine);
            }

            try
            {
                System.IO.File.WriteAllText(file, output.ToString());
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"<--Ошибка записи в файл: {ex.Message}");
            }
        }
    }
}
