﻿using PgSqlMigrator_Library;
using System;
using System.IO;

namespace PgSqlMigrator_Configurator
{
    internal class Program
    {
        static ProgramData Data;
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
                //SaveLoader.Delete();
                //CheckKey();
                //LoadData();
                //key = KeyMaker.Create();
                //Data.key = InfoCoder.Incode(key, KeyMaker.defaultKey);
                //ChangeOut();
                //ChangeIn();
                CreateDataMap();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                return;
            }
        }

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

        private static bool CheckKey()
        {
            try
            {
                Data = SaveLoader.Load();

                if (Data.key != null)
                {
                    key = InfoCoder.Decode(Data.key, KeyMaker.defaultKey);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        private static bool LoadData()
        {
            try
            {
                Data = SaveLoader.Load();

                if (Data.inAddress != null && Data.inLogin != null && Data.inPass != null && Data.inDB != null && Data.outAddress != null && Data.outLogin != null && Data.outPass != null && Data.outDB != null)
                {
                    inAddress = InfoCoder.Decode(Data.inAddress, key);
                    inLogin = InfoCoder.Decode(Data.inLogin, key);
                    inPass = InfoCoder.Decode(Data.inPass, key);
                    inDB = InfoCoder.Decode(Data.inDB, key);
                    inTable = InfoCoder.Decode(Data.inTable, key);
                    outAddress = InfoCoder.Decode(Data.outAddress, key);
                    outLogin = InfoCoder.Decode(Data.outLogin, key);
                    outPass = InfoCoder.Decode(Data.outPass, key);
                    outDB = InfoCoder.Decode(Data.outDB, key);
                    outTable = InfoCoder.Decode(Data.outTable, key);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

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
            SaveLoader.WriteToFile(fieldConformity, file);
        }
    }
}
