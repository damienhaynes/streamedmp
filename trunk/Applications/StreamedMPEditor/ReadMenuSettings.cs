using System;
using System.Drawing;
using System.Xml;
using System.IO;

namespace StreamedMPEditor
{
  public partial class formStreamedMpEditor
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


      int subMenuItems1 = 0;
      int subMenuItems2 = 0;

      bool folderUpdateRequired = false;

      itemsOnMenubar.Items.Clear();

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
          helper.showError("Can't find usermenuprofile.xml \r\r" + SkinInfo.mpPaths.configBasePath + "usermenuprofile.xml", errorCode.major);
        }
      }
      try
      {
        doc.Load(usermenuprofile);
      }
      catch (Exception e)
      {
        helper.showError("Exception while loading usermenuprofile.xml\n\nUnable to Continue - please restore from backup" + e.Message, errorCode.major);
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
          if (foldersUpdated())
            bgFolderName = "SMPbackgrounds";           
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
            verticalStyle.Checked = true;
            cboLabelFont.Enabled = false;
            cboSelectedFont.Enabled = false;
            cboSelectedFont.Text = "mediastream16tc";
            cboLabelFont.Text = "mediastream28tc";
            break;
          case "horizontalStandardStyle":
            menuStyle = chosenMenuStyle.horizontalStandardStyle;
            horizontalStyle.Checked = true;
            cboSelectedFont.Text = "mediastream28tc";
            cboLabelFont.Text = "mediastream28tc";
            break;
          case "horizontalContextStyle":
            menuStyle = chosenMenuStyle.horizontalContextStyle;
            horizontalStyle2.Checked = true;
            cboSelectedFont.Text = "mediastream28tc";
            cboLabelFont.Text = "mediastream28tc";
            break;
          case "graphicMenuStyle":
            menuStyle = chosenMenuStyle.graphicMenuStyle;
            graphicalStyle.Checked = true;
            cboSelectedFont.Text = "mediastream28tc";
            cboLabelFont.Text = "mediastream28tc";
            break;
          default:
            menuStyle = chosenMenuStyle.verticalStyle;
            verticalStyle.Checked = true;
            cboLabelFont.Enabled = false;
            cboSelectedFont.Enabled = false;
            cboSelectedFont.Text = "mediastream16tc";
            cboLabelFont.Text = "mediastream28tc";
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
      focusAlpha.Text = readEntryValue(optionsTag, "menuitemFocus", nodelist).Substring(0, 2);
      focusAlphaSlider.Value = int.Parse(focusAlpha.Text, System.Globalization.NumberStyles.HexNumber);
      try
      {
        string RGB = defFocus;
        RGB = readEntryValue(optionsTag, "menuitemFocus", nodelist).Substring(2);
        Color col = ColorFromRGB(RGB);
        txtFocusColour.BackColor = col;
        txtFocusColour.ForeColor = ColorInvert(col);
        txtFocusColour.Text = RGB;
      }
      catch
      {
        txtFocusColour.Text = defFocus;
      }

      // Get the NoFocus Colour and set the background on the control
      noFocusAlpha.Text = readEntryValue(optionsTag, "menuitemNoFocus", nodelist).Substring(0, 2);
      noFocusAlphaSlider.Value = int.Parse(noFocusAlpha.Text, System.Globalization.NumberStyles.HexNumber);
      try
      {
        string RGB = defUnFocus;
        RGB = readEntryValue(optionsTag, "menuitemNoFocus", nodelist).Substring(2);
        Color col = ColorFromRGB(RGB);
        txtNoFocusColour.BackColor = col;
        txtNoFocusColour.ForeColor = ColorInvert(col);
        txtNoFocusColour.Text = RGB;
      }
      catch
      {
        txtNoFocusColour.Text = defUnFocus;
      }


      // Line up all the options this also sets the defaults for the style
      // which can be overidden by user settings below
      syncEditor(sync.OnLoad);

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
        txtMenuPos.Text = readEntryValue(optionsTag, "menuXPos", nodelist);
      else
        txtMenuPos.Text = readEntryValue(optionsTag, "menuYPos", nodelist);



      //
      // Check and set the Global and Plugin options and apply any customization by user
      //
      try
      {
        _selectedFont = readEntryValue(optionsTag, "selectedFont", nodelist);
        _labelFont = readEntryValue(optionsTag, "labelFont", nodelist);
        tbAcceleration.Text = readEntryValue(optionsTag, "acceleration", nodelist);
        tbDuration.Text = readEntryValue(optionsTag, "duration", nodelist);
        cbDropShadow.Checked = bool.Parse(readEntryValue(optionsTag, "dropShadow", nodelist));
        enableRssfeed.Checked = bool.Parse(readEntryValue(optionsTag, "enableRssfeed", nodelist));
        enableTwitter.Checked = bool.Parse(readEntryValue(optionsTag, "enableTwitter", nodelist));
        wrapString.Checked = bool.Parse(readEntryValue(optionsTag, "wrapString", nodelist));
        weatherBGlink.Checked = bool.Parse(readEntryValue(optionsTag, "weatherBGlink", nodelist));
        enableFiveDayWeather.Checked = bool.Parse(readEntryValue(optionsTag, "fiveDayWeatherCheckBox", nodelist));
        summaryWeatherCheckBox.Checked = bool.Parse(readEntryValue(optionsTag, "summaryWeatherCheckBox", nodelist));
        cboClearCache.Checked = bool.Parse(readEntryValue(optionsTag, "cboClearCache", nodelist));
        WeatherIconsAnimated.Checked = bool.Parse(readEntryValue(optionsTag, "animatedWeather", nodelist));
        horizontalContextLabels.Checked = bool.Parse(readEntryValue(optionsTag, "horizontalContextLabels", nodelist));
        fullWeatherSummaryBottom.Checked = bool.Parse(readEntryValue(optionsTag, "fullWeatherSummaryBottom", nodelist));
        fullWeatherSummaryMiddle.Checked = bool.Parse(readEntryValue(optionsTag, "fullWeatherSummaryMiddle", nodelist));
        activeRssImageType = readEntryValue(optionsTag, "activeRssImageType", nodelist);
        cbDisableClock.Checked = bool.Parse(readEntryValue(optionsTag, "disableOnScreenClock", nodelist));
        targetScreenRes = readEntryValue(optionsTag, "targetScreenRes", nodelist);
        splashScreenImage = readEntryValue(optionsTag, "splashScreenImage", nodelist);
        cbHideFanartScraper.Checked = bool.Parse(readEntryValue(optionsTag, "hideFanartScrapingtext", nodelist));
        cbOverlayFanart.Checked = bool.Parse(readEntryValue(optionsTag, "enableOverlayFanart", nodelist));
        cbAnimateBackground.Checked = bool.Parse(readEntryValue(optionsTag, "animatedBackground", nodelist));
        cbMostRecentTvSeries.Checked = bool.Parse(readEntryValue(optionsTag, "tvSeriesMostRecent", nodelist));
        cbMostRecentMovPics.Checked = bool.Parse(readEntryValue(optionsTag, "movPicsMostRecent", nodelist));
        tvRecentDisplayType = readEntryValue(optionsTag, "tvRecentDisplayType", nodelist);
        movPicsDisplayType = readEntryValue(optionsTag, "movPicsDisplayType", nodelist);
        mostRecentSumStyle = readEntryValue(optionsTag, "mostRecentSumStyle", nodelist);  // Hang over from when juts TVSeries was supported
        mostRecentTVSeriesSummStyle = readEntryValue(optionsTag, "mostRecentTVSeriesSummStyle", nodelist);
        mostRecentMovPicsSummStyle = readEntryValue(optionsTag, "mostRecentMovPicsSummStyle", nodelist);
        cbCycleFanart.Checked = bool.Parse(readEntryValue(optionsTag, "mostRecentCycleFanart", nodelist));
        tvSeriesOptions.mrSeriesEpisodeFormat = bool.Parse(readEntryValue(optionsTag, "mrSeriesEpisodeFormat", nodelist));
        tvSeriesOptions.mrTitleLast = bool.Parse(readEntryValue(optionsTag, "mrTitleLast", nodelist));
        cbExitStyleNew.Checked = bool.Parse(readEntryValue(optionsTag, "settingOldStyleExitButtons", nodelist));
        mostRecentTVSeriesCycleFanart = bool.Parse(readEntryValue(optionsTag, "mrTVSeriesCycleFanart", nodelist));
        mostRecentMovPicsCycleFanart = bool.Parse(readEntryValue(optionsTag, "mrMovPicsCycleFanart", nodelist));
        tvSeriesOptions.mrEpisodeFont = readEntryValue(optionsTag, "mrEpisodeFont", nodelist);
        tvSeriesOptions.mrSeriesFont = readEntryValue(optionsTag, "mrSeriesFont", nodelist);
        movPicsOptions.MovieTitleFont = readEntryValue(optionsTag, "mrMovieTitleFont", nodelist);
        movPicsOptions.MovieDetailFont = readEntryValue(optionsTag, "mrMovieDetailFont", nodelist);
        movPicsOptions.HideRuntime = bool.Parse(readEntryValue(optionsTag, "mrMovPicsHideRuntime", nodelist));
        movPicsOptions.HideCertification = bool.Parse(readEntryValue(optionsTag, "mrMovPicsHideCertification", nodelist));
        movPicsOptions.HideRating = bool.Parse(readEntryValue(optionsTag, "mrMovPicsHideRating", nodelist));
        movPicsOptions.UseTextRating = bool.Parse(readEntryValue(optionsTag, "mrMovPicsUseTextRating", nodelist));
        cbMovPicsRecentWatched.Checked = bool.Parse(readEntryValue(optionsTag, "mrMovPicsWatched", nodelist));
        cbTVSeriesRecentWatched.Checked = bool.Parse(readEntryValue(optionsTag, "mrTVSeriesWatched", nodelist));
        tvSeriesOptions.mrDisableFadeLabels = bool.Parse(readEntryValue(optionsTag, "mrTVSeriesDisableFadeLabel", nodelist));
        movPicsOptions.DisableFadeLabels = bool.Parse(readEntryValue(optionsTag, "mrMovPicsDisableFadeLabel", nodelist));
        cbEnableRecentRecordedTV.Checked = bool.Parse(readEntryValue(optionsTag, "mrRecordedTVEnabled", nodelist));
        cbEnableRecentMusic.Checked = bool.Parse(readEntryValue(optionsTag, "mrMusicEnabled", nodelist));
        driveFreeSpaceList = readEntryValue(optionsTag, "driveFreeSpaceList", nodelist);
        cbSleepControlOverlay.Checked = bool.Parse(readEntryValue(optionsTag, "sleepControlEnabled", nodelist));
        cbSocksOverlay.Checked = bool.Parse(readEntryValue(optionsTag, "stocksControlEnabled", nodelist));
        cbPowerControlOverlay.Checked = bool.Parse(readEntryValue(optionsTag, "powerControlEnabled", nodelist));
        cbHtpcInfoOverlay.Checked = bool.Parse(readEntryValue(optionsTag, "htpcinfoControlEnabled", nodelist));
        cbUpdateControlOverlay.Checked = bool.Parse(readEntryValue(optionsTag, "updateControlEnabled", nodelist));
        cbDisableExitMenu.Checked = bool.Parse(readEntryValue(optionsTag, "disableExitMenu", nodelist));
        cbContextLabelBelow.Checked = bool.Parse(readEntryValue(optionsTag, "contextLabelBelow", nodelist));      
      }
      catch
      {
        // Most likley a new option added but not written to file yet - just continue
      }

      if (string.IsNullOrEmpty(driveFreeSpaceList))
        driveFreeSpaceList = "false";

      if (!(driveFreeSpaceList == "false"))
      {
        cbFreeDriveSpaceOverlay.Checked = true;
        string[] configuredDrives = driveFreeSpaceList.Split(',');
        foreach (string hd in configuredDrives)
        {
          DriveInfo hdDetails = new DriveInfo(hd);
          string thisDrive = hd + " (" + hdDetails.VolumeLabel + ")";
          formStreamedMpEditor.driveFreeSpaceDrives.Add(hd);
        }
      }
      


      if (tvSeriesOptions.mrSeriesFont == "mediastream9c")
        tvSeriesOptions.mrSeriesFont = "mediastream10c";

      if (tvSeriesOptions.mrEpisodeFont == "mediastream9c")
        tvSeriesOptions.mrEpisodeFont = "mediastream10c";

      if (tvSeriesOptions.mrSeriesFont == "false" || tvSeriesOptions.mrEpisodeFont == "false")
      {
        tvSeriesOptions.mrSeriesFont = "mediastream10c";
        tvSeriesOptions.mrEpisodeFont = "mediastream10tc (Bold)";
      }

      if (movPicsOptions.MovieTitleFont == "false" || movPicsOptions.MovieDetailFont == "false")
      {
        movPicsOptions.MovieTitleFont = "mediastream10tc (Bold)";
        movPicsOptions.MovieDetailFont = "mediastream10c";
      }

      // As only saving the animated state set the static state true if animimated state is false
      if (!animatedIconsInstalled())
      {
        WeatherIconsAnimated.Enabled = false;
        WeatherIconsAnimated.Checked = false;
      }
      if (WeatherIconsAnimated.Checked)
        weatherIconsStatic.Checked = false;
      else
        weatherIconsStatic.Checked = true;



      if (!weatherBackgoundsInstalled())
      {
        weatherBGlink.Checked = false;
        weatherBGlink.Enabled = false;
        weatherBGlink.Text = "Link Background to Current Weather (Not Installed)";
        installWeatherBackgrounds.Visible = true;
      }

      if (tvRecentDisplayType == "summary" || movPicsDisplayType == "summary")
        btFormatOptions.Enabled = true;

      if (tvRecentDisplayType == "summary")
      {
        tvSeriesRecentStyle = tvSeriesRecentType.summary;
        rbTVSeriesSummary.Checked = true;
        gbSummaryStyle.Enabled = true;
      }
      else
      {
        tvSeriesRecentStyle = tvSeriesRecentType.full;
        rbTBSeriesFull.Checked = true;
        gbSummaryStyle.Enabled = false;

      }

      if (movPicsDisplayType == "summary")
      {
        movPicsRecentStyle = movPicsRecentType.summary;
        rbMovPicsSummary.Checked = true;
      }
      else
      {
        movPicsRecentStyle = movPicsRecentType.full;
        rbMovPicsFull.Checked = true;
      }

      // TVSeries most recent summry Style
      if (mostRecentTVSeriesSummStyle == "fanart")
      {
        mrTVSeriesSummStyle = mostRecentTVSeriesSummaryStyle.fanart;
        rbFanartStyle.Checked = true;
      }
      else if (mostRecentTVSeriesSummStyle == "poster")
      {
        mrTVSeriesSummStyle = mostRecentTVSeriesSummaryStyle.poster;
        rbPosterStyle.Checked = true;
      }
      else
      {
        //Default to Fanart Style
        mrTVSeriesSummStyle = mostRecentTVSeriesSummaryStyle.fanart;
        rbFanartStyle.Checked = true;
      }

      // Moving Pictures most recent summry Style
      if (mostRecentMovPicsSummStyle == "fanart")
      {
        mrMovPicsSummStyle = mostRecentMovPicsSummaryStyle.fanart;
        rbFanartStyle.Checked = true;
      }
      else if (mostRecentMovPicsSummStyle == "poster")
      {
        mrMovPicsSummStyle = mostRecentMovPicsSummaryStyle.poster;
        rbPosterStyle.Checked = true;
      }
      else
      {
        //Default to Fanart Style
        mrMovPicsSummStyle = mostRecentMovPicsSummaryStyle.fanart;
        rbFanartStyle.Checked = true;
      }

      //
      // Old Setting - if found in usermenuprofile use as setting for TVSeries Summary Style
      //
      if (mostRecentSumStyle == "fanart")
      {
        mrTVSeriesSummStyle = mostRecentTVSeriesSummaryStyle.fanart;
        rbFanartStyle.Checked = true;
      }
      else if (mostRecentSumStyle == "poster")
      {
        mrTVSeriesSummStyle = mostRecentTVSeriesSummaryStyle.poster;
        rbPosterStyle.Checked = true;
      }

      if (splashScreenImage == "false")
        splashScreenImage = "splashscreen.png";

      if (targetScreenRes == "HD")
        setHDScreenRes();
      else if (targetScreenRes == "SD")
        setSDScreenRes();

      switch (activeRssImageType)
      {
        case "infoservice":
          rssImage = rssImageType.infoserviceImage;
          rbRssInfoServiceImage.Checked = true;
          break;
        case "noimage":
          rssImage = rssImageType.noImage;
          rbRssNoImage.Checked = true;
          break;
        case "skin":
          rssImage = rssImageType.skinImage;
          rbRssSkinImage.Checked = true;
          break;
        default:
          rssImage = rssImageType.skinImage;
          rbRssSkinImage.Checked = true;
          break;
      }

      if (_selectedFont != "false")
      {
        cboSelectedFont.Text = _selectedFont;
      }
      if (_labelFont != "false")
      {
        cboLabelFont.Text = _labelFont;
      }

      if (menuStyle == chosenMenuStyle.verticalStyle)
      {
        txtMenuPos.Text = readEntryValue(optionsTag, "menuXPos", nodelist);
        cbContextLabelBelow.Enabled = true;
      }
      else
      {
        txtMenuPos.Text = readEntryValue(optionsTag, "menuYPos", nodelist);
        cbContextLabelBelow.Enabled = false;
      }

      Version isver = new Version("1.6.0.0");
      if (getInfoServiceVersion().CompareTo(isver) >= 0)
        infoServiceDayProperty = "forecast";
      else
        infoServiceDayProperty = "day";

      // Check if Moving Pictures is installed and enabled, if not disable most recent options
      if (MovingPicturesVersion == "0.0.0.0")
      {
        pMovPicsRecent.Enabled = false;
        cbMostRecentMovPics.Checked = false;
        cbMovPicsRecentWatched.Checked = false;
        cbMostRecentMovPics.Refresh();
      }
      else
      {
        if (!helper.pluginEnabled(Helper.Plugins.MovingPictures))
        {
          pMovPicsRecent.Enabled = false;
          cbMostRecentMovPics.Checked = false;
          cbMovPicsRecentWatched.Checked = false;
          cbMostRecentMovPics.Refresh();
        }
      }

      // Check in TVSeries is installed and enabled, if not disable most recent options
      if (TVSeriesVersion == "0.0.0.0")
      {
        pTVSeriesRecent.Enabled = false;
        cbMostRecentTvSeries.Checked = false;
        cbTVSeriesRecentWatched.Checked = false;
        cbMostRecentTvSeries.Refresh();
      }
      else
      {
        if (!helper.pluginEnabled(Helper.Plugins.MPTVSeries))
        {
          pTVSeriesRecent.Enabled = false;
          cbMostRecentTvSeries.Checked = false;
          cbTVSeriesRecentWatched.Checked = false;
          cbMostRecentTvSeries.Refresh();
        }
      }
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
        if (mnuItem.xmlFileName == "false")
          mnuItem.xmlFileName = getXMLFileName(mnuItem.hyperlink);

        mnuItem.showMostRecent = readMostRecentDisplayOption(readEntryValue(menuTag, "menuitem" + i.ToString() + "showMostRecent", nodelist), mnuItem.hyperlink);
        mnuItem.fhBGSource = readFHSource(readEntryValue(menuTag, "menuitem" + i.ToString() + "fanartSource", nodelist), mnuItem.fanartProperty);
        //
        // Graphical Menu Default Image Load
        //
        if (string.IsNullOrEmpty(mnuItem.buttonTexture) || mnuItem.buttonTexture.ToLower() == "false")
        {
          mnuItem.buttonTexture = setDefaultIcons(int.Parse(mnuItem.hyperlink),"Black");
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

        isWeather.Checked = mnuItem.isWeather;
        disableBGSharing.Checked = mnuItem.disableBGSharing;

        if (mnuItem.fanartHandlerEnabled)
          checkAndSetRandomFanart(mnuItem.fanartProperty);

        // Set the default control
        if (mnuItem.isDefault)
          defaultcontrol = mnuItem.id.ToString();
        id = mnuItem.id.ToString();
        itemsOnMenubar.Items.Add(mnuItem.name, id.Equals(defaultcontrol));

        // If user decides not to use multiimage backgrounds then we need a default image, lets check and set if one is required
        defaultImage = readEntryValue(menuTag, "menuitem" + i.ToString() + "defaultimage", nodelist);

        // Version 2.0 usermenu profile - check if folder structure updated but menu has not.
        // if so replace "Animations" with "SMPBackgrounds"
        // Belts and Braces checks
        if (versionNum == "1.0" && foldersUpdated())
        {
          if (defaultImage.ToLower().StartsWith("animations"))
          {
            defaultImage = bgFolderName + "\\" + mnuItem.bgFolder + "\\" + Path.GetFileName("C:\\" + defaultImage);
          }
        }

        // Check if the stored image still exists, if nor set to default.jpg
        if (!System.IO.File.Exists((imageDir(defaultImage))))
          defaultImage = bgFolderName + "\\" + mnuItem.bgFolder + "\\default.jpg";

        if (defaultImage.ToLower().StartsWith(bgFolderName.ToLower()))
          mnuItem.defaultImage = defaultImage;
        else
        {
          if (!mnuItem.bgFolder.Contains("\\"))
            mnuItem.defaultImage = bgFolderName + "\\" + mnuItem.bgFolder + "\\default.jpg";
          else
            mnuItem.defaultImage = mnuItem.bgFolder + "\\default.jpg";
        }

        menuItems.Add(mnuItem);
      }
      reloadBackgroundItems();
      //UpdateImageControlVisibility();
      btGenerateMenu.Enabled = true;

      if (folderUpdateRequired)
        updateBackgroundFolders();
    }

    string getXMLFileName(string hyperLink)
    {
      int index = ids.IndexOf(hyperLink);
      string firstFound;

      try
      {

        firstFound = xmlFiles.Items[index].ToString();

        index = ids.IndexOf(hyperLink, index + 1);
        if (index != -1 && hyperLink == "1")
        {
          if (!helper.pluginEnabled(Helper.Plugins.ForTheRecord))
            firstFound = xmlFiles.Items[index].ToString();
        }
      }
      catch
      {
        firstFound = "false";
      }

      return firstFound;
    }

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
          theIcon =  "homebuttons\\" + theme +"_tv.png";
          break;
        case 2:
          theIcon = "homebuttons\\" + theme +"_pictures.png";
          break;
        case 4:
          theIcon = "homebuttons\\" + theme +"_settings.png";
          break;
        case 6:
          theIcon = "homebuttons\\" + theme +"_videos.png";
          break;
        case 30:
          theIcon = "homebuttons\\" + theme +"_radio.png";
          break;
        case 34:
          theIcon = "homebuttons\\" + theme +"_plugins.png";
          break;
        case 501:
        case 504:
          theIcon = "homebuttons\\" + theme +"_music.png";
          break;
        case 2600:
          theIcon = "homebuttons\\" + theme +"_weather.png";
          break;
        case 4755:
          theIcon = "homebuttons\\" + theme +"_onlinevideos.png";
          break;
        case 5900:
          theIcon = "homebuttons\\" + theme +"_trailers.png";
          break;
        case 7890:
          theIcon = "homebuttons\\" + theme +"_lastfm.png";
          break;
        case 9811:
          theIcon = "homebuttons\\" + theme +"_tv-series.png";
          break;
        case 16001:
          theIcon = "homebuttons\\" + theme +"_news.png";
          break;
        case 96742:
          theIcon = "homebuttons\\" + theme +"_movies.png";
          break;
        case 25650:
          theIcon = "homebuttons\\" + theme +"_music.png";
          break;
        case 711992:
          theIcon = "homebuttons\\" + theme +"_showtimes.png";
          break;
        default:
          theIcon = "homebuttons\\" + theme +"_unset.png";
          break;
      }
      return theIcon;
    }


    void updateBackgroundFolders()
    {

      // Up the location of of the background folders
      // Move from sub folders of animations folder to new background folder.
      // get list of folders to move
      string[] directories = Directory.GetDirectories(SkinInfo.mpPaths.streamedMPpath + "media\\animations");
      foreach (string folder in directories)
      {
        switch (Path.GetFileName(folder).ToLower())
        {
          case "anvu":
            break;
          case "ledvu":
            break;
          case "play":
            break;
          case "weathericons":
            break;
          default:
            foldersToMove.Add(folder);
            break;
        }
      }

      // Create the new directory SMPBackgrounds
      if (!Directory.Exists(SkinInfo.mpPaths.streamedMPpath + "media\\SMPBackgrounds"))
        Directory.CreateDirectory(SkinInfo.mpPaths.streamedMPpath + "media\\SMPBackgrounds");

      // Now move the folders
      foreach (string folder in foldersToMove)
      {
        string fromDir = folder;
        string toDir = SkinInfo.mpPaths.streamedMPpath + "media\\SMPBackgrounds\\" + Path.GetFileName(folder);

        copyDirectory(fromDir, toDir);
        Directory.Delete(fromDir, true);
      }


      // directories moved - update menu image directory to point at new dir
      bgFolderName = "SMPBackgrounds";
      foreach (menuItem mi in menuItems)
      {
        if (!mi.fanartHandlerEnabled && mi.defaultImage.ToLower().StartsWith("animations"))
        {
          mi.defaultImage = bgFolderName + "\\" + mi.bgFolder + "\\" + Path.GetFileName("c:\\" + mi.defaultImage);
        }
      }
      genMenu(true);
      itemsOnMenubar.Items.Clear();
      bgItems.Clear();
      menuItems.Clear();
      // reset item id's as it is possible to generate again.
      foreach (menuItem item in menuItems)
      {
        item.id = menuItems.IndexOf(item);
      }
      loadMenuSettings();
    }

    void copyDirectory(string sourceDir, string destinationDir)
    {
      String[] backgroundFiles;

      if (destinationDir[destinationDir.Length - 1] != Path.DirectorySeparatorChar)
        destinationDir += Path.DirectorySeparatorChar;

      if (!Directory.Exists(destinationDir))
        Directory.CreateDirectory(destinationDir);

      backgroundFiles = Directory.GetFileSystemEntries(sourceDir);

      foreach (string Element in backgroundFiles)
      {
        if (Directory.Exists(Element))
          copyDirectory(Element, destinationDir + Path.GetFileName(Element));
        else
            File.Copy(Element, destinationDir + Path.GetFileName(Element), true);
      }
    }


    bool foldersUpdated()
    {
      // Ok, quick check to see if the folder have already moved but the usermenuprofile
      // has not been updated.
      // this could happen if running against SVN or if a clean install with previous usermenuprofile.
      if (Directory.Exists(SkinInfo.mpPaths.streamedMPpath + "media\\SMPBackgrounds"))
        return true;
      else
        return false;
    }

  }
}


