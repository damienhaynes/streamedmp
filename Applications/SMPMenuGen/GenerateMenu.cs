using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SMPCheckSum;

namespace SMPMenuGen
{
  public partial class GenerateMenu
  {

    #region enums

    public enum errorCode
    {
      info,
      infoQuestion,
      loadError,
      readError,
      major,
    };

    public enum chosenMenuStyle
    {
      verticalStyle,
      horizontalStandardStyle,
      horizontalContextStyle,
      graphicMenuStyle
    };

    enum chosenWeatherStyle
    {
      bottom,
      middle
    }

    enum menuType
    {
      vertical,
      horizontal,
    };

    enum sync
    {
      OnLoad,
      editing,
    };

    enum rssImageType
    {
      noImage,
      skinImage,
      infoserviceImage,
    };

    enum screenResolutionType
    {
      res1280x720,
      res1920x1080
    };

    public enum CompareResult
    {
      ciCompareOk,
      ciPixelMismatch,
      ciSizeMismatch
    };

    enum tvSeriesRecentType
    {
      summary,
      full
    }

    enum movPicsRecentType
    {
      summary,
      full
    }

    enum mostRecentTVSeriesSummaryStyle
    {
      poster,
      fanart
    }

    enum mostRecentMovPicsSummaryStyle
    {
      poster,
      fanart
    }

    public enum displayMostRecent
    {
      off,
      tvSeries,
      movies,
      music,
      recordedTV,
      freeDriveSpace,
      powerControl,
      sleepControl,
      stocks,
      htpcInfo,
      updateControl
    }

    enum isOverlayType
    {
      TVSeries,
      MovPics,
      Music,
      RecordedTV,
      freeDriveSpace,
      powerControl,
      sleepControl,
      stocks,
      htpcInfo,
      updateControl
    }

    public enum fanartSource
    {
      Scraper,
      UserDef
    }

    #endregion

    #region Variables

    public MenuDefinition menudef = new MenuDefinition();


    public static List<menuItem> menuItems = new List<menuItem>();
    public static List<string> driveFreeSpaceDrives = new List<string>();


    public static List<string> theTVSeriesViews = new List<string>();
    public static List<string> theMusicViews = new List<string>();
    public static List<string> theOnlineVideosViews = new List<string>();

    List<backgroundItem> bgItems = new List<backgroundItem>();
    List<string> ids = new List<string>();
    List<string> idsTemp = new List<string>();
    List<string> skinFontsFocused = new List<string>();
    List<string> skinFontsUnFocused = new List<string>();
    List<string> foldersToMove = new List<string>();


    public const string tvseriesSkinID = "9811";
    public const string movingPicturesSkinID = "96742";
    public const string musicSkinID = "501";
    public const string tvMenuSkinID = "1";
    public const string onlineVideosSkinID = "4755";
    public const bool hyperlinkParameterEnabled = true;
    public const bool hyperlinkParameterDisabled = false;
    const string quote = "\"";

    public static bool basicHomeLoadError = false;
    public static bool changeOutstanding = false;

    bool useInfoServiceSeparator = false;
    bool fanartHandlerUsed = false;
    bool exitCondition = false;
    bool mostRecentTVSeriesCycleFanart = true;
    bool mostRecentMovPicsCycleFanart = true;
    bool subMenuL1Exists = false;
    bool subMenuL2Exists = false;
    bool fanartHandlerRelease2 = false;
    public static bool hlWarningDone = false;
    public static bool isAlpha = false;
    public static bool isBeta = false;

    string xml;
    string xmlTemplate;
    string dropShadowColor = "1d1f1b";
    string infoServiceDayProperty = "forecast";
    string splashScreenImage = null;
    string defFocus = "FFFFFF";
    string defUnFocus = "C0C0C0";
    string level1LateralBladeVisible;
    string level2LateralBladeVisible;
    public string bgFolderName = "animations";
    public string fhUserDef = string.Empty;
    public string fhSuffix = ".any";
    public static string driveFreeSpaceList = string.Empty;

    int textXOffset = -25;
    int maxXPosition = 520;
    int menuOffset = 0;
    int deskHeight;
    int deskWidth;

    bool xmlFilesDisplayed = false;

