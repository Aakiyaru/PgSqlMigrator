using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgSqlMigrator_Core
{
    public class InfCoding
    {
        public static string Incode(string input, string key)
        {
            string output = "";
            for (int i = 0; i < input.Length; i++)
            {
                output += (char)(input[i] ^ key[i % key.Length]);
            }
            return output;
        }

        public static string Decode(string input, string key)
        {
            return Incode(input, key);
        }
    }
}
