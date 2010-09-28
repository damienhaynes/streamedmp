namespace StreamedMPEditor
{
  partial class ItemNameDialog
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
      this.tbItemDisplayName = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // tbItemDisplayName
      // 
      this.tbItemDisplayName.Location = new System.Drawing.Point(148, 27);
      this.tbItemDisplayName.Name = "tbItemDisplayName";
      this.tbItemDisplayName.Size = new System.Drawing.Size(188, 20);
      this.tbItemDisplayName.TabIndex = 0;
      this.tbItemDisplayName.TextChanged += new System.EventHandler(this.tbItemDisplayName_TextChanged);
      this.tbItemDisplayName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbItemDisplayName_KeyUp);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(17, 30);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(125, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Menu Item Display Name";
      // 
      // ItemNameDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(367, 80);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.tbItemDisplayName);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Name = "ItemNameDialog";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Enter Item Display Name";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox tbItemDisplayName;
    private System.Windows.Forms.Label label1;
  }
}