// <copyright file="ICommand.cs" company="PlaceholderCompany">
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
    /// Interface for a command object.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Execute command.
        /// </summary>
        void Execute();

        /// <summary>
        /// UnExecute comman.
        /// </summary>
        void UnExecute();

        /// <summary>
        /// Gets a name for the command.
        /// </summary>
        /// <returns>
        /// The commands name.
        /// </returns>
        string GetTag();
    }
}