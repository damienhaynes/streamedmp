﻿using System;
using System.Drawing;
using System.Xml;
using System.IO;

namespace SMPMenuGen
{
  public partial class GenerateMenu
  {
    public void loadMenuSettings()
    {
      string defaultcontrol = null;
      string defaultImage = null;
      string usermenuprofile = SkinInfo.mpPaths.configBasePath + "usermenuprofile.xml";
      string id = null;
      string _selectedFont = null;
      string _labelFont = null;
      string activeRssImageType = null;
      string targetScreenRes = null;
      string tvRecentDisplayType = null;
      string movPicsDisplayType = null;
      string mostRecentSumStyle = null;
      string mostRecentTVSeriesSummStyle = null;
      string mostRecentMovPicsSummStyle = null;
      string bgFolderName = null;


      int subMenuItems1 = 0;
      int subMenuItems2 = 0;

      bool folderUpdateRequired = false;

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
          //helper.showError("Can't find usermenuprofile.xml \r\r" + SkinInfo.mpPaths.configBasePath + "usermenuprofile.xml", errorCode.major);
        }
      }
      try
      {
        doc.Load(usermenuprofile);
      }
      catch (Exception e)
      {
        //helper.showError("Exception while loading usermenuprofile.xml\n\nUnable to Continue - please restore from backup" + e.Message, errorCode.major);
      }

      //
      // Get the version of usermenuprofile
      //
      XmlNode versionControlNode = doc.DocumentElement.SelectSingleNode("/profile/version");
      string versionNum = null, optionsTag = null, menuTag = null;

      if (versionControlNode != null)
      {
        versionNum = versionControlNode.InnerText;
      }
      switch (versionNum)
      {
        case "2.0":
          optionsTag = "StreamedMP Options";
          menuTag = "StreamedMP Menu Items";
          bgFolderName = "SMPbackgrounds";
          break;
        case "1.0":
          optionsTag = "StreamedMP Options";
          menuTag = "StreamedMP Menu Items";
          folderUpdateRequired = true;
          //if (foldersUpdated())
          //  bgFolderName = "SMPbackgrounds";
          break;
        default:
          optionsTag = "StreamedMP Options";
          menuTag = "Menu Items";
          break;
      }

      // Now read the file
      XmlNodeList nodelist = doc.DocumentElement.SelectNodes("/profile/skin");
      // Get the last selected Menu...
      try
      {
        switch (readEntryValue(optionsTag, "menustyle", nodelist))
        {
          case "verticalStyle":
            menuStyle = chosenMenuStyle.verticalStyle;
            menudef.verticalStyle = true;
            menudef.selectedFont = "mediastream16tc";
            menudef.labelFont = "mediastream28tc";
            break;
          case "horizontalStandardStyle":
            menuStyle = chosenMenuStyle.horizontalStandardStyle;
            menudef.selectedFont = "mediastream28tc";
            menudef.labelFont = "mediastream28tc";
            break;
          case "horizontalContextStyle":
            menuStyle = chosenMenuStyle.horizontalContextStyle;
            menudef.selectedFont = "mediastream28tc";
            menudef.labelFont = "mediastream28tc";
            break;
          case "graphicMenuStyle":
            menuStyle = chosenMenuStyle.graphicMenuStyle;
            menudef.selectedFont = "mediastream28tc";
            menudef.labelFont = "mediastream28tc";
            break;
          default:
            menuStyle = chosenMenuStyle.verticalStyle;
            menudef.selectedFont = "mediastream16tc";
            menudef.labelFont = "mediastream28tc";
            break;
        }

        //...and Weather styles
        if (readEntryValue(optionsTag, "weatherstyle", nodelist) == "bottom")
        {
          weatherStyle = chosenWeatherStyle.bottom;
        }
        else if (readEntryValue(optionsTag, "weatherstyle", nodelist) == "middle")
        {
          weatherStyle = chosenWeatherStyle.middle;
        }
        else
        {
          weatherStyle = chosenWeatherStyle.bottom;
        }
      }
      catch { }

      // Get the Focus Colour and set the background on the control
      menudef.focusAlpha = readEntryValue(optionsTag, "menuitemFocus", nodelist).Substring(0, 2);
      try
      {
        string RGB = defFocus;
        RGB = readEntryValue(optionsTag, "menuitemFocus", nodelist).Substring(2);
        Color col = ColorFromRGB(RGB);
        menudef.focusColor = RGB;
      }
      catch
      {
        menudef.focusColor = defFocus;
      }
      // Get the NoFocus Colour and set the background on the control
      menudef.noFocusAlpha = readEntryValue(optionsTag, "menuitemNoFocus", nodelist).Substring(0, 2);
      try
      {
        string RGB = defUnFocus;
        RGB = readEntryValue(optionsTag, "menuitemNoFocus", nodelist).Substring(2);
        Color col = ColorFromRGB(RGB);
        menudef.noFocusColor = RGB;
      }
      catch
      {
      menudef.noFocusColor = defUnFocus;
      }

      // Line up all the options this also sets the defaults for the style
      // which can be overidden by user settings below
      //syncEditor(sync.OnLoad);

