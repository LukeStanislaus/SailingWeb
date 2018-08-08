using Dapper;
using SailingWeb.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public static List<BoatClass> ReturnClass()
        {
            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal()))
            {
                // Runs query.
                var list = connection.Query<BoatClass>("call returnclass").ToList();
                return list;
            }
        }


        /// <summary>
        /// Returns a list of all the distinct names in the fulllist db. Used a array due to 
        /// compatibility with javascript.
        /// </summary>
        /// <returns>List of names.</returns>
        public static IEnumerable<string> GetNames()
        {
            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal()))
            {
                // Returns a list of all the distinct names in the fulllist db.
                var listOfNames = connection.Query<string>("call returnnames").Distinct();
                listOfNames = listOfNames.Where(x => x != Program.Globals.LastName).ToArray();

                return listOfNames;
            }
        }


        /// <summary>
        /// Returns the list of racers
        /// </summary>
        /// <returns>Returns boat data, including if the person is a crew or not.</returns>
        public static List<BoatsTidy> GetRacers()
        {
            // Tables names cannot have spaces.


            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal()))
            {
                try
                {
                    // Appends together the query. Stops SQL injection.
                    var sql1 = new StringBuilder();
                    sql1.Append("select * from ");
                    sql1.Append(Program.Globals.RacenameTable);
                    sql1.Append(";");
                    return connection.Query<BoatsTidy>(sql1.ToString()).ToList();
                }
                catch
                {
                    return new List<BoatsTidy>();
                }

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
                sql.Append("', '");
                sql.Append(boat.BoatNumber);
                sql.Append("', ");
                sql.Append(boat.Py);
                sql.Append(");");

                // Query.
                connection.Query(sql.ToString());

            }

        }


        /// <summary>
        /// Removes boats, knows if they are crew or not.
        /// </summary>
        /// <param name="boat"></param>
        public static void RemoveBoats(BoatsTidy boat, string race)
        {




            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal()))
            {

                race = Program.ReturnRaceString(race);
                // Creates query.

                    var sql = new StringBuilder();
                    sql.Append("delete from ");
                    sql.Append(race);
                    sql.Append(" where name ='");
                    sql.Append(boat.Name);
                    sql.Append("';");

                    connection.Query(sql.ToString());
                

            }

        }


        /// <summary>
        /// Inserts sailor into a named race.
        /// </summary>
        /// <param name="boat">Boat data of person to add.</param>
        /// <param name="crew">Are they crew?</param>
        public static void SetBoats(BoatsTidy boat, string race)
        {
            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal()))
            {
                connection.Query(CreateTable(race));
                var sql = new StringBuilder();
                sql.Append("insert into ");
                sql.Append(race);
                sql.Append(" values('");
                sql.Append(boat.Name);
                sql.Append("','");
                sql.Append(boat.Boat);
                sql.Append("','");
                sql.Append(boat.BoatNumber);
                sql.Append("','");
                sql.Append(boat.Crew != null ? boat.Crew : "");
                sql.Append("',");
                sql.Append(boat.Py != 0 ? boat.Py : 0);
                sql.Append(",'");
                sql.Append(boat.Notes != null ? boat.Notes : "");
                sql.Append("');");
                connection.Query(sql.ToString());

            }

        }


        private static string CreateTable(string tablename)
        {
            var sql = new StringBuilder();
            sql.Append("CREATE TABLE if not exists  ");
            sql.Append(tablename);
            sql.Append(
                "(`name` VARCHAR(45) NOT NULL, " +
                "`boat` VARCHAR(45) NOT NULL, " +
                "`boatNumber` VARCHAR(45) NOT NULL, " +
                "`crew` VARCHAR(45) NULL,`py` INT(10) NOT NULL, " +
                "`notes` VARCHAR(150) NULL,PRIMARY KEY(`name`, `boat`, `boatNumber`, `py`)); ");
            return sql.ToString();
        }

        /*
        /// <summary>
        /// Adds boats to the race db. Runs logic for for whether they have crew or not.
        /// </summary>
        /// <param name="boat"></param>
        public static void SetBoats(Boats boat)
        {
            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal()))
            {

                // If they don't have crew add single.
                if (Program.Globals.Crew == null)
                {

                    // Try to add them.
                    try
                    {

                        InsertInto(boat, 0);

                    }

                    // Else add new DB for new race then add them
                    catch
                    {

                        connection.Execute(CreateTable(_race));
                        InsertInto(boat, 0);
                    }
                }

                //Else remove both.
                else
                {

                    // Try to add one person.
                    try
                    {

                        InsertInto(boat,0);

                    }

                    // Else create db and then add person.
                    catch
                    {

                        connection.Execute(CreateTable(_race));
                        InsertInto(boat,0);

                    }

                    // In every case we will add the second without fail.
                    //                    finally
                    //                    {

                    var boats1 = new Boats(Program.Globals.Crew, boat.BoatName, boat.BoatNumber, ReturnClass().FindAll(x => x.boatName == boat.BoatName).First().py);
                        InsertInto(boats1,1);
                        
//                    }

                }

            }

        }
        */

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
        public static List<Calendar> Todaysevent()
        {

            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal()))
            {

                var value = connection.Query<Calendar>("call todaysevent").ToList();
                return value;
            }

        }
    }
}