    Version baseISVer = new Version("0.9.9.3");
    Version baseISVerTwitter = new Version("1.2.0.0");
    Version isSeparatorVer = new Version("1.1.0.0");
    Version mpReleaseVersion = new Version("1.0.2.22554");
    Version isWeatherVersion = new Version("1.6.0.0");
    Version fanarthandlerVersionRequired = new Version("2.2.0.0");
    Version fhRelease2 = new Version("2.2.2.0");

    Version mpAlphaRelease = new Version("1.1.5.0");
    Version mpBetaRelease = new Version("1.1.5.26731");

    public Version fhOverlayVersion;


    // Default Style to StreamedMP standard
    public static chosenMenuStyle menuStyle = chosenMenuStyle.verticalStyle;
    chosenWeatherStyle weatherStyle = chosenWeatherStyle.bottom;
    rssImageType rssImage = rssImageType.skinImage;

    // Default to Full Details
    tvSeriesRecentType tvSeriesRecentStyle = tvSeriesRecentType.full;
    movPicsRecentType movPicsRecentStyle = movPicsRecentType.full;

    //Defualt to Fanart Summary Style
    mostRecentTVSeriesSummaryStyle mrTVSeriesSummStyle = mostRecentTVSeriesSummaryStyle.fanart;
    mostRecentMovPicsSummaryStyle mrMovPicsSummStyle = mostRecentMovPicsSummaryStyle.fanart;

    // Defaut to SD res - this is any resoloution other than 1920x1080 (FullHD)
    screenResolutionType screenres = screenResolutionType.res1280x720;
    screenResolutionType detectedres = screenResolutionType.res1280x720;

    //Most Recent
    public static List<KeyValuePair<string, string>> tvseriesViews = new List<KeyValuePair<string, string>>();
    public static List<KeyValuePair<string, string>> musicViews = new List<KeyValuePair<string, string>>();
    public static List<KeyValuePair<string, string>> onlineVideosViews = new List<KeyValuePair<string, string>>();

    Helper helper = new Helper();
    randomFanartSetting randomFanart = new randomFanartSetting();
    editorValues basicHomeValues = new editorValues();
    CheckSum checkSum = new CheckSum();

    #endregion

    #region Constructor

    public GenerateMenu()
    {
      SkinInfo skinInfo = new SkinInfo();

      // Check in Infoservice is enabled
      if (!helper.pluginEnabled(Helper.Plugins.InfoService))
        menudef.infoServiceOptions = false;
      else
        menudef.infoServiceOptions = true;

      loadMenuSettings();
      setBasicHomeValues();


      if (menuStyle == chosenMenuStyle.verticalStyle)
      {
        writeMenu(menuType.vertical, true);
      }
      else
      {
        writeMenu(menuType.horizontal, true);
      }

    }

    #endregion

    #region Public methods

    public bool validForMPVersion(string validForMPVersion)
    {
      Version versionToTest = new Version(validForMPVersion);
      Version mpVersion = new Version(MediaPortalVersion);

      if (mpVersion.CompareTo(versionToTest) > 0)
        return true;
      else
        return false;
    }


    #endregion

    #region Private Methods

