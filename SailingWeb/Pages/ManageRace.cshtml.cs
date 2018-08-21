using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailingWeb.Data;
using static SailingWeb.Data.Boats;

namespace SailingWeb.Pages
{
    public class ManageRaceModel : PageModel
    {
        public static int PlaceOf(BoatsTidy boat)
        {
            try
            {
                Dictionary<BoatsTidy, TimeSpan> places = new Dictionary<BoatsTidy, TimeSpan>();
                foreach (var x in Race.Item2)
                {
                    if (x.Value.Count != 0)
                    {
                        TimeSpan totaltime = new TimeSpan();
                        foreach (var y in x.Value)
                        {
                            totaltime += y.LapTime;
                        }
                        TimeSpan averageperlap = totaltime / x.Value.Count;
                        TimeSpan correctedttime = (averageperlap / x.Key.Py) * 1000;
                        places.Add(x.Key, correctedttime);
                    }
                    else
                    {
                       
                    }
                }
                var results = places.OrderBy(x => x.Value);
                return results.ToList().IndexOf(results.Where(x => x.Key == boat).First())+1;
            }
            catch
            {
                return 0;
            }
}  
        public static int NoOfLaps { get
            {
                List<int> list = new List<int>();
                foreach (var person in Race.Item2)
                {
                    list.Add(person.Value.Count);

                }
                try
                {
                    return list.Max();
                }
                catch
                {
                    return 0;
                }
            }
            set {
            }
        }
        public static Tuple<Calendar, Dictionary<BoatsTidy, List<BoatLap>>, DateTime> Race { get; set; }
        [BindProperty]
        public string RaceName { get; set; }
        public static string RaceNameStatic { get; set; }
        public static DateTime StartTime { get; set; }


        public void OnGet()
        {

        }
        public void OnPost()
        {
            if(RaceNameStatic == null)
            RaceNameStatic = RaceName;
 
                RefreshTable();
            

        }
        public void RefreshTable()
        {
            var arr = ManageRaceModel.RaceNameStatic.Split("abc123");
            Calendar cal = new Calendar(arr[0], "", Convert.ToDateTime(arr[1]));

            Dictionary<BoatsTidy, List<BoatLap>> dictionary = new Dictionary<BoatsTidy, List<BoatLap>>();
            foreach (var person in Sql.GetRacers(cal))
            {
                dictionary.Add(person, new List<BoatLap>());
            }
            Tuple<Calendar, Dictionary<BoatsTidy, List<BoatLap>>, DateTime> tup = new Tuple<Calendar, Dictionary<BoatsTidy, List<BoatLap>>, DateTime>(cal, dictionary, StartTime ==null? new DateTime() : StartTime);

            ManageRaceModel.Race = tup;
        }
    }
}