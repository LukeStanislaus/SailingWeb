using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RazorPagesContacts.Data;
using RazorPagesContacts.Pages;
using SailingWeb.Data;

namespace SailingWeb
{
    public class Program
    {
        public static class Globals
        {
            public static Boats name = new Boats(); // Modifiable
            public static string bla1 = "";
        }
        public static String[] GetNames()
        {
            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal("sailingDB")))
            {
                //connection.Query("call removeperson('" + race + "', '" + name + "')");
                string[] hi = connection.Query<string>("call returnnames").ToArray();
                string [] hi2 = hi.Distinct().ToArray();
                string str = "[";
                int i1 = 0;
                foreach (string item in hi2)
                {
                    if (i1 == 0)
                    {
                        str += @" """ + item + @"""";
                    }
                    else
                    {
                        str += @", """ + item + @"""";
                    }
                    i1++;
                }
                str += " ]";
                str = str.Replace(@"\", "");
                string inputString = str;

                StringBuilder sb = new StringBuilder();
                string[] parts = inputString.Split(new char[] { ' ', '\n', '\t', '\r', '\f', '\v', '\\' }, StringSplitOptions.RemoveEmptyEntries);
                int size = parts.Length;
                for (int i = 0; i < size; i++)
                    sb.AppendFormat("{0} ", parts[i]);
                string ab = sb.ToString();
                var jsonString = JsonConvert.SerializeObject(hi2);

                //string json = jsonSerializer.Serialize(hi2);
                return hi2;
            }
        }
        public static String[] GetBoats()
        {
            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal("sailingDB")))
            {
                //connection.Query("call removeperson('" + race + "', '" + name + "')");
                string[] hi = connection.Query<string>("call returnboats").ToArray();
                string[] hi2 = hi.Distinct().ToArray();
                string str = "[";
                int i1 = 0;
                foreach (string item in hi2)
                {
                    if (i1 == 0)
                    {
                        str += @" """ + item + @"""";
                    }
                    else
                    {
                        str += @", """ + item + @"""";
                    }
                    i1++;
                }
                str += " ]";
                str = str.Replace(@"\", "");
                string inputString = str;

                StringBuilder sb = new StringBuilder();
                string[] parts = inputString.Split(new char[] { ' ', '\n', '\t', '\r', '\f', '\v', '\\' }, StringSplitOptions.RemoveEmptyEntries);
                int size = parts.Length;
                for (int i = 0; i < size; i++)
                    sb.AppendFormat("{0} ", parts[i]);
                string ab = sb.ToString();
                var jsonString = JsonConvert.SerializeObject(hi2);

                //string json = jsonSerializer.Serialize(hi2);
                return hi2;
            }
        }
        public static void SetBoats(Boats Boats)
        {
            //return (Globals.name.name + " " + Globals.name.boatName + " " + Globals.name.boatNumber.ToString());
            Console.WriteLine(Boats.name);
            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal("sailingDB")))
            {
                //connection.Query("call removeperson('" + race + "', '" + name + "')");
                connection.Query("call enterraceperson(@name, @boatName, @boatNumber, 0)", new {
                    name = Boats.name,
                    boatName = Boats.boatName,
                    boatNumber = Boats.boatNumber
                });
            }
            
        }
        public static void Main(string[] args)
        {
            //CreateModel();
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
