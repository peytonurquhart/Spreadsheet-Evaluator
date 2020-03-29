// <copyright file="CellReferenceEvents_TestClass.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Spreadsheet_Tests
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using CptS321;
    using NUnit.Framework;

    /// <summary>
    /// Class for testing cell reference events.
    /// </summary>
    [TestFixture]
    public class CellReferenceEvents_TestClass
    {
        private bool eventFlag = false;

        /// <summary>
        /// Test for a cell subscribing to another cells propertychanged event.
        /// </summary>
        [Test]
        public void CellToCellSubscription_Test1()
        {
            Cell subscriber = new BasicCell(1, 1);
            Cell a1 = new BasicCell(2, 2);
            Cell b2 = new BasicCell(3, 3);
            Cell c3 = new BasicCell(4, 4);

            subscriber.PropertyChanged += this.CellPropertyChanged;
            a1.PropertyChanged += this.CellPropertyChanged;
            b2.PropertyChanged += this.CellPropertyChanged;
            c3.PropertyChanged += this.CellPropertyChanged;

            a1.Text = "cell A1";
            b2.Text = "cell B2";
            c3.Text = "cell C3";

            a1.PropertyChanged += subscriber.ReferenceCellPropertyChanged;
            b2.PropertyChanged += subscriber.ReferenceCellPropertyChanged;
            c3.PropertyChanged += subscriber.ReferenceCellPropertyChanged;

            this.eventFlag = false;

            Assert.False(this.eventFlag);

            a1.Text = "newtext";

            Assert.True(this.eventFlag);
        }

        /// <summary>
        /// Test for a cell subscribing to another cells propertychanged event.
        /// </summary>
        [Test]
        public void CellReferenceList_Test1()
        {
            Cell subscriber = new BasicCell(1, 1);
            Cell a1 = new BasicCell(2, 2);
            Cell b2 = new BasicCell(3, 3);
            Cell c3 = new BasicCell(4, 4);

            subscriber.PropertyChanged += this.CellPropertyChanged;
            a1.PropertyChanged += this.CellPropertyChanged;
            b2.PropertyChanged += this.CellPropertyChanged;
            c3.PropertyChanged += this.CellPropertyChanged;

            a1.Text = "cell A1";
            b2.Text = "cell B2";
            c3.Text = "cell C3";

            subscriber.AddReference(a1);
            subscriber.AddReference(b2);
            subscriber.AddReference(c3);

            this.eventFlag = false;

            Assert.False(this.eventFlag);

            a1.Text = "newtext";

            Assert.True(this.eventFlag);
        }

        // This method should be called with propertyname "Reference" when a subscribing cells referenced cell changes.
        private void CellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Reference")
            {
                this.eventFlag = true;
            }
            else if (e.PropertyName == "Text")
            {
                Cell c = (Cell)sender;

                c.Value = c.Text;
            }
        }
    }
}
