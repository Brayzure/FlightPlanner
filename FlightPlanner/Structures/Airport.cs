using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightPlanner.Structures
{
    class Airport
    {
        public string Code;

        public List<Flight> Arrivals = new List<Flight>();

        public List<Flight> Departures = new List<Flight>();

        public List<Flight> Flights
        {
            get
            {
                return Arrivals.Concat(Departures).ToList();
            }
        }

        public Airport(string code)
        {
            Code = code;
        }

        public void RegisterArrival(Flight flight)
        {
            Arrivals.Add(flight);
        }

        public void RegisterDeparture(Flight flight)
        {
            Departures.Add(flight);
        }
    }
}
