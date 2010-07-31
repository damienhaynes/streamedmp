namespace SMPpatch
{
  partial class UpdateMessage
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
      this.lbCountDown = new System.Windows.Forms.Label();
      this.lbStatusMessage = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // lbCountDown
      // 
      this.lbCountDown.AutoSize = true;
      this.lbCountDown.Location = new System.Drawing.Point(111, 67);
      this.lbCountDown.Name = "lbCountDown";
      this.lbCountDown.Size = new System.Drawing.Size(128, 13);
      this.lbCountDown.TabIndex = 0;
      this.lbCountDown.Text = "Restarting MediaPortal (x)";
      // 
      // lbStatusMessage
      // 
      this.lbStatusMessage.AutoSize = true;
      this.lbStatusMessage.Location = new System.Drawing.Point(42, 30);
      this.lbStatusMessage.Name = "lbStatusMessage";
      this.lbStatusMessage.Size = new System.Drawing.Size(266, 13);
      this.lbStatusMessage.TabIndex = 1;
      this.lbStatusMessage.Text = "StreamedMP Sucessfully Updated to Version : x.x.x.xxx";
      // 
      // UpdateMessage
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(350, 116);
      this.Controls.Add(this.lbStatusMessage);
      this.Controls.Add(this.lbCountDown);
      this.MaximumSize = new System.Drawing.Size(366, 154);
      this.MinimumSize = new System.Drawing.Size(366, 154);
      this.Name = "UpdateMessage";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "UpdateMessage";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lbCountDown;
    private System.Windows.Forms.Label lbStatusMessage;



  }
}