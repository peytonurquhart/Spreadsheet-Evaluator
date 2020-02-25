// <copyright file="BasicCell_Test.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// Peyton Urquhart, 11622450

namespace Spreadsheet_Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// BasicCell test used to create basic cells from the abstract cell class.
    /// </summary>
    public class BasicCell_Test : CellTest_Test
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasicCell_Test"/> class.
        /// </summary>
        /// <param name="columnIndex">
        ///  The column index for the cell. Read only.
        /// </param>
        /// <param name="rowIndex">
        /// The row index for the cell. Read only.
        /// </param>
        public BasicCell_Test(int rowIndex, string columnIndex)
            : base(rowIndex, columnIndex)
        {
            return;
        }
    }
}
