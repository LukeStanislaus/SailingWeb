using SailingWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SailingWeb
{
    public class JSONRace
    {
        public BoatsTidy Boat { get; set; }
        public Calendar RaceEvent { get; set; }
        public BoatLap LapNo { get; set; }
        public DateTime RaceStart { get; set; }

        public JSONRace(BoatsTidy boat, Calendar raceEvent, BoatLap lapNo, DateTime raceStart)
        {
            this.Boat = boat;
            this.RaceEvent = raceEvent;
            this.LapNo = lapNo;
            this.RaceStart = raceStart;
        }
    }
}
