using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Text.RegularExpressions;
using MediaPortal.Configuration;
using MediaPortal.GUI.Library;

namespace StreamedMPConfig
{
  class Translation
  {
    #region Private variables

    private static Dictionary<string, string> _translations;
    private static Dictionary<string, string> DynamicTranslations = new Dictionary<string, string>();
    private static readonly string _path = string.Empty;
    private static readonly DateTimeFormatInfo _info;
    private static readonly logger smcLog = logger.GetInstance();

    #endregion

    #region Constructor

    static Translation()
    {    
        _info = DateTimeFormatInfo.GetInstance(CultureInfo.CurrentUICulture);
        _path = Config.GetSubFolder(Config.Dir.Language, "StreamedMP");        
    }

    #endregion

    #region Public Properties

    public static Dictionary<string, string> FixedTranslations = new Dictionary<string, string>();

    /// <summary>
    /// Gets the translated strings collection in the active language
    /// </summary>
    public static Dictionary<string, string> Strings
    {
      get
      {
        if (_translations == null)
        {
          _translations = new Dictionary<string, string>();
          Type transType = typeof(Translation);
          var fields = transType.GetFields(BindingFlags.Public | BindingFlags.Static)
                      .Where(p=>p.FieldType == typeof(string));

          foreach (var field in fields)
          {
            if (DynamicTranslations.ContainsKey(field.Name))
            {
              if (field.GetValue(transType).ToString() != string.Empty)
                _translations.Add(field.Name + ":" + DynamicTranslations[field.Name], field.GetValue(transType).ToString());
            }
            else
            {
              if (field.GetValue(transType).ToString() != string.Empty)
                _translations.Add(field.Name, field.GetValue(transType).ToString());
            }
          }
        }
        return _translations;
      }
    }

    #endregion

    #region Public Methods

    public static void Init()
    {
      // reset active translations
      _translations = null;
      FixedTranslations.Clear();

      string lang = settings.PreviousLanguage = settings.CurrentLanguage;

      smcLog.WriteLog("StreamedMPConfig: Using language " + lang, LogLevel.Info);

      if (!System.IO.Directory.Exists(_path))
        System.IO.Directory.CreateDirectory(_path);

      LoadTranslations(lang);
    }

    public static int LoadTranslations(string lang)
    {
      XmlDocument doc = new XmlDocument();
      Dictionary<string, string> TranslatedStrings = new Dictionary<string, string>();
      string langPath = "";
      try
      {
        langPath = Path.Combine(_path, lang + ".xml");
        smcLog.WriteLog("StreamedMpConfig: Loading Language File : " + langPath,LogLevel.Debug);
        doc.Load(langPath);
      }
      catch (Exception e)
      {
        if (lang == "en")
          return 0; // otherwise we are in an endless loop!

        if (e.GetType() == typeof(FileNotFoundException))
          smcLog.WriteLog(string.Format("Cannot find translation file {0}.  Failing back to English", langPath),LogLevel.Warning);
        else
        {
          smcLog.WriteLog(string.Format("Error in translation xml file: {0}. Failing back to English", lang),LogLevel.Error);
          smcLog.WriteLog(e.ToString(),LogLevel.Error);
        }

        return LoadTranslations("en");
      }
      foreach (XmlNode stringEntry in doc.DocumentElement.ChildNodes)
      {
        if (stringEntry.NodeType == XmlNodeType.Element)
          try
          {
            if (stringEntry.Attributes.GetNamedItem("Field").Value.StartsWith("#"))
            {
              FixedTranslations.Add(stringEntry.Attributes.GetNamedItem("Field").Value, stringEntry.InnerText);
            }
            else
              TranslatedStrings.Add(stringEntry.Attributes.GetNamedItem("Field").Value, stringEntry.InnerText);
          }
          catch (Exception ex)
          {
            smcLog.WriteLog("Error in Translation Engine",LogLevel.Error);
            smcLog.WriteLog(ex.ToString(),LogLevel.Error);
          }
      }

      Type TransType = typeof(Translation);
      var fieldInfos = TransType.GetFields(BindingFlags.Public | BindingFlags.Static)
                       .Where(p=>p.FieldType == typeof(string));

      foreach (var fi in fieldInfos)
      {
        if (TranslatedStrings != null && TranslatedStrings.ContainsKey(fi.Name))
          TransType.InvokeMember(fi.Name, BindingFlags.SetField, null, TransType, new object[] { TranslatedStrings[fi.Name] });
        else
        {
          // There is no hard-coded translation so create one
          smcLog.WriteLog(string.Format("Translation not found for field: {0}.  Using hard-coded English default.", fi.Name),LogLevel.Info);
        }
      }
      return TranslatedStrings.Count;
    }

