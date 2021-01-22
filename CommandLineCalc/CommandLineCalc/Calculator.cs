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

        /* Writes formatted into text to console.
         * 
         * No arg(s).
         * 
         * No return.
         */
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

        /* Prompts user for input
         * 
         * No arg(s)
         * 
         * Return: user input
         */
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

        /* Parses input of user into something we can solve
         * 
         * input: user input
         * 
         * Return: IMathTree with values 'branching' to solve
         *  
         */
        static IMathTree ParseUserInput(string input)
        {
            foreach ((char op, Type type) type in IMathTree.Types) // iterates through all of the operators that IMathTree knows about
            {
                if (type.op == ' ') // base case, accounts for no operators left in string
                {
                    return (IMathTree)Activator.CreateInstance(type.type, double.Parse(input));
                }
                else if (input.Contains(type.op))
                {
                    return (IMathTree)Activator.CreateInstance(type.type, SplitTree(input, type.op));
                }
            }

            throw new FormatException(); // this should never be reached
        }

        /* Helper method to ParseUserInput
         * 
         * Takes an input string and a operator (char)
         * 
         * Returns a IMathTree array split by that operator
         */
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
