namespace StreamedMPEditor
{
    partial class SelectHardDrives
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
            this.lboxAvailableDrives = new System.Windows.Forms.ListBox();
            this.lboxSelectedDrives = new System.Windows.Forms.ListBox();
            this.btRemove = new System.Windows.Forms.Button();
            this.btAdd = new System.Windows.Forms.Button();
            this.lbAvailableDrives = new System.Windows.Forms.Label();
            this.lbSelectedDrives = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btUp = new System.Windows.Forms.Button();
            this.btDown = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btSaveAndClose = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lboxAvailableDrives
            // 
            this.lboxAvailableDrives.FormattingEnabled = true;
            this.lboxAvailableDrives.Location = new System.Drawing.Point(22, 37);
            this.lboxAvailableDrives.Name = "lboxAvailableDrives";
            this.lboxAvailableDrives.Size = new System.Drawing.Size(167, 147);
            this.lboxAvailableDrives.TabIndex = 0;
            // 
            // lboxSelectedDrives
            // 
            this.lboxSelectedDrives.FormattingEnabled = true;
            this.lboxSelectedDrives.Location = new System.Drawing.Point(326, 33);
            this.lboxSelectedDrives.Name = "lboxSelectedDrives";
            this.lboxSelectedDrives.Size = new System.Drawing.Size(167, 56);
            this.lboxSelectedDrives.TabIndex = 1;
            // 
            // btRemove
            // 
            this.btRemove.Location = new System.Drawing.Point(212, 66);
            this.btRemove.Name = "btRemove";
            this.btRemove.Size = new System.Drawing.Size(75, 23);
            this.btRemove.TabIndex = 2;
            this.btRemove.Text = "<<<";
            this.btRemove.UseVisualStyleBackColor = true;
            this.btRemove.Click += new System.EventHandler(this.btRemove_Click);
            // 
            // btAdd
            // 
            this.btAdd.Location = new System.Drawing.Point(212, 37);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(75, 23);
            this.btAdd.TabIndex = 3;
            this.btAdd.Text = ">>>";
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // lbAvailableDrives
            // 
            this.lbAvailableDrives.AutoSize = true;
            this.lbAvailableDrives.Location = new System.Drawing.Point(4, 4);
            this.lbAvailableDrives.Name = "lbAvailableDrives";
            this.lbAvailableDrives.Size = new System.Drawing.Size(91, 13);
            this.lbAvailableDrives.TabIndex = 5;
            this.lbAvailableDrives.Text = "Configured Drives";
            // 
            // lbSelectedDrives
            // 
            this.lbSelectedDrives.AutoSize = true;
            this.lbSelectedDrives.Location = new System.Drawing.Point(3, 4);
            this.lbSelectedDrives.Name = "lbSelectedDrives";
            this.lbSelectedDrives.Size = new System.Drawing.Size(127, 13);
            this.lbSelectedDrives.TabIndex = 6;
            this.lbSelectedDrives.Text = "Drives to show in Overlay";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.lbAvailableDrives);
            this.panel1.Location = new System.Drawing.Point(22, 15);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(167, 22);
            this.panel1.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lbSelectedDrives);
            this.panel2.Location = new System.Drawing.Point(326, 11);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(192, 79);
            this.panel2.TabIndex = 8;
            // 
            // btUp
            // 
            this.btUp.Image = global::StreamedMPEditor.Properties.Resources.up;
            this.btUp.Location = new System.Drawing.Point(493, 34);
            this.btUp.Name = "btUp";
            this.btUp.Size = new System.Drawing.Size(24, 27);
            this.btUp.TabIndex = 9;
            this.btUp.UseVisualStyleBackColor = true;
            this.btUp.Click += new System.EventHandler(this.btUp_Click);
            // 
            // btDown
            // 
            this.btDown.Image = global::StreamedMPEditor.Properties.Resources.down;
            this.btDown.Location = new System.Drawing.Point(493, 62);
            this.btDown.Name = "btDown";
            this.btDown.Size = new System.Drawing.Size(23, 27);
            this.btDown.TabIndex = 10;
            this.btDown.UseVisualStyleBackColor = true;
            this.btDown.Click += new System.EventHandler(this.btDown_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(212, 103);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(304, 48);
            this.textBox1.TabIndex = 11;
            this.textBox1.Text = "From the list on the left of drives configured in DriveFreeSpace select up to a m" +
                "aximum of 3 drives to be displayed in the overlay.\r\n";
            // 
            // btSaveAndClose
            // 
            this.btSaveAndClose.Location = new System.Drawing.Point(212, 161);
            this.btSaveAndClose.Name = "btSaveAndClose";
            this.btSaveAndClose.Size = new System.Drawing.Size(306, 23);
            this.btSaveAndClose.TabIndex = 12;
            this.btSaveAndClose.Text = "Save and Close";
            this.btSaveAndClose.UseVisualStyleBackColor = true;
            this.btSaveAndClose.Click += new System.EventHandler(this.btSaveAndClose_Click);
            // 
            // SelectHardDrives
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 199);
            this.Controls.Add(this.btSaveAndClose);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btDown);
            this.Controls.Add(this.btUp);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btAdd);
            this.Controls.Add(this.btRemove);
            this.Controls.Add(this.lboxSelectedDrives);
            this.Controls.Add(this.lboxAvailableDrives);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SelectHardDrives";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Free Drive Space Overlay - Drive Selection";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lboxAvailableDrives;
        private System.Windows.Forms.ListBox lboxSelectedDrives;
        private System.Windows.Forms.Button btRemove;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Label lbAvailableDrives;
        private System.Windows.Forms.Label lbSelectedDrives;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btUp;
        private System.Windows.Forms.Button btDown;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btSaveAndClose;

    }
}