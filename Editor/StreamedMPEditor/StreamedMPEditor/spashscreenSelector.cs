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
    public partial class streamedMpEditor
    {

        string[] fileList = null;

        int imagePos = 0;
        int numOfImages = 0;

        Image workingImage = null;

        private bool splashScreensInstalled()
        {
            if (Directory.Exists(mpPaths.streamedMPpath + "media\\SplashScreens"))
                return true;
            else
                return false;
        }

        private void checkSplashScreens()
        {

            if (!splashScreensInstalled())
            {
                spashscreenPreview.Visible = false;
                gbSplashDL.Visible = true;
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
            showActiveSplashScreen();
            fileList = getFileListing(mpPaths.streamedMPpath + "media\\splashscreens", "*.*");
            numOfImages = fileList.Length;

            // Display current chosen image - Do we really want to do this
            //foreach (string fileName in fileList)
            //{
            //    if (Path.GetFileName(fileName) == splashScreenImage)
            //    {
            //        break;
            //    }
            //    imagePos++;
            //}
            displayImage(fileList[imagePos]);
        }

        private void displayImage(string imageFile)
        {
            workingImage = Image.FromFile(imageFile);
            spashscreenPreview.Image = workingImage.GetThumbnailImage(650, 365, null, new IntPtr());
            toolStripStatusLabel2.Text = "SpashScreen Image " + (imagePos + 1).ToString() + " of " + numOfImages.ToString() + "   " + "[" + Path.GetFileName(imageFile) + "]";
        }

        private void btSplashPrev_Click(object sender, EventArgs e)
        {
            if (imagePos > 0)
                imagePos--;
            else
                imagePos = numOfImages - 1;

            displayImage(fileList[imagePos]);
        }

        private void btSplashNext_Click(object sender, EventArgs e)
        {
            if ((imagePos + 1) < numOfImages)
                imagePos++;
            else
                imagePos = 0;

            displayImage(fileList[imagePos]);
        }

        private void btSplashSelect_Click(object sender, EventArgs e)
        {
            string sourceFile = fileList[imagePos];
            string destinationFile = mpPaths.streamedMPpath + @"media\splashscreen.png";

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

            // Update Image Preview
            splashScreenImage = Path.GetFileName(fileList[imagePos]);
            showActiveSplashScreen();
            userConfirmation();
        }


        private void splashDownloadLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            optionDownloadURL = "http://streamedmp.googlecode.com/files/StreamedMP-SplashScreens-V1.0.zip";
            optionDownloadPath = System.IO.Path.GetTempPath() + "StreamedMP-SplashScreens-V1.0.zip";
            destinationPath = mpPaths.skinBasePath;
            downloadForm.Text = "Download and Install Alternative SplashScreens";
            pLabel.Text = "Starting Download";
            thrDownload = new Thread(Download);
            thrDownload.Start();
            downloadForm.Show();
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

            btOk.Width = 80;
            btOk.Text = "OK";
            btOk.Location = new System.Drawing.Point(310, 65);
            btOk.Click += new System.EventHandler(btOk_Click);
            userConfirm.Controls.Add(btOk);


            cbShowAgain.Text = "Do not show this message again";
            cbShowAgain.AutoSize = true;
            cbShowAgain.Location = new System.Drawing.Point(10, 70);
            cbShowAgain.Click += new System.EventHandler(cbShowAgain_Click);
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

        private void cbShowAgain_Click(object sender, EventArgs e)
        {

        }

        private void showActiveSplashScreen()
        {
            workingImage = Image.FromFile(mpPaths.streamedMPpath + "media//splashscreen.png");
            pbActiveSplashScreen.Image = workingImage.GetThumbnailImage(100, 56, null, new IntPtr());
            workingImage.Dispose();
        }

    }
}
