using SailingWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SailingWeb
{
    public class RaceHelpers
    {

        public static BoatsTidy ReturnBoatFromPos(int pos, Tuple<Calendar, Dictionary<BoatsTidy, List<BoatLap>>, DateTime> Race)
        {
            foreach (var boat in Race.Item2)
            {
                if (PlaceOf(boat.Key, Race) == pos)
                {
                    return boat.Key;
                }
            }
            return null;
        }

        public static int PlaceOf(BoatsTidy boat, Tuple<Calendar, Dictionary<BoatsTidy, List<BoatLap>>, DateTime> Race)
        {
            try
            {
                Dictionary<BoatsTidy, TimeSpan> places = new Dictionary<BoatsTidy, TimeSpan>();
                foreach (var x in Race.Item2)
                {
                    if (x.Value.Count != 0)
                    {
                        TimeSpan totaltime = new TimeSpan();
                        foreach (var y in x.Value)
                        {
                            totaltime += y.LapTime;
                        }
                        TimeSpan averageperlap = totaltime / x.Value.Count;
                        TimeSpan correctedTime = TimeSpan.FromSeconds((totaltime.TotalSeconds * NoOfLaps(Race) * 1000) / (x.Key.Py * x.Value.Count));

                        places.Add(x.Key, correctedTime);
                    }
                    else
                    {

                    }
                }
                var results = places.OrderBy(y => y.Value);
                var z = results.Where(x => x.Key.Boat == boat.Boat
                && x.Key.BoatNumber == boat.BoatNumber && x.Key.Crew == boat.Crew && x.Key.Name == boat.Name
                && x.Key.Notes == boat.Notes && x.Key.Py == boat.Py);

                return results.ToList().IndexOf(z.First()) + 1;
            }
            catch
            {
                return 0;
            }
        }

        public static int NoOfLaps(Tuple<Calendar, Dictionary<BoatsTidy, List<BoatLap>>, DateTime> Race)
        {
            try
            {
                List<int> list = new List<int>();
                foreach (var person in Race.Item2)
                {
                    list.Add(person.Value.Count);

                }
                if (list.Count != 0)
                {
                    return list.Max();
                }
                return 0;
            }
            catch(NullReferenceException)
            {
                return 0;
            }

        }

        public static List<JSONRace> ConvertToJSON(Tuple<Calendar, Dictionary<BoatsTidy, List<BoatLap>>, DateTime> Race)
        {
            List<JSONRace> Laps = new List<JSONRace>();
            foreach (var x in Race.Item2)
            {
                foreach (var item in x.Value)
                {
                    Laps.Add(new JSONRace(x.Key, Race.Item1, item, Race.Item3));
                }
            }
            return Laps;
        }

        public static TimeSpan CorrectedTime(BoatsTidy boat, Tuple<Calendar, Dictionary<BoatsTidy, List<BoatLap>>, DateTime> Race)
        {
            try
            {
                var x = Race.Item2[boat];

                TimeSpan totaltime = new TimeSpan();
                foreach (var y in x)
                {
                    totaltime += y.LapTime;
                }
                TimeSpan averageperlap = totaltime / x.Count;
                TimeSpan correctedTime = TimeSpan.FromSeconds((totaltime.TotalSeconds * NoOfLaps(Race) * 1000) / (boat.Py * x.Count));
                return correctedTime;
            }
            catch
            {
                return new TimeSpan();
            }

        }

        public static Tuple<Calendar, Dictionary<BoatsTidy, List<BoatLap>>, DateTime> JSONtoTuple(List<JSONRace> jSONRaces)
        {
            List<BoatsTidy> listofpeople = new List<BoatsTidy>();
            foreach (var item in jSONRaces)
            {
                listofpeople.Add(item.Boat);
            }

            List<BoatsTidy> distinctpeople = listofpeople.GroupBy(p => p.Name).Select(g => g.First()).ToList();
            Dictionary<BoatsTidy, List<BoatLap>> dictionary = new Dictionary<BoatsTidy, List<BoatLap>>();
            foreach (var person in distinctpeople)
            {
                var laps = new List<BoatLap>();

                foreach (var item in jSONRaces.FindAll(x => x.Boat.Name == person.Name))
                {
                    laps.Add(new BoatLap(item.LapNo.LapNumber, item.LapNo.LapTime));
                }
                dictionary.Add(person, laps);
            }
            Tuple<Calendar, Dictionary<BoatsTidy, List<BoatLap>>, DateTime> tup = new Tuple<Calendar,
                Dictionary<BoatsTidy, List<BoatLap>>, DateTime>(jSONRaces[0].RaceEvent, dictionary, jSONRaces[0].RaceStart);
            return tup;

        }
    }
}
