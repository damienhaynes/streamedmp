namespace SMPCheckSum_Test
{
  partial class SMPCheckSum_Test
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SMPCheckSum_Test));
      this.chkSumFiles = new System.Windows.Forms.ListView();
      this.Filename = new System.Windows.Forms.ColumnHeader();
      this.Checksum = new System.Windows.Forms.ColumnHeader();
      this.ilCheckSum = new System.Windows.Forms.ImageList(this.components);
      this.btAddChksumToAll = new System.Windows.Forms.Button();
      this.btVerifyChksum = new System.Windows.Forms.Button();
      this.pbCheckSum = new System.Windows.Forms.ProgressBar();
      this.btReGenerate = new System.Windows.Forms.Button();
      this.btInvalidFileName = new System.Windows.Forms.Button();
      this.btInvaildFile = new System.Windows.Forms.Button();
      this.btCompare = new System.Windows.Forms.Button();
      this.btRemoveCheckSum = new System.Windows.Forms.Button();
      this.panel1 = new System.Windows.Forms.Panel();
      this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
      this.cboxXmlFolder = new System.Windows.Forms.ComboBox();
      this.btBrowse = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // chkSumFiles
      // 
      this.chkSumFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Filename,
            this.Checksum});
      this.chkSumFiles.Location = new System.Drawing.Point(6, 49);
      this.chkSumFiles.Name = "chkSumFiles";
      this.chkSumFiles.Size = new System.Drawing.Size(681, 632);
      this.chkSumFiles.SmallImageList = this.ilCheckSum;
      this.chkSumFiles.TabIndex = 0;
      this.chkSumFiles.UseCompatibleStateImageBehavior = false;
      this.chkSumFiles.View = System.Windows.Forms.View.Details;
      this.chkSumFiles.SelectedIndexChanged += new System.EventHandler(this.chkSumFiles_SelectedIndexChanged);
      // 
      // Filename
      // 
      this.Filename.Text = "FileName";
      this.Filename.Width = 284;
      // 
      // Checksum
      // 
      this.Checksum.Text = "Checksum";
      this.Checksum.Width = 393;
      // 
      // ilCheckSum
      // 
      this.ilCheckSum.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilCheckSum.ImageStream")));
      this.ilCheckSum.TransparentColor = System.Drawing.Color.Transparent;
      this.ilCheckSum.Images.SetKeyName(0, "checkSumOk.png");
      this.ilCheckSum.Images.SetKeyName(1, "checkSumFailed.png");
      // 
      // btAddChksumToAll
      // 
      this.btAddChksumToAll.Location = new System.Drawing.Point(16, 6);
      this.btAddChksumToAll.Name = "btAddChksumToAll";
      this.btAddChksumToAll.Size = new System.Drawing.Size(224, 23);
      this.btAddChksumToAll.TabIndex = 1;
      this.btAddChksumToAll.Text = "Add/Display checksum on all files";
      this.btAddChksumToAll.UseVisualStyleBackColor = true;
      this.btAddChksumToAll.Click += new System.EventHandler(this.btAddChksumToAll_Click);
      // 
      // btVerifyChksum
      // 
      this.btVerifyChksum.Location = new System.Drawing.Point(16, 93);
      this.btVerifyChksum.Name = "btVerifyChksum";
      this.btVerifyChksum.Size = new System.Drawing.Size(224, 23);
      this.btVerifyChksum.TabIndex = 2;
      this.btVerifyChksum.Text = "Verify Checksum on Selected File(s)";
      this.btVerifyChksum.UseVisualStyleBackColor = true;
      this.btVerifyChksum.Click += new System.EventHandler(this.btVerifyChksum_Click);
      // 
      // pbCheckSum
      // 
      this.pbCheckSum.Location = new System.Drawing.Point(6, 687);
      this.pbCheckSum.Name = "pbCheckSum";
      this.pbCheckSum.Size = new System.Drawing.Size(681, 20);
      this.pbCheckSum.TabIndex = 3;
      // 
      // btReGenerate
      // 
      this.btReGenerate.Location = new System.Drawing.Point(16, 64);
      this.btReGenerate.Name = "btReGenerate";
      this.btReGenerate.Size = new System.Drawing.Size(224, 23);
      this.btReGenerate.TabIndex = 4;
      this.btReGenerate.Text = "Re-Generate Checksum on Selected File(s)";
      this.btReGenerate.UseVisualStyleBackColor = true;
      this.btReGenerate.Click += new System.EventHandler(this.btReGenerate_Click);
      // 
      // btInvalidFileName
      // 
      this.btInvalidFileName.Location = new System.Drawing.Point(16, 198);
      this.btInvalidFileName.Name = "btInvalidFileName";
      this.btInvalidFileName.Size = new System.Drawing.Size(224, 23);
      this.btInvalidFileName.TabIndex = 5;
      this.btInvalidFileName.Text = "Test Invalid Filename";
      this.btInvalidFileName.UseVisualStyleBackColor = true;
      this.btInvalidFileName.Click += new System.EventHandler(this.button1_Click);
      // 
      // btInvaildFile
      // 
      this.btInvaildFile.Location = new System.Drawing.Point(16, 228);
      this.btInvaildFile.Name = "btInvaildFile";
      this.btInvaildFile.Size = new System.Drawing.Size(224, 23);
      this.btInvaildFile.TabIndex = 6;
      this.btInvaildFile.Text = "Test Invaild File Type";
      this.btInvaildFile.UseVisualStyleBackColor = true;
      this.btInvaildFile.Click += new System.EventHandler(this.btInvaildFile_Click);
      // 
      // btCompare
      // 
      this.btCompare.Location = new System.Drawing.Point(16, 35);
      this.btCompare.Name = "btCompare";
      this.btCompare.Size = new System.Drawing.Size(224, 23);
      this.btCompare.TabIndex = 7;
      this.btCompare.Text = "Compare Checksum on all Files";
      this.btCompare.UseVisualStyleBackColor = true;
      this.btCompare.Click += new System.EventHandler(this.btCompare_Click);
      // 
      // btRemoveCheckSum
      // 
      this.btRemoveCheckSum.Location = new System.Drawing.Point(16, 122);
      this.btRemoveCheckSum.Name = "btRemoveCheckSum";
      this.btRemoveCheckSum.Size = new System.Drawing.Size(224, 23);
      this.btRemoveCheckSum.TabIndex = 8;
      this.btRemoveCheckSum.Text = "Remove Checksum from Selected File(s)";
      this.btRemoveCheckSum.UseVisualStyleBackColor = true;
      this.btRemoveCheckSum.Click += new System.EventHandler(this.btRemoveCheckSum_Click);
      // 
      // panel1
      // 
      this.panel1.BackColor = System.Drawing.Color.LightGray;
      this.panel1.Controls.Add(this.btRemoveCheckSum);
      this.panel1.Controls.Add(this.btCompare);
      this.panel1.Controls.Add(this.btAddChksumToAll);
      this.panel1.Controls.Add(this.btInvaildFile);
      this.panel1.Controls.Add(this.btInvalidFileName);
      this.panel1.Controls.Add(this.btReGenerate);
      this.panel1.Controls.Add(this.btVerifyChksum);
      this.panel1.Location = new System.Drawing.Point(693, 49);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(256, 658);
      this.panel1.TabIndex = 9;
      // 
      // cboxXmlFolder
      // 
      this.cboxXmlFolder.AccessibleDescription = "";
      this.cboxXmlFolder.FormattingEnabled = true;
      this.cboxXmlFolder.Location = new System.Drawing.Point(96, 13);
      this.cboxXmlFolder.Name = "cboxXmlFolder";
      this.cboxXmlFolder.Size = new System.Drawing.Size(751, 21);
      this.cboxXmlFolder.TabIndex = 10;
      // 
      // btBrowse
      // 
      this.btBrowse.Location = new System.Drawing.Point(853, 10);
      this.btBrowse.Name = "btBrowse";
      this.btBrowse.Size = new System.Drawing.Size(93, 24);
      this.btBrowse.TabIndex = 11;
      this.btBrowse.Text = "Browse";
      this.btBrowse.UseVisualStyleBackColor = true;
      this.btBrowse.Click += new System.EventHandler(this.btBrowse_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(6, 16);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(84, 13);
      this.label1.TabIndex = 12;
      this.label1.Text = "Location of Files";
      // 
      // SMPCheckSum_Test
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(958, 712);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.btBrowse);
      this.Controls.Add(this.cboxXmlFolder);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.pbCheckSum);
      this.Controls.Add(this.chkSumFiles);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Name = "SMPCheckSum_Test";
      this.Text = "Checksum Test Program";
      this.Load += new System.EventHandler(this.SMPCheckSum_Load);
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ListView chkSumFiles;
    private System.Windows.Forms.ColumnHeader Filename;
    private System.Windows.Forms.ColumnHeader Checksum;
    private System.Windows.Forms.Button btAddChksumToAll;
    private System.Windows.Forms.Button btVerifyChksum;
    private System.Windows.Forms.ImageList ilCheckSum;
    private System.Windows.Forms.ProgressBar pbCheckSum;
    private System.Windows.Forms.Button btReGenerate;
    private System.Windows.Forms.Button btInvalidFileName;
    private System.Windows.Forms.Button btInvaildFile;
    private System.Windows.Forms.Button btCompare;
    private System.Windows.Forms.Button btRemoveCheckSum;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    private System.Windows.Forms.ComboBox cboxXmlFolder;
    private System.Windows.Forms.Button btBrowse;
    private System.Windows.Forms.Label label1;
  }
}

