﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Drawing;
using MediaPortal.Configuration;
using MediaPortal.GUI.Library;
using MediaPortal.Utils;
using System.Text.RegularExpressions;
using SMPCheckSum;
using WindowPlugins.GUITVSeries;
using SQLite.NET;
using MediaPortal.GUI.Music;
using MediaPortal.GUI.View;
using OnlineVideos;

namespace StreamedMPEditor
{
  [PluginIcons("StreamedMPEditor.Resources.SMPEditor.png", "StreamedMPEditor.Resources.SMPEditorDisabled.png")]
  public partial class formStreamedMpEditor : Form, ISetupForm
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

    Label fhChoice = new Label();
    RadioButton fhRBUserDef = new RadioButton();
    RadioButton fhRBScraper = new RadioButton();

    public static List<prettyItem> prettyItems = new List<prettyItem>();
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
    public string fhUserDef = string.Empty;
    public string fhSuffix = ".any";
    public static string driveFreeSpaceList = string.Empty;

    int textXOffset = -25;
    int maxXPosition = 520;
    int menuOffset = 0;
    int deskHeight;
    int deskWidth;

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

    public static Regex isIleagalXML = new Regex("[&<>]");

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

    CheckSum checkSum = new CheckSum();
    SkinInfo skInfo = new SkinInfo();
    Helper helper = new Helper();
    Form userConfirm = new Form();
    Button btOk = new Button();
    TextBox tbInfo = new TextBox();
    CheckBox cbShowAgain = new CheckBox();
    Panel selectPanel = new Panel();
    Button nextBatch = new Button();
    Button prevBatch = new Button();
    Button imgCancel = new Button();
    ProgressBar pBar = new ProgressBar();
    Label pLabel = new Label();
    Form downloadForm = new Form();
    Button downloadStop = new Button();
    TVSereisFormatOptions tvSeriesOptions = new TVSereisFormatOptions();
    MovPicsSummaryOptions movPicsOptions = new MovPicsSummaryOptions();
    editorValues basicHomeValues = new editorValues();
    defaultImages defImgs = new defaultImages();
    randomFanartSetting randomFanart = new randomFanartSetting();
    mostRecentDisplaySelection mrDisplaySelection = new mostRecentDisplaySelection();
    getButtonTexture buttonTexture = new getButtonTexture();

    public static List<KeyValuePair<string, string>> tvseriesViews = new List<KeyValuePair<string, string>>();
    public static List<KeyValuePair<string, string>> musicViews = new List<KeyValuePair<string, string>>();
    public static List<KeyValuePair<string, string>> onlineVideosViews = new List<KeyValuePair<string, string>>();

    #endregion

    #region Public methods

    public formStreamedMpEditor()
    {
      InitializeComponent();
      randomFanart.fanartGames = false;
      randomFanart.fanartMovies = false;
      randomFanart.fanartMovingPictures = false;
      randomFanart.fanartMusic = false;
      randomFanart.fanartPictures = false;
      randomFanart.fanartPlugins = false;
      randomFanart.fanartTv = false;
      randomFanart.fanartTVSeries = false;
      randomFanart.fanartScoreCenter = false;
      randomFanart.fanartMoviesScraperFanart = false;
      randomFanart.fanartMusicScraperFanart = false;

      //Check the display res
      deskHeight = Screen.PrimaryScreen.Bounds.Height;
      deskWidth = Screen.PrimaryScreen.Bounds.Width;

      if (deskWidth == 1920 && deskHeight == 1080)
      {
        detectedres = screenResolutionType.res1920x1080;
        setHDScreenRes();
      }
      else
      {
        detectedres = screenResolutionType.res1280x720;
        setSDScreenRes();
      }

      Version mpVersion = new Version(MediaPortalVersion);
      if (mpVersion.CompareTo(mpAlphaRelease) >= 0)
        isAlpha = true;

      if (mpVersion.CompareTo(mpBetaRelease) >= 0 || File.Exists("C:\\usebeta.txt"))
        isBeta = true;

      rbFanartStyle.Checked = true;

      buildDownloadForm();
      inialiseImgControls();

      releaseVersion.Text = String.Format("Version: {0}", Assembly.GetExecutingAssembly().GetName().Version.ToString());
      DateTime buildDate = getLinkerTimeStamp(System.Reflection.Assembly.GetEntryAssembly().Location.ToString());
      compileTime.Text += " " + buildDate.ToString() + " GMT";

      lastUsedTab.Checked = Properties.Settings.Default.rememberLastUsedTab;
      if (Properties.Settings.Default.rememberLastUsedTab)
      {
        StreamedMPMenu.SelectedIndex = Properties.Settings.Default.lastUsedTab;
      }
      backupVersionsToKeep.Text = Properties.Settings.Default.keepVersions.ToString();
      if (Properties.Settings.Default.autoPurge)
      {
        autoPurgeBackups.Checked = true;
      }

      if (isBeta)
      {
        musicViews = GetMusicViews();
        cboParameterViews.Visible = false;
        lbParameterView.Visible = false;
        cboParameterViews.Items.Clear();
        theMusicViews.Clear();
        foreach (KeyValuePair<string, string> mvv in musicViews)
        {
          theMusicViews.Add(mvv.Value);
        }
        // For 1.2 Weather Icons are now part of skin
        useSkinWeatherIcons.Visible = false;
      }

      string filename = Path.Combine(Path.Combine(SkinInfo.mpPaths.pluginPath, "windows"), "MP-TVSeries.dll");
      if (Helper.IsAssemblyAvailable("MP-TVSeries", new Version(2, 6, 5, 1265), filename))
      {
        tvseriesViews = GetTVSeriesViews();
        cboParameterViews.Visible = false;
        lbParameterView.Visible = false;
        cboParameterViews.Items.Clear();
        theTVSeriesViews.Clear();
        foreach (KeyValuePair<string, string> tvv in tvseriesViews)
        {
          theTVSeriesViews.Add(tvv.Value);
        }
      }

      filename = Path.Combine(Path.Combine(SkinInfo.mpPaths.pluginPath, "windows\\OnlineVideos"), "OnlineVideos.dll");
      if (Helper.IsAssemblyAvailable("OnlineVideos", new Version(0, 27, 0, 0), filename))
      {
        onlineVideosViews = GetOnlineVideosViews();
        cboParameterViews.Visible = false;
        lbParameterView.Visible = false;
        cboParameterViews.Items.Clear();
        theOnlineVideosViews.Clear();
        foreach (KeyValuePair<string, string> ovv in onlineVideosViews)
        {
          theOnlineVideosViews.Add(ovv.Value);    
        }
      }

      Version fhVersion = new Version(helper.fileVersion(Path.Combine(Path.Combine(SkinInfo.mpPaths.pluginPath, "process"), "FanartHandler.dll")));

      if (fhVersion.CompareTo(fhRelease2) >= 0)
      {
        fhUserDef = ".userdef";
        fanartHandlerRelease2 = true;
      }

      buildFHchoiceControls();
      checkAndEnableOverlays();
    }

