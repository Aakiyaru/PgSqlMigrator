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
        /// <summary>
        /// Метод исполнения команды
        /// </summary>
        /// <param name="connIn"></param>
        /// <param name="connOut"></param>
        /// <param name="table"></param>
        /// <returns>true либо false в зависимости от успешности операции</returns>
        public static bool Execute(NpgsqlConnection connIn, NpgsqlConnection connOut, string table)
        {
            try
            {
                NpgsqlCommand commandOut = new NpgsqlCommand($"SELECT * FROM public.\"{table}\";", connOut);
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

                string commandText = $"INSERT INTO \"{table}\" VALUES (";

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
                    commandText = $"INSERT INTO \"{table}\" VALUES (";

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



        ///// <summary>
        ///// Метод исполнения команды
        ///// </summary>
        ///// <param name="connIn"></param>
        ///// <param name="connOut"></param>
        ///// <param name="table"></param>
        ///// <param name="field"></param>
        ///// <returns>true либо false в зависимости от успешности операции</returns>
        //public static bool Execute(NpgsqlConnection connIn, NpgsqlConnection connOut, string table, string field)
        //{
        //    try
        //    {
        //        List<string> downloadData = new List<string>();
        //        NpgsqlCommand commandOut = new NpgsqlCommand($"SELECT {field} FROM public.\"{table}\";", connOut);
        //        NpgsqlDataReader reader = commandOut.ExecuteReader();
        //        Console.WriteLine($"{DateTime.Now}: Данные с сервера получены.");

        //        if (reader.HasRows)
        //        {
        //            while (reader.Read())
        //            {
        //                for (int i = 0; i < reader.FieldCount; i++)
        //                {
        //                    if (reader.GetFieldType(i).ToString() != "System.String")
        //                    {
        //                        downloadData.Add(Convert.ToString(reader.GetValue(i)));
        //                    }
        //                    else
        //                    {
        //                        downloadData.Add(reader.GetString(i));
        //                    }
        //                }
        //            }
        //        }
        //        reader.Close();
        //        Console.WriteLine($"{DateTime.Now}: Данные с сервера собраны в пакет.");

        //        try
        //        {
        //            for (int i = 0; i < downloadData.Count; i++)
        //            {
        //                NpgsqlCommand commandIn = new NpgsqlCommand(
        //                    $"INSERT INTO \"{table}\"({field}) VALUES ('{downloadData[i]}');", connIn);
        //                commandIn.ExecuteNonQuery();
        //            }
        //            Console.WriteLine($"{DateTime.Now}: Данные отправлены на второй сервер.");
        //            Console.WriteLine($"УСПЕШНО! Операция возобновится через 1 минуту...\n  ");
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine($"{DateTime.Now}: {ex.Message}");
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"{DateTime.Now}: " + ex.Message);
        //        return false;
        //    }
        //}
    }
}
