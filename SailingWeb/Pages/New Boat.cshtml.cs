using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailingWeb.Data;

namespace SailingWeb.Pages
{
    public class TestModel : PageModel
    {
        [BindProperty]
        public Boats Boat { get; set; }
        
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync()
        {
            // Remove boat from db
            Sql.SetNewFullBoat(Boat);
            // Create boat alerttext
            Program.Globals.Alerttext = "You have added your boat, a " + Boat.BoatName + 
                                        " with boat number " + Boat.BoatNumber + ".";
            //Return home.
            return RedirectToPage("/Index");
        }
    }
}