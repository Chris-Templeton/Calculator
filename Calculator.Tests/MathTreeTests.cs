﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Calculator.MathTree;

namespace CommandLineCalc.Tests
{
    [TestClass]
    public class MathTreeTests
    {
        [DataTestMethod]
        [DataRow(3)]
        [DataRow(4.5)]
        [DataRow(300000.762)]
        public void BaseTests(double value)
        {
            MathTree root = new RootValue((decimal)value);
            Assert.AreEqual((decimal)value, root.Solve());
        }

        [DataTestMethod]
        [DataRow(7, 4, 3)]
        [DataRow(10, 1, 2, 3, 4)]
        [DataRow(5.5, 2.3, 3.2)]
        public void AdditionTests(double solution, params double[] values)
        {
            MathTree add = new Addition(ConvertDoubleToTree(values));
            Assert.AreEqual((decimal)solution, add.Solve());
        }

        [DataTestMethod]
        [DataRow(3, 7, 4)]
        [DataRow(-5, 10, 7, 8)]
        [DataRow(.2, 7.5, 3.5, 3.8)]
        public void SubtractionTests(double solution, params double[] values)
        {
            MathTree sub = new Subtraction(ConvertDoubleToTree(values));
            Assert.AreEqual((decimal)solution, sub.Solve());
        }

        [DataTestMethod]
        [DataRow(4, 2, 2)]
        [DataRow(48, 3, 2, 8)]
        [DataRow(5, 2.5, 2)]
        public void MultiplactionTests(double solution, params double[] values)
        {
            MathTree mult = new Multiplication(ConvertDoubleToTree(values));
            Assert.AreEqual((decimal)solution, mult.Solve());
        }

        [DataTestMethod]
        [DataRow(10, 20, 2)]
        [DataRow(5, 20, 2, 2)]
        [DataRow(.3, 3, 10)]
        public void DivisionTests(double solution, params double[] values)
        {
            MathTree div = new Division(ConvertDoubleToTree(values));
            Assert.AreEqual((decimal)solution, div.Solve());
        }

        [DataTestMethod]
        [DataRow(4, 2, 2)]
        [DataRow(16, 2, 2, 2)]
        [DataRow(6.25, 2.5, 2)]
        public void ExponentTests(double solution, params double[] values)
        {
            MathTree exp = new Exponent(ConvertDoubleToTree(values));
            Assert.AreEqual((decimal)solution, exp.Solve());
        }



        /// <summary>
        /// Converts array of double inputs to an array of MathTree outputs
        /// </summary>
        /// <param name="values">Double values to convert.</param>
        /// <returns>MathTree array of converted values.</returns>
        private MathTree[] ConvertDoubleToTree(params double[] values)
        {
            List<MathTree> treeValues = new List<MathTree>();
            foreach (double value in values)
            {
                treeValues.Add(new RootValue((decimal)value));
            }
            return treeValues.ToArray();
        }
    }
}
