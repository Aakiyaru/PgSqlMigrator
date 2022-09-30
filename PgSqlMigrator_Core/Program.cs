using System;

namespace PgSqlMigrator_Core
{
    class Program
    {
        static string inAddress;
        static string inLogin;
        static string inPass;
        string outAddress;
        string outLogin;
        string outPass;
        
        static void Main(string[] args)
        {
            Console.Write("Введите адрес базы данных: ");
            inAddress = Console.ReadLine();
            Console.Write("Введите логин базы данных: ");
            inLogin = Console.ReadLine();
            Console.Write("Введите пароль базы данных: ");
            inPass = Console.ReadLine();

            ChangeIn();
        }

        private void Registration()
        {
            
        }

        static private void ChangeIn()
        {
            Console.Write("Введите имя базы данных: ");
            string dbName = Console.ReadLine();
            DbConn.CreateConn(inAddress, inLogin, inPass, dbName);
        }

        private void ChangeOut()
        {

        }

        private void Logout()
        {

        }

        private void StartMigration()
        {

        }
    }
}
