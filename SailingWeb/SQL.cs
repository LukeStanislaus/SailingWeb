using Dapper;
using Newtonsoft.Json;
using SailingWeb.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailingWeb
{
    public class SQL
    {
        public static String[] GetNames()
        {
            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal("sailingDB")))
            {
                //connection.Query("call removeperson('" + race + "', '" + name + "')");
                string[] hi = connection.Query<string>("call returnnames").ToArray();
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
        public static List<BoatsRacing> GetRacers()
        {
            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal("sailingDB")))
            {
                return connection.Query<BoatsRacing>("call returnracers").ToList();
            }
        }
        public static List<Boats> GetBoats(string name)
        {
            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal("sailingDB")))
            {
                return connection.Query<Boats>("call returnboatsspecific(@name)", new
                { name = name }
                    ).ToList();
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
                connection.Query("call enterracepersonold(@name, @boatName, @boatNumber, 0)", new
                {
                    name = Boats.name,
                    boatName = Boats.boatName,
                    boatNumber = Boats.boatNumber
                });
            }

        }
        public static void SetBoats(Boats Boats, int crew)
        {
            //return (Globals.name.name + " " + Globals.name.boatName + " " + Globals.name.boatNumber.ToString());
            Console.WriteLine(Boats.name);
            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal("sailingDB")))
            {
                //connection.Query("call removeperson('" + race + "', '" + name + "')");
                connection.Query("call enterracepersonold(@name, @boatName, @boatNumber, @crew)", new
                {
                    name = Boats.name,
                    boatName = Boats.boatName,
                    boatNumber = Boats.boatNumber,
                    crew = crew

                });
            }

        }
        public static void SetBoats(Boats Boats, string race)
        {
            race = race.Replace(" ", "");
            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal("sailingDB")))
            {
                try
                {
                    connection.Query("call enterraceperson(@name, @boatName, @boatNumber, 0, @race)", new
                    {
                        name = Boats.name,
                        boatName = Boats.boatName,
                        boatNumber = Boats.boatNumber,
                        race = race
                    });
                }

                catch
                {
                    var sql = new StringBuilder();
                    sql.Append("CREATE TABLE if not exists  ");
                    sql.Append(race);
                    sql.Append(
                    " (`name` varchar(50) NOT NULL,`boat` varchar(50) DEFAULT NULL," +
                    "`boatNumber` int(11) DEFAULT NULL," +
                    "`crew` int(1) DEFAULT NULL,PRIMARY KEY(`name`)) ENGINE = InnoDB DEFAULT CHARSET" +
                    " = utf8mb4;");
                    connection.Execute(sql.ToString());
                    sql = new StringBuilder();
                    sql.Append("insert into ");
                    sql.Append(race);
                    sql.Append(" values('");
                    sql.Append(Boats.name);
                    sql.Append("','");
                    sql.Append(Boats.boatName);
                    sql.Append("','");
                    sql.Append(Boats.boatNumber);
                    sql.Append("',0);");
                    connection.Query(sql.ToString());
                }
            }
            }
            public static void SetBoats(Boats Boats, int crew, string race)
            {
                race = race.Replace(" ", "");
                using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal("sailingDB")))
                {
                    //connection.Query("call removeperson('" + race + "', '" + name + "')");

                    var sql = new StringBuilder();
                    sql.Append("insert into ");
                    sql.Append(race);
                    sql.Append(" values('");
                    sql.Append(Boats.name);
                    sql.Append("','");
                    sql.Append(Boats.boatName);
                    sql.Append("','");
                    sql.Append(Boats.boatNumber);
                    sql.Append("',");
                    sql.Append(crew);
                    sql.Append(");");
                    connection.Query(sql.ToString());


                }
            }
        public static int GetCrew(string boatName)
        {
            //return (Globals.name.name + " " + Globals.name.boatName + " " + Globals.name.boatNumber.ToString());
            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal("sailingDB")))
            {
                //connection.Query("call removeperson('" + race + "', '" + name + "')");
                return connection.Query<int>("call returncrew(@boatName)", new
                {
                    boatName = boatName,
                }).FirstOrDefault();
            }

        }
        public static void newcalendar(Calendar cal)
        {
            //return (Globals.name.name + " " + Globals.name.boatName + " " + Globals.name.boatNumber.ToString());
            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal("sailingDB")))
            {
                //connection.Query("call removeperson('" + race + "', '" + name + "')");
                connection.Query("call newcalendar(@summary, @description, @date)", new
                {
                    summary = cal.summary,
                    description = cal.description,
                    date = cal.dateTime

                });
            }

        }
        public static List<string> todaysevent()
        {
            //return (Globals.name.name + " " + Globals.name.boatName + " " + Globals.name.boatNumber.ToString());
            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal("sailingDB")))
            {
                //connection.Query("call removeperson('" + race + "', '" + name + "')");
                return connection.Query<string>("call todaysevent").ToList();
            }

        }
    }

}
