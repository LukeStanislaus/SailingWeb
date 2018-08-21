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
        public void OnGet(string name, int lapNumber)
        {
            var boat1 = JsonConvert.DeserializeObject<BoatsTidy>(name.ToString());
            foreach (KeyValuePair<BoatsTidy, List<BoatLap>> x in ManageRaceModel.Race.Item2)
                if (x.Key.Boat.Equals(boat1.Boat) && x.Key.BoatNumber.Equals(boat1.BoatNumber) && x.Key.Crew.Equals(boat1.Crew) &&
                    x.Key.Name.Equals(boat1.Name) && x.Key.Notes.Equals(boat1.Notes) &&
                    x.Key.Py.Equals(boat1.Py))
                {
                    x.Value.OrderBy(z => z.LapNumber);
                    for (int i = (lapNumber); i < (x.Value.Count +1); i++)
                    {
                        try
                        {
                            if (x.Value[i] != null)
                            {
                                x.Value[i].LapNumber = x.Value[i].LapNumber - 1;
                                x.Value[i - 1] = x.Value[i];

                            }
                        }
                        catch
                        {
                            x.Value.Remove(x.Value[i - 1]);
                        }
                    }
                }

                    
        }
    }
}