namespace StreamedMPEditor
{
    partial class tvSeriesParameter
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
            this.cboTVSViews = new System.Windows.Forms.ComboBox();
            this.btSaveAndClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cboTVSViews
            // 
            this.cboTVSViews.FormattingEnabled = true;
            this.cboTVSViews.Location = new System.Drawing.Point(88, 13);
            this.cboTVSViews.Name = "cboTVSViews";
            this.cboTVSViews.Size = new System.Drawing.Size(214, 21);
            this.cboTVSViews.TabIndex = 0;
            // 
            // btSaveAndClose
            // 
            this.btSaveAndClose.Location = new System.Drawing.Point(67, 53);
            this.btSaveAndClose.Name = "btSaveAndClose";
            this.btSaveAndClose.Size = new System.Drawing.Size(164, 23);
            this.btSaveAndClose.TabIndex = 1;
            this.btSaveAndClose.Text = "Save and Close";
            this.btSaveAndClose.UseVisualStyleBackColor = true;
            this.btSaveAndClose.Click += new System.EventHandler(this.SaveAndClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Startup View:";
            // 
            // tvSeriesParameter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 88);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btSaveAndClose);
            this.Controls.Add(this.cboTVSViews);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "tvSeriesParameter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Startup View";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboTVSViews;
        private System.Windows.Forms.Button btSaveAndClose;
        private System.Windows.Forms.Label label1;
    }
}