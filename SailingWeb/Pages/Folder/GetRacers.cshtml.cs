using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SailingWeb.Pages.Folder
{
    public class GetRacersModel : PageModel
    {
        public JsonResult OnGet(string request)
        {
            var x = request.Split("abc123");
            return  new JsonResult(Sql.GetRacers(new Calendar(x[0], "", Convert.ToDateTime(x[1]))));
        }
    }
}