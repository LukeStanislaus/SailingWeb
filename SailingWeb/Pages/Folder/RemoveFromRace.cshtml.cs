using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SailingWeb.Pages.Folder
{
    public class RemoveFromRaceModel : PageModel
    {
        public void OnGet()
        {
            Sql.RemoveBoats(Program.Globals.Removeboat, Program.Globals.Racename);
        }
    }
}