      // Re-read and set menupos and 5 day weather location
      if (readEntryValue(optionsTag, "weatherstyle", nodelist) == "bottom")
      {
        weatherStyle = chosenWeatherStyle.bottom;
      }
      else if (readEntryValue(optionsTag, "weatherstyle", nodelist) == "middle")
      {
        weatherStyle = chosenWeatherStyle.middle;
      }
      else
      {
        weatherStyle = chosenWeatherStyle.bottom;
      }

      if (readEntryValue(optionsTag, "menustyle", nodelist) == "verticalStyle")
        menudef.menuPos = readEntryValue(optionsTag, "menuXPos", nodelist);
      else
        menudef.menuPos = readEntryValue(optionsTag, "menuYPos", nodelist);



      //
      // Check and set the Global and Plugin options and apply any customization by user
      //
      try
      {
        menudef.selectedFont = readEntryValue(optionsTag, "selectedFont", nodelist);
        menudef.labelFont = readEntryValue(optionsTag, "labelFont", nodelist);
        menudef.acceleration = readEntryValue(optionsTag, "acceleration", nodelist);
        menudef.duration = readEntryValue(optionsTag, "duration", nodelist);
        menudef.dropShadow = bool.Parse(readEntryValue(optionsTag, "dropShadow", nodelist));
        menudef.rssEnabled = bool.Parse(readEntryValue(optionsTag, "enableRssfeed", nodelist));
        menudef.twitterEnabled = bool.Parse(readEntryValue(optionsTag, "enableTwitter", nodelist));
        menudef.wrapString = bool.Parse(readEntryValue(optionsTag, "wrapString", nodelist));
        menudef.weatherBGLink = bool.Parse(readEntryValue(optionsTag, "weatherBGlink", nodelist));
        menudef.enableFiveDayWeather = bool.Parse(readEntryValue(optionsTag, "fiveDayWeatherCheckBox", nodelist));
        menudef.summaryWeatherEnabled = bool.Parse(readEntryValue(optionsTag, "summaryWeatherCheckBox", nodelist));
        menudef.clearCache = bool.Parse(readEntryValue(optionsTag, "cboClearCache", nodelist));
        menudef.animatedWeatherIcons = bool.Parse(readEntryValue(optionsTag, "animatedWeather", nodelist));
        menudef.horizontalContextLabels = bool.Parse(readEntryValue(optionsTag, "horizontalContextLabels", nodelist));
        menudef.fullWeatherSummaryBottom = bool.Parse(readEntryValue(optionsTag, "fullWeatherSummaryBottom", nodelist));
        menudef.fullWeatherSummaryMiddle = bool.Parse(readEntryValue(optionsTag, "fullWeatherSummaryMiddle", nodelist));
        menudef.disableClock = bool.Parse(readEntryValue(optionsTag, "disableOnScreenClock", nodelist));
        menudef.hideFanartScraper = bool.Parse(readEntryValue(optionsTag, "hideFanartScrapingtext", nodelist));
        menudef.overlayFanart = bool.Parse(readEntryValue(optionsTag, "enableOverlayFanart", nodelist));
        menudef.animateBackground = bool.Parse(readEntryValue(optionsTag, "animatedBackground", nodelist));
        menudef.enableMostRecentTVSeries = bool.Parse(readEntryValue(optionsTag, "tvSeriesMostRecent", nodelist));
        menudef.enableMostRecentMovPics = bool.Parse(readEntryValue(optionsTag, "movPicsMostRecent", nodelist));
        menudef.cycleMostRecentFanart = bool.Parse(readEntryValue(optionsTag, "mostRecentCycleFanart", nodelist));
        menudef.mrSeriesEpisodeFormat = bool.Parse(readEntryValue(optionsTag, "mrSeriesEpisodeFormat", nodelist));
        menudef.mrTitleLast = bool.Parse(readEntryValue(optionsTag, "mrTitleLast", nodelist));
        menudef.exitStyleNew = bool.Parse(readEntryValue(optionsTag, "settingOldStyleExitButtons", nodelist));
        menudef.tvseriesCycleFanart = bool.Parse(readEntryValue(optionsTag, "mrTVSeriesCycleFanart", nodelist));
        menudef.movepicsCycleFanart = bool.Parse(readEntryValue(optionsTag, "mrMovPicsCycleFanart", nodelist));
        menudef.tvSeriesEpisodeFont = readEntryValue(optionsTag, "mrEpisodeFont", nodelist);
        menudef.tvSeriesSeriesFont = readEntryValue(optionsTag, "mrSeriesFont", nodelist);
        menudef.movpicsTitleFont = readEntryValue(optionsTag, "mrMovieTitleFont", nodelist);
        menudef.movpicsDetailFont = readEntryValue(optionsTag, "mrMovieDetailFont", nodelist);
        menudef.hideRuntime = bool.Parse(readEntryValue(optionsTag, "mrMovPicsHideRuntime", nodelist));
        menudef.hideCertifcation = bool.Parse(readEntryValue(optionsTag, "mrMovPicsHideCertification", nodelist));
        menudef.hideRating = bool.Parse(readEntryValue(optionsTag, "mrMovPicsHideRating", nodelist));
        menudef.useTextRating = bool.Parse(readEntryValue(optionsTag, "mrMovPicsUseTextRating", nodelist));
        menudef.movpicsRecentWatched = bool.Parse(readEntryValue(optionsTag, "mrMovPicsWatched", nodelist));
        menudef.tvSeriesRecentWatched = bool.Parse(readEntryValue(optionsTag, "mrTVSeriesWatched", nodelist));
        menudef.tvSeriesDisableFadeLables = bool.Parse(readEntryValue(optionsTag, "mrTVSeriesDisableFadeLabel", nodelist));
        menudef.movpicsDisableFadeLables = bool.Parse(readEntryValue(optionsTag, "mrMovPicsDisableFadeLabel", nodelist));
        menudef.enableMostRecentRecordedTV = bool.Parse(readEntryValue(optionsTag, "mrRecordedTVEnabled", nodelist));
        menudef.enableMostRecentMusic = bool.Parse(readEntryValue(optionsTag, "mrMusicEnabled", nodelist));
        menudef.driveFreeSpaceList = readEntryValue(optionsTag, "driveFreeSpaceList", nodelist);
        menudef.enableSleepControlOverlay = bool.Parse(readEntryValue(optionsTag, "sleepControlEnabled", nodelist));
        menudef.enableStocksOverlay = bool.Parse(readEntryValue(optionsTag, "stocksControlEnabled", nodelist));
        menudef.enablePowerControlOverlay = bool.Parse(readEntryValue(optionsTag, "powerControlEnabled", nodelist));
        menudef.enableHTPCInfoOverlay = bool.Parse(readEntryValue(optionsTag, "htpcinfoControlEnabled", nodelist));
        menudef.enableUpdateControlOverlay = bool.Parse(readEntryValue(optionsTag, "updateControlEnabled", nodelist));
        menudef.disableExitMenu = bool.Parse(readEntryValue(optionsTag, "disableExitMenu", nodelist));

        activeRssImageType = readEntryValue(optionsTag, "activeRssImageType", nodelist);
        targetScreenRes = readEntryValue(optionsTag, "targetScreenRes", nodelist);
        splashScreenImage = readEntryValue(optionsTag, "splashScreenImage", nodelist);
        tvRecentDisplayType = readEntryValue(optionsTag, "tvRecentDisplayType", nodelist);
        movPicsDisplayType = readEntryValue(optionsTag, "movPicsDisplayType", nodelist);
        mostRecentSumStyle = readEntryValue(optionsTag, "mostRecentSumStyle", nodelist);  // Hang over from when juts TVSeries was supported
        mostRecentTVSeriesSummStyle = readEntryValue(optionsTag, "mostRecentTVSeriesSummStyle", nodelist);
        mostRecentMovPicsSummStyle = readEntryValue(optionsTag, "mostRecentMovPicsSummStyle", nodelist);    
      }
      catch
      {
        // Most likley a new option added but not written to file yet - just continue
      }

