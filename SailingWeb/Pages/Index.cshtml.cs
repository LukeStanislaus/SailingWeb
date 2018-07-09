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
            Globals.AskedCrew = 0;
            Globals.Crew = "";
            Globals.Racename = new Calendar();
            Globals.Removeboat = new Boats();
        }
    }
}
