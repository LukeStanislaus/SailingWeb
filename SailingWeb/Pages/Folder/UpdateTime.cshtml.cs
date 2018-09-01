﻿using System;
using System.Collections.Generic;
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
        public async void OnGet(string name, int lapNumber, TimeSpan lapTime)
        {
            var boat1 = JsonConvert.DeserializeObject<BoatsTidy>(name.ToString());
            await Sql.RemoveLap(boat1, ManageRaceModel.RaceNameStatic, lapNumber);
            await Sql.NewLap(boat1, Sql.GetStartTime(ManageRaceModel.RaceNameStatic) + lapTime, ManageRaceModel.RaceNameStatic, lapNumber);

        }
    }
}