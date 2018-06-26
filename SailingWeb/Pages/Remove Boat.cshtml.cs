﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailingWeb.Data;
using static SailingWeb.Program;

namespace SailingWeb.Pages
{
    public class RemoveBoatModel : PageModel
    {
        public void OnGet()
        {
            SQL.RemoveBoats(Program.Globals.removeboat, Program.Globals.racename);
            Program.Globals.removeboat = new Data.Boats();
            Globals.name = new Boats();
            Globals.namecrew = new Boats();
            Globals.askedCrew = 0;
        }
    }
}