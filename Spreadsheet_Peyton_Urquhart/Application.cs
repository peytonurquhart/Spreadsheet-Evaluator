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

                this.CellValueChanged_Helper(c);
            }

            if (e.PropertyName == "BGColor")
            {
                Cell c = (Cell)sender;

                this.CellBGColorChanged_Helper(c);
            }
        }

        /// <summary>
        /// To be called when SpreadSheetPropertyChanged event gets flag "Value".
        /// </summary>
        /// <param name="c">
        /// Cell whoms value changed.
        /// </param>
        private void CellValueChanged_Helper(Cell c)
        {
            // Update the corresponding UI cell to match the value in the Engine.
            this.gridMain.Rows[c.RowIndex].Cells[c.ColumnIndex].Value = c.Value;
        }

        /// <summary>
        /// To be called when SpreadSheetPropertyChanged event gets flag "BGColor".
        /// </summary>
        /// <param name="c">
        /// Cell whoms background color changed.
        /// </param>
        private void CellBGColorChanged_Helper(Cell c)
        {
            Color newColor = Color.FromArgb((int)c.BGColor);

            this.gridMain.Rows[c.RowIndex].Cells[c.ColumnIndex].Style.BackColor = newColor;
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

        // When the user selects Format->Cells->Change Color this method is invoked.
        private void FormatCellsChangeColor_Click(object sender, EventArgs e)
        {
            Color newColor;

            // If the user selected a new color, update the cells.
            if (this.GetColorFromDialog(out newColor))
            {
                DataGridViewSelectedCellCollection selectedCells = this.gridMain.SelectedCells;

                // For each cell that was selected..
                for (int i = 0; i < selectedCells.Count; i++)
                {
                    // Get the corresponding spreadsheet cell.
                    Cell c = this.mainSpreadsheet.GetCell(selectedCells[i].RowIndex, selectedCells[i].ColumnIndex);

                    // Set the cell to the color the user chose
                    c.BGColor = (uint)newColor.ToArgb();
                }
            }
        }

        // Opens a ColorDialog window, and allows the user to select a new color.
        private bool GetColorFromDialog(out Color col)
        {
            ColorDialog colDialog = new ColorDialog();

            // Update the text box color if the user clicks OK 
            if (colDialog.ShowDialog() == DialogResult.OK)
            {
                col = colDialog.Color;

                return true;
            }

            col = Color.White;

            return false;
        }
    }
}
