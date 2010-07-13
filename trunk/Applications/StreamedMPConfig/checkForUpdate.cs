﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Net;
using System.Threading;
using ICSharpCode.SharpZipLib.Zip;
using MediaPortal.Configuration;
using MediaPortal.GUI.Library;
using System.Diagnostics;

namespace StreamedMPConfig
{
  public class updateCheck
  {
    #region Variables

    private static Stream strResponse;
    private static Stream strLocal;
    private static HttpWebRequest webRequest;
    private static HttpWebResponse webResponse;
    private static int PercentProgress;

    private static string optionDownloadURL = null;
    private static string optionDownloadPath = null;
    private static string destinationPath = null;

    private static ProgressBar pBar = new ProgressBar();
    private static Label pLabel = new Label();
    private static Form downloadForm = new Form();
    public static Button downloadStop = new Button();
    private static XmlTextReader reader;

    public static Version newVersion = null;
    public static Version curVersion = null;
    public static string url = "";
    public static string changeLogFile;
    public static List<patches> patchList = new List<patches>();

    public class patches
    {
      public Version patchVersion;
      public Version minSkinVersionForPatch;
      public string patchURL;
      public string patchChangeLog;
    }

    public static bool abortDownload = false;

    #endregion

    #region Public methods

    // This section checks to see if there is a later version of the skin
    public static bool updateAvailable()
    {
      try
      {
        string elementName = "";
        string xmlURL = null;
        patchList.Clear();
        Version skinVersionIs = updateCheck.SkinVersion();

        // Allow for testing
        if (System.IO.File.Exists("C:\\SkinUpdate.xml"))
          xmlURL = "C:\\SkinUpdate.xml";
        else
          xmlURL = "http://streamedmp.googlecode.com/svn/trunk/SkinUpdate/SkinUpdate.xml";

        // Read the file
        reader = new XmlTextReader(xmlURL);
        reader.MoveToContent();
        if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "StreamedMP"))
        {
          while (reader.Read())
          {
            if (reader.NodeType == XmlNodeType.Element && reader.Name == "patch")
            {
              patches thisPatch = new patches();
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
                        thisPatch.patchVersion = new Version(reader.Value);
                        break;
                      case "minversion":
                        thisPatch.minSkinVersionForPatch = new Version(reader.Value);
                        break;
                      case "url":
                        thisPatch.patchURL = reader.Value;
                        break;
                      case "changelog":
                        if (System.IO.File.Exists("C:\\ChangeLog.rtf"))
                          thisPatch.patchChangeLog = "C:\\ChangeLog.rtf";
                        else
                          thisPatch.patchChangeLog = reader.Value;
                        break;
                    }
                  }
                }
                if (reader.NodeType == XmlNodeType.EndElement)
                {
                  if (reader.Name == "patch")
                  {
                    // Is this patch valid for the skin version we are running
                    if (skinVersionIs.CompareTo(thisPatch.minSkinVersionForPatch) >= 0)
                    {
                      // Only add patch if current skin version is less
                      if (skinVersionIs.CompareTo(thisPatch.patchVersion) < 0)
                        patchList.Add(thisPatch);
                    }
                    break;
                  }
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
      if (patchList.Count > 0)
      {
        StreamedMPConfig.udateAvailable = true;
        return true;
      }
      else
      {
        StreamedMPConfig.udateAvailable = false;
        return false;
      }
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

    public static void installUpdate()
    {
      buildDownloadForm();
      downloadForm.Show();

      // sort the list of patches with the oldest first - this is the order they will be insalled in
      updateCheck.patchList.Sort(delegate(updateCheck.patches p1, updateCheck.patches p2) { return p1.patchVersion.CompareTo(p2.patchVersion); });

      foreach (updateCheck.patches thePatch in updateCheck.patchList)
      {
        optionDownloadURL = thePatch.patchURL;
        optionDownloadPath = Path.Combine(Path.GetTempPath(), Path.GetFileName(optionDownloadURL));
        destinationPath = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(optionDownloadURL) + DateTime.Now.Ticks.ToString());

        //SkinInfo.mpPaths.configBasePath;
        downloadForm.Text = "Download and Install StreamedMP Update r" + thePatch.patchVersion.ToString();
        pLabel.Text = "Starting Download of Patch r" + thePatch.patchVersion.ToString();
        Cursor.Current = Cursors.WaitCursor;
        Download();
      }
      downloadForm.Hide();
      Cursor.Current = Cursors.Default;
      if (StreamedMPConfig.manualInstallNeeded)
      {
        string messageStr = Translation.mupdateline1 + " " + Translation.mupdateline2 + "\n\n";
        messageStr = messageStr + string.Format(Translation.mupdateline3, Path.GetFileName(optionDownloadPath)) + "\n\n" + Translation.mupdateline4;
        MessageBox.Show(messageStr, Translation.mupdateheader);
      }
    }


    #endregion

    #region Private methods

    public static void buildDownloadForm()
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
    }

    public static void Download()
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
          downloadForm.Refresh();
          while ((bytesSize = strResponse.Read(downBuffer, 0, downBuffer.Length)) > 0)
          {
            strLocal.Write(downBuffer, 0, bytesSize);
            PercentProgress = Convert.ToInt32((strLocal.Length * 100) / fileSize);
            pBar.Value = PercentProgress;
            pLabel.Text = "Downloaded " + strLocal.Length + " out of " + fileSize + " (" + PercentProgress + "%)";
            downloadForm.Refresh();
          }
        }
        catch { }
        finally
        {
          webResponse.Close();
          strResponse.Close();
          strLocal.Close();
          extractAndCleanup();
          downloadForm.Hide();
        }
      }
    }

    static void extractAndCleanup()
    {
      if (System.IO.File.Exists(optionDownloadPath))
      {
        if (Path.GetExtension(optionDownloadPath).ToLower() != ".zip")
        {
          if (Path.GetFileNameWithoutExtension(optionDownloadPath).ToLower().StartsWith("smppatch"))
          {
            //Lets run it
            if (File.Exists(optionDownloadPath))
            {
              ProcessStartInfo upgradeProcess = new ProcessStartInfo(optionDownloadPath);
              upgradeProcess.WorkingDirectory = Path.GetDirectoryName(optionDownloadPath);
              if (StreamedMPConfig.patchUtilityRunUnattended)
                upgradeProcess.Arguments = "/unattended ";
              if (StreamedMPConfig.patchUtilityRestartMP)
                upgradeProcess.Arguments += "/restartconfuration";
              System.Diagnostics.Process.Start(upgradeProcess);
              Application.Exit();
            }
          }
          else
          {
            // Not a zip so can't process internally - download to desktop and set flag so we can inform the user to exit MP and manually install the update
            System.IO.File.Copy(optionDownloadPath, Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), Path.GetFileName(optionDownloadPath)), true);
            System.IO.File.Delete(optionDownloadPath);
            StreamedMPConfig.manualInstallNeeded = true;
          }
        }
        else
        {
          FastZip fz = new FastZip();
          fz.ExtractZip(optionDownloadPath, destinationPath, "");
          System.IO.File.Delete(optionDownloadPath);
          StreamedMPConfig.manualInstallNeeded = false;
          // Now check what we have and copy to the right places.....
          StreamedMPConfig.checkAndCopy(destinationPath);
          Directory.Delete(destinationPath, true);
        }
      }
      pBar.Value = 0;
    }

    #endregion
  }
}
