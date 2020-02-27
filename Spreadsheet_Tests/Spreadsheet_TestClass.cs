﻿// <copyright file="Spreadsheet_TestClass.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// Peyton Urquhart, 11622450

namespace Spreadsheet_Tests
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using CptS321;
    using NUnit.Framework;

    /// <summary>
    /// Class for implementing spreadsheet class tests. Only supports single lettered columns at this point.
    /// </summary>
    [TestFixture]
    public class Spreadsheet_TestClass : INotifyPropertyChanged
    {
        private Cell[,] matrix = null;

        private int numRows = 0;

        private int numColumns = 0;

        private bool eventFlag = false;

        /// <summary>
        /// PropertyChanged will fire each time any cells Text has changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// A test constructor for the upcoming spreadsheet class, will have two params in the future, numRows and numColumns.
        /// </summary>
        [Test]
        public void ConstructorTest()
        {
            this.numRows = 50;
            this.numColumns = 26;

            this.matrix = new BasicCell[this.numRows, this.numColumns];

            // Initialize the rows and columns for each cell
            for (int i = 0; i < this.numRows; i++)
            {
                for (int j = 0; j < this.numColumns; j++)
                {
                    this.matrix[i, j] = new BasicCell(i, j);
                }
            }

            Assert.AreEqual(0, this.matrix[49, 0].ColumnIndex);
            Assert.AreEqual(25, this.matrix[49, 25].ColumnIndex);
            Assert.AreEqual(25, this.matrix[48, 25].ColumnIndex);

            Assert.AreEqual(49, this.matrix[49, 20].RowIndex);
            Assert.AreEqual(48, this.matrix[48, 20].RowIndex);
            Assert.AreEqual(0, this.matrix[0, 20].RowIndex);

            // Subscribe to each cells PropertyChangedEvent
            for (int i = 0; i < this.numRows; i++)
            {
                for (int j = 0; j < this.numColumns; j++)
                {
                    this.matrix[i, j].PropertyChanged += this.CellPropertyChanged;
                }
            }
        }

        /// <summary>
        /// A test for the CellPropertyChanged event.
        /// </summary>
        [Test]
        public void PropertyChanged_Test()
        {
            this.ConstructorTest();

            // Since no cells text has been changed yet, CellPropertyChanged shouldnt have been called
            Assert.False(this.eventFlag);

            // Change a cells property
            this.matrix[0, 0].Text = "hello world";

            // Not CellProperyChanged should have been called
            Assert.True(this.eventFlag);
        }

        /// <summary>
        /// A test for the GetCell Method.
        /// </summary>
        [Test]
        public void GetCell_Test()
        {
            this.ConstructorTest();

            // Get a valid cell
            Cell c = this.GetCell(0, 0);

            // Ensure its the correct cell
            Assert.AreEqual(0, c.ColumnIndex);
            Assert.AreEqual(0, c.RowIndex);

            Cell c1 = this.GetCell(49, 25);

            Assert.AreEqual(25, c1.ColumnIndex);
            Assert.AreEqual(49, c1.RowIndex);
        }

        /// <summary>
        /// A second test for the GetCell Method.
        /// </summary>
        [Test]
        public void GetCell_EdgeTest1()
        {
            this.ConstructorTest();

            // Get an invalid cell
            Cell c = this.GetCell(0, -1);

            // Ensure null is returned
            Assert.Null(c);
        }

        /// <summary>
        /// A third test for the GetCell Method.
        /// </summary>
        [Test]
        public void GetCell_EdgeTest2()
        {
            this.ConstructorTest();

            // Get an invalid cell
            Cell c = this.GetCell(50, 20);

            // Ensure its null
            Assert.Null(c);
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
        /// Test method for getting cell from cell reference string.
        /// </summary>
        [Test]
        public void GetCellFromRef_Test()
        {
            this.ConstructorTest();

            int r = 0;

            int c = 0;

            Assert.True(this.GetCellIndexFromCellReference("=Z49", out r, out c));

            Assert.AreEqual(25, c);

            Assert.AreEqual(48, r);

            // cell Z0 should not be valid because the user interface starts at index 1
            Assert.False(this.GetCellIndexFromCellReference("=Z0", out r, out c));

            Assert.True(this.GetCellIndexFromCellReference("=R50", out r, out c));
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

                this.eventFlag = true;
            }
        }

        /// <summary>
        /// Returns true if given a valid index for the spreadsheet, else false.
        /// </summary>
        private bool IsValidIndex(int r, int c)
        {
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
        /// Accepts a possible cell reference string and outputs the index if valid, Only supports one-letter column values.
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

            // parse the column number to an int
            row = int.Parse(rowBuilder) - 1;

            // if we get a valid matrix index return true
            if (this.IsValidIndex(row, col))
            {
                return true;
            }

            return false;
        }
    }
}