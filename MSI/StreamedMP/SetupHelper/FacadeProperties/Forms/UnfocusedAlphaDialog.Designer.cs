namespace FacadeProperties {
    partial class UnfocusedAlphaDialog {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.trackBarThumbAlpha = new System.Windows.Forms.TrackBar();
            this.trackBarListAlpha = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.linkReset = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblListItemAlpha = new System.Windows.Forms.Label();
            this.lblThumbsAlpha = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarThumbAlpha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarListAlpha)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblThumbsAlpha);
            this.groupBox1.Controls.Add(this.lblListItemAlpha);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.trackBarThumbAlpha);
            this.groupBox1.Controls.Add(this.trackBarListAlpha);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(373, 233);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // trackBarThumbAlpha
            // 
            this.trackBarThumbAlpha.LargeChange = 15;
            this.trackBarThumbAlpha.Location = new System.Drawing.Point(42, 136);
            this.trackBarThumbAlpha.Maximum = 255;
            this.trackBarThumbAlpha.Name = "trackBarThumbAlpha";
            this.trackBarThumbAlpha.Size = new System.Drawing.Size(280, 45);
            this.trackBarThumbAlpha.SmallChange = 5;
            this.trackBarThumbAlpha.TabIndex = 4;
            this.trackBarThumbAlpha.TickFrequency = 5;
            this.trackBarThumbAlpha.Value = 100;
            this.trackBarThumbAlpha.Scroll += new System.EventHandler(this.trackBarThumbAlpha_Scroll);
            // 
            // trackBarListAlpha
            // 
            this.trackBarListAlpha.LargeChange = 15;
            this.trackBarListAlpha.Location = new System.Drawing.Point(42, 72);
            this.trackBarListAlpha.Maximum = 255;
            this.trackBarListAlpha.Name = "trackBarListAlpha";
            this.trackBarListAlpha.Size = new System.Drawing.Size(280, 45);
            this.trackBarListAlpha.SmallChange = 5;
            this.trackBarListAlpha.TabIndex = 3;
            this.trackBarListAlpha.TickFrequency = 5;
            this.trackBarListAlpha.Value = 100;
            this.trackBarListAlpha.Scroll += new System.EventHandler(this.trackBarListAlpha_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Thumbnail Items:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "List Items:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(325, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select how transparent an item on a list or thumbnail panel appears \r\nwhen unfocu" +
                "sed:";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(310, 243);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // linkReset
            // 
            this.linkReset.AutoSize = true;
            this.linkReset.Location = new System.Drawing.Point(12, 243);
            this.linkReset.Name = "linkReset";
            this.linkReset.Size = new System.Drawing.Size(35, 13);
            this.linkReset.TabIndex = 2;
            this.linkReset.TabStop = true;
            this.linkReset.Text = "Reset";
            this.linkReset.Click += new System.EventHandler(this.linkReset_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(37, 157);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(316, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "255";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(316, 157);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(25, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "255";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(22, 187);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(330, 26);
            this.label8.TabIndex = 9;
            this.label8.Text = "Note: Setting the value to \'0\' will make the item invisible, set the item \r\nto \'2" +
                "55\' to make items Opaque.";
            // 
            // lblListItemAlpha
            // 
            this.lblListItemAlpha.AutoSize = true;
            this.lblListItemAlpha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblListItemAlpha.Location = new System.Drawing.Point(163, 58);
            this.lblListItemAlpha.Name = "lblListItemAlpha";
            this.lblListItemAlpha.Size = new System.Drawing.Size(28, 13);
            this.lblListItemAlpha.TabIndex = 10;
            this.lblListItemAlpha.Text = "100";
            // 
            // lblThumbsAlpha
            // 
            this.lblThumbsAlpha.AutoSize = true;
            this.lblThumbsAlpha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThumbsAlpha.Location = new System.Drawing.Point(163, 124);
            this.lblThumbsAlpha.Name = "lblThumbsAlpha";
            this.lblThumbsAlpha.Size = new System.Drawing.Size(28, 13);
            this.lblThumbsAlpha.TabIndex = 11;
            this.lblThumbsAlpha.Text = "100";
            // 
            // UnfocusedAlphaDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 272);
            this.Controls.Add(this.linkReset);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UnfocusedAlphaDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Unfocused Alpha";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarThumbAlpha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarListAlpha)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBarListAlpha;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar trackBarThumbAlpha;
        private System.Windows.Forms.LinkLabel linkReset;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblThumbsAlpha;
        private System.Windows.Forms.Label lblListItemAlpha;
    }
}