    public static string GetByName(string name)
    {
      if (!Strings.ContainsKey(name))
        return name;

      return Strings[name];
    }

    public static string GetByName(string name, params object[] args)
    {
      return String.Format(GetByName(name), args);
    }

    /// <summary>
    /// Takes an input string and replaces all ${named} variables with the proper translation if available
    /// </summary>
    /// <param name="input">a string containing ${named} variables that represent the translation keys</param>
    /// <returns>translated input string</returns>
    public static string ParseString(string input)
    {
      Regex replacements = new Regex(@"\$\{([^\}]+)\}");
      MatchCollection matches = replacements.Matches(input);
      foreach (Match match in matches)
      {
        input = input.Replace(match.Value, GetByName(match.Groups[1].Value));
      }
      return input;
    }

    #endregion

    #region Translations / Strings

    /// <summary>
    /// These will be loaded with the language files content
    /// if the selected lang file is not found, it will first try to load en(us).xml as a backup
    /// if that also fails it will use the hardcoded strings as a last resort.
    /// </summary>

    public static string CDCover = "Show CD Cover Only in Music Now Playing";
    public static string ShowEQ = "Show Graphic EQ in Music Now Playing";
    public static string MinVideoOSD = "Use minimal Video OSD when paused";
    public static string VideoWallpaperLabel = "Video Wallpaper";
    public static string MusicMenu = "Music Menu";
    public static string VideoMenu = "Video Menu";
    public static string SkinUpdate = "Skin Update";
    public static string MiscMenu = "Misc Settings";
    public static string UpdateInstall = "Update and Install";
    public static string NoUpdatesAvailable = "No patches are currently available";
    public static string DownloadingPatch = "Downloading Patch: {0}";
    public static string DownloadingProgress = "Downloaded {0}KB out of {1}KB ({2}%)";
    public static string NumPatchesInstalled = "Number of patch files installed: {0}";
    public static string PatchUpdateComplete = "Update to Skin Version: {0} Complete";
    public static string UpdateAvailableHeading = "Update Available - Change Log";    
    public static string mupdateheader = "Patch Installer Downloaded to Desktop";
    public static string mupdateline1 = "Manual Update Required - To apply close";
    public static string mupdateline2 = "MediaPortal and/or Configuration";
    public static string mupdateline3 = "and run:  {0}";
    public static string mupdateline4 = "which can be found on your Desktop";
    public static string SkinSettings = "Skin Settings";
    public static string TVSeriesMenu = "TVSeries Settings";
    public static string FanartStyle = "Fanart Style";
    public static string WideBanners = "WideBanners";
    public static string WideBannerDefault = "WideBanners: Default";
    public static string WideBanner5x2Grid = "WideBanners: 5x2 Grid with Poster";
    public static string WideBanner5x3Grid = "WideBanners: 5x3 Grid";
    public static string WideBanner7x3Grid = "WideBanners: 7x3 Grid";
    public static string WideBanner10x4Grid = "WideBanners: 10x4 Grid";
    public static string MovingPicturesMenu = "MovingPictures Settings";
    public static string Thumbnails = "Thumbnails";
    public static string ThumbnailDefault = "Thumbnails: Default";
    public static string Thumbnail_12x4_8x3_Grid = "Thumbnails: 12x4 Small Icons, 8x3 Large Icons";
    public static string Thumbnail_12x3_8x2_Grid = "Thumbnails: 12x3 Small Icons, 8x2 Large Icons + Info";
    public static string TVMenu = "TV Settings";
    public static string TVGuideSize = "TV Guide Size";
    public static string TVMiniGuideSize = "TV Mini Guide Size";
    public static string TVGuideEightRows = "TV Guide: 8 Rows";
    public static string TVGuideTenRows = "TV Guide: 10 Rows";
    public static string TVGuideTwelveRows = "TV Guide: 12 Rows";
    public static string TVGuideSixteenRows = "TV Guide: 16 Rows";
    public static string TVMiniGuideThreeRows = "TV Mini Guide: 3 Rows";
    public static string TVMiniGuideFiveRows = "TV Mini Guide: 5 Rows";
    public static string TVMiniGuideSevenRows = "TV Mini Guide: 7 Rows";
    public static string TVMiniGuideNineRows = "TV Mini Guide: 9 Rows";
    public static string ShowHiddenMenuImage = "Show Hidden Menu Image";    
    public static string ShowRoundedPosters = "Show Rounded Poster Images";
    public static string ShowIconsInArtwork = "Show Watched/UnWatched Icons In Artwork";
    public static string LatestEpisodes = "Latest Episodes";
    public static string LatestMovies = "Latest Movies";
    public static string NowPlayingStyles = "Now Playing Screen Styles";
    public static string Rated = "Rated";
    public static string Director = "Director";
    public static string ReleaseDate = "Release Date";
    public static string Runtime = "Runtime";
    public static string SearchMusic = "Search Music";
    public static string SearchPhrase = "Search Phrase";
    public static string SearchHistory = "Search History";
    public static string SearchFields = "Search Fields";
    public static string SearchType = "Search Type";
    public static string CaseSensitive = "Case Sensitive";
    public static string PlayMostRecents = "Enable Play Action for Most Recents";
    public static string RecentlyWatched = "Recently Watched";
    public static string MediaPortalExit = "Exit MediaPortal";
    public static string MediaPortalRestart = "Restart MediaPortal";
    public static string MediaPortalShutDownMenu = "Shutdown Menu";
    public static string MediaPortalRestartMessage = "Are you sure you want to restart MediaPortal?";
    public static string LatestMusic = "Latest Music";
    public static string LatestTVRecordings = "Latest TV Recordings";
    public static string FilterWatchedRecents = "Filter Watched in Recently Added";
    public static string RecordedOn = "Recorded On";
    public static string Votes = "votes";
    public static string SkinInfoText = "Skin Information";
    public static string EnableRandomTVSeriesFanartInMyTV = "Enable Random TVSeries Fanart";
    public static string ConfigRequiresRestartLine1 = "The following configuration changes will take";
    public static string ConfigRequiresRestartLine2 = "affect on next MediaPortal restart.";
    public static string ConfigRequiresRestartLine3 = "Would you like to restart now?";
    public static string Trailer = "Trailer";
    public static string UnfocusedAlpha = "Transparency/Opacity Settings";    
    public static string UnfocusedAlphaMenuLists = "Unfocused Alpha on List Layouts: {0}";
    public static string UnfocusedAlphaMenuThumbs = "Unfocused Alpha on Thumb Layouts: {0}";
    public static string UnfocusedAlphaInvalidLine1 = "Invalid value entered, please enter a value";
    public static string UnfocusedAlphaInvalidLine2 = "between 0 - 255.";
    public static string UnfocusedAlphaInvalidLine3 = "Opaque = 255, Transparent = 0";
    public static string UseLargeFonts = "Use Large Fonts";
    public static string RestoreDefaults = "Restore Defaults";
    public static string ListColors = "Set List Colors";
    public static string ListDefaultColor = "Default Color: {0}";
    public static string ListText2Color = "Text 2 Color: {0}";
    public static string ListText3Color = "Text 3 Color: {0}";
    public static string ListWatchedColor = "Watched Color: {0}";
    public static string ListRemoteColor = "Remote Color: {0}";
    public static string ListColorInvalidLine1 = "Invalid color code entered, please enter a";
    public static string ListColorInvalidLine2 = "valid HEX code e.g. the color red is FF0000.";
    public static string ListColorInvalidLine3 = "which is '255' red, '0' green, and '0' blue.";
    public static string CustomColor = "Custom Color";
    public static string SkinMenu = "Skin";
    public static string MediaPortalSettings = "MediaPortal Settings";
    public static string ShowListScrollingPopup = "Show List Scrolling Popup";
    public static string Scraper = "Scraper";
    public static string TextualMediaInfoLogos = "Textual MediaInfo Logos";
    #endregion
  }
}

