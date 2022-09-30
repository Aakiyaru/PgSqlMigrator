using System;

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
            if (inAddress == null || inLogin == null || inPass == null)
            {
                ChangeIn();
            }
            if (outAddress == null || outLogin == null || outPass == null)
            {
                ChangeOut();
            }
            
            ConnIn();
            ConnOut();
        }

        static private void ConnIn()
        {
            string cons = "IN | ";
            Console.Write(cons+"Введите имя базы данных: ");
            string dbName = Console.ReadLine();
            DbConn.CreateConn(cons, inAddress, inLogin, inPass, dbName);
        }

        static private void ConnOut()
        {
            string cons = "OUT | ";
            Console.Write(cons+"Введите имя базы данных: ");
            string dbName = Console.ReadLine();
            DbConn.CreateConn(cons, outAddress, outLogin, outPass, dbName);
        }

        static private void ChangeIn()
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

        static private void ChangeOut()
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

        private void Logout()
        {

        }

        private void StartMigration()
        {

        }
    }
}
