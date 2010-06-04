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
          this.cbShowEqGraphic = new System.Windows.Forms.CheckBox();
          this.cbCdCoverOnly = new System.Windows.Forms.CheckBox();
          this.VideoScreens = new System.Windows.Forms.TabPage();
          this.groupBox2 = new System.Windows.Forms.GroupBox();
          this.minVideoOSD = new System.Windows.Forms.RadioButton();
          this.fullVideoOSD = new System.Windows.Forms.RadioButton();
          this.CheckUpdate = new System.Windows.Forms.TabPage();
          this.label2 = new System.Windows.Forms.Label();
          this.cbCheckForUpdateAt = new System.Windows.Forms.CheckBox();
          this.comboCheckInterval = new System.Windows.Forms.ComboBox();
          this.cbCheckOnStart = new System.Windows.Forms.CheckBox();
          this.btCheckForUpdate = new System.Windows.Forms.Button();
          this.textBox1 = new System.Windows.Forms.TextBox();
          this.aboutTab = new System.Windows.Forms.TabPage();
          this.groupBox1 = new System.Windows.Forms.GroupBox();
          this.compileTime = new System.Windows.Forms.Label();
          this.richTextBox1 = new System.Windows.Forms.RichTextBox();
          this.releaseVersion = new System.Windows.Forms.Label();
          this.label1 = new System.Windows.Forms.Label();
          this.tabControl1.SuspendLayout();
          this.MusicScreens.SuspendLayout();
          this.VideoScreens.SuspendLayout();
          this.groupBox2.SuspendLayout();
          this.CheckUpdate.SuspendLayout();
          this.aboutTab.SuspendLayout();
          this.groupBox1.SuspendLayout();
          this.SuspendLayout();
          // 
          // btCancel
          // 
          this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
          this.btCancel.Location = new System.Drawing.Point(493, 313);
          this.btCancel.Name = "btCancel";
          this.btCancel.Size = new System.Drawing.Size(75, 23);
          this.btCancel.TabIndex = 31;
          this.btCancel.Text = "Cancel";
          this.btCancel.UseVisualStyleBackColor = true;
          this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
          // 
          // btSave
          // 
          this.btSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
          this.btSave.Location = new System.Drawing.Point(399, 313);
          this.btSave.Name = "btSave";
          this.btSave.Size = new System.Drawing.Size(75, 23);
          this.btSave.TabIndex = 30;
          this.btSave.Text = "Save";
          this.btSave.UseVisualStyleBackColor = true;
          this.btSave.Click += new System.EventHandler(this.btSave_Click);
          // 
          // tabControl1
          // 
          this.tabControl1.Controls.Add(this.MusicScreens);
          this.tabControl1.Controls.Add(this.VideoScreens);
          this.tabControl1.Controls.Add(this.CheckUpdate);
          this.tabControl1.Controls.Add(this.aboutTab);
          this.tabControl1.Location = new System.Drawing.Point(12, 52);
          this.tabControl1.Name = "tabControl1";
          this.tabControl1.SelectedIndex = 0;
          this.tabControl1.Size = new System.Drawing.Size(556, 253);
          this.tabControl1.TabIndex = 9;
          // 
          // MusicScreens
          // 
          this.MusicScreens.Controls.Add(this.cbShowEqGraphic);
          this.MusicScreens.Controls.Add(this.cbCdCoverOnly);
          this.MusicScreens.Location = new System.Drawing.Point(4, 22);
          this.MusicScreens.Name = "MusicScreens";
          this.MusicScreens.Padding = new System.Windows.Forms.Padding(3);
          this.MusicScreens.Size = new System.Drawing.Size(548, 227);
          this.MusicScreens.TabIndex = 0;
          this.MusicScreens.Text = "Music Screens";
          this.MusicScreens.UseVisualStyleBackColor = true;
          // 
          // cbShowEqGraphic
          // 
          this.cbShowEqGraphic.AutoSize = true;
          this.cbShowEqGraphic.BackColor = System.Drawing.Color.Transparent;
          this.cbShowEqGraphic.Location = new System.Drawing.Point(14, 39);
          this.cbShowEqGraphic.Name = "cbShowEqGraphic";
          this.cbShowEqGraphic.Size = new System.Drawing.Size(258, 17);
          this.cbShowEqGraphic.TabIndex = 11;
          this.cbShowEqGraphic.Text = "Enable EQ Graphic in Music Now Playing Screen";
          this.cbShowEqGraphic.UseVisualStyleBackColor = false;
          // 
          // cbCdCoverOnly
          // 
          this.cbCdCoverOnly.AutoSize = true;
          this.cbCdCoverOnly.Location = new System.Drawing.Point(14, 15);
          this.cbCdCoverOnly.Name = "cbCdCoverOnly";
          this.cbCdCoverOnly.Size = new System.Drawing.Size(278, 17);
          this.cbCdCoverOnly.TabIndex = 10;
          this.cbCdCoverOnly.Text = "Display CD Cover Only on Music Now Playing Screen";
          this.cbCdCoverOnly.UseVisualStyleBackColor = true;
          // 
          // VideoScreens
          // 
          this.VideoScreens.Controls.Add(this.groupBox2);
          this.VideoScreens.Location = new System.Drawing.Point(4, 22);
          this.VideoScreens.Name = "VideoScreens";
          this.VideoScreens.Size = new System.Drawing.Size(548, 227);
          this.VideoScreens.TabIndex = 3;
          this.VideoScreens.Text = "Video Screens";
          this.VideoScreens.UseVisualStyleBackColor = true;
          // 
          // groupBox2
          // 
          this.groupBox2.Controls.Add(this.minVideoOSD);
          this.groupBox2.Controls.Add(this.fullVideoOSD);
          this.groupBox2.Location = new System.Drawing.Point(11, 20);
          this.groupBox2.Name = "groupBox2";
          this.groupBox2.Size = new System.Drawing.Size(521, 35);
          this.groupBox2.TabIndex = 0;
          this.groupBox2.TabStop = false;
          this.groupBox2.Text = "VideoOSD";
          // 
          // minVideoOSD
          // 
          this.minVideoOSD.AutoSize = true;
          this.minVideoOSD.Location = new System.Drawing.Point(297, 12);
          this.minVideoOSD.Name = "minVideoOSD";
          this.minVideoOSD.Size = new System.Drawing.Size(86, 17);
          this.minVideoOSD.TabIndex = 1;
          this.minVideoOSD.TabStop = true;
          this.minVideoOSD.Text = "Minimal OSD";
          this.minVideoOSD.UseVisualStyleBackColor = true;
          // 
          // fullVideoOSD
          // 
          this.fullVideoOSD.AutoSize = true;
          this.fullVideoOSD.Location = new System.Drawing.Point(106, 12);
          this.fullVideoOSD.Name = "fullVideoOSD";
          this.fullVideoOSD.Size = new System.Drawing.Size(97, 17);
          this.fullVideoOSD.TabIndex = 0;
          this.fullVideoOSD.TabStop = true;
          this.fullVideoOSD.Text = "Full Video OSD";
          this.fullVideoOSD.UseVisualStyleBackColor = true;
          // 
          // CheckUpdate
          // 
          this.CheckUpdate.Controls.Add(this.label2);
          this.CheckUpdate.Controls.Add(this.cbCheckForUpdateAt);
          this.CheckUpdate.Controls.Add(this.comboCheckInterval);
          this.CheckUpdate.Controls.Add(this.cbCheckOnStart);
          this.CheckUpdate.Controls.Add(this.btCheckForUpdate);
          this.CheckUpdate.Controls.Add(this.textBox1);
          this.CheckUpdate.Location = new System.Drawing.Point(4, 22);
          this.CheckUpdate.Name = "CheckUpdate";
          this.CheckUpdate.Size = new System.Drawing.Size(548, 227);
          this.CheckUpdate.TabIndex = 4;
          this.CheckUpdate.Text = "Skin Update";
          this.CheckUpdate.UseVisualStyleBackColor = true;
          // 
          // label2
          // 
          this.label2.AutoSize = true;
          this.label2.Location = new System.Drawing.Point(273, 126);
          this.label2.Name = "label2";
          this.label2.Size = new System.Drawing.Size(30, 13);
          this.label2.TabIndex = 5;
          this.label2.Text = "Time";
          // 
          // cbCheckForUpdateAt
          // 
          this.cbCheckForUpdateAt.AutoSize = true;
          this.cbCheckForUpdateAt.Location = new System.Drawing.Point(16, 126);
          this.cbCheckForUpdateAt.Name = "cbCheckForUpdateAt";
          this.cbCheckForUpdateAt.Size = new System.Drawing.Size(110, 17);
          this.cbCheckForUpdateAt.TabIndex = 2;
          this.cbCheckForUpdateAt.Text = "Check for Update";
          this.cbCheckForUpdateAt.UseVisualStyleBackColor = true;
          // 
          // comboCheckInterval
          // 
          this.comboCheckInterval.FormattingEnabled = true;
          this.comboCheckInterval.Items.AddRange(new object[] {
            "Every Day",
            "Every Week",
            "Every 2 Weeks",
            "Every 4 Weeks"});
          this.comboCheckInterval.Location = new System.Drawing.Point(131, 126);
          this.comboCheckInterval.Name = "comboCheckInterval";
          this.comboCheckInterval.Size = new System.Drawing.Size(121, 21);
          this.comboCheckInterval.TabIndex = 4;
          // 
          // cbCheckOnStart
          // 
          this.cbCheckOnStart.AutoSize = true;
          this.cbCheckOnStart.Checked = true;
          this.cbCheckOnStart.CheckState = System.Windows.Forms.CheckState.Checked;
          this.cbCheckOnStart.Location = new System.Drawing.Point(16, 87);
          this.cbCheckOnStart.Name = "cbCheckOnStart";
          this.cbCheckOnStart.Size = new System.Drawing.Size(224, 17);
          this.cbCheckOnStart.TabIndex = 1;
          this.cbCheckOnStart.Text = "Check for update when MediaPortal starts";
          this.cbCheckOnStart.UseVisualStyleBackColor = true;
          // 
          // btCheckForUpdate
          // 
          this.btCheckForUpdate.Location = new System.Drawing.Point(176, 191);
          this.btCheckForUpdate.Name = "btCheckForUpdate";
          this.btCheckForUpdate.Size = new System.Drawing.Size(160, 23);
          this.btCheckForUpdate.TabIndex = 8;
          this.btCheckForUpdate.Text = "Check for Skin Update Now";
          this.btCheckForUpdate.UseVisualStyleBackColor = true;
          this.btCheckForUpdate.Click += new System.EventHandler(this.btCheckForUpdate_Click);
          // 
          // textBox1
          // 
          this.textBox1.Location = new System.Drawing.Point(16, 15);
          this.textBox1.Multiline = true;
          this.textBox1.Name = "textBox1";
          this.textBox1.ReadOnly = true;
          this.textBox1.Size = new System.Drawing.Size(508, 54);
          this.textBox1.TabIndex = 0;
          this.textBox1.Text = "This screen will allow you to check if a skin update is available, download and i" +
              "nstall. You can also configue this to be an automatic process if required.";
          // 
          // aboutTab
          // 
          this.aboutTab.Controls.Add(this.groupBox1);
          this.aboutTab.Location = new System.Drawing.Point(4, 22);
          this.aboutTab.Name = "aboutTab";
          this.aboutTab.Padding = new System.Windows.Forms.Padding(3);
          this.aboutTab.Size = new System.Drawing.Size(548, 227);
          this.aboutTab.TabIndex = 2;
          this.aboutTab.Text = "About";
          this.aboutTab.UseVisualStyleBackColor = true;
          // 
          // groupBox1
          // 
          this.groupBox1.Controls.Add(this.compileTime);
          this.groupBox1.Controls.Add(this.richTextBox1);
          this.groupBox1.Controls.Add(this.releaseVersion);
          this.groupBox1.Location = new System.Drawing.Point(16, 16);
          this.groupBox1.Name = "groupBox1";
          this.groupBox1.Size = new System.Drawing.Size(521, 121);
          this.groupBox1.TabIndex = 1;
          this.groupBox1.TabStop = false;
          this.groupBox1.Text = "About StreamedMP Configuration Plugin";
          // 
          // compileTime
          // 
          this.compileTime.AutoSize = true;
          this.compileTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.compileTime.Location = new System.Drawing.Point(24, 100);
          this.compileTime.Name = "compileTime";
          this.compileTime.Size = new System.Drawing.Size(30, 13);
          this.compileTime.TabIndex = 4;
          this.compileTime.Text = "Built:";
          // 
          // richTextBox1
          // 
          this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
          this.richTextBox1.Location = new System.Drawing.Point(9, 20);
          this.richTextBox1.Name = "richTextBox1";
          this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
          this.richTextBox1.Size = new System.Drawing.Size(478, 64);
          this.richTextBox1.TabIndex = 0;
          this.richTextBox1.Text = "This is the configuration tool for the StreamedMP Skin. \n\nIt allows you to custom" +
              "ise aspects of the skin to suit your preferance. This will be updated\nbased on u" +
              "sers requests.\n";
          // 
          // releaseVersion
          // 
          this.releaseVersion.AutoSize = true;
          this.releaseVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.releaseVersion.Location = new System.Drawing.Point(9, 83);
          this.releaseVersion.Name = "releaseVersion";
          this.releaseVersion.Size = new System.Drawing.Size(45, 13);
          this.releaseVersion.TabIndex = 3;
          this.releaseVersion.Text = "Version:";
          // 
          // label1
          // 
          this.label1.AutoSize = true;
          this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.label1.Location = new System.Drawing.Point(211, 9);
          this.label1.Name = "label1";
          this.label1.Size = new System.Drawing.Size(353, 26);
          this.label1.TabIndex = 10;
          this.label1.Text = "StreamedMP Skin Configuration";
          // 
          // ConfigurationForm
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(580, 346);
          this.Controls.Add(this.label1);
          this.Controls.Add(this.tabControl1);
          this.Controls.Add(this.btSave);
          this.Controls.Add(this.btCancel);
          this.Name = "ConfigurationForm";
          this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
          this.Text = "StreamedMP Skin Configuration";
          this.Load += new System.EventHandler(this.ConfigurationForm_Load);
          this.tabControl1.ResumeLayout(false);
          this.MusicScreens.ResumeLayout(false);
          this.MusicScreens.PerformLayout();
          this.VideoScreens.ResumeLayout(false);
          this.groupBox2.ResumeLayout(false);
          this.groupBox2.PerformLayout();
          this.CheckUpdate.ResumeLayout(false);
          this.CheckUpdate.PerformLayout();
          this.aboutTab.ResumeLayout(false);
          this.groupBox1.ResumeLayout(false);
          this.groupBox1.PerformLayout();
          this.ResumeLayout(false);
          this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage MusicScreens;
        private System.Windows.Forms.CheckBox cbCdCoverOnly;
        private System.Windows.Forms.CheckBox cbShowEqGraphic;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage aboutTab;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label compileTime;
        private System.Windows.Forms.Label releaseVersion;
        private System.Windows.Forms.TabPage VideoScreens;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton minVideoOSD;
        private System.Windows.Forms.RadioButton fullVideoOSD;
        private System.Windows.Forms.TabPage CheckUpdate;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboCheckInterval;
        private System.Windows.Forms.CheckBox cbCheckOnStart;
        private System.Windows.Forms.Button btCheckForUpdate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbCheckForUpdateAt;

    }
}