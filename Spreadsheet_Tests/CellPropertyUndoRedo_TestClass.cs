// <copyright file="CellPropertyUndoRedo_TestClass.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Spreadsheet_Tests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using CptS321;
    using NUnit.Framework;

    /// <summary>
    /// Test class for the cell property undo / redo command objects.
    /// </summary>
    [TestFixture]
    public class CellPropertyUndoRedo_TestClass
    {
        /// <summary>
        /// Interface for a command object.
        /// </summary>
        public interface ICommand
        {
            /// <summary>
            /// Execute command.
            /// </summary>
            void Execute();

            /// <summary>
            /// UnExecute comman.
            /// </summary>
            void UnExecute();
        }

        /// <summary>
        /// A test for the cell command objects.
        /// </summary>
        [Test]
        public void CellCommands_Test1()
        {
            CellRemote remote = new CellRemote();

            Cell c1 = new BasicCell(1, 1);

            c1.Text = "abcd";
            Assert.AreEqual("abcd", c1.Text);

            // This should set c1's text to "this is new text"
            remote.ExecuteCommand(new CellChangeTextCommand(c1, "this is new text"));
            Assert.AreEqual("this is new text", c1.Text);

            // This should now revert c1s text back to what it was: "abcd"
            remote.UndoCommand();
            Assert.AreEqual("abcd", c1.Text);

            // This should not do anything because there is nothing else to undo.
            remote.UndoCommand();
            Assert.AreEqual("abcd", c1.Text);

            // This should now revert back to what the text was before the first undo.
            remote.RedoCommand();
            Assert.AreEqual("this is new text", c1.Text);
        }

        /// <summary>
        /// A test for multiple cell command objects.
        /// </summary>
        [Test]
        public void CellCommands_Test2()
        {
            CellRemote remote = new CellRemote();

            Cell c1 = new BasicCell(1, 1);
            Cell c2 = new BasicCell(2, 2);

            c1.BGColor = 1;
            c2.Text = "first text";

            remote.ExecuteCommand(new CellChangeBGColorCommand(c1, 33));
            remote.ExecuteCommand(new CellChangeTextCommand(c2, "abc"));
            Assert.AreEqual(33, c1.BGColor);
            Assert.AreEqual("abc", c2.Text);

            // Change c2's text to def through command
            remote.ExecuteCommand(new CellChangeTextCommand(c2, "def"));
            Assert.AreEqual("def", c2.Text);

            // this shoudl revert c2's text back to abc.
            remote.UndoCommand();
            Assert.AreEqual("abc", c2.Text);

            // this should change c2's text back to "first text", should not change c1's color yet
            remote.UndoCommand();
            Assert.AreEqual("first text", c2.Text);
            Assert.AreEqual(33, c1.BGColor);

            // this should change c1's color back to '1'.
            remote.UndoCommand();
            Assert.AreEqual(1, c1.BGColor);

            // this should change everything back to its most recent state. (33 and "def").
            remote.RedoCommand();
            remote.RedoCommand();
            remote.RedoCommand();
            Assert.AreEqual(33, c1.BGColor);
            Assert.AreEqual("def", c2.Text);
        }

        private class CellRemote
        {
            private Stack<ICommand> undoStack = new Stack<ICommand>();

            private Stack<ICommand> redoStack = new Stack<ICommand>();

            // Changes a cells property and pushes the change command to the undoStack.
            public void ExecuteCommand(ICommand c)
            {
                c.Execute();

                this.undoStack.Push(c);
            }

            public void UndoCommand()
            {
                if (this.undoStack.Count > 0)
                {
                    ICommand old = this.undoStack.Pop();

                    old.UnExecute();

                    this.redoStack.Push(old);
                }
            }

            public void RedoCommand()
            {
                if (this.redoStack.Count > 0)
                {
                    ICommand old = this.redoStack.Pop();

                    old.Execute();

                    this.undoStack.Push(old);
                }
            }
        }

        /// <summary>
        /// Execute() changes the text of the cell to new text. UnExecute() reverts the cells text back to its previous text.
        /// </summary>
        private class CellChangeTextCommand : ICommand
        {
            // A user friendly tag to describe what the command does.
            private string tag = "Change Cell Text";

            private Cell cellRef = null;

            private string oldText = null;

            private string newText = null;

            public CellChangeTextCommand(Cell c, string newText)
            {
                this.cellRef = c;
                this.oldText = c.Text;
                this.newText = newText;
            }

            void ICommand.Execute()
            {
                this.cellRef.Text = this.newText;
            }

            void ICommand.UnExecute()
            {
                this.cellRef.Text = this.oldText;
            }
        }

        /// <summary>
        /// Execute() changes the cells color to newColor. UnExecute reverts the cells color to its previous color.
        /// </summary>
        private class CellChangeBGColorCommand : ICommand
        {
            // A user friendly tag to describe what the command does.
            private string tag = "Change Cell Color";

            private Cell cellRef = null;

            private uint oldColor;

            private uint newColor;

            public CellChangeBGColorCommand(Cell c, uint newColor)
            {
                this.cellRef = c;
                this.oldColor = c.BGColor;
                this.newColor = newColor;
            }

            void ICommand.Execute()
            {
                this.cellRef.BGColor = this.newColor;
            }

            void ICommand.UnExecute()
            {
                this.cellRef.BGColor = this.oldColor;
            }
        }
    }
}
