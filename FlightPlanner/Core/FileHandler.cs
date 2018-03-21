using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightPlanner.Core
{
    class FileHandler
    {
        private string fileLocation;

        public FileHandler(string fileLoc)
        {
            fileLocation = fileLoc;
        }

        public List<string> GetLines()
        {
            List<string> lines = new List<string>();
            using (System.IO.StreamReader file = new System.IO.StreamReader(fileLocation))
            {
                string line;

                // Read each flight
                while ((line = file.ReadLine()) != null)
                {
                    System.Console.WriteLine(line);
                    lines.Add(line);
                }
            }

            return lines;
        }
    }
}
