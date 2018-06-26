using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailingWeb.Data;

namespace SailingWeb.Pages
{
    public class TestModel : PageModel
    {
        [BindProperty]
        public Boats boat { get; set; }
        
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync()
        {
            SQL.SetNewFullBoat(boat);
            Program.Globals.alerttext = "You have added your boat, a " + boat.boatName + " with boat number " + boat.boatNumber + ".";
            return RedirectToPage("/Index");
        }
    }
}