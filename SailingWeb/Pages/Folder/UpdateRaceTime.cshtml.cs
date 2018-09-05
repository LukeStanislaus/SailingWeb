using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace SailingWeb.Pages.Folder
{
    public class UpdateRaceTimeModel : PageModel
    {
        public void OnGet(string racecal, string racetime)
        {
            var cal = JsonConvert.DeserializeObject<Calendar>(racecal);
            DateTime d;
            //  d = DateTime.ParseExact(racetime, "HH:mm:ss", CultureInfo.InvariantCulture);
            d = DateTime.Parse(racetime);
            Sql.SetStartTime(cal, d);

        }
    }
}