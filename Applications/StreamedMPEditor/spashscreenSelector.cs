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
using System.Threading;
using ICSharpCode.SharpZipLib.Zip;

namespace StreamedMPEditor
{
  public partial class formStreamedMpEditor
  {
    string[] fileList = null;
    int imagePos = 0;
    int numOfImages = 0;
    Image workingImage = null;

    private bool splashScreensInstalled
    {
      get
      {
        if (Directory.Exists(SkinInfo.mpPaths.streamedMPpath + "media\\SplashScreens"))
        {
          fileList = getFileListing(SkinInfo.mpPaths.streamedMPpath + "media\\splashscreens", "*.*", true);
          numOfImages = fileList.Length;
          if (numOfImages > 0) return true;
        }
        return false;
      }
    }

    private void checkSplashScreens()
    {
      if (!splashScreensInstalled)
      {
        spashscreenPreview.Visible = false;
        gbSplashDL.Visible = true;

        btSplashNext.Enabled = false;
        btSplashPrev.Enabled = false;
        btSplashSelect.Enabled = false;
      }
      else
      {
        spashscreenPreview.Visible = true;
        gbSplashDL.Visible = false;
        GetSplashScreens();
      }
    }

    private void GetSplashScreens()
    {
      if (!splashScreensInstalled) return;

      showActiveSplashScreen();
      displayImage(fileList[imagePos]);
      btSplashNext.Enabled = true;
      btSplashPrev.Enabled = true;
      btSplashSelect.Enabled = true;
    }

    private void displayImage(string imageFile)
    {
      if (File.Exists(imageFile))
      {
        workingImage = Image.FromFile(imageFile);
        spashscreenPreview.Image = workingImage.GetThumbnailImage(650, 365, null, new IntPtr());
        workingImage.Dispose();
        toolStripStatusLabel2.Text = "SpashScreen Image " + (imagePos + 1).ToString() + " of " + numOfImages.ToString() + "   " + "[" + Path.GetFileName(imageFile) + "]";
      }
    }

    private void btSplashPrev_Click(object sender, EventArgs e)
    {
      if (fileList == null || numOfImages == 0) return;

      if (imagePos > 0)
        imagePos--;
      else
        imagePos = numOfImages - 1;

      displayImage(fileList[imagePos]);
    }

    private void btSplashNext_Click(object sender, EventArgs e)
    {
      if (fileList == null || numOfImages == 0) return;

      if ((imagePos + 1) < numOfImages)
        imagePos++;
      else
        imagePos = 0;

      displayImage(fileList[imagePos]);
    }

    private void btSplashSelect_Click(object sender, EventArgs e)
    {
      string sourceFile = fileList[imagePos];
      string destinationFile = SkinInfo.mpPaths.streamedMPpath + @"media\splashscreen.png";

      try
      {
        // Delete old splashscreen and copy newly selected one
        // Thumbnail in explorer wont update if overwritten
        if (File.Exists(destinationFile)) File.Delete(destinationFile);
        File.Copy(sourceFile, destinationFile);
      }
      catch (Exception ex)
      {
        MessageBox.Show(string.Format("Failed to set new splashscreen\n\n{0}", ex.Message), "Splashscreen", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      // Save Image filename for later 
      splashScreenImage = Path.GetFileName(fileList[imagePos]);
      showActiveSplashScreen();
      userConfirmation();
    }

    private void splashDownloadLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        if (!downloadActive)
        {
            optionDownloadURL = "https://github.com/damienhaynes/streamedmp/releases/download/BH/StreamedMP-SplashScreens-V1.0.zip";
            optionDownloadPath = System.IO.Path.GetTempPath() + "StreamedMP-SplashScreens-V1.0.zip";
            destinationPath = SkinInfo.mpPaths.skinBasePath;
            downloadForm.Text = "Download and Install Alternative SplashScreens";
            pLabel.Text = "Starting Download";
            theDownload = new Thread(Download);
            theDownload.Start();
            downloadForm.Show();
        }
        else
        {
            DialogResult result = helper.showError("Please wait until the current download has finished before continuing", errorCode.info);
            downloadForm.BringToFront();
        }
    }

    private void userConfirmation()
    {
      if (Properties.Settings.Default.hideSplashConfirm)
      {
        return;
      }

      userConfirm.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      userConfirm.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      userConfirm.Name = "frmUserConfirm";
      userConfirm.Text = "SplashScreen has been Configured";
      userConfirm.ControlBox = true;
      userConfirm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      userConfirm.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      userConfirm.StartPosition = FormStartPosition.CenterScreen;
      userConfirm.Width = 400;
      userConfirm.Height = 120;
      userConfirm.MaximizeBox = false;
      userConfirm.MinimizeBox = false;
      userConfirm.TopMost = true;

      btOk.Width = 80;
      btOk.Text = "OK";
      btOk.Location = new System.Drawing.Point(310, 65);
      btOk.Click += new System.EventHandler(btOk_Click);
      userConfirm.Controls.Add(btOk);

      cbShowAgain.Text = "Do not show this message again";
      cbShowAgain.AutoSize = true;
      cbShowAgain.Location = new System.Drawing.Point(10, 70);
      userConfirm.Controls.Add(cbShowAgain);

      tbInfo.BorderStyle = BorderStyle.None;
      tbInfo.Multiline = true;
      tbInfo.Text = "Splashscreen has been set, this action is immediate and does not require";
      tbInfo.AppendText(Environment.NewLine + "the Menu to be generated to take effect.");
      tbInfo.Location = new System.Drawing.Point(20, 20);
      tbInfo.WordWrap = true;
      tbInfo.Width = 350;
      tbInfo.Height = 60;
      tbInfo.ReadOnly = true;
      userConfirm.Controls.Add(tbInfo);

      userConfirm.Show();
    }

    private void btOk_Click(object sender, EventArgs e)
    {
      userConfirm.Hide();
      if (cbShowAgain.Checked)
        Properties.Settings.Default.hideSplashConfirm = true;
    }

    private void showActiveSplashScreen()
    {
      if (File.Exists(SkinInfo.mpPaths.streamedMPpath + @"media\splashscreen.png"))
      {
        workingImage = Image.FromFile(SkinInfo.mpPaths.streamedMPpath + @"media\splashscreen.png");
        pbActiveSplashScreen.Image = workingImage.GetThumbnailImage(100, 56, null, new IntPtr());
        workingImage.Dispose();
      }
    }
  }
}
