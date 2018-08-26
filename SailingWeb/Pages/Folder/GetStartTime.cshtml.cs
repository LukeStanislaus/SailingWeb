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
            try
            {
                if (ManageRaceModel.Race.Item3 != DateTime.MinValue)
                {
                    var time = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;
                    return new JsonResult((ManageRaceModel.Race.Item3.ToUniversalTime().Ticks - time) / 10000);
                }
                return new JsonResult(0);
            }
            catch
            {
                return new JsonResult(0);
            }
        }
    }
}