
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

    #region XML Configuration Sections
    public const string cXMLSectionTVSeries = "TVSeriesConfig";
    public const string cXMLSectionMovingPictures = "MovingPicturesConfig";
    public const string cXMLSectionTV = "TVConfig";
    public const string cXMLSectionMusic = "MusicConfig";
    public const string cXMLSectionMisc = "MiscConfig";
    public const string cXMLSectionVideo = "VideoConfig";
    public const string cXMLSectionUpdate = "UpdateConfig";
    
    public const string cXMLSectionEditorOptions = "StreamedMP Options";
    #endregion

    #region XML Configuration Strings
    public const string cXMLSettingUpdateCheckOnStart = "updateCheckOnStart";
    public const string cXMLSettingUpdateCheckForUpdateAt = "updateCheckForUpdateAt";
    public const string cXMLSettingUpdateCheckInterval = "updateCheckInterval";
    public const string cXMLSettingUpdateCheckTime = "updateCheckTime";
    public const string cXMLSettingUpdateRunPatchUtilityUnattended = "updateRunPatchUtilityUnattended";
    public const string cXMLSettingUpdateNextUpdateCheck = "updateNextUpdateCheck";
    public const string cXMLSettingUpdatePatchUtilityRestartMP = "updatePatchUtilityRestartMP";

    public const string cXMLSettingMusicCdCoverOnly = "musicCdCoverOnly";
    public const string cXMLSettingMusicShowEqGraphic = "musicShowEqGraphic";
    public const string cXMLSettingNowPlayingStyle = "musicNowPlayingStyle";

    public const string cXMLSettingTVSeriesDefaultStyle = "tvseriesDefaultStyle";
    public const string cXMLSettingTVSeriesWideBannerMod = "tvseriesWideBannerMod";
    public const string cXMLSettingTVSeriesShowTotalEpisodeCounts = "tvseriesShowTotalEpisodeCounts";
    public const string cXMLSettingTVSeriesShowIconsInListView = "tvseriesShowIconsInListView";

    public const string cXMLSettingMovingPicturesDefaultStyle = "movingpicturesDefaultStyle";
    public const string cXMLSettingMovingPicturesThumbMod = "movingpicturesThumbMod";

    public const string cXMLSettingTVGuideSize = "tvGuideSize";
    public const string cXMLSettingTVMiniGuideSize = "tvMiniGuideSize";
    public const string cXMLSettingTVEnableRandomTVSeriesFanart = "tvEnableRandomTVSeriesFanart";

    public const string cXMLSettingVideoFullVideoOSD = "videoFullVideoOSD";

    public const string cXMLSettingMiscShowHiddenMenuImage = "miscShowHiddenMenuImage";
    public const string cXMLSettingMiscShowRoundedCovers = "miscShowRoundedCovers";
    public const string cXMLSettingMiscShowIconsInArtwork = "miscShowIconsInArtwork";
    public const string cXMLSettingMiscMostRecentFanartTimerInt = "miscMostRecentFanartTimerInt";
    public const string cXMLSettingMiscUseLargeFonts = "miscUseLargeFonts";
    public const string cXMLSettingMiscUnfocusedAlphaListItems = "miscUnfocusedAlphaListItems";
    public const string cXMLSettingMiscUnfocusedAlphaThumbs = "miscUnfocusedAlphaThumbs";
    public const string cXMLSettingMiscTextColor = "miscTextColor";
    public const string cXMLSettingTextColor2 = "miscTextColor2";
    public const string cXMLSettingTextColor3 = "miscTextColor3";
    public const string cXMLSettingWatchedColor = "miscWatchedColor";
    public const string cXMLSettingRemoteColor = "miscRemoteColor";

    public const string cXMLSettingMovPicsRecentAdded = "movPicsMostRecent";
    public const string cXMLSettingMovPicsRecentWatched = "mrMovPicsWatched";
    public const string cXMLSettingTVSeriesRecentAdded = "tvSeriesMostRecent";
    public const string cXMLSettingTVSeriesRecentWatched = "mrTVSeriesWatched";
    #endregion

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

    public static void LoadEditorProperties(string section)
    {
      smcLog.WriteLog(string.Format("BasicHome Menu: Settings.Load({0})", section), LogLevel.Info);

      // Get settings from editor...would be best to unify all these into the same configuration file    
      XmlDocument xmlDoc = Helper.LoadXMLDocument(Config.GetFile(Config.Dir.Config, "usermenuprofile.xml"));

      if (xmlDoc == null)
      {
        // set defaults
        StreamedMPConfig.movPicRecentAddedEnabled = true;
        StreamedMPConfig.tvSeriesRecentAddedEnabled = true;
        return;
      }

      XmlNode node = xmlDoc.SelectSingleNode(string.Format("/profile/skin[@name='StreamedMP']/section[@name='{0}']", section));

      if (Helper.ReadEntryValue(section, settings.cXMLSettingMovPicsRecentAdded, node) == "true")
        StreamedMPConfig.movPicRecentAddedEnabled = true;

      if (Helper.ReadEntryValue(section, settings.cXMLSettingMovPicsRecentWatched, node) == "true")
        StreamedMPConfig.movPicRecentWatchedEnabled = true;

      if (Helper.ReadEntryValue(section, settings.cXMLSettingTVSeriesRecentAdded, node) == "true")
        StreamedMPConfig.tvSeriesRecentAddedEnabled = true;

      if (Helper.ReadEntryValue(section, settings.cXMLSettingTVSeriesRecentWatched, node) == "true")
        StreamedMPConfig.tvSeriesRecentWatchedEnabled = true;
     
    }

    public static void Load(string section)
    {
      smcLog.WriteLog(string.Format("StreamedMPConfig: Settings.Load({0})", section), LogLevel.Info);
      using (MediaPortal.Profile.Settings xmlreader = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "StreamedMPConfig.xml")))
      {
        // Read user settings from configuration file
        switch (section)
        {
          #region Update
          case settings.cXMLSectionUpdate:           
            if (xmlreader.GetValueAsInt(section, settings.cXMLSettingUpdateCheckOnStart, 1) != 0)
              StreamedMPConfig.checkOnStart = true;
            if (xmlreader.GetValueAsInt(section, settings.cXMLSettingUpdateCheckForUpdateAt, 1) != 0)
              StreamedMPConfig.checkForUpdateAt = true;
            if (xmlreader.GetValueAsInt(section, settings.cXMLSettingUpdateRunPatchUtilityUnattended, 1) != 0)
              StreamedMPConfig.patchUtilityRunUnattended = true;
            if (xmlreader.GetValueAsInt(section, settings.cXMLSettingUpdatePatchUtilityRestartMP, 1) != 0)
              StreamedMPConfig.patchUtilityRestartMP = true;

            if (StreamedMPConfig.checkForUpdateAt)
            {
              string checkTime = xmlreader.GetValueAsString(section, settings.cXMLSettingUpdateCheckTime, "");

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

                StreamedMPConfig.nextUpdateCheck = xmlreader.GetValueAsString(section, settings.cXMLSettingUpdateNextUpdateCheck, "");
                StreamedMPConfig.checkInterval = xmlreader.GetValueAsInt(section, settings.cXMLSettingUpdateCheckInterval, 1);
              }
            }
            break;
          #endregion

          #region Music
          case settings.cXMLSectionMusic:
            if (xmlreader.GetValueAsInt(section, settings.cXMLSettingMusicCdCoverOnly, 1) != 0)
              MusicOptionsGUI.cdCoverOnly = true;
            if (xmlreader.GetValueAsInt(section, settings.cXMLSettingMusicShowEqGraphic, 1) != 0)
              MusicOptionsGUI.showEqGraphic = true;
            MusicOptionsGUI.nowPlayingStyle = (MusicOptionsGUI.NowPlayingStyles)xmlreader.GetValueAsInt(section, settings.cXMLSettingNowPlayingStyle, 0);
            break;
          #endregion

          #region TVSeries
          case settings.cXMLSectionTVSeries:
            TVSeriesConfig.IsDefaultStyle = xmlreader.GetValueAsInt(section, settings.cXMLSettingTVSeriesDefaultStyle, 1) == 1;
            TVSeriesConfig.WideBannerMod = (TVSeriesConfig.WideBanners)xmlreader.GetValueAsInt(section, settings.cXMLSettingTVSeriesWideBannerMod, 0);
            break;
          #endregion

          #region MovingPictures
          case settings.cXMLSectionMovingPictures:
            MovingPicturesConfig.IsDefaultStyle = xmlreader.GetValueAsInt(section, settings.cXMLSettingMovingPicturesDefaultStyle, 1) == 1;
            MovingPicturesConfig.ThumbnailMod = (MovingPicturesConfig.Thumbnails)xmlreader.GetValueAsInt(section, settings.cXMLSettingMovingPicturesThumbMod, 0);
            break;
          #endregion

          #region TV
          case settings.cXMLSectionTV:
            TVConfig.TVGuideRowSize = (TVConfig.TVGuideRows)xmlreader.GetValueAsInt(section, settings.cXMLSettingTVGuideSize, 10);
            TVConfig.TVMiniGuideRowSize = (TVConfig.TVMiniGuideRows)xmlreader.GetValueAsInt(section, settings.cXMLSettingTVMiniGuideSize, 7);
            break;
          #endregion

          #region Video
          case settings.cXMLSectionVideo:
            VideoOptionsGUI.FullVideoOSD = xmlreader.GetValueAsInt(section, settings.cXMLSettingVideoFullVideoOSD, 1) == 1;
            break;
          #endregion

          #region Misc
          case settings.cXMLSectionMisc:
            MiscConfigGUI.ShowHiddenMenuImage = xmlreader.GetValueAsInt(section, settings.cXMLSettingMiscShowHiddenMenuImage, 1) == 1;
            MiscConfigGUI.ShowRoundedImages = xmlreader.GetValueAsInt(section, settings.cXMLSettingMiscShowRoundedCovers, 1) == 1;
            MiscConfigGUI.ShowIconsInArtwork = xmlreader.GetValueAsInt(section, settings.cXMLSettingMiscShowIconsInArtwork, 1) == 1;
            MiscConfigGUI.MostRecentFanartTimerInt = xmlreader.GetValueAsInt(section, settings.cXMLSettingMiscMostRecentFanartTimerInt, 7);
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
          #region Update
          case settings.cXMLSectionUpdate:            
            xmlwriter.SetValue(section, settings.cXMLSettingUpdateCheckOnStart, StreamedMPConfig.checkOnStart ? 1 : 0);
            xmlwriter.SetValue(section, settings.cXMLSettingUpdateCheckForUpdateAt, StreamedMPConfig.checkForUpdateAt ? 1 : 0);
            xmlwriter.SetValue(section, settings.cXMLSettingUpdateCheckInterval, StreamedMPConfig.checkInterval);
            xmlwriter.SetValue(section, settings.cXMLSettingUpdateCheckTime, StreamedMPConfig.checkTime.ToShortTimeString());
            xmlwriter.SetValue(section, settings.cXMLSettingUpdateNextUpdateCheck, StreamedMPConfig.nextUpdateCheck);
            xmlwriter.SetValue(section, settings.cXMLSettingUpdateRunPatchUtilityUnattended, StreamedMPConfig.patchUtilityRunUnattended ? 1 : 0);
            xmlwriter.SetValue(section, settings.cXMLSettingUpdatePatchUtilityRestartMP, StreamedMPConfig.patchUtilityRestartMP ? 1 : 0);            
            break;
          #endregion

          #region Music
          case settings.cXMLSectionMusic:
            xmlwriter.SetValue(section, settings.cXMLSettingMusicCdCoverOnly, MusicOptionsGUI.cdCoverOnly ? 1 : 0);
            xmlwriter.SetValue(section, settings.cXMLSettingMusicShowEqGraphic, MusicOptionsGUI.showEqGraphic ? 1 : 0);
            xmlwriter.SetValue(section, settings.cXMLSettingNowPlayingStyle, (int)MusicOptionsGUI.nowPlayingStyle);
            break;
          #endregion

          #region TVSeries
          case cXMLSectionTVSeries:
            xmlwriter.SetValue(section, settings.cXMLSettingTVSeriesDefaultStyle, TVSeriesConfig.IsDefaultStyle ? 1 : 0);
            xmlwriter.SetValue(section, settings.cXMLSettingTVSeriesWideBannerMod, (int)TVSeriesConfig.WideBannerMod);
            break;
          #endregion

          #region MovingPictures
          case cXMLSectionMovingPictures:
            xmlwriter.SetValue(section, settings.cXMLSettingMovingPicturesDefaultStyle, MovingPicturesConfig.IsDefaultStyle ? 1 : 0);
            xmlwriter.SetValue(section, settings.cXMLSettingMovingPicturesThumbMod, (int)MovingPicturesConfig.ThumbnailMod);
            break;
          #endregion

          #region TV
          case settings.cXMLSectionTV:
            xmlwriter.SetValue(section, settings.cXMLSettingTVGuideSize, (int)TVConfig.TVGuideRowSize);
            xmlwriter.SetValue(section, settings.cXMLSettingTVMiniGuideSize, (int)TVConfig.TVMiniGuideRowSize);
            break;
          #endregion

          #region Video
          case settings.cXMLSectionVideo:
            xmlwriter.SetValue(section, settings.cXMLSettingVideoFullVideoOSD, VideoOptionsGUI.FullVideoOSD ? 1 : 0);          
            break;
          #endregion

          #region Misc
          case settings.cXMLSectionMisc:
            xmlwriter.SetValue(section, settings.cXMLSettingMiscShowHiddenMenuImage, MiscConfigGUI.ShowHiddenMenuImage ? 1 : 0);
            xmlwriter.SetValue(section, settings.cXMLSettingMiscShowRoundedCovers, MiscConfigGUI.ShowRoundedImages ? 1 : 0);
            xmlwriter.SetValue(section, settings.cXMLSettingMiscShowIconsInArtwork, MiscConfigGUI.ShowIconsInArtwork ? 1 : 0);
            xmlwriter.SetValue(section, settings.cXMLSettingMiscMostRecentFanartTimerInt, MiscConfigGUI.MostRecentFanartTimerInt);
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
