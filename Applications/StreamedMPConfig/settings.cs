
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
    private static readonly logger smcLog = logger.GetInstance();

    #region Public methods

    public bool logLevelError
    {
      get
      {
        if (_logLevel() >= 0)
          return true;
        else
          return false;
      }
    }

    public bool logLevelWarning
    {
      get
      {
        if (_logLevel() >= 1)
          return true;
        else
          return false;
      }
    }

    public bool logLevelDebug
    {
      get
      {
        if (_logLevel() == 3)
          return true;
        else
          return false;
      }
    }

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

    public bool mrSeasonEpisodeStyle2
    {
      get
      {
        return _mrSeasonEpisodeStyle2();
      }
    }

    public bool mpSetAsFullScreen
    {
      get
      {
        return _mpSetAsFullScreen();
      }
    }

    public static void Load(string section)
    {
      smcLog.WriteLog(string.Format("StreamedMPConfig: Settings.Load({0})", section), LogLevel.Info);
      using (MediaPortal.Profile.Settings xmlreader = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "StreamedMPConfig.xml")))
      {
        // Read user settings from configuration file
        switch (section)
        {
          #region StreamedMPConfig
          case "StreamedMPConfig":
            if (xmlreader.GetValueAsInt(section, "fullVideoOSD", 1) != 0)
              StreamedMPConfig.fullVideoOSD = true;
            if (xmlreader.GetValueAsInt(section, "checkOnStart", 1) != 0)
              StreamedMPConfig.checkOnStart = true;
            if (xmlreader.GetValueAsInt(section, "checkForUpdateAt", 1) != 0)
              StreamedMPConfig.checkForUpdateAt = true;
            if (xmlreader.GetValueAsInt(section, "runPatchUtilityUnattended", 1) != 0)
              StreamedMPConfig.patchUtilityRunUnattended = true;
            if (xmlreader.GetValueAsInt(section, "patchUtilityRestartMP", 1) != 0)
              StreamedMPConfig.patchUtilityRestartMP = true;
            StreamedMPConfig.mrFanartTimer = xmlreader.GetValueAsInt(section, "mostRecentTimer", 7);


            if (StreamedMPConfig.checkForUpdateAt)
            {
              string checkTime = xmlreader.GetValueAsString(section, "checkTime", "");

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

                StreamedMPConfig.nextUpdateCheck = xmlreader.GetValueAsString(section, "nextUpdateCheck", "");
                StreamedMPConfig.checkInterval = xmlreader.GetValueAsInt(section, "checkInterval", 1);
              }
            }
            break;
          #endregion

          #region Music
          case "MusicConfigGUI":
            if (xmlreader.GetValueAsInt(section, "cdCoverOnly", 1) != 0)
              StreamedMPConfig.cdCoverOnly = true;
            if (xmlreader.GetValueAsInt(section, "showEqGraphic", 1) != 0)
              StreamedMPConfig.showEqGraphic = true;
            StreamedMPConfig.nowPlayingStyle = xmlreader.GetValueAsInt(section, "nowPlayingStyle", 0);
            break;
          #endregion

          #region TVSeries
          case "TVSeriesConfigGUI":
            TVSeriesConfigGUI.IsDefaultStyle = xmlreader.GetValueAsInt(section, "tvseriesDefaultStyle", 1) == 1;
            TVSeriesConfigGUI.WideBannerMod = (TVSeriesConfigGUI.WideBanners)xmlreader.GetValueAsInt(section, "tvseriesWideBannerMod", 0);
            break;
          #endregion

          #region MovingPictures
          case "MovingPicturesConfigGUI":
            MovingPicturesConfigGUI.IsDefaultStyle = xmlreader.GetValueAsInt(section, "movingpicturesDefaultStyle", 1) == 1;
            MovingPicturesConfigGUI.ThumbnailMod = (MovingPicturesConfigGUI.Thumbnails)xmlreader.GetValueAsInt(section, "movingpicturesThumbMod", 0);
            break;
          #endregion

          #region Music
          case "MusicGUI":
            break;
          #endregion

          #region TV
          case "TVConfigGUI":
            TVConfigGUI.TVGuideRowSize = (TVConfigGUI.TVGuideRows)xmlreader.GetValueAsInt(section, "tvGuideSize", 10);
            TVConfigGUI.TVMiniGuideRowSize = (TVConfigGUI.TVMiniGuideRows)xmlreader.GetValueAsInt(section, "tvMiniGuideSize", 7);
            break;
          #endregion

          #region Misc
          case "Misc":
            MiscConfigGUI.ShowHiddenMenuImage = xmlreader.GetValueAsInt(section, "miscShowHiddenMenuImage", 1) == 1;
            MiscConfigGUI.ShowRoundedImages = xmlreader.GetValueAsInt(section, "miscShowRoundedCovers", 1) == 1;
            break;
          #endregion

        }
      }
    }

    public static void Save(string section)
    {
      smcLog.WriteLog(string.Format("StreamedMP: Settings.Save({0})", section), LogLevel.Info);
      using (MediaPortal.Profile.Settings xmlwriter = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "StreamedMPConfig.xml")))
      {
        switch (section)
        {
          #region StreamedMPConfig
          case "StreamedMPConfig":

            xmlwriter.SetValue(section, "fullVideoOSD", StreamedMPConfig.fullVideoOSD ? 1 : 0);
            xmlwriter.SetValue(section, "checkOnStart", StreamedMPConfig.checkOnStart ? 1 : 0);
            xmlwriter.SetValue(section, "checkForUpdateAt", StreamedMPConfig.checkForUpdateAt ? 1 : 0);
            xmlwriter.SetValue(section, "checkInterval", StreamedMPConfig.checkInterval);
            xmlwriter.SetValue(section, "checkTime", StreamedMPConfig.checkTime.ToShortTimeString());
            xmlwriter.SetValue(section, "nextUpdateCheck", StreamedMPConfig.nextUpdateCheck);
            xmlwriter.SetValue(section, "runPatchUtilityUnattended", StreamedMPConfig.patchUtilityRunUnattended ? 1 : 0);
            xmlwriter.SetValue(section, "patchUtilityRestartMP", StreamedMPConfig.patchUtilityRestartMP ? 1 : 0);
            xmlwriter.SetValue(section, "mostRecentTimer", StreamedMPConfig.mrFanartTimer);
            break;
          #endregion

          #region Music
          case "MusicConfigGUI":
            xmlwriter.SetValue(section, "cdCoverOnly", StreamedMPConfig.cdCoverOnly ? 1 : 0);
            xmlwriter.SetValue(section, "showEqGraphic", StreamedMPConfig.showEqGraphic ? 1 : 0);
            xmlwriter.SetValue(section, "nowPlayingStyle", StreamedMPConfig.nowPlayingStyle.ToString());
            break;
          #endregion

          #region TVSeries
          case "TVSeriesConfigGUI":
            xmlwriter.SetValue(section, "tvseriesDefaultStyle", TVSeriesConfigGUI.IsDefaultStyle ? 1 : 0);
            xmlwriter.SetValue(section, "tvseriesWideBannerMod", (int)TVSeriesConfigGUI.WideBannerMod);
            break;
          #endregion

          #region MovingPictures
          case "MovingPicturesConfigGUI":
            xmlwriter.SetValue(section, "movingpicturesDefaultStyle", MovingPicturesConfigGUI.IsDefaultStyle ? 1 : 0);
            xmlwriter.SetValue(section, "movingpicturesThumbMod", (int)MovingPicturesConfigGUI.ThumbnailMod);
            break;
          #endregion

          #region TV
          case "TVConfigGUI":
            xmlwriter.SetValue(section, "tvGuideSize", (int)TVConfigGUI.TVGuideRowSize);
            xmlwriter.SetValue(section, "tvMiniGuideSize", (int)TVConfigGUI.TVMiniGuideRowSize);
            break;
          #endregion

          #region Misc
          case "Misc":
            xmlwriter.SetValue(section, "miscShowHiddenMenuImage", MiscConfigGUI.ShowHiddenMenuImage ? 1 : 0);
            xmlwriter.SetValue(section, "miscShowRoundedCovers", MiscConfigGUI.ShowRoundedImages ? 1 : 0);
            break;
          #endregion

        }
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

    bool _mpSetAsFullScreen()
    {
      using (MediaPortal.Profile.Settings xmlreader = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "MediaPortal.xml")))
      {
        return xmlreader.GetValueAsBool("general", "startfullscreen", false);
      }
    }

    int _logLevel()
    {
      using (MediaPortal.Profile.Settings xmlreader = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "MediaPortal.xml")))
      {
        return xmlreader.GetValueAsInt("general", "loglevel", 0);
      }
    }

    bool _mrSeasonEpisodeStyle2()
    {
      string optionsTag = "StreamedMP Options";
      XmlDocument doc = new XmlDocument();

      if (openUsermenuProfile(doc))
      {

        XmlNodeList nodelist = doc.DocumentElement.SelectNodes("/profile/skin");
        try
        {
          return bool.Parse(readEntryValue(optionsTag, "mrSeriesEpisodeFormat", nodelist));
        }
        catch
        {
          smcLog.WriteLog("StreamedMPConfig: Option mrSeriesEpisodeFormat not present",LogLevel.Error);
          return false;
        }
      }
      return false;
    }

    bool _timerRequired()
    {
      string optionsTag = "StreamedMP Options";
      XmlDocument doc = new XmlDocument();

      if (openUsermenuProfile(doc))
      {

        XmlNodeList nodelist = doc.DocumentElement.SelectNodes("/profile/skin");
        try
        {
          return bool.Parse(readEntryValue(optionsTag, "mostRecentCycleFanart", nodelist));
        }
        catch
        {
          smcLog.WriteLog("StreamedMPConfig: Option mostRecentCycleFanart not present",LogLevel.Error);
          return false;
        }
      }
      return false;
    }

    bool openUsermenuProfile(XmlDocument doc)
    {
      string usermenuprofile = SkinInfo.mpPaths.configBasePath + "usermenuprofile.xml";
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
          smcLog.WriteLog("Can't find usermenuprofile.xml \r\r" + SkinInfo.mpPaths.configBasePath + "usermenuprofile.xml",LogLevel.Error);
          return false;
        }
      }

      try
      {
        doc.Load(usermenuprofile);
        return true;
      }
      catch (Exception e)
      {
        smcLog.WriteLog("Exception while loading usermenuprofile.xml\n\n" + e.Message,LogLevel.Error);
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
