// <copyright file="Spreadsheet.cs" company="PlaceholderCompany">
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
    /// Spreadsheet class which serves as the logic engine behind the spreadsheet of cells. Encapsulates a 2d array of cells.
    /// </summary>
    public class Spreadsheet : INotifyPropertyChanged
    {
        private Cell[,] matrix = null;

        private int numRows = 0;

        private int numColumns = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
        /// Inititalizes the matrix of cells.
        /// </summary>
        /// <param name="numRows">
        /// The number of rows the spreadsheet should contain.
        /// </param>
        /// <param name="numColumns">
        /// The number of columns the spreadsheet should contain.
        /// </param>
        /// <param name="c">
        /// Type of cell to initialize the spreadsheet to. Only 'Celltype.Basic' supported.
        /// </param>
        public Spreadsheet(int numRows, int numColumns, CellType c)
        {
            if (c == CellType.Basic)
            {
                this.numRows = numRows;
                this.numColumns = numColumns;

                // Create a matrix with the specified amount of rows and columns
                this.matrix = new BasicCell[this.numRows, this.numColumns];

                // Initialize the rows and columns for each cell
                for (int i = 0; i < this.numRows; i++)
                {
                    for (int j = 0; j < this.numColumns; j++)
                    {
                        this.matrix[i, j] = new BasicCell(i, j);
                    }
                }

                // Subscribe to each cells PropertyChangedEvent
                for (int i = 0; i < this.numRows; i++)
                {
                    for (int j = 0; j < this.numColumns; j++)
                    {
                        this.matrix[i, j].PropertyChanged += this.CellPropertyChanged;
                    }
                }
            }
        }

        /// <summary>
        /// PropertyChanged will fire each time any cells Text has changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// Provides enumerated types for the type of cell the spreadsheet should autogenreate.
        /// </summary>
        public enum CellType
        {
            /// <summary>
            /// A basic spreadsheet cell.
            /// </summary>
            Basic,
        }

        /// <summary>
        /// Gets the number of rows in the spreadsheet
        /// </summary>
        public int RowCount
        {
            get
            {
                return this.numRows;
            }
        }

        /// <summary>
        /// Gets the number of columns in the spreadsheet
        /// </summary>
        public int ColumnCount
        {
            get
            {
                return this.numRows;
            }
        }

        /// <summary>
        /// Accepts a row and column index and returns the corresponding cell if it exists, otherwise null.
        /// </summary>
        /// <param name="rowIndex">
        /// The desired row index for the return cell.
        /// </param>
        /// <param name="columnIndex">
        /// The desired column index for the return cell.
        /// </param>
        /// <returns>
        /// Returns a cell at the given row and column indeces if that cell exits, otherwise returns null;
        /// </returns>
        public Cell GetCell(int rowIndex, int columnIndex)
        {
            if (rowIndex >= 0 && rowIndex < this.numRows)
            {
                if (columnIndex >= 0 && columnIndex < this.numColumns)
                {
                    return this.matrix[rowIndex, columnIndex];
                }
            }

            return null;
        }

        /// <summary>
        /// Event handler for when any cells property is changed, also calls an overarching propertychanged event to notify that the spreadsheet has changed.
        /// </summary>
        private void CellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // If the cells text has changed then notify that a cell has changed
            if (e.PropertyName == "Text")
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs("Cell"));
            }
        }
    }
}