namespace StreamedMPEditor
{
  partial class mrsFormatOptions
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
      this.btSaveAndExit = new System.Windows.Forms.Button();
      this.cbMRSeriesEpisodeFormat = new System.Windows.Forms.CheckBox();
      this.cboxMRSeriesFont = new System.Windows.Forms.ComboBox();
      this.lbSeriesFont = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.cboxMREpisodeFont = new System.Windows.Forms.ComboBox();
      this.cbTitleSwap = new System.Windows.Forms.CheckBox();
      this.SuspendLayout();
      // 
      // btSaveAndExit
      // 
      this.btSaveAndExit.Location = new System.Drawing.Point(122, 162);
      this.btSaveAndExit.Name = "btSaveAndExit";
      this.btSaveAndExit.Size = new System.Drawing.Size(194, 23);
      this.btSaveAndExit.TabIndex = 0;
      this.btSaveAndExit.Text = "Save and Exit";
      this.btSaveAndExit.UseVisualStyleBackColor = true;
      this.btSaveAndExit.Click += new System.EventHandler(this.btSaveAndExit_Click);
      // 
      // cbMRSeriesEpisodeFormat
      // 
      this.cbMRSeriesEpisodeFormat.AutoSize = true;
      this.cbMRSeriesEpisodeFormat.Location = new System.Drawing.Point(122, 12);
      this.cbMRSeriesEpisodeFormat.Name = "cbMRSeriesEpisodeFormat";
      this.cbMRSeriesEpisodeFormat.Size = new System.Drawing.Size(208, 17);
      this.cbMRSeriesEpisodeFormat.TabIndex = 1;
      this.cbMRSeriesEpisodeFormat.Text = "Use S01E01 format for Series/Episode";
      this.cbMRSeriesEpisodeFormat.UseVisualStyleBackColor = true;
      // 
      // cboxMRSeriesFont
      // 
      this.cboxMRSeriesFont.FormattingEnabled = true;
      this.cboxMRSeriesFont.Items.AddRange(new object[] {
            "mediastream9c",
            "mediastream10tc (Bold)"});
      this.cboxMRSeriesFont.Location = new System.Drawing.Point(208, 78);
      this.cboxMRSeriesFont.Name = "cboxMRSeriesFont";
      this.cboxMRSeriesFont.Size = new System.Drawing.Size(154, 21);
      this.cboxMRSeriesFont.TabIndex = 2;
      this.cboxMRSeriesFont.Text = "mediastream9c";
      // 
      // lbSeriesFont
      // 
      this.lbSeriesFont.AutoSize = true;
      this.lbSeriesFont.Location = new System.Drawing.Point(84, 81);
      this.lbSeriesFont.Name = "lbSeriesFont";
      this.lbSeriesFont.Size = new System.Drawing.Size(118, 13);
      this.lbSeriesFont.TabIndex = 3;
      this.lbSeriesFont.Text = "Font for Series/Episode";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(95, 115);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(107, 13);
      this.label1.TabIndex = 5;
      this.label1.Text = "Font for Episode Title";
      // 
      // cboxMREpisodeFont
      // 
      this.cboxMREpisodeFont.FormattingEnabled = true;
      this.cboxMREpisodeFont.Items.AddRange(new object[] {
            "mediastream9c",
            "mediastream10tc (Bold)"});
      this.cboxMREpisodeFont.Location = new System.Drawing.Point(208, 112);
      this.cboxMREpisodeFont.Name = "cboxMREpisodeFont";
      this.cboxMREpisodeFont.Size = new System.Drawing.Size(154, 21);
      this.cboxMREpisodeFont.TabIndex = 4;
      this.cboxMREpisodeFont.Text = "mediastream10tc (Bold)";
      // 
      // cbTitleSwap
      // 
      this.cbTitleSwap.AutoSize = true;
      this.cbTitleSwap.Location = new System.Drawing.Point(122, 36);
      this.cbTitleSwap.Name = "cbTitleSwap";
      this.cbTitleSwap.Size = new System.Drawing.Size(254, 17);
      this.cbTitleSwap.TabIndex = 6;
      this.cbTitleSwap.Text = "Use \"SeriesNo/EpisodeNo - Title\" display format";
      this.cbTitleSwap.UseVisualStyleBackColor = true;
      // 
      // mrsFormatOptions
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(439, 202);
      this.Controls.Add(this.cbTitleSwap);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.cboxMREpisodeFont);
      this.Controls.Add(this.lbSeriesFont);
      this.Controls.Add(this.cboxMRSeriesFont);
      this.Controls.Add(this.cbMRSeriesEpisodeFormat);
      this.Controls.Add(this.btSaveAndExit);
      this.Name = "mrsFormatOptions";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = " Most Recent Summary Formatting";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btSaveAndExit;
    private System.Windows.Forms.CheckBox cbMRSeriesEpisodeFormat;
    private System.Windows.Forms.ComboBox cboxMRSeriesFont;
    private System.Windows.Forms.Label lbSeriesFont;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox cboxMREpisodeFont;
    private System.Windows.Forms.CheckBox cbTitleSwap;
  }
}