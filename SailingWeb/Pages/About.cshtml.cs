using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesContacts.Data;
using SailingWeb;
using SailingWeb.Data;
using static SailingWeb.Program;

namespace RazorPagesContacts.Pages
{

    public class CreateModel : PageModel
    {

        protected void autocomplete(object sender, EventArgs e)
        {

            //string name = Request.Form["Name"];
            //string email = autocomplete.Value;
        }

        public readonly AppDbContext _db;

        public CreateModel(AppDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Boats Boats { get; set; }
        public static string selectbox(int i)
        {

            if (Program.Globals.name.name != null)
            {
                return SQL.GetBoats(Program.Globals.name.name)[i].boatName;
            }
            else
                return "";
        }
        public static int countofboats()
        {
            if (Program.Globals.name.name != null)
            {
                return SQL.GetBoats(Program.Globals.name.name).Count;

            }
            else return 0;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            //ScriptManager.RegisterStartupScript
            //if (!ModelState.IsValid)
            {
            //    return Page();
            }
            
            _db.Boatss.Add(Boats);
            //Globals.name = _db.Boatss.Find(Boats.name);
            Globals.name = Boats;
            await _db.SaveChangesAsync();
            SQL.SetBoats(Boats);
            //@Program.Globals.bla1 = $('#autocomplete').val();
            var value = Globals.bla1;
            return RedirectToPage("/About");
        }
    }
}