    void writeMenu(menuType direction, bool onFormClosing)
    {

      randomFanart.fanartGames = false;
      randomFanart.fanartMovies = false;
      randomFanart.fanartMoviesScraperFanart = false;
      randomFanart.fanartMovingPictures = false;
      randomFanart.fanartMusic = false;
      randomFanart.fanartMusicScraperFanart = false;
      randomFanart.fanartPictures = false;
      randomFanart.fanartPlugins = false;
      randomFanart.fanartTv = false;
      randomFanart.fanartTVSeries = false;
      randomFanart.fanartScoreCenter = false;

      switchOnOffFHControls();
      assoicateDefaultItems();
      generateXML(direction);
      generateBg(direction);

      if (fanartHandlerUsed)
      {
        generateFanartControls();
        generateBackgroundLoading();
      }

      if (!menudef.disableClock)
        generateClock();

      if (!menudef.hideFanartScraper)
        generatefanartScraper();

      if (menudef.enableFiveDayWeather)
        GenerateFiveDayWeather();

      if (menudef.summaryWeatherEnabled && menudef.infoServiceOptions)
      {
        bool itemHasWeather = false;
        foreach (backgroundItem item in bgItems)
        {
          // Check what item to hide weather summary on
          if (item.isWeather)
          {
            itemHasWeather = true;
            basicHomeValues.weatherControl = (int.Parse(item.ids[0]) + 200);
            generateWeathersummary(basicHomeValues.weatherControl);
          }
        }
        // always show weather summary
        if (!itemHasWeather) generateWeathersummary(null);
      }

      if (direction == menuType.horizontal)
      {
        if (!menudef.disableExitMenu)
          generateTopBarH();

        generateMenuGraphicsH();

        if (menuStyle == chosenMenuStyle.graphicMenuStyle)
          generateGraphicCrowdingFixH();
        else
          generateCrowdingFixH();

        if (menudef.horizontalContextLabels)
          GenerateContextLabelsH();
      }
      else if (direction == menuType.vertical)
      {
        if (!menudef.disableExitMenu)
        {
          if (!menudef.exitStyleNew)
            generateTopBarV1();
          else
            generateTopBarV();
        }

        generateMenuGraphicsV();
        generateCrowdingFixV();
        GenerateContextLabelsV();
      }

      if (menudef.rssEnabled && menudef.infoServiceOptions)
      {
        if (direction == menuType.horizontal)
        {
          generateRSSTicker();
          if (menudef.twitterEnabled && menudef.infoServiceOptions) generateTwitter();
        }
        else if (direction == menuType.vertical)
        {
          generateRSSTickerV();
          if (menudef.twitterEnabled && menudef.infoServiceOptions) generateTwitterV();
        }
      }

      generateMostRecentFilesAndImports("AddImports");


      if (System.IO.File.Exists(SkinInfo.mpPaths.streamedMPpath + "BasicHome.xml"))
        System.IO.File.Delete(SkinInfo.mpPaths.streamedMPpath + "BasicHome.xml");

      xml = xml.Replace("<!-- BEGIN GENERATED ID CODE-->", "<id>35</id>");
      writeXMLFile("BasicHome.xml");

      generateMostRecentFilesAndImports("GenImports");
      if (menuStyle == chosenMenuStyle.graphicMenuStyle)
        //generateOverlay(int.Parse(txtMenuPos.Text), 0, basicHomeValues.weatherControl);
        generateOverlay(0, 380, basicHomeValues.weatherControl);
      else
        generateOverlay(int.Parse(menudef.menuPos), 765, basicHomeValues.weatherControl);

      if (!onFormClosing)
      {
        //reset everything
        ids.Clear();
        bgItems.Clear();
        menuItems.Clear();
        // reset item id's as it is possible to generate again.
        foreach (menuItem item in menuItems)
        {
          item.id = menuItems.IndexOf(item);
        }
      }
    }

