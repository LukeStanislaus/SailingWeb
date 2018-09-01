using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SailingWeb.Pages.Folder
{
    public class RemoveRaceModel : PageModel
    {
        public void OnGet()
        {
            ManageRaceModel.RaceNameStatic = null;
            //ManageRaceModel.Race.Item3 = new DateTime();
        }
    }
}