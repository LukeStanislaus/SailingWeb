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
        [BindProperty]
        public Boats Boats1 { get; set; }
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
            if (Globals.name.name == null)
            Globals.name = Boats;
            await _db.SaveChangesAsync();

            if (RazorPagesContacts.Pages.CreateModel.countofboats() == 1)
            {
                Boats.boatName = RazorPagesContacts.Pages.CreateModel.selectbox(0);
                Program.Globals.name.boatName = RazorPagesContacts.Pages.CreateModel.selectbox(0);
            }
            if (Boats1.name != null)
            {
                Boats boat2 = new Boats(Boats1.name, Boats.boatName, Globals.name.boatNumber);
                SQL.SetBoats(boat2, 1);
                Globals.name = new Boats();
                Globals.namecrew = new Boats();
                Globals.askedCrew = 0;
                return RedirectToPage("/Index");
            }

            else if (Boats.name != null && Boats.boatName != null && Boats1.name == null && Program.Globals.askedCrew == 1)

            {
                Globals.name = new Boats();
                Globals.namecrew = new Boats();
                Globals.askedCrew = 0;
                return RedirectToPage("/Index");
            }
            else if (Boats.name != null && Boats.boatName != null)
            {
                Boats.boatNumber = SQL.GetBoats(Globals.name.name).Find(x => x.boatName.Equals(Boats.boatName)).boatNumber;
                Boats boat1 = new Boats(Globals.name.name, Boats.boatName, Boats.boatNumber);
                Globals.name.boatName = Boats.boatName;
                Globals.name.boatNumber = Boats.boatNumber;
                SQL.SetBoats(boat1);
                if (SQL.GetCrew(Program.Globals.name.boatName.ToUpper()) == 1)
                {
                    Globals.name = new Boats();
                    Globals.namecrew = new Boats();
                    Globals.askedCrew = 0;
                    return RedirectToPage("/Index");
                }
                else return Page();

            }

            //@Program.Globals.bla1 = $('#autocomplete').val();
            var value = Globals.bla1;
            return Page();
        }
    }
}