    //
    // Switch on or off the FH controls
    //
    void switchOnOffFHControls()
    {
      foreach (menuItem menItem in menuItems)
      {
        if (menItem.fanartHandlerEnabled)
        {
          // Check and set random fanart
          if (menItem.fanartProperty.ToLower().Contains("games"))
            randomFanart.fanartGames = true;

          if (menItem.fanartProperty.ToLower().Contains("plugins"))
            randomFanart.fanartPlugins = true;

          if (menItem.fanartProperty.ToLower().Contains("picture"))
            randomFanart.fanartPictures = true;

          if (menItem.fanartProperty.ToLower().Contains("tv"))
            randomFanart.fanartTv = true;

          if (menItem.fanartProperty.ToLower().Contains("music"))
          {
            if (fanartHandlerRelease2)
            {
              if (menItem.fhBGSource == fanartSource.Scraper)
              {
                randomFanart.fanartMusic = false;
                randomFanart.fanartMusicScraperFanart = true;
              }
              else
              {
                randomFanart.fanartMusic = true;
                randomFanart.fanartMusicScraperFanart = false;
              }
            }
            else
              randomFanart.fanartMusic = true;
          }

          if (menItem.fanartProperty.ToLower().Contains("tvseries"))
            randomFanart.fanartTVSeries = true;

          if (menItem.fanartProperty.ToLower().Contains("movingpicture"))
            randomFanart.fanartMovingPictures = true;

          if (menItem.fanartProperty.ToLower().Contains("movie"))
          {
            if (fanartHandlerRelease2)
            {
              if (menItem.fhBGSource == fanartSource.Scraper)
              {
                randomFanart.fanartMovies = false;
                randomFanart.fanartMoviesScraperFanart = true;
              }
              else
              {
                randomFanart.fanartMovies = true;
                randomFanart.fanartMoviesScraperFanart = false;
              }
            }
            else
              randomFanart.fanartMovies = true;
          }

          if (menItem.fanartProperty.ToLower().Contains("scorecenter"))
            randomFanart.fanartScoreCenter = true;
        }
      }
    }
    //
    // Generate the Most recent importfile and add to basic home
    //
    void generateMostRecentFilesAndImports(string recentAction)
    {
      //
      // Generate the Infoservice Most Recent Import files
      //
      if (recentAction == "GenImports")
      {
        if (menudef.enableMostRecentTVSeries)
          // Params: Overlay Type, Recent added summary x,y, Recent watched summary x,y
          generateMostRecentOverlay(menuStyle, isOverlayType.TVSeries, 976, 50, 976, 370);

        if (menudef.enableMostRecentMovPics)
          // Params: Overlay Type, Recent added summary x,y, Recent watched summary x,y
          generateMostRecentOverlay(menuStyle, isOverlayType.MovPics, 976, 50, 976, 370);
        //
        // Only generate music and RecordedTV if the correct Fanart Handler version is installed and enabled
        //
        if (helper.pluginEnabled(Helper.Plugins.FanartHandler) && (fanarthandlerVersionRequired.CompareTo(fhOverlayVersion) <= 0))
        {
          // Params: Overlay Type, Recent added summary x,y, Recent watched summary x,y
          if (menudef.enableMostRecentMusic)
            generateMostRecentOverlay(menuStyle, isOverlayType.Music, 976, 50, 0, 0);

          // Params: Overlay Type, Recent added summary x,y, Recent watched summary x,y
          if (menudef.enableMostRecentRecordedTV)
            generateMostRecentOverlay(menuStyle, isOverlayType.RecordedTV, 976, 50, 0, 0);
        }

        if (menudef.enableDriveFreeSpace)
          generateMostRecentOverlay(menuStyle, isOverlayType.freeDriveSpace, 976, 50, 0, 0);

        if (menudef.enableSleepControlOverlay)
          generateMostRecentOverlay(menuStyle, isOverlayType.sleepControl, 976, 50, 0, 0);

        if (menudef.enableStocksOverlay)
          generateMostRecentOverlay(menuStyle, isOverlayType.stocks, 976, 50, 0, 0);

        if (menudef.enablePowerControlOverlay)
          generateMostRecentOverlay(menuStyle, isOverlayType.powerControl, 976, 50, 0, 0);

        if (menudef.enableHTPCInfoOverlay)
          generateMostRecentOverlay(menuStyle, isOverlayType.htpcInfo, 976, 50, 0, 0);

        if (menudef.enableUpdateControlOverlay)
          generateMostRecentOverlay(menuStyle, isOverlayType.updateControl, 976, 50, 0, 0);
      }
      //
      // Add the imports to basichome
      //
      if (recentAction == "AddImports")
      {
        if (menudef.enableMostRecentTVSeries && mostRecentVisibleControls(isOverlayType.TVSeries) != null)
          generateMostRecentInclude(isOverlayType.TVSeries);

        if (menudef.enableMostRecentMovPics && mostRecentVisibleControls(isOverlayType.MovPics) != null)
          generateMostRecentInclude(isOverlayType.MovPics);

        //
        // Only add imports to basichome if the correct Fanart Handler version is installed and enabled
        //
        if (helper.pluginEnabled(Helper.Plugins.FanartHandler) && (fanarthandlerVersionRequired.CompareTo(fhOverlayVersion) <= 0))
        {
          if (menudef.enableMostRecentMusic && mostRecentVisibleControls(isOverlayType.Music) != null)
            generateMostRecentInclude(isOverlayType.Music);

          if (menudef.enableMostRecentRecordedTV && mostRecentVisibleControls(isOverlayType.RecordedTV) != null)
            generateMostRecentInclude(isOverlayType.RecordedTV);
        }

        if (menudef.enableDriveFreeSpace)
          generateMostRecentInclude(isOverlayType.freeDriveSpace);

        if (menudef.enableSleepControlOverlay)
          generateMostRecentInclude(isOverlayType.sleepControl);

        if (menudef.enableStocksOverlay)
          generateMostRecentInclude(isOverlayType.stocks);

        if (menudef.enablePowerControlOverlay)
          generateMostRecentInclude(isOverlayType.powerControl);

        if (menudef.enableHTPCInfoOverlay)
          generateMostRecentInclude(isOverlayType.htpcInfo);

        if (menudef.enableUpdateControlOverlay)
          generateMostRecentInclude(isOverlayType.updateControl);
      }
    }
    //
    // Check and set default menu item for Recent Music or RecordedTV
    //
    void assoicateDefaultItems()
    {
      bool norecent = true;

      // Check if most recent music is enabled, does it have an assiocated menu item, if not default to music
      if (mostRecentVisibleControls(isOverlayType.Music) == "No" && menudef.enableMostRecentMusic)
      {
        foreach (menuItem menItem in menuItems)
        {
          if (menItem.showMostRecent == displayMostRecent.music)
            norecent = false;
        }
        if (norecent)
        {
          for (int i = 0; i < menuItems.Count; i++)
          {
            if (menuItems[i].hyperlink.ToString() == musicSkinID)
              menuItems[i].showMostRecent = displayMostRecent.music;
          }
        }
      }
      // Check if most recent recorded TV is enabled, is there an assiocated menu item, if not default to RecordedTV
      norecent = true;
      if (mostRecentVisibleControls(isOverlayType.RecordedTV) == "No" && menudef.enableMostRecentRecordedTV)
      {
        foreach (menuItem menItem in menuItems)
        {
          if (menItem.showMostRecent == displayMostRecent.recordedTV)
            norecent = false;
        }
        if (norecent)
        {
          for (int i = 0; i < menuItems.Count; i++)
          {
            if (menuItems[i].hyperlink.ToString() == tvMenuSkinID)
              menuItems[i].showMostRecent = displayMostRecent.recordedTV;
          }
        }
      }
    }

