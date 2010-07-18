namespace SMPRefPatch
{
  partial class fmRefPatch
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
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.appExit = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(15, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(295, 20);
      this.label1.TabIndex = 0;
      this.label1.Text = "StreamedMP referances.xml updated for";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(56, 45);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(212, 20);
      this.label2.TabIndex = 1;
      this.label2.Text = "MediaPortal Version V1.1";
      // 
      // appExit
      // 
      this.appExit.Location = new System.Drawing.Point(134, 86);
      this.appExit.Name = "appExit";
      this.appExit.Size = new System.Drawing.Size(56, 23);
      this.appExit.TabIndex = 2;
      this.appExit.Text = "Close";
      this.appExit.UseVisualStyleBackColor = true;
      this.appExit.Click += new System.EventHandler(this.appExit_Click);
      // 
      // fmRefPatch
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(322, 121);
      this.Controls.Add(this.appExit);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Name = "fmRefPatch";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "StreamedMP Referances Patch";
      this.Load += new System.EventHandler(this.fmRefPatch_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button appExit;
  }
}

