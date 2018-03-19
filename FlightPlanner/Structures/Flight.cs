using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightPlanner.Structures
{
    class Flight
    {
        public Airport Origin;

        public Airport Destination;

        public DateTime Departure;

        public DateTime Arrival
        {
            get
            {
                return Departure + Duration;
            }
        }

        public TimeSpan Duration;

        public Flight(Airport origin, Airport destination, DateTime departure, TimeSpan duration)
        {
            Origin = origin;
            Destination = destination;
            Departure = departure;
            Duration = duration;
        }
    }
}
