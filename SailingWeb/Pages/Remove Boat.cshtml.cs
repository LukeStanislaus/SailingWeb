using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SailingWeb.Pages
{
    public class RemoveBoatModel : PageModel
    {
        public void OnGet()
        {
            SQL.RemoveBoats(Program.Globals.removeboat, Program.Globals.racename);
        }
    }
}