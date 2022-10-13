using System;

namespace PgSqlMigrator_Library
{
    /// <summary>
    /// Класс создания ключей шифрования
    /// </summary>
    public class KeyMaker
    {
        /// <summary>
        /// Ключ по-умолчанию, используемый для шифровки ключа шифрования
        /// </summary>
        public const string defaultKey = "defaultKey";

        /// <summary>
        /// Набор символов, доступных в ключе шифрования
        /// </summary>
        private static string[] symbols = {
            "1", "2", "3", "4", "5", "6", "7", "8", "9", "0",
            "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "-", "_", "+", "=",
            "q", "w", "e", "r", "t", "y", "u", "i", "o", "p",
            "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P",
            "a", "s", "d", "f", "g", "h", "j", "k", "l", ";",
            "A", "S", "D", "F", "G", "H", "J", "K", "L", ":",
            "z", "x", "c", "v", "b", "n", "m", ",", ".",
            "Z", "X", "C", "V", "B", "N", "M", "<", ">"
        };

        /// <summary>
        /// Создание ключа шифрования
        /// </summary>
        /// <returns>Ключ шифрования</returns>
        public static string Create()
        {
            string answer = "";

            for (int i = 0; i < 256; i++)
            {
                Random rand = new Random();
                int symbolNum = rand.Next(0, symbols.Length);
                answer += symbols[symbolNum];
            }

            return answer;
        }
    }
}
