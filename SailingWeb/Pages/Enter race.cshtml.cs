using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailingWeb;
using SailingWeb.Data;
using System;
using System.Threading.Tasks;

namespace RazorPagesContacts.Pages
{

    public class CreateModel : PageModel
    {


        protected void Autocomplete(object sender, EventArgs e)
        {

            //string name = Request.Form["Name"];
            //string email = autocomplete.Value;
        }

        [BindProperty]
        public string Boatandnumber { get; set; }
        public static int NewBoat { get; set; }
        [BindProperty]
        public BoatsTidy Boats { get; set; }
        [BindProperty]
        public string Crew { get; set; }
        [BindProperty]
        public string Race { get; set; }
        [BindProperty]
        public string Notes { get; set; }
        [BindProperty]
        public Boats Response1 { get; set; }



        public async Task<int> SetBoats(BoatsTidy boats)
        {
            string[] str = Race.Split("abc123");
            Calendar cal = new Calendar(str[0], null,
    Convert.ToDateTime(str[1]));
            try
            {
                return await Sql.SetBoats(boats, cal);
            }
            catch (Exception)
            {
                throw;
            }
            

        }


        public async void OnGetAsyc()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                string[] str = Race.Split("abc123");
                Calendar cal = new Calendar(str[0], null,
        Convert.ToDateTime(str[1]));
                ViewData["Calendar"] = cal;
            }
            catch
            {

            }

            // If they have not selected a boat, split the "boatandnumber" string into constituent parts
            if (Boatandnumber != "test")
            {
                string[] str = Boatandnumber.Split(", ");
                Boats.Boat = str[0];
                Boats.BoatNumber = str[1];
            }
            else
            {
                Boats.Boat = Boatandnumber;
            }


            //If they have entered their boat info, figure out boat py and add.
            if (Boats.Name != null && Boats.Boat != "test")
            {

                Boats.Py = Sql.GetBoats(Boats.Name).Find(x => x.BoatName.Equals(Boats.Boat) && x.BoatNumber.Equals(Boats.BoatNumber)).Py;
                //if we have a crew, add the crew
                if (Crew != null)
                {
                    try
                    {
                        SetBoats(new BoatsTidy(Boats.Name, Boats.Boat, Boats.BoatNumber, Crew, Boats.Py, Notes));
                        ViewData["Alreadyin"] = true;
                    }
                    catch
                    {
                        ViewData["Alreadyin"] = false;
                    }
                    ViewData["SetCrew"] = new BoatsTidy(Boats.Name, Boats.Boat, Boats.BoatNumber, Crew, Boats.Py, Notes);
                    return Page();
                }
                // Else just add the boat to the race
                try
                {
                    await SetBoats(new BoatsTidy(Boats.Name, Boats.Boat, Boats.BoatNumber, Crew, Boats.Py, Notes));
                    ViewData["Alreadyin"] = true;
                }
                catch
                {
                    ViewData["Alreadyin"] = false;
                }

                ViewData["SetBoat"] = Boats;
                return Page();
            }
            //Else if we need to add a new boat, add that boat then enter the person to the race.
            else if (Boats.Boat == "test" && Boats.Name != null && Response1.BoatName != null &&
                Response1.BoatNumber != null && Response1.Py != 0)
            {
                Response1.Name = Boats.Name;
                // Ensure we have finished adding the boat properly before we add the boat to the race.
                await Sql.SetNewFullBoat(Response1);
                //ViewData["Alreadyin"] = SetBoats(new BoatsTidy(Response1.Name, Response1.BoatName, Response1.BoatNumber, Crew, Response1.Py, ""));
                ViewData["SetAndEntered"] = new BoatsTidy(Response1.Name, Response1.BoatName, Response1.BoatNumber, Crew, Response1.Py, "");
                return Page();
            }
            return Page();
        }

    }
}