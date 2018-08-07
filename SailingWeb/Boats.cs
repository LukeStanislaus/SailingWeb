using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO.Compression;
using System.Linq;

namespace SailingWeb.Data
{
    
    public class BoatClass
    {
        public string boatName { get; set; }
        public int noOfCrew { get; set; }
        public int py { get; set; }
    }

    /// <summary>
    /// Boat data, contains data for an individual boat.
    /// </summary>
    /// TODO Migrate to this instead of Boats type.
    public class BoatsTidy
    {/*
        public static List<BoatsTidy> Tidyup(List<BoatsRacing> list)
        {
            List<BoatsTidy> listnew = new List<BoatsTidy>();
            var i=1;
            foreach (var boat in list.FindAll(x => x.Crew.Equals(0)))
            {
                BoatsTidy boat1 = new BoatsTidy();
                boat1.Name = boat.Name;
                boat1.Boat = boat.Boat;
                boat1.BoatNumber = boat.BoatNumber;
                boat1.Py = boat.Py;
                boat1.Notes = boat1.Notes;
                listnew.Add(boat1);
                i++;
            }

            foreach (var boat in list.FindAll(x => x.Crew.Equals(1)))
            {
                List<BoatsTidy> listtemp = listnew.FindAll(x => x.Boat.Equals(boat.Boat));
                var index = listnew.IndexOf(listtemp.Find(x => x.BoatNumber.Equals(boat.BoatNumber)));
                listnew[index].Crew = boat.Name;
                listnew[index].Notes += boat.Notes;
            }

            return listnew;
        }*/

        [Key]
        [StringLength(100)]
        public string Name { get; set; }
        public string Boat { get; set; }
        public string BoatNumber { get; set; }
        public string Crew { get; set; }
        public int Py { get; set; }
        public string Notes { get; set; }

        public BoatsTidy()
        {
        }

        public BoatsTidy(string name, string boat, string boatNumber, string crew, int py, string notes)
        {
            Name = name;
            BoatNumber = boatNumber;
            Boat = boat;
            Crew = crew;
            Py = py;
            Notes = notes;
        }
    }
    /*
    /// <summary>
    /// Boat data, contains data for an individual sailor including if they are a crew or not.
    /// </summary>
    /// TODO Migrate to this instead of Boats type.
    public class BoatsRacing
    {
        [Key]
        [StringLength(100)]
        public string Name { get; set; }
        public string Boat { get; set; }
        public string BoatNumber { get; set; }
        public int Crew { get; set; }
        public int Py { get; set; }
        public string Notes { get; set; }

        public BoatsRacing(string name, string boat, string boatNumber, int crew, int py, string notes)
        {
            Name = name;
            BoatNumber = boatNumber;
            Boat = boat;
            Crew = crew;
            Py = py;
            Notes = notes;
        }
    }
    */
    /// <summary>
    /// Boat data, contains data for an individual sailor.
    /// </summary>
    public class Boats
    {

        [Key]
        [StringLength(100)]
        public string Name { get; set; }
        public string BoatName { get; set; }
        public string BoatNumber { get; set; }
        public int Py { get; set; }


        public Boats(string name, string boat, string boatNumber, int py)
        {

            Name = name;
            BoatName = boat;
            BoatNumber = boatNumber;
            Py = py;

        }

        public Boats(string name)
        {
            Name = name;
        }
        public Boats()
        {
        }
    }

}
