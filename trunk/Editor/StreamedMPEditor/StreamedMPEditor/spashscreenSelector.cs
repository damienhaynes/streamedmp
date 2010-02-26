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

        private bool IsSplashScreensInstalled
        {
            get
            {
                if (Directory.Exists(mpPaths.streamedMPpath + "media\\SplashScreens"))
                {
                    fileList = getFileListing(mpPaths.streamedMPpath + "media\\splashscreens", "*.*");
                    numOfImages = fileList.Length;
                    if (numOfImages > 0) return true;
                }                
                return false;
            }
        }

        private void checkSplashScreens()
        {
            if (!IsSplashScreensInstalled)
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
            if (!IsSplashScreensInstalled) return;

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

            // Save Image filename for later
            splashScreenImage = Path.GetFileName(fileList[imagePos]);
        }


        private void splashDownloadLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!downloadActive)
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
            else
            {
                DialogResult result = showError("Please wait till current download has finished before contining", errorCode.info);
                downloadForm.BringToFront();
            }
        }

   }

}
