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
    public class RemoveBoatPromptModel : PageModel
    {
        public void OnGet(string boat, string cal)
        {
            var x = JsonConvert.DeserializeObject<BoatsTidy>(boat);
            var y = JsonConvert.DeserializeObject<Calendar>(cal);
            Sql.RemoveBoats(x, y);
        }
    }
}