using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

    #endregion

    #region Constructor

    public GenerateMenu()
    {

      
    }

    #endregion

    #region Public methods

    

    #endregion

    #region Private Methods

    #endregion

    #region Classes and Data Structures

    public struct MenuDefinition
    {
      public bool verticalStyle;
      public bool horizontalStyle;
      public string selectedFont;
      public string labelFont;
      public string focusAlpha;
      public string noFocusAlpha;
      public string menuPos;
      public string acceleration;
      public string duration;
      public bool dropShadow;
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
      public bool mostRecentTVSeries;
      public bool mostRecentMovPics;
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
      public bool tvSeriesDisableFadeLables;
      public bool movpicsDisableFadeLables;
      public bool enableMostRecentMusic;
      public bool enableMostRecentRecordedTV;
      public bool enableSleepControlOverlay;
      public bool enableStocksOverlay;
      public bool enablePowerControlOverlay;
      public bool enableHTPCInfoOverlay;
      public bool enableUpdateControlOverlay;
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
