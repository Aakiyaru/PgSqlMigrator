﻿using System;
using System.IO;
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
    }
}