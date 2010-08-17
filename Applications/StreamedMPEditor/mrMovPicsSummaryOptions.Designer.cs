namespace StreamedMPEditor
{
  partial class MovPicsSummaryOptions
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
      this.cboxMRMovieTitle = new System.Windows.Forms.ComboBox();
      this.cboxMRMovieDetail = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.bySaveAndExit = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // cboxMRMovieTitle
      // 
      this.cboxMRMovieTitle.FormattingEnabled = true;
      this.cboxMRMovieTitle.Items.AddRange(new object[] {
            "mediastream10c",
            "mediastream10tc (Bold)"});
      this.cboxMRMovieTitle.Location = new System.Drawing.Point(165, 46);
      this.cboxMRMovieTitle.Name = "cboxMRMovieTitle";
      this.cboxMRMovieTitle.Size = new System.Drawing.Size(165, 21);
      this.cboxMRMovieTitle.TabIndex = 0;
      this.cboxMRMovieTitle.Text = "mediastream10tc (Bold)";
      // 
      // cboxMRMovieDetail
      // 
      this.cboxMRMovieDetail.FormattingEnabled = true;
      this.cboxMRMovieDetail.Items.AddRange(new object[] {
            "mediastream10c",
            "mediastream10tc (Bold)"});
      this.cboxMRMovieDetail.Location = new System.Drawing.Point(165, 91);
      this.cboxMRMovieDetail.Name = "cboxMRMovieDetail";
      this.cboxMRMovieDetail.Size = new System.Drawing.Size(165, 21);
      this.cboxMRMovieDetail.TabIndex = 1;
      this.cboxMRMovieDetail.Text = "mediastream10c";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(73, 49);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(83, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Movie Title Font";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(61, 94);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(95, 13);
      this.label2.TabIndex = 3;
      this.label2.Text = "Movie Details Font";
      // 
      // bySaveAndExit
      // 
      this.bySaveAndExit.Location = new System.Drawing.Point(165, 143);
      this.bySaveAndExit.Name = "bySaveAndExit";
      this.bySaveAndExit.Size = new System.Drawing.Size(103, 23);
      this.bySaveAndExit.TabIndex = 4;
      this.bySaveAndExit.Text = "Close";
      this.bySaveAndExit.UseVisualStyleBackColor = true;
      this.bySaveAndExit.Click += new System.EventHandler(this.btSaveAndExit_Click);
      // 
      // MovPicsSummaryOptions
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(439, 202);
      this.Controls.Add(this.bySaveAndExit);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.cboxMRMovieDetail);
      this.Controls.Add(this.cboxMRMovieTitle);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Name = "MovPicsSummaryOptions";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = " Most Recent MovingPictures Summary Formatting";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ComboBox cboxMRMovieTitle;
    private System.Windows.Forms.ComboBox cboxMRMovieDetail;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button bySaveAndExit;
  }
}