using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SailingWeb.Pages.Folder
{
    public class GetAllModel : PageModel
    {
        public JsonResult OnGet(string name)
        {
            var list = new List<string>();
            foreach (var x in Sql.GetBoats(name))
            {
                list.Add(x.BoatName);
            }
            var json = new JsonResult(name != null ? Sql.GetBoats(name) : null);
            //return new JsonResult(list);
            return json;
            
        }
    }
}