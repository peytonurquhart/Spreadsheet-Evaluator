// <copyright file="SpreadsheetFiles.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CptS321
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Linq;

    /// <summary>
    /// Static methods for loading a new spreadsheet from a file.
    /// </summary>
    public static class SpreadsheetFiles
    {
        /// <summary>
        /// Load the data from xml file stream "fileStream" into the referenced spreadsheet.
        /// </summary>
        /// <param name="sheet">
        /// A spreadsheet to load to.
        /// </param>
        /// <param name="fileStream">
        /// A spreadsheet xml cell filestream to load from.
        /// </param>
        public static void Load(ref Spreadsheet sheet, Stream fileStream)
        {
            using (XmlReader reader = XmlReader.Create(fileStream))
            {
                while (reader.Read())
                {
                    // If we have found a cell..
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "cell")
                    {
                        Cell ctemp = null;

                        string cellRefString = "=" + reader.GetAttribute("name");

                        int row = -1, col = -1;

                        if (sheet.GetCellIndexFromCellReference(cellRefString, out row, out col))
                        {
                            ctemp = sheet.GetCell(row, col);

                            // Get a subtree under the cell element.
                            XmlReader sub = reader.ReadSubtree();

                            if (sub != null)
                            {
                                // Pass in a subtree of all the elements under this cell, and parse it in a separate function.
                                SpreadsheetFiles.XML_ParseCell(sub, ctemp);
                            }
                            else
                            {
                                throw new Exception("ERROR: Could not parse XML subtree on load");
                            }
                        }
                        else
                        {
                            throw new Exception("ERROR: Save file format invalid, cell: " + cellRefString + " unacceptable");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Saves spreadsheet data to a .xml file for later use at the location pointed to by fileStream.
        /// </summary>
        /// <param name="sheet">
        /// Reference to a working spreadsheet to save.
        /// </param>
        /// <param name="fileStream">
        /// Filestream representing an open file for saving.
        /// </param>
        public static void Save(ref Spreadsheet sheet, Stream fileStream)
        {
            // Obtain all the populated cells from the spreadsheet.
            List<Cell> cl = sheet.GetPopulatedCells();

            XDocument newDoc = new XDocument();

            // Create the root spreadsheet element.
            XElement xRoot = new XElement("spreadsheet");

            // For each populated cell in the list
            foreach (Cell c in cl)
            {
                // Get the proper cell name.
                string cellName = sheet.GetCellName(c);

                // Add a new cell under the root element
                xRoot.Add(SpreadsheetFiles.XML_CreateCellElement(cellName, c.BGColor.ToString(), c.Text));
            }

            // Add the root with all the cells attached.
            newDoc.Add(xRoot);

            // Save the new document.
            newDoc.Save(fileStream);
        }

        /// <summary>
        /// Creates a cell element to be attached to a root spreadsheet element.
        /// </summary>
        private static XElement XML_CreateCellElement(string name, string bgcolor, string text)
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

        private static void XML_ParseCell(XmlReader subtree, Cell currentCell)
        {
            while (subtree.Read())
            {
                switch (subtree.NodeType)
                {
                    case XmlNodeType.Element:

                        if (subtree.Name == "bgcolor")
                        {
                            string color = subtree.ReadElementContentAsString();

                            currentCell.BGColor = uint.Parse(color);
                        }

                        if (subtree.Name == "text")
                        {
                            currentCell.Text = subtree.ReadElementContentAsString();
                        }

                        break;
                }
            }
        }
    }
}
