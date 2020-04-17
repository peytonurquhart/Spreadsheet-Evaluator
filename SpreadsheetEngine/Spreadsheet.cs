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
        /// <summary>
        /// Cell remote controller.
        /// </summary>
        private CellRemote cellRemote = new CellRemote();

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

            // Subscribe to the remotees propertychangedevent.
            this.cellRemote.PropertyChanged += this.RemotePropertyChanged;
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
        /// Gets the spreadsheets cellRemote.
        /// </summary>
        public CellRemote Remote
        {
            get
            {
                return this.cellRemote;
            }
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
        /// Clears the spreadsheet only keeping cell rows and columns intact.
        /// </summary>
        public void Clean()
        {
            for (int i = 0; i < this.numRows; i++)
            {
                for (int j = 0; j < this.numColumns; j++)
                {
                    this.matrix[i, j].Clean();
                }
            }
        }

        /// <summary>
        /// Gets a list of all cells in the spreadsheet which are non-default.
        /// </summary>
        /// <returns>
        /// A list of populated cells.
        /// </returns>
        public List<Cell> GetPopulatedCells()
        {
            List<Cell> cl = new List<Cell>();

            for (int i = 0; i < this.numRows; i++)
            {
                for (int j = 0; j < this.numColumns; j++)
                {
                    Cell c = this.matrix[i, j];

                    // If the cell is somehow populated add it to the list.
                    if (c.Text != string.Empty || c.BGColor != 0xFFFFFFFF)
                    {
                        cl.Add(c);
                    }
                }
            }

            return cl;
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
        /// Accepts a possible cell reference string and outputs the index if valid, Only supports one-letter column values for now.
        /// String should be of type =A1. Where A is the column and 1 is the row.
        /// </summary>
        /// <param name="cellRef">
        /// String representing a cell reference.
        /// <param name="row">
        /// Out row number.
        /// </param>
        /// </param>
        /// <param name="col">
        /// Out column number.
        /// </param>
        /// <returns>
        /// If the cell grab was successful or not.
        /// </returns>
        public bool GetCellIndexFromCellReference(string cellRef, out int row, out int col)
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

        /// <summary>
        /// Gets a name for a specified cell in the spreadsheet.
        /// </summary>
        /// <param name="c">
        /// A cell in this spreadsheet.
        /// </param>
        /// <returns>
        /// The name for the cell ex: "A1".
        /// </returns>
        public string GetCellName(Cell c)
        {
            return ((char)(c.ColumnIndex + 65)).ToString() + (c.RowIndex + 1).ToString();
        }

        private void RemotePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Notify the UI that the remotes property has changed so it can update accordingly.
            this.PropertyChanged(sender, e);
        }

        /// <summary>
        /// Event handler for when any cells property is changed. Accepts: { "Text", "Reference", "BGColor" }. Does not handle: { "Value" }.
        /// </summary>
        /// <param name="sender">
        /// Objects whoms property changed.
        /// </param>
        /// <param name="e">
        /// Arguments from the changed object.
        /// </param>
        private void CellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // The sender is always a cell for (CellPropertyChanged).
            if (sender is Cell)
            {
                Cell c = (Cell)sender;

                // If the cells text has changed then notify that a cell has changed
                if (e.PropertyName == "Text")
                {
                    this.CellTextChanged_Helper(c);
                }

                // Here we must re-evaluate any formula because a referenced cell of the sender has changed.
                if (e.PropertyName == "Reference")
                {
                    this.CellReferenceChanged_Helper(c);
                }

                // If the cells background color has changed we must update the UI.
                if (e.PropertyName == "BGColor")
                {
                    this.CellBGColorChanged_Helper(c);
                }
            }
            else
            {
                throw new Exception("ERROR: CellPropertyChanged invoked but the sender was not of type: Cell");
            }
        }

        /// <summary>
        /// To be called when the CellPropertyChanged event handler recieves flag "BGColor".
        /// </summary>
        /// <param name="c">
        /// Cell whoms background color changed.
        /// </param>
        private void CellBGColorChanged_Helper(Cell c)
        {
            // Notify the ui that a cells background has changed, send the cell with the tag "BGColor".
            this.PropertyChanged(c, new PropertyChangedEventArgs("BGColor"));
        }

        /// <summary>
        /// To be called when the CellPropertyChanged event handler recieves flag "Reference".
        /// </summary>
        /// <param name="c">
        /// Cell whoms referenced cell changed.
        /// </param>
        private void CellReferenceChanged_Helper(Cell c)
        {
            // Clear all the cells references before updating to avoid floating references.
            c.ClearReferences();

            // (This should always be true)
            if (this.IsReferenceOrFormula(c.Text))
            {
                // This function will make appropriate PropertyChanged calls to the UI, and handle errors.
                this.UpdateNewReferenceOrFormula(c);
            }
            else
            {
                throw new Exception("ERROR: Floating reference. Cells reference changed but that reference was no longer in the formula.");
            }
        }

        /// <summary>
        /// To be called when the CellPropertyChanged event handler recieves flag "Text".
        /// </summary>
        /// <param name="c">
        /// Cell whoms text changed.
        /// </param>
        private void CellTextChanged_Helper(Cell c)
        {
            // Clear all the cells references before updating to avoid floating references.
            c.ClearReferences();

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

        // If a cells text has changed and the new text is a reference of formula, this method is called. The cells value will be changed appropriatly.
        private void UpdateNewReferenceOrFormula(Cell c)
        {
            string expression = c.Text;

            // Get rid of the = character at the start of the expression.
            expression = expression.Replace("=", " ");

            // If the expression contains prohibited characters automatically reject it.
            if (ExpressionTreeHelper.ContainsProhibitedCharacters(expression))
            {
                c.Value = "ERROR";

                this.PropertyChanged(c, new PropertyChangedEventArgs("Value"));

                return;
            }

            // New expression tree created from the expression
            ExpressionTree eTree = new ExpressionTree(expression);

            int numVarsFound = 0;

            // Get a list of the variables in the expression.
            string[] vars = ExpressionTreeHelper.GetSpreadsheetVariablesInExpression(expression, out numVarsFound);

            if (vars != null)
            {
                // Get the value of the cell each variable references and set the value within the tree.
                for (int i = 0; i < numVarsFound; i++)
                {
                    // Get the current variable we must set.
                    string var = vars[i];

                    // Make the variable a proper cell reference.
                    string newRef = "=" + var;

                    int row = 0;

                    int col = 0;

                    // Attempt to grab the index of the cell with the given reference.
                    if (this.GetCellIndexFromCellReference(newRef, out row, out col))
                    {
                        double referencedVal = 0;

                        Cell r = this.GetCell(row, col);

                        // Add the new cell to the list of this cells references.
                        c.AddReference(r);

                        // Attempt to parse the referenced cells value to a double, else set the value to 0 by default.
                        if (!double.TryParse(this.matrix[row, col].Value, out referencedVal))
                        {
                            referencedVal = 0;
                        }

                        // Set the proper variable in the tree.
                        eTree.SetVariable(var, referencedVal);
                    }
                    else
                    {
                        // If we could not grab an index for the reference the expression is invalid.
                        c.Value = "ERROR";

                        this.PropertyChanged(c, new PropertyChangedEventArgs("Value"));

                        return;
                    }
                }
            }

            // Evaluate the tree and set the sending cells value to the evaluation.
            c.Value = eTree.Evaluate().ToString();

            // Notify property changed to update the ui.
            this.PropertyChanged(c, new PropertyChangedEventArgs("Value"));
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

        // Returns true if the given cell text is a reference or formula.
        private bool IsReferenceOrFormula(string cellText)
        {
            if (cellText != null)
            {
                if (cellText.Length > 0 && cellText[0] == '=')
                {
                    return true;
                }
            }

            return false;
        }
    }
}