using Dapper;
using SailingWeb.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailingWeb
{
    public static class Sql
    {
        public async static Task<IEnumerable<int>> NoOfLaps( Calendar cal)
        {
            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal()))
            {
                return await connection.QueryAsync<int>("select max(racelap) from races where " +
                    "summary = @summary and eventStart = @eventStart", new
                    {
                        summary = cal.Summary,
                        eventStart = cal.DateTime
                    });

            }
        }
        public async static Task<IEnumerable<BoatLap>> GetLaps(BoatsTidy boat, Calendar cal)
        {
           
            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal()))
            {
                return await connection.QueryAsync<BoatLap>("select racelap, laptime from races where name = @name and " +
                    "summary = @summary", new
                    {
                        name = boat.Name,
                        summary = cal.Summary

                    });


            }
        }
        public async static Task<IEnumerable<int>> NoOfLaps(BoatsTidy boat, Calendar cal)
        {
            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal()))
            {
                return await connection.QueryAsync<int>("select max(racelap) from races where name = @name and " +
                    "summary = @summary", new
                    {
                        name = boat.Name,
                        summary = cal.Summary

                    });
                    
            }
        }

        public async static Task<IEnumerable<BoatsTidy>> GetRacersReel(Calendar cal)
        {
            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal()))
            {
                return await connection.QueryAsync<BoatsTidy>("select name, boat, boatNumber, crew, py, notes from signonlists where" +
                    "summary = @summary and dateTime = @dateTime and sailingClub = @sailingClub"
                    , new
                    {
                        
                        summary = cal.Summary,
                        dateTime = cal.DateTime,
                        sailingClub = "Whitefriars Sailing Club"

                    });


            }
        }
        public async static Task<int> RemoveLap(BoatsTidy boat, Calendar cal, int lapNo)
        {
            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal()))
            {
                return await connection.ExecuteAsync("delete from races where name = @name and " +
                    "summary = @summary and racelap = @racelap and eventStart = @eventStart", new
                    {
                        name = boat.Name,
                        summary = cal.Summary,
                        racelap = lapNo,
                        eventStart = cal.DateTime
                    });


            }
        }

        public async static Task<int> NewLap(BoatsTidy boat, DateTime lapTime, Calendar cal)
        {
            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal()))
            {
                return await connection.ExecuteAsync("insert into races values(@name, @summary, @sailingClub," +
                    " @racelap, @laptime)", new
                    {
                        name = boat.Name,
                        summary = cal.Summary,
                        sailingClub = "Whitefriars Sailing Club",
                        racelap = NoOfLaps(boat, cal),
                        laptime= lapTime

                    });



            }


            }

        /// <summary>
        /// Returns a stored procedure that returns all classes from boat data db.
        /// </summary>
        /// <returns>List of all classes.</returns>
        public static List<String> ReturnClassNames()
        {
            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal()))
            {
                // Runs query.
                var list = connection.Query<BoatClass>("call returnclass").ToList();
                var stringlist = new List<String>();
                foreach(var boat in list)
                {
                    stringlist.Add(boat.BoatName);
                }
                return stringlist;
            }
        }

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

                    string str = "select name, boat, boatNumber, crew, py, notes from signonlists where (summary = @summary and dateTime = @dateTime);";
                    return connection.Query<BoatsTidy>(str, new {
                        summary = Program.Globals.Racename.Summary,
                        dateTime = Program.Globals.Racename.DateTime
                    }).ToList();
                }
                catch
                {
                    var list = new List<BoatsTidy>();
                    list.Add(new BoatsTidy("Nothing to show.", "", "", "", 0, ""));
                    return list;
                }

            }

        }

        /// <summary>
        /// Returns the list of racers
        /// </summary>
        /// <returns>Returns boat data, including if the person is a crew or not.</returns>
        public static List<BoatsTidy> GetRacers(Calendar cal)
        {
            // Tables names cannot have spaces.

            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal()))
            {
                try
                {

                    string str = "select name, boat, boatNumber, crew, py, notes from signonlists where (summary = @summary and dateTime = @dateTime);";
                    return connection.Query<BoatsTidy>(str, new
                    {
                        summary = cal.Summary,
                        dateTime = cal.DateTime
                    }).ToList();
                }
                catch
                {
                    var list = new List<BoatsTidy>();
                    list.Add(new BoatsTidy("Nothing to show.", "", "", "", 0, ""));
                    return list;
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
                return connection.Query<Boats>("select * from fulllist where name = @name", new
                { name = name }).ToList();

            }

        }

        /*
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

        }*/


        /// <summary>
        /// Adds a new boat/person
        /// </summary>
        /// <param name="boat">Boat data to add.</param>
        public async static Task<IEnumerable<dynamic>> SetNewFullBoat(Boats boat)
        {

            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal()))
            {

                //Appends together the query, stops SQL injection.
                // Query.
                return await connection.QueryAsync("insert into fulllist value (@name, @boatName, " +
                    "@boatNumber, @py, @sailingClub)", new {
                        name = boat.Name,
                        boatName = boat.BoatName,
                        boatNumber = boat.BoatNumber,
                        py = boat.Py,
                        sailingClub = "Whitefriars Sailing Club"
                });

            }

        }


        /// <summary>
        /// Removes boats, knows if they are crew or not.
        /// </summary>
        /// <param name="boat"></param>
        public static void RemoveBoats(BoatsTidy boat, Calendar race)
        {
            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal()))
            {
                // Creates query.

                string str = "delete from signonlists where (name = @name and summary = @summary " +
                        "and dateTime = @dateTime and sailingClub = @sailingClub)";
                    connection.Query(str, new {
                            name = boat.Name,
                            summary = race.Summary,
                            dateTime = race.DateTime,
                            sailingClub = "Whitefriars Sailing Club"

                        });
                

            }

        }


        /// <summary>
        /// Inserts sailor into a named race.
        /// </summary>
        /// <param name="boat">Boat data of person to add.</param>
        /// <param name="crew">Are they crew?</param>
        public async static Task<int> SetBoats(BoatsTidy boat, Calendar race)
        {
            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal()))
            {
                string str = "insert into signonlists values(@name, @boatName, @boatNumber, @crew, " +
                    "@py, @notes, @summary, @dateTime, @sailingClub)";
                return await connection.ExecuteAsync(str, new {
                        name = boat.Name,
                        boatName = boat.Boat,
                        boatNumber = boat.BoatNumber,
                        crew = boat.Crew == null? "" : boat.Crew,
                        py = boat.Py,
                        notes = boat.Notes == null ? "" : boat.Crew,
                        summary = race.Summary,
                        dateTime = race.DateTime,
                        sailingClub = "Whitefriars Sailing Club"

                });

            }

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
        /*
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
        */

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


