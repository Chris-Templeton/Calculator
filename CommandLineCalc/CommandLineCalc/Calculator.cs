using System;

namespace CommandLineCalc
{
    class Calculator
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
        static IMathTree ParseUserInput(string input)
        {
            foreach ((char op, Type t) type in IMathTree.Types) // iterates through all of the operators (op) that IMathTree knows about
            {
                if (type.op == ' ') // base case, accounts for no operators left in string
                {
                    return (IMathTree)Activator.CreateInstance(type.t, double.Parse(input));
                }
                else if (input.Contains(type.op))
                {
                    return (IMathTree)Activator.CreateInstance(type.t, SplitTree(input, type.op));
                }
            }

            throw new FormatException(); // this should never be reached
        }

        /// <summary>
        /// Helper method to ParseUserInput. Does the work of splitting a string by a given operator.
        /// </summary>
        /// <param name="input">User input.</param>
        /// <param name="op">Math operator to split input.</param>
        /// <returns>Math Tree array of split Math Trees by given operator.</returns>
        static IMathTree[] SplitTree(string input, char op)
        {
            string[] split = input.Split(op);
            IMathTree[] output = new IMathTree[split.Length];
            for (int i = 0; i < split.Length; i++)
            {
                output[i] = ParseUserInput(split[i]);
            }
            return output;
        }
    }
}
