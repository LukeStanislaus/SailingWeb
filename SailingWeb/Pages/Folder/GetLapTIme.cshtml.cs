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
    public class GetLapTImeModel : PageModel
    {
        public JsonResult OnGet(string boat, int lapNumber)
        {
            var boat1 = JsonConvert.DeserializeObject<BoatsTidy>(JsonConvert.DeserializeObject(boat).ToString());
            return new JsonResult(Sql.GetLaps(boat1, ManageRaceModel.RaceNameStatic).Where(y => y.LapNumber == lapNumber).First().LapTime.ToString(@"d\:hh\:mm\:ss"));
            
        }
    }
}