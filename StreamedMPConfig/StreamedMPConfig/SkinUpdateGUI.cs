﻿using System;
using System.Collections;
using System.IO;
using MediaPortal.GUI.Library;
using MediaPortal.Dialogs;
using System.Net;
using ICSharpCode.SharpZipLib.Zip;

namespace StreamedMPConfig
{
  public class SkinUpdateGUI : GUIWindow
  {
    #region Skin Connection

    [SkinControl((int)StreamedMPConfig.SkinControlIDs.cmc_ChangeLog)]
    protected GUITextControl cmc_ChangeLog = null;
    [SkinControl((int)StreamedMPConfig.SkinControlIDs.btDoUpdate)]
    protected GUIButtonControl btDoUpdate = null;

    #endregion

    #region Local Variables

    private static Stream strResponse;
    private static Stream strLocal;
    private static HttpWebRequest webRequest;
    private static HttpWebResponse webResponse;
    private static int PercentProgress;

    private static string optionDownloadURL = null;
    private static string optionDownloadPath = null;
    private static string destinationPath = null;

    #endregion

    #region Core Functions

    public override int GetID
    {
      get
      {
        return (int)StreamedMPConfig.SMPScreenID.SMPSkinUpdate;
      }
      set
      {
      }
    }
    public override bool Init()
    {
      return Load(GUIGraphicsContext.Skin + @"\StreamedMPConfig_update.xml");
    }

    public SkinUpdateGUI()
    {
      settings.Load();
    }

    protected override void OnPageLoad()
    {
      cmc_ChangeLog.Visible = false;
      btDoUpdate.Visible = false;
      settings.Load();
      if (checkForUpdate.checkIfUpdate())
      {
        updateFound.downloadChangeLog();
        System.Windows.Forms.RichTextBox rtBox = new System.Windows.Forms.RichTextBox();
        string s = System.IO.File.ReadAllText(Path.Combine(Path.GetTempPath(), "ChangeLog.rtf"));
        rtBox.Rtf = s;
        string plainText = rtBox.Text;
        cmc_ChangeLog.Label = plainText.ToString();
        cmc_ChangeLog.Visible = true;
        btDoUpdate.Visible = true;
      }
      else
      {
        GUIDialogOK dlg = (GUIDialogOK)GUIWindowManager.GetWindow((int)GUIWindow.Window.WINDOW_DIALOG_OK);
        dlg.SetHeading("StreamedMP Skin Update");
        dlg.SetLine(1, String.Empty);
        dlg.SetLine(2, "No Update Avaiable - Running Latest Release");
        dlg.SetLine(3, String.Empty);
        dlg.DoModal(GUIWindowManager.ActiveWindow);
        GUIWindowManager.ShowPreviousWindow();
      }
    }

    protected override void OnClicked(int controlId, GUIControl control, Action.ActionType actionType)
    {
      if (control == btDoUpdate)
      {
        installUpdateGUI(checkForUpdate.url);
      }
    }

    protected override void OnPageDestroy(int new_windowId)
    {
      settings.Save();
    }


    private void installUpdateGUI(string downloadURL)
    {
      SkinUpdateGUI skinUpdate = new SkinUpdateGUI();
      optionDownloadURL = downloadURL;
      optionDownloadPath = Path.Combine(Path.GetTempPath(), "SkinUpdate.zip");
      destinationPath = SkinInfo.mpPaths.skinBasePath;

      GUIDialogProgress progressDialog = (GUIDialogProgress)GUIWindowManager.GetWindow((int)GUIWindow.Window.WINDOW_DIALOG_PROGRESS);
      progressDialog.Reset();
      progressDialog.DisplayProgressBar = true;
      progressDialog.ShowWaitCursor = false;
      //progressDialog.DisableCancel(true);
      progressDialog.SetHeading("StreamedMP Update Download & Install");
      progressDialog.Percentage = 0;
      progressDialog.SetLine(1, string.Empty);
      progressDialog.SetLine(2, string.Empty);
      progressDialog.StartModal(skinUpdate.GetID);
      GUIWindowManager.Process();

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
          int upd = 0;
          int bytesSize = 0;
          byte[] downBuffer = new byte[2048];
          while ((bytesSize = strResponse.Read(downBuffer, 0, downBuffer.Length)) > 0)
          {
            strLocal.Write(downBuffer, 0, bytesSize);
            progressDialog.Percentage = Convert.ToInt32((strLocal.Length * 100) / fileSize);
            progressDialog.SetLine(2, "Downloaded " + strLocal.Length.ToString() + " out of " + fileSize.ToString() + " (" + PercentProgress.ToString() + "%)");
            ++upd;            
            // Only Update the progress bar every 1MB of downloaded data, any more offen slows the download to much.
            if (upd > 50)
            {
              GUIWindowManager.Process();
              upd = 0;
              if (progressDialog.IsCanceled)
                break;
            }
          }
        }
        catch (Exception e)
        {
          Log.Error("Error in Download" + e.Message);
        }
        finally
        {
          webResponse.Close();
          strResponse.Close();
          strLocal.Close();
          if (System.IO.File.Exists(optionDownloadPath) && !progressDialog.IsCanceled)
          {
            FastZip fz = new FastZip();
            fz.ExtractZip(optionDownloadPath, destinationPath, "");
            System.IO.File.Delete(optionDownloadPath);
          }
          progressDialog.Close();
        }
      }
    }

    #endregion
  }
}
