// <copyright file="XML_Reading_TestClass.cs" company="PlaceholderCompany">
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
    using CptS321;
    using NUnit.Framework;

    /// <summary>
    /// Test class for reading XML documents using .NET libraries.
    /// </summary>
    [TestFixture]
    public class XML_Reading_TestClass
    {
        /// <summary>
        /// Test for opening and parsing an xml file.
        /// </summary>
        [Test]
        public void XMLReader_Test1()
        {
            string startElements = null;
            string endElements = null;
            string textElements = null;
            string cellNames = null;

            // Input file path here.
            string fp = "C:/Users/Peyton/Documents/Visual Studio 2015/Projects/Spreadsheet_Peyton_Urquhart/Spreadsheet_Tests/testfile.xml";

            Stream s = this.OpenFileR(fp);

            using (XmlReader reader = XmlReader.Create(s))
            {
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:

                            startElements += reader.Name + " ";

                            // The reader should always have attributes in this format
                            if (reader.Name == "cell" && reader.HasAttributes)
                            {
                                cellNames += reader.GetAttribute("name") + " ";
                            }

                            break;
                        case XmlNodeType.EndElement:
                            endElements += reader.Name + " ";
                            break;
                        case XmlNodeType.Text:
                            textElements += reader.Value + " ";
                            break;
                        default:
                            break;
                    }
                }
            }

            // Check that different types of attributes were read correctly.
            Assert.AreEqual("spreadsheet cell bgcolor text cell bgcolor text ", startElements);
            Assert.AreEqual("bgcolor text cell bgcolor text cell spreadsheet ", endElements);
            Assert.AreEqual("blue cell1 red cell2 ", textElements);
            Assert.AreEqual("B1 B2 ", cellNames);

            s.Dispose();
        }

        /// <summary>
        /// Test for parsing data within xml nodes to a type.
        /// </summary>
        [Test]
        public void XMLReader_Test2()
        {
            CellAttributes b1 = new CellAttributes();
            CellAttributes b2 = new CellAttributes();
            CellAttributes b3 = new CellAttributes();

            // Input file path here.
            string fp = "C:/Users/Peyton/Documents/Visual Studio 2015/Projects/Spreadsheet_Peyton_Urquhart/Spreadsheet_Tests/testfile2.xml";

            Stream s = this.OpenFileR(fp);

            using (XmlReader reader = XmlReader.Create(s))
            {
                while (reader.Read())
                {
                    // If we have found a cell..
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "cell")
                    {
                        CellAttributes ctemp = null;

                        // FIND A MATCHING SPREADSHEET CELL FOR THE SAVED DATA
                        // Here we would get the cell from the spreadsheet based on the name.
                        if (reader.GetAttribute("name") == "B1")
                        {
                            ctemp = b1;
                        }
                        else if (reader.GetAttribute("name") == "B2")
                        {
                            ctemp = b2;
                        }
                        else if (reader.GetAttribute("name") == "B3")
                        {
                            ctemp = b3;
                        }
                        else
                        {
                            throw new Exception("ERROR: Unnamed cell");
                        }

                        // Set the name of the cell reference to Xml.Node.Attribute.Name (the cell).
                        ctemp.Name = reader.GetAttribute("name");

                        // Get a subtree under the cell element.
                        XmlReader sub = reader.ReadSubtree();

                        // Make sure teh subtree is not null.
                        Assert.NotNull(sub);

                        // Pass in a subtree of all the elements under this cell, and parse it in a separate function.
                        this.XML_ParseCell(sub, ctemp);
                    }
                }
            }

            // Check if the cell properties were set correctly.
            Assert.AreEqual("B1", b1.Name);
            Assert.AreEqual("blue", b1.Color);
            Assert.AreEqual("cell1", b1.Text);

            Assert.AreEqual("B2", b2.Name);
            Assert.AreEqual("red", b2.Color);
            Assert.AreEqual("cell2", b2.Text);

            Assert.AreEqual("B3", b3.Name);
            Assert.AreEqual("purple", b3.Color);
            Assert.AreEqual("cell3", b3.Text);

            s.Dispose();
        }

        private void XML_ParseCell(XmlReader subtree, CellAttributes currentCell)
        {
            while (subtree.Read())
            {
                switch (subtree.NodeType)
                {
                    case XmlNodeType.Element:

                        if (subtree.Name == "bgcolor")
                        {
                            currentCell.Color = subtree.ReadElementContentAsString();
                        }

                        if (subtree.Name == "text")
                        {
                            currentCell.Text = subtree.ReadElementContentAsString();
                        }

                        break;
                }
            }
        }

        private Stream OpenFileR(string path)
        {
            FileStream s = File.Open(path, FileMode.Open);

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
