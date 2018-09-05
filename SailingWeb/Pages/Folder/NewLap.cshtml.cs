using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SailingWeb.Data;
using System;

namespace SailingWeb.Pages.Folder
{
    public class NewLapModel : PageModel
    {

        public static int x = 0;
        public void OnGet(string boat, DateTime lapTime)
        {
            try
            {
                BoatsTidy boat1 = JsonConvert.DeserializeObject<BoatsTidy>(JsonConvert.DeserializeObject(boat).ToString());
                Sql.NewLap(boat1, lapTime, ManageRaceModel.RaceNameStatic);
            }
            catch (NullReferenceException)
            {

            }
            catch (ArgumentNullException)
            {

            }
        }

    }
}