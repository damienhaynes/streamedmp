using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Net;
using System.Threading;
using ICSharpCode.SharpZipLib.Zip;

namespace StreamedMPEditor
{
  public partial class formStreamedMpEditor
  {
    private Thread theDownload;
    private Stream strResponse;
    private Stream strLocal;
    private HttpWebRequest webRequest;
    private HttpWebResponse webResponse;
    private static int PercentProgress;
    private delegate void UpdateProgessCallback(Int64 BytesRead, Int64 TotalBytes);

    string optionDownloadURL = null;
    string optionDownloadPath = null;
    string destinationPath = null;

    bool downloadActive = false;

    private void buildDownloadForm()
    {
      downloadForm.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      downloadForm.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      downloadForm.Name = "frmDownload";
      downloadForm.ControlBox = true;
      downloadForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      downloadForm.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      downloadForm.StartPosition = FormStartPosition.CenterScreen;
      downloadForm.Width = 400;
      downloadForm.Height = 120;
      pBar.Width = 350;
      downloadForm.MaximizeBox = false;
      downloadForm.MinimizeBox = false;
      downloadForm.TopMost = true;
      pBar.Location = new System.Drawing.Point(25, 10);
      downloadForm.Controls.Add(pBar);

      pLabel.Text = "Starting Download";
      pLabel.Location = new System.Drawing.Point(25, 40);
      pLabel.Width = 350;
      downloadForm.Controls.Add(pLabel);

      downloadStop.Text = "Cancel Install";
      downloadStop.Width = 150;
      downloadStop.Location = new System.Drawing.Point(230, 65);
      downloadStop.Click += new System.EventHandler(downloadStop_Click);
      downloadForm.Controls.Add(downloadStop);
      downloadForm.Enabled = false;
    }

    private void installAnimatedIcons_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      if (!downloadActive)
      {
        optionDownloadURL = "http://streamedmp.googlecode.com/files/StreamedMP_V1.0_AnimatedWeatherIcons.zip";
        optionDownloadPath = Path.Combine(Path.GetTempPath(),"StreamedMP_V1.0_AnimatedWeatherIcons.zip");
        destinationPath = SkinInfo.mpPaths.skinBasePath;
        downloadForm.Text = "Download and Install Animated Weather Icons";
        pLabel.Text = "Starting Download";
        theDownload = new Thread(Download);
        theDownload.Start();
        downloadForm.Show();
      }
      else
      {
        DialogResult result = helper.showError("Please wait until current download has finished before contining", errorCode.info);
        downloadForm.BringToFront();
      }
    }

    private void installWeatherBackgrounds_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      if (!downloadActive)
      {
        optionDownloadURL = "http://streamedmp.googlecode.com/files/StreamedMP_V1.0_LinkedWeatherBackgrounds.zip";
        optionDownloadPath = Path.Combine(Path.GetTempPath(),"StreamedMP_V1.0_LinkedWeatherBackgrounds.zip");
        destinationPath = SkinInfo.mpPaths.skinBasePath;
        downloadForm.Text = "Download and Install Weather Backgrounds";
        pLabel.Text = "Starting Download";
        theDownload = new Thread(Download);
        theDownload.Start();
        downloadForm.Show();
      }
      else
      {
        DialogResult result = helper.showError("Please wait until current download has finished before contining", errorCode.info);
        downloadForm.BringToFront();
      }
    }

    private void Download()
    {
      downloadActive = true;
      using (WebClient wcDownload = new WebClient())
      {
        try
        {
          downloadForm.Enabled = false;
          webRequest = (HttpWebRequest)WebRequest.Create(optionDownloadURL);
          webRequest.Credentials = CredentialCache.DefaultCredentials;
          webResponse = (HttpWebResponse)webRequest.GetResponse();
          Int64 fileSize = webResponse.ContentLength;
          strResponse = wcDownload.OpenRead(optionDownloadURL);
          strLocal = new FileStream(optionDownloadPath, FileMode.Create, FileAccess.Write, FileShare.None);
          int bytesSize = 0;
          byte[] downBuffer = new byte[2048];
          downloadForm.Enabled = true;
          while ((bytesSize = strResponse.Read(downBuffer, 0, downBuffer.Length)) > 0)
          {
            strLocal.Write(downBuffer, 0, bytesSize);
            this.Invoke(new UpdateProgessCallback(this.UpdateProgress), new object[] { strLocal.Length, fileSize });
          }
        }
        catch
        {
          //MessageBox.Show("Error in Download");
        }
        finally
        {
          webResponse.Close();
          strResponse.Close();
          strLocal.Close();
          this.Invoke(new MethodInvoker(extractAndCleanup));
          downloadActive = false;
        }
      }
    }

    private void UpdateProgress(Int64 BytesRead, Int64 TotalBytes)
    {
      PercentProgress = Convert.ToInt32((BytesRead * 100) / TotalBytes);
      pBar.Value = PercentProgress;
      pLabel.Text = "Downloaded " + BytesRead + " out of " + TotalBytes + " (" + PercentProgress + "%)";
    }

    private void downloadStop_Click(object sender, EventArgs e)
    {
      webResponse.Close();
      strResponse.Close();
      strLocal.Close();
      theDownload.Abort();

      pBar.Value = 0;
      if (!animatedIconsInstalled())
      {
        WeatherIconsAnimated.Enabled = false;
        WeatherIconsAnimated.Text = "Animated (Not Installed)";
        installAnimatedIcons.Visible = true;
      }
      File.Delete(optionDownloadPath);
    }

    private void extractAndCleanup()
    {
      if (File.Exists(optionDownloadPath))
      {
        FastZip fz = new FastZip();
        fz.ExtractZip(optionDownloadPath, destinationPath, "");
        File.Delete(optionDownloadPath);
        updateBackgroundFolders();
      }

      downloadForm.Hide();
      pBar.Value = 0;

      if (!animatedIconsInstalled())
      {
        WeatherIconsAnimated.Enabled = false;
        WeatherIconsAnimated.Text = "Animated (Not Installed)";
        installAnimatedIcons.Visible = true;
      }
      else
      {
        WeatherIconsAnimated.Enabled = true;
        WeatherIconsAnimated.Text = "Animated";
        installAnimatedIcons.Visible = false;
      }

      if (!weatherBackgoundsInstalled())
      {
        weatherBGlink.Checked = false;
        weatherBGlink.Enabled = false;
        weatherBGlink.Text = "Link Background to Current Weather (Not Installed)";
        installWeatherBackgrounds.Visible = true;
      }
      else
      {
        weatherBGlink.Enabled = true;
        weatherBGlink.Text = "Link Background to Current Weather";
        installWeatherBackgrounds.Visible = false;
      }

      if (!splashScreensInstalled)
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
  }
}