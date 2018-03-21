using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightPlanner.Structures
{
    class Directory
    {
        public Dictionary<string, Airport> Airports = new Dictionary<string, Airport>();

        public double MinimumLayover;

        public Directory(double minimumLayover = 45)
        {
            MinimumLayover = minimumLayover;
        }

        public void PopulateFlights(List<string> flightStrings)
        {
            foreach(string fs in flightStrings)
            {
                ConvertStringToFlight(fs);
            }
        }

        public void ConvertStringToFlight(string flightString)
        {
            Airport airport;
            List<string> args = flightString.Split(' ').ToList();
            DateTime departure = DateTime.ParseExact(args[2], "H:mm", null);
            TimeSpan duration = TimeSpan.FromMinutes(Double.Parse(args[3]));
            Airport origin;
            Airport destination;
            if(Airports.TryGetValue(args[0], out airport))
            {
                origin = airport;
            }
            else
            {
                airport = new Airport(args[0]);
                origin = airport;
                Airports.Add(airport.Code, airport);
            }
            if (Airports.TryGetValue(args[1], out airport))
            {
                destination = airport;
            }
            else
            {
                airport = new Airport(args[1]);
                destination = airport;
                Airports.Add(airport.Code, airport);
            }
            Flight flight = new Flight(origin, destination, departure, duration);

            RegisterFlight(flight);
        }

        public void RegisterFlight(Flight flight)
        {
            System.Console.WriteLine("Parsed flight from {0} to {1}.", flight.Origin.Code, flight.Destination.Code);
            flight.Origin.RegisterDeparture(flight);
            flight.Destination.RegisterArrival(flight);
        }

        public List<List<Flight>> Navigate(string start, string end, List<Flight> previous = null)
        {
            List<List<Flight>> trips = new List<List<Flight>>();
            List<List<Flight>> outer = new List<List<Flight>>();
            Airport startingAirport;
            if(Airports.TryGetValue(start, out startingAirport))
            {
                List<Flight> outbounds = startingAirport.Departures;
                
                // Filter out flights that go to a visited airport, or leave before last flight arrives
                if(previous != null)
                {
                    List<Airport> visited = new List<Airport>();
                    // Visited already
                    previous.ForEach((f) =>
                    {
                        visited.Add(f.Origin);
                    });

                    // Leaves before last flight arrives
                    // TODO: Add layover minimum (45min?)
                    outbounds = outbounds.Where((flight) =>
                    {
                        return flight.Departure > previous.Last().Departure + previous.Last().Duration + TimeSpan.FromMinutes(MinimumLayover)
                        && !visited.Contains(flight.Destination);
                    }).ToList();
                }

                // Check each valid outbound flight for trips to the destination
                outbounds.ForEach((f) =>
                {
                    // Flight ends at target destination
                    if (f.Destination.Code == end)
                    {
                        List<Flight> inbound = new List<Flight>();
                        inbound.Add(f);
                        outer.Add(inbound);
                    }
                    // Keep looking!
                    else
                    {
                        List<Flight> temp = new List<Flight>();
                        temp.Add(f);
                        if(previous == null)
                        {
                            previous = new List<Flight>();
                        }
                        List<List<Flight>> inbound = Navigate(f.Destination.Code, end, previous.Concat(temp).ToList());
                        inbound.ForEach((trip) =>
                        {
                            outer.Add(temp.Concat(trip).ToList());
                        });
                    }
                });

                return outer;
            }
            else
            {
                return trips;
            }
        }
    }
}
