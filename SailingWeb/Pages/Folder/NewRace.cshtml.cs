using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailingWeb.Data;


namespace SailingWeb.Pages.Folder
{
    public class NewRaceModel : PageModel
    {
        public void OnGet(Calendar racename)
        {
            Dictionary<BoatsTidy, List<BoatLap>> dictionary = new Dictionary<BoatsTidy, List<BoatLap>>();
            foreach(var person in Sql.GetRacers(racename))
            {
                dictionary.Add(person, new List<BoatLap>());
            }
            Tuple<Calendar, Dictionary<BoatsTidy, List<BoatLap>>, DateTime> tup = new Tuple<Calendar, Dictionary<BoatsTidy, List<BoatLap>>, DateTime>(racename, dictionary, new DateTime());

            ManageRaceModel.Race = tup;

        }
    }
}