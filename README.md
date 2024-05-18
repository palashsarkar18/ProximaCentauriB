# Proxima Centauri B Orbit Calculator

This project calculates the total number of direct and indirect orbits from a given orbit map data file. The orbit map data is read from a file named `data.txt` which contains orbit relationships in the format `AAA)BBB`, meaning `BBB` orbits `AAA`.

## Project Structure

- `OrbitCalculator`: The main class containing methods to read the orbit data, calculate the total orbits, and count direct and indirect orbits. **This assignment requires inclusion of a single source code file.**

## How to Run

1. Ensure `data.txt` is located in the same directory as the executable or the current working directory.
2. Build and run the project using your preferred C# development environment (e.g., Visual Studio, Visual Studio Code).

### Output

The program will output the total number of direct and indirect orbits calculated from the provided data.

## Code Explanation

The project consists of the following key methods:

- `Main`: The entry point of the application. Reads the orbit data from `data.txt` and calculates the total number of orbits.
- `GetDataMapFromFile`: Reads the orbit data from a file and creates a dictionary mapping orbiters to their centers.
- `CountOrbits`: Counts the total number of direct and indirect orbits for a given orbiter.
- `CalculateTotalOrbits`: Calculates the total number of direct and indirect orbits for all orbiters in the dictionary.

## Error Handling

The program includes error handling for the following scenarios:
- File not found (`FileNotFoundException`).
- Invalid data format (`InvalidDataException`).
- Any other unexpected errors (`Exception`).

## Notes
**What is the total number of direct and indirect orbits in your map data?**

**270768**