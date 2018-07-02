using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SailingWeb.Pages
{
    public class ContactModel : PageModel
    {
        public string Message { get; set; }
        [BindProperty]
        public string race { get; set; }
        public void OnGet()
        {
            Program.Globals.Event1 = Program.GetCalendar().Result;
            Message = "Your contact page.";
        }
        public async Task<IActionResult> OnPostAsync()
        {
            Program.Globals.Racename = race;

            return Page();
        }

    }
}
