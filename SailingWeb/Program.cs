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
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar;
using Google.Apis.Services;
using System.Security.Cryptography.X509Certificates;
using Google.Apis.Auth.OAuth2;

namespace SailingWeb
{
    public class Program
    {
        public static class Globals
        {
            public static Boats name = new Boats(); // Modifiable
            public static string bla1 = "";
            public static Events Event1 = new Events(); 
        }
        public static async Task<Events> GetCalendar()
        {
            string[] scopes = new string[] {
     CalendarService.Scope.Calendar, // Manage your calendars
 	CalendarService.Scope.CalendarReadonly // View your Calendars
 };




            CalendarService service = new CalendarService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyDSFKJPXA5vQfDAM6iBpo9ShYCnQNEhMZQ",
                
            });

            return await service.Events.List("wfscweb@gmail.com").ExecuteAsync();
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
