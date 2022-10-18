using Npgsql;
using System;

namespace PgSqlMigrator_Core
{
    public class CommandExecutor
    {
        /// <summary>
        /// Метод исполнения команды
        /// </summary>
        /// <param name="connIn">Подключение к БД для записи</param>
        /// <param name="connOut">Подключение к БД для чтения</param>
        /// <param name="inTable">Название таблицы для записи</param>
        /// /// <param name="outTable">Название таблицы для чтения</param>
        /// <returns>true либо false в зависимости от успешности операции</returns>
        public static bool Execute(NpgsqlConnection connIn, NpgsqlConnection connOut, string inTable, string outTable)
        {
            try
            {
                NpgsqlCommand commandOut = new NpgsqlCommand($"SELECT * FROM public.\"{inTable}\";", connOut);
                NpgsqlDataReader rowCounter = commandOut.ExecuteReader();
                int rowCount = 0;

                while(rowCounter.Read())
                {
                    rowCount++;
                }

                rowCounter.Close();
                NpgsqlDataReader reader = commandOut.ExecuteReader();
                Console.WriteLine($"{DateTime.Now}: Данные с сервера получены.");

                string[,] downloadData = new string[rowCount,reader.FieldCount];
                int readerCount = 0;

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            downloadData[readerCount, i] = Convert.ToString(reader.GetValue(i));
                        }
                        readerCount++;
                    }
                }

                Console.WriteLine($"{DateTime.Now}: Данные с сервера собраны в пакет.");

                string commandText = $"INSERT INTO \"{outTable}\" VALUES (";

                for (int i = 0; i < downloadData.Length/reader.FieldCount; i++)
                {
                    for (int j = 0; j < reader.FieldCount; j++)
                    {
                        commandText += $"'{downloadData[i,j]}',";
                    }
                    commandText = commandText.Substring(0, commandText.Length - 1);
                    commandText += ");";

                    NpgsqlCommand commandIn = new NpgsqlCommand(commandText, connIn);
                    commandIn.ExecuteNonQuery();
                    commandText = $"INSERT INTO \"{outTable}\" VALUES (";

                }
                Console.WriteLine($"{DateTime.Now}: Данные отправлены на второй сервер.");
                Console.WriteLine($"УСПЕШНО! Операция возобновится через 1 минуту...\n  ");

                reader.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now}: " + ex.Message);
                return false;
            }

        }
    }
}
