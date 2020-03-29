// <copyright file="ExpressionTreeHelper.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CptS321
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class for assisting in building expression trees for evaluating formulas.
    /// </summary>
    public static class ExpressionTreeHelper
    {
        /// <summary>
        /// Gets an array of valid spreadsheet variables from a given infix expression.
        /// </summary>
        /// <param name="input">
        /// An infix expression string.
        /// </param>
        /// <param name="numFound">
        /// Outputs number of variables found.
        /// </param>
        /// <returns>
        /// A list of spreadsheet variables found in an Expression.
        /// </returns>
        public static string[] GetSpreadsheetVariablesInExpression(string input, out int numFound)
        {
            string delimVars = null;

            numFound = 0;

            // Iterate through each character in the input string.
            for (int i = 0; i < input.Length; i++)
            {
                // If the current character represents the start of a variable..
                if (ExpressionTreeHelper.IsUpperCaseLetter(input[i]))
                {
                    delimVars += input[i];

                    numFound++;

                    while ((i + 1 < input.Length) && ExpressionTreeHelper.IsInteger(input[i + 1]))
                    {
                        i++;
                        delimVars += input[i];
                    }

                    delimVars += "|";
                }
            }

            if (numFound >= 1)
            {
                return delimVars.Split('|');
            }

            return null;
        }

        /// <summary>
        /// Returns true if the expression contains prohibited characters for an expression.
        /// </summary>
        /// <param name="input">
        /// An infix expression.
        /// </param>
        /// <returns>
        /// A value indicatin if the input has prohibited characters or not.
        /// </returns>
        public static bool ContainsProhibitedCharacters(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (!IsOp(input[i]) && !IsUpperCaseLetter(input[i]) && !IsInteger(input[i])
                    && input[i] != ' ' && input[i] != '.' && input[i] != ')' && input[i] != '(')
                {
                    return true;
                }
            }

            return false;
        }

        // Returns true if the input represents an upper case letter.
        private static bool IsUpperCaseLetter(char c)
        {
            if ((int)c >= 65 && (int)c <= 90)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool IsOp(char c)
        {
            char[] ops = { '+', '-', '*', '/', '^' };

            for (int i = 0; i < 5; i++)
            {
                if (c == ops[i])
                {
                    return true;
                }
            }

            return false;
        }

        // Returns true if the input represents an integer.
        private static bool IsInteger(char c)
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
