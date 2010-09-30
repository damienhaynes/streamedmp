using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Reflection;
using MediaPortal.Configuration;
using MediaPortal.GUI.Library;
using MediaPortal.Utils;
using System.Text.RegularExpressions;
using SMPCheckSum;

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

    enum chosenMenuStyle
    {
      verticalStyle,
      horizontalStandardStyle,
      horizontalContextStyle,
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

    enum isOverlayType
    {
      TVSeries,
      MovPics
    }

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
      moviesAdded,
      moviesWatched,
      music,
      recordedTV
    }

    #endregion

    #region Variables


    public static List<prettyItem> prettyItems = new List<prettyItem>();
    public static List<menuItem> menuItems = new List<menuItem>();

    List<backgroundItem> bgItems = new List<backgroundItem>();
    List<string> ids = new List<string>();
    List<string> idsTemp = new List<string>();
    List<string> skinFontsFocused = new List<string>();
    List<string> skinFontsUnFocused = new List<string>();

    const string tvseriesSkinID = "9811";
    const string movingPicturesSkinID = "96742";
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

    string xml;
    string xmlTemplate;
    string dropShadowColor = "1d1f1b";
    string infoServiceDayProperty = "forecast";
    string splashScreenImage = null;
    string defFocus = "FFFFFF";
    string defUnFocus = "C0C0C0";
    string level1LateralBladeVisible;
    string level2LateralBladeVisible;


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

    public static Regex isIleagalXML = new Regex("[&<>]");

    // Default Style to StreamedMP standard
    chosenMenuStyle menuStyle = chosenMenuStyle.verticalStyle;
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
          itemName.Enabled = true;
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
          itemName.Text = null;
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

        Version mpVersion = new Version(MediaPortalVersion);
        if (mpVersion.CompareTo(mpReleaseVersion) > 0)
        {
          wrapString.Enabled = true;
        }
        else
        {
          wrapString.Enabled = false;
        }

        if (basicHomeLoadError)
        {
          DialogResult result = helper.showError("There was an issue reading your current BasicHome.xml file\r\rthe format is to differnet to be parsed correctly\r\rWould you like save your existing BasicHome\r\rand load a template BasicHome for Editing?", errorCode.infoQuestion);
          if (result == DialogResult.Yes)
          {
            BasicHomeFromTemplate();
            basicHomeLoadError = false;
            loadMenuSettings();
            GetDefaultBackgroundImages();
          }
          else
            helper.showError("Editing is not possible due to parsing issues with current BasicHome.xml file", errorCode.major);
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

      //return true;
      if (xmlFiles.Items.Count > 0)
      {
        helper.loadPrettyItems(ref cboQuickSelect,ids);
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
      if (rbDisplayMostRecentNone.Checked)
        return displayMostRecent.off;

      if (rbDisplayMostRecentTVSeries.Checked)
        return displayMostRecent.tvSeries;

      return displayMostRecent.moviesAdded;
    }

    void setMostRecentDisplayOption(displayMostRecent dmr)
    {
      switch (dmr)
      {
        case displayMostRecent.off:
          {
            rbDisplayMostRecentNone.Checked = true;
            rbDisplayMostRecentMovies.Checked = false;
            rbDisplayMostRecentTVSeries.Checked = false;
            break;
          }
        case displayMostRecent.tvSeries:
          {
            rbDisplayMostRecentNone.Checked = false;
            rbDisplayMostRecentMovies.Checked = false;
            rbDisplayMostRecentTVSeries.Checked = true;
            break;
          }
        case displayMostRecent.moviesAdded:
          {
            rbDisplayMostRecentNone.Checked = false;
            rbDisplayMostRecentMovies.Checked = true;
            rbDisplayMostRecentTVSeries.Checked = false;
            break;
          }
      }
    }


    void addButton_Click(object sender, EventArgs e)
    {

      if (xmlFiles.SelectedItem != null && (bgBox.Text != "" || cboFanartProperty.Text != "") && itemName.Text != "")
      {
        toolStripStatusLabel1.Text = xmlFiles.SelectedItem.ToString() + " added to menu";
        menuItem item = new menuItem();
        item.name = itemName.Text;
        item.contextLabel = cboContextLabel.Text;
        item.hyperlink = ids[xmlFiles.SelectedIndex];
        item.bgFolder = bgBox.Text;
        item.fanartProperty = cboFanartProperty.Text;
        item.fanartHandlerEnabled = cbItemFanartHandlerEnable.Checked;
        item.EnableMusicNowPlayingFanart = cbEnableMusicNowPlayingFanart.Checked;
        item.isWeather = isWeather.Checked;
        item.disableBGSharing = disableBGSharing.Checked;
        item.showMostRecent = getMostRecentDisplayOption();

        if (item.fanartHandlerEnabled)
          checkAndSetRandomFanart(item.fanartProperty);
        else
          checkAndSetDefultImage(item);

        menuItems.Add(item);
        itemsOnMenubar.Items.Add(item.name);
        reloadBackgroundItems();
        itemName.Text = "";
        bgBox.Text = "";

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
      setXMLFilesIndex(mnuItem.hyperlink);
      itemName.Text = mnuItem.name;
      cboContextLabel.Text = mnuItem.contextLabel;
      bgBox.Text = mnuItem.bgFolder;
      cboFanartProperty.Text = mnuItem.fanartProperty;

      if (cboFanartProperty.Text.ToLower() == "false")
        cboFanartProperty.Text = "";

      cbItemFanartHandlerEnable.Checked = mnuItem.fanartHandlerEnabled;
      cbEnableMusicNowPlayingFanart.Checked = mnuItem.EnableMusicNowPlayingFanart;
      disableBGSharing.Checked = mnuItem.disableBGSharing;
      isWeather.Checked = mnuItem.isWeather;
      selectedWindow.Text = xmlFiles.Text;
      selectedWindowID.Text = ids[index];
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
        item.name = itemName.Text;
        item.contextLabel = cboContextLabel.Text;
        item.bgFolder = bgBox.Text;
        item.fanartProperty = cboFanartProperty.Text;
        item.fanartHandlerEnabled = cbItemFanartHandlerEnable.Checked;
        item.EnableMusicNowPlayingFanart = cbEnableMusicNowPlayingFanart.Checked;
        item.hyperlink = ids[xmlFiles.SelectedIndex];
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

        menuItems[index] = item;
        itemsOnMenubar.Items.RemoveAt(index);
        itemsOnMenubar.Items.Insert(index, item.name);
        reloadBackgroundItems();
        screenReset();
        disableItemControls();
        cancelCreateButton.Visible = false;
        btGenerateMenu.Enabled = true;
        changeOutstanding = true;
      }
    }

    void setXMLFilesIndex(string hyperlink)
    {
      for (int i = 0; i < ids.Count; i++)
      {
        if (ids[i] == hyperlink)
          xmlFiles.SelectedIndex = i;
      }
    }

    void screenReset()
    {
      if (saveButton.Enabled)
      {
        itemName.Text = null;
        cboContextLabel.Text = null;
        isWeather.Checked = false;
        bgBox.SelectedIndex = -1;
        cboFanartProperty.SelectedIndex = -1;
        saveButton.Enabled = false;
        cancelButton.Enabled = false;
        editButton.Enabled = true;
      }
      selectedWindow.Text = null;
      selectedWindowID.Text = null;

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

      setXMLFilesIndex(mnuItem.hyperlink);
      cboContextLabel.Text = mnuItem.contextLabel;
      itemName.Text = mnuItem.name;
      cboFanartProperty.Text = mnuItem.fanartProperty;
      cbItemFanartHandlerEnable.Checked = mnuItem.fanartHandlerEnabled;
      cbEnableMusicNowPlayingFanart.Checked = mnuItem.EnableMusicNowPlayingFanart;
      disableBGSharing.Checked = mnuItem.disableBGSharing;

      menuitemName.Text = mnuItem.name;
      menuItemLabel.Text = mnuItem.contextLabel;
      menuitemBGFolder.Text = mnuItem.bgFolder;
      bgBox.Text = mnuItem.bgFolder;
      menuitemWindow.Text = xmlFiles.Text;
      setMostRecentDisplayOption(mnuItem.showMostRecent);

      UpdateImageControlVisibility(mnuItem.fanartHandlerEnabled);
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
      int start = itemName.SelectionStart;
      if (isIlegalXML(itemName.Text))
      {
        itemName.Text = itemName.Text.Substring(0, itemName.Text.Length - 1);
        itemName.SelectionStart = start;
        return;
      }
      itemName.Text = itemName.Text.ToUpper();
      itemName.SelectionStart = start;
    }


    void cboContextLabels_TextChanged(object sender, EventArgs e)
    {
      int start = cboContextLabel.SelectionStart;
      if (isIlegalXML(itemName.Text))
      {
        itemName.Text = itemName.Text.Substring(0, itemName.Text.Length - 1);
        itemName.SelectionStart = start;
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
        foreach (backgroundItem item in bgItems)
        {
          if (item.isWeather)
          {
            basicHomeValues.weatherControl = (int.Parse(item.ids[0]) + 200);
            generateWeathersummary(basicHomeValues.weatherControl);
          }
        }
      }

      if (direction == menuType.horizontal)
      {
        generateTopBarH();
        generateMenuGraphicsH();
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
      //
      // Infoservice Most Recent Imports
      //
      if (infoserviceOptions.Enabled)
      {
        if (cbMostRecentTvSeries.Checked)
        {
          generateMostRecentInclude(isOverlayType.TVSeries);
        }

        if (cbMostRecentMovPics.Checked)
        {
          generateMostRecentInclude(isOverlayType.MovPics);
        }
      }

      toolStripStatusLabel1.Text = "Done!";

      if (System.IO.File.Exists(SkinInfo.mpPaths.streamedMPpath + "BasicHome.xml"))
        System.IO.File.Copy(SkinInfo.mpPaths.streamedMPpath + "BasicHome.xml", SkinInfo.mpPaths.streamedMPpath + "BasicHome.xml.backup." + DateTime.Now.Ticks.ToString());

      if (System.IO.File.Exists(SkinInfo.mpPaths.streamedMPpath + "BasicHome.xml"))
        System.IO.File.Delete(SkinInfo.mpPaths.streamedMPpath + "BasicHome.xml");

      xml = xml.Replace("<!-- BEGIN GENERATED ID CODE-->", "<id>35</id>");

      CheckSum checkSum = new CheckSum();

      writeXMLFile("BasicHome.xml");

      generateOverlay(int.Parse(txtMenuPos.Text), basicHomeValues.weatherControl);

      //
      // Generate the Infoservice Most Recent Import files
      //
      if (infoserviceOptions.Enabled)
      {
        if (cbMostRecentTvSeries.Checked)
        {
          generateMostRecentOverlay(menuStyle, isOverlayType.TVSeries);
        }

        if (cbMostRecentMovPics.Checked)
        {
          generateMostRecentOverlay(menuStyle, isOverlayType.MovPics);
        }
      }

      changeOutstanding = false;
      getBackupFileTotals();
      if (!onFormClosing)
      {
        DialogResult result = helper.showError("BasicHome.xml Saved Sucessfully \n\n  Backup file has been created \n\nDo you want to Contine Editing", errorCode.infoQuestion);
        if (result == DialogResult.No)
          this.Close();

        // reset item id's as it is possible to generate again.
        foreach (menuItem item in menuItems)
        {
          item.id = menuItems.IndexOf(item);
        }
      }
    }

    void horizontalContextLabels_CheckedChanged(object sender, EventArgs e)
    {
      setBasicHomeValues();
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

    private void addSubmenus_Click(object sender, EventArgs e)
    {
      formSubMenuDesigner subMenuForm = new formSubMenuDesigner();
      subMenuForm.createSubmenu(itemsOnMenubar.SelectedIndex);
    }

  }
}



