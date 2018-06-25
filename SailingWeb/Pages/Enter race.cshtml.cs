﻿using System;
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
        [BindProperty]
        public string race { get; set; }
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
        public async void OnGetAsyc()
        {
            Boats boat2 = new Boats(Boats1.name, Globals.name.boatName, Globals.name.boatNumber);
            try
            {
                SQL.SetBoats(boat2, 1, race);
                Program.Globals.alerttext = null;
                Program.Globals.removeboat = null;
            }
            catch
            {
                Program.Globals.alerttext = "You have already been entered into the race";
                Program.Globals.removeboat = boat2;
                
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            Program.Globals.removeboat = new Boats();
            Program.Globals.alerttext = "";
            //ScriptManager.RegisterStartupScript
            //if (!ModelState.IsValid)
            {
            //    return Page();
            }

            _db.Boatss.Add(Boats);
            race = Program.Globals.racename;
            //Globals.name = _db.Boatss.Find(Boats.name);
            if (Globals.name.name == null)
            Globals.name = Boats;
            Program.Globals.Boats1 = Boats1;

            if (CreateModel.countofboats() == 1)
            {
                Boats.boatName = CreateModel.selectbox(0);
                Program.Globals.name.boatName = CreateModel.selectbox(0);
            }
            if (Boats1.name != null)
            {
                Boats boat2 = new Boats(Boats1.name, Boats.boatName, Globals.name.boatNumber);
                try
                {
                    SQL.SetBoats(boat2, 1, race);
                }
                catch
                {
                    Program.Globals.alerttext = "You have already been entered into the race";
                    Program.Globals.removeboat = boat2;
                    return Page();
                }
                exit(Globals.name, Boats1);
                _db.Dispose();


                return RedirectToPage("/Index");
            }

            else if (Boats.name != null && Boats.boatName != null && Boats1.name == null && Program.Globals.askedCrew == 1)

            {
                exit(Globals.name);
                _db.Dispose();

                return RedirectToPage("/Index");
            }
            else if (Boats.name != null && Boats.boatName != null)
            {
                Boats.boatNumber = SQL.GetBoats(Globals.name.name).Find(x => x.boatName.Equals(Boats.boatName)).boatNumber;
                Boats boat1 = new Boats(Globals.name.name, Boats.boatName, Boats.boatNumber);
                Globals.name.boatName = Boats.boatName;
                Globals.name.boatNumber = Boats.boatNumber;
                try
                {
                    SQL.SetBoats(boat1, race);
                }
                
                catch
                {
                    Program.Globals.alerttext = "You have already been entered into the race";
                    Program.Globals.removeboat = boat1;
                    return Page();
                }
                
                if (SQL.GetCrew(Program.Globals.name.boatName.ToUpper()) == 1)
                {
                    exit(Boats);
                    _db.Dispose();

                    return RedirectToPage("/Index");
                }
                else return Page();

            }

            //@Program.Globals.bla1 = $('#autocomplete').val();
            var value = Globals.bla1;
            await _db.SaveChangesAsync();
            return Page();
        }
    }
}