// <copyright file="OperatorNodeFactory.cs" company="PlaceholderCompany">
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
    /// Factory class for creating an operator node based on the operator type.
    /// </summary>
    internal static class OperatorNodeFactory
    {
        /// <summary>
        /// Creates a new operator node based on the operator type.
        /// </summary>
        /// <param name="op">
        /// A char representing the opeartor type.
        /// </param>
        /// <returns>
        /// A new operator node subclass object based on the given operator.
        /// </returns>
        internal static EOperatorNode CreateOperatorNode(char op)
        {
            switch (op)
            {
                case '+':
                    return new EOperatorNodePlus();
                case '-':
                    return new EOperatorNodeMinus();
                case '*':
                    return new EOperatorNodeTimes();
                case '/':
                    return new EOperatorNodeDivide();
                case '^':
                    return new EOperatorNodePow();
                default:
                    throw new Exception("ERROR: Invalid operator given to operator node factory: " + op.ToString());
            }
        }
    }
}
