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


        protected void Autocomplete(object sender, EventArgs e)
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
        public string Race { get; set; }
        [BindProperty]
        public string Response { get; set; }

        /// <summary>
        /// Takes the index of the boat from list of boats someone has sailed before and returns the boat name.
        /// </summary>
        /// <param name="i">Index of boat in the array.</param>
        /// <returns>Returns the boat from that specific array.</returns>
        public static string Selectbox(int i)
        {
            return Globals.Boat.Name != null ? Sql.GetBoats(Globals.Boat.Name)[i].BoatName : "";
        }

        //TODO remove this, put inside selectbox.
        public static int Countofboats()
        {
            return Globals.Boat.Name != null ? Sql.GetBoats(Globals.Boat.Name).Count : 0;
        }


        public async void OnGetAsyc()
        {
            //if (Program.Globals.Response == true)
            //{
             //   return RedirectToPage("/Index");
            //}
            /*
            // Everytime we run this page, try to enter the person into the race. On success remove Globals.
            try
            {
                Sql.SetBoats(Boats);
                Globals.Alerttext = null;
                Globals.Removeboat = null;
            }
            // TODO Fix this catch.
            catch
            {
                if (Boats.Name != "" && Boats.BoatName != "" && Boats.BoatNumber !=0)
                Globals.Alerttext = "You have already been entered into the race, " +
                    "would you like to remove yourself?";
                Globals.Removeboat = Boats;

            }
            */

            //return RedirectToAction("OnPost");
        }
        public static bool SetBoats(Boats boat)
        {
            
            try
            {
                Sql.SetBoats(boat);
                return false;
            }

            catch
            {

                Globals.Alerttext = "You have already been entered into the race, " +
                    "would you like to remove yourself?";
                Globals.Removeboat = Program.Globals.Boat;
                return true;

            }
            
        }

        public async Task<IActionResult> OnPost()
        {
            // Set crew
            // Initially no crew would be set to Crew would be Null

            Globals.Crew = Crew;

            //On second run, when we have the race name, this will run. First time will set to nothing. 
            //When set will not run again, may an issue?
            if (Race != null)
            {

                Globals.Racename = Race;

            }


            // Ensure these are empty before we start entering things. Should already be empty due to OnGetAsync.
            Globals.Removeboat = new Boats();
            Globals.Alerttext = "";


            // If we don't boat own name global hasn't been entered, we have an entry for local, we haven't got a boatName or
            // boatNumber yet then set boat global.
            if (Globals.Boat.Name == null && Boats.Name != null && Boats.BoatName == null &&
                Boats.BoatNumber == 0)
            {

                Globals.Boat.Name = Boats.Name;

            }


            // Else if we have boat own name global and boatname local entered but we haven't got boatNumber local 
            // entered, set global boatname.
            else if (Globals.Boat.Name != null && Boats.BoatName != null && Boats.BoatNumber == 0)
            {

                Globals.Boat.BoatName = Boats.BoatName;

            }


            // Else if we have boatName global and boatNumber local entered, set boatNumber global.
            else if (Globals.Boat.BoatName != null && Boats.BoatNumber != 0)
            {

                Globals.Boat.BoatNumber = Boats.BoatNumber;

            }

            // Else if we have name and boat name,  calculate their boat number.

            if (Program.Globals.Boat.Name != null && Program.Globals.Boat.BoatName != null)
            {
                //Calculate boat number
                Boats.BoatNumber = Sql.GetBoats(Globals.Boat.Name).Find(x =>
                    x.BoatName.Equals(Program.Globals.Boat.BoatName)).BoatNumber;
                Globals.Boat.BoatNumber = Boats.BoatNumber;
            }


            //If they only have one boat, skip asking for which boat they are sailing.
            if (Countofboats() == 1)
            {

                Boats.BoatName = Selectbox(0);
                Globals.Boat.BoatName = Selectbox(0);
                Globals.Boat.BoatNumber = Sql.GetBoats(Globals.Boat.Name).Find(x =>
                    x.BoatName.Equals(Globals.Boat.BoatName)).BoatNumber;
                if (Sql.GetCrew(Globals.Boat.BoatName)==1)
                {
                    SetBoats(Program.Globals.Boat);
                    Exit(Program.Globals.Boat);
                    return RedirectToPage("/Index");
                    
                }

            }


            // If we have the crew entered, set boats.
            if (Crew != null)
            {
                // Try to set boats.
                if (false == SetBoats(Program.Globals.Boat))
                {
                    Exit(Globals.Boat);
                    return RedirectToPage("/Index");
                }

                return Page();


            }



            // Else if we have entered the boat data but have asked for their crew name, assume no crew and set boat.

            if (Globals.Boat.Name != null && Globals.Boat.BoatName != null && 
                Globals.Crew == null && Globals.AskedCrew == 1)
            {

                Exit(Globals.Boat);
                Sql.SetBoats(Globals.Boat);
                return RedirectToPage("/Index");

            }




        // Try to set boats.
            /*
        if (false == SetBoats(Boats))
            {
                Exit(Globals.Boat);
                return RedirectToPage("/Index");
            }*/

            return Page();
            // If all fails, refresh.
        }
    }
}