using System;
using System.Collections.Generic;

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
                        // parse user input
                        IMathTree tree = ParseUserInput(input);

                        // calculate result
                        result = $"Equals: {tree.Solve()}";
                    }
                    catch (FormatException)
                    {
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
            string input = Console.ReadLine();

            // if string starts with a '-' (ie -7) add a 0 to the front to force subtraction
            if (!String.IsNullOrEmpty(input) && input[0] == '-')
            {
                input = $"0{input}";
            }

            return input;
        }

        /// <summary>
        /// Parses user input into a IMathTree. Recursively creates tree based on sub-portions of input broken by mathematical operators
        /// </summary>
        /// <param name="input">User input.</param>
        /// <returns>Math Tree respecting order of ops.</returns>
        public static IMathTree ParseUserInput(string input)
        {
            // handle parenthesis
            input = CheckForParenthesis(input);

            // iterates through all of the operators (op) that IMathTree knows about
            foreach ((char op, Type t) type in IMathTree.Types)
            {
                if (type.op == ' ') // base case, accounts for no operators left in string
                {
                    return (IMathTree)Activator.CreateInstance(type.t, double.Parse(input));
                }

                List<int> charIndexToSplit = new List<int>();
                int numParenthesis = 0;
                for (int i = 0; i < input.Length; i++)
                {
                    if (input[i] == '(')
                    {
                        numParenthesis++;
                    }
                    if (input[i] == ')')
                    {
                        numParenthesis--;
                    }

                    if (numParenthesis == 0 && input[i] == type.op)
                    {
                        charIndexToSplit.Add(i);
                    }
                }
                if (charIndexToSplit.Count > 0)
                {
                    return (IMathTree)Activator.CreateInstance(type.t, SplitTree(input, charIndexToSplit.ToArray()));
                }
            }

            throw new FormatException(); // this should never be reached
        }

        /// <summary>
        /// Helper method to ParseUserInput. Does the work of splitting a string by a given operator.
        /// </summary>
        /// <param name="input">User input.</param>
        /// <param name="opIndexes">Indexes of an operator in input to split the string.</param>
        /// <returns>Math Tree array of split Math Trees by given operator.</returns>
        static IMathTree[] SplitTree(string input, params int[] opIndexes)
        {
            List<IMathTree> output = new List<IMathTree>();

            for (int i = 0; i < opIndexes.Length; i++)
            {
                if (i == 0)
                {
                    output.Add(ParseUserInput(input.Substring(0, opIndexes[i])));
                }
                else
                {
                    output.Add(ParseUserInput(input.Substring(opIndexes[i - 1] + 1, opIndexes[i] - opIndexes[i - 1] - 1)));
                }

                if (i == opIndexes.Length - 1)
                {
                    output.Add(ParseUserInput(input.Substring(opIndexes[i] + 1)));
                }
            }

            return output.ToArray();
        }

        /// <summary>
        /// Removes outer parenthesis from string if and only if the parenthesis 'match'.
        /// </summary>
        /// <param name="input">User input string.</param>
        /// <returns>String without outer parenthesis (if applicable).</returns>
        static string CheckForParenthesis(string input)
        {
            // need to trim input in case of extra space
            input = input.Trim();

            // if input starts & ends with parenthesis, check that they match and remove
            bool parenthesisMatch = true;
            while (parenthesisMatch && input[0] == '(' && input[input.Length - 1] == ')')
            {
                int numParenthesis = 0;
                for (int i = 0; parenthesisMatch && i < input.Length-1; i++)
                {
                    if (input[i] == '(')
                    {
                        numParenthesis++;
                    }
                    if (input[i] == ')')
                    {
                        numParenthesis--;
                    }
                    if (numParenthesis == 0)
                    {
                        parenthesisMatch = false;
                    }
                }

                if (parenthesisMatch)
                {
                    input = input.Substring(1, input.Length - 2);
                }
            }
            return input;
        }
    }
}
