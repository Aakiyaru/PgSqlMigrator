using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgSqlMigrator_Core
{
    public class CommandExecutor
    {

        public static void Execute(NpgsqlConnection connIn, NpgsqlConnection connOut, string table)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand($"SELECT * FROM public.\"{table}\";", connOut);
                NpgsqlDataReader reader = command.ExecuteReader();
                Console.WriteLine($"{DateTime.Now}: Данные с сервера получены.");

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now}: " + ex.Message);
                return;
            }
        }

        public static void Execute(NpgsqlConnection connIn, NpgsqlConnection connOut, string table, string field)
        {

        }
    }
}
