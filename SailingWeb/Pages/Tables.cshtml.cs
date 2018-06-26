using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SailingWeb.Pages
{
    public class ContactModel : PageModel
    {
        public string Message { get; set; }
        [BindProperty]
        public string race { get; set; }
        public void OnGet()
        {
            Program.Globals.Event1 = Program.GetCalendar().Result;
            Message = "Your contact page.";
        }
        public async Task<IActionResult> OnPostAsync()
        {
            Program.Globals.racenametable = race;
            Calendar cal = new Calendar();
            if (1 == 0)
            {
                for (int i = 0; i < Program.Globals.Event1.Items.Count; i++)
                {
                    try
                    {

                        if (Program.Globals.Event1.Items[i].Summary != null)
                        {
                            if (Convert.ToDateTime(Program.Globals.Event1.Items[i].Start.Date).Year >= 2018)
                            {

                                cal.summary = Program.Globals.Event1.Items[i].Summary;
                                cal.description = Program.Globals.Event1.Items[i].Description;

                                if (Program.Globals.Event1.Items[i].Start.Date != null)
                                {
                                    cal.dateTime = Convert.ToDateTime(Program.Globals.Event1.Items[i].Start.Date);
                                }

                                else if (Program.Globals.Event1.Items[i].Start.DateTime.HasValue)
                                {
                                    cal.dateTime = Program.Globals.Event1.Items[i].Start.DateTime.Value.Date;
                                }
                                else
                                {
                                }
                            }
                            else
                            {
                                throw new InsufficientExecutionStackException();
                            }
                        }
                        else
                        {

                        }

                    }
                    catch
                    {

                        if (Program.Globals.Event1.Items[i].Summary != null)
                        {
                            if (Program.Globals.Event1.Items[i].Start.DateTime.Value.Year >= 2018)
                            {




                                cal.summary = Program.Globals.Event1.Items[i].Summary;
                                cal.description = Program.Globals.Event1.Items[i].Description;
                                if (Program.Globals.Event1.Items[i].Start.Date != null)
                                {
                                    cal.dateTime = Convert.ToDateTime(Program.Globals.Event1.Items[i].Start.Date);
                                }

                                else if (Program.Globals.Event1.Items[i].Start.DateTime.HasValue)
                                {
                                    cal.dateTime = Program.Globals.Event1.Items[i].Start.DateTime.Value.Date;
                                }



                            }
                        }
                        else
                        {

                        }

                    }
                    try
                    {
                        SQL.newcalendar(cal);
                    }
                    catch { }
                }
            }
            //Program.Globals.alerttext = "Completed insert.";
            return Page();
        }

    }
}
