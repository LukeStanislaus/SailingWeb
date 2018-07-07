using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;

namespace SailingWeb
{
    /// <summary>
    /// Calendar data type, stores information about an event.
    /// </summary>
    public class Calendar
    {
        //TODO Change from timestamp to datetime :(
        public DateTime DateTime{ get; set; }      
        public string Summary { get; set; }
        public string Description { get; set; }


        public Calendar(string summary, string description, DateTime date)
        {
            Summary = summary;
            Description = description;
            DateTime = date;

        }
        public Calendar()
        {

        }
    }
}
