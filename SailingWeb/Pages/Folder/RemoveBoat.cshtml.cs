using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SailingWeb.Pages.Folder
{
    public class RemoveBoatModel : PageModel
    {
        public void OnGet(int indexof, string race)
        {
            try
            {
                var race1 = race.Split("abc123");
                Sql.RemoveBoats(Sql.GetRacers(new Calendar(race1[0], "", Convert.ToDateTime(race1[1]))
                    )[indexof], new Calendar(race1[0], "", Convert.ToDateTime(race1[1])));
                //Program.Globals.Alerttext = Sql.GetRacers()[indexof].Name + " has been removed from the race.";
            }
            catch
            {

            }
        }
    }
}