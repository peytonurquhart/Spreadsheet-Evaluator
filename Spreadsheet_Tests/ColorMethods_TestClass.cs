// <copyright file="ColorMethods_TestClass.cs" company="PlaceholderCompany">
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
    /// Class for tests regarding use of the .NET Color class.
    /// </summary>
    [TestFixture]
    public class ColorMethods_TestClass
    {
        /// <summary>
        /// Test for manipulating cell colors.
        /// </summary>
        [Test]
        public void GetColor_Test1()
        {
            ColoredCell c = new ColoredCell();

            c.BGColor = 0xFFFF0000;

            Color color = Color.FromArgb((int)c.BGColor);

            int a = (int)c.BGColor;

            // Test that the color is being instantiated correctly from the Cell.BGColor property.
            Assert.AreEqual(a, color.ToArgb());
        }

        private class ColoredCell
        {
            private uint bgColor = 0xFFFFFFFF;

            public uint BGColor
            {
                get
                {
                    return this.bgColor;
                }

                set
                {
                    this.bgColor = value;
                }
            }
        }
    }
}
