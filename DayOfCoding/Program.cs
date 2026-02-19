using System;
using System.IO;
using System.Linq;

namespace DayOfCoding
{
    internal class Program
    {
        static void Main()
        {
            bool _fileRead = false;
            do
            {
                try
                {
                    Console.WriteLine("Enter the path to the input file:"); // Prompt the user to enter the file path
                    var inputFile = Console.ReadLine(); // read the file path
                    if (System.IO.File.Exists(inputFile))
                    {
                        _fileRead = true; // Set the flag to true if the file exists
                        int safe = safeLevelsCheck(inputFile); // Call the function to check the safe levels
                        Console.WriteLine("Total safe levels: " + safe); // Output the total number of safe levels
                        Console.WriteLine("Press any key to exit...");
                        Console.ReadLine(); 
                    }
                    else
                    {
                        Console.WriteLine("File not found. Please try again."); // Inform the user if the file does not exist
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message); // Handle any exceptions that may occur
                }
            }
            while (!_fileRead);
        }

        static int safeLevelsCheck(string inputFile)
        {
            int safeTotal = 0;

            Console.WriteLine("Reading current data from file: " + inputFile);
            var lines = File.ReadAllLines(inputFile);

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                int[] levels = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                Console.Write(string.Join(" ", levels));
                if (isSafe(levels))
                {
                    safeTotal++;
                    Console.WriteLine(" - Safe");

                }
                else
                {
                    Console.WriteLine(" - Unsafe");

                }
            }
            return safeTotal;
        }

        static bool isSafe(int[] x)
        {
            if (x.Length < 2) return true; // If there are less than 2 levels, it's considered safe
            int firstDiff = x[1] - x[0]; // Calculate the first difference to determine the trend

            if (firstDiff == 0) return false;

            bool increasing = firstDiff > 0;

            int abs = Math.Abs(firstDiff);

            if (abs < 1 || abs > 3) return false; // Check if the difference is within the allowed range

            for (int i = 2; i < x.Length; i++)
            {
                int diff = x[i] - x[i - 1];
                if (diff == 0) return false; // No level should be the same as the previous one
                if ((diff > 0) != increasing) return false; // Check if the trend is consistent
                if (Math.Abs(diff) < 1 || Math.Abs(diff) > 3) return false; // Check if the difference is within the allowed range
            }

            return true; // If all checks passed, the levels are considered safe
        }
    }
}