﻿using System.Threading.Tasks;
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
            Boat.Py = Sql.ReturnClass().Find(x => x.BoatName == Boat.BoatName).Py;
            // Remove boat from db
            await Sql.SetNewFullBoat(Boat);
            // Create boat alerttext
            Program.Globals.Alerttext = "You have added your boat, a " + Boat.BoatName + 
                                        " with boat number " + Boat.BoatNumber + ".";
            //Return home.
            return RedirectToPage("/Index");
        }
    }
}