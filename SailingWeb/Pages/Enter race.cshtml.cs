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
        public string Crew { get; set; }
        [BindProperty]
        public string race { get; set; }
        [BindProperty]
        public string response { get; set; }
        public static string selectbox(int i)
        {

            if (Program.Globals.Boat.name != null)
            {
                return SQL.GetBoats(Program.Globals.Boat.name)[i].boatName;
            }
            else
                return "";
        }
        public static int countofboats()
        {
            if (Program.Globals.Boat.name != null)
            {
                return SQL.GetBoats(Program.Globals.Boat.name).Count;

            }
            else return 0;
        }
        public async void OnGetAsyc()
        {
            Boats boat2 = new Boats(Crew, Globals.Boat.boatName, Globals.Boat.boatNumber);
            try
            {
                SQL.SetBoats(boat2);
                Program.Globals.alerttext = null;
                Program.Globals.removeboat = null;
            }
            catch
            {
                Program.Globals.alerttext = "You have already been entered into the race, " +
                    "would you like to remove yourself?";
                Program.Globals.removeboat = boat2;
                
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if(race!=null)
            Program.Globals.racename = race;
            Program.Globals.removeboat = new Boats();
            Program.Globals.alerttext = "";
            //ScriptManager.RegisterStartupScript
            //if (!ModelState.IsValid)
            {
            //    return Page();
            }
            if (Program.Globals.Boat.name == null && Boats.name != null && Boats.boatName == null && Boats.boatNumber == 0)
                Program.Globals.Boat.name = Boats.name;
            else if (Program.Globals.Boat.name != null && Boats.boatName != null && Boats.boatNumber == 0)
                Program.Globals.Boat.boatName = Boats.boatName;
            else if (Program.Globals.Boat.boatName != null && Boats.boatNumber != 0)
                Program.Globals.Boat.boatNumber = Boats.boatNumber;
            _db.Boatss.Add(Boats);
            //Program.Globals.racename = race;
            //Globals.Boat.= _db.Boatss.Find(Boats.name);

            Program.Globals.Crew = Crew;

            if (CreateModel.countofboats() == 1)
            {
                Boats.boatName = CreateModel.selectbox(0);
                Program.Globals.Boat.boatName = CreateModel.selectbox(0);
            }
            if (Crew != null)
            {
                Boats boat2 = new Boats(Crew, Boats.boatName, Globals.Boat.boatNumber);
                try
                {
                    SQL.SetBoats(boat2);
                }
                catch
                {
                    Program.Globals.alerttext = "You have already been entered into the race, " +
                        "would you like to remove yourself?";
                    Program.Globals.removeboat = boat2;
                    return Page();
                }
                exit(Globals.Boat);
                _db.Dispose();


                return RedirectToPage("/Index");
            }

            else if (Boats.name != null && Boats.boatName != null && Crew == null && Program.Globals.askedCrew == 1)

            {
                exit(Globals.Boat);
                _db.Dispose();

                return RedirectToPage("/Index");
            }
            else if (Boats.name != null && Boats.boatName != null)
            {
                Boats.boatNumber = SQL.GetBoats(Globals.Boat.name).Find(x => x.boatName.Equals(Boats.boatName)).boatNumber;
                Boats boat1 = new Boats(Globals.Boat.name, Boats.boatName, Boats.boatNumber);
                Globals.Boat.boatName = Boats.boatName;
                Globals.Boat.boatNumber = Boats.boatNumber;
                try
                {
                    SQL.SetBoats(boat1);
                }
                
                catch
                {
                    Program.Globals.alerttext = "You have already been entered into the race, " +
                        "would you like to remove yourself?";
                    Program.Globals.removeboat = boat1;
                    return Page();
                }
                
                if (SQL.GetCrew(Program.Globals.Boat.boatName.ToUpper()) == 1)
                {
                    exit(Boats);
                    _db.Dispose();

                    return RedirectToPage("/Index");
                }
                else return Page();

            }

            await _db.SaveChangesAsync();
            return Page();
        }
    }
}