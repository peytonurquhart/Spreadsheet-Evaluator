// <copyright file="ExpressionTree.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// Peyton Urquhart, 11622450

namespace CptS321
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class for building and evaulating expression trees from an infix expressions.
    /// </summary>
    public class ExpressionTree
    {
        /// <summary>
        /// Ambigous node representing the root of the expression tree.
        /// </summary>
        private ENode root = null;

        /// <summary>
        /// Dictionary for holding the values of variables used in expressions.
        /// </summary>
        private Dictionary<string, double> variables = new Dictionary<string, double>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="expression">
        /// A valid infix expression to evaluate.
        /// </param>
        public ExpressionTree(string expression)
        {
            this.Condense(ref expression);
            this.root = this.CompileFromPostfix(this.BuildPostfixExpression(expression));
        }

        /// <summary>
        /// Provides enumerated types parenthesis.
        /// </summary>
        private enum ParenthesisType
        {
            None,
            Left,
            Right,
        }

        /// <summary>
        /// Provides enumerated types representing associativity.
        /// </summary>
        private enum Associative
        {
            None,
            Left,
            Right,
        }

        /// <summary>
        /// Adds a new variable that can be used in expressions.
        /// </summary>
        /// <param name="variableName">
        /// The name of the new variable.
        /// </param>
        /// <param name="variableValue">
        /// The value of the new variable.
        /// </param>
        public void SetVariable(string variableName, double variableValue)
        {
             this.variables[variableName] = variableValue;
        }

        /// <summary>
        /// Evaluates the expression tree.
        /// </summary>
        /// <returns>
        /// The result of the expression evaluation.
        /// </returns>
        public double Evaluate()
        {
            if (this.root == null)
            {
                return 0.0;
            }
            else
            {
                return this.root.Evaluate();
            }
        }

        /// <summary>
        /// Compiles an expression tree from a postfix expression and returns the root node.
        /// </summary>
        private ENode CompileFromPostfix(string s)
        {
            Stack<ENode> stack = new Stack<ENode>();

            ENode t = null;

            // For each symbol in the string:
            for (int i = 0; i < s.Length; i++)
            {
                char symbol = s[i];

                t = null;

                // If the symbol is an operator create a tree with its operands and push it back to the stack.
                if (this.IsOp(symbol))
                {
                    EOperatorNode o = OperatorNodeFactory.CreateOperatorNode(symbol);

                    o.Right = stack.Pop();

                    o.Left = stack.Pop();

                    stack.Push(o);
                }
                else
                {
                    double d = 0.0;

                    // If we have a constant
                    if ((int)symbol >= 48 && (int)symbol <= 57)
                    {
                        string n = null;

                        // Continue to build the string for the constant until we find the delimiter representing the end of the constant
                        while (s[i] != '|')
                        {
                            n += s[i];

                            i++;
                        }

                        d = double.Parse(n);

                        t = new EConstantNode(d);

                        stack.Push(t);
                    }
                    else
                    {
                        // We have a variable
                        string n = null;

                        // Continue to build the string for the variable until we hit the delimiter representing the end of the variable
                        while (s[i] != '|')
                        {
                            n += s[i];

                            i++;
                        }

                        t = new EVariableNode(n, ref this.variables);

                        stack.Push(t);
                    }
                }
            }

            // The last item on the stack will be the root of the tree, return it.
            return stack.Pop();
        }

        /// <summary>
        /// Method which implements the Shunting Yard Algorithm for building postfix expressions.
        /// </summary>
        /// <param name="s">
        /// An infix expression to be converted, must be valid.
        /// </param>
        /// <returns>
        /// Returns a postfix expression equivalent to the given infix expression.
        /// </returns>
        private string BuildPostfixExpression(string s)
        {
            // Stack to hold various symbols throughout the algorithm.
            Stack<char> stack = new Stack<char>();

            // String to hold the postfix expression we will build.
            string postfix = string.Empty;

            // Iterate through each symbol in the given infix expression.
            for (int i = 0; i < s.Length; i++)
            {
                char symbol = s[i];

                /*
                 If the symbol is a left parenthesis push to the stack,
                 If the symbol is a right parenthesis build the string from the current stack until we hit a left parenthesis
                 If the symbol is an operator:
                        - If the stack is empty or there is a left parenthesis on top of the stack, push symbol to the stack
                        - If the symbol precedence is greater than that of the stacks top operator or the precedences are the same and the symbol is left associative, push symbol to the stack
                        - If the symbol precedence is less than that of the stacks top operator or the precedences are equal and the symbol is right associative pop and add all symbols from the stack until these conditions are not met.
                 If the symbol is an operand add immediatly to postfix expression.
                 */
                if (this.IsParenthesis(symbol) == ParenthesisType.Left)
                {
                    stack.Push(symbol);
                }
                else if (this.IsParenthesis(symbol) == ParenthesisType.Right)
                {
                    char x = '\0';

                    while (this.IsParenthesis(x) != ParenthesisType.Left && !(stack.Count == 0))
                    {
                        x = stack.Pop();

                        if (this.IsParenthesis(x) != ParenthesisType.Left)
                        {
                            postfix += x;
                        }
                    }
                }
                else if (this.IsOp(symbol))
                {
                    if (stack.Count == 0 || this.IsParenthesis(stack.Peek()) == ParenthesisType.Left)
                    {
                        stack.Push(symbol);
                    }
                    else if ((this.GetOpPrecedence(symbol) > this.GetOpPrecedence(stack.Peek())) ||
                        ((this.GetOpPrecedence(symbol) == this.GetOpPrecedence(stack.Peek())) && this.GetOperatorAssociative(symbol) == Associative.Left))
                    {
                        stack.Push(symbol);
                    }
                    else if ((this.GetOpPrecedence(symbol) < this.GetOpPrecedence(stack.Peek())) ||
                        ((this.GetOpPrecedence(symbol) == this.GetOpPrecedence(stack.Peek())) && this.GetOperatorAssociative(symbol) == Associative.Right))
                    {
                        while (stack.Count > 0 && ((this.GetOpPrecedence(symbol) < this.GetOpPrecedence(stack.Peek())) ||
                            ((this.GetOpPrecedence(symbol) == this.GetOpPrecedence(stack.Peek())) && this.GetOperatorAssociative(symbol) == Associative.Right)))
                        {
                            postfix += stack.Pop();
                        }

                        stack.Push(symbol);
                    }
                }
                else
                {
                    // We have found a variable or a constant
                    postfix += s[i];

                    if (i < s.Length - 1)
                    {
                        // While the next character in the string is not an operator or parentheis, keep adding the rest of this constant or variable
                        while (i < s.Length - 1 && (!this.IsOp(s[i + 1]) && (this.IsParenthesis(s[i + 1]) == ParenthesisType.None)))
                        {
                            i++;

                            postfix += s[i];
                        }
                    }

                    // Add a delimiter representing the end of the variable or constant
                    postfix += "|";
                }
            }

            // Write the stacks remianing symbols to our postfix string.
            while (stack.Count > 0)
            {
                postfix += stack.Pop();
            }

            // Return the postfix string.
            return postfix;
        }

        /// <summary>
        /// Returns true if the paramater is an operator.
        /// </summary>
        private bool IsOp(char c)
        {
            char[] ops = { '+', '-', '*', '/', '^' };

            // Return true if the given symbol matches any of the operator chars
            for (int i = 0; i < 5; i++)
            {
                if (c == ops[i])
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns the precedence of an operator.
        /// </summary>
        private int GetOpPrecedence(char c)
        {
            // Switch based on operator type and return the corresponding precedence
            switch (c)
            {
                case '+':
                    return 6;
                case '-':
                    return 7;
                case '*':
                    return 8;
                case '/':
                    return 8;
                case '^':
                    return 10;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Returns the type of parenthesy the paramater is, or ParenthesisType.None.
        /// </summary>
        private ParenthesisType IsParenthesis(char c)
        {
            if (c == '(')
            {
                return ParenthesisType.Left;
            }
            else if (c == ')')
            {
                return ParenthesisType.Right;
            }

            return ParenthesisType.None;
        }

        /// <summary>
        /// Returns the associativity of the given operator, or Associative.None.
        /// </summary>
        private Associative GetOperatorAssociative(char c)
        {
            // Switch bases on operator type and return the corresponding associativity
            switch (c)
            {
                case '+':
                    return Associative.Left;
                case '-':
                    return Associative.Left;
                case '*':
                    return Associative.Left;
                case '/':
                    return Associative.Left;
                case '^':
                    return Associative.Right;
                default:
                    return Associative.None;
            }
        }

        /// <summary>
        /// Removes all whitespace from a given string.
        /// </summary>
        private void Condense(ref string s)
        {
            s = s.Replace(" ", string.Empty);
            s = s.Replace(" ", "");
        }
    }
}
