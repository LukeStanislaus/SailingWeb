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
        public void OnGet(string boat, DateTime lapTime, int lapNumber)
        {
            if(x==0)
            {
                bool z = true;
                x++;
                var boat1 = JsonConvert.DeserializeObject<BoatsTidy>(boat.ToString());

                var lapTime2 = lapTime.Subtract(ManageRaceModel.StartTime);

                BoatLap lap = new BoatLap(lapNumber, lapTime2);
                foreach (KeyValuePair<BoatsTidy, List<BoatLap>> x in ManageRaceModel.Race.Item2)
                    if (x.Key.Boat.Equals(boat1.Boat) && x.Key.BoatNumber.Equals(boat1.BoatNumber) && x.Key.Crew.Equals(boat1.Crew) &&
                        x.Key.Name.Equals(boat1.Name) && x.Key.Notes.Equals(boat1.Notes) &&
                        x.Key.Py.Equals(boat1.Py))
                    {
                        foreach (var y in x.Value)
                        {
                            if (y.LapNumber == lap.LapNumber)
                                z = false; break;
                            
                        }
                        if(z)
                        ManageRaceModel.Race.Item2[x.Key].Add(lap);
                    }
                x--;
            }
            
        }
    }
}