using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SailingWeb.Data;

namespace SailingWeb.Pages.Folder
{
    public class UpdateTimeModel : PageModel
    {
        public async void OnGet(string name, int lapNumber, string lapTime)
        {
            TimeSpan d;
            Console.WriteLine(lapTime);
            try
            {
               d = DateTime.ParseExact(lapTime, "HH:mm:ss", CultureInfo.InvariantCulture).TimeOfDay;

            }
            catch
            {

                d = DateTime.ParseExact(lapTime, "d.HH:mm:ss", CultureInfo.InvariantCulture).TimeOfDay;
            }
            var boat1 = JsonConvert.DeserializeObject<BoatsTidy>(JsonConvert.DeserializeObject(name).ToString());
            Sql.RemoveLap(boat1, ManageRaceModel.RaceNameStatic, lapNumber, false);
            await Sql.NewLap(boat1, Sql.GetStartTime(ManageRaceModel.RaceNameStatic) + d, ManageRaceModel.RaceNameStatic, lapNumber);

        }
    }
}