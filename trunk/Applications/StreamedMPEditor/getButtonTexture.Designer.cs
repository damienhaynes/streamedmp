namespace StreamedMPEditor
{
  partial class getButtonTexture
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
      this.flIconPanel = new System.Windows.Forms.FlowLayoutPanel();
      this.lbMenuItem = new System.Windows.Forms.Label();
      this.pbCurrentIcon = new System.Windows.Forms.PictureBox();
      this.label1 = new System.Windows.Forms.Label();
      this.btSaveAndExit = new System.Windows.Forms.Button();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.textBox1 = new System.Windows.Forms.TextBox();
      ((System.ComponentModel.ISupportInitialize)(this.pbCurrentIcon)).BeginInit();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.SuspendLayout();
      // 
      // flIconPanel
      // 
      this.flIconPanel.AutoScroll = true;
      this.flIconPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.flIconPanel.Location = new System.Drawing.Point(6, 18);
      this.flIconPanel.Name = "flIconPanel";
      this.flIconPanel.Size = new System.Drawing.Size(462, 218);
      this.flIconPanel.TabIndex = 0;
      // 
      // lbMenuItem
      // 
      this.lbMenuItem.AutoSize = true;
      this.lbMenuItem.Location = new System.Drawing.Point(154, 12);
      this.lbMenuItem.Name = "lbMenuItem";
      this.lbMenuItem.Size = new System.Drawing.Size(108, 13);
      this.lbMenuItem.TabIndex = 3;
      this.lbMenuItem.Text = "<displays menu Item>";
      // 
      // pbCurrentIcon
      // 
      this.pbCurrentIcon.Location = new System.Drawing.Point(10, 20);
      this.pbCurrentIcon.Name = "pbCurrentIcon";
      this.pbCurrentIcon.Size = new System.Drawing.Size(128, 128);
      this.pbCurrentIcon.TabIndex = 4;
      this.pbCurrentIcon.TabStop = false;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(11, 12);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(137, 13);
      this.label1.TabIndex = 6;
      this.label1.Text = "Editing Icon for Menu Item: ";
      // 
      // btSaveAndExit
      // 
      this.btSaveAndExit.Location = new System.Drawing.Point(12, 130);
      this.btSaveAndExit.Name = "btSaveAndExit";
      this.btSaveAndExit.Size = new System.Drawing.Size(172, 23);
      this.btSaveAndExit.TabIndex = 7;
      this.btSaveAndExit.Text = "Save and Close";
      this.btSaveAndExit.UseVisualStyleBackColor = true;
      this.btSaveAndExit.Click += new System.EventHandler(this.btSaveAndExit_Click);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.pbCurrentIcon);
      this.groupBox1.Location = new System.Drawing.Point(338, 12);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(148, 159);
      this.groupBox1.TabIndex = 8;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Selected Icon";
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.flIconPanel);
      this.groupBox2.Location = new System.Drawing.Point(12, 177);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(474, 242);
      this.groupBox2.TabIndex = 9;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Available Menu Icons";
      // 
      // textBox1
      // 
      this.textBox1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
      this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.textBox1.Location = new System.Drawing.Point(12, 46);
      this.textBox1.Margin = new System.Windows.Forms.Padding(5);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(314, 68);
      this.textBox1.TabIndex = 10;
      this.textBox1.Text = "This screen shows the default or previously configured icon\r\nfor this menu item.\r" +
          "\n\r\nTo change please select a Icon from the this list below.";
      // 
      // getButtonTexture
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(498, 431);
      this.Controls.Add(this.textBox1);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.btSaveAndExit);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.lbMenuItem);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Name = "getButtonTexture";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Graphical menu - Select Icon for Menu Item";
      ((System.ComponentModel.ISupportInitialize)(this.pbCurrentIcon)).EndInit();
      this.groupBox1.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.FlowLayoutPanel flIconPanel;
    private System.Windows.Forms.Label lbMenuItem;
    private System.Windows.Forms.PictureBox pbCurrentIcon;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btSaveAndExit;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.TextBox textBox1;
  }
}