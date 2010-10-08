﻿namespace StreamedMPEditor
{
  partial class mostRecentDisplaySelection
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
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.rbSubOff = new System.Windows.Forms.RadioButton();
      this.rbSubTV = new System.Windows.Forms.RadioButton();
      this.rbSubMusic = new System.Windows.Forms.RadioButton();
      this.rbSubMovies = new System.Windows.Forms.RadioButton();
      this.rbSubTVSeries = new System.Windows.Forms.RadioButton();
      this.btSaveAndClose = new System.Windows.Forms.Button();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.rbSubOff);
      this.groupBox1.Controls.Add(this.rbSubTV);
      this.groupBox1.Controls.Add(this.rbSubMusic);
      this.groupBox1.Controls.Add(this.rbSubMovies);
      this.groupBox1.Controls.Add(this.rbSubTVSeries);
      this.groupBox1.Location = new System.Drawing.Point(23, 12);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(231, 141);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "When Menu Item is Selected Display";
      // 
      // rbSubOff
      // 
      this.rbSubOff.AutoSize = true;
      this.rbSubOff.Location = new System.Drawing.Point(11, 22);
      this.rbSubOff.Name = "rbSubOff";
      this.rbSubOff.Size = new System.Drawing.Size(167, 17);
      this.rbSubOff.TabIndex = 5;
      this.rbSubOff.TabStop = true;
      this.rbSubOff.Text = "No Recently Added Displayed";
      this.rbSubOff.UseVisualStyleBackColor = true;
      // 
      // rbSubTV
      // 
      this.rbSubTV.AutoSize = true;
      this.rbSubTV.Enabled = false;
      this.rbSubTV.Location = new System.Drawing.Point(10, 115);
      this.rbSubTV.Name = "rbSubTV";
      this.rbSubTV.Size = new System.Drawing.Size(192, 17);
      this.rbSubTV.TabIndex = 4;
      this.rbSubTV.TabStop = true;
      this.rbSubTV.Text = "Most Recent Recorded TV Overlay";
      this.rbSubTV.UseVisualStyleBackColor = true;
      // 
      // rbSubMusic
      // 
      this.rbSubMusic.AutoSize = true;
      this.rbSubMusic.Enabled = false;
      this.rbSubMusic.Location = new System.Drawing.Point(11, 92);
      this.rbSubMusic.Name = "rbSubMusic";
      this.rbSubMusic.Size = new System.Drawing.Size(156, 17);
      this.rbSubMusic.TabIndex = 3;
      this.rbSubMusic.TabStop = true;
      this.rbSubMusic.Text = "Most Recent Music Overlay";
      this.rbSubMusic.UseVisualStyleBackColor = true;
      // 
      // rbSubMovies
      // 
      this.rbSubMovies.AutoSize = true;
      this.rbSubMovies.Location = new System.Drawing.Point(11, 69);
      this.rbSubMovies.Name = "rbSubMovies";
      this.rbSubMovies.Size = new System.Drawing.Size(162, 17);
      this.rbSubMovies.TabIndex = 1;
      this.rbSubMovies.TabStop = true;
      this.rbSubMovies.Text = "Most Recent Movie Overlays";
      this.rbSubMovies.UseVisualStyleBackColor = true;
      // 
      // rbSubTVSeries
      // 
      this.rbSubTVSeries.AutoSize = true;
      this.rbSubTVSeries.Location = new System.Drawing.Point(11, 46);
      this.rbSubTVSeries.Name = "rbSubTVSeries";
      this.rbSubTVSeries.Size = new System.Drawing.Size(176, 17);
      this.rbSubTVSeries.TabIndex = 0;
      this.rbSubTVSeries.TabStop = true;
      this.rbSubTVSeries.Text = "Most Recent TVSeries Overlays";
      this.rbSubTVSeries.UseVisualStyleBackColor = true;
      // 
      // btSaveAndClose
      // 
      this.btSaveAndClose.Location = new System.Drawing.Point(43, 159);
      this.btSaveAndClose.Name = "btSaveAndClose";
      this.btSaveAndClose.Size = new System.Drawing.Size(182, 23);
      this.btSaveAndClose.TabIndex = 1;
      this.btSaveAndClose.Text = "Save and Close";
      this.btSaveAndClose.UseVisualStyleBackColor = true;
      this.btSaveAndClose.Click += new System.EventHandler(this.btSaveAndClose_Click);
      // 
      // mostRecentDisplaySelection
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(284, 191);
      this.Controls.Add(this.btSaveAndClose);
      this.Controls.Add(this.groupBox1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Name = "mostRecentDisplaySelection";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "mostRecentDisplaySelection";
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.RadioButton rbSubTV;
    private System.Windows.Forms.RadioButton rbSubMusic;
    private System.Windows.Forms.RadioButton rbSubMovies;
    private System.Windows.Forms.RadioButton rbSubTVSeries;
    private System.Windows.Forms.Button btSaveAndClose;
    private System.Windows.Forms.RadioButton rbSubOff;
  }
}