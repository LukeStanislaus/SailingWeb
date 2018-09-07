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
            await Sql.SetNewFullBoat(Boat);
            // Create boat alerttext
            //Return home.
            return RedirectToPage("/Index");
        }
    }
}