using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SailingWeb.Data;

namespace SailingWeb.Pages.Folder
{
    public class NewLapModel : PageModel
    {

        public static int x = 0;
        public async void OnGet(string boat, DateTime lapTime, int lapNumber)
        {
            var boat1 = JsonConvert.DeserializeObject<BoatsTidy>(boat.ToString());
            var x = await Sql.NewLap(boat1, lapTime, ManageRaceModel.RaceNameStatic);
        }
    }
}