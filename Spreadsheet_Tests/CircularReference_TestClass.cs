// <copyright file="CircularReference_TestClass.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Spreadsheet_Tests
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using CptS321;
    using NUnit.Framework;

    /// <summary>
    /// Class for testing solutions to the circular reference problem.
    /// </summary>
    [TestFixture]
    public class CircularReference_TestClass
    {
        /// <summary>
        /// Circular reference test 1.
        /// </summary>
        [Test]
        public void CR_Test1()
        {
            CellTest a1 = new CellTest("A1");

            CellTest b1 = new CellTest("B1");

            a1.AddReference(b1);

            b1.AddReference(a1);

            Assert.True(a1.CheckCircularReferences());

            Assert.True(b1.CheckCircularReferences());
        }

        /// <summary>
        /// Circular reference test 2.
        /// </summary>
        [Test]
        public void CR_Test2()
        {
            CellTest a1 = new CellTest("A1");

            CellTest b1 = new CellTest("B1");

            CellTest c1 = new CellTest("C1");

            CellTest d2 = new CellTest("D2");

            CellTest a4 = new CellTest("A4");

            a1.AddReference(b1);

            b1.AddReference(c1);
            b1.AddReference(d2);

            d2.AddReference(a4);

            // there should be no circular references here.
            Assert.False(a1.CheckCircularReferences());
            Assert.False(b1.CheckCircularReferences());
            Assert.False(c1.CheckCircularReferences());
            Assert.False(d2.CheckCircularReferences());
            Assert.False(a4.CheckCircularReferences());

            // this should now create a circular reference.
            a4.AddReference(a1);

            Assert.True(a1.CheckCircularReferences());

            // all of these also have a circular reference.
            Assert.True(b1.CheckCircularReferences());
            Assert.True(d2.CheckCircularReferences());
            Assert.True(a4.CheckCircularReferences());

            // c1 shouldnt have a problem.
            Assert.False(c1.CheckCircularReferences());
        }

        /// <summary>
        /// Simplified cell class for testing.
        /// </summary>
        private class CellTest
        {
            // the name of the cell.
            private string name;

            private List<CellTest> references = new List<CellTest>();

            public CellTest(string name)
            {
                this.name = name;
            }

            // Gets the name
            public string Name
            {
                get
                {
                    return this.name;
                }
            }

            // Adds a reference
            public void AddReference(CellTest c)
            {
                this.references.Add(c);
            }

            // Calls recursive checkcircularreferences.
            public bool CheckCircularReferences()
            {
                return this.CheckCircularReferenceR(this);
            }

            protected bool CheckCircularReferenceR(CellTest check)
            {
                // Base case, the cell has no references, there are no circular references for this cell.
                if (this.references.Count == 0)
                {
                    return false;
                }
                else if (this.references.Contains(check))
                {
                    return true;
                }
                else
                {
                    bool f = false;

                    foreach (CellTest c in this.references)
                    {
                        f = c.CheckCircularReferenceR(check);

                        if (f == true)
                        {
                            return true;
                        }
                    }

                    return f;
                }
            }
        }
    }
}
