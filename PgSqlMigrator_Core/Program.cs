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
                key = KeyMaker.Make();
                Data.key = InfCoding.Incode(key, KeyMaker.defaultKey);
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
                }
            }
        }

        private static bool CheckKey()
        {
            try
            {
                Data = DataSaver.GetProgramData();

                if (Data.key != null)
                {
                    key = InfCoding.Decode(Data.key, KeyMaker.defaultKey);
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
                Data = DataSaver.GetProgramData();

                if (Data.inAddress != null && Data.inLogin != null && Data.inPass != null && Data.inDB != null && Data.outAddress != null && Data.outLogin != null && Data.outPass != null && Data.outDB != null)
                {
                    inAddress = InfCoding.Decode(Data.inAddress, key);
                    inLogin = InfCoding.Decode(Data.inLogin, key);
                    inPass = InfCoding.Decode(Data.inPass, key);
                    inDB = InfCoding.Decode(Data.inDB, key);
                    outAddress = InfCoding.Decode(Data.outAddress, key);
                    outLogin = InfCoding.Decode(Data.outLogin, key);
                    outPass = InfCoding.Decode(Data.outPass, key);
                    outDB = InfCoding.Decode(Data.outDB, key);

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
            return DbConn.CreateConn("IN | ", inAddress, inLogin, inPass, inDB);
        }

        private static NpgsqlConnection ConnOut()
        {
            return DbConn.CreateConn("OUT | ", outAddress, outLogin, outPass, outDB);
        }

        private static void ChangeIn()
        {
            string cons = "IN | ";
            Console.Write(cons + "Введите адрес сервера: ");
            inAddress = Console.ReadLine();
            Data.inAddress = InfCoding.Incode(inAddress, key);
            Console.Write(cons + "Введите имя пользователя: ");
            inLogin = Console.ReadLine();
            Data.inLogin = InfCoding.Incode(inLogin, key);
            Console.Write(cons + "Введите пароль пользователя: ");
            inPass = Console.ReadLine();
            Data.inPass = InfCoding.Incode(inPass, key);
            Console.Write(cons + "Введите имя базы данных: ");
            inDB = Console.ReadLine();
            Data.inDB = InfCoding.Incode(inDB, key);
            Console.WriteLine("");
            DataSaver.SaveProgData(Data);
        }

        private static void ChangeOut()
        {
            string cons = "OUT | ";
            Console.Write(cons + "Введите адрес сервера: ");
            outAddress = Console.ReadLine();
            Data.outAddress = InfCoding.Incode(outAddress, key);
            Console.Write(cons + "Введите имя пользователя: ");
            outLogin = Console.ReadLine();
            Data.outLogin = InfCoding.Incode(outLogin, key);
            Console.Write(cons + "Введите пароль пользователя: ");
            outPass = Console.ReadLine();
            Data.outPass = InfCoding.Incode(outPass, key);
            Console.Write(cons + "Введите имя базы данных: ");
            outDB = Console.ReadLine();
            Data.outDB = InfCoding.Incode(outDB, key);
            Console.WriteLine("");
            DataSaver.SaveProgData(Data);
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
