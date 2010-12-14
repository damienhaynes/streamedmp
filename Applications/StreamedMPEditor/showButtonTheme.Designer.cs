namespace StreamedMPEditor
{
  partial class showButtonTheme
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
      this.flThemeButtons = new System.Windows.Forms.FlowLayoutPanel();
      this.themePreview = new System.Windows.Forms.PictureBox();
      this.cboSelectTheme = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.btSetActiveTheme = new System.Windows.Forms.Button();
      this.btClose = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.themePreview)).BeginInit();
      this.SuspendLayout();
      // 
      // flThemeButtons
      // 
      this.flThemeButtons.AutoScroll = true;
      this.flThemeButtons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.flThemeButtons.Location = new System.Drawing.Point(266, 12);
      this.flThemeButtons.Name = "flThemeButtons";
      this.flThemeButtons.Size = new System.Drawing.Size(460, 302);
      this.flThemeButtons.TabIndex = 0;
      // 
      // themePreview
      // 
      this.themePreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.themePreview.Location = new System.Drawing.Point(12, 39);
      this.themePreview.Name = "themePreview";
      this.themePreview.Size = new System.Drawing.Size(248, 140);
      this.themePreview.TabIndex = 1;
      this.themePreview.TabStop = false;
      // 
      // cboSelectTheme
      // 
      this.cboSelectTheme.FormattingEnabled = true;
      this.cboSelectTheme.Items.AddRange(new object[] {
            "Black",
            "Blue"});
      this.cboSelectTheme.Location = new System.Drawing.Point(116, 12);
      this.cboSelectTheme.Name = "cboSelectTheme";
      this.cboSelectTheme.Size = new System.Drawing.Size(144, 21);
      this.cboSelectTheme.TabIndex = 2;
      this.cboSelectTheme.Text = "Black";
      this.cboSelectTheme.SelectedIndexChanged += new System.EventHandler(this.cboSelectTheme_SelectedIndexChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(37, 15);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(73, 13);
      this.label1.TabIndex = 3;
      this.label1.Text = "Select Theme";
      // 
      // btSetActiveTheme
      // 
      this.btSetActiveTheme.Location = new System.Drawing.Point(12, 185);
      this.btSetActiveTheme.Name = "btSetActiveTheme";
      this.btSetActiveTheme.Size = new System.Drawing.Size(247, 23);
      this.btSetActiveTheme.TabIndex = 4;
      this.btSetActiveTheme.Text = "Set as Active Theme and Close";
      this.btSetActiveTheme.UseVisualStyleBackColor = true;
      this.btSetActiveTheme.Click += new System.EventHandler(this.btSetActiveTheme_Click);
      // 
      // btClose
      // 
      this.btClose.Location = new System.Drawing.Point(12, 214);
      this.btClose.Name = "btClose";
      this.btClose.Size = new System.Drawing.Size(247, 23);
      this.btClose.TabIndex = 5;
      this.btClose.Text = "Exit without Setting Theme";
      this.btClose.UseVisualStyleBackColor = true;
      this.btClose.Click += new System.EventHandler(this.btClose_Click);
      // 
      // showButtonTheme
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(738, 326);
      this.Controls.Add(this.btClose);
      this.Controls.Add(this.btSetActiveTheme);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.cboSelectTheme);
      this.Controls.Add(this.themePreview);
      this.Controls.Add(this.flThemeButtons);
      this.Name = "showButtonTheme";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "showButtonTheme";
      ((System.ComponentModel.ISupportInitialize)(this.themePreview)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.FlowLayoutPanel flThemeButtons;
    private System.Windows.Forms.PictureBox themePreview;
    private System.Windows.Forms.ComboBox cboSelectTheme;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btSetActiveTheme;
    private System.Windows.Forms.Button btClose;
  }
}