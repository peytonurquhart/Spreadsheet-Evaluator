// <copyright file="ExpressionTreeEvaluator_TestClass.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Spreadsheet_Tests
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using NUnit.Framework;

    /// <summary>
    /// Class for implementing expression tree evaluation of cells tests.
    /// </summary>
    [TestFixture]
    public class ExpressionTreeEvaluator_TestClass
    {
        /// <summary>
        /// Standard Case Test for generating a list of variables from an infix expression.
        /// </summary>
        [Test]
        public void GetVariables_Test1()
        {
            string input = "=A2+A3-8*B6";

            string[] vars = this.GetVariablesFromExpression(input);

            Assert.AreEqual("A2", vars[0]);
            Assert.AreEqual("A3", vars[1]);
            Assert.AreEqual("B6", vars[2]);
        }

        /// <summary>
        /// Standard Case Test for generating a list of variables from an infix expression.
        /// </summary>
        [Test]
        public void GetVariables_Test2()
        {
            string input = "=A204+A312-8*B678";

            string[] vars = this.GetVariablesFromExpression(input);

            Assert.AreEqual("A204", vars[0]);
            Assert.AreEqual("A312", vars[1]);
            Assert.AreEqual("B678", vars[2]);
        }

        /// <summary>
        /// Standard Case Test for generating a list of variables from an infix expression.
        /// </summary>
        [Test]
        public void GetVariables_Test3()
        {
            string input = "=8 + (9*9 + Z21) + 6 + 8 - 2 *(F60- 5)";

            string[] vars = this.GetVariablesFromExpression(input);

            Assert.AreEqual("Z21", vars[0]);
            Assert.AreEqual("F60", vars[1]);
        }

        /// <summary>
        /// Edge test to make sure the parser function stays within bounds when an incorrect input is given.
        /// </summary>
        [Test]
        public void GetVariables_EdgeTest1()
        {
            string input = "=A2+A3-8*B";

            string[] vars = this.GetVariablesFromExpression(input);

            Assert.AreEqual("A2", vars[0]);
            Assert.AreEqual("A3", vars[1]);
            Assert.AreEqual("B", vars[2]);
        }

        /// <summary>
        /// Ensures the algorithm works when there is only one variable in the expression.
        /// </summary>
        [Test]
        public void GetVariables_EdgeTest2()
        {
            string input = "=A2";

            string[] vars = this.GetVariablesFromExpression(input);

            Assert.AreEqual("A2", vars[0]);
        }

        // Generates a list of variables from an infix expression.
        private string[] GetVariablesFromExpression(string input)
        {
            string delimVars = null;

            // Iterate through each character in the input string.
            for (int i = 0; i < input.Length; i++)
            {
                // If the current character represents the start of a variable..
                if (this.IsUpperCaseLetter(input[i]))
                {
                    delimVars += input[i];

                    while ((i + 1 < input.Length) && this.IsInteger(input[i + 1]))
                    {
                        i++;
                        delimVars += input[i];
                    }

                    delimVars += "|";
                }
            }

            return delimVars.Split('|');
        }

        // Returns true if the input represents an upper case letter.
        private bool IsUpperCaseLetter(char c)
        {
            if ((int)c >= 65 && (int) c <= 90)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Returns true if the input represents an integer.
        private bool IsInteger(char c)
        {
            if ((int)c >= 48 && (int)c <= 57)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
