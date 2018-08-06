using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SailingWeb.Pages.Folder
{
    public class GetCountModel : PageModel
    {
        public JsonResult OnGet(string name)
        {


            var result = new JsonResult(name != null ? Sql.GetBoats(name).Count : 0);
            string str = result.Value.ToString();
            return result;
        }
    }
}