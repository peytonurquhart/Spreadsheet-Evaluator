// <copyright file="Cell_TestClass.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// Peyton Urquhart, 11622450

namespace Spreadsheet_Tests
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using NUnit.Framework;

    /// <summary>
    /// Class for implementing cell tests.
    /// </summary>
    [TestFixture]
    public class Cell_TestClass
    {
        private CellTest_Test[] matrix = null;

        private CellTest_Test cell1 = null;

        private bool eventFlag = false;

        /// <summary>
        /// Test method for instantiating an abstract cell object through an encapsulating basiccell class.
        /// </summary>
        [Test]
        public void SingleInstantiation_Test()
        {
            // Instantiate a new basic cell with the given row and column indeces
            this.cell1 = new BasicCell_Test(1, "A");

            // Ensure that these were correctly set and are able to be retrieved by the properties.
            Assert.AreEqual(1, this.cell1.RowIndex);

            Assert.AreEqual("A", this.cell1.ColumnIndex);

            this.cell1.Text = "hello";

            Assert.AreEqual("hello", this.cell1.Text);
        }

        /// <summary>
        /// Test method for instantiating an abstract cell object matrix through an encapsulating basiccell class.
        /// </summary>
        [Test]
        public void MatrixInstantiation_Test()
        {
            this.matrix = new BasicCell_Test[10];

            // Call constructor for all of the cells in the matrix.
            for (int i = 0; i < 10; i++)
            {
                this.matrix[i] = new BasicCell_Test(i + 1, ((char)(i + 65)).ToString());
            }

            Assert.AreEqual(10, this.matrix[9].RowIndex);

            Assert.AreEqual("J", this.matrix[9].ColumnIndex);
        }

        /// <summary>
        /// Test method for the property changed event handler for the cell class.
        /// </summary>
        [Test]
        public void PropertyChangedEvent_Test()
        {
            this.cell1 = new BasicCell_Test(1, "A");

            // subscribe cell1 to Cell1_PropertyChanged().
            this.cell1.PropertyChanged += this.Cell1_PropertyChanged;

            // change the property.
            this.cell1.Text = "hello";

            // ensure that the event handler was called and the flag was set to true.
            Assert.True(this.eventFlag);
        }

        /// <summary>
        /// this method should be called whenever the text property is changed within cells which subscribe to it.
        /// </summary>
        private void Cell1_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Text")
            {
                this.eventFlag = true;
            }
        }
    }
}