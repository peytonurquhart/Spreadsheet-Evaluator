// <copyright file="CellRemote.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CptS321
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Remote to handle cell changes and updates, keeping track of undo and redo data.
    /// </summary>
    public class CellRemote : INotifyPropertyChanged
    {
        private Stack<ICommand> undoStack = new Stack<ICommand>();

        private Stack<ICommand> redoStack = new Stack<ICommand>();

        /// <summary>
        /// PropertyChanged notifier for cell remote.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// Gets the number of items in the undo stack.
        /// </summary>
        public int UndoStackCount
        {
            get
            {
                return this.undoStack.Count;
            }
        }

        /// <summary>
        /// Gets the number of items in the redo stack.
        /// </summary>
        public int RedoStackCount
        {
            get
            {
                return this.redoStack.Count;
            }
        }

        /// <summary>
        /// Executes a given command that implements the ICommand interface.
        /// </summary>
        /// <param name="c">
        /// A command to be executed.
        /// </param>
        public void ExecuteCommand(ICommand c)
        {
            c.Execute();

            // Push the command to the undo stack for later undo, notify the spreadsheet that the undo stack is populated.
            this.undoStack.Push(c);
            this.PropertyChanged(this, new PropertyChangedEventArgs("UndoStack:Populated:" + c.GetTag()));
        }

        /// <summary>
        /// Undos the previous command, send propertychangedevent to subscribers regarding the state of the undo and redo stack.
        /// </summary>
        public void UndoCommand()
        {
            if (this.undoStack.Count > 0)
            {
                ICommand old = this.undoStack.Pop();

                old.UnExecute();

                // Push the command to the redo stack and notify the spreadsheet that the redo stack is populated.
                this.redoStack.Push(old);
                this.PropertyChanged(this, new PropertyChangedEventArgs("RedoStack:Populated:" + old.GetTag()));
            }

            // Check if we need to notify of any other important changes in the undo stack, and notify the spreadsheet appropriately.
            if (this.undoStack.Count <= 0)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs("UndoStack:Empty"));
            }
            else
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs("UndoStack:Populated:" + this.undoStack.Peek().GetTag()));
            }
        }

        /// <summary>
        /// Redos the latest command to be undone, send propertychangedevent to subscribers regarding the state of the undo and redo stack.
        /// </summary>
        public void RedoCommand()
        {
            if (this.redoStack.Count > 0)
            {
                ICommand old = this.redoStack.Pop();

                old.Execute();

                // Push the redo command back to the undo stack and notify the spreadsheet
                this.undoStack.Push(old);
                this.PropertyChanged(this, new PropertyChangedEventArgs("UndoStack:Populated:" + old.GetTag()));
            }

            // Check if we need to notify of any other important changes in the undo stack, and notify the spreadsheet appropriately.
            if (this.redoStack.Count <= 0)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs("RedoStack:Empty"));
            }
            else
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs("RedoStack:Populated:" + this.redoStack.Peek().GetTag()));
            }
        }

        /// <summary>
        /// Clears the undo and redo stacks for the program.
        /// </summary>
        public void Clear()
        {
            this.undoStack.Clear();
            this.redoStack.Clear();
        }
    }
}
