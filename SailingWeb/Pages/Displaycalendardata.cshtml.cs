using System;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SailingWeb.Pages
{
    public class DisplaycalendardataModel : PageModel
    {

        public void OnGetAsync()
        {
            Program.Globals.Event1 = Program.GetCalendar().Result;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var cal = new Calendar();
            foreach (var t in Program.Globals.Event1.Items)
            {
                try
                {

                    if (t.Summary != null)
                    {
                        if (Convert.ToDateTime(t.Start.Date).Year >= 2018)
                        {

                            cal.Summary = t.Summary;
                            cal.Description = t.Description;

                            if (t.Start.Date != null)
                            {
                                cal.DateTime = Convert.ToDateTime(t.Start.Date);
                            }

                            else if (t.Start.DateTime.HasValue)
                            {
                                cal.DateTime = t.Start.DateTime.Value;
                            }

                        }
                        else
                        {
                            throw new Exception();


                        }

                    }


                }
                catch
                {

                    if (t.Summary != null)
                    {
                        try
                        {
                            // ReSharper disable once PossibleInvalidOperationException trycatch
                            if (t.Start.DateTime.Value.Year >= 2018)
                            {

                                cal.Summary = t.Summary;
                                cal.Description = t.Description;
                                cal.DateTime = t.Start.Date != null ? 
                                    Convert.ToDateTime(t.Start.Date) : 
                                    t.Start.DateTime.Value; 

                            }
                        }
                        catch (Exception ex)
                        {
                            Program.Globals.Alerttext = "There was a problem with pulling the data, " + ex;
                            return RedirectToPage("/Index");
                        }
                    }


                }

                try
                {
                    Sql.Newcalendar(cal);

                }
                catch(Exception ex)
                {
                    Program.Globals.Alerttext = "There was a problem with pulling the data, " + ex;
                    return RedirectToPage("/Index");
                }
            }
            Program.Globals.Alerttext = "The data has been successfully pulled into the database.";
            return RedirectToPage("/Index");
            //Program.Globals.alerttext = "Completed insert.";
        }
    }
}