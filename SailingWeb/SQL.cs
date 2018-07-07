using Dapper;
using SailingWeb.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SailingWeb
{
    public static class Sql
    {
        /// <summary>
        /// Returns a stored procedure that returns all classes from boat data db.
        /// </summary>
        /// <returns>List of all classes.</returns>
        public static List<string> ReturnClass()
        {
            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal()))
            {
                // Runs query.
                return connection.Query<string>("call returnclass").ToList();
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
        /// <param name="name">Person to search for boats.</param>
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
        /// <param name="boat">Boat data to add.</param>
        public static void SetNewFullBoat(Boats boat)
        {

            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal()))
            {

                //Appends together the query, stops SQL injection.
                var sql = new StringBuilder();
                sql.Append("insert into fulllist values ('");
                sql.Append(boat.Name);
                sql.Append("', '");
                sql.Append(boat.BoatName);
                sql.Append("', ");
                sql.Append(boat.BoatNumber);
                sql.Append(");");

                // Query.
                connection.Query(sql.ToString());

            }

        }


        /// <summary>
        /// Removes boats, knows if they are crew or not.
        /// </summary>
        /// <param name="boat"></param>
        public static void RemoveBoats(Boats boat)
        {

            // Cannot have table name with space 
            var race = Program.Globals.Racename.Replace(" ", "");

            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal()))
            {


                // Creates query.
                var sql1 = new StringBuilder();
                sql1.Append("select * from ");
                sql1.Append(race);
                sql1.Append(" where boat ='");
                sql1.Append(boat.BoatName);
                sql1.Append("' and boatNumber=");
                sql1.Append(boat.BoatNumber);
                sql1.Append(" and crew=1;");
                var name = connection.Query<BoatsRacing>(sql1.ToString()).FirstOrDefault().Name;


                // If they have a crew, remove both.
                if (name != null)
                {
                    var sql = new StringBuilder();
                    sql.Append("delete from ");
                    sql.Append(race);
                    sql.Append(" where name ='");
                    sql.Append(boat.Name);
                    sql.Append("';");

                    connection.Query(sql.ToString());
                    sql = new StringBuilder();
                    sql.Append("delete from ");
                    sql.Append(race);
                    sql.Append(" where name ='");
                    sql.Append(name);
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
                    sql.Append(boat.Name);
                    sql.Append("';");

                    connection.Query(sql.ToString());
                }

            }

        }


        /// <summary>
        /// Inserts sailor into a named race.
        /// </summary>
        /// <param name="boat">Boat data of person to add.</param>
        /// <param name="race">Name of race.</param>
        private static void InsertInto(Boats boat, string race)
        {
            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal()))
            {

                var sql = new StringBuilder();
                sql.Append("insert into ");
                sql.Append(race);
                sql.Append(" values('");
                sql.Append(boat.Name);
                sql.Append("','");
                sql.Append(boat.BoatName);
                sql.Append("','");
                sql.Append(boat.BoatNumber);
                sql.Append("',0);");
                connection.Query(sql.ToString());

            }

        }


        /// <summary>
        /// Adds boats to the race db. Runs logic for for whether they have crew or not.
        /// </summary>
        /// <param name="boat"></param>
        public static void SetBoats(Boats boat)
        {
            var race = Program.Globals.Racename.Replace(" ", "");
            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal()))
            {

                // If they don't have crew remove single.
                if (Program.Globals.Crew == "")
                {

                    // Try to add them.
                    try
                    {

                        InsertInto(boat, race);

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
                        InsertInto(boat, race);
                    }
                }

                //Else remove both.
                else
                {

                    // Try to add one person.
                    try
                    {

                        InsertInto(boat, race);

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
                        InsertInto(boat, race);

                    }

                    // In every case we will add the second without fail.
                    finally
                    {

                        var boats1 = new Boats(Program.Globals.Crew, boat.BoatName, boat.BoatNumber);
                        InsertInto(boats1, race);
                        
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
        public static void Newcalendar(Calendar cal)
        {

            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal()))
            {

                connection.Query("call newcalendar(@summary, @description, @date)", new
                {
                    summary = cal.Summary,
                    description = cal.Description,
                    date = cal.DateTime

                });
            }

        }


        /// <summary>
        /// Finds what events are on today with stored procedure.
        /// </summary>
        /// <returns>List of events on a particular day.</returns>
        public static List<string> Todaysevent()
        {

            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal()))
            {

                return connection.Query<string>("call todaysevent").ToList();

            }

        }
    }
}


