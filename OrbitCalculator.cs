namespace ProximaCentauriB
{
    internal class OrbitCalculator
    {

        /// <summary>
        /// The main entry point of the application.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        static void Main(string[] args)
        {
            string filename = "data.txt";
            try
            {
                // Read orbit data from file and calculate total orbits
                Dictionary<string, string> orbits = GetDataMapFromFile(filename);
                int totalOrbits = CalculateTotalOrbits(orbits);

                Console.WriteLine($"what is the total number of direct and indirect orbits in your map data?: {totalOrbits}");
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"ERROR: {e.Message}");
            }
            catch (InvalidDataException e)
            {
                Console.WriteLine($"ERROR: Invalid data in file '{filename}': {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR: An unexpected error occurred: {e.Message}");
            }

        }

        /// <summary>
        /// Reads orbit data from a file and creates a dictionary mapping orbiters to their centers.
        /// </summary>
        /// <param name="filename">The name of the file containing orbit data.</param>
        /// <returns>A dictionary mapping each orbiter to its center.</returns>
        /// <exception cref="FileNotFoundException">Thrown if the file is not found.</exception>
        /// <exception cref="InvalidDataException">Thrown if the file contains invalid data.</exception>
        static Dictionary<string, string> GetDataMapFromFile(string filename)
        {
            var orbits = new Dictionary<string, string>();

            // Ensure the file path is relative to the current directory
            string currentDirectory = Directory.GetCurrentDirectory();
            string filepath = Path.Combine(currentDirectory, filename);

            // Check if file exists
            if (!File.Exists(filepath))
            {
                throw new FileNotFoundException($"File '{filepath}' could not be found.");
            }

            string[] lines = File.ReadAllLines(filepath);

            foreach (string line in lines)
            {
                // Validate the format of the line
                if (string.IsNullOrWhiteSpace(line) || !line.Contains(')'))
                {
                    throw new InvalidDataException($"Invalid line format: '{line}'");
                }

                // Split the line into center and orbiter
                var parts = line.Split(")");
                if (parts.Length != 2)
                {
                    throw new InvalidDataException($"Invalid line format: '{line}'");
                }

                string center = parts[0];
                string orbiter = parts[1];

                // Check for duplicate orbiters
                if (orbits.TryGetValue(orbiter, out string? existingCenter))
                {
                    throw new InvalidDataException($"Duplicate orbiter detected: '{orbiter}' already orbits '{existingCenter}', cannot orbit '{center}'");
                }

                // Add the orbiter and its center to the dictionary
                orbits.Add(orbiter, center);
            }
            return orbits;
        }

        /// <summary>
        /// Counts the total number of direct and indirect orbits for a given orbiter.
        /// </summary>
        /// <param name="orbits">A dictionary mapping each orbiter to its center.</param>
        /// <param name="orbiter">The orbiter to count orbits for.</param>
        /// <returns>The total number of direct and indirect orbits.</returns>
        static int CountOrbits(Dictionary<string, string> orbits, string orbiter)
        {
            int count = 0;

            // Traverse the orbit chain until the center of mass (COM) is reached
            while (orbits.TryGetValue(orbiter, out string? center))
            {
                orbiter = center!;
                count++;
            }

            return count;
        }

        /// <summary>
        /// Calculates the total number of direct and indirect orbits for all orbiters.
        /// </summary>
        /// <param name="orbits">A dictionary mapping each orbiter to its center.</param>
        /// <returns>The total number of direct and indirect orbits.</returns>
        static int CalculateTotalOrbits(Dictionary<string, string> orbits)
        {
            int totalOrbits = 0;

            // Sum the orbits for each orbiter in the dictionary
            foreach (string orbiter in orbits.Keys)
            {
                totalOrbits += CountOrbits(orbits, orbiter);
            }

            return totalOrbits;
        }
    }
}
