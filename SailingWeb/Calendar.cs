using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SailingWeb
{
    /// <summary>
    /// Calendar data type, stores information about an event.
    /// </summary>
    public class Calendar
    {
        public string summary { get; set; }
        public string description { get; set; }
        public DateTime dateTime { get; set; }

        public Calendar(string Summary, string Description, DateTime Date)
        {
            summary = Summary;
            description = Description;
            dateTime = Date;

        }
        public Calendar()
        {

        }
    }
}
