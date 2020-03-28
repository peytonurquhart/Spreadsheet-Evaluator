// <copyright file="Application.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// Peyton Urquhart, 11622450

namespace Spreadsheet_Peyton_Urquhart
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using CptS321;

    /// <summary>
    /// Class to hold the event handlers for the spreadsheet application.
    /// </summary>
    public partial class Application : Form
    {
        private Spreadsheet mainSpreadsheet = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Application"/> class.
        /// Creates 26 columns (A - Z) and 50 rows (1 - 50).
        /// </summary>
        public Application()
        {
            this.InitializeComponent();

            // Add 26 columns labeled A-Z
            for (int i = 65; i < (65 + 26); i++)
            {
                this.gridMain.Columns.Add(((char)i).ToString(), ((char)i).ToString());
            }

            // Add 50 rows labeled 1 - 50
            for (int i = 1; i < 51; i++)
            {
                this.gridMain.Rows.Add();
                this.gridMain.Rows[i - 1].HeaderCell.Value = i.ToString();
            }

            // Initialize a spreadsheet engine object with 50 rows and 26 columns and the Basic cell type
            this.mainSpreadsheet = new Spreadsheet(50, 26, Spreadsheet.CellType.Basic);

            // Subscribe to the spreadsheets propertychanged event
            this.mainSpreadsheet.PropertyChanged += this.SpreadsheetPropertyChanged;
        }

        /// <summary>
        /// When a cell in the spreadsheet changes value, the cell that changed is passed through as the sender. e will be "Value".
        /// The value of the UI will be updated.
        /// </summary>
        private void SpreadsheetPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Value")
            {
                Cell c = (Cell)sender;

                // Update the corresponding UI cell to match the value in the Engine.
                this.gridMain.Rows[c.RowIndex].Cells[c.ColumnIndex].Value = c.Value;
            }
        }

        // Updates a cell to display its text property as opposed to its value when the user begins editing it.
        private void GridMain_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // Get the appropriate engine cell from the spreadsheet.
            Cell c = this.mainSpreadsheet.GetCell(e.RowIndex, e.ColumnIndex);

            // Make sure the spreadsheet shows the engine cells text property when editing.
            this.gridMain.Rows[c.RowIndex].Cells[c.ColumnIndex].Value = c.Text;
        }

        // Retruns a cell to displaying its value property when the user stops editing it.
        private void GridMain_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Get the cell that the user is done editing.
            Cell c = this.mainSpreadsheet.GetCell(e.RowIndex, e.ColumnIndex);

            // Set the cells text property to the new text the user entered (if its a reference or formula it will automatically update)
            c.Text = (string)this.gridMain.Rows[c.RowIndex].Cells[c.ColumnIndex].Value;

            // Now that the new cells value is updated update the ui again.
            this.gridMain.Rows[c.RowIndex].Cells[c.ColumnIndex].Value = c.Value;
        }

        /// <summary>
        /// Runs a quick demo of the UI updating based on the spreadsheet engine.
        /// </summary>
        private void DemoButton_Click(object sender, EventArgs e)
        {
            var rand = new Random();

            // Set 100 random cells to "Hello world!"
            for (int i = 0; i < 100; i++)
            {
                // Get two random column and row indeces
                int c = rand.Next() % 26;
                int r = rand.Next() % 50;

                // Get a cell with the random indeces
                Cell cell = this.mainSpreadsheet.GetCell(r, c);

                // Set the cells text
                cell.Text = "hello world!";
            }

            // Label all the B cells with text
            for (int i = 0; i < 50; i++)
            {
                Cell cell = this.mainSpreadsheet.GetCell(i, 1);

                string message = "This is B";

                message += (i + 1).ToString();

                cell.Text = message;
            }

            // Put a reference to all the B cells in the corresponding A cells
            for (int i = 0; i < 50; i++)
            {
                Cell cell = this.mainSpreadsheet.GetCell(i, 0);

                string message = "=B";

                message += (i + 1).ToString();

                cell.Text = message;
            }
        }
    }
}
