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
    using System.IO;
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

            // Undo and redo must be disabled to start.
            this.undoToolStripMenuItem.Enabled = false;
            this.redoToolStripMenuItem.Enabled = false;

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
            string[] args = e.PropertyName.Split(':');

            if (args[0] == "Value")
            {
                Cell c = (Cell)sender;

                this.CellValueChanged_Helper(c);
            }

            if (args[0] == "BGColor")
            {
                Cell c = (Cell)sender;

                this.CellBGColorChanged_Helper(c);
            }

            if (args[0] == "UndoStack")
            {
                this.UndoStackChanged_Helper(args);
            }

            if (args[0] == "RedoStack")
            {
                this.RedoStackChanged_Helper(args);
            }
        }

        private void UndoStackChanged_Helper(string[] args)
        {
            if (args[1] == "Populated")
            {
                this.undoToolStripMenuItem.Enabled = true;
                this.undoToolStripMenuItem.Text = "Undo | " + args[2];
            }

            if (args[1] == "Empty")
            {
                this.undoToolStripMenuItem.Enabled = false;
                this.undoToolStripMenuItem.Text = "Undo";
            }
        }

        private void RedoStackChanged_Helper(string[] args)
        {
            if (args[1] == "Populated")
            {
                this.redoToolStripMenuItem.Enabled = true;
                this.redoToolStripMenuItem.Text = "Redo | " + args[2];
            }

            if (args[1] == "Empty")
            {
                this.redoToolStripMenuItem.Enabled = false;
                this.redoToolStripMenuItem.Text = "Redo";
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
            string newText = (string)this.gridMain.Rows[c.RowIndex].Cells[c.ColumnIndex].Value;

            // Set the cells text through its command object.
            this.mainSpreadsheet.Remote.ExecuteCommand(new CellChangeTextCommand(c, newText));

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

                    // Set the cells color throught the spreadsheets command object.
                    this.mainSpreadsheet.Remote.ExecuteCommand(new CellChangeBGColorCommand(c, (uint)newColor.ToArgb()));
                }
            }
        }

        private void EditUndo_Click(object sender, EventArgs e)
        {
            this.mainSpreadsheet.Remote.UndoCommand();
        }

        private void EditRedo_Click(object sender, EventArgs e)
        {
            this.mainSpreadsheet.Remote.RedoCommand();
        }

        private void FileLoad_Click(object sender, EventArgs e)
        {
            Stream fs = this.GetStream_LoadXMLFile();

            if (fs != null)
            {
                // Clean out the main spreadsheet.
                this.mainSpreadsheet.Clean();

                // Load the data into the main spreadsheet.
                SpreadsheetFiles.Load(ref this.mainSpreadsheet, fs);

                // Clear all undo and redo stacks.
                this.mainSpreadsheet.Remote.Clear();

                // Clear toolstrip item.
                this.undoToolStripMenuItem.Text = "Undo";
                this.undoToolStripMenuItem.Enabled = false;

                // Clear toostrip item.
                this.redoToolStripMenuItem.Text = "Redo";
                this.redoToolStripMenuItem.Enabled = false;

                fs.Dispose();
            }
        }

        private void FileSave_Click(object sender, EventArgs e)
        {
            // Get a save file stream.
            Stream fs = this.GetStream_SaveXMLFile();

            if (fs != null)
            {
                // Save the spreadsheet data to the file.
                SpreadsheetFiles.Save(ref this.mainSpreadsheet, fs);

                fs.Dispose();
            }
        }

        // Opens a ColorDialog window, and allows the user to select a new color.
        private bool GetColorFromDialog(out Color col)
        {
            ColorDialog colDialog = new ColorDialog();

            // Update the text box color if the user clicks OK.
            if (colDialog.ShowDialog() == DialogResult.OK)
            {
                col = colDialog.Color;

                return true;
            }

            col = Color.White;

            return false;
        }

        private Stream GetStream_LoadXMLFile()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            // Set the filter to only give the option of opening text files
            fileDialog.Filter = "XML Files (.xml)|*.xml";

            // Dont allow the the user to select multiple files
            fileDialog.Multiselect = false;

            // Capture the result of the users action in the file explorer
            DialogResult dialogResult = fileDialog.ShowDialog();

            // If the user ended up selecting a file do the stream operations to the textbox
            if (dialogResult == DialogResult.OK)
            {
                Stream s = fileDialog.OpenFile();

                if (s != null)
                {
                    return s;
                }
            }

            return null;
        }

        private Stream GetStream_SaveXMLFile()
        {
            SaveFileDialog fileDialog = new SaveFileDialog();

            // Set the filter to only give the option of opening text files
            fileDialog.Filter = "XML Files (.xml)|*.xml";

            // Capture the result of the users action in the file explorer
            DialogResult dialogResult = fileDialog.ShowDialog();

            // If the user ended up selecting a file do the stream operations to the textbox
            if (dialogResult == DialogResult.OK)
            {
                Stream s = fileDialog.OpenFile();

                if (s != null)
                {
                    return s;
                }
            }

            return null;
        }
    }
}
