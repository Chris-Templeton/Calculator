using System;
using System.Collections.Generic;
using System.Text;
using Calculator.MathTree;

namespace CommandLineCalc
{
    public static class ParseHelper
    {
        /// <summary>
        /// Parse input string into an IMathTree
        /// </summary>
        /// <param name="input">Input string.</param>
        /// <returns>Parsed IMathTree.</returns>
        public static MathTree Parse(string input)
        {
            // clean up parenthesis
            input = CheckForParenthesis(input);

            // clean up leading negative sign
            if (!String.IsNullOrEmpty(input) && input[0] == '-')
            {
                input = $"0 {input}";
            }

            // finds a defined operator (defined in MathTree) and splits input based on it
            foreach ((char op, Type t) type in MathTree.Types)
            {
                if (type.op == ' ') // base case, accounts for no operators left in string
                {
                    return (MathTree)Activator.CreateInstance(type.t, decimal.Parse(input));
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

                    // only adds the operator to the list to split if it's outside of all parenthesis
                    if (numParenthesis == 0 && input[i] == type.op) 
                    {
                        charIndexToSplit.Add(i);
                    }
                }

                // split if operator is found
                if (charIndexToSplit.Count > 0)
                {
                    return (MathTree)Activator.CreateInstance(type.t, SplitTree(input, charIndexToSplit.ToArray()));
                }
            }

            throw new FormatException(); // this should never be reached
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
                for (int i = 0; parenthesisMatch && i < input.Length - 1; i++)
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

        /// <summary>
        /// Helper method to Parse. Does the work of splitting a string by a given operator.
        /// </summary>
        /// <param name="input">User input.</param>
        /// <param name="opIndexes">Indexes of an operator in input to split the string.</param>
        /// <returns>Math Tree array of split Math Trees by given operator.</returns>
        static MathTree[] SplitTree(string input, params int[] opIndexes)
        {
            List<MathTree> output = new List<MathTree>();

            for (int i = 0; i < opIndexes.Length; i++)
            {
                if (i == 0)
                {
                    output.Add(Parse(input.Substring(0, opIndexes[i])));
                }
                else
                {
                    output.Add(Parse(input.Substring(opIndexes[i - 1] + 1, opIndexes[i] - opIndexes[i - 1] - 1)));
                }

                if (i == opIndexes.Length - 1)
                {
                    output.Add(Parse(input.Substring(opIndexes[i] + 1)));
                }
            }

            return output.ToArray();
        }
    }
}
