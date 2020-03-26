// <copyright file="EConstantNode.cs" company="PlaceholderCompany">
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
    /// Constant node class, (a number to be evaluated as a double).
    /// </summary>
    internal class EConstantNode : ENode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EConstantNode"/> class.
        /// </summary>
        /// <param name="val">
        /// The value of the constant node (a double).
        /// </param>
        public EConstantNode(double val = 0)
        {
            this.Value = val;
        }

        /// <summary>
        /// Gets or sets the value of the constant Node.
        /// </summary>
        private double Value { get; set; }

        /// <summary>
        /// Evaluates the value of the constant Node.
        /// </summary>
        /// <returns>
        /// A double representing the value of the Node.
        /// </returns>
        public override double Evaluate()
        {
            return this.Value;
        }
    }
}