    //
    // Key/value read methods TVSeries
    //
    // TVSeries Key
    //
    public string getTVSeriesViewKey(string value)
    {
      foreach (KeyValuePair<string, string> tvv in tvseriesViews)
      {
        if (tvv.Value.ToLower() == value.ToLower())
          return tvv.Key;
      }
      return "false";
    }
    // TVSeries Value
    //
    public string getTVSeriesViewValue(string key)
    {
      foreach (KeyValuePair<string, string> tvv in tvseriesViews)
      {
        if (tvv.Key.ToLower() == key.ToLower())
          return tvv.Value;
      }
      return "false";
    }
    //
    // Key/value read methods Music
    //
    // Music key
    //
    public string getMusicViewKey(string value)
    {
      foreach (KeyValuePair<string, string> mvv in musicViews)
      {
        if (mvv.Value.ToLower() == value.ToLower())
          return mvv.Key;
      }
      return "false";
    }
    //Music Value
    //
    public string getMusicViewValue(string key)
    {
      foreach (KeyValuePair<string, string> mvv in musicViews)
      {
        if (mvv.Key.ToLower() == key.ToLower())
          return mvv.Value;
      }
      return "false";
    }
    //
    // Key/value read methods OnlineVideos
    //
    //OnlineVideos Key
    //
    public string getOnlineVideosViewKey(string value)
    {
      foreach (KeyValuePair<string, string> mvv in onlineVideosViews)
      {
        if (mvv.Value.ToLower() == value.ToLower())
          return mvv.Key;
      }
      return "false";
    }
    //OnlineVideos Value
    //
    public string getOnlineVideosViewValue(string key)
    {
      foreach (KeyValuePair<string, string> mvv in onlineVideosViews)
      {
        if (mvv.Key.ToLower() == key.ToLower())
          return mvv.Value;
      }
      return "false";
    }
    //
    // Does the plugin support paramerters
    //
    public static bool pluginTakesParameter(string hyperLink)
    {
      Helper helper = new Helper();
      if (!isAlpha)
        return false;

      Dictionary<string, bool> parametersValid = new Dictionary<string, bool>();

      // List of plugin skinIDs that take parameters - all a bit manual and should add a file to store these at some point
      parametersValid.Add(tvseriesSkinID, true);
      parametersValid.Add(onlineVideosSkinID, true);

      if (isBeta)
      {
        parametersValid.Add(musicSkinID, true);
      }

      if (parametersValid.ContainsKey(hyperLink))
        return parametersValid[hyperLink];
      else
        return false;
    }

    #endregion

