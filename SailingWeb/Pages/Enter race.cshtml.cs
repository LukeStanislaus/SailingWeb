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

        [BindProperty]
        public string Boatandnumber { get; set; }
        public static int NewBoat { get; set; }
        [BindProperty]
        public BoatsTidy Boats { get; set; }
        [BindProperty]
        public string Crew { get; set; }
        [BindProperty]
        public String Race { get; set; }
        [BindProperty]
        public string Notes { get; set; }
        [BindProperty]
        public Boats Response1 { get; set; }



        public async void SetBoats(BoatsTidy boats)
        {
            string[] str = Race.Split("abc123");
            Calendar cal = new Calendar(str[0], null,
    Convert.ToDateTime(str[1]));
            try
            {

                
                var result = await Sql.SetBoats(boats, cal);
                Program.Exit(boats, cal);
                Program.Globals.Racename = new Calendar(Race, "", new DateTime());

            }
            catch
            {
                Program.Globals.Removeboat = boats;
                Program.Globals.Alerttext = "You are already added to the race, would you like to remove yourself?";
                Program.Globals.Racename = cal;
                
            }
        }

        public async void OnGetAsyc()
        {

        }

        public async Task<IActionResult> OnPost()
        {

                // If they have not selected a boat, split the "boatandnumber" string into constituent parts
                if (Boatandnumber != "test")
                {
                    var str = Boatandnumber.Split(", ");
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
                    SetBoats(new BoatsTidy(Boats.Name, Boats.Boat, Boats.BoatNumber, Crew, Boats.Py, Notes));
                    return RedirectToPage("/Index");
                }
                // Else just add the boat to the race
                SetBoats(Boats);
                return RedirectToPage("/Index");
            }
            //Else if we need to add a new boat, add that boat then enter the person to the race.
            else if (Boats.Boat == "test" && Boats.Name != null && Response1.BoatName != null &&
                Response1.BoatNumber != null && Response1.Py != 0)
            {
                Response1.Name = Boats.Name;
                // Ensure we have finished adding the boat properly before we add the boat to the race.
                await Sql.SetNewFullBoat(Response1);
                SetBoats(new BoatsTidy(Response1.Name, Response1.BoatName, Response1.BoatNumber, Crew, Response1.Py, ""));
                return RedirectToPage("/Index");
            }
            return Page();
        }
        
    }
}