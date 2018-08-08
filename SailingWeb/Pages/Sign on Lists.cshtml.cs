using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailingWeb.Data;

namespace SailingWeb.Pages
{
    public class ContactModel : PageModel
    {
        public string Message { get; set; }
        [BindProperty]
        public string race { get; set; }
        //[BindProperty]
        //public List<BoatsTidy> List { get; set; }

        public void OnGet()
        {
            Program.Globals.Event1 = Program.GetCalendar().Result;
            Message = "Your contact page.";
        }
        public async Task<IActionResult> OnPostAsync()
        {
            for (int i = 0; i < Sql.GetRacers().Count; i++)
            {
                var thing = Sql.GetRacers()[i];
                int integer = i;
            }
            if (race != null)
            {
                Program.Globals.Racename = Program.Globals.Todaysevents[int.Parse(race)];
            }
            //var list = BoatsTidy.Tidyup(Sql.GetRacers());
            return Page();
        }

    }
}
