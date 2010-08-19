using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
      string lang;

      try
      {
        lang = GUILocalizeStrings.GetCultureName(GUILocalizeStrings.CurrentLanguage());
        _info = DateTimeFormatInfo.GetInstance(CultureInfo.CurrentUICulture);
      }
      catch (Exception)
      {
        lang = CultureInfo.CurrentUICulture.Name;
        _info = DateTimeFormatInfo.GetInstance(CultureInfo.CurrentUICulture);
      }

      smcLog.WriteLog("StreamedMPConfig: Using language " + lang,LogLevel.Info);

      _path = Config.GetSubFolder(Config.Dir.Language, "StreamedMP");

      if (!System.IO.Directory.Exists(_path))
        System.IO.Directory.CreateDirectory(_path);

      LoadTranslations(lang);
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
          FieldInfo[] fields = transType.GetFields(BindingFlags.Public | BindingFlags.Static);
          foreach (FieldInfo field in fields)
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
      FieldInfo[] fieldInfos = TransType.GetFields(BindingFlags.Public | BindingFlags.Static);
      foreach (FieldInfo fi in fieldInfos)
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
    public static string UpdateInstall = "Update and Install";
    public static string NoUpdatesAvailable = "No patches are currently available";
    public static string DownloadingPatch = "Downloading Patch: {0}";
    public static string DownloadingProgress = "Downloaded {0}KB out of {1}KB ({2}%)";
    public static string NumPatchesInstalled = "Number of patch files installed: {0}";
    public static string PatchUpdateComplete = "Update to Skin Version: {0} Complete";
    public static string UpdateAvailableHeading = "Update Available - Change Log";
    public static string Revision = "Revision:";
    public static string Author = "Author:";
    public static string Date = "Date:";
    public static string Message = "Message:";
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

    #endregion
  }
}

