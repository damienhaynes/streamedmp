using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Net;
using System.Threading;
using ICSharpCode.SharpZipLib.Zip;
using MediaPortal.Configuration;
using MediaPortal.GUI.Library;

namespace StreamedMPConfig
{
  public class checkForUpdate
  {
    #region Variables

    private static Thread thrDownload;
    private static Stream strResponse;
    private static Stream strLocal;
    private static HttpWebRequest webRequest;
    private static HttpWebResponse webResponse;
    private static int PercentProgress;
    private delegate void UpdateProgessCallback(Int64 BytesRead, Int64 TotalBytes);

    private static string optionDownloadURL = null;
    private static string optionDownloadPath = null;
    private static string destinationPath = null;

    private static ProgressBar pBar = new ProgressBar();
    private static Label pLabel = new Label();
    private static Form downloadForm = new Form();
    private static Button downloadStop = new Button();

    public static Version newVersion = null;
    public static Version curVersion = null;
    public static string url = "";
    public static string changeLogFile;
    private static XmlTextReader reader;

    #endregion

    #region Public methods

    public static void installUpdate(string downloadURL)
    {
      buildDownloadForm();
      optionDownloadURL = downloadURL;
      optionDownloadPath = Path.Combine(Path.GetTempPath(), "SkinUpdate.zip");
      destinationPath = SkinInfo.mpPaths.skinBasePath;
      downloadForm.Text = "Download and Install StreamedMP Update";
      pLabel.Text = "Starting Download";
      thrDownload = new Thread(Download);
      thrDownload.Start();
      downloadForm.Show();
    }

    // This section checks to see if there is a later version of the skin
    public static bool checkIfUpdate()
    {
      try
      {
        string xmlURL = null;
        // Allow for testing
        if (System.IO.File.Exists("C:\\SkinUpdate.xml"))
          xmlURL = "C:\\SkinUpdate.xml";
        else
          xmlURL = "http://streamedmp.googlecode.com/svn/trunk/SkinUpdate/SkinUpdate.xml";

        reader = new XmlTextReader(xmlURL);
        reader.MoveToContent();
        string elementName = "";
        if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "StreamedMP"))
        {
          while (reader.Read())
          {
            if (reader.NodeType == XmlNodeType.Element)
              elementName = reader.Name;
            else
            {
              if ((reader.NodeType == XmlNodeType.Text) && (reader.HasValue))
              {
                switch (elementName)
                {
                  case "version":
                    newVersion = new Version(reader.Value);
                    break;
                  case "url":
                    url = reader.Value;
                    break;
                  case "changelog":
                    if (System.IO.File.Exists("C:\\ChangeLog.rtf"))
                      changeLogFile = "C:\\ChangeLog.rtf";
                    else
                      changeLogFile = reader.Value;
                    break;
                }
              }
            }
          }
        }
      }
      catch (Exception e)
      {
        Log.Error("Exception while attempting to read upgrade xml file\n\n" + e.Message);
      }
      finally
      {
        if (reader != null) reader.Close();
      }
      if (checkForUpdate.SkinVersion().CompareTo(newVersion) < 0)
        return true;
      else
        return false;
    }

    public static Version SkinVersion()
    {
      try
      {
        string xmlURL = SkinInfo.mpPaths.streamedMPpath + "streamedmp.version.xml";
        reader = new XmlTextReader(xmlURL);
        reader.MoveToContent();
        string elementName = "";
        if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "window"))
        {
          while (reader.Read())
          {
            if (reader.NodeType == XmlNodeType.Element)
              elementName = reader.Name;
            else
            {
              if ((reader.NodeType == XmlNodeType.Text) && (reader.HasValue))
              {
                if (elementName == "label")
                {
                  string aa = reader.Value.Substring(12);
                  curVersion = new Version(reader.Value.Substring(12));
                }
              }
            }
          }
        }
      }
      catch (Exception e)
      {
        Log.Error("Exception while attempting to read upgrade xml file\n\n" + e.Message);
      }
      finally
      {
        if (reader != null) reader.Close();
      }
      return curVersion;
    }

    #endregion

    #region Private methods

    static void buildDownloadForm()
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
    }

    static void Download()
    {
      using (WebClient wcDownload = new WebClient())
      {
        try
        {
          webRequest = (HttpWebRequest)WebRequest.Create(optionDownloadURL);
          webRequest.Credentials = CredentialCache.DefaultCredentials;
          webResponse = (HttpWebResponse)webRequest.GetResponse();
          Int64 fileSize = webResponse.ContentLength;
          strResponse = wcDownload.OpenRead(optionDownloadURL);
          strLocal = new FileStream(optionDownloadPath, FileMode.Create, FileAccess.Write, FileShare.None);
          int bytesSize = 0;
          byte[] downBuffer = new byte[2048];
          while ((bytesSize = strResponse.Read(downBuffer, 0, downBuffer.Length)) > 0)
          {
            strLocal.Write(downBuffer, 0, bytesSize);
            downloadForm.Invoke(new UpdateProgessCallback(checkForUpdate.UpdateProgress), new object[] { strLocal.Length, fileSize });
          }
        }
        catch { }
        finally
        {
          webResponse.Close();
          strResponse.Close();
          strLocal.Close();
          downloadForm.Invoke(new MethodInvoker(extractAndCleanup));
        }
      }
    }

    static void UpdateProgress(Int64 BytesRead, Int64 TotalBytes)
    {
      PercentProgress = Convert.ToInt32((BytesRead * 100) / TotalBytes);
      pBar.Value = PercentProgress;
      pLabel.Text = "Downloaded " + BytesRead + " out of " + TotalBytes + " (" + PercentProgress + "%)";
    }

    static void downloadStop_Click(object sender, EventArgs e)
    {
      webResponse.Close();
      strResponse.Close();
      strLocal.Close();
      thrDownload.Abort();
      pBar.Value = 0;
      System.IO.File.Delete(optionDownloadPath);
      downloadForm.Hide();
    }

    static void extractAndCleanup()
    {
      if (System.IO.File.Exists(optionDownloadPath))
      {
        FastZip fz = new FastZip();
        fz.ExtractZip(optionDownloadPath, destinationPath, "");
        System.IO.File.Delete(optionDownloadPath);
      }
      downloadForm.Hide();
      pBar.Value = 0;
    }

    #endregion
  }
}
