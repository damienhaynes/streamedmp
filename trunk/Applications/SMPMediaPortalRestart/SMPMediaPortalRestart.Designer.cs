﻿namespace SMPMediaPortalRestart
{
  partial class SMPMediaPortalRestart
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SMPMediaPortalRestart));
      this.pbSplashScreen = new System.Windows.Forms.PictureBox();
      this.lbStatus = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.pbSplashScreen)).BeginInit();
      this.SuspendLayout();
      // 
      // pbSplashScreen
      // 
      this.pbSplashScreen.Image = ((System.Drawing.Image)(resources.GetObject("pbSplashScreen.Image")));
      this.pbSplashScreen.Location = new System.Drawing.Point(0, 0);
      this.pbSplashScreen.Margin = new System.Windows.Forms.Padding(0);
      this.pbSplashScreen.Name = "pbSplashScreen";
      this.pbSplashScreen.Size = new System.Drawing.Size(1280, 720);
      this.pbSplashScreen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pbSplashScreen.TabIndex = 0;
      this.pbSplashScreen.TabStop = false;
      // 
      // lbStatus
      // 
      this.lbStatus.AutoSize = true;
      this.lbStatus.BackColor = System.Drawing.Color.Transparent;
      this.lbStatus.Font = new System.Drawing.Font("Fluid Title Caps", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbStatus.ForeColor = System.Drawing.Color.White;
      this.lbStatus.Location = new System.Drawing.Point(576, 467);
      this.lbStatus.Name = "lbStatus";
      this.lbStatus.Size = new System.Drawing.Size(0, 28);
      this.lbStatus.TabIndex = 1;
      // 
      // SMPMediaPortalRestart
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.ClientSize = new System.Drawing.Size(1280, 720);
      this.Controls.Add(this.lbStatus);
      this.Controls.Add(this.pbSplashScreen);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "SMPMediaPortalRestart";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Form1";
      this.TopMost = true;
      this.TransparencyKey = System.Drawing.Color.Transparent;
      this.Load += new System.EventHandler(this.SMPMediaPortalRestart_Load);
      ((System.ComponentModel.ISupportInitialize)(this.pbSplashScreen)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PictureBox pbSplashScreen;
    private System.Windows.Forms.Label lbStatus;
  }
}

