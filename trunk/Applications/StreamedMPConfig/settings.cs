using System;
using System.Collections.Generic;
using System.Globalization;
using MediaPortal.Configuration;
using MediaPortal.GUI.Library;
using MediaPortal.Util;
using System.Xml;
using System.IO;

namespace StreamedMPConfig
{
  class settings
  {
    #region Public methods


    public bool isVisEnabled 
    {
      get
      {
        return _isVisEnabled();
      }
    }

    public bool timerRequired
    {
      get
      {
        return _timerRequired();
      }
    }


    public static void Load()
    {
      Log.Info("StreamedMPConfig: Settings.Load()");
      using (MediaPortal.Profile.Settings xmlreader = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "StreamedMPConfig.xml")))
      {
        // Read user settings from configuration file

        if (xmlreader.GetValueAsInt("StreamedMPConfig", "cdCoverOnly", 1) != 0)
          StreamedMPConfig.cdCoverOnly = true;
        if (xmlreader.GetValueAsInt("StreamedMPConfig", "showEqGraphic", 1) != 0)
          StreamedMPConfig.showEqGraphic = true;
        if (xmlreader.GetValueAsInt("StreamedMPConfig", "fullVideoOSD", 1) != 0)
          StreamedMPConfig.fullVideoOSD = true;
        if (xmlreader.GetValueAsInt("StreamedMPConfig", "checkOnStart", 1) != 0)
          StreamedMPConfig.checkOnStart = true;
        if (xmlreader.GetValueAsInt("StreamedMPConfig", "checkForUpdateAt", 1) != 0)
          StreamedMPConfig.checkForUpdateAt = true;
        if (xmlreader.GetValueAsInt("StreamedMPConfig", "runPatchUtilityUnattended", 1) != 0)
          StreamedMPConfig.patchUtilityRunUnattended = true;
        if (xmlreader.GetValueAsInt("StreamedMPConfig", "patchUtilityRestartMP", 1) != 0)
          StreamedMPConfig.patchUtilityRestartMP = true;

        if (StreamedMPConfig.checkForUpdateAt)
        {
          string checkTime = xmlreader.GetValueAsString("StreamedMPConfig", "checkTime", "");

          if (string.IsNullOrEmpty(checkTime))
          {
            StreamedMPConfig.checkTime = DateTime.Parse("03:00");
            StreamedMPConfig.checkInterval = 1;
          }
          else
          {
            DateTime dtCheckTime = new DateTime();
            if (!DateTime.TryParse(checkTime, CultureInfo.CurrentCulture, DateTimeStyles.None, out dtCheckTime))
              dtCheckTime = DateTime.Now;
            StreamedMPConfig.checkTime = dtCheckTime;
            
            StreamedMPConfig.nextUpdateCheck = xmlreader.GetValueAsString("StreamedMPConfig", "nextUpdateCheck", "");
            StreamedMPConfig.checkInterval = xmlreader.GetValueAsInt("StreamedMPConfig", "checkInterval", 1);
          }
        }
      }
    }

    public static void Save()
    {
      Log.Info("StreamedMP: Settings.Save()");
      using (MediaPortal.Profile.Settings xmlwriter = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "StreamedMPConfig.xml")))
      {
        xmlwriter.SetValue("StreamedMPConfig", "cdCoverOnly", StreamedMPConfig.cdCoverOnly ? 1 : 0);
        xmlwriter.SetValue("StreamedMPConfig", "showEqGraphic", StreamedMPConfig.showEqGraphic ? 1 : 0);
        xmlwriter.SetValue("StreamedMPConfig", "fullVideoOSD", StreamedMPConfig.fullVideoOSD ? 1 : 0);
        xmlwriter.SetValue("StreamedMPConfig", "checkOnStart", StreamedMPConfig.checkOnStart ? 1 : 0);
        xmlwriter.SetValue("StreamedMPConfig", "checkForUpdateAt", StreamedMPConfig.checkForUpdateAt ? 1 : 0);
        xmlwriter.SetValue("StreamedMPConfig", "checkInterval", StreamedMPConfig.checkInterval);
        xmlwriter.SetValue("StreamedMPConfig", "checkTime", StreamedMPConfig.checkTime.ToShortTimeString());
        xmlwriter.SetValue("StreamedMPConfig", "nextUpdateCheck", StreamedMPConfig.nextUpdateCheck);
        xmlwriter.SetValue("StreamedMPConfig", "runPatchUtilityUnattended", StreamedMPConfig.patchUtilityRunUnattended ? 1 : 0);
        xmlwriter.SetValue("StreamedMPConfig", "patchUtilityRestartMP", StreamedMPConfig.patchUtilityRestartMP ? 1 : 0);

        MediaPortal.Profile.Settings.SaveCache();
      }
    }

    bool _isVisEnabled()
    {
      using (MediaPortal.Profile.Settings xmlreader = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "MediaPortal.xml")))
      {
        return xmlreader.GetValueAsBool("musicmisc", "showVisInNowPlaying", false);
      }
    }

    bool _timerRequired()
    {
      string usermenuprofile = SkinInfo.mpPaths.configBasePath + "usermenuprofile.xml";
      string optionsTag = "StreamedMP Options";
      XmlDocument doc = new XmlDocument();
      //
      // Open the usermenu settings file - NOTE: need to check for it in correct location, if not found look in skin dir for default version
      //
      if (!File.Exists(usermenuprofile))
      {
        // Ok, so no usermenuprofile.xml exists, this is most likely because this is a new skin install and this is the first time
        // the editor has been run and the file has not yet been created in the default location.
        // Check for and load the default version supplied with the skin
        usermenuprofile = SkinInfo.mpPaths.streamedMPpath + "usermenuprofile.xml";
        if (!File.Exists(usermenuprofile))
        {
          //ok, so now really in trouble, throw an error to the user and bailout!
          Log.Error("Can't find usermenuprofile.xml \r\r" + SkinInfo.mpPaths.configBasePath + "usermenuprofile.xml");
        }
      }
      try
      {
        doc.Load(usermenuprofile);
      }
      catch (Exception e)
      {
        Log.Error("Exception while loading usermenuprofile.xml\n\n" + e.Message);
        return false;
      }
      XmlNodeList nodelist = doc.DocumentElement.SelectNodes("/profile/skin");
      try
      {
        return bool.Parse(readEntryValue(optionsTag, "mostRecentCycleFanart", nodelist));
      }
      catch
      {
        Log.Error("StreamedMPConfig: Option mostRecentCycleFanart not present");
        return false;
      }

    }

    private string readEntryValue(string section, string elementName, XmlNodeList unodeList)
    {
      string entryValue;

      foreach (XmlNode node in unodeList)
      {
        XmlNode innerNode = node.Attributes.GetNamedItem("name");
        if (innerNode.InnerText == "StreamedMP")
        {
          XmlNodeList skinNodeList = node.SelectNodes("section");
          foreach (XmlNode skinNode in skinNodeList)
          {
            XmlNode skinNodeSection = skinNode.Attributes.GetNamedItem("name");
            if (skinNodeSection.InnerText == section)
            {
              XmlNode path = skinNode.SelectSingleNode("entry[@name=\"" + elementName + "\"]");
              if (path != null)
              {
                entryValue = path.InnerText;
                return entryValue;
              }
            }
          }
        }
      }
      return "false";
    }


    #endregion
  }
}
