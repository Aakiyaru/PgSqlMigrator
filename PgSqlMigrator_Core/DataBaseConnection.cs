using System;
using Npgsql;

namespace PgSqlMigrator_Core
{
    /// <summary>
    /// Класс подключения к БД
    /// </summary>
    public class DataBaseConnection
    {
        /// <summary>
        /// Создать подключение к БД
        /// </summary>
        /// <param name="address"></param>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="dbname"></param>
        /// <returns>Объект подключения к БД класса NpgsqlConnection</returns>
        public static NpgsqlConnection CreateConnection(string address, string login, string password, string dbname, string table)
        {
            string connString = $"Host={address};Username={login};Password={password};Database={dbname}";
            NpgsqlConnection connection = new NpgsqlConnection(connString);
            try
            {
                connection.Open();
                Console.WriteLine($"{DateTime.Now}: успешное подключение\n" +
                    $"  Адрес: {address}\n" +
                    $"  Пользователь: {login}\n" +
                    $"  База данных: {dbname}\n" +
                    $"  Таблица: {table}\n");
                return connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now}: {ex.Message}");
                return null;
            }
        }
    }
}
