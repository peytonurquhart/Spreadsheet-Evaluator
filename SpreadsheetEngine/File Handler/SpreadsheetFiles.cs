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

                            currentCell.BGColor = uint.Parse(color, System.Globalization.NumberStyles.HexNumber);
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
