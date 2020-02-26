// <copyright file="CellTest_Test.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// Peyton Urquhart, 11622450

namespace Spreadsheet_Tests
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Cell test class.
    /// </summary>
    public abstract class CellTest_Test : INotifyPropertyChanged
    {
        /// <summary>
        /// Represents the text inside the cell.
        /// </summary>
        protected string mText = null;

        /// <summary>
        /// Represents the value of the cell. Is equal to 'Text' if the text does not start with the = character.
        /// </summary>
        protected string mValue = null;

        /// <summary>
        /// Represents the rowIndex for the cell.
        /// </summary>
        private int rowIndex = -1;

        /// <summary>
        /// Represents the columnIndex for the cell.
        /// </summary>
        private string columnIndex = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="CellTest_Test"/> class.
        /// </summary>
        /// <param name="columnIndex">
        /// The column index for the cell. Read only.
        /// </param>
        /// <param name="rowIndex">
        /// The row index for the cell. Read only.
        /// </param>
        public CellTest_Test(int rowIndex, string columnIndex)
        {
            this.rowIndex = rowIndex;
            this.columnIndex = columnIndex;
        }

        /// <summary>
        /// PropertyChanged event handler for the cell class. (A list of delegates) '(sender, e) => { }' is alternate syntax for 'delegate'.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// Gets rowIndex member.
        /// </summary>
        public int RowIndex
        {
            get
            {
                return this.rowIndex;
            }
        }

        /// <summary>
        /// Gets columnIndex member.
        /// </summary>
        public string ColumnIndex
        {
            get
            {
                return this.columnIndex;
            }
        }

        /// <summary>
        /// Gets or sets mText. Notifies the event handler if there is a change.
        /// </summary>
        public string Text
        {
            get
            {
                return this.mText;
            }

            set
            {
                if (value == this.mText)
                {
                    return;
                }

                this.mText = value;

                this.PropertyChanged(this, new PropertyChangedEventArgs("Text"));
            }
        }

        /// <summary>
        /// Gets or sets mValue, public get so anybody can obtain the value, private set so it can only be set under certain conditions.
        /// Represents either the text in the cell, or an evaluation of the formula that is typed in the cell (if the text starts with an '=').
        /// </summary>
        public string Value
        {
            // The outside world *should* be able to get this property.
            get
            {
                return this.mValue;
            }

            // The outside world should never be able to set this property, so its access modifier must be more restrictive.
            protected set
            {
                if (value == this.mValue)
                {
                    return;
                }

                this.mValue = value;
            }
        }
    }
}
