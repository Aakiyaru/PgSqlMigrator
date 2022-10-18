using PgSqlMigrator_Library;
using System;
using System.IO;

namespace PgSqlMigrator_Configurator
{
    internal class Program
    {
        static ProgramData Data = new ProgramData();
        static string inAddress;
        static string inLogin;
        static string inPass;
        static string inDB;
        static string inTable;
        static string outAddress;
        static string outLogin;
        static string outPass;
        static string outDB;
        static string outTable;
        static string key;

        static void Main(string[] args)
        {
            try
            {
                SaveLoader.Delete(); //удаление старых данных
                key = KeyMaker.Create();
                Data.key = InfoCoder.Incode(key, KeyMaker.defaultKey); //создание ключей шифрования
                ChangeOut(); //изменение параметров подключения к БД 1
                ChangeIn(); //изменение параметров подключения к БД 2
                CreateDataMap(); //создание карты соответствия между полями таблиц
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                return;
            }
        }

        /// <summary>
        /// Изменение параметров подключения к БД куда идёт запись
        /// </summary>
        private static void ChangeIn()
        {
            string cons = "IN | ";
            Console.Write(cons + "Введите адрес сервера: ");
            inAddress = Console.ReadLine();
            Data.inAddress = InfoCoder.Incode(inAddress, key);
            Console.Write(cons + "Введите имя пользователя: ");
            inLogin = Console.ReadLine();
            Data.inLogin = InfoCoder.Incode(inLogin, key);
            Console.Write(cons + "Введите пароль пользователя: ");
            inPass = Console.ReadLine();
            Data.inPass = InfoCoder.Incode(inPass, key);
            Console.Write(cons + "Введите имя базы данных: ");
            inDB = Console.ReadLine();
            Data.inDB = InfoCoder.Incode(inDB, key);
            Console.Write(cons + "Введите имя таблицы: ");
            inTable = Console.ReadLine();
            Data.inTable = InfoCoder.Incode(inTable, key);
            SaveLoader.Save(Data);
        }

        /// <summary>
        /// Изменение параметров подключения к БД откуда идёт чтение
        /// </summary>
        private static void ChangeOut()
        {
            string cons = "OUT | ";
            Console.Write(cons + "Введите адрес сервера: ");
            outAddress = Console.ReadLine();
            Data.outAddress = InfoCoder.Incode(outAddress, key);
            Console.Write(cons + "Введите имя пользователя: ");
            outLogin = Console.ReadLine();
            Data.outLogin = InfoCoder.Incode(outLogin, key);
            Console.Write(cons + "Введите пароль пользователя: ");
            outPass = Console.ReadLine();
            Data.outPass = InfoCoder.Incode(outPass, key);
            Console.Write(cons + "Введите имя базы данных: ");
            outDB = Console.ReadLine();
            Data.outDB = InfoCoder.Incode(outDB, key);
            Console.Write(cons + "Введите имя таблицы: ");
            outTable = Console.ReadLine();
            Data.outTable = InfoCoder.Incode(outTable, key);
            SaveLoader.Save(Data);
        }

        /// <summary>
        /// Создание карты соответствия между полями таблиц
        /// </summary>
        private static void CreateDataMap()
        {
            Console.Write("Введите количество переносимых полей: ");
            int fieldsCount = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Введите соответствие полей:");
            string[,] fieldConformity = new string[fieldsCount, 2];

            for (int i = 0; i < fieldsCount; i++)
            {
                Console.Write($"OUT {i}: ");
                fieldConformity[i, 0] = Console.ReadLine();

                Console.Write($"IN {i}: ");
                fieldConformity[i, 1] = Console.ReadLine();
            }

            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments, Environment.SpecialFolderOption.Create);
            string file = Path.Combine(docFolder, "datamap.dat");
            SaveLoader.WriteMapToFile(fieldConformity, file);
        }
    }
}
