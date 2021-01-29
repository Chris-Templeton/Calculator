using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommandLineCalc.Tests
{
    [TestClass]
    public class CalcTests
    {

        [DataTestMethod]
        [DataRow("3 + 4",7)]
        [DataRow("3 + 4 + 3", 10)]
        [DataRow("3.5 + 6.5 + 2.0 + 1", 13)]
        public void AdditionTests(string input, double solution)
        {
            double sol = Calculator.Calculate(input);
            Assert.AreEqual(solution, sol);
        }


        [DataTestMethod]
        [DataRow("3-4", -1)]
        [DataRow("4 - 3", 1)]
        [DataRow("6.5 - 2 - 3.5", 1)]
        [DataRow("250 - 50 - 25 - 30", 145)]
        [DataRow("6 + 7 - 3", 10)]
        [DataRow("10 - 3 + 4", 11)]
        public void SubtractionTests(string input, double solution)
        {
            double sol = Calculator.Calculate(input);
            Assert.AreEqual(solution, sol);
        }


        [DataTestMethod]
        [DataRow("3 * 4", 12)]
        [DataRow("3 * 4*5", 60)]
        [DataRow(".5 * 2 * 7 * 1.5", 10.5)]
        [DataRow("3 * 4 + 2", 14)]
        [DataRow("2 * 3 + 4 * 5", 26)]
        [DataRow("4 * 5 - 2 * 3", 14)]
        public void MultiplicationTests(string input, double solution)
        {
            double sol = Calculator.Calculate(input);
            Assert.AreEqual(solution, sol);
        }

        
        [DataTestMethod]
        [DataRow(" 2 / 4", .5)]
        [DataRow("3 + 4 / 2", 5)]
        [DataRow("16 / 2 / 2", 4)]
        public void DivisionTests(string input, double solution)
        {
            double sol = Calculator.Calculate(input);
            Assert.AreEqual(solution, sol);
        }


        [DataTestMethod]
        [DataRow("2 ^ 4", 16)]
        [DataRow("2 ^ 2 ^ 2", 16)]
        [DataRow("3 ^ 4 ^ 5", 3486784401)]
        public void ExponentTests(string input, double solution)
        {
            double sol = Calculator.Calculate(input);
            Assert.AreEqual(solution, sol);
        }


        [DataTestMethod]
        // addition
        [DataRow("(3+4)", 7)]
        [DataRow("(3+4)+6", 13)]
        [DataRow("(6+2) + (8 + 1)", 17)]
        // subtraction
        [DataRow("15 - (3 + 2)", 10)]
        [DataRow("7 - (4 - 2)", 5)]
        // multiplication
        [DataRow("3 * (4 + 2)", 18)]
        [DataRow("(4 - 3) * 5 + 2", 7)]
        [DataRow("2 +((3 + 2) * 2) -5", 7)]
        // division
        [DataRow("(3 + 5) / 2", 4)]
        [DataRow("8 / (1 + 1)", 4)]
        // exponents
        [DataRow("2 ^ (3 + 1)", 16)]
        [DataRow("(3 + 2) ^ (1 + 1)", 25)]
        public void ParenthesisTests(string input, double solution)
        {
            double sol = Calculator.Calculate(input);
            Assert.AreEqual(solution, sol);
        }
    }
}
