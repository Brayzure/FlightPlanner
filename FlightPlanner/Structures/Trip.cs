using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightPlanner.Structures
{
    class Trip
    {
        public List<Flight> Flights = new List<Flight>();

        public Trip(List<Flight> flights = null)
        {
            if(flights != null)
            {
                Flights = flights;
            }
        }

        public void RegisterFlight(Flight f)
        {
            // TODO: Check if flight falls within existing flights
            Flights.Add(f);
        }

        public string Summary()
        {
            string summary = "------";

            TimeSpan duration = (Flights.Last().Departure - Flights.First().Departure) + Flights.Last().Duration;
            summary += String.Format("\nDuration: {0}h{1}m", duration.Hours, duration.Minutes);

            int i = 0;
            while(i<Flights.Count)
            {
                summary += String.Format("\n{0} ->> {1}", Flights[i].Origin.Code, Flights[i].Destination.Code);

                if(Flights.Count - i != 1)
                {
                    TimeSpan layover = Flights[i + 1].Departure - Flights[i].Arrival;
                    if(layover.Hours > 0)
                    {
                        summary += String.Format("\nLayover: {0}h{1}m", layover.Hours, layover.Minutes);
                    }
                    else
                    {
                        summary += String.Format("\nLayover {0}m", layover.Minutes);
                    }
                }

                i++;
            }

            return summary;
        }
    }
}
