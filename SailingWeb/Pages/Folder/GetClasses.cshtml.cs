using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SailingWeb.Pages.Folder
{
    public class GetClassesModel : PageModel
    {
        public JsonResult OnGet(string name)
        {
            var json = new JsonResult(Sql.ReturnClass().Where(x => x.boatName.Equals(name)).FirstOrDefault());
            return json;
        }
    }
}