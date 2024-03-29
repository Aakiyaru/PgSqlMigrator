﻿using Npgsql;
using PgSqlMigrator_Library.DataController;
using System;
using System.ComponentModel.Design;
using System.Reflection.PortableExecutable;

namespace PgSqlMigrator_Core.DataBase
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
                //SELECT
                string[,] fieldsMap = SaveLoader.ReadMapFromFile();
                string commandOutTextFields = "";

                for (int i = 0; i < fieldsMap.Length / 2; i++)
                {
                    commandOutTextFields += fieldsMap[i, 0] + ", ";
                }
                commandOutTextFields = commandOutTextFields.Substring(0, commandOutTextFields.Length - 2);

                string commnadOutText = $"SELECT {commandOutTextFields} FROM public.\"{inTable}\";";
                
                NpgsqlCommand commandOut = new NpgsqlCommand(commnadOutText, connOut);
                NpgsqlDataReader rowCounter = commandOut.ExecuteReader();
                int rowCount = 0;

                while (rowCounter.Read())
                {
                    rowCount++;
                }

                rowCounter.Close();
                NpgsqlDataReader reader = commandOut.ExecuteReader();
                Console.WriteLine($"{DateTime.Now}: Данные с сервера получены.");

                string[,] downloadData = new string[rowCount, reader.FieldCount];
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


                //INSERT
                string FieldsIn = "";

                for (int i = 0; i < fieldsMap.Length / 2; i++)
                {
                    FieldsIn += fieldsMap[i, 1] + ", ";
                }
                FieldsIn = FieldsIn.Substring(0, FieldsIn.Length - 2);

                string valuesIn = "";
                string commandText = "";

                for (int i = 0; i < downloadData.Length / reader.FieldCount; i++)
                {
                    for (int j = 0; j < reader.FieldCount; j++)
                    {
                        valuesIn += $"'{downloadData[i,j]}',";
                    }
                    valuesIn = valuesIn.Substring(0, valuesIn.Length - 1);

                    commandText = $"INSERT INTO \"{outTable}\" ({FieldsIn}) VALUES ({valuesIn});";
                    try
                    {
                        NpgsqlCommand commandIn = new NpgsqlCommand(commandText, connIn);
                        commandIn.ExecuteNonQuery();
                        commandText = "";
                        valuesIn = "";
                        continue;
                    }
                    catch (Npgsql.PostgresException)
                    {
                        //UPDATE
                        string condition = "";
                        for (int p = 0; p < fieldsMap.Length / 2; p++)
                        {
                            condition += $" ({fieldsMap[p, 1]} = '{downloadData[i, p]}') AND";
                        }
                        condition = condition.Substring(0, condition.Length - 3);

                        valuesIn = "";

                        for (int j = 0; j < reader.FieldCount; j++)
                        {
                            valuesIn += $"'{downloadData[i, j]}',";
                        }
                        valuesIn = valuesIn.Substring(0, valuesIn.Length - 1);

                        commandText = $"UPDATE \"{outTable}\" SET ({FieldsIn}) = ({valuesIn}) WHERE{condition};";
                        NpgsqlCommand updCommand = new NpgsqlCommand(commandText, connIn);
                        updCommand.ExecuteNonQuery();
                        commandText = "";
                        valuesIn = "";
                        continue;
                    }
                }

                Console.WriteLine($"{DateTime.Now}: Данные отправлены на второй сервер.");
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
