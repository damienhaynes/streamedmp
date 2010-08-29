namespace SMPpatch
{
  partial class SMPpatch
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SMPpatch));
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.pictureBox2 = new System.Windows.Forms.PictureBox();
      this.mediaPortalStatusLb = new System.Windows.Forms.Label();
      this.mediaportalStatus = new System.Windows.Forms.Label();
      this.ConfigurationStatusLb = new System.Windows.Forms.Label();
      this.SMPEditorActivelb = new System.Windows.Forms.Label();
      this.smpeditorStatus = new System.Windows.Forms.Label();
      this.configurationStatus = new System.Windows.Forms.Label();
      this.processCheckGroup = new System.Windows.Forms.GroupBox();
      this.introBox = new System.Windows.Forms.TextBox();
      this.btInstallPatch = new System.Windows.Forms.Button();
      this.btExit = new System.Windows.Forms.Button();
      this.PatchFileName = new System.Windows.Forms.ColumnHeader();
      this.patchFileVersion = new System.Windows.Forms.ColumnHeader();
      this.thePatches = new System.Windows.Forms.ListView();
      this.installedVersion = new System.Windows.Forms.ColumnHeader();
      this.imageList1 = new System.Windows.Forms.ImageList(this.components);
      this.patchProgressBar = new System.Windows.Forms.ProgressBar();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
      this.processCheckGroup.SuspendLayout();
      this.SuspendLayout();
      // 
      // pictureBox1
      // 
      this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
      this.pictureBox1.Location = new System.Drawing.Point(13, 13);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(174, 310);
      this.pictureBox1.TabIndex = 0;
      this.pictureBox1.TabStop = false;
      // 
      // pictureBox2
      // 
      this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
      this.pictureBox2.Location = new System.Drawing.Point(193, 13);
      this.pictureBox2.Name = "pictureBox2";
      this.pictureBox2.Size = new System.Drawing.Size(355, 27);
      this.pictureBox2.TabIndex = 1;
      this.pictureBox2.TabStop = false;
      // 
      // mediaPortalStatusLb
      // 
      this.mediaPortalStatusLb.AutoSize = true;
      this.mediaPortalStatusLb.Location = new System.Drawing.Point(15, 18);
      this.mediaPortalStatusLb.Name = "mediaPortalStatusLb";
      this.mediaPortalStatusLb.Size = new System.Drawing.Size(96, 13);
      this.mediaPortalStatusLb.TabIndex = 2;
      this.mediaPortalStatusLb.Text = "MediaPortal Active";
      // 
      // mediaportalStatus
      // 
      this.mediaportalStatus.AutoSize = true;
      this.mediaportalStatus.ForeColor = System.Drawing.Color.Red;
      this.mediaportalStatus.Location = new System.Drawing.Point(283, 18);
      this.mediaportalStatus.Name = "mediaportalStatus";
      this.mediaportalStatus.Size = new System.Drawing.Size(47, 13);
      this.mediaportalStatus.TabIndex = 3;
      this.mediaportalStatus.Text = "Running";
      // 
      // ConfigurationStatusLb
      // 
      this.ConfigurationStatusLb.AutoSize = true;
      this.ConfigurationStatusLb.Location = new System.Drawing.Point(9, 36);
      this.ConfigurationStatusLb.Name = "ConfigurationStatusLb";
      this.ConfigurationStatusLb.Size = new System.Drawing.Size(102, 13);
      this.ConfigurationStatusLb.TabIndex = 4;
      this.ConfigurationStatusLb.Text = "Configuration Active";
      // 
      // SMPEditorActivelb
      // 
      this.SMPEditorActivelb.AutoSize = true;
      this.SMPEditorActivelb.Location = new System.Drawing.Point(21, 54);
      this.SMPEditorActivelb.Name = "SMPEditorActivelb";
      this.SMPEditorActivelb.Size = new System.Drawing.Size(90, 13);
      this.SMPEditorActivelb.TabIndex = 5;
      this.SMPEditorActivelb.Text = "SMPEditor Active";
      // 
      // smpeditorStatus
      // 
      this.smpeditorStatus.AutoSize = true;
      this.smpeditorStatus.ForeColor = System.Drawing.Color.Red;
      this.smpeditorStatus.Location = new System.Drawing.Point(283, 54);
      this.smpeditorStatus.Name = "smpeditorStatus";
      this.smpeditorStatus.Size = new System.Drawing.Size(47, 13);
      this.smpeditorStatus.TabIndex = 6;
      this.smpeditorStatus.Text = "Running";
      // 
      // configurationStatus
      // 
      this.configurationStatus.AutoSize = true;
      this.configurationStatus.ForeColor = System.Drawing.Color.Red;
      this.configurationStatus.Location = new System.Drawing.Point(283, 36);
      this.configurationStatus.Name = "configurationStatus";
      this.configurationStatus.Size = new System.Drawing.Size(47, 13);
      this.configurationStatus.TabIndex = 7;
      this.configurationStatus.Text = "Running";
      // 
      // processCheckGroup
      // 
      this.processCheckGroup.Controls.Add(this.mediaPortalStatusLb);
      this.processCheckGroup.Controls.Add(this.configurationStatus);
      this.processCheckGroup.Controls.Add(this.mediaportalStatus);
      this.processCheckGroup.Controls.Add(this.smpeditorStatus);
      this.processCheckGroup.Controls.Add(this.ConfigurationStatusLb);
      this.processCheckGroup.Controls.Add(this.SMPEditorActivelb);
      this.processCheckGroup.Location = new System.Drawing.Point(194, 209);
      this.processCheckGroup.Name = "processCheckGroup";
      this.processCheckGroup.Size = new System.Drawing.Size(354, 76);
      this.processCheckGroup.TabIndex = 8;
      this.processCheckGroup.TabStop = false;
      this.processCheckGroup.Text = "Active Process Check";
      // 
      // introBox
      // 
      this.introBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.introBox.Location = new System.Drawing.Point(193, 46);
      this.introBox.Multiline = true;
      this.introBox.Name = "introBox";
      this.introBox.ReadOnly = true;
      this.introBox.Size = new System.Drawing.Size(353, 41);
      this.introBox.TabIndex = 9;
      this.introBox.Text = "This utility will update the parts of the skin that can not be updated while any " +
          "of the Skin or MediaPortal processes are active.";
      // 
      // btInstallPatch
      // 
      this.btInstallPatch.Location = new System.Drawing.Point(194, 294);
      this.btInstallPatch.Name = "btInstallPatch";
      this.btInstallPatch.Size = new System.Drawing.Size(104, 24);
      this.btInstallPatch.TabIndex = 12;
      this.btInstallPatch.Text = "Install";
      this.btInstallPatch.UseVisualStyleBackColor = true;
      this.btInstallPatch.Click += new System.EventHandler(this.btInstallPatch_Click);
      // 
      // btExit
      // 
      this.btExit.Location = new System.Drawing.Point(444, 294);
      this.btExit.Name = "btExit";
      this.btExit.Size = new System.Drawing.Size(104, 24);
      this.btExit.TabIndex = 14;
      this.btExit.Text = "Exit";
      this.btExit.UseVisualStyleBackColor = true;
      this.btExit.Click += new System.EventHandler(this.btExit_Click);
      // 
      // PatchFileName
      // 
      this.PatchFileName.Text = "File";
      this.PatchFileName.Width = 150;
      // 
      // patchFileVersion
      // 
      this.patchFileVersion.Text = "Version";
      this.patchFileVersion.Width = 80;
      // 
      // thePatches
      // 
      this.thePatches.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.PatchFileName,
            this.patchFileVersion,
            this.installedVersion});
      this.thePatches.Location = new System.Drawing.Point(193, 76);
      this.thePatches.Name = "thePatches";
      this.thePatches.Size = new System.Drawing.Size(355, 106);
      this.thePatches.SmallImageList = this.imageList1;
      this.thePatches.TabIndex = 15;
      this.thePatches.UseCompatibleStateImageBehavior = false;
      this.thePatches.View = System.Windows.Forms.View.Details;
      // 
      // installedVersion
      // 
      this.installedVersion.Text = "Installed Version";
      this.installedVersion.Width = 100;
      // 
      // imageList1
      // 
      this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
      this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
      this.imageList1.Images.SetKeyName(0, "patchInstalled.png");
      this.imageList1.Images.SetKeyName(1, "patchNotInstalled.png");
      // 
      // patchProgressBar
      // 
      this.patchProgressBar.Location = new System.Drawing.Point(194, 191);
      this.patchProgressBar.Name = "patchProgressBar";
      this.patchProgressBar.Size = new System.Drawing.Size(351, 12);
      this.patchProgressBar.TabIndex = 16;
      // 
      // SMPpatch
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(560, 330);
      this.Controls.Add(this.patchProgressBar);
      this.Controls.Add(this.thePatches);
      this.Controls.Add(this.btExit);
      this.Controls.Add(this.btInstallPatch);
      this.Controls.Add(this.introBox);
      this.Controls.Add(this.processCheckGroup);
      this.Controls.Add(this.pictureBox2);
      this.Controls.Add(this.pictureBox1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.Name = "SMPpatch";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "StreamedMP Patch Utility";
      this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
      this.Load += new System.EventHandler(this.SMPpatch_Load);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SMPpatch_FormClosing);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
      this.processCheckGroup.ResumeLayout(false);
      this.processCheckGroup.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.PictureBox pictureBox2;
    private System.Windows.Forms.Label mediaPortalStatusLb;
    private System.Windows.Forms.Label mediaportalStatus;
    private System.Windows.Forms.Label ConfigurationStatusLb;
    private System.Windows.Forms.Label SMPEditorActivelb;
    private System.Windows.Forms.Label smpeditorStatus;
    private System.Windows.Forms.Label configurationStatus;
    private System.Windows.Forms.GroupBox processCheckGroup;
    private System.Windows.Forms.TextBox introBox;
    private System.Windows.Forms.Button btInstallPatch;
    private System.Windows.Forms.Button btExit;
    private System.Windows.Forms.ColumnHeader PatchFileName;
    private System.Windows.Forms.ColumnHeader patchFileVersion;
    private System.Windows.Forms.ListView thePatches;
    private System.Windows.Forms.ProgressBar patchProgressBar;
    private System.Windows.Forms.ColumnHeader installedVersion;
    private System.Windows.Forms.ImageList imageList1;
  }
}

