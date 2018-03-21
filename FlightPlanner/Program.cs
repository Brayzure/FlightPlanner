using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightPlanner.Structures;
using FlightPlanner.Core;

namespace FlightPlanner
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileLoc = @"../../Data/Flights.txt";
            string path = System.IO.Path.GetFullPath(fileLoc);

            FileHandler handler = new FileHandler(path);

            List<string> flightStrings = handler.GetLines();

            string start = "DTW";
            string end = "TPA";

            Directory directory = new Directory();
            directory.PopulateFlights(flightStrings);

            System.Console.WriteLine("Finished reading flights, found {0}", flightStrings.Count);

            // Output all airport info
            foreach(KeyValuePair<string, Airport> pair in directory.Airports)
            {
                System.Console.WriteLine("Airport {0} has {1} flights ({2} arrivals and {3} departures).",
                    pair.Value.Code, pair.Value.Flights.Count, pair.Value.Arrivals.Count, pair.Value.Departures.Count);
            }

            // Path from start to end
            List<List<Flight>> trips = directory.Navigate(start, end);
            List<Trip> final = new List<Trip>();
            trips.ForEach((t) =>
            {
                final.Add(new Trip(t));
            });
            System.Console.WriteLine("Found {0} valid trips!", final.Where(t => t.Flights.Count <= 2).Count());
            final.ForEach((t) =>
            {
                if(t.Flights.Count <= 2)
                {
                    System.Console.WriteLine(t.Summary());
                }
            });

            System.Console.ReadLine();
        }
    }
}
