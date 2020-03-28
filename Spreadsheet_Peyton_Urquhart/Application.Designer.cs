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
            this.DemoButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridMain)).BeginInit();
            this.SuspendLayout();
            // 
            // gridMain
            // 
            this.gridMain.AllowUserToAddRows = false;
            this.gridMain.AllowUserToDeleteRows = false;
            this.gridMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridMain.Location = new System.Drawing.Point(12, 79);
            this.gridMain.Name = "gridMain";
            this.gridMain.RowHeadersWidth = 60;
            this.gridMain.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gridMain.RowTemplate.Height = 33;
            this.gridMain.Size = new System.Drawing.Size(1415, 787);
            this.gridMain.TabIndex = 0;
            this.gridMain.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.GridMain_CellBeginEdit);
            this.gridMain.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridMain_CellEndEdit);
            // 
            // DemoButton
            // 
            this.DemoButton.Location = new System.Drawing.Point(12, 24);
            this.DemoButton.Name = "DemoButton";
            this.DemoButton.Size = new System.Drawing.Size(177, 49);
            this.DemoButton.TabIndex = 1;
            this.DemoButton.Text = "Run Demo";
            this.DemoButton.UseVisualStyleBackColor = true;
            this.DemoButton.Click += new System.EventHandler(this.DemoButton_Click);
            // 
            // Application
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2336, 1281);
            this.Controls.Add(this.DemoButton);
            this.Controls.Add(this.gridMain);
            this.Name = "Application";
            this.Text = "Spreadsheet_Peyton_Urquhart";
            ((System.ComponentModel.ISupportInitialize)(this.gridMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridMain;
        private System.Windows.Forms.Button DemoButton;
    }
}

