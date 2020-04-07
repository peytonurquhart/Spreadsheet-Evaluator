// <copyright file="CellChangeBGColorCommand.cs" company="PlaceholderCompany">
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
    /// Command object for changing a cells background color (and reverting it).
    /// </summary>
    public class CellChangeBGColorCommand : ICommand
    {
        private Cell cellRef = null;

        private uint oldColor;

        private uint newColor;

        /// <summary>
        /// Initializes a new instance of the <see cref="CellChangeBGColorCommand"/> class.
        /// </summary>
        /// <param name="c">
        /// An existing cell.
        /// </param>
        /// <param name="newColor">
        /// The new color intended for the cell.
        /// </param>
        public CellChangeBGColorCommand(Cell c, uint newColor)
        {
            this.cellRef = c;
            this.oldColor = c.BGColor;
            this.newColor = newColor;
        }

        /// <summary>
        /// Gets the name for the command.
        /// </summary>
        /// <returns>
        /// The commands name.
        /// </returns>
        string ICommand.GetTag()
        {
            return "Change Cell Color";
        }

        /// <summary>
        /// Changes the cells color to its intended new color.
        /// </summary>
        void ICommand.Execute()
        {
            this.cellRef.BGColor = this.newColor;
        }

        /// <summary>
        /// reverts the cells color back to its old color.
        /// </summary>
        void ICommand.UnExecute()
        {
            this.cellRef.BGColor = this.oldColor;
        }
    }
}
