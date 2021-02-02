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
            (' ', typeof(RootValue))
        };


        /// <summary>
        /// Solves the IMathTree to a double value.
        /// </summary>
        /// <returns>Double value from solving all Math Tree operations (types).</returns>
        double Solve();
    }

    /////////////////////////
    // below are all of the classes that implement IMathTree
    /////////////////////////

    /// <summary>
    /// Most low-level IMathTree class. represents an individual double value
    /// </summary>
    public class RootValue : IMathTree
    {
        private double Value;

        public RootValue(double value)
        {
            Value = value;
        }

        public double Solve()
        {
            return Value;
        }
    }

    public class Addition : IMathTree
    {
        private IMathTree[] Values;

        public Addition(params IMathTree[] values)
        {
            Values = values;
        }

        public Addition(params double[] values)
        {
            Values = new IMathTree[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                Values[i] = new RootValue(values[i]);
            }
        }

        public double Solve()
        {
            decimal result = (decimal)Values[0].Solve();
            for (int i = 1; i < Values.Length; i++)
            {
                result += (decimal)Values[i].Solve();
            }
            return (double)result;
        }
    }

    public class Subtraction : IMathTree
    {
        private IMathTree[] Values;

        public Subtraction(params IMathTree[] values)
        {
            Values = values;
        }

        public double Solve()
        {
            decimal result = (decimal)Values[0].Solve();
            for (int i = 1; i < Values.Length; i++)
            {
                result -= (decimal)Values[i].Solve();
            }
            return (double)result;
        }
    }

    public class Multiplication : IMathTree
    {
        private IMathTree[] Values;

        public Multiplication(params IMathTree[] values)
        {
            Values = values;
        }

        public double Solve()
        {
            decimal result = (decimal)Values[0].Solve();
            for (int i = 1; i < Values.Length; i++)
            {
                result *= (decimal)Values[i].Solve();
            }
            return (double)result;
        }
    }

    public class Division : IMathTree
    {
        private IMathTree[] Values;

        public Division(params IMathTree[] values)
        {
            Values = values;
        }

        public double Solve()
        {
            decimal result = (decimal)Values[0].Solve();
            for (int i = 1; i < Values.Length; i++)
            {
                result /= (decimal)Values[i].Solve();
            }
            return (double)result;
        }
    }

    public class Exponent : IMathTree
    {
        private IMathTree[] Values;

        public Exponent(params IMathTree[] values)
        {
            Values = values;
        }

        public double Solve()
        {
            decimal result = (decimal)Values[0].Solve();
            for (int i = 1; i < Values.Length; i++)
            {
                result = (decimal)Math.Pow((double)result, Values[i].Solve());
            }
            return (double)result;
        }
    }
}