    #region Base overrides

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);

      if (helper.readMPConfiguration("skin", "name") != "StreamedMP")
      {
        DialogResult result = helper.showError("MediaPortal is not configured to use the StreamedMP skin\n\nThe Menu Editor requires the selected Skin to be StreamedMP.\n\nDo you want set StreamedMP as the selected Skin", errorCode.infoQuestion);

        if (result == DialogResult.No)
        {
          this.Hide();
          exitCondition = true; ;
        }
        else
        {
          helper.writeMPConfiguration("skin", "name", "StreamedMP");
          MediaPortal.Profile.Settings.SaveCache();
        }
      }
      if (!exitCondition)
      {
        Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("StreamedMPEditor.rtfFiles.introduction.rtf");
        menuDescription.LoadFile(stream, RichTextBoxStreamType.RichText);
        GetMediaPortalSkinPath();
        readFonts();
        getBackupFileTotals();
        if (helper.readMPConfiguration("general", "startbasichome") == "no")
        {
          DialogResult result = helper.showError("MediaPortal is not configured to start with the BasicHome menu\n\nThe Menu Generated by this Editor requires this option to be enabled.\n\nDo you want the Basichome Menu enabled?", errorCode.infoQuestion);

          if (result == DialogResult.No)
          {
            this.Close();
          }
          else
            helper.writeMPConfiguration("general", "startbasichome", "yes");
        }

        if (!System.IO.File.Exists(SkinInfo.mpPaths.sMPbaseDir + "\\Weather\\128x128.zip"))
          useSkinWeatherIcons.Text = "Replace Standard Weather Icons with Skin Supplied Versions";
        else
          useSkinWeatherIcons.Text = "Restore Standard Weather Icons";

        if (loadIDs() == true)
        {
          bgBox.Enabled = true;
          tbItemName.Enabled = true;
          addButton.Enabled = true;
          editButton.Enabled = false;
          cancelButton.Enabled = false;
          saveButton.Enabled = false;
          removeButton.Enabled = true;
          itemsOnMenubar.Enabled = true;
          disableBGSharing.Enabled = true;

          rbRssInfoServiceImage.Checked = false;
          rbRssNoImage.Checked = false;
          rbRssSkinImage.Checked = true;

          menuitemName.Text = null;
          menuItemLabel.Text = null;
          menuitemBGFolder.Text = null;
          menuitemTimeonPage.Text = null;
          menuitemWindow.Text = null;

          xmlFiles.SelectedItem = null;
          cboContextLabel.Text = null;
          tbItemName.Text = null;
          bgBox.Text = null;
          isWeather.Checked = false;
          selectedWindow.Text = null;
          selectedWindowID.Text = null;


          selectedWindow.Text = null;
          selectedWindowID.Text = null;

          rbTBSeriesFull.Checked = true;
          rbMovPicsFull.Checked = true;

          pbPosterPicTVSeries.Visible = true;
          pbFanartPicTVSeries.Visible = true;
          pbPosterPicMovPics.Visible = false;
          pbFanartPicMovPics.Visible = false;
          if (fanarthandlerVersionRequired.CompareTo(fhOverlayVersion) > 0)
          {
            cbEnableRecentMusic.Enabled = false;
            cbEnableRecentRecordedTV.Enabled = false;
            mrDisplaySelection.disableMusicRB = false;
            mrDisplaySelection.disableRecordedTVRB = false;
          }
        }

        loadMenuSettings();
        checkSplashScreens();
        toolStripStatusLabel2.Visible = false;
        itemsOnMenubar.SelectedIndex = 0;
        screenReset();
        setScreenProperties(itemsOnMenubar.SelectedIndex);
        disableItemControls();
        cancelCreateButton.Visible = false;
        btGenerateMenu.Enabled = true;
        editButton.Enabled = true;
        btMenuIcon.Visible = false;

        Version mpVersion = new Version(MediaPortalVersion);
        if (mpVersion.CompareTo(mpReleaseVersion) > 0)
        {
          wrapString.Enabled = true;
        }
        else
        {
          wrapString.Enabled = false;
        }

        if (menuStyle == chosenMenuStyle.graphicMenuStyle)
        {
          btMenuIcon.Visible = true;
          pbMenuIconInfo.Visible = true;
        }
      }
      else
        this.Close();
    }



    #endregion

    #region Private methods

    bool loadIDs()
    {
      xmlFiles.Enabled = true;
      helper.getSkinFileList(ref xmlFiles, ref ids);
      fhOverlayVersion = new Version(helper.fileVersion(Path.Combine(Path.Combine(SkinInfo.mpPaths.pluginPath, "Process"), "Fanarthandler.dll")));
      //return true;
      if (xmlFiles.Items.Count > 0)
      {
        helper.loadPrettyItems(ref cboQuickSelect, ids);
        disableItemControls();
        cancelCreateButton.Visible = false;
        btGenerateMenu.Enabled = true;
        return true;
      }
      else
      {
        helper.showError("Error reading Skin diectory - no files found", errorCode.major);
        return false;
      }
    }


    displayMostRecent getMostRecentDisplayOption()
    {
      if (mrDisplaySelection.mrToDisplay == displayMostRecent.tvSeries)
        return displayMostRecent.tvSeries;

      if (mrDisplaySelection.mrToDisplay == displayMostRecent.movies)
        return displayMostRecent.movies;

      return displayMostRecent.off;
    }

    void setMostRecentDisplayOption(displayMostRecent dmr)
    {
      switch (dmr)
      {
        case displayMostRecent.off:
          {
            mrDisplaySelection.mrToDisplay = displayMostRecent.off;
            break;
          }
        case displayMostRecent.tvSeries:
          {
            mrDisplaySelection.mrToDisplay = displayMostRecent.tvSeries;
            break;
          }
        case displayMostRecent.movies:
          {
            mrDisplaySelection.mrToDisplay = displayMostRecent.movies;
            break;
          }
      }
    }


    void addButton_Click(object sender, EventArgs e)
    {
      //selectedWindowID.Text = 
      int index = ids.IndexOf(selectedWindowID.Text);

      try
      {
        while (xmlFiles.Items[index].ToString().ToLower() != selectedWindow.Text.ToLower())
        {
          index = ids.IndexOf(selectedWindowID.Text, index + 1);
        }
      }
      catch (Exception exp)
      {
        helper.showError("QuickSelect Skin XML Missing or Invalid" + exp.Message, errorCode.info);
        return;
      }

      if (xmlFiles.SelectedItem != null && (bgBox.Text != "" || cboFanartProperty.Text != "") && tbItemName.Text != "")
      {
        xmlFiles.SelectedIndex = index;
        toolStripStatusLabel1.Text = xmlFiles.SelectedItem.ToString() + " added to menu";
        menuItem item = new menuItem();
        item.xmlFileName = xmlFiles.SelectedItem.ToString();
        item.name = tbItemName.Text;
        item.contextLabel = cboContextLabel.Text;
        item.hyperlink = ids[xmlFiles.SelectedIndex];
        item.bgFolder = bgBox.Text;
        item.fanartProperty = cboFanartProperty.Text;

        if (fhRBScraper.Checked)
          item.fhBGSource = fanartSource.Scraper;
        else
          item.fhBGSource = fanartSource.UserDef;

        item.fanartHandlerEnabled = cbItemFanartHandlerEnable.Checked;
        item.EnableMusicNowPlayingFanart = cbEnableMusicNowPlayingFanart.Checked;
        item.isWeather = isWeather.Checked;
        item.disableBGSharing = disableBGSharing.Checked;
        item.showMostRecent = getMostRecentDisplayOption();
        if (pluginTakesParameter(item.hyperlink) && cboParameterViews.SelectedIndex != -1)
        {
          switch (item.hyperlink)
          {
            case tvseriesSkinID:
              item.hyperlinkParameter = getTVSeriesViewKey(cboParameterViews.SelectedItem.ToString()); 
              break;
            case musicSkinID:
              item.hyperlinkParameter = getMusicViewKey(cboParameterViews.SelectedItem.ToString()); 
              break;
            case onlineVideosSkinID:
              item.hyperlinkParameter = getOnlineVideosViewKey(cboParameterViews.SelectedItem.ToString());
              break;
            default:
              break;
          }
        }
        else
          item.hyperlinkParameter = "false";

        if (item.fanartHandlerEnabled)
          checkAndSetRandomFanart(item.fanartProperty);
        else
          checkAndSetDefultImage(item);

        if (string.IsNullOrEmpty(buttonTexture.SelectedIcon))
          item.buttonTexture = setDefaultIcons(int.Parse(item.hyperlink));
        else
          item.buttonTexture = buttonTexture.SelectedIcon;
        
        //buttonTexture.MenuItem = item.name;
        setDefaultIcons(int.Parse(item.hyperlink));
        menuItems.Add(item);
        itemsOnMenubar.Items.Add(item.name);
        reloadBackgroundItems();
        // clear up
        tbItemName.Text = string.Empty;
        bgBox.Text = string.Empty;
        cboContextLabel.Text = string.Empty;

        if (itemsOnMenubar.Items.Count > 2)
          btGenerateMenu.Enabled = true;
        xmlFiles.SelectedIndex = -1;
        changeOutstanding = true;
      }
      else
      {
        helper.showError("All fields must be complete before a Menu Item can be added", errorCode.info);
      }

    }

    void editButton_Click(object sender, EventArgs e)
    {
      if (itemsOnMenubar.SelectedIndex == -1)
      {
        helper.showError("No menu item selected\n\nPlease select item above to edit", errorCode.info);
        return;
      }

      int index = itemsOnMenubar.SelectedIndex;
      menuItem mnuItem = menuItems[index];

      btGenerateMenu.Enabled = false;
      xmlFiles.SelectedIndex = ids.IndexOf(mnuItem.hyperlink);
      tbItemName.Text = mnuItem.name;
      cboContextLabel.Text = mnuItem.contextLabel;
      bgBox.Text = mnuItem.bgFolder;
      cboFanartProperty.Text = mnuItem.fanartProperty;
      buttonTexture.SelectedIcon = mnuItem.buttonTexture;
      buttonTexture.MenuItem = mnuItem.name;

      if (mnuItem.fhBGSource == fanartSource.Scraper)
      {
        fhRBScraper.Checked = true;
        fhRBUserDef.Checked = false;
      }
      else
      {
        fhRBScraper.Checked = false;
        fhRBUserDef.Checked = true;
      }

      switch (mnuItem.hyperlink)
      {
        case tvseriesSkinID:
          cboParameterViews.DataSource = theTVSeriesViews;
          if (mnuItem.hyperlinkParameter != "false")
            cboParameterViews.Text = getTVSeriesViewValue(mnuItem.hyperlinkParameter);
          else
            cboParameterViews.Text = string.Empty;
          break;
        case musicSkinID:
          cboParameterViews.DataSource = theMusicViews;
          if (mnuItem.hyperlinkParameter != "false")
            cboParameterViews.Text = getMusicViewValue(mnuItem.hyperlinkParameter);
          else
            cboParameterViews.Text = string.Empty;
          break;
        case onlineVideosSkinID:
          cboParameterViews.DataSource = theOnlineVideosViews;
          if (mnuItem.hyperlinkParameter != "false")
            cboParameterViews.Text = getOnlineVideosViewValue(mnuItem.hyperlinkParameter);
          else
            cboParameterViews.Text = string.Empty;
          break;
        default:
          break;
      }



      if (cboFanartProperty.Text.ToLower() == "false")
        cboFanartProperty.Text = "";

      cbItemFanartHandlerEnable.Checked = mnuItem.fanartHandlerEnabled;
      cbEnableMusicNowPlayingFanart.Checked = mnuItem.EnableMusicNowPlayingFanart;
      disableBGSharing.Checked = mnuItem.disableBGSharing;
      isWeather.Checked = mnuItem.isWeather;
      selectedWindow.Text = xmlFiles.Text;
      selectedWindowID.Text = mnuItem.hyperlink;
      setMostRecentDisplayOption(mnuItem.showMostRecent);
      if (mnuItem.fanartHandlerEnabled)
        checkAndSetRandomFanart(mnuItem.fanartProperty);

      saveButton.Enabled = true;
      cancelButton.Enabled = true;
      editButton.Enabled = false;
      enableItemControls();
      addButton.Enabled = false;
      cancelCreateButton.Visible = false;
    }

    void saveButton_Click(object sender, EventArgs e)
    {
      if (itemsOnMenubar.SelectedItem != null)
      {
        int index = itemsOnMenubar.SelectedIndex;
        menuItem item = new menuItem();
        item = menuItems[index];
        item.name = tbItemName.Text;
        item.contextLabel = cboContextLabel.Text;
        item.bgFolder = bgBox.Text;
        item.fanartProperty = cboFanartProperty.Text;

        if (fhRBScraper.Checked)
          item.fhBGSource = fanartSource.Scraper;
        else
          item.fhBGSource = fanartSource.UserDef;

        item.fanartHandlerEnabled = cbItemFanartHandlerEnable.Checked;
        item.EnableMusicNowPlayingFanart = cbEnableMusicNowPlayingFanart.Checked;
        item.hyperlink = ids[xmlFiles.SelectedIndex];
        if (cboParameterViews.SelectedIndex != -1)
        {
          if (item.hyperlink == tvseriesSkinID)
            item.hyperlinkParameter = getTVSeriesViewKey(cboParameterViews.SelectedItem.ToString());

          if (item.hyperlink == musicSkinID)
            item.hyperlinkParameter = getMusicViewKey(cboParameterViews.SelectedItem.ToString());

          if (item.hyperlink == onlineVideosSkinID)
            item.hyperlinkParameter = getOnlineVideosViewKey(cboParameterViews.SelectedItem.ToString());
        }
        else
        {
          item.hyperlinkParameter = "false";
          cboParameterViews.Text = string.Empty;
        }
        item.disableBGSharing = disableBGSharing.Checked;
        item.isWeather = isWeather.Checked;
        item.showMostRecent = getMostRecentDisplayOption();

        if (item.isWeather && weatherBGlink.Checked && item.fanartHandlerEnabled)
        {
          DialogResult result = helper.showError("You have selected to use Fanart Random images for the Weather item\nbut the option 'Link Background to Current Weather' is enabled\nand will override this setting\n\nDisable the 'Link Background to Current Weather' Option? ", errorCode.infoQuestion);
          if (result == DialogResult.Yes)
          {
            weatherBGlink.Checked = false;
          }
        }

        if (item.fanartHandlerEnabled)
          checkAndSetRandomFanart(item.fanartProperty);
        else
        {
          checkAndSetDefultImage(item);
        }
        item.buttonTexture = buttonTexture.SelectedIcon;

        menuItems[index] = item;
        itemsOnMenubar.Items.RemoveAt(index);
        itemsOnMenubar.Items.Insert(index, item.name);
        reloadBackgroundItems();
        screenReset();
        disableItemControls();
        cancelCreateButton.Visible = false;
        btGenerateMenu.Enabled = true;
        changeOutstanding = true;
        cboParameterViews.Visible = false;
        if (menuStyle == chosenMenuStyle.graphicMenuStyle)
          displayMenuIcon(item.buttonTexture);
      }
    }

    void screenReset()
    {
      if (saveButton.Enabled)
      {
        tbItemName.Text = string.Empty;
        cboContextLabel.Text = string.Empty;
        isWeather.Checked = false;
        bgBox.SelectedIndex = -1;
        cboFanartProperty.SelectedIndex = -1;
        saveButton.Enabled = false;
        cancelButton.Enabled = false;
        editButton.Enabled = true;
      }
      selectedWindow.Text = string.Empty;
      selectedWindowID.Text = string.Empty;
      cboParameterViews.Text = string.Empty;
    }


    void itemsOnMenubar_SelectedIndexChanged(object sender, EventArgs e)
    {
      screenReset();
      setScreenProperties(itemsOnMenubar.SelectedIndex);
      editButton.Enabled = true;
      disableItemControls();
    }

    void cancelButton_Click(object sender, EventArgs e)
    {
      screenReset();
      setScreenProperties(itemsOnMenubar.SelectedIndex);
      disableItemControls();
      btGenerateMenu.Enabled = true;
    }

    void setScreenProperties(int index)
    {
      if (index < 0) return;

      menuItem mnuItem = menuItems[index];

      xmlFiles.SelectedItem = mnuItem.xmlFileName;
      cboContextLabel.Text = mnuItem.contextLabel;
      tbItemName.Text = mnuItem.name;
      cboFanartProperty.Text = mnuItem.fanartProperty;
      cbItemFanartHandlerEnable.Checked = mnuItem.fanartHandlerEnabled;
      cbEnableMusicNowPlayingFanart.Checked = mnuItem.EnableMusicNowPlayingFanart;
      disableBGSharing.Checked = mnuItem.disableBGSharing;
      if (pluginTakesParameter(mnuItem.hyperlink))
      {
        switch (mnuItem.hyperlink)
        {
          case tvseriesSkinID:
            cboParameterViews.DataSource = theTVSeriesViews;
            if (mnuItem.hyperlinkParameter != "false")
              cboParameterViews.Text = getTVSeriesViewKey(mnuItem.hyperlinkParameter);
            else
            {
              cboParameterViews.Text = string.Empty;
              cboParameterViews.SelectedIndex = -1;
            }
            break;
          case musicSkinID:
            cboParameterViews.DataSource = theMusicViews;
            if (mnuItem.hyperlinkParameter != "false")
              cboParameterViews.Text = getMusicViewValue(mnuItem.hyperlinkParameter);
            else
            {
              cboParameterViews.Text = string.Empty;
              cboParameterViews.SelectedIndex = -1;
            }
            break;
          case onlineVideosSkinID:
            cboParameterViews.DataSource = theOnlineVideosViews;
            if (mnuItem.hyperlinkParameter != "false")
              cboParameterViews.Text = getOnlineVideosViewValue(mnuItem.hyperlinkParameter);
            else
            {
              cboParameterViews.Text = string.Empty;
              cboParameterViews.SelectedIndex = -1;
            }
            break;
          default:
            break;
        }
        cboParameterViews.Visible = true;
        lbParameterView.Visible = true;

      }
      else
      {
        cboParameterViews.Visible = false;
        lbParameterView.Visible = false;
      }

      menuitemName.Text = mnuItem.name;
      menuItemLabel.Text = mnuItem.contextLabel;
      menuitemBGFolder.Text = mnuItem.bgFolder;
      bgBox.Text = mnuItem.bgFolder;
      menuitemWindow.Text = xmlFiles.Text;
      setMostRecentDisplayOption(mnuItem.showMostRecent);
      if (menuStyle == chosenMenuStyle.graphicMenuStyle)
        displayMenuIcon(mnuItem.buttonTexture);

      UpdateImageControlVisibility(mnuItem.fanartHandlerEnabled);
    }

    void displayMenuIcon(string icon)
    {
      string streamedMPMediaPath = Path.Combine(SkinInfo.mpPaths.streamedMPpath, "media");
      if (string.IsNullOrEmpty(icon) || !File.Exists(Path.Combine(streamedMPMediaPath, icon)))
        icon = "homeButtons\\_noimage.png";

      pbMenuIconInfo.Image = Image.FromFile(Path.Combine(streamedMPMediaPath, icon)).GetThumbnailImage(40, 40, null, new IntPtr());
    }

    void browseButton_Click(object sender, EventArgs e)
    {
      folderBrowserDialog.ShowNewFolderButton = false;
      folderBrowserDialog.Description = "Select the folder containing the images for this menu item";
      folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
      if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
      {
        bgBox.Text = folderBrowserDialog.SelectedPath;
      }
    }



    void xmlFiles_Click(object sender, EventArgs e)
    {
      selectedWindow.Text = xmlFiles.Text;
      selectedWindowID.Text = ids[xmlFiles.SelectedIndex];
      enableItemControls();
      editButton.Enabled = false;
      cancelCreateButton.Visible = true;
      btGenerateMenu.Enabled = false;
      if (pluginTakesParameter(selectedWindowID.Text))
      {
        cboParameterViews.Visible = true;
        lbParameterView.Visible = true;
      }
      else
      {
        cboParameterViews.Visible = false;
        lbParameterView.Visible = false;
      }
    }


    // Move selected menu item up one position in list. 
    void btMoveUp_Click(object sender, EventArgs e)
    {
      // Do nothing if no item is selected or if already at top
      if (itemsOnMenubar.SelectedItem != null && itemsOnMenubar.SelectedIndex > 0)
      {
        int index = itemsOnMenubar.SelectedIndex;


        Object listItem = itemsOnMenubar.SelectedItem;
        menuItem mnuItem = menuItems[index];

        itemsOnMenubar.Items.RemoveAt(index);
        menuItems.RemoveAt(index);

        itemsOnMenubar.Items.Insert(index - 1, listItem);
        menuItems.Insert(index - 1, mnuItem);

        itemsOnMenubar.SelectedIndex = index - 1;

      }
      foreach (menuItem item in menuItems)
      {
        Console.WriteLine(item.name);
      }
      Console.WriteLine("");

    }

    // Move selected menu item down one position in list. 
    void btMoveDown_Click(object sender, EventArgs e)
    {
      // Do nothing if no item is selected or if already at bottom
      if (itemsOnMenubar.SelectedItem != null && itemsOnMenubar.SelectedIndex < itemsOnMenubar.Items.Count - 1)
      {
        int index = itemsOnMenubar.SelectedIndex;


        Object listItem = itemsOnMenubar.SelectedItem;
        menuItem mnuItem = menuItems[index];

        itemsOnMenubar.Items.RemoveAt(index);
        menuItems.RemoveAt(index);
        if (index < itemsOnMenubar.Items.Count)
        {
          itemsOnMenubar.Items.Insert(index + 1, listItem);
          menuItems.Insert(index + 1, mnuItem);
        }
        else
        {
          itemsOnMenubar.Items.Add(listItem);
          menuItems.Add(mnuItem);
        }
        itemsOnMenubar.SelectedIndex = index + 1;

      }

      foreach (menuItem item in menuItems)
      {
        Console.WriteLine(item.name);
      }
      Console.WriteLine("");
    }

    bool isIlegalXML(string theValue)
    {
      Match m = isIleagalXML.Match(theValue);
      return m.Success;
    }

    void itemName_TextChanged(object sender, EventArgs e)
    {
      int start = tbItemName.SelectionStart;
      if (isIlegalXML(tbItemName.Text))
      {
        tbItemName.Text = tbItemName.Text.Substring(0, tbItemName.Text.Length - 1);
        tbItemName.SelectionStart = start;
        return;
      }
      tbItemName.Text = tbItemName.Text.ToUpper();
      tbItemName.SelectionStart = start;
    }


    void cboContextLabels_TextChanged(object sender, EventArgs e)
    {
      int start = cboContextLabel.SelectionStart;
      if (isIlegalXML(tbItemName.Text))
      {
        tbItemName.Text = tbItemName.Text.Substring(0, tbItemName.Text.Length - 1);
        tbItemName.SelectionStart = start;
        return;
      }
      cboContextLabel.Text = cboContextLabel.Text.ToUpper();
      cboContextLabel.SelectionStart = start;
    }


    void btGenerateMenu_Click(object sender, EventArgs e)
    {
      if (itemsOnMenubar.CheckedItems.Count > 1 || itemsOnMenubar.CheckedItems.Count == 0)
      {
        if (itemsOnMenubar.CheckedItems.Count == 0) helper.showError("\t      No Default Item\n\nYou must set one item as the default menu item.", errorCode.info);
        if (itemsOnMenubar.CheckedItems.Count > 1) helper.showError("          More than one default item set\n\nOnly one item can be set as the default menu.", errorCode.info);
      }
      else if (itemsOnMenubar.Items.Count < 3)
      {
        helper.showError("          Too few Menu Items - The minium is 3\n\nPlease add additional Menu items(s) before trying to Generate.", errorCode.info);
      }
      else
        genMenu(false);

      changeOutstanding = false;

    }

    void genMenu(bool onFormClosing)
    {
      validateMenuOffset();
      foreach (menuItem item in menuItems)
      {
        item.id = 1000 + menuItems.IndexOf(item);
        if (item.name == itemsOnMenubar.CheckedItems[0].ToString())
        {
          item.isDefault = true;
          basicHomeValues.defaultId = menuItems.IndexOf(item);
        }
        else
        {
          item.isDefault = false;
        }
        if (!infoserviceOptions.Enabled || !weatherBGlink.Checked)
          if (item.bgFolder == null && item.fanartProperty == null)
          {
            helper.showError("Menu Item " + item.name + " Has no Background Image folder\n\n\tPlease set a Folder", errorCode.info);
            return;
          }
      }
      setBasicHomeValues();
      if (menuStyle == chosenMenuStyle.verticalStyle)
      {
        writeMenu(menuType.vertical, onFormClosing);
      }
      else
      {
        writeMenu(menuType.horizontal, onFormClosing);
      }
      if (cboClearCache.Checked)
        clearCacheDir();
    }


    // This function call will regenerate the menu from the currenly saved usermenuprofile.xml
    // it assums that this file is correct.....
    public void reGenterateMenu()
    {
      this.WindowState = FormWindowState.Minimized;
      GetMediaPortalSkinPath();
      loadIDs();
      readFonts();
      getBackupFileTotals();
      loadMenuSettings();
      setBasicHomeValues();
      foreach (menuItem item in menuItems)
      {
        item.id = 1000 + menuItems.IndexOf(item);
        if (item.name == itemsOnMenubar.CheckedItems[0].ToString())
        {
          item.isDefault = true;
          basicHomeValues.defaultId = menuItems.IndexOf(item);
        }
        else
        {
          item.isDefault = false;
        }
        if (!infoserviceOptions.Enabled || !weatherBGlink.Checked)
          if (item.bgFolder == null && item.fanartProperty == null)
          {
            helper.showError("Menu Item " + item.name + " Has no Background Image folder\n\n\tPlease set a Folder", errorCode.info);
            return;
          }
      }
      if (menuStyle == chosenMenuStyle.verticalStyle)
      {
        writeMenu(menuType.vertical, true);
      }
      else
      {
        writeMenu(menuType.horizontal, true);
      }
      if (cboClearCache.Checked)
        clearCacheDir();
      Application.Exit();
    }

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

      if (!cbDisableClock.Checked)
        generateClock();

      if (!cbHideFanartScraper.Checked)
        generatefanartScraper();

      if (enableFiveDayWeather.Checked)
        GenerateFiveDayWeather();

      if (summaryWeatherCheckBox.Checked && infoserviceOptions.Enabled)
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
        generateTopBarH();
        generateMenuGraphicsH();
        
        if (menuStyle == chosenMenuStyle.graphicMenuStyle)
          generateGraphicCrowdingFixH();
        else
          generateCrowdingFixH();

        if (horizontalContextLabels.Checked)
          GenerateContextLabelsH();
      }
      else if (direction == menuType.vertical)
      {
        if (!cbExitStyleNew.Checked)
          generateTopBarV1();
        else
          generateTopBarV();

        generateMenuGraphicsV();
        generateCrowdingFixV();
        GenerateContextLabelsV();
      }

      if (enableRssfeed.Checked && infoserviceOptions.Enabled)
      {
        if (direction == menuType.horizontal)
        {
          generateRSSTicker();
          if (enableTwitter.Checked && infoserviceOptions.Enabled) generateTwitter();
        }
        else if (direction == menuType.vertical)
        {
          generateRSSTickerV();
          if (enableTwitter.Checked && infoserviceOptions.Enabled) generateTwitterV();
        }
      }

      generateMostRecentFilesAndImports("AddImports");

      toolStripStatusLabel1.Text = "Done!";

      if (System.IO.File.Exists(SkinInfo.mpPaths.streamedMPpath + "BasicHome.xml"))
        System.IO.File.Delete(SkinInfo.mpPaths.streamedMPpath + "BasicHome.xml");

      xml = xml.Replace("<!-- BEGIN GENERATED ID CODE-->", "<id>35</id>");
      CheckSum checkSum = new CheckSum();
      writeXMLFile("BasicHome.xml");

      generateMostRecentFilesAndImports("GenImports");
      generateOverlay(int.Parse(txtMenuPos.Text), basicHomeValues.weatherControl);
      changeOutstanding = false;
      getBackupFileTotals();
      if (!onFormClosing)
      {
        DialogResult result = helper.showError("BasicHome.xml Saved Sucessfully \n\n  Backup file has been created \n\nDo you want to Contine Editing", errorCode.infoQuestion);
        if (result == DialogResult.No)
        {
          //reset everything
          xmlFiles.Items.Clear();
          cboQuickSelect.Items.Clear();
          itemsOnMenubar.Items.Clear();
          prettyItems.Clear();
          ids.Clear();
          bgItems.Clear();
          menuItems.Clear();
          this.Close();
        }

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
              if (menItem.fhBGSource ==  fanartSource.Scraper)
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
            if (cbMostRecentTvSeries.Checked)
                // Params: Overlay Type, Recent added summary x,y, Recent watched summary x,y
                generateMostRecentOverlay(menuStyle, isOverlayType.TVSeries, 976, 50, 976, 370);

            if (cbMostRecentMovPics.Checked)
                // Params: Overlay Type, Recent added summary x,y, Recent watched summary x,y
                generateMostRecentOverlay(menuStyle, isOverlayType.MovPics, 976, 50, 976, 370);
            //
            // Only generate music and RecordedTV if the correct Fanart Handler version is installed and enabled
            //
            if (helper.pluginEnabled(Helper.Plugins.FanartHandler) && (fanarthandlerVersionRequired.CompareTo(fhOverlayVersion) <= 0))
            {
                // Params: Overlay Type, Recent added summary x,y, Recent watched summary x,y
                if (cbEnableRecentMusic.Checked)
                    generateMostRecentOverlay(menuStyle, isOverlayType.Music, 976, 50, 0, 0);

                // Params: Overlay Type, Recent added summary x,y, Recent watched summary x,y
                if (cbEnableRecentRecordedTV.Checked)
                    generateMostRecentOverlay(menuStyle, isOverlayType.RecordedTV, 976, 50, 0, 0);
            }

            if (cbFreeDriveSpaceOverlay.Checked)
                generateMostRecentOverlay(menuStyle, isOverlayType.freeDriveSpace, 976, 50, 0, 0);

            if (cbSleepControlOverlay.Checked)
                generateMostRecentOverlay(menuStyle, isOverlayType.sleepControl, 976, 50, 0, 0);

            if (cbSocksOverlay.Checked)
                generateMostRecentOverlay(menuStyle, isOverlayType.stocks, 976, 50, 0, 0);

            if (cbPowerControlOverlay.Checked)
                generateMostRecentOverlay(menuStyle, isOverlayType.powerControl, 976, 50, 0, 0);

            if (cbHtpcInfoOverlay.Checked)
                generateMostRecentOverlay(menuStyle, isOverlayType.htpcInfo, 976, 50, 0, 0);

            if (cbUpdateControlOverlay.Checked)
                generateMostRecentOverlay(menuStyle, isOverlayType.updateControl, 976, 50, 0, 0);
        }
      //
      // Add the imports to basichome
      //
      if (recentAction == "AddImports")
      {
        if (cbMostRecentTvSeries.Checked && mostRecentVisibleControls(isOverlayType.TVSeries) != null)
          generateMostRecentInclude(isOverlayType.TVSeries);

        if (cbMostRecentMovPics.Checked && mostRecentVisibleControls(isOverlayType.MovPics) != null)
          generateMostRecentInclude(isOverlayType.MovPics);

        //
        // Only add imports to basichome if the correct Fanart Handler version is installed and enabled
        //
        if (helper.pluginEnabled(Helper.Plugins.FanartHandler) && (fanarthandlerVersionRequired.CompareTo(fhOverlayVersion) <= 0))
        {
          if (cbEnableRecentMusic.Checked && mostRecentVisibleControls(isOverlayType.Music) != null)
            generateMostRecentInclude(isOverlayType.Music);

          if (cbEnableRecentRecordedTV.Checked && mostRecentVisibleControls(isOverlayType.RecordedTV) != null)
            generateMostRecentInclude(isOverlayType.RecordedTV);
        }

        if (cbFreeDriveSpaceOverlay.Checked)
          generateMostRecentInclude(isOverlayType.freeDriveSpace);

        if (cbSleepControlOverlay.Checked)
          generateMostRecentInclude(isOverlayType.sleepControl);

        if (cbSocksOverlay.Checked)
          generateMostRecentInclude(isOverlayType.stocks);

        if (cbPowerControlOverlay.Checked)
          generateMostRecentInclude(isOverlayType.powerControl);

        if (cbHtpcInfoOverlay.Checked)
          generateMostRecentInclude(isOverlayType.htpcInfo);

          if (cbUpdateControlOverlay.Checked)
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
      if (mostRecentVisibleControls(isOverlayType.Music) == "No" && cbEnableRecentMusic.Checked)
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
      if (mostRecentVisibleControls(isOverlayType.RecordedTV) == "No" && cbEnableRecentRecordedTV.Checked)
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

    void horizontalContextLabels_CheckedChanged(object sender, EventArgs e)
    {
      setBasicHomeValues();
    }

    private void addSubmenus_Click(object sender, EventArgs e)
    {
      if (itemsOnMenubar.SelectedIndex != -1)
      {
        formSubMenuDesigner subMenuForm = new formSubMenuDesigner();
        subMenuForm.createSubmenu(itemsOnMenubar.SelectedIndex);

        // If we have added a submenu then disable Background sharing - get background issues outherwise
        if (menuItems[itemsOnMenubar.SelectedIndex].subMenuLevel1.Count > 0)
            menuItems[itemsOnMenubar.SelectedIndex].disableBGSharing = true;
      }
      else
        helper.showError("Please Highlight Menu Item to add SubMenus to", errorCode.info);
    }

    private void btSelectOverlays_Click(object sender, EventArgs e)
    {
      setOverlayStates();
      if (itemsOnMenubar.SelectedIndex != -1)
      {
        mrDisplaySelection.mrToDisplay = menuItems[itemsOnMenubar.SelectedIndex].showMostRecent;
        mrDisplaySelection.ShowDialog();
        menuItems[itemsOnMenubar.SelectedIndex].showMostRecent = mrDisplaySelection.mrToDisplay;
      }
      else
        helper.showError("Please Highlight Menu Item to edit Overlays to", errorCode.info);
    }

    void setOverlayStates()
    {
      mrDisplaySelection.setEnableState(displayMostRecent.freeDriveSpace, cbFreeDriveSpaceOverlay.Checked);
      mrDisplaySelection.setEnableState(displayMostRecent.htpcInfo, cbHtpcInfoOverlay.Checked);
      mrDisplaySelection.setEnableState(displayMostRecent.music, cbEnableRecentMusic.Checked);
      mrDisplaySelection.setEnableState(displayMostRecent.powerControl, cbPowerControlOverlay.Checked);
      mrDisplaySelection.setEnableState(displayMostRecent.recordedTV, cbEnableRecentRecordedTV.Checked);
      mrDisplaySelection.setEnableState(displayMostRecent.sleepControl, cbSleepControlOverlay.Checked);
      mrDisplaySelection.setEnableState(displayMostRecent.stocks, cbSocksOverlay.Checked);
      mrDisplaySelection.setEnableState(displayMostRecent.updateControl, cbUpdateControlOverlay.Checked);

      mrDisplaySelection.setEnableState(displayMostRecent.tvSeries, false);
      if (cbMostRecentTvSeries.Checked || cbTVSeriesRecentWatched.Checked)
        mrDisplaySelection.setEnableState(displayMostRecent.tvSeries, true);

      mrDisplaySelection.setEnableState(displayMostRecent.movies, false);
      if (cbMostRecentMovPics.Checked || cbMovPicsRecentWatched.Checked)
        mrDisplaySelection.setEnableState(displayMostRecent.movies, cbMostRecentMovPics.Checked);
    }

    private void cboParameterViews_SelectedIndexChanged(object sender, EventArgs e)
    {
      //cboContextLabel.Text = tvseriesViews[cboTvSeriesView.SelectedIndex].Value.ToUpper();
    }

    private void btConfigureFreeDriveSpace_Click(object sender, EventArgs e)
    {
      SelectHardDrives selectDrives = new SelectHardDrives();
      selectDrives.ShowDialog();
    }

    private void cboFanartProperty_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (!fanartHandlerRelease2)
        return;

      if (cboFanartProperty.Text.ToLower() == "music" || cboFanartProperty.Text.ToLower() == "movie")
      {
        fhChoice.Visible = true;
        fhRBScraper.Visible = true;
        fhRBUserDef.Visible = true;
      }
      else
      {
        fhChoice.Visible = false;
        fhRBScraper.Visible = false;
        fhRBUserDef.Visible = false;
      }
    }

    private void cbFreeDriveSpaceOverlay_CheckedChanged(object sender, EventArgs e)
    {
      btConfigureFreeDriveSpace.Enabled = cbFreeDriveSpaceOverlay.Checked ? true : false;
      mrDisplaySelection.setEnableState(displayMostRecent.freeDriveSpace, cbFreeDriveSpaceOverlay.Checked);
    }

    private void cbPowerControlOverlay_CheckedChanged(object sender, EventArgs e)
    {
      mrDisplaySelection.setEnableState(displayMostRecent.powerControl, cbPowerControlOverlay.Checked);
    }

    private void cbSleepControlOverlay_CheckedChanged(object sender, EventArgs e)
    {
      mrDisplaySelection.setEnableState(displayMostRecent.sleepControl, cbSleepControlOverlay.Checked);
    }

    private void cbSocksOverlay_CheckedChanged(object sender, EventArgs e)
    {
      mrDisplaySelection.setEnableState(displayMostRecent.stocks, cbSocksOverlay.Checked);
    }

    private void cbHtpcInfoOverlay_CheckedChanged(object sender, EventArgs e)
    {
      mrDisplaySelection.setEnableState(displayMostRecent.htpcInfo, cbHtpcInfoOverlay.Checked);
    }

    private void cbUpdateControlOverlay_CheckedChanged(object sender, EventArgs e)
    {
      mrDisplaySelection.setEnableState(displayMostRecent.updateControl, cbUpdateControlOverlay.Checked);
    }

    private void cbEnableRecentMusic_CheckedChanged(object sender, EventArgs e)
    {
      mrDisplaySelection.setEnableState(displayMostRecent.music, cbEnableRecentMusic.Checked);
    }

    private void cbEnableRecentRecordedTV_CheckedChanged(object sender, EventArgs e)
    {
      mrDisplaySelection.setEnableState(displayMostRecent.recordedTV, cbEnableRecentRecordedTV.Checked);
    }

    private void btMenuIcon_Click(object sender, EventArgs e)
    {
      buttonTexture.setButtonTexture();
      displayMenuIcon(buttonTexture.SelectedIcon);
    }

    #endregion

    #region Parmeter Handling

    /// <summary>
    /// Get list of views in TVseries database
    /// Key: should be used as hyperlinkParameter
    /// Val: can be used as a default display name
    /// </summary>    
    private List<KeyValuePair<string, string>> GetTVSeriesViews()
    {
      // check if we have already got them
      if (tvseriesViews.Count == 0)
        tvseriesViews = DBView.GetSkinViews();

      return tvseriesViews;
    }

    private List<KeyValuePair<string, string>> GetOnlineVideosViews()
    {
      // check if we have already got them
      if (onlineVideosViews.Count == 0)
      {
        // set path of config file, so we load user settings
        OnlineVideoSettings.Instance.ConfigDir = SkinInfo.mpPaths.configBasePath;

        // load list of sites
        OnlineVideoSettings onlineVideos = OnlineVideos.OnlineVideoSettings.Instance;
        onlineVideos.LoadSites();

        foreach (SiteSettings site in onlineVideos.SiteSettingsList)
        {
          // just get a list of enabled sites
          if (site.IsEnabled)
          {
            KeyValuePair<string, string> view = new KeyValuePair<string, string>(site.Name, site.Name);
            onlineVideosViews.Add(view);
          }
        }

      }
  
      return onlineVideosViews;
    }

    /// <summary>
    /// Get list of views in Music Views database
    /// Key: should be used as hyperlinkParameter
    /// Val: can be used as a default display name
    /// </summary>    
    private List<KeyValuePair<string, string>> GetMusicViews()
    {
      if (musicViews.Count == 0)
      {
        MusicViewHandler MusicViews = new MusicViewHandler();
        foreach (ViewDefinition MusicView in MusicViews.Views)
        {
          string viewKey = MusicView.Name;
          string viewValue = MusicView.LocalizedName;
          KeyValuePair<string, string> skinview = new KeyValuePair<string, string>(viewKey, viewValue);
          musicViews.Add(skinview);
        }
      }

      return musicViews;
    }

    #endregion

    #region ISetupForm Members

    /// <summary>
    /// Returns the name of the plugin which is shown in the plugin menu
    /// </summary>
    /// <returns>the name of the plugin which is shown in the plugin menu</returns>
    public string PluginName()
    {
      return "StreamedMP BasicHome Editor";
    }

    /// <summary>
    /// Returns the description of the plugin which is shown in the plugin menu
    /// </summary>
    /// <returns>the description of the plugin which is shown in the plugin menu</returns>
    public string Description()
    {
      return "StreamedMP BasicHome Editor";
    }

    /// <summary>
    /// Returns the author of the plugin which is shown in the plugin menu
    /// </summary>
    /// <returns>the author of the plugin which is shown in the plugin menu</returns>
    public string Author()
    {
      return "The StreamedMP Team";
    }

    /// <summary>
    /// Indicates whether plugin can be enabled/disabled
    /// </summary>
    public void ShowPlugin()
    {
      formStreamedMpEditor startEditor = new formStreamedMpEditor();
      startEditor.ShowDialog();
    }

    /// <summary>
    /// Indicates whether plugin can be enabled/disabled
    /// </summary>
    /// <returns>true if the plugin can be enabled/disabled</returns>
    public bool CanEnable()
    {
      return false;
    }

    /// <summary>
    /// Get Windows-ID
    /// </summary>
    /// <returns>unique id for this plugin</returns>
    public int GetWindowId()
    {
      // WindowID of windowplugin belonging to this setup
      // enter your own unique code
      return -1;
    }

    /// <summary>
    /// Indicates if plugin is enabled by default
    /// </summary>
    /// <returns>true if this plugin is enabled by default</returns>
    public bool DefaultEnabled()
    {
      return true;
    }

    /// <summary>
    /// indicates if a plugin has its own setup screen
    /// </summary>
    /// <returns>true if the plugin has its own setup screen</returns>
    public bool HasSetup()
    {
      return true;
    }

    /// <summary>
    /// no Home for this plugin, return false
    /// </summary>
    /// <param name="strButtonText"></param>
    /// <param name="strButtonImage"></param>
    /// <param name="strButtonImageFocus"></param>
    /// <param name="strPictureImage"></param>
    /// <returns></returns>
    public bool GetHome(out string strButtonText, out string strButtonImage,
                        out string strButtonImageFocus, out string strPictureImage)
    {
      strButtonText = String.Empty;
      strButtonImage = String.Empty;
      strButtonImageFocus = String.Empty;
      strPictureImage = String.Empty;
      return false;
    }

    #endregion

  }
}



