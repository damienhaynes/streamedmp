namespace streamedmp_editor
{
    partial class frmStreamedMPEditor
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
            this.components = new System.ComponentModel.Container();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lstAvailableWindows = new System.Windows.Forms.ListBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.chklstWinddowsInMenu = new System.Windows.Forms.CheckedListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openOpenFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBGTime = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cboBGFolder = new System.Windows.Forms.ComboBox();
            this.chkBGRandom = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtMenuXPos = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtNoFocusColour = new System.Windows.Forms.TextBox();
            this.txtfocusColour = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRemove = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cboContextLabels = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.llRssTicker = new System.Windows.Forms.LinkLabel();
            this.chkRssTicker = new System.Windows.Forms.CheckBox();
            this.btMoveUp = new System.Windows.Forms.Button();
            this.btMoveDown = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.statusStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Enabled = false;
            this.btnAdd.Location = new System.Drawing.Point(136, 407);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 26);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "&Add >>";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lstAvailableWindows
            // 
            this.lstAvailableWindows.Enabled = false;
            this.lstAvailableWindows.FormattingEnabled = true;
            this.lstAvailableWindows.Location = new System.Drawing.Point(13, 150);
            this.lstAvailableWindows.Name = "lstAvailableWindows";
            this.lstAvailableWindows.ScrollAlwaysVisible = true;
            this.lstAvailableWindows.Size = new System.Drawing.Size(198, 251);
            this.lstAvailableWindows.TabIndex = 3;
            this.lstAvailableWindows.SelectedIndexChanged += new System.EventHandler(this.lstAvailableWindows_SelectedIndexChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 521);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(764, 22);
            this.statusStrip1.TabIndex = 13;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel1.Text = "Ready";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "PNG Images|*.png";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "&Folder:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Enabled = false;
            this.btnGenerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerate.Location = new System.Drawing.Point(567, 29);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(106, 49);
            this.btnGenerate.TabIndex = 0;
            this.btnGenerate.Text = "&Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // chklstWinddowsInMenu
            // 
            this.chklstWinddowsInMenu.ContextMenuStrip = this.contextMenuStrip1;
            this.chklstWinddowsInMenu.Enabled = false;
            this.chklstWinddowsInMenu.FormattingEnabled = true;
            this.chklstWinddowsInMenu.Location = new System.Drawing.Point(520, 157);
            this.chklstWinddowsInMenu.Name = "chklstWinddowsInMenu";
            this.chklstWinddowsInMenu.ScrollAlwaysVisible = true;
            this.chklstWinddowsInMenu.Size = new System.Drawing.Size(198, 244);
            this.chklstWinddowsInMenu.TabIndex = 9;
            this.chklstWinddowsInMenu.MouseEnter += new System.EventHandler(this.lstWinddowsInMenu_MouseEnter);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(118, 26);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Available Windows";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(517, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Windows in Menu";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(764, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openOpenFolderToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openOpenFolderToolStripMenuItem
            // 
            this.openOpenFolderToolStripMenuItem.Name = "openOpenFolderToolStripMenuItem";
            this.openOpenFolderToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.openOpenFolderToolStripMenuItem.Text = "&Open Skin Folder";
            this.openOpenFolderToolStripMenuItem.Click += new System.EventHandler(this.openStreamedMPFolderToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "&Time Per Image:";
            // 
            // txtBGTime
            // 
            this.txtBGTime.Enabled = false;
            this.txtBGTime.Location = new System.Drawing.Point(97, 48);
            this.txtBGTime.Name = "txtBGTime";
            this.txtBGTime.Size = new System.Drawing.Size(68, 20);
            this.txtBGTime.TabIndex = 3;
            this.txtBGTime.Text = "30";
            this.txtBGTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(168, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Seconds";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cboBGFolder);
            this.groupBox2.Controls.Add(this.chkBGRandom);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtBGTime);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(226, 239);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(278, 111);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Background Images";
            // 
            // cboBGFolder
            // 
            this.cboBGFolder.FormattingEnabled = true;
            this.cboBGFolder.Items.AddRange(new object[] {
            "movies",
            "tv",
            "music",
            "weather",
            "pictures",
            "settings",
            "pluginsbg",
            "radio",
            "emulator",
            "games"});
            this.cboBGFolder.Location = new System.Drawing.Point(97, 17);
            this.cboBGFolder.Name = "cboBGFolder";
            this.cboBGFolder.Size = new System.Drawing.Size(146, 21);
            this.cboBGFolder.TabIndex = 1;
            this.cboBGFolder.MouseEnter += new System.EventHandler(this.cboBGFolder_MouseEnter);
            // 
            // chkBGRandom
            // 
            this.chkBGRandom.AutoSize = true;
            this.chkBGRandom.Checked = true;
            this.chkBGRandom.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBGRandom.Enabled = false;
            this.chkBGRandom.Location = new System.Drawing.Point(99, 74);
            this.chkBGRandom.Name = "chkBGRandom";
            this.chkBGRandom.Size = new System.Drawing.Size(66, 17);
            this.chkBGRandom.TabIndex = 5;
            this.chkBGRandom.Text = "Random";
            this.chkBGRandom.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtMenuXPos);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtNoFocusColour);
            this.groupBox1.Controls.Add(this.txtfocusColour);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnGenerate);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(705, 99);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Global settings";
            // 
            // txtMenuXPos
            // 
            this.txtMenuXPos.Location = new System.Drawing.Point(314, 29);
            this.txtMenuXPos.Name = "txtMenuXPos";
            this.txtMenuXPos.Size = new System.Drawing.Size(62, 20);
            this.txtMenuXPos.TabIndex = 6;
            this.txtMenuXPos.Text = "350";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(221, 32);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Menu X-Position:";
            // 
            // txtNoFocusColour
            // 
            this.txtNoFocusColour.Location = new System.Drawing.Point(122, 58);
            this.txtNoFocusColour.Name = "txtNoFocusColour";
            this.txtNoFocusColour.Size = new System.Drawing.Size(62, 20);
            this.txtNoFocusColour.TabIndex = 4;
            this.txtNoFocusColour.Text = "ffffff";
            this.txtNoFocusColour.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtNoFocusColour_MouseClick);
            // 
            // txtfocusColour
            // 
            this.txtfocusColour.Location = new System.Drawing.Point(122, 29);
            this.txtfocusColour.Name = "txtfocusColour";
            this.txtfocusColour.Size = new System.Drawing.Size(62, 20);
            this.txtfocusColour.TabIndex = 2;
            this.txtfocusColour.Text = "ffffff";
            this.txtfocusColour.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtfocusColour_MouseClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "No Focus Item Colour:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Focus Item Colour:";
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(520, 407);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 26);
            this.btnRemove.TabIndex = 10;
            this.btnRemove.Text = "<< &Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cboContextLabels);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.txtItemName);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Location = new System.Drawing.Point(226, 150);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(278, 83);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Item properties";
            // 
            // cboContextLabels
            // 
            this.cboContextLabels.FormattingEnabled = true;
            this.cboContextLabels.Items.AddRange(new object[] {
            "Watch Your",
            "Watch",
            "Check The",
            "Listen To",
            "View Your",
            "Browse Your",
            "Configure"});
            this.cboContextLabels.Location = new System.Drawing.Point(97, 19);
            this.cboContextLabels.Name = "cboContextLabels";
            this.cboContextLabels.Size = new System.Drawing.Size(148, 21);
            this.cboContextLabels.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(55, 27);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(36, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Label:";
            // 
            // txtItemName
            // 
            this.txtItemName.Location = new System.Drawing.Point(97, 49);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.Size = new System.Drawing.Size(148, 20);
            this.txtItemName.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(30, 56);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "&Item Name:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.llRssTicker);
            this.groupBox4.Controls.Add(this.chkRssTicker);
            this.groupBox4.Location = new System.Drawing.Point(12, 439);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(705, 72);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Plugin specific settings";
            // 
            // llRssTicker
            // 
            this.llRssTicker.AutoSize = true;
            this.llRssTicker.Location = new System.Drawing.Point(8, 48);
            this.llRssTicker.Name = "llRssTicker";
            this.llRssTicker.Size = new System.Drawing.Size(59, 13);
            this.llRssTicker.TabIndex = 1;
            this.llRssTicker.TabStop = true;
            this.llRssTicker.Text = "Homepage";
            this.llRssTicker.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llRssTicker_LinkClicked);
            // 
            // chkRssTicker
            // 
            this.chkRssTicker.AutoSize = true;
            this.chkRssTicker.Location = new System.Drawing.Point(8, 28);
            this.chkRssTicker.Name = "chkRssTicker";
            this.chkRssTicker.Size = new System.Drawing.Size(133, 17);
            this.chkRssTicker.TabIndex = 0;
            this.chkRssTicker.Text = "R&SS Ticker by Sambal";
            this.chkRssTicker.UseVisualStyleBackColor = true;
            // 
            // btMoveUp
            // 
            this.btMoveUp.Image = global::streamedmp_editor.Properties.Resources.up;
            this.btMoveUp.Location = new System.Drawing.Point(724, 214);
            this.btMoveUp.Name = "btMoveUp";
            this.btMoveUp.Size = new System.Drawing.Size(33, 23);
            this.btMoveUp.TabIndex = 11;
            this.btMoveUp.UseVisualStyleBackColor = true;
            this.btMoveUp.Click += new System.EventHandler(this.btMoveUp_Click);
            // 
            // btMoveDown
            // 
            this.btMoveDown.Image = global::streamedmp_editor.Properties.Resources.down;
            this.btMoveDown.Location = new System.Drawing.Point(724, 242);
            this.btMoveDown.Name = "btMoveDown";
            this.btMoveDown.Size = new System.Drawing.Size(33, 23);
            this.btMoveDown.TabIndex = 12;
            this.btMoveDown.UseVisualStyleBackColor = true;
            this.btMoveDown.Click += new System.EventHandler(this.btMoveDown_Click);
            // 
            // frmStreamedMPEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 543);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btMoveDown);
            this.Controls.Add(this.btMoveUp);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chklstWinddowsInMenu);
            this.Controls.Add(this.lstAvailableWindows);
            this.Controls.Add(this.btnRemove);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(780, 579);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(780, 579);
            this.Name = "frmStreamedMPEditor";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "StreamedMP Menu Generator";
            this.Load += new System.EventHandler(this.frmStreamedMPEditor_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        

       

       

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ListBox lstAvailableWindows;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.CheckedListBox chklstWinddowsInMenu;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openOpenFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBGTime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkBGRandom;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNoFocusColour;
        private System.Windows.Forms.TextBox txtfocusColour;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btMoveUp;
        private System.Windows.Forms.Button btMoveDown;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.CheckBox chkRssTicker;
        private System.Windows.Forms.LinkLabel llRssTicker;
        private System.Windows.Forms.TextBox txtMenuXPos;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.ComboBox cboContextLabels;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cboBGFolder;
    }
}

