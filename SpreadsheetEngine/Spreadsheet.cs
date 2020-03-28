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
            // Ensure numRows and numColumns are valid values.
            if (numRows > 0 && numColumns > 0)
            {
                // For the basic cell type
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
        /// Gets the number of rows in the spreadsheet.
        /// </summary>
        public int RowCount
        {
            get
            {
                return this.numRows;
            }
        }

        /// <summary>
        /// Gets the number of columns in the spreadsheet.
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
        /// Returns a cell at the given row and column indeces if that cell exits, otherwise returns null.
        /// </returns>
        public Cell GetCell(int rowIndex, int columnIndex)
        {
            // Ensure the given index is valid, and if so return the cell at that index
            if (this.IsValidIndex(rowIndex, columnIndex))
            {
                return this.matrix[rowIndex, columnIndex];
            }

            return null;
        }

        /// <summary>
        /// Returns true if given a valid index for the spreadsheet, else false.
        /// </summary>
        private bool IsValidIndex(int r, int c)
        {
            // Return true if the given index is valid for the spreadsheet
            if (r >= 0 && r < this.numRows)
            {
                if (c >= 0 && c < this.numColumns)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Accepts a possible cell reference string and outputs the index if valid, Only supports one-letter column values for now.
        /// String should be of type =A1. Where A is the column and 1 is the row.
        /// </summary>
        private bool GetCellIndexFromCellReference(string cellRef, out int row, out int col)
        {
            // row should be a single letter at the first index (cast to its integer index counterpart)
            col = (int)((char)cellRef[1]) - 65;

            string rowBuilder = null;

            // column number should start at index 2 to index[strlen - 1]
            for (int i = 2; i < cellRef.Length; i++)
            {
                rowBuilder += cellRef[i];
            }

            // if the parse fails
            if (!int.TryParse(rowBuilder, out row))
            {
                return false;
            }
            else
            {
                row -= 1;
            }

            // if we get a valid matrix index return true
            if (this.IsValidIndex(row, col))
            {
                return true;
            }

            return false;
        }

        // Returns true if the given cell text is a reference or formula.
        private bool IsReferenceOrFormula(string cellText)
        {
            if (cellText[0] == '=')
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Event handler for when any cells property is changed, also calls an overarching propertychanged event to notify that the spreadsheet has changed.
        /// </summary>
        /// <param name="sender">
        /// Objects whoms property changed.
        /// </param>
        /// <param name="e">
        /// Arguments from the changed object.
        /// </param>
        private void CellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // If the cells text has changed then notify that a cell has changed
            if (e.PropertyName == "Text")
            {
                Cell c = (Cell)sender;

                // If it is a cell referecne
                if (this.IsReferenceOrFormula(c.Text))
                {
                    this.UpdateNewReferenceOrFormula(c);
                }
                else
                {
                    c.Value = c.Text;

                    // We have updated a cells value, so we will send the cell with tag "Value".
                    this.PropertyChanged(c, new PropertyChangedEventArgs("Value"));
                }
            }
        }

        // If a cells text has changed and the new text is a reference of formula, this method is called. The cells value will be changed appropriatly.
        private void UpdateNewReferenceOrFormula(Cell c)
        {
            int row = 0;

            int col = 0;

            // Note we are changing the cells text which will call CellPropertyChanged again, then its final value will finally be updated.
            if (this.GetCellIndexFromCellReference(c.Text, out row, out col))
            {
                c.Value = this.matrix[row, col].Value;

                // We now update the cells value to match that of the cell it references. Note that its text should still be =[Cell].
                this.PropertyChanged(c, new PropertyChangedEventArgs("Value"));
            }
            else
            {
                c.Text = "Invalid Cell";
            }
        }
    }
}