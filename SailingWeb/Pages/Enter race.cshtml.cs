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

        /// <summary>
        /// Takes the index of the boat from list of boats someone has sailed before and returns the boat name.
        /// </summary>
        /// <param name="i">Index of boat in the array.</param>
        /// <returns>Returns the boat from that specific array.</returns>
        public static string selectbox(int i)
        {

            if (Program.Globals.Boat.name != null)
            {
                return SQL.GetBoats(Program.Globals.Boat.name)[i].boatName;
            }
            else
                return "";
        }

        //TODO remove this, put inside selectbox.
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
            // Everytime we run this page, try to enter the person into the race. On success remove Globals.
            try
            {
                SQL.SetBoats(Boats);
                Program.Globals.alerttext = null;
                Program.Globals.removeboat = null;
            }
            // TODO Fix this catch.
            catch
            {
                Program.Globals.alerttext = "You have already been entered into the race, " +
                    "would you like to remove yourself?";
                Program.Globals.removeboat = Boats;

            }
        }
        public static bool SetBoats(Boats Boats)
        {
            try
            {
                SQL.SetBoats(Boats);
                return false;
            }

            catch
            {

                Program.Globals.alerttext = "You have already been entered into the race, " +
                    "would you like to remove yourself?";
                Program.Globals.removeboat = Boats;
                return true;

            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            // Set crew
            Program.Globals.Crew = Crew;

            //On second run, when we have the race name, this will run. First time will set to nothing. 
            //When set will not run again, may an issue?
            if (race != null)
            {

                Program.Globals.racename = race;

            }


            // Ensure these are empty before we start entering things. Should already be empty due to OnGetAsync.
            Program.Globals.removeboat = new Boats();
            Program.Globals.alerttext = "";


            // If we don't boat own name global hasn't been entered, we have an entry for local, we haven't got a boatName or
            // boatNumber yet then set boat global.
            if (Program.Globals.Boat.name == null && Boats.name != null && Boats.boatName == null &&
                Boats.boatNumber == 0)
            {

                Program.Globals.Boat.name = Boats.name;

            }


            // Else if we have boat own name global and boatname local entered but we haven't got boatNumber local 
            // entered, set global boatname.
            else if (Program.Globals.Boat.name != null && Boats.boatName != null && Boats.boatNumber == 0)
            {

                Program.Globals.Boat.boatName = Boats.boatName;

            }


            // Else if we have boatName global and boatNumber local entered, set boatNumber global.
            else if (Program.Globals.Boat.boatName != null && Boats.boatNumber != 0)
            {

                Program.Globals.Boat.boatNumber = Boats.boatNumber;

            }


            //If they only have one boat, skip asking for which boat they are sailing.
            if (CreateModel.countofboats() == 1)
            {

                Boats.boatName = CreateModel.selectbox(0);
                Program.Globals.Boat.boatName = CreateModel.selectbox(0);

            }


            // If we have the crew entered, set boats.
            if (Crew != null)
            {
                // Try to set boats.
                if (false == SetBoats(Boats))
                    return RedirectToPage("/Index");
                else
                    return Page();


            }


            // Else if we have entered the boat data but have asked for their crew name, assume no crew and set boat.
            else if (Boats.name != null && Boats.boatName != null && Crew == null && Program.Globals.askedCrew == 1)
            {

                exit(Globals.Boat);
                SQL.SetBoats(Boats);
                return RedirectToPage("/Index");

            }


            // Else if we have name and boat name,  calculate their boat number.
            else if (Boats.name != null && Boats.boatName != null)
            {

                //Calculate boat number
                Boats.boatNumber = SQL.GetBoats(Globals.Boat.name).Find(x => 
                x.boatName.Equals(Boats.boatName)).boatNumber;
                Globals.Boat.boatNumber = Boats.boatNumber;

                // Try to set boats.
                if (false == SetBoats(Boats))
                    return RedirectToPage("/Index");
                else
                    return Page();

            }
            // If all fails, refresh.
            return Page();
        }
    }
}