﻿namespace StreamedMPEditor
{
  partial class SubItemProperties
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
        this.cboTVSViews = new System.Windows.Forms.ComboBox();
        this.btSaveAndClose = new System.Windows.Forms.Button();
        this.gbHyperlinkParameter = new System.Windows.Forms.GroupBox();
        this.btClearParameter = new System.Windows.Forms.Button();
        this.gbHyperlinkParameter.SuspendLayout();
        this.SuspendLayout();
        // 
        // tbItemDisplayName
        // 
        this.tbItemDisplayName.Location = new System.Drawing.Point(148, 27);
        this.tbItemDisplayName.Name = "tbItemDisplayName";
        this.tbItemDisplayName.Size = new System.Drawing.Size(200, 20);
        this.tbItemDisplayName.TabIndex = 0;
        this.tbItemDisplayName.TextChanged += new System.EventHandler(this.tbItemDisplayName_TextChanged);
        this.tbItemDisplayName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbItemDisplayName_KeyUp);
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(17, 30);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(128, 13);
        this.label1.TabIndex = 1;
        this.label1.Text = "Menu Item Display Name:";
        // 
        // cboTVSViews
        // 
        this.cboTVSViews.FormattingEnabled = true;
        this.cboTVSViews.Location = new System.Drawing.Point(6, 19);
        this.cboTVSViews.Name = "cboTVSViews";
        this.cboTVSViews.Size = new System.Drawing.Size(188, 21);
        this.cboTVSViews.TabIndex = 3;
        this.cboTVSViews.SelectedIndexChanged += new System.EventHandler(this.cboTVSViews_SelectedIndexChanged);
        // 
        // btSaveAndClose
        // 
        this.btSaveAndClose.Location = new System.Drawing.Point(10, 124);
        this.btSaveAndClose.Name = "btSaveAndClose";
        this.btSaveAndClose.Size = new System.Drawing.Size(132, 23);
        this.btSaveAndClose.TabIndex = 5;
        this.btSaveAndClose.Text = "Save and Close";
        this.btSaveAndClose.UseVisualStyleBackColor = true;
        this.btSaveAndClose.Click += new System.EventHandler(this.btSaveAndClose_Click);
        // 
        // gbHyperlinkParameter
        // 
        this.gbHyperlinkParameter.Controls.Add(this.btClearParameter);
        this.gbHyperlinkParameter.Controls.Add(this.cboTVSViews);
        this.gbHyperlinkParameter.Location = new System.Drawing.Point(148, 63);
        this.gbHyperlinkParameter.Name = "gbHyperlinkParameter";
        this.gbHyperlinkParameter.Size = new System.Drawing.Size(200, 84);
        this.gbHyperlinkParameter.TabIndex = 6;
        this.gbHyperlinkParameter.TabStop = false;
        this.gbHyperlinkParameter.Text = "Hyperlink Parameter:";
        // 
        // btClearParameter
        // 
        this.btClearParameter.Location = new System.Drawing.Point(6, 46);
        this.btClearParameter.Name = "btClearParameter";
        this.btClearParameter.Size = new System.Drawing.Size(102, 23);
        this.btClearParameter.TabIndex = 4;
        this.btClearParameter.Text = "Clear Parameter";
        this.btClearParameter.UseVisualStyleBackColor = true;
        this.btClearParameter.Click += new System.EventHandler(this.btClearParameter_Click);
        // 
        // SubItemProperties
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(394, 158);
        this.Controls.Add(this.gbHyperlinkParameter);
        this.Controls.Add(this.btSaveAndClose);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.tbItemDisplayName);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.Name = "SubItemProperties";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Submenu Item Properties";
        this.gbHyperlinkParameter.ResumeLayout(false);
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox tbItemDisplayName;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox cboTVSViews;
    private System.Windows.Forms.Button btSaveAndClose;
    private System.Windows.Forms.GroupBox gbHyperlinkParameter;
    private System.Windows.Forms.Button btClearParameter;
  }
}