// <copyright file="ENode.cs" company="PlaceholderCompany">
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
    /// Abstract node class, which will be inherited by three different types of Node.
    /// </summary>
    internal abstract class ENode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ENode"/> class.
        /// </summary>
        public ENode()
        {
            return;
        }

        /// <summary>
        /// Abstract function which will evaluate the value of a Node.
        /// </summary>
        /// <returns>
        /// The evaulated value of the Node.
        /// </returns>
        public abstract double Evaluate();
    }
}