      if (string.IsNullOrEmpty(driveFreeSpaceList))
        driveFreeSpaceList = "false";

      if (!(driveFreeSpaceList == "false"))
      {
        menudef.enableDriveFreeSpace = true;
        string[] configuredDrives = driveFreeSpaceList.Split(',');
        foreach (string hd in configuredDrives)
        {
          DriveInfo hdDetails = new DriveInfo(hd);
          string thisDrive = hd + " (" + hdDetails.VolumeLabel + ")";
          formStreamedMpEditor.driveFreeSpaceDrives.Add(hd);
        }
      }



      //if (tvSeriesOptions.mrSeriesFont == "mediastream9c")
      //  tvSeriesOptions.mrSeriesFont = "mediastream10c";

      //if (tvSeriesOptions.mrEpisodeFont == "mediastream9c")
      //  tvSeriesOptions.mrEpisodeFont = "mediastream10c";

      //if (tvSeriesOptions.mrSeriesFont == "false" || tvSeriesOptions.mrEpisodeFont == "false")
      //{
      //  tvSeriesOptions.mrSeriesFont = "mediastream10c";
      //  tvSeriesOptions.mrEpisodeFont = "mediastream10tc (Bold)";
      //}

      //if (movPicsOptions.MovieTitleFont == "false" || movPicsOptions.MovieDetailFont == "false")
      //{
      //  movPicsOptions.MovieTitleFont = "mediastream10tc (Bold)";
      //  movPicsOptions.MovieDetailFont = "mediastream10c";
      //}

      // As only saving the animated state set the static state true if animimated state is false
      //if (!animatedIconsInstalled())
      //{
      //  WeatherIconsAnimated.Enabled = false;
      //  menudef.animatedWeatherIcons = false;
      //}
      //if (menudef.animatedWeatherIcons)
      //  weatherIconsStatic.Checked = false;
      //else
      //  weatherIconsStatic.Checked = true;



      if (!weatherBackgoundsInstalled())
      {
        menudef.weatherBGLink = false;
      }

      //if (tvRecentDisplayType == "summary" || movPicsDisplayType == "summary")
      //  btFormatOptions.Enabled = true;

      //if (tvRecentDisplayType == "summary")
      //{
      //  tvSeriesRecentStyle = tvSeriesRecentType.summary;
      //  rbTVSeriesSummary.Checked = true;
      //  gbSummaryStyle.Enabled = true;
      //}
      //else
      //{
      //  tvSeriesRecentStyle = tvSeriesRecentType.full;
      //  rbTBSeriesFull.Checked = true;
      //  gbSummaryStyle.Enabled = false;
      //}

