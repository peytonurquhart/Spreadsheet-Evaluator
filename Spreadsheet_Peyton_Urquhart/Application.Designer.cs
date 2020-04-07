namespace Spreadsheet_Peyton_Urquhart
{
    partial class Application
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gridMain = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.formatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cellColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridMain)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridMain
            // 
            this.gridMain.AllowUserToAddRows = false;
            this.gridMain.AllowUserToDeleteRows = false;
            this.gridMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridMain.Location = new System.Drawing.Point(12, 43);
            this.gridMain.Name = "gridMain";
            this.gridMain.RowHeadersWidth = 60;
            this.gridMain.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gridMain.RowTemplate.Height = 33;
            this.gridMain.Size = new System.Drawing.Size(1415, 787);
            this.gridMain.TabIndex = 0;
            this.gridMain.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.GridMain_CellBeginEdit);
            this.gridMain.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridMain_CellEndEdit);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.formatToolStripMenuItem,
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(2336, 42);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "mainMenuStrip";
            // 
            // formatToolStripMenuItem
            // 
            this.formatToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cellColorToolStripMenuItem});
            this.formatToolStripMenuItem.Name = "formatToolStripMenuItem";
            this.formatToolStripMenuItem.Size = new System.Drawing.Size(102, 38);
            this.formatToolStripMenuItem.Text = "Format";
            // 
            // cellColorToolStripMenuItem
            // 
            this.cellColorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeColorToolStripMenuItem});
            this.cellColorToolStripMenuItem.Name = "cellColorToolStripMenuItem";
            this.cellColorToolStripMenuItem.Size = new System.Drawing.Size(268, 38);
            this.cellColorToolStripMenuItem.Text = "Cells";
            // 
            // changeColorToolStripMenuItem
            // 
            this.changeColorToolStripMenuItem.Name = "changeColorToolStripMenuItem";
            this.changeColorToolStripMenuItem.Size = new System.Drawing.Size(260, 38);
            this.changeColorToolStripMenuItem.Text = "Change Color";
            this.changeColorToolStripMenuItem.Click += new System.EventHandler(this.FormatCellsChangeColor_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(67, 38);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(268, 38);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.EditUndo_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(268, 38);
            this.redoToolStripMenuItem.Text = "Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.EditRedo_Click);
            // 
            // Application
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2336, 1281);
            this.Controls.Add(this.gridMain);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Application";
            this.Text = "Spreadsheet_Peyton_Urquhart";
            ((System.ComponentModel.ISupportInitialize)(this.gridMain)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridMain;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem formatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cellColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
    }
}

