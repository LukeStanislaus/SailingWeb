using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SailingWeb.Pages.Folder
{
    public class RemoveFulllistAllModel : PageModel
    {
        public void OnGet(string person)
        {
            Sql.RemoveFulllistAll(person);
        }
    }
}