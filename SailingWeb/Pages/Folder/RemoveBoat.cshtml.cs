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
        public void OnGet(int indexof)
        {
            try
            {
                Sql.RemoveBoats(Sql.GetRacers()[indexof], Program.Globals.Racename);
                Program.Globals.Alerttext = Sql.GetRacers()[indexof].Name + " has been removed from the race.";
            }
            catch
            {

            }
        }
    }
}