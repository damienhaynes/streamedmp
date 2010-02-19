namespace StreamedMPConfig
{
    partial class ConfigurationForm
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
            this.btCancel = new System.Windows.Forms.Button();
            this.btSave = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.MusicScreens = new System.Windows.Forms.TabPage();
            this.MiscScreens = new System.Windows.Forms.TabPage();
            this.cdCoverOnly = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.MusicScreens.SuspendLayout();
            this.SuspendLayout();
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.Location = new System.Drawing.Point(493, 309);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 5;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btSave
            // 
            this.btSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSave.Location = new System.Drawing.Point(399, 309);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(75, 23);
            this.btSave.TabIndex = 8;
            this.btSave.Text = "Save";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.MusicScreens);
            this.tabControl1.Controls.Add(this.MiscScreens);
            this.tabControl1.Location = new System.Drawing.Point(12, 40);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(556, 253);
            this.tabControl1.TabIndex = 9;
            // 
            // MusicScreens
            // 
            this.MusicScreens.Controls.Add(this.cdCoverOnly);
            this.MusicScreens.Location = new System.Drawing.Point(4, 22);
            this.MusicScreens.Name = "MusicScreens";
            this.MusicScreens.Padding = new System.Windows.Forms.Padding(3);
            this.MusicScreens.Size = new System.Drawing.Size(548, 227);
            this.MusicScreens.TabIndex = 0;
            this.MusicScreens.Text = "Music Screens";
            this.MusicScreens.UseVisualStyleBackColor = true;
            // 
            // MiscScreens
            // 
            this.MiscScreens.Location = new System.Drawing.Point(4, 22);
            this.MiscScreens.Name = "MiscScreens";
            this.MiscScreens.Padding = new System.Windows.Forms.Padding(3);
            this.MiscScreens.Size = new System.Drawing.Size(548, 227);
            this.MiscScreens.TabIndex = 1;
            this.MiscScreens.Text = "Misc Screens";
            this.MiscScreens.UseVisualStyleBackColor = true;
            // 
            // cdCoverOnly
            // 
            this.cdCoverOnly.AutoSize = true;
            this.cdCoverOnly.Location = new System.Drawing.Point(14, 15);
            this.cdCoverOnly.Name = "cdCoverOnly";
            this.cdCoverOnly.Size = new System.Drawing.Size(241, 17);
            this.cdCoverOnly.TabIndex = 10;
            this.cdCoverOnly.Text = "Display CD Cover Only on Music Now Playing";
            this.cdCoverOnly.UseVisualStyleBackColor = true;
            // 
            // ConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 346);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.btCancel);
            this.Name = "ConfigurationForm";
            this.Text = "StreamedMP Skin Configuration";
            this.Load += new System.EventHandler(this.ConfigurationForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.MusicScreens.ResumeLayout(false);
            this.MusicScreens.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage MusicScreens;
        private System.Windows.Forms.CheckBox cdCoverOnly;
        private System.Windows.Forms.TabPage MiscScreens;

    }
}