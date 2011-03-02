namespace GenerateTester
{
  partial class GenerateTester
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
      this.btGo = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // btGo
      // 
      this.btGo.Location = new System.Drawing.Point(120, 36);
      this.btGo.Name = "btGo";
      this.btGo.Size = new System.Drawing.Size(177, 23);
      this.btGo.TabIndex = 0;
      this.btGo.Text = "Go!";
      this.btGo.UseVisualStyleBackColor = true;
      this.btGo.Click += new System.EventHandler(this.btGo_Click);
      // 
      // GenerateTester
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(451, 106);
      this.Controls.Add(this.btGo);
      this.Name = "GenerateTester";
      this.Text = "Menu Generation Test";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btGo;
  }
}

