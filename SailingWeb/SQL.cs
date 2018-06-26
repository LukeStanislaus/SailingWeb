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
        /// <summary>
        /// Returns a stored procedure that returns all classes from boat data db.
        /// </summary>
        /// <returns>List of all classes.</returns>
        public static List<String> ReturnClass()
        {
            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal()))
            {
                // Runs query.
                return connection.Query<String>("call returnclass").ToList();
            }
        }


        /// <summary>
        /// Returns a list of all the distinct names in the fulllist db. Used a array due to 
        /// compatibility with javascript.
        /// </summary>
        /// <returns>List of names.</returns>
        public static String[] GetNames()
        {
            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal()))
            {
                // Returns a list of all the distinct names in the fulllist db.
                string[] listOfNames = connection.Query<string>("call returnnames").Distinct().ToArray();


                return listOfNames;
            }
        }



        /// <summary>
        /// Returns the list of racers
        /// </summary>
        /// <returns>Returns boat data, including if the person is a crew or not.</returns>
        /// <param name="racename">Race name, cannot be global as this is used in tables sheet.</param>
        public static List<BoatsRacing> GetRacers(string racename)
        {
            // Tables names cannot have spaces.
            string race = racename.Replace(" ", "");


            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal()))
            {

                // Appends together the query. Stops SQL injection.
                var sql1 = new StringBuilder();
                sql1.Append("select * from ");
                sql1.Append(race);
                sql1.Append(";");
                return connection.Query<BoatsRacing>(sql1.ToString()).ToList();

            }

        }

        /// <summary>
        /// Get boats of a specific person.
        /// </summary>
        /// <param name="Name">Person to search for boats.</param>
        /// <returns></returns>
        public static List<Boats> GetBoats(string name)
        {

            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal()))
            {

                //Runs stored procedure.
                return connection.Query<Boats>("call returnboatsspecific(@name)", new
                { name = name }).ToList();

            }

        }


        /// <summary>
        /// Gets the list of distinct boats which have been sailed before.
        /// </summary>
        /// <returns>Array of all boats used before.</returns>
        public static String[] GetBoats()
        {

            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal()))
            {

                // Query.
                return connection.Query<string>("call returnboats").Distinct().ToArray();

            }

        }


        /// <summary>
        /// Adds a new boat/person
        /// </summary>
        /// <param name="Boat">Boat data to add.</param>
        public static void SetNewFullBoat(Boats Boat)
        {

            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal()))
            {

                //Appends together the query, stops SQL injection.
                var sql = new StringBuilder();
                sql.Append("insert into fulllist values ('");
                sql.Append(Boat.name);
                sql.Append("', '");
                sql.Append(Boat.boatName);
                sql.Append("', ");
                sql.Append(Boat.boatNumber);
                sql.Append(");");

                // Query.
                connection.Query(sql.ToString());

            }

        }


        /// <summary>
        /// Removes boats, knows if they are crew or not.
        /// </summary>
        /// <param name="Boats"></param>
        public static void RemoveBoats(Boats Boats)
        {

            // Cannot have table name with space 
            string race = Program.Globals.racename.Replace(" ", "");

            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal()))
            {


                // Creates query.
                var sql1 = new StringBuilder();
                sql1.Append("select * from ");
                sql1.Append(race);
                sql1.Append(" where boat ='");
                sql1.Append(Boats.boatName);
                sql1.Append("' and boatNumber=");
                sql1.Append(Boats.boatNumber);
                sql1.Append(" and crew=1;");
                string Name = connection.Query<BoatsRacing>(sql1.ToString()).FirstOrDefault().name;


                // If they have a crew, remove both.
                if (Name != null)
                {
                    var sql = new StringBuilder();
                    sql.Append("delete from ");
                    sql.Append(race);
                    sql.Append(" where name ='");
                    sql.Append(Boats.name);
                    sql.Append("';");

                    connection.Query(sql.ToString());
                    sql = new StringBuilder();
                    sql.Append("delete from ");
                    sql.Append(race);
                    sql.Append(" where name ='");
                    sql.Append(Name);
                    sql.Append("';");

                    connection.Query(sql.ToString());


                }

                // Else just remove them.
                else
                {
                    var sql = new StringBuilder();
                    sql.Append("delete from ");
                    sql.Append(race);
                    sql.Append(" where name ='");
                    sql.Append(Boats.name);
                    sql.Append("';");

                    connection.Query(sql.ToString());
                }

            }

        }


        /// <summary>
        /// Inserts sailor into a named race.
        /// </summary>
        /// <param name="Boats">Boat data of person to add.</param>
        /// <param name="race">Name of race.</param>
        public static void InsertInto(Boats Boats, string race)
        {
            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal()))
            {

                var sql = new StringBuilder();
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


        /// <summary>
        /// Adds boats to the race db. Runs logic for for whether they have crew or not.
        /// </summary>
        /// <param name="Boats"></param>
        public static void SetBoats(Boats Boats)
        {
            string race = Program.Globals.racename.Replace(" ", "");
            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal()))
            {

                // If they don't have crew remove single.
                if (Program.Globals.Crew == "")
                {

                    // Try to add them.
                    try
                    {

                        InsertInto(Boats, race);

                    }

                    // Else add new DB for new race then add them
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
                        InsertInto(Boats, race);
                    }
                }

                //Else remove both.
                else
                {

                    // Try to add one person.
                    try
                    {

                        InsertInto(Boats, race);

                    }

                    // Else create db and then add person.
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
                        InsertInto(Boats, race);

                    }

                    // In every case we will add the second without fail.
                    finally
                    {

                        Boats Boats1 = new Boats(Program.Globals.Crew, Boats.boatName, Boats.boatNumber);
                        InsertInto(Boats1, race);
                        
                    }

                }

            }

        }


        /// <summary>
        /// Returns the number of crew a boat has.
        /// </summary>
        /// <param name="boatName">Class of boat</param>
        /// <returns>The number of crew a particular boat has.</returns>
        public static int GetCrew(string boatName)
        {

            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal()))
            {

                return connection.Query<int>("call returncrew(@boatName)", new{boatName = boatName}).FirstOrDefault();
            }

        }

        /// <summary>
        /// Inserts into the db calendar a single event.
        /// </summary>
        /// <param name="cal">A single event.</param>
        public static void newcalendar(Calendar cal)
        {

            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal()))
            {

                connection.Query("call newcalendar(@summary, @description, @date)", new
                {
                    summary = cal.summary,
                    description = cal.description,
                    date = cal.dateTime

                });
            }

        }


        /// <summary>
        /// Finds what events are on today with stored procedure.
        /// </summary>
        /// <returns>List of events on a particular day.</returns>
        public static List<string> todaysevent()
        {

            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal()))
            {

                return connection.Query<string>("call todaysevent").ToList();

            }

        }
    }
}