      //if (movPicsDisplayType == "summary")
      //{
      //  movPicsRecentStyle = movPicsRecentType.summary;
      //  rbMovPicsSummary.Checked = true;
      //}
      //else
      //{
      //  movPicsRecentStyle = movPicsRecentType.full;
      //  rbMovPicsFull.Checked = true;
      //}

      // TVSeries most recent summry Style
      //if (mostRecentTVSeriesSummStyle == "fanart")
      //{
      //  mrTVSeriesSummStyle = mostRecentTVSeriesSummaryStyle.fanart;
      //  rbFanartStyle.Checked = true;
      //}
      //else if (mostRecentTVSeriesSummStyle == "poster")
      //{
      //  mrTVSeriesSummStyle = mostRecentTVSeriesSummaryStyle.poster;
      //  rbPosterStyle.Checked = true;
      //}
      //else
      //{
      //  //Default to Fanart Style
      //  mrTVSeriesSummStyle = mostRecentTVSeriesSummaryStyle.fanart;
      //  rbFanartStyle.Checked = true;
      //}

      // Moving Pictures most recent summry Style
      //if (mostRecentMovPicsSummStyle == "fanart")
      //{
      //  mrMovPicsSummStyle = mostRecentMovPicsSummaryStyle.fanart;
      //  rbFanartStyle.Checked = true;
      //}
      //else if (mostRecentMovPicsSummStyle == "poster")
      //{
      //  mrMovPicsSummStyle = mostRecentMovPicsSummaryStyle.poster;
      //  rbPosterStyle.Checked = true;
      //}
      //else
      //{
      //  //Default to Fanart Style
      //  mrMovPicsSummStyle = mostRecentMovPicsSummaryStyle.fanart;
      //  rbFanartStyle.Checked = true;
      //}

      //
      // Old Setting - if found in usermenuprofile use as setting for TVSeries Summary Style
      //
      //if (mostRecentSumStyle == "fanart")
      //{
      //  mrTVSeriesSummStyle = mostRecentTVSeriesSummaryStyle.fanart;
      //  rbFanartStyle.Checked = true;
      //}
      //else if (mostRecentSumStyle == "poster")
      //{
      //  mrTVSeriesSummStyle = mostRecentTVSeriesSummaryStyle.poster;
      //  rbPosterStyle.Checked = true;
      //}

      //if (splashScreenImage == "false")
      //  splashScreenImage = "splashscreen.png";

      //if (targetScreenRes == "HD")
      //  setHDScreenRes();
      //else if (targetScreenRes == "SD")
      //  setSDScreenRes();

      switch (activeRssImageType)
      {
        case "infoservice":
          rssImage = rssImageType.infoserviceImage;
          break;
        case "noimage":
          rssImage = rssImageType.noImage;
          break;
        case "skin":
          rssImage = rssImageType.skinImage;
          break;
        default:
          rssImage = rssImageType.skinImage;
          break;
      }

      //if (_selectedFont != "false")
      //{
      //  cboSelectedFont.Text = _selectedFont;
      //}
      //if (_labelFont != "false")
      //{
      //  cboLabelFont.Text = _labelFont;
      //}

      //if (menuStyle == chosenMenuStyle.verticalStyle)
      //  menudef.menuPos = readEntryValue(optionsTag, "menuXPos", nodelist);
      //else
      //  menudef.menuPos = readEntryValue(optionsTag, "menuYPos", nodelist);

      //Version isver = new Version("1.6.0.0");
      //if (getInfoServiceVersion().CompareTo(isver) >= 0)
      //  infoServiceDayProperty = "forecast";
      //else
      //  infoServiceDayProperty = "day";

      //// Check if Moving Pictures is installed and enabled, if not disable most recent options
      //if (MovingPicturesVersion == "0.0.0.0")
      //{
      //  pMovPicsRecent.Enabled = false;
      //  cbMostRecentMovPics.Checked = false;
      //  cbMovPicsRecentWatched.Checked = false;
      //  cbMostRecentMovPics.Refresh();
      //}
      //else
      //{
      //  if (!helper.pluginEnabled(Helper.Plugins.MovingPictures))
      //  {
      //    pMovPicsRecent.Enabled = false;
      //    cbMostRecentMovPics.Checked = false;
      //    cbMovPicsRecentWatched.Checked = false;
      //    cbMostRecentMovPics.Refresh();
      //  }
      //}

