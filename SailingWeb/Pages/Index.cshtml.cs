using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailingWeb.Data;
using static SailingWeb.Program;

namespace SailingWeb.Pages
{
    public class IndexModel : PageModel
    {

        public void OnGet()
        {
            //Reset globals. Untidy.
            Globals.Boat = new Boats();
            Globals.askedCrew = 0;
            Globals.Crew = "";
            Globals.racename = "";
            Globals.removeboat = new Boats();
        }
    }
}
