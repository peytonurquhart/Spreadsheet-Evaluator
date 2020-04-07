// <copyright file="CellChangeTextCommand.cs" company="PlaceholderCompany">
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
    /// Command object for changing a cells text (and reverting it).
    /// </summary>
    public class CellChangeTextCommand : ICommand
    {
        private Cell cellRef = null;

        private string oldText = null;

        private string newText = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="CellChangeTextCommand"/> class.
        /// </summary>
        /// <param name="c">
        /// An existing cell to instantiate a command from.
        /// </param>
        /// <param name="newText">
        /// The new text intended for the cell.
        /// </param>
        public CellChangeTextCommand(Cell c, string newText)
        {
            this.cellRef = c;
            this.oldText = c.Text;
            this.newText = newText;
        }

        /// <summary>
        /// Gets the name for the command.
        /// </summary>
        /// <returns>
        /// The commands name.
        /// </returns>
        string ICommand.GetTag()
        {
            return "Change Cell Text";
        }

        /// <summary>
        /// Update the cells old text to its intended new text.
        /// </summary>
        void ICommand.Execute()
        {
            this.cellRef.Text = this.newText;
        }

        /// <summary>
        /// Revert the cells new text to its old text.
        /// </summary>
        void ICommand.UnExecute()
        {
            this.cellRef.Text = this.oldText;
        }
    }
}
