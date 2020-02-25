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

    /// <summary>
    /// Class to hold the event handlers for the spreadsheet application.
    /// </summary>
    public partial class Application : Form
    {
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
                char a = (char)i;
                this.gridMain.Columns.Add(a.ToString(), a.ToString());
            }

            // Add 50 rows labeled 1 - 50
            for (int i = 1; i < 51; i++)
            {
                this.gridMain.Rows.Add();
                this.gridMain.Rows[i - 1].HeaderCell.Value = i.ToString();
            }
        }
    }
}