    //
    // Write out a formatted xml file
    //
    public void writeXMLFile(string xmlFileName)
    {
      // Delete any existing file
      if (System.IO.File.Exists(SkinInfo.mpPaths.streamedMPpath + xmlFileName))
        System.IO.File.Delete(SkinInfo.mpPaths.streamedMPpath + xmlFileName);

      //Write tempory file
      StreamWriter tmpwriter;
      tmpwriter = System.IO.File.CreateText(Path.Combine(SkinInfo.mpPaths.streamedMPpath, xmlFileName));
      tmpwriter.Write(xml);
      tmpwriter.Close();

      checkSum.Add(Path.Combine(SkinInfo.mpPaths.streamedMPpath, xmlFileName));
    }

    private string weatherIcon(int theDay)
    {
      string day;
      if (theDay == 0)
        day = "today";
      else
        if (getInfoServiceVersion().CompareTo(isWeatherVersion) >= 0)
          day = "forecast" + (theDay + 1).ToString() + ".day";
        else
          day = "day" + (theDay + 1).ToString() + ".day";
      if (menudef.animatedWeatherIcons)
      {
        // relative from Animations folder
        return "weathericons\\animated\\128x128\\#infoservice.weather." + day + ".img.big.filenamewithoutext";
      }
      else
      {
        // relative from Media folder
        return "animations\\weathericons\\static\\128x128\\#infoservice.weather." + day + ".img.big.filenamewithoutext.png";
      }
    }


