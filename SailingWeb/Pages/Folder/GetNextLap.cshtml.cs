﻿using System;
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
            foreach (KeyValuePair<BoatsTidy, List<BoatLap>> x in ManageRaceModel.Race.Item2)
                if (x.Key.Boat.Equals(boat1.Boat) && x.Key.BoatNumber.Equals(boat1.BoatNumber) && x.Key.Crew.Equals(boat1.Crew) &&
                    x.Key.Name.Equals(boat1.Name) && x.Key.Notes.Equals(boat1.Notes) &&
                    x.Key.Py.Equals(boat1.Py))
                {
                    return new JsonResult(x.Value.Count);
                }
            return new JsonResult("Error");
            }
    }
}