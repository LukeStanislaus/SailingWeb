using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SailingWeb.Data;

namespace SailingWeb.Pages.Folder
{
    public class ReturnPlaceModel : PageModel
    {
        public JsonResult OnGet(string boat)
        {
            var boat1 = JsonConvert.DeserializeObject<BoatsTidy>(boat.ToString());
            try
            {
                return new JsonResult(Sql.PlaceOf(ManageRaceModel.RaceNameStatic, boat1));
            }
            catch
            {
                return new JsonResult(0);
            }
        }
    }
}