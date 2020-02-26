// <copyright file="BasicCell.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// Peyton Urquhart, 11622450

namespace CptS321
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// BasicCell test used to create basic cells from the abstract cell class.
    /// </summary>
    public class BasicCell : Cell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasicCell"/> class.
        /// </summary>
        /// <param name="columnIndex">
        ///  The column index for the cell. Read only.
        /// </param>
        /// <param name="rowIndex">
        /// The row index for the cell. Read only.
        /// </param>
        public BasicCell(int rowIndex, int columnIndex)
            : base(rowIndex, columnIndex)
        {
            return;
        }
    }
}