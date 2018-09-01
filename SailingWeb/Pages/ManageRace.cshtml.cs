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
        //public static Tuple<Calendar, Dictionary<BoatsTidy, List<BoatLap>>, DateTime> Race { get; set; }

        [BindProperty]
        public string RaceName { get; set; }

        public static Calendar RaceNameStatic { get; set; }

        public void OnGet()
        {
            if (RaceNameStatic == null && RaceName != null)
            {
                RaceNameStatic = Sql.Todaysevent().Where(x => x.Summary == RaceName.Split("abc123")[0] &&
                    Convert.ToDateTime(RaceName.Split("abc123")[1]) == x.DateTime
                    ).First();
                RaceName = null;
            }

            /*
            try
            {
                System.IO.File.WriteAllText("wwwroot/json/race.json", JsonConvert.SerializeObject(ConvertToJSON(Race), Formatting.Indented));
            }
            catch (NullReferenceException)
            {

            }*/
        }

        public void OnPost()
        {
            if (RaceNameStatic == null && RaceName != null)
            {
                RaceNameStatic = Sql.Todaysevent().Where(x => x.Summary == RaceName.Split("abc123")[0] &&
                    Convert.ToDateTime(RaceName.Split("abc123")[1]) == x.DateTime
                    ).First();
                RaceName = null;
            }

            //  RefreshTable(RaceNameStatic, Race);

        }

    }
}