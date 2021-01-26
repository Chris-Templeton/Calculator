using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLineCalc
{
    public interface IMathTree
    {
        /// <summary>
        /// List of operators stored as characters and matching IMathTree type. Must be in reverse order of operations order.
        /// </summary>
        public static List<(char, Type)> Types = new List<(char, Type)>()
        {
            ('+', typeof(Addition)),
            ('-', typeof(Subtraction)),
            ('*', typeof(Multiplication)),
            ('/', typeof(Division)),
            ('^', typeof(Exponent)),
            (' ', typeof(Base)) 
        };

        /// <summary>
        /// Solves the IMathTree to a double value.
        /// </summary>
        /// <returns>Double value from solving all Math Tree operations (types).</returns>
        public double Solve();
    }

    // below are all of the classes that implement IMathTree
    class Addition : IMathTree
    {
        private IMathTree[] Values;

        public Addition(params IMathTree[] values)
        {
            Values = values;
        }

        public double Solve()
        {
            double result = 0;
            foreach (IMathTree value in Values)
            {
                result += value.Solve();
            }
            return result;
        }
    }

    class Subtraction : IMathTree
    {
        private IMathTree[] Values;

        public Subtraction(params IMathTree[] values)
        {
            Values = values;
        }

        public double Solve()
        {
            double result = Values[0].Solve();
            for (int i = 1; i < Values.Length; i++)
            {
                result -= Values[i].Solve();
            }
            return result;
        }
    }

    class Multiplication : IMathTree
    {
        private IMathTree[] Values;

        public Multiplication(params IMathTree[] values)
        {
            Values = values;
        }

        public double Solve()
        {
            double result = Values[0].Solve();
            for (int i = 1; i < Values.Length; i++)
            {
                result *= Values[i].Solve();
            }
            return result;
        }
    }

    class Division : IMathTree
    {
        private IMathTree[] Values;

        public Division(params IMathTree[] values)
        {
            Values = values;
        }

        public double Solve()
        {
            double result = Values[0].Solve();
            for (int i = 1; i < Values.Length; i++)
            {
                result /= Values[i].Solve();
            }
            return result;
        }
    }

    class Exponent : IMathTree
    {
        private IMathTree[] Values;

        public Exponent(params IMathTree[] values)
        {
            Values = values;
        }

        public double Solve()
        {
            double result = Values[0].Solve();
            for (int i = 1; i < Values.Length; i++)
            {
                result = Math.Pow(result, Values[i].Solve());
            }
            return result;
        }
    }

    class Base : IMathTree
    {
        private double Value;
        public Base(double value)
        {
            Value = value;
        }

        public double Solve()
        {
            return Value;
        }
    }
}
