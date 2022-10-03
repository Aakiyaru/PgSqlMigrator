using System;
using Npgsql;

namespace PgSqlMigrator_Core
{
    class Program
    {
        static ProgramData Data;
        static string inAddress;
        static string inLogin;
        static string inPass;
        static string outAddress;
        static string outLogin;
        static string outPass;
        static string key;

        static void Main(string[] args)
        {
            while (true)
            {
                if (CheckKey() && LoadData())
                {
                    
                }
                else
                {
                    key = KeyMaker.Make();
                    Data.key = InfCoding.Incode(key, KeyMaker.defaultKey);
                    ChangeIn();
                    ChangeOut();
                }

                NpgsqlConnection connectionIn = ConnIn();
                NpgsqlConnection connectionOut = ConnOut();

                DataSaver.SaveProgData(Data);
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

                if (Data.inAddress != null || Data.inLogin != null || Data.inPass != null || Data.outAddress != null || Data.outLogin != null || Data.outPass != null)
                {
                    inAddress = InfCoding.Decode(Data.inAddress, key);
                    inLogin = InfCoding.Decode(Data.inLogin, key);
                    inPass = InfCoding.Decode(Data.inPass, key);
                    outAddress = InfCoding.Decode(Data.outAddress, key);
                    outLogin = InfCoding.Decode(Data.outLogin, key);
                    outPass = InfCoding.Decode(Data.outPass, key);

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
            string cons = "IN | ";
            Console.Write(cons+"Введите имя базы данных: ");
            string dbName = Console.ReadLine();
            return DbConn.CreateConn(cons, inAddress, inLogin, inPass, dbName);
        }

        private static NpgsqlConnection ConnOut()
        {
            string cons = "OUT | ";
            Console.Write(cons+"Введите имя базы данных: ");
            string dbName = Console.ReadLine();
            return DbConn.CreateConn(cons, outAddress, outLogin, outPass, dbName);
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
            Console.WriteLine("");
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
            Console.WriteLine("");
        }

        private static void Logout()
        {

        }

        private static void StartMigration()
        {

        }
    }
}
