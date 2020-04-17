// <copyright file="XML_Writing_TestClass.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Spreadsheet_Tests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Xml;
    using System.Xml.Linq;
    using CptS321;
    using NUnit.Framework;

    /// <summary>
    /// Test class for operations regarding saving data to an XML file.
    /// </summary>
    [TestFixture]
    public class XML_Writing_TestClass
    {
        /// <summary>
        /// Test method for creating a new xml file and writing to it.
        /// </summary>
        [Test]
        public void XMLWriter_Test1()
        {
            string fp = "C:/Users/Peyton/Documents/Visual Studio 2015/Projects/Spreadsheet_Peyton_Urquhart/Spreadsheet_Tests/testwrite.xml";

            Stream s = this.OpenFile(fp, FileMode.Create);

            XDocument newDoc = new XDocument();

            // Create the root spreadsheet element.
            XElement xRoot = new XElement("spreadsheet");

            // Add a new cell under the root element
            xRoot.Add(this.XML_CreateCellElement("A1", "blue", "this is cell A1"));

            // Add another new cell under the root.
            xRoot.Add(this.XML_CreateCellElement("A2", "red", "this is cell A2"));

            // Add the rot
            newDoc.Add(xRoot);

            // Save the new document.
            newDoc.Save(s);

            // Close the stream.
            s.Dispose();

            // Open the file for reading.
            using (TextReader t = File.OpenText(fp))
            {
                t.ReadLine();

                // Ensure the first cell was compiled correctly + startroot.
                Assert.AreEqual("<spreadsheet>", t.ReadLine());
                Assert.AreEqual("  <cell name=\"A1\">", t.ReadLine());
                Assert.AreEqual("    <bgcolor>blue</bgcolor>", t.ReadLine());
                Assert.AreEqual("    <text>this is cell A1</text>", t.ReadLine());
                Assert.AreEqual("  </cell>", t.ReadLine());

                // Ensure the second cell was compiled correctly + endroot.
                Assert.AreEqual("  <cell name=\"A2\">", t.ReadLine());
                Assert.AreEqual("    <bgcolor>red</bgcolor>", t.ReadLine());
                Assert.AreEqual("    <text>this is cell A2</text>", t.ReadLine());
                Assert.AreEqual("  </cell>", t.ReadLine());
                Assert.AreEqual("</spreadsheet>", t.ReadLine());
            }
        }

        private XElement XML_CreateCellElement(string name, string bgcolor, string text)
        {
            XElement newCell = new XElement("cell", new XAttribute("name", name));

            // Create bgcolor element
            XElement col = new XElement("bgcolor");
            col.Value = bgcolor;

            // Create text element.
            XElement tex = new XElement("text");
            tex.Value = text;

            // Add elements to the cell element.
            newCell.Add(col, tex);

            return newCell;
        }

        private Stream OpenFile(string path, FileMode fm = FileMode.Open)
        {
            FileStream s = File.Open(path, fm);

            if (s == null)
            {
                throw new Exception("ERROR: Could not open file: " + path);
            }

            return s;
        }

        /// <summary>
        /// Struct for cell attributes.
        /// </summary>
        private class CellAttributes
        {
            private string name = null;
            private string color = null;
            private string text = null;

            /// <summary>
            /// Gets or sets the cells name.
            /// </summary>
            public string Name
            {
                get
                {
                    return this.name;
                }

                set
                {
                    this.name = value;
                }
            }

            /// <summary>
            /// Gets or sets the cells color.
            /// </summary>
            public string Color
            {
                get
                {
                    return this.color;
                }

                set
                {
                    this.color = value;
                }
            }

            /// <summary>
            /// Gets or sets the cells value.
            /// </summary>
            public string Text
            {
                get
                {
                    return this.text;
                }

                set
                {
                    this.text = value;
                }
            }
        }
    }
}
