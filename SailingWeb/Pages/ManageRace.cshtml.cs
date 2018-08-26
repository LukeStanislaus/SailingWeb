using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using SailingWeb.Data;
using static SailingWeb.RaceHelpers;
using static SailingWeb.Data.Boats;

namespace SailingWeb.Pages
{
    public class ManageRaceModel : PageModel
    {
        public static Tuple<Calendar, Dictionary<BoatsTidy, List<BoatLap>>, DateTime> Race { get; set; }

        [BindProperty]
        public string RaceName { get; set; }

        public static string RaceNameStatic { get; set; }

        public void OnGet()
        {
            if (RaceNameStatic == null)
                RaceNameStatic = RaceName;
            try
            {
                System.IO.File.WriteAllText("wwwroot/json/race.json", JsonConvert.SerializeObject(ConvertToJSON(Race), Formatting.Indented));
            }
            catch (NullReferenceException)
            {

            }
        }

        public void OnPost()
        {
            if (RaceNameStatic == null)
                RaceNameStatic = RaceName;

            RefreshTable(RaceNameStatic, Race);

        }

    }
}