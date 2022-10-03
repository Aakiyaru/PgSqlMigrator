using System;
using Npgsql;

namespace PgSqlMigrator_Core
{
    class Program
    {
        static string inAddress;
        static string inLogin;
        static string inPass;
        static string outAddress;
        static string outLogin;
        static string outPass;
        
        static void Main(string[] args)
        {
            while (true)
            {
                if (inAddress == null || inLogin == null || inPass == null)
                {
                    ChangeIn();
                }
                if (outAddress == null || outLogin == null || outPass == null)
                {
                    ChangeOut();
                }

                NpgsqlConnection connectionIn = ConnIn();
                NpgsqlConnection connectionOut = ConnOut();

                string code = InfCoding.Incode(inAddress);
                Console.WriteLine("Coded: " + code);

                string decode = InfCoding.Decode(code);
                Console.WriteLine("Decoded: " + decode);
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
            Console.Write(cons + "Введите имя пользователя: ");
            inLogin = Console.ReadLine();
            Console.Write(cons + "Введите пароль пользователя: ");
            inPass = Console.ReadLine();
            Console.WriteLine("");
        }

        private static void ChangeOut()
        {
            string cons = "OUT | ";
            Console.Write(cons + "Введите адрес сервера: ");
            outAddress = Console.ReadLine();
            Console.Write(cons + "Введите имя пользователя: ");
            outLogin = Console.ReadLine();
            Console.Write(cons + "Введите пароль пользователя: ");
            outPass = Console.ReadLine();
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
