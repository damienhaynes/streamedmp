namespace StreamedMPEditor
{
  partial class formSubMenuDesigner
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
      this.lboxSubmenuXMLFiles = new System.Windows.Forms.ListBox();
      this.lboxSubMenuLevel1 = new System.Windows.Forms.ListBox();
      this.lboxSubMenuLevel2 = new System.Windows.Forms.ListBox();
      this.lbBaseMenuItem = new System.Windows.Forms.Label();
      this.pEditingInfo = new System.Windows.Forms.Panel();
      this.pEditingInfo.SuspendLayout();
      this.SuspendLayout();
      // 
      // lboxSubmenuXMLFiles
      // 
      this.lboxSubmenuXMLFiles.FormattingEnabled = true;
      this.lboxSubmenuXMLFiles.Location = new System.Drawing.Point(12, 12);
      this.lboxSubmenuXMLFiles.Name = "lboxSubmenuXMLFiles";
      this.lboxSubmenuXMLFiles.Size = new System.Drawing.Size(216, 407);
      this.lboxSubmenuXMLFiles.TabIndex = 0;
      // 
      // lboxSubMenuLevel1
      // 
      this.lboxSubMenuLevel1.FormattingEnabled = true;
      this.lboxSubMenuLevel1.Location = new System.Drawing.Point(537, 12);
      this.lboxSubMenuLevel1.Name = "lboxSubMenuLevel1";
      this.lboxSubMenuLevel1.Size = new System.Drawing.Size(216, 186);
      this.lboxSubMenuLevel1.TabIndex = 1;
      // 
      // lboxSubMenuLevel2
      // 
      this.lboxSubMenuLevel2.FormattingEnabled = true;
      this.lboxSubMenuLevel2.Location = new System.Drawing.Point(537, 220);
      this.lboxSubMenuLevel2.Name = "lboxSubMenuLevel2";
      this.lboxSubMenuLevel2.Size = new System.Drawing.Size(216, 199);
      this.lboxSubMenuLevel2.TabIndex = 2;
      // 
      // lbBaseMenuItem
      // 
      this.lbBaseMenuItem.AutoSize = true;
      this.lbBaseMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaption;
      this.lbBaseMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbBaseMenuItem.Location = new System.Drawing.Point(1, 3);
      this.lbBaseMenuItem.Name = "lbBaseMenuItem";
      this.lbBaseMenuItem.Size = new System.Drawing.Size(157, 15);
      this.lbBaseMenuItem.TabIndex = 3;
      this.lbBaseMenuItem.Text = "Editing Sub Menus for :";
      // 
      // pEditingInfo
      // 
      this.pEditingInfo.BackColor = System.Drawing.SystemColors.ActiveCaption;
      this.pEditingInfo.Controls.Add(this.lbBaseMenuItem);
      this.pEditingInfo.Location = new System.Drawing.Point(234, 12);
      this.pEditingInfo.Name = "pEditingInfo";
      this.pEditingInfo.Size = new System.Drawing.Size(297, 25);
      this.pEditingInfo.TabIndex = 4;
      // 
      // formSubMenuDesigner
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(765, 430);
      this.Controls.Add(this.lboxSubMenuLevel2);
      this.Controls.Add(this.lboxSubMenuLevel1);
      this.Controls.Add(this.lboxSubmenuXMLFiles);
      this.Controls.Add(this.pEditingInfo);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Name = "formSubMenuDesigner";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Sub Menu Designer";
      this.pEditingInfo.ResumeLayout(false);
      this.pEditingInfo.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ListBox lboxSubmenuXMLFiles;
    private System.Windows.Forms.ListBox lboxSubMenuLevel1;
    private System.Windows.Forms.ListBox lboxSubMenuLevel2;
    private System.Windows.Forms.Label lbBaseMenuItem;
    private System.Windows.Forms.Panel pEditingInfo;
  }
}