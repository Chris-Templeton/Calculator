using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.MathTree
{
    public abstract class MathTree
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


        protected MathTree[] Values;
        protected decimal _value;
        public MathTree(params MathTree[] values)
        {
            Values = values;
        }
        public MathTree(decimal value)
        {
            _value = value;
        }

        /// <summary>
        /// Solves the MathTree to a decimal value.
        /// </summary>
        /// <returns>Double value from solving all Math Tree operations (types).</returns>
        public virtual decimal Solve()
        {
            return _value;
        }
    }
}
