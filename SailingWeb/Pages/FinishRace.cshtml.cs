using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace SailingWeb.Pages.Folder
{
    public class FinishRaceModel : PageModel
    {
        [BindProperty]
        public string Race { get; set; }
        [BindProperty]
        public string RaceCookie { get; set; }
        public void OnGet()
        {

        }
        public void OnPost()
        {
            try
            {
                ViewData["race"] = JsonConvert.DeserializeObject<Calendar>(RaceCookie);
            }
            catch
            {
                Calendar cal = JsonConvert.DeserializeObject<Calendar>(Race);
                ViewData["race"] = cal;
            }
        }
        public void OnPostCookie()
        {

        }
        public void OnPostRemoveRace()
        {

        }
    }
}