using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SailingWeb.Data;
using static SailingWeb.RaceHelpers;

namespace SailingWeb.Pages
{
    public class ParseRaceModel : PageModel
    {
        [BindProperty]
        public IFormFile Upload { get; set; }

        public List<JSONRace> RaceJSON;

        public static Tuple<Calendar, Dictionary<BoatsTidy, List<BoatLap>>, DateTime> Race { get; set; }

        public async Task OnPostAsync()
        {
            var file = "wwwroot/json/parserace.json";
            //var file = Path.Combine(_environment.ContentRootPath, "uploads", Upload.FileName);
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await Upload.CopyToAsync(fileStream);
            }
            try
            {
                RaceJSON = JsonConvert.DeserializeObject<List<JSONRace>>(System.IO.File.ReadAllText(file));
                Race = JSONtoTuple(RaceJSON);
            }
            catch
            {

            }
        }

        public void OnGetAsync()
        {
            var file = "wwwroot/json/parserace.json";
            try
            {
                RaceJSON = JsonConvert.DeserializeObject<List<JSONRace>>(System.IO.File.ReadAllText(file));
                Race = JSONtoTuple(RaceJSON);
            }
            catch
            {

            }
        }

    }
}