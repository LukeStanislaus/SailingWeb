using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SailingWeb.Pages.Folder
{
    public class GetNamesModel : PageModel
    {
        public JsonResult OnGet(string name)
        {

                using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(Helper.CnnVal()))
                {
                    // Returns a list of all the distinct names in the fulllist db.
                    var listOfNames = connection.Query<string>("call returnnames").Distinct();
                    listOfNames = listOfNames.Where(x => x != name).ToArray();

                    return new JsonResult(listOfNames);
                }
            
        }
    }
}