    private void setBasicHomeValues()
    {

      basicHomeValues.offsetMymenu = -39;
      basicHomeValues.textYOffset = 6;
      basicHomeValues.offsetSubmenu = 76;
      basicHomeValues.offsetRssImage = 73;
      basicHomeValues.offsetRssText = 75;
      basicHomeValues.offsetTwitter = 55;
      basicHomeValues.offsetTwitterImage = 28;
      basicHomeValues.offsetButtons = 42;
      basicHomeValues.menuHeight = 165;
      basicHomeValues.subMenuHeight = 60;
      basicHomeValues.subMenuXpos = 0;
      basicHomeValues.subMenuWidth = 1280;
      basicHomeValues.subMenuTopHeight = 60;
      basicHomeValues.Button3Slide = 70;


      basicHomeValues.mymenu = "vmenu_main.png";
      basicHomeValues.mymenu_submenu = "vmenu_submenu.png";
      basicHomeValues.mymenu_submenutop = "vmenu_submenutop.png";


      // Now adjust depending on Menu Style chosen
      switch (menuStyle)
      {
        case chosenMenuStyle.verticalStyle:
          basicHomeValues.mymenu_submenutop = "hometwitter.png";
          basicHomeValues.offsetTwitter = 0;
          break;
        case chosenMenuStyle.horizontalStandardStyle:
          if (menudef.horizontalContextLabels)
          {
            basicHomeValues.menuHeight += 34;
            basicHomeValues.offsetMymenu -= 25;
            basicHomeValues.offsetButtons += 16;
            basicHomeValues.offsetTwitter += 15;
            basicHomeValues.offsetTwitterImage += 15;
          }
          break;
        case chosenMenuStyle.horizontalContextStyle:
          if (menudef.horizontalContextLabels)
          {
            basicHomeValues.menuHeight += 33;
            basicHomeValues.offsetMymenu -= 24;
            basicHomeValues.offsetButtons += 16;
            basicHomeValues.offsetTwitter += 15;
            basicHomeValues.offsetTwitterImage += 15;
          }
          break;
        case chosenMenuStyle.graphicMenuStyle:
          basicHomeValues.menuHeight = 198;
          basicHomeValues.offsetMymenu = 27;
          basicHomeValues.subMenuXpos = -70;
          basicHomeValues.subMenuWidth = 1420;
          basicHomeValues.subMenuHeight = 70;
          basicHomeValues.offsetSubmenu = 257;
          basicHomeValues.offsetRssText = 259;


          basicHomeValues.textYOffset = 30;

          basicHomeValues.offsetRssImage = 73;

          basicHomeValues.offsetTwitter = 55;
          basicHomeValues.offsetTwitterImage = 28;
          basicHomeValues.offsetButtons = 42;



          basicHomeValues.subMenuTopHeight = 60;
          basicHomeValues.Button3Slide = 70;
          weatherStyle = chosenWeatherStyle.middle;
          break;
      }
    }



    private Version getInfoServiceVersion()
    {
      Version fv = new Version(helper.fileVersion(SkinInfo.mpPaths.pluginPath + "\\windows\\infoservice.dll"));
      return fv;
    }


    private string MovingPicturesVersion
    {
      get
      {
        return helper.fileVersion(SkinInfo.mpPaths.pluginPath + "\\windows\\MovingPictures.dll");
      }
    }

    private string TVSeriesVersion
    {
      get
      {
        return helper.fileVersion(SkinInfo.mpPaths.pluginPath + "\\windows\\MP-TVSeries.dll");
      }
    }

    private string MediaPortalVersion
    {
      get
      {
        return helper.fileVersion(Path.Combine(SkinInfo.mpPaths.sMPbaseDir, "MediaPortal.exe"));
      }
    }


    #endregion

    #region Classes and Data Structures

