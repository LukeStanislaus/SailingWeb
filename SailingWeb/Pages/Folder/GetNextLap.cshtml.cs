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
    public class GetNextLapModel : PageModel
    {
        public JsonResult OnGet(string boat)
        {
            var boat1 = JsonConvert.DeserializeObject<BoatsTidy>(boat.ToString());
            var laps = Sql.GetLaps(boat1, ManageRaceModel.RaceNameStatic);
            return new JsonResult((laps.Max(x => x.LapNumber))+1);


            }
    }
}