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
    public class RemoveTimeModel : PageModel
    {
        public async void OnGet(string name, int lapNumber)
        {

            var boat1 = JsonConvert.DeserializeObject<BoatsTidy>(JsonConvert.DeserializeObject(name).ToString());
            Sql.RemoveLap(boat1, ManageRaceModel.RaceNameStatic, lapNumber, true);
        }
    }
}