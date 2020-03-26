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
    internal abstract class EOperatorNode : ENode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EOperatorNode"/> class.
        /// </summary>
        public EOperatorNode()
        {
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
        /// Evaluates the operator nodes operands based on its operator type and returns the result.
        /// </summary>
        /// <param name="n">
        /// This node.
        /// </param>
        /// <returns>
        /// A double representing an evaluation of the operator node.
        /// </returns>
        protected abstract double Evaluate(ENode n);
    }
}
