// <copyright file="EOperatorNodePow.cs" company="PlaceholderCompany">
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
    /// Plus Operator Node Class.
    /// </summary>
    internal class EOperatorNodePow : EOperatorNode
    {
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
        /// <param name="n">
        /// This node.
        /// </param>
        /// <returns>
        /// A double representing an evaluation of the operator node.
        /// </returns>
        protected override double Evaluate(ENode n)
        {
            EOperatorNodePow o = n as EOperatorNodePow;

            if (o != null)
            {
                return System.Math.Pow(o.Left.Evaluate(), o.Right.Evaluate());
            }

            return 0.0;
        }
    }
}
