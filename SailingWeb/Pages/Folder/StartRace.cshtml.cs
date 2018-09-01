using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SailingWeb.Data;

namespace SailingWeb.Pages.Folder
{
    public class StartRaceModel : PageModel
    {
        public static DateTime JavaTimeStampToDateTime(double javaTimeStamp)
        {
            // Java timestamp is milliseconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(javaTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public void OnGet(string dateTime)
        {
            Sql.SetStartTime(ManageRaceModel.RaceNameStatic, JavaTimeStampToDateTime(double.Parse(dateTime)));
            /*
            var datetime = JavaTimeStampToDateTime(double.Parse(dateTime)).AddMinutes(0);
            var arr = ManageRaceModel.RaceNameStatic.Split("abc123");
            Calendar cal = new Calendar(arr[0], "", Convert.ToDateTime(arr[1]));

            Dictionary<BoatsTidy, List<BoatLap>> dictionary = new Dictionary<BoatsTidy, List<BoatLap>>();
            foreach (var person in Sql.GetRacers(cal))
            {
                dictionary.Add(person, new List<BoatLap>());
            }
            Tuple<Calendar, Dictionary<BoatsTidy, List<BoatLap>>, DateTime> tup = new Tuple<Calendar, Dictionary<BoatsTidy, List<BoatLap>>, DateTime>(cal, dictionary, datetime);
            //ManageRaceModel.Race.Item3 = DateTime.Now.AddMinutes(5);
            ManageRaceModel.Race = tup;*/
        }
    }
}