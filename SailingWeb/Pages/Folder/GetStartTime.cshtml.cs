using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SailingWeb.Pages.Folder
{
    public class GetStartTimeModel : PageModel
    {
        public JsonResult OnGet()
        {
            var time = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;
            return new JsonResult((ManageRaceModel.StartTime.ToUniversalTime().Ticks - time)/10000);
        }
    }
}