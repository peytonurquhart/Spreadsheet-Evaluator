// <copyright file="EOperatorNode.cs" company="PlaceholderCompany">
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
    /// Operator Node class.
    /// </summary>
    internal class EOperatorNode : ENode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EOperatorNode"/> class.
        /// </summary>
        /// <param name="c">
        /// A char representing the type of operator the node represents.
        /// </param>
        public EOperatorNode(char c)
        {
            this.Operator = c;

            this.Left = null;
            this.Right = null;
        }

        /// <summary>
        /// Gets or sets the left operand of the operator.
        /// </summary>
        public ENode Left { get; set; }

        /// <summary>
        /// Gets or sets the right operand of the operator.
        /// </summary>
        public ENode Right { get; set; }

        /// <summary>
        /// Gets or sets the type of operator the node represents.
        /// </summary>
        private char Operator { get; set; }

        /// <summary>
        /// Evaluates the value of the operator regarding its two operands, calles private evaluate(Node) function.
        /// </summary>
        /// <returns>
        /// A double representing the final evaluation of the operator with regards to its operands.
        /// </returns>
        public override double Evaluate()
        {
            return this.Evaluate(this);
        }

        /// <summary>
        /// Evaluates the operator nodes operands based on its operator type and returns the result.
        /// </summary>
        private double Evaluate(ENode n)
        {
            EOperatorNode o = n as EOperatorNode;

            if (o != null)
            {
                switch (o.Operator)
                {
                    case '+':
                        return o.Left.Evaluate() + o.Right.Evaluate();
                    case '-':
                        return o.Left.Evaluate() - o.Right.Evaluate();
                    case '*':
                        return o.Left.Evaluate() * o.Right.Evaluate();
                    case '/':
                        return o.Left.Evaluate() / o.Right.Evaluate();
                    case '^':
                        return System.Math.Pow(o.Left.Evaluate(), o.Right.Evaluate());
                }
            }

            return 0.0;
        }
    }
}