    public struct MenuDefinition
    {
      public bool verticalStyle;
      public bool horizontalStyle;
      public string selectedFont;
      public string labelFont;
      public string focusAlpha;
      public string focusColor;
      public string noFocusAlpha;
      public string noFocusColor;
      public string menuPos;
      public string acceleration;
      public string duration;
      public bool dropShadow;
      public bool infoServiceOptions;
      public bool rssEnabled;
      public bool twitterEnabled;
      public bool wrapString;
      public bool weatherBGLink;
      public bool enableFiveDayWeather;
      public bool summaryWeatherEnabled;
      public bool clearCache;
      public bool animatedWeatherIcons;
      public bool horizontalContextLabels;
      public bool fullWeatherSummaryBottom;
      public bool fullWeatherSummaryMiddle;
      public bool disableClock;
      public bool hideFanartScraper;
      public bool overlayFanart;
      public bool animateBackground;
      public bool enableMostRecentTVSeries;
      public bool enableMostRecentMovPics;
      public bool cycleMostRecentFanart;
      public bool mrSeriesEpisodeFormat;
      public bool mrTitleLast;
      public bool exitStyleNew;
      public string tvSeriesEpisodeFont;
      public string tvSeriesSeriesFont;
      public string movpicsTitleFont;
      public string movpicsDetailFont;
      public bool hideRuntime;
      public bool hideCertifcation;
      public bool hideRating;
      public bool useTextRating;
      public bool movpicsRecentWatched;
      public bool tvSeriesRecentWatched;
      public bool movpicsSummaryEnabled;
      public bool tvseriesSummaryEnabled;
      public bool tvSeriesDisableFadeLables;
      public bool movpicsDisableFadeLables;
      public bool movepicsCycleFanart;
      public bool tvseriesCycleFanart;
      public bool enableMostRecentMusic;
      public bool enableMostRecentRecordedTV;
      public bool enableSleepControlOverlay;
      public bool enableStocksOverlay;
      public bool enablePowerControlOverlay;
      public bool enableHTPCInfoOverlay;
      public bool enableUpdateControlOverlay;
      public bool enableDriveFreeSpace;
      public bool disableExitMenu;
      public string driveFreeSpaceList;
    }

    public class menuItem
    {
      public string hyperlink;
      public string hyperlinkParameter;
      public string hyperlinkParameterOption;
      public bool isDefault;
      public bool isWeather;
      public string bgFolder;
      public bool fanartHandlerEnabled;
      public bool EnableMusicNowPlayingFanart;
      public string fanartProperty;
      public fanartSource fhBGSource;
      public string name;
      public int id;
      public bool updateStatus;
      public string contextLabel;
      public string defaultImage;
      public bool disableBGSharing;
      public displayMostRecent showMostRecent;
      public int subMenuLevel1ID;
      public List<subMenuItem> subMenuLevel1 = new List<subMenuItem>();
      public List<subMenuItem> subMenuLevel2 = new List<subMenuItem>();
      public string xmlFileName;
      public string buttonTexture;
    }

    public class subMenuItem
    {
      public string displayName;
      public string baseDisplayName;
      public string xmlFileName;
      public string hyperlink;
      public string hyperlinkParameter;
      public string hyperlinkParameterOption;
      public displayMostRecent showMostRecent;
    }

    public class backgroundItem
    {
      public string name;
      public string folder;
      public string fanartPropery;
      public fanartSource fhBGSource;
      public bool fanartHandlerEnabled;
      public bool EnableMusicNowPlayingFanart;
      public string image;
      public List<string> ids = new List<string>();
      public List<string> mname = new List<string>();
      public bool isWeather;
      public bool disableBGSharing;
      public int subMenuID;
    }

    public struct editorPaths
    {
      public string sMPbaseDir;
      public string skinBasePath;
      public string cacheBasePath;
      public string configBasePath;
      public string streamedMPpath;
      public string pluginPath;
      //public string backgroundPath;
      public string fanartBasePath;
      public string thumbsPath;
    }

    public struct randomFanartSetting
    {
      public bool fanartGames;
      public bool fanartTVSeries;
      public bool fanartPlugins;
      public bool fanartMovingPictures;
      public bool fanartMusic;
      public bool fanartPictures;
      public bool fanartTv;
      public bool fanartMovies;
      public bool fanartScoreCenter;
      public bool fanartMoviesScraperFanart;
      public bool fanartMusicScraperFanart;
    }

    public struct editorValues
    {
      public bool basicHomeLoadError;
      public bool useInfoServiceSeparator;
      public int defaultId;
      public int textYOffset;
      public int weatherControl;
      public int tvseriesControl;
      public int movingPicturesControl;
      public int musicControl;
      public int tvControl;
      public int offsetMymenu;
      public int offsetSubmenu;
      public int offsetRssImage;
      public int offsetRssText;
      public int offsetTwitter;
      public int offsetTwitterImage;
      public int offsetButtons;
      public int menuHeight;
      public int subMenuHeight;
      public int subMenuXpos;
      public int subMenuWidth;
      public int subMenuTopHeight;
      public int Button3Slide;
      public string mymenu;
      public string mymenu_submenu;
      public string mymenu_submenutop;
    }


    #endregion
  }
}
