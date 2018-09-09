using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailingWeb
{
    public static class Helper
    {

        /// <summary>
        /// Returns the SQL connection string.
        /// </summary>
        /// <returns>The SQL connection string.</returns>
        public static string CnnVal()
        {

            return @"Server=yewstock.ddns.net;Database=sailing1;Uid=luke;Pwd=abc123;Encrypt=false;";

        }

    }
}