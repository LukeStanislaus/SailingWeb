using System.ComponentModel.DataAnnotations;

namespace SailingWeb.Data
{

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
        public int BoatNumber { get; set; }
        public int Crew { get; set; }
        public BoatsRacing(string name, string boat, int boatNumber, int crew)
        {
            Name = name;
            BoatNumber = boatNumber;
            Boat = boat;
            Crew = crew;
        }
    }

    /// <summary>
    /// Boat data, contains data for an individual sailor.
    /// </summary>
    public class Boats
    {




        [Key]
        [StringLength(100)]
        public string Name { get; set; }
        public string BoatName { get; set; }
        public int BoatNumber { get; set; }


        public Boats(string name, string boat, int boatNumber)
        {

            Name = name;
            BoatName = boat;
            BoatNumber = boatNumber;

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
