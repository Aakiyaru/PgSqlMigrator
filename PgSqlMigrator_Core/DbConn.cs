﻿using System;
using Npgsql;

namespace PgSqlMigrator_Core
{
    public class DbConn
    {
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
