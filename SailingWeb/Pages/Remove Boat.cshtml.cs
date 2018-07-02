using Microsoft.AspNetCore.Mvc.RazorPages;
using SailingWeb.Data;
using static SailingWeb.Program;

namespace SailingWeb.Pages
{
    public class RemoveBoatModel : PageModel
    {
        public void OnGet()
        {
            //Reset and return to page.
            Sql.RemoveBoats(Globals.Removeboat);
            Globals.Removeboat = new Boats();
            Globals.Boat = new Boats();
            Globals.AskedCrew = 0;
        }
    }
}