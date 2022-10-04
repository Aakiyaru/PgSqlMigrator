using System;
using System.Threading;
using Npgsql;

namespace PgSqlMigrator_Core
{
    class Program
    {
        static ProgramData Data;
        static string inAddress;
        static string inLogin;
        static string inPass;
        static string inDB;
        static string outAddress;
        static string outLogin;
        static string outPass;
        static string outDB;
        static string key;
        static NpgsqlConnection connectionIn;
        static NpgsqlConnection connectionOut;

        static void Main(string[] args)
        {
            if (!(CheckKey() && LoadData()))
            {
                key = KeyMaker.Create();
                Data.key = InfoCoder.Incode(key, KeyMaker.defaultKey);
                ChangeIn();
                ChangeOut();
            }

            connectionOut = ConnOut();
            connectionIn = ConnIn();

            while (true)
            {
                Console.Write(">>> ");
                string inp = Console.ReadLine();

                switch(inp)
                {
                    case "change in":
                        connectionIn.Close();
                        Console.Clear();
                        ChangeIn();
                        connectionIn = ConnIn();
                        break;

                    case "change out":
                        connectionOut.Close();
                        Console.Clear();
                        ChangeOut();
                        connectionOut = ConnOut();
                        break;

                    case "start":
                        StartMigration();
                        break;

                    case "help":
                        PrintHelp();
                        break;

                    default:
                        Console.WriteLine($"Неизвестная команда '{inp}', обратитесь к справочнику или напишите 'help'\n");
                        break;
                }
            }
        }

        private static void PrintHelp()
        {
            Console.WriteLine("'change in' - изменить параметры подключения к БД, куда будет просиходить миграция.");
            Console.WriteLine("'change out' - изменить параметры подключения к БД, откуда будет просиходить миграция.");
            Console.WriteLine("'start' - начать миграцию с заданными параметрами.");
            Console.WriteLine("'help' - справка.\n");
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
                    outAddress = InfoCoder.Decode(Data.outAddress, key);
                    outLogin = InfoCoder.Decode(Data.outLogin, key);
                    outPass = InfoCoder.Decode(Data.outPass, key);
                    outDB = InfoCoder.Decode(Data.outDB, key);

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

        private static NpgsqlConnection ConnIn()
        {   
            return DataBaseConnection.CreateConnection("IN | ", inAddress, inLogin, inPass, inDB);
        }

        private static NpgsqlConnection ConnOut()
        {
            return DataBaseConnection.CreateConnection("OUT | ", outAddress, outLogin, outPass, outDB);
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
            Console.WriteLine("");
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
            Console.WriteLine("");
            SaveLoader.Save(Data);
        }

        private static void StartMigration()
        {
            for (int i = 0; i < 50; i++)
            {
                Thread.Sleep(500);
                Console.Write(">");
            }
            Console.WriteLine("Успешно!\n");
        }
    }
}
