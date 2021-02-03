using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.MathTree
{
    public class Division : MathTree
    {
        public Division(params MathTree[] values) : base(values) { }

        public override decimal Solve()
        {
            decimal result = Values[0].Solve();
            for (int i = 1; i < Values.Length; i++)
            {
                result /= Values[i].Solve();
            }
            return result;
        }
    }
}
