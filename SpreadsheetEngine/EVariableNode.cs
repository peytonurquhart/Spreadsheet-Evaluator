// <copyright file="EVariableNode.cs" company="PlaceholderCompany">
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
    /// Variable Node class.
    /// </summary>
    internal class EVariableNode : ENode
    {
        // This field should hold a reference to the dictionary of variable values in the expression tree class.
        private Dictionary<string, double> variables = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="EVariableNode"/> class.
        /// </summary>
        /// <param name="varName">
        /// The name of the variable to encapsulate in the node.
        /// </param>
        /// <param name="allVars">
        /// A reference to the expression trees dictionary of variables.
        /// </param>
        internal EVariableNode(string varName, ref Dictionary<string, double> allVars)
        {
            this.Name = varName;

            this.variables = allVars;
        }

        /// <summary>
        /// Gets or sets the name of the variable.
        /// </summary>
        private string Name { get; set; }

        /// <summary>
        /// Evaluates the numberical value of the variable (a double).
        /// </summary>
        /// <returns>
        /// A double representing the numerical value of the variable.
        /// </returns>
        public override double Evaluate()
        {
            if (this.variables.ContainsKey(this.Name))
            {
                double value = this.variables[this.Name];

                return value;
            }

            return 0.0;
        }
    }
}
