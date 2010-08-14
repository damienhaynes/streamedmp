namespace SMPCheckSum
{
  partial class SMPCheckSum
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SMPCheckSum));
      this.chkSumFiles = new System.Windows.Forms.ListView();
      this.Filename = new System.Windows.Forms.ColumnHeader();
      this.Checksum = new System.Windows.Forms.ColumnHeader();
      this.btAddChksumToAll = new System.Windows.Forms.Button();
      this.btVerifyChksum = new System.Windows.Forms.Button();
      this.ilCheckSum = new System.Windows.Forms.ImageList(this.components);
      this.pbCheckSum = new System.Windows.Forms.ProgressBar();
      this.SuspendLayout();
      // 
      // chkSumFiles
      // 
      this.chkSumFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Filename,
            this.Checksum});
      this.chkSumFiles.Location = new System.Drawing.Point(122, 12);
      this.chkSumFiles.Name = "chkSumFiles";
      this.chkSumFiles.Size = new System.Drawing.Size(681, 215);
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
      // btAddChksumToAll
      // 
      this.btAddChksumToAll.Location = new System.Drawing.Point(122, 264);
      this.btAddChksumToAll.Name = "btAddChksumToAll";
      this.btAddChksumToAll.Size = new System.Drawing.Size(179, 23);
      this.btAddChksumToAll.TabIndex = 1;
      this.btAddChksumToAll.Text = "Add/Display checksum on all files";
      this.btAddChksumToAll.UseVisualStyleBackColor = true;
      this.btAddChksumToAll.Click += new System.EventHandler(this.btAddChksumToAll_Click);
      // 
      // btVerifyChksum
      // 
      this.btVerifyChksum.Location = new System.Drawing.Point(616, 264);
      this.btVerifyChksum.Name = "btVerifyChksum";
      this.btVerifyChksum.Size = new System.Drawing.Size(187, 23);
      this.btVerifyChksum.TabIndex = 2;
      this.btVerifyChksum.Text = "Verify Checksum on Selected File";
      this.btVerifyChksum.UseVisualStyleBackColor = true;
      this.btVerifyChksum.Click += new System.EventHandler(this.btVerifyChksum_Click);
      // 
      // ilCheckSum
      // 
      this.ilCheckSum.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilCheckSum.ImageStream")));
      this.ilCheckSum.TransparentColor = System.Drawing.Color.Transparent;
      this.ilCheckSum.Images.SetKeyName(0, "checkSumOk.png");
      this.ilCheckSum.Images.SetKeyName(1, "checkSumFailed.png");
      // 
      // pbCheckSum
      // 
      this.pbCheckSum.Location = new System.Drawing.Point(123, 235);
      this.pbCheckSum.Name = "pbCheckSum";
      this.pbCheckSum.Size = new System.Drawing.Size(679, 20);
      this.pbCheckSum.TabIndex = 3;
      // 
      // SMPCheckSum
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(924, 318);
      this.Controls.Add(this.pbCheckSum);
      this.Controls.Add(this.btVerifyChksum);
      this.Controls.Add(this.btAddChksumToAll);
      this.Controls.Add(this.chkSumFiles);
      this.Name = "SMPCheckSum";
      this.Text = "Form1";
      this.Load += new System.EventHandler(this.SMPCheckSum_Load);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ListView chkSumFiles;
    private System.Windows.Forms.ColumnHeader Filename;
    private System.Windows.Forms.ColumnHeader Checksum;
    private System.Windows.Forms.Button btAddChksumToAll;
    private System.Windows.Forms.Button btVerifyChksum;
    private System.Windows.Forms.ImageList ilCheckSum;
    private System.Windows.Forms.ProgressBar pbCheckSum;
  }
}

