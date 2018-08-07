using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using SailingWeb.Data;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using static Google.Apis.Calendar.v3.EventsResource.ListRequest;
using Humanizer;

namespace SailingWeb
{
    public class Program
    {

        public static class Globals
        {
            /// <summary>
            /// Stores todays events to increase efficiency TODO fix this so that it works on a second day without rerunning the program.
            /// </summary>
            public static List<Calendar> Todaysevents = Sql.Todaysevent();
            /// <summary>
            /// Checking whether or not the user has said yes or no to the popup for removing from themselves from the database.
            ///<para>True if they said yes, false if they said no.</para>
            /// </summary>
            public static bool Response = false; //Modifiable
            /// <summary>
            /// This is where the name of the crew number is stored.
            /// </summary>
            public static string Crew = "";
            /// <summary>
            /// This is where the information about the boat being removed is stored. 
            /// It is set by calculating the last boat added when response is true.
            /// </summary>
            public static BoatsTidy Removeboat = new BoatsTidy();
            /// <summary>
            /// This is where the race name is stored for use in the SQL queries.
            ///<para></para>
            /// </summary>
            public static Calendar Racename = new Calendar(); //Modifiable

            public static string RacenameTable
            {
                get
                {
                    var racenametemp = new StringBuilder();
                    racenametemp.Append(Racename.Summary.Replace(" ", "").Replace("&", ""));
                    racenametemp.Append(Racename.DateTime.ToString().Replace(" ", "").Replace("/", "").Replace(":", ""));
                    return racenametemp.ToString();
                }
                private set => RacenameTable = value;
            }

            /// <summary>
            ///This is the location of the alert text. This is also used by the confirmation popup. 
            /// Within the html, immediately after the javascript has been run it is turned back to quotes. 
            ///<para>Feels clunky.</para>
            /// </summary>
            public static string Alerttext = ""; //Modifiable
            /// <summary>
            /// Within the enter race page, this checks if the user has been asked about the crew or not.
            ///<para>
            /// 0 if not asked, 1 if asked.
            /// Extremely clunky. Must fix.</para>
            /// </summary>
            public static int AskedCrew = 0; // Modifiable
            /// <summary>
            /// This is where the information of the boat is store. I have a bunch of very inefficient 
            /// if statements in the enter race page that seem unstable and probably will not hold up 
            /// with error handling. 
            /// </summary>
            public static Boats Boat = new Boats(); // Modifiable
            /// <summary>
            ///  This is where the calendar data is stored so we don't have to load each time.
            /// </summary>
            public static Events Event1 = new Events();
        }


        /// <summary>
        /// Calls the calendar async, should only be called once.
        /// </summary>
        /// <returns>Returns the event data async.</returns>
        public static async Task<Events> GetCalendar()
        {
            /*
            // Set the scopes for the API call.
            string[] scopes = new string[] {
                CalendarService.Scope.Calendar, // Manage your calendars
                CalendarService.Scope.CalendarReadonly // View your Calendars
            };
            */
            // Create the calendar service, enter API key.
            CalendarService service = new CalendarService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyDSFKJPXA5vQfDAM6iBpo9ShYCnQNEhMZQ",

            });
            // Setting the calendar ID.
            EventsResource.ListRequest req = service.Events.List("wfscweb@gmail.com");

            // Setting the time limits. This calls all the events from today onwards.
            req.TimeMin = new DateTime(DateTime.Now.Year,
                DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

            // Setting the order, when the calendar is displayed this helps a lot.
            req.OrderBy = OrderByEnum.StartTime;

            // Not sure what this does but it is required to set the order.
            req.SingleEvents = true;

            // Gets the data async.
            return await req.ExecuteAsync();

        }


        /// <summary>
        /// Sets the alert text and resets globals. Including crew through logic.
        /// </summary>
        /// <param name="Boat">The boat they are sailing.</param>
        /// TODO Use if statements for an/a
        /// TODO Why aren't the crew names coming in properly?
        /// TODO Why aren't quotes working?
        public static void Exit(BoatsTidy boat, Calendar cal)
        {
            if (Globals.Alerttext == "")
            {
                if (boat.Crew == null)
                {
                    Globals.Alerttext = "You, " + boat.Name + " have been entered into the race "
    + cal.Summary + " sailing a " + boat.Boat + " with boat number "
    + boat.BoatNumber + ". The race starts in " + cal.DateTime.TimeOfDay.Humanize(5) + ". Good Luck!";
                }
                else
                {
                    // Sets the alert text, plus crew.
                    Globals.Alerttext = "You, " + boat.Name + " have been entered into the race "
                        + cal.Summary + " sailing a " + boat.Boat + " with boat number "
                        + boat.BoatNumber + ". Your crew is " + boat.Crew + ". The race starts in "+cal.DateTime.TimeOfDay.Humanize(5)+". Good Luck!";
                }

            }



        }

        public static Calendar ReturnRaceDateTime(string calname)
        {
            return Sql.Todaysevent().Find(x => x.Summary.Equals(calname));
        }

        public static string ReturnRaceString(string cal)
        {
            var calendar = Sql.Todaysevent().Find(x => x.Summary.Equals(cal));
            var racenametemp = new StringBuilder();
            racenametemp.Append(calendar.Summary.Replace(" ", "").Replace("&", ""));
            racenametemp.Append(calendar.DateTime.ToString().Replace(" ", "").Replace("/", "").Replace(":", ""));
            return racenametemp.ToString();
        }

        /// <summary>
        /// Main. Runs Website.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            GetCalendar();
            //Runs website.
            BuildWebHost(args).Run();
        }

        /// <summary>
        /// Runs website.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
