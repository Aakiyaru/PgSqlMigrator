using System;
using Npgsql;

namespace PgSqlMigrator_Core
{
    /// <summary>
    /// Класс подключения к БД
    /// </summary>
    public class DbConn
    {
        /// <summary>
        /// Создать подключение к БД
        /// </summary>
        /// <param name="cons"></param>
        /// <param name="address"></param>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="dbname"></param>
        /// <returns>Объект подключения к БД класса NpgsqlConnection</returns>
        public static NpgsqlConnection CreateConn(string cons, string address, string login, string password, string dbname)
        {
            cons += "Connect | ";
            string connString = $"Host={address};Username={login};Password={password};Database={dbname}";
            NpgsqlConnection connection = new NpgsqlConnection(connString);
            try
            {
                connection.Open();
                Console.WriteLine(cons + "Успешно.\n");
                return connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{cons}{ex.Message}");
                return null;
            }
        }
    }
}
