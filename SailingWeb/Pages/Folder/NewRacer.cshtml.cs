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
    public class NewRacerModel : PageModel
    {
        public async void OnGet(string boat, string cal, string crew)
        {
            if (crew != null)
            {
                var y = JsonConvert.DeserializeObject<Calendar>(cal);
                var x = JsonConvert.DeserializeObject<BoatsTidy>(boat);
                var z = new BoatsTidy(x.Name, x.Boat, x.BoatNumber, crew, x.Py, x.Notes);
                await Sql.SetBoats(x, y);
            }
            else
            {
                var y = JsonConvert.DeserializeObject<Calendar>(cal);
                var x = JsonConvert.DeserializeObject<BoatsTidy>(boat);
                await Sql.SetBoats(x, y);
            }

        }
    }
}