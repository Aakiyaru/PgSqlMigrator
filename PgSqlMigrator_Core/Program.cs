using System;
using System.Threading;
using Npgsql;
using PgSqlMigrator_Library;

namespace PgSqlMigrator_Core
{
    class Program
    {
        static ProgramData Data;
        static string inAddress;
        static string inLogin;
        static string inPass;
        static string inDB;
        static string inTable;
        static string outAddress;
        static string outLogin;
        static string outPass;
        static string outDB;
        static string outTable;
        static string key;
        static NpgsqlConnection connectionIn;
        static NpgsqlConnection connectionOut;

        static void Main(string[] args)
        {
            if (!(CheckKey() && LoadData()))
            {
                Console.WriteLine("Данные не введены, откройте файл \"PgSqlMigrator_Configurator.exe\" для настройки подключения");
                Console.ReadLine();
                return;
            }

            connectionOut = ConnOut();
            connectionIn = ConnIn();

            while (true)
            {
                Console.Write("\n>>> ");
                string inp = Console.ReadLine();

                switch(inp)
                {
                    case "start":
                        OnWork();
                        break;

                    default:
                        Console.WriteLine($"Неизвестная команда '{inp}', обратитесь к справочнику или напишите 'help'");
                        break;
                }
            }
        }

        private static void OnWork()
        {
            while (true)
            {
                if (!CommandExecutor.Execute(connectionIn, connectionOut, inTable, outTable))
                {
                    break;
                }
                Thread.Sleep(60000);
            }
        }

        private static bool CheckKey()
        {
            try
            {
                Data = SaveLoader.Load();

                if (Data.key != null)
                {
                    key = InfoCoder.Decode(Data.key, KeyMaker.defaultKey);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        private static bool LoadData()
        {
            try
            {
                Data = SaveLoader.Load();

                if (Data.inAddress != null && Data.inLogin != null && Data.inPass != null && Data.inDB != null && Data.outAddress != null && Data.outLogin != null && Data.outPass != null && Data.outDB != null)
                {
                    inAddress = InfoCoder.Decode(Data.inAddress, key);
                    inLogin = InfoCoder.Decode(Data.inLogin, key);
                    inPass = InfoCoder.Decode(Data.inPass, key);
                    inDB = InfoCoder.Decode(Data.inDB, key);
                    inTable = InfoCoder.Decode(Data.inTable, key);
                    outAddress = InfoCoder.Decode(Data.outAddress, key);
                    outLogin = InfoCoder.Decode(Data.outLogin, key);
                    outPass = InfoCoder.Decode(Data.outPass, key);
                    outDB = InfoCoder.Decode(Data.outDB, key);
                    outTable = InfoCoder.Decode(Data.outTable, key);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        private static NpgsqlConnection ConnIn()
        {   
            return DataBaseConnection.CreateConnection(inAddress, inLogin, inPass, inDB, inTable);
        }

        private static NpgsqlConnection ConnOut()
        {
            return DataBaseConnection.CreateConnection(outAddress, outLogin, outPass, outDB, outTable);
        }

        
    }
}
