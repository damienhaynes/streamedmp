using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Reflection;
using Microsoft.Win32;
using System.Security;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;


namespace StreamedMPConfig
{
  public static class updateFound
  {


    private static Form changlogForm = new Form();
    private static RichTextBox changeLog = new RichTextBox();
 
    private static Button okButton = new Button();
    private static Button cancelButton = new Button();

    [DllImport("user32.dll", EntryPoint = "HideCaret")]
    public static extern long HideCaret(IntPtr hwnd);

    public static void displayDetail()
    {    
      changlogForm.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      changlogForm.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      changlogForm.Name = "frmDownloadDetail";
      changlogForm.ControlBox = true;
      changlogForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      changlogForm.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      changlogForm.StartPosition = FormStartPosition.CenterScreen;
      changlogForm.Text = "StreamedMP Skin Update - Change log";
      changlogForm.Width = 600;
      changlogForm.Height = 300;
      changlogForm.MaximizeBox = false;
      changlogForm.MinimizeBox = false;
      changlogForm.TopMost = true;

      changeLog.Name = "tboxChangeLog";
      changeLog.Location = new System.Drawing.Point(10, 10);
      changeLog.Width = 580;
      changeLog.Height = 200;
      changeLog.Multiline = true;
      changeLog.WordWrap = true;
      changeLog.ReadOnly = true; 

      WebClient client = new WebClient();
      client.DownloadFile(checkForUpdate.changeLogFile, Path.Combine(Path.GetTempPath(), "ChangeLog.rtf"));
      changeLog.LoadFile(Path.Combine(Path.GetTempPath(), "ChangeLog.rtf"),RichTextBoxStreamType.RichText);
      
      changeLog.ScrollBars = RichTextBoxScrollBars.Vertical;
      changeLog.SelectionStart = 0;

      changlogForm.Controls.Add(changeLog);

      okButton.Text = "Install Update";
      okButton.Width = 120;
      okButton.Location = new System.Drawing.Point(20, 240);
      okButton.Click += new System.EventHandler(okButton_Click);
      okButton.Focus();
      changlogForm.Controls.Add(okButton);


      cancelButton.Text = "Cancel";
      cancelButton.Width = 120;
      cancelButton.Location = new System.Drawing.Point(460, 240);
      cancelButton.Click += new System.EventHandler(cancelButton_Click);
      changlogForm.Controls.Add(cancelButton);


      changlogForm.Show();
      HideCaret(changlogForm.Handle);


    }

    private static void okButton_Click(object sender, EventArgs e)
    {
      checkForUpdate.installUpdate(checkForUpdate.url);
    }

    private static void cancelButton_Click(object sender, EventArgs e)
    {
      changlogForm.Hide();
    }


  }
}
