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
            System.IO.File.Copy(fileList[imagePos], mpPaths.streamedMPpath + "media//splashscreen.png", true);
            splashScreenImage = Path.GetFileName(fileList[imagePos]);
        }


        private void splashDownloadLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            optionDownloadURL = "http://streamedmp.googlecode.com/files/StreamedMP-test-splashscreens.zip";
            optionDownloadPath = System.IO.Path.GetTempPath() + "StreamedMP-test-splashscreens.zip";
            destinationPath = mpPaths.skinBasePath;
            downloadForm.Text = "Download and Install Alternative SplashScreens";
            pLabel.Text = "Starting Download";
            thrDownload = new Thread(Download);
            thrDownload.Start();
            downloadForm.Show();
        }
   }

}
