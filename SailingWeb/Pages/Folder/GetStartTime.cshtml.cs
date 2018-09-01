using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace SailingWeb.Pages.Folder
{
    public class GetStartTimeModel : PageModel
    {
        public JsonResult OnGet()
        {
            DateTime x = Sql.GetStartTime(ManageRaceModel.RaceNameStatic);
            if (x != new DateTime())
            {
                return new JsonResult(Sql.GetStartTime(ManageRaceModel.RaceNameStatic));
            }
            else
            {
                return new JsonResult(0);
            }
        }
    }
}