      //// Check in TVSeries is installed and enabled, if not disable most recent options
      //if (TVSeriesVersion == "0.0.0.0")
      //{
      //  pTVSeriesRecent.Enabled = false;
      //  cbMostRecentTvSeries.Checked = false;
      //  cbTVSeriesRecentWatched.Checked = false;
      //  cbMostRecentTvSeries.Refresh();
      //}
      //else
      //{
      //  if (!helper.pluginEnabled(Helper.Plugins.MPTVSeries))
      //  {
      //    pTVSeriesRecent.Enabled = false;
      //    cbMostRecentTvSeries.Checked = false;
      //    cbTVSeriesRecentWatched.Checked = false;
      //    cbMostRecentTvSeries.Refresh();
      //  }
      //}
      //
      // Read in the menu items
      //
      for (int i = 0; i < int.Parse(readEntryValue(menuTag, "count", nodelist)); i++)
      {
        menuItem mnuItem = new menuItem();
        mnuItem.disableBGSharing = true;
        mnuItem.name = readEntryValue(menuTag, "menuitem" + i.ToString() + "name", nodelist);
        mnuItem.contextLabel = readEntryValue(menuTag, "menuitem" + i.ToString() + "label", nodelist);
        mnuItem.bgFolder = readEntryValue(menuTag, "menuitem" + i.ToString() + "folder", nodelist);
        mnuItem.fanartProperty = readEntryValue(menuTag, "menuitem" + i.ToString() + "fanartproperty", nodelist);
        mnuItem.hyperlink = readEntryValue(menuTag, "menuitem" + i.ToString() + "hyperlink", nodelist);
        mnuItem.hyperlinkParameter = readEntryValue(menuTag, "menuitem" + i.ToString() + "hyperlinkParameter", nodelist);
        mnuItem.hyperlinkParameterOption = readEntryValue(menuTag, "menuitem" + i.ToString() + "hyperlinkParameterOption", nodelist);
        mnuItem.fanartHandlerEnabled = bool.Parse(readEntryValue(menuTag, "menuitem" + i.ToString() + "fanarthandlerenabled", nodelist));
        mnuItem.EnableMusicNowPlayingFanart = bool.Parse(readEntryValue(menuTag, "menuitem" + i.ToString() + "enablemusicnowplayingfanart", nodelist));
        mnuItem.isDefault = bool.Parse(readEntryValue(menuTag, "menuitem" + i.ToString() + "isdefault", nodelist));
        mnuItem.isWeather = bool.Parse(readEntryValue(menuTag, "menuitem" + i.ToString() + "isweather", nodelist));
        mnuItem.updateStatus = bool.Parse(readEntryValue(menuTag, "menuitem" + i.ToString() + "updatestatus", nodelist));
        mnuItem.disableBGSharing = bool.Parse(readEntryValue(menuTag, "menuitem" + i.ToString() + "disableBGSharing", nodelist));
        mnuItem.id = int.Parse(readEntryValue(menuTag, "menuitem" + i.ToString() + "id", nodelist));
        mnuItem.buttonTexture = readEntryValue(menuTag, "menuitem" + i.ToString() + "buttonTexture", nodelist);

        mnuItem.xmlFileName = readEntryValue(menuTag, "menuitem" + i.ToString() + "xmlFileName", nodelist);
        //if (mnuItem.xmlFileName == "false")
        //  mnuItem.xmlFileName = getXMLFileName(mnuItem.hyperlink);

        mnuItem.showMostRecent = readMostRecentDisplayOption(readEntryValue(menuTag, "menuitem" + i.ToString() + "showMostRecent", nodelist), mnuItem.hyperlink);
        mnuItem.fhBGSource = readFHSource(readEntryValue(menuTag, "menuitem" + i.ToString() + "fanartSource", nodelist), mnuItem.fanartProperty);
        //
        // Graphical Menu Default Image Load
        //
        if (string.IsNullOrEmpty(mnuItem.buttonTexture) || mnuItem.buttonTexture.ToLower() == "false")
        {
          mnuItem.buttonTexture = setDefaultIcons(int.Parse(mnuItem.hyperlink), "Black");
        }



        //
        // Convert any 504 skinID's back to 501 (they will be converted back if there is a hyperlink parameter)
        //
        if (mnuItem.hyperlink == "504")
          mnuItem.hyperlink = "501";
        //
        // Read submenu data
        //
        if (readEntryValue(menuTag, "menuitem" + i.ToString() + "subMenuLevel1ID", nodelist) != "false")
        {
          mnuItem.subMenuLevel1ID = int.Parse(readEntryValue(menuTag, "menuitem" + i.ToString() + "subMenuLevel1ID", nodelist));

          subMenuItems1 = int.Parse(readEntryValue(menuTag, "menuitem" + i.ToString() + "submenu1", nodelist));
          subMenuItems2 = int.Parse(readEntryValue(menuTag, "menuitem" + i.ToString() + "submenu2", nodelist));


          if (subMenuItems1 > 0)
          {
            for (int k = 0; k < subMenuItems1; k++)
            {
              subMenuItem subItem = new subMenuItem();
              subItem.displayName = readEntryValue(menuTag, "submenu" + i.ToString() + "1subitem" + k.ToString() + "displayName", nodelist);
              subItem.baseDisplayName = readEntryValue(menuTag, "submenu" + i.ToString() + "1subitem" + k.ToString() + "baseDisplayName", nodelist);
              subItem.xmlFileName = readEntryValue(menuTag, "submenu" + i.ToString() + "1subitem" + k.ToString() + "xmlFileName", nodelist);
              subItem.hyperlink = readEntryValue(menuTag, "submenu" + i.ToString() + "1subitem" + k.ToString() + "hyperlink", nodelist);
              subItem.hyperlinkParameter = readEntryValue(menuTag, "submenu" + i.ToString() + "1subitem" + k.ToString() + "hyperlinkParameter", nodelist);
              subItem.hyperlinkParameterOption = readEntryValue(menuTag, "submenu" + i.ToString() + "1subitem" + k.ToString() + "hyperlinkParameterOption", nodelist);
              subItem.hyperlinkParameterSearch = readEntryValue(menuTag, "submenu" + i.ToString() + "1subitem" + k.ToString() + "hyperlinkParameterSearch", nodelist);
              //
              // Convert any 504 skinID's back to 501 (they will be converted back if there is a hyperlink parameter)
              //
              if (subItem.hyperlink == "504")
                subItem.hyperlink = "501";

              switch (readEntryValue(menuTag, "submenu" + i.ToString() + "1subitem" + k.ToString() + "mrDisplay", nodelist))
              {
                case "off":
                  subItem.showMostRecent = displayMostRecent.off;
                  break;
                case "tvSeries":
                  subItem.showMostRecent = displayMostRecent.tvSeries;
                  break;
                case "movies":
                  subItem.showMostRecent = displayMostRecent.movies;
                  break;
                case "music":
                  subItem.showMostRecent = displayMostRecent.music;
                  break;
                case "recordedTV":
                  subItem.showMostRecent = displayMostRecent.recordedTV;
                  break;
                case "freeDriveSpace":
                  subItem.showMostRecent = displayMostRecent.freeDriveSpace;
                  break;
                case "htpcInfo":
                  subItem.showMostRecent = displayMostRecent.htpcInfo;
                  break;
                case "powerControl":
                  subItem.showMostRecent = displayMostRecent.powerControl;
                  break;
                case "sleepControl":
                  subItem.showMostRecent = displayMostRecent.sleepControl;
                  break;
                case "stocks":
                  subItem.showMostRecent = displayMostRecent.stocks;
                  break;
                case "updateControl":
                  subItem.showMostRecent = displayMostRecent.updateControl;
                  break;
              }
              mnuItem.subMenuLevel1.Add(subItem);
            }
          }

          if (subMenuItems2 > 0)
          {
            for (int k = 0; k < subMenuItems2; k++)
            {
              subMenuItem subItem = new subMenuItem();
              subItem.displayName = readEntryValue(menuTag, "submenu" + i.ToString() + "2subitem" + k.ToString() + "displayName", nodelist);
              subItem.baseDisplayName = readEntryValue(menuTag, "submenu" + i.ToString() + "2subitem" + k.ToString() + "baseDisplayName", nodelist);
              subItem.xmlFileName = readEntryValue(menuTag, "submenu" + i.ToString() + "2subitem" + k.ToString() + "xmlFileName", nodelist);
              subItem.hyperlink = readEntryValue(menuTag, "submenu" + i.ToString() + "2subitem" + k.ToString() + "hyperlink", nodelist);
              subItem.hyperlinkParameter = readEntryValue(menuTag, "submenu" + i.ToString() + "2subitem" + k.ToString() + "hyperlinkParameter", nodelist);
              subItem.hyperlinkParameterOption = readEntryValue(menuTag, "submenu" + i.ToString() + "2subitem" + k.ToString() + "hyperlinkParameterOption", nodelist);
              subItem.hyperlinkParameterSearch = readEntryValue(menuTag, "submenu" + i.ToString() + "2subitem" + k.ToString() + "hyperlinkParameterSearch", nodelist);              
              //
              // Convert any 504 skinID's back to 501 (they will be converted back if there is a hyperlink parameter)
              //
              if (subItem.hyperlink == "504")
                subItem.hyperlink = "501";

              switch (readEntryValue(menuTag, "submenu" + i.ToString() + "2subitem" + k.ToString() + "mrDisplay", nodelist))
              {
                case "off":
                  subItem.showMostRecent = displayMostRecent.off;
                  break;
                case "tvSeries":
                  subItem.showMostRecent = displayMostRecent.tvSeries;
                  break;
                case "movies":
                  subItem.showMostRecent = displayMostRecent.movies;
                  break;
                case "music":
                  subItem.showMostRecent = displayMostRecent.music;
                  break;
                case "recordedTV":
                  subItem.showMostRecent = displayMostRecent.recordedTV;
                  break;
                case "freeDriveSpace":
                  subItem.showMostRecent = displayMostRecent.freeDriveSpace;
                  break;
                case "htpcInfo":
                  subItem.showMostRecent = displayMostRecent.htpcInfo;
                  break;
                case "powerControl":
                  subItem.showMostRecent = displayMostRecent.powerControl;
                  break;
                case "sleepControl":
                  subItem.showMostRecent = displayMostRecent.sleepControl;
                  break;
                case "stocks":
                  subItem.showMostRecent = displayMostRecent.stocks;
                  break;
                case "updateControl":
                  subItem.showMostRecent = displayMostRecent.updateControl;
                  break;
              }
              mnuItem.subMenuLevel2.Add(subItem);
            }
          }

        }

        //isWeather.Checked = mnuItem.isWeather;
        //disableBGSharing.Checked = mnuItem.disableBGSharing;

        //if (mnuItem.fanartHandlerEnabled)
        //  checkAndSetRandomFanart(mnuItem.fanartProperty);

        //// Set the default control
        //if (mnuItem.isDefault)
        //  defaultcontrol = mnuItem.id.ToString();
        //id = mnuItem.id.ToString();
        //itemsOnMenubar.Items.Add(mnuItem.name, id.Equals(defaultcontrol));

        // If user decides not to use multiimage backgrounds then we need a default image, lets check and set if one is required
        defaultImage = readEntryValue(menuTag, "menuitem" + i.ToString() + "defaultimage", nodelist);

        // Version 2.0 usermenu profile - check if folder structure updated but menu has not.
        // if so replace "Animations" with "SMPBackgrounds"
        // Belts and Braces checks
        //if (versionNum == "1.0" && foldersUpdated())
        //{
        //  if (defaultImage.ToLower().StartsWith("animations"))
        //  {
        //    defaultImage = bgFolderName + "\\" + mnuItem.bgFolder + "\\" + Path.GetFileName("C:\\" + defaultImage);
        //  }
        //}

        // Check if the stored image still exists, if nor set to default.jpg
        //if (!System.IO.File.Exists((imageDir(defaultImage))))
        //  defaultImage = bgFolderName + "\\" + mnuItem.bgFolder + "\\default.jpg";

        //if (defaultImage.ToLower().StartsWith(bgFolderName.ToLower()))
        //  mnuItem.defaultImage = defaultImage;
        //else
        //{
        //  if (!mnuItem.bgFolder.Contains("\\"))
        //    mnuItem.defaultImage = bgFolderName + "\\" + mnuItem.bgFolder + "\\default.jpg";
        //  else
        //    mnuItem.defaultImage = mnuItem.bgFolder + "\\default.jpg";
        //}

        menuItems.Add(mnuItem);
      }
      //reloadBackgroundItems();
      //UpdateImageControlVisibility();
      //btGenerateMenu.Enabled = true;

