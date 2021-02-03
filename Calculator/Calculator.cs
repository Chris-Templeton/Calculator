using System;
using System.Collections.Generic;
using Calculator.MathTree;

namespace CommandLineCalc
{
    public class Calculator
    {
        static void Main(string[] args)
        {
            // introduction to app
            WriteIntro();

            // allows multiple calculations to be made without restarting the program
            bool running = true;
            while (running)
            {
                // get user input
                string input = GetUserInput();

                if (input == "q")
                {
                    running = false;
                }

                if (running)
                {
                    string result;
                    try
                    {
                        decimal sol = Calculate(input);
                        result = $"Equals: {sol}";
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                        result = "Formatting Error";
                    }

                    // display result
                    Console.WriteLine(result);
                }
            }
        }

        /// <summary>
        /// Writes header to project.
        /// </summary>
        static void WriteIntro()
        {
            Console.WriteLine("#################################");
            Console.WriteLine("#### Command Line Calculator ####");
            Console.WriteLine("####           V1            ####");
            Console.WriteLine("####     Chris Templeton     ####");
            Console.WriteLine("#################################");
            Console.WriteLine();
            Console.WriteLine("           'q' to quit           ");
        }

        /// <summary>
        /// Reads user input. Adds leading 0 if input starts with '-' to account for negative operator.
        /// </summary>
        /// <returns>User input string.</returns>
        static string GetUserInput()
        {
            Console.Write("\nEnter Expression: ");
            return Console.ReadLine();
        }

        /// <summary>
        /// Calculates result from user input.
        /// </summary>
        /// <param name="input">User input.</param>
        /// <returns>double result of expression.</returns>
        public static decimal Calculate(string input)
        {
            MathTree tree = ParseHelper.Parse(input);
            return tree.Solve();
        }
    }
}