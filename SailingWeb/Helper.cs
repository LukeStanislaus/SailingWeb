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
        public static string CnnVal(string name)
        {
            //return ConfigurationManager.ConnectionStrings[name].ConnectionString;
            string connect = @"Server=localhost;Database=sailing;Uid=root;Pwd=abc123;Encrypt=false;";
            //;  providerName=MySQL.Data.MySqlClient";
            return connect;
        }

    }
}