      //if (folderUpdateRequired)
      //  updateBackgroundFolders();
    }

    //string getXMLFileName(string hyperLink)
    //{
    //  int index = ids.IndexOf(hyperLink);
    //  string firstFound;

    //  try
    //  {

    //    firstFound = xmlFiles.Items[index].ToString();

    //    index = ids.IndexOf(hyperLink, index + 1);
    //    if (index != -1 && hyperLink == "1")
    //    {
    //      if (!helper.pluginEnabled(Helper.Plugins.ForTheRecord))
    //        firstFound = xmlFiles.Items[index].ToString();
    //    }
    //  }
    //  catch
    //  {
    //    firstFound = "false";
    //  }

    //  return firstFound;
    //}

    displayMostRecent readMostRecentDisplayOption(string mrOption, string skinId)
    {
      // Enable most recent movies on MovingPictures menu item if not defined
      if (mrOption == "false" && skinId == movingPicturesSkinID)
        return displayMostRecent.movies;

      // Enable most recent TVSeries on TVSeries menu item if not defined
      if (mrOption == "false" && skinId == tvseriesSkinID)
        return displayMostRecent.tvSeries;

      // Enable most recent RecordedTV on TV menu item if not defined
      if (mrOption == "false" && skinId == tvMenuSkinID)
        return displayMostRecent.recordedTV;

      // Enable most recent Music on Music menu item if not defined
      if (mrOption == "false" && skinId == musicSkinID)
        return displayMostRecent.music;

      if (mrOption == displayMostRecent.movies.ToString() || mrOption == "movies")
        return displayMostRecent.movies;
      else if (mrOption == displayMostRecent.tvSeries.ToString())
        return displayMostRecent.tvSeries;
      else if (mrOption == displayMostRecent.music.ToString())
        return displayMostRecent.music;
      else if (mrOption == displayMostRecent.recordedTV.ToString())
        return displayMostRecent.recordedTV;
      else if (mrOption == displayMostRecent.freeDriveSpace.ToString())
        return displayMostRecent.freeDriveSpace;
      else if (mrOption == displayMostRecent.htpcInfo.ToString())
        return displayMostRecent.htpcInfo;
      else if (mrOption == displayMostRecent.powerControl.ToString())
        return displayMostRecent.powerControl;
      else if (mrOption == displayMostRecent.sleepControl.ToString())
        return displayMostRecent.sleepControl;
      else if (mrOption == displayMostRecent.stocks.ToString())
        return displayMostRecent.stocks;
      else if (mrOption == displayMostRecent.updateControl.ToString())
        return displayMostRecent.updateControl;
      else
        return displayMostRecent.off;
    }

    fanartSource readFHSource(string source, string fanartProperty)
    {
      if (source == fanartSource.Scraper.ToString())
        return fanartSource.Scraper;
      else if (source == fanartSource.UserDef.ToString())
        return fanartSource.UserDef;
      else
      {
        // Default Music and Movies to scraper images as source
        if (fanartProperty.ToLower() == "music" || fanartProperty.ToLower() == "movie")
          return fanartSource.Scraper;
        else
          return fanartSource.UserDef;
      }
    }

    public static string setDefaultIcons(int hyperlink, string theme)
    {
      if (string.IsNullOrEmpty(theme))
        theme = "black";

      string theIcon;
      switch (hyperlink)
      {
        case 1:
          theIcon = "homebuttons\\" + theme + "_tv.png";
          break;
        case 2:
          theIcon = "homebuttons\\" + theme + "_pictures.png";
          break;
        case 4:
          theIcon = "homebuttons\\" + theme + "_settings.png";
          break;
        case 6:
          theIcon = "homebuttons\\" + theme + "_videos.png";
          break;
        case 30:
          theIcon = "homebuttons\\" + theme + "_radio.png";
          break;
        case 34:
          theIcon = "homebuttons\\" + theme + "_plugins.png";
          break;
        case 501:
        case 504:
          theIcon = "homebuttons\\" + theme + "_music.png";
          break;
        case 2600:
          theIcon = "homebuttons\\" + theme + "_weather.png";
          break;
        case 4755:
          theIcon = "homebuttons\\" + theme + "_onlinevideos.png";
          break;
        case 5900:
          theIcon = "homebuttons\\" + theme + "_trailers.png";
          break;
        case 7890:
          theIcon = "homebuttons\\" + theme + "_lastfm.png";
          break;
        case 9811:
          theIcon = "homebuttons\\" + theme + "_tv-series.png";
          break;
        case 16001:
          theIcon = "homebuttons\\" + theme + "_news.png";
          break;
        case 96742:
          theIcon = "homebuttons\\" + theme + "_movies.png";
          break;
        case 25650:
          theIcon = "homebuttons\\" + theme + "_music.png";
          break;
        case 711992:
          theIcon = "homebuttons\\" + theme + "_showtimes.png";
          break;
        default:
          theIcon = "homebuttons\\" + theme + "_unset.png";
          break;
      }
      return theIcon;
    }


    //void updateBackgroundFolders()
    //{

    //  // Up the location of of the background folders
    //  // Move from sub folders of animations folder to new background folder.
    //  // get list of folders to move
    //  string[] directories = Directory.GetDirectories(SkinInfo.mpPaths.streamedMPpath + "media\\animations");
    //  foreach (string folder in directories)
    //  {
    //    switch (Path.GetFileName(folder).ToLower())
    //    {
    //      case "anvu":
    //        break;
    //      case "ledvu":
    //        break;
    //      case "play":
    //        break;
    //      case "weathericons":
    //        break;
    //      default:
    //        foldersToMove.Add(folder);
    //        break;
    //    }
    //  }

    //  // Create the new directory SMPBackgrounds
    //  if (!Directory.Exists(SkinInfo.mpPaths.streamedMPpath + "media\\SMPBackgrounds"))
    //    Directory.CreateDirectory(SkinInfo.mpPaths.streamedMPpath + "media\\SMPBackgrounds");

    //  // Now move the folders
    //  foreach (string folder in foldersToMove)
    //  {
    //    string fromDir = folder;
    //    string toDir = SkinInfo.mpPaths.streamedMPpath + "media\\SMPBackgrounds\\" + Path.GetFileName(folder);
    //    try
    //    {
    //      Directory.Move(fromDir, toDir);
    //    }
    //    catch (ArgumentNullException)
    //    {
    //      //helper.showError("Path is a null reference.\n\n" + fromDir + "\n\n" + toDir, errorCode.info);
    //    }
    //    catch (System.Security.SecurityException)
    //    {
    //      //helper.showError("The caller does not have the required permission.\n\n" + fromDir + "\n\n" + toDir, errorCode.info);
    //    }
    //    catch (IOException)
    //    {
    //      //helper.showError("An attempt was made to move a directory to a different volume, or destDirName already exists.\nClick Ok to continue processing.\n\n" + fromDir + "\n\n" + toDir, errorCode.info);
    //    }
    //    catch (Exception e)
    //    {
    //      //helper.showError(e.Message + "\n\n" + fromDir + "\n\n" + toDir, errorCode.info);
    //    }
    //  }

    //  // directories moved - update menu image directory to point at new dir
    //  bgFolderName = "SMPBackgrounds";
    //  foreach (menuItem mi in menuItems)
    //  {
    //    if (!mi.fanartHandlerEnabled && mi.defaultImage.ToLower().StartsWith("animations"))
    //    {
    //      mi.defaultImage = bgFolderName + "\\" + mi.bgFolder + "\\" + Path.GetFileName("c:\\" + mi.defaultImage);
    //    }
    //  }
    //  genMenu(true);
    //  itemsOnMenubar.Items.Clear();
    //  bgItems.Clear();
    //  menuItems.Clear();
    //  // reset item id's as it is possible to generate again.
    //  foreach (menuItem item in menuItems)
    //  {
    //    item.id = menuItems.IndexOf(item);
    //  }
    //  loadMenuSettings();
    //}

    //bool foldersUpdated()
    //{
    //  // Ok, quick check to see if the folder have already moved but the usermenuprofile
    //  // has not been updated.
    //  // this could happen if running against SVN or if a clean install with previous usermenuprofile.
    //  if (Directory.Exists(SkinInfo.mpPaths.streamedMPpath + "media\\SMPBackgrounds"))
    //    return true;
    //  else
    //    return false;
    //}

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


    private Color ColorFromRGB(string RGB)
    {
      if (RGB.Length != 6)
        return System.Drawing.Color.FromArgb(255, 255, 255);

      byte R = ColorTranslator.FromHtml("#" + RGB).R;
      byte G = ColorTranslator.FromHtml("#" + RGB).G;
      byte B = ColorTranslator.FromHtml("#" + RGB).B;

      return System.Drawing.Color.FromArgb(int.Parse(R.ToString()),
                                           int.Parse(G.ToString()),
                                           int.Parse(B.ToString()));
    }

  }
}

