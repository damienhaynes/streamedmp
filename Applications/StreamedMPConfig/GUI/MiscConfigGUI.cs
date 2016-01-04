using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MediaPortal.Configuration;
using MediaPortal.Dialogs;
using MediaPortal.GUI.Library;
using Action = MediaPortal.GUI.Library.Action;

namespace StreamedMPConfig
{
  public class MiscConfigGUI : GUIWindow
  {
    #region Enums
    private enum GUIControls
    {
      HiddenMenuImage = 2,
      RoundedImages = 3,
      IconsInArtwork = 4,
      PlayRecents = 5,
      FilterWatchedRecents = 6,
      LargeFontSize = 7,
      UnfocusedAlpha = 8,
      ListColors = 9,
      ListControlScrollPopup = 10,
      TextualLogos = 11,
      MyVideoWatchedProgress = 12,
      TraktIconsInArtwork = 13
    }  
    #endregion

    #region Local Variables
    private static readonly logger smcLog = logger.GetInstance();

    private string UnfocusedAlphaListTemp;
    private string UnfocusedAlphaThumbsTemp;

    private string TextColorTemp;
    private string TextColor2Temp;
    private string TextColor3Temp;
    private string WatchedColorTemp;
    private string RemoteColorTemp;

    private bool TextualLogosTemp;

    private Dictionary<int, string> KnownColors = new Dictionary<int, string>();
    #endregion

    #region Skin Controls
    [SkinControl((int)GUIControls.HiddenMenuImage)]
    protected GUICheckButton btnHiddenMenuImage = null;

    [SkinControl((int)GUIControls.RoundedImages)]
    protected GUICheckButton btnRoundedImages = null;

    [SkinControl((int)GUIControls.IconsInArtwork)]
    protected GUICheckButton btnIconsInArtwork = null;

    [SkinControl((int)GUIControls.TraktIconsInArtwork)]
    protected GUICheckButton btnTraktIconsInArtwork = null;

    [SkinControl((int)GUIControls.PlayRecents)]
    protected GUICheckButton btnPlayRecents = null;

    [SkinControl((int)GUIControls.FilterWatchedRecents)]
    protected GUICheckButton btnFilterWatchedRecents = null;

    [SkinControl((int)GUIControls.LargeFontSize)]
    protected GUICheckButton btnLargeFontSize = null;

    [SkinControl((int)GUIControls.UnfocusedAlpha)]
    protected GUIButtonControl btnUnfocusedAlpha = null;

    [SkinControl((int)GUIControls.ListColors)]
    protected GUIButtonControl btnListColors = null;

    [SkinControl((int)GUIControls.ListControlScrollPopup)]
    protected GUICheckButton btnListControlScrollPopup = null;

    [SkinControl((int)GUIControls.TextualLogos)]
    protected GUICheckButton btnTextualLogos = null;

    [SkinControl((int)GUIControls.MyVideoWatchedProgress)]
    protected GUICheckButton btnMyVideoWatchedProgress = null;
    #endregion

    #region Constructor
    public MiscConfigGUI() {}
    #endregion

    #region Public Properties
    public static bool ShowHiddenMenuImage { get; set; }
    public static bool ShowRoundedImages { get; set; }
    public static bool ShowIconsInArtwork { get; set; }
    public static bool ShowTraktStyleIconsInArtwork { get; set; }
    public static bool EnablePlayMostRecents { get; set; }
    public static bool FilterWatchedInRecentlyAdded { get; set; }
    public static int MostRecentFanartTimerInt { get; set; }
    public static string TextColor { get; set; }
    public static string TextColor2 { get; set; }
    public static string TextColor3 { get; set; }
    public static string WatchedColor { get; set; }
    public static string RemoteColor { get; set; }
    public static int UnfocusedAlphaListItems { get; set; }
    public static int UnfocusedAlphaThumbs { get; set; }
    public static bool UseLargeFonts { get; set; }
    public static bool ShowListScrollingPopup { get; set; }
    public static bool TextualLogos { get; set; }
    public static bool MyVideoWatchedProgress { get; set; }
    #endregion

    #region Public Methods
    public static void SetProperties()
    {
      StreamedMPConfig.SetProperty("#StreamedMP.ActionMenu.Image", ShowHiddenMenuImage ? "Action_menu.png" : "-");
      StreamedMPConfig.SetProperty("#StreamedMP.Settings.ShowRoundedCovers", ShowRoundedImages.ToString());

      // Icons in Artwork require a few image properties
      StreamedMPConfig.SetProperty("#StreamedMP.Icons.Background", ShowIconsInArtwork ? "overlaywubg.png" : "-");
      StreamedMPConfig.SetProperty("#StreamedMP.Icons.Watched", ShowIconsInArtwork ? "overlaywatched.png" : "-");
      StreamedMPConfig.SetProperty("#StreamedMP.Icons.UnWatched", ShowIconsInArtwork ? "overlayunwatched.png" : "-");
      StreamedMPConfig.SetProperty("#StreamedMP.Icons.NAWatched", ShowIconsInArtwork ? "overlaywatchedna.png" : "-");
      StreamedMPConfig.SetProperty("#StreamedMP.Icons.NAUnWatched", ShowIconsInArtwork ? "overlayunwatchedna.png" : "-");
      StreamedMPConfig.SetProperty("#StreamedMP.Icons.Hide", ShowIconsInArtwork ? "false" : "true");
      StreamedMPConfig.SetProperty("#StreamedMP.Icons.Trakt", ShowTraktStyleIconsInArtwork ? "true" : "false");

      StreamedMPConfig.SetProperty("#StreamedMP.ListScrollPopup", ShowListScrollingPopup ? "yes" : "no");

      // Logo Style
      StreamedMPConfig.SetProperty("#StreamedMP.MediaInfo.Type", TextualLogos ? "Textual" : "Graphical");

      // My Video Watched Progress Indicators on Facade
      StreamedMPConfig.SetProperty("#StreamedMP.ShowProgressIndicators", MyVideoWatchedProgress.ToString().ToLowerInvariant());
    }

    public static void SetColors()
    {
      string file = GUIGraphicsContext.Skin + @"\references.xml";

      smcLog.WriteLog("Setting List Control Colors...", LogLevel.Info);
      Helper.SetNodeText(file, "/controls/control[type='listcontrol']/textcolor", string.Concat("FF", MiscConfigGUI.TextColor));
      Helper.SetNodeText(file, "/controls/control[type='listcontrol']/textcolor2", string.Concat("FF", MiscConfigGUI.TextColor2));
      Helper.SetNodeText(file, "/controls/control[type='listcontrol']/textcolor3", string.Concat("FF", MiscConfigGUI.TextColor3));
      Helper.SetNodeText(file, "/controls/control[type='listcontrol']/playedColor", string.Concat("FF", MiscConfigGUI.WatchedColor));
      Helper.SetNodeText(file, "/controls/control[type='listcontrol']/remoteColor", string.Concat("FF", MiscConfigGUI.RemoteColor));
      
      smcLog.WriteLog("Setting PlayList Control Colors...", LogLevel.Info);
      Helper.SetNodeText(file, "/controls/control[type='playlistcontrol']/textcolor", string.Concat("FF", MiscConfigGUI.TextColor));
      Helper.SetNodeText(file, "/controls/control[type='playlistcontrol']/textcolor2", string.Concat("FF", MiscConfigGUI.TextColor2));
      Helper.SetNodeText(file, "/controls/control[type='playlistcontrol']/textcolor3", string.Concat("FF", MiscConfigGUI.TextColor3));
      Helper.SetNodeText(file, "/controls/control[type='playlistcontrol']/playedColor", string.Concat("FF", MiscConfigGUI.WatchedColor));
      Helper.SetNodeText(file, "/controls/control[type='playlistcontrol']/remoteColor", string.Concat("FF", MiscConfigGUI.RemoteColor));
    }

    public static void SetAlphaTransparency()
    {
      string file = GUIGraphicsContext.Skin + @"\references.xml";

      smcLog.WriteLog("Setting Unfocused Alpha Settings", LogLevel.Info);
      Helper.SetNodeText(file, "/controls/control[type='listcontrol']/unfocusedAlpha", MiscConfigGUI.UnfocusedAlphaListItems.ToString());
      Helper.SetNodeText(file, "/controls/control[type='playlistcontrol']/unfocusedAlpha", MiscConfigGUI.UnfocusedAlphaListItems.ToString());
      Helper.SetNodeText(file, "/controls/control[type='thumbnailpanel']/unfocusedAlpha", MiscConfigGUI.UnfocusedAlphaThumbs.ToString());
    }
    #endregion

    #region Private Methods
    private void ShowInvalidUnfocusedAlphaMessage()
    {
      GUIDialogOK dlg = (GUIDialogOK)GUIWindowManager.GetWindow((int)GUIWindow.Window.WINDOW_DIALOG_OK);
      dlg.Reset();
      dlg.SetHeading(Translation.UnfocusedAlpha);
      dlg.SetLine(1, Translation.UnfocusedAlphaInvalidLine1);
      dlg.SetLine(2, Translation.UnfocusedAlphaInvalidLine2);
      dlg.SetLine(3, Translation.UnfocusedAlphaInvalidLine3);
      dlg.DoModal(GetID);
    }

    private void ShowInvalidColorMessage()
    {
      GUIDialogOK dlg = (GUIDialogOK)GUIWindowManager.GetWindow((int)GUIWindow.Window.WINDOW_DIALOG_OK);
      dlg.Reset();
      dlg.SetHeading(Translation.ListColors);
      dlg.SetLine(1, Translation.ListColorInvalidLine1);
      dlg.SetLine(2, Translation.ListColorInvalidLine2);
      dlg.SetLine(3, Translation.ListColorInvalidLine3);
      dlg.DoModal(GetID);
    }

    private Dictionary<string, string> GetColors()
    {
      Dictionary<string, string> colors = new Dictionary<string, string>();
      
      // get the color names from the Known color enum
      string[] colorNames = Enum.GetNames(typeof(KnownColor)); 

      foreach (string colorName in colorNames)
      {
        // cast the colorName into a KnownColor
        KnownColor knownColor = (KnownColor)Enum.Parse(typeof(KnownColor), colorName);
        Color color = Color.FromKnownColor(knownColor);

        // check if the knownColor variable is a System color      
        if (knownColor > KnownColor.Transparent)
        {
          colors.Add(colorName, GetHexCode(color));
        }
      }
      return colors;
    }

    private string GetHexCode(Color color)
    {
      byte[] data = { color.R, color.G, color.B };
      string hex = BitConverter.ToString(data);
      return hex.Replace("-", string.Empty);
    }

    private Color GetColor(string hexCode)
    {
      try
      {
        int r = Convert.ToInt32(hexCode.Substring(0, 2), 16);
        int g = Convert.ToInt32(hexCode.Substring(2, 2), 16);
        int b = Convert.ToInt32(hexCode.Substring(4, 2), 16);
        return Color.FromArgb(r, g, b);
      }
      catch
      {
        return Color.Empty;
      }
    }

    private string GetColorName(string hexCode)
    {      
      Color color = GetColor(hexCode);

      if (KnownColors.Count == 0)
      {
        Color someColor;
        Array knownColors = Enum.GetValues(typeof(KnownColor));
        foreach (KnownColor knownColor in knownColors)
        {
          someColor = Color.FromKnownColor(knownColor);
          if (!someColor.IsSystemColor && !KnownColors.ContainsKey(someColor.ToArgb()))          
          {
            KnownColors.Add(someColor.ToArgb(), someColor.Name);
          }
        }
      }

      string colorName = string.Empty;

      if (!KnownColors.TryGetValue(color.ToArgb(), out colorName))
      {
        colorName = color.Name.Substring(2);
      }
      return colorName;
    }

    private bool IsValidColor(string hexCode)
    {
      if (hexCode.Length != 6) return false;
      return GetColor(hexCode) != Color.Empty;      
    }

    private bool ShowColorSelectorMenu(string currentColor, out string chosenColor)
    {
      chosenColor = currentColor;

      IDialogbox dlg = (IDialogbox)GUIWindowManager.GetWindow((int)GUIWindow.Window.WINDOW_DIALOG_MENU);
      if (dlg == null) return false;

      dlg.Reset();
      dlg.SetHeading(Translation.ListColors);

      // Get List of Known Colors
      Dictionary<string, string> colors = GetColors();

      // Create Custom Color menu item
      GUIListItem listItem = new GUIListItem(string.Format("{0} ...", Translation.CustomColor));
      listItem.IconImage = GetColorTextureFromMemory(new Size(34, 34), currentColor);
      dlg.Add(listItem);

      // Create color menu items
      foreach (var color in colors)
      {
        listItem = new GUIListItem(color.Key);
        listItem.IconImage = GetColorTextureFromMemory(new Size(34, 34), color.Value);
        dlg.Add(listItem);
      }

      dlg.DoModal(GUIWindowManager.ActiveWindow);
      if (dlg.SelectedId <= 0) return false;

      string retValue = string.Empty;
      bool validColor = false;

      switch (dlg.SelectedId)
      {
        case 1:
          // Custom Color, invoke keyboard
          while (!validColor)
          {
            if (StreamedMPConfig.ShowKeyboard(currentColor, out retValue))
            {
              if (IsValidColor(retValue))
              {
                chosenColor = retValue;
                validColor = true;
              }
              else
                ShowInvalidColorMessage();
            }
            else
              return false;
          }
          break;

        default:
          // Convert Known Color to HEX string
          Color color = Color.FromName(dlg.SelectedLabelText);
          chosenColor = GetHexCode(color);
          break;
      }

      return true;
    }

    private string GetColorTextureFromMemory(Size size, string identifer)
    {
      try
      {
        // create a solid image
        Bitmap bmp = new Bitmap(size.Width, size.Height);
        Graphics gfx = Graphics.FromImage(bmp);
        Brush brsh = new SolidBrush(GetColor(identifer));
        gfx.FillRectangle(brsh, new Rectangle(0, 0, size.Width, size.Height));

        string textureIdentifer = string.Concat("[#StreamedMP.", identifer, "]");

        // will load from memory if it exists
        GUITextureManager.LoadFromMemory(bmp, textureIdentifer, 0, size.Width, size.Height);
        return textureIdentifer;
      }
      catch (Exception e)
      {
        smcLog.WriteLog("Error getting texture from memory: {0}", LogLevel.Error, e.Message);
        return string.Empty;
      }
    }

    private void ShowListColorsMenu()
    {      
      IDialogbox dlg = (IDialogbox)GUIWindowManager.GetWindow((int)GUIWindow.Window.WINDOW_DIALOG_MENU);
      if (dlg == null) return;

      dlg.Reset();
      dlg.SetHeading(Translation.ListColors);

      // Create menu items
      GUIListItem listItem = new GUIListItem(string.Format(Translation.ListDefaultColor, GetColorName(TextColorTemp)));      
      listItem.IconImage = GetColorTextureFromMemory(new Size(34, 34), TextColorTemp);
      dlg.Add(listItem);

      listItem = new GUIListItem(string.Format(Translation.ListWatchedColor, GetColorName(WatchedColorTemp)));
      listItem.IconImage = GetColorTextureFromMemory(new Size(34, 34), WatchedColorTemp);
      dlg.Add(listItem);

      listItem = new GUIListItem(string.Format(Translation.ListRemoteColor, GetColorName(RemoteColorTemp)));
      listItem.IconImage = GetColorTextureFromMemory(new Size(34, 34), RemoteColorTemp);
      dlg.Add(listItem);

      listItem = new GUIListItem(string.Format(Translation.ListText2Color, GetColorName(TextColor2Temp)));
      listItem.IconImage = GetColorTextureFromMemory(new Size(34, 34), TextColor2Temp);
      dlg.Add(listItem);

      listItem = new GUIListItem(string.Format(Translation.ListText3Color, GetColorName(TextColor3Temp)));
      listItem.IconImage = GetColorTextureFromMemory(new Size(34, 34), TextColor3Temp);
      dlg.Add(listItem);

      listItem = new GUIListItem(Translation.RestoreDefaults);
      dlg.Add(listItem);

      dlg.DoModal(GUIWindowManager.ActiveWindow);
      if (dlg.SelectedId <= 0) return;

      string chosenColor = string.Empty;

      switch (dlg.SelectedId)
      {
        case 1:
          if (ShowColorSelectorMenu(TextColorTemp, out chosenColor))
          {
            TextColorTemp = chosenColor;
          }
          break;

        case 2:
          if (ShowColorSelectorMenu(WatchedColorTemp, out chosenColor))
          {
            WatchedColorTemp = chosenColor;         
          }
          break;

        case 3:
          if (ShowColorSelectorMenu(RemoteColorTemp, out chosenColor))
          {
            RemoteColorTemp = chosenColor;          
          }
          break;

        case 4:
          if (ShowColorSelectorMenu(TextColor2Temp, out chosenColor))
          {
            TextColor2Temp = chosenColor;
          }          
          break;

        case 5:
          if (ShowColorSelectorMenu(TextColor3Temp, out chosenColor))
          {
            TextColor3Temp = chosenColor;
          }
          break;

        case 6:
          TextColorTemp = "FFFFFF";
          TextColor2Temp = "FFFFFF";
          TextColor3Temp = "FFFFFF";
          WatchedColorTemp = "808080";
          RemoteColorTemp = "FFA075";
          break;
      }

    }

    private void ShowUnfocusedAlphaMenu()
    {
      IDialogbox dlg = (IDialogbox)GUIWindowManager.GetWindow((int)GUIWindow.Window.WINDOW_DIALOG_MENU);
      if (dlg == null) return;

      dlg.Reset();
      dlg.SetHeading(Translation.UnfocusedAlpha);             
    
      // Create menu items
      GUIListItem listItem = new GUIListItem(string.Format(Translation.UnfocusedAlphaMenuLists, UnfocusedAlphaListTemp));      
      dlg.Add(listItem);

      listItem = new GUIListItem(string.Format(Translation.UnfocusedAlphaMenuThumbs, UnfocusedAlphaThumbsTemp));
      dlg.Add(listItem);

      listItem = new GUIListItem(Translation.RestoreDefaults);
      dlg.Add(listItem);

      dlg.DoModal(GUIWindowManager.ActiveWindow);
      if (dlg.SelectedId <= 0) return;

      string retValue = string.Empty;
      bool invalid = false;
      switch (dlg.SelectedId)
      {
        case 1:
          string input = UnfocusedAlphaListTemp;
          if (StreamedMPConfig.ShowKeyboard(input, out retValue))
          {
            int output = 0;
            if (int.TryParse(retValue, out output))
            {
              if (output < 0 || output > 255)
                invalid = true;
              else
                UnfocusedAlphaListTemp = retValue;
            }
            else
              invalid = true;
          }
          break;

        case 2:
          input = UnfocusedAlphaThumbsTemp;
          if (StreamedMPConfig.ShowKeyboard(input, out retValue))
          {
            int output = 0;
            if (int.TryParse(retValue, out output))
            {
              if (output < 0 || output > 255)
                invalid = true;
              else
                UnfocusedAlphaThumbsTemp = retValue;
            }
            else
              invalid = true;
          }
          break;

        case 3:
          UnfocusedAlphaThumbsTemp = "100";
          UnfocusedAlphaListTemp = "100";
          break;
      }

      if (invalid)
      {
        ShowInvalidUnfocusedAlphaMessage();
        return;
      }
      
    }

    private void SetControlStates()
    {      
      // Hidden Menu Images
      btnHiddenMenuImage.Selected = ShowHiddenMenuImage;
      btnHiddenMenuImage.Label = Translation.ShowHiddenMenuImage; 

      // Rounded Poster Images
      btnRoundedImages.Selected = ShowRoundedImages;
      btnRoundedImages.Label = Translation.ShowRoundedPosters;

      // Icons In Artwork
      btnIconsInArtwork.Selected = ShowIconsInArtwork;
      btnIconsInArtwork.Label = Translation.ShowIconsInArtwork;

      // Icons In Artwork
      btnTraktIconsInArtwork.Selected = ShowTraktStyleIconsInArtwork;
      btnTraktIconsInArtwork.Label = Translation.ShowTraktIconsInArtwork;

      // Play Most Recents
      btnPlayRecents.Selected = EnablePlayMostRecents;
      btnPlayRecents.Label = Translation.PlayMostRecents;

      // Filter Watched Episodes From RecentlyAdded
      btnFilterWatchedRecents.Selected = FilterWatchedInRecentlyAdded;
      btnFilterWatchedRecents.Label = Translation.FilterWatchedRecents;

      // Fonts
      btnLargeFontSize.Selected = UseLargeFonts;
      btnLargeFontSize.Label = Translation.UseLargeFonts;

      // List Control Scrolling Popup
      btnListControlScrollPopup.Selected = ShowListScrollingPopup;
      btnListControlScrollPopup.Label = Translation.ShowListScrollingPopup;

      // Logo Style
      btnTextualLogos.Selected = TextualLogos;
      btnTextualLogos.Label = Translation.TextualMediaInfoLogos;

      // MyVideos Facade Watched Progress
      btnMyVideoWatchedProgress.Selected = MyVideoWatchedProgress;
      btnMyVideoWatchedProgress.Label = Translation.MyVideoWatchedProgress;

      // Unfocused Alpha Settings
      btnUnfocusedAlpha.Label = string.Format("{0} ...", Translation.UnfocusedAlpha);

      // List Color Settings
      btnListColors.Label = string.Format("{0} ...", Translation.ListColors);
    }

    private void GetControlStates()
    {
        ShowHiddenMenuImage = btnHiddenMenuImage.Selected;
        ShowRoundedImages = btnRoundedImages.Selected;
        ShowIconsInArtwork = btnIconsInArtwork.Selected;
        ShowTraktStyleIconsInArtwork = btnTraktIconsInArtwork.Selected;
        EnablePlayMostRecents = btnPlayRecents.Selected;
        ShowListScrollingPopup = btnListControlScrollPopup.Selected;
        TextualLogos = btnTextualLogos.Selected;
        MyVideoWatchedProgress = btnMyVideoWatchedProgress.Selected;

        // Update BasicHome RecentlyAdded with new setting
        if (FilterWatchedInRecentlyAdded != btnFilterWatchedRecents.Selected)
        {
            FilterWatchedInRecentlyAdded = btnFilterWatchedRecents.Selected;
            StreamedMPConfig.getLastThreeAddedTVSeries();
            StreamedMPConfig.getLastThreeAddedMovies();
        }
    }

    private void UpdateTVSeriesLogos()
    {
      try
      {
        var filename = Path.Combine(GUIGraphicsContext.Skin, @"TVSeries.SkinSettings.xml");
        var fileContents = File.ReadAllText(filename);
        if (TextualLogos)
        {
          fileContents = fileContents.Replace("MediaInfo\\Graphical", "MediaInfo\\Textual");
        }
        else
        {
          fileContents = fileContents.Replace("MediaInfo\\Textual", "MediaInfo\\Graphical");
        }
        File.WriteAllText(filename, fileContents);
      }
      catch (Exception e)
      {
        smcLog.WriteLog("Failed to update TVSeries Logo rules: " + e.Message, LogLevel.Error);
      }
    }

    private void UpdateMyFilmsLogos()
    {
      try
      {
        var filename = Path.Combine(GUIGraphicsContext.Skin, @"MyFilmsLogos.xml");
        var fileContents = File.ReadAllText(filename);
        if (TextualLogos)
        {
          fileContents = fileContents.Replace("MediaInfo\\Graphical", "MediaInfo\\Textual");
        }
        else
        {
          fileContents = fileContents.Replace("MediaInfo\\Textual", "MediaInfo\\Graphical");
        }
        File.WriteAllText(filename, fileContents);

        // Delete Cached Logos
        string path = Path.Combine(Config.GetFolder(Config.Dir.Thumbs), @"MyFilms\Thumbs\MyFilms_Logos\");
        foreach (var file in Directory.GetFiles(path, "MyFilms_StreamedMP*.png"))
        {
          File.Delete(file);
        }
      }
      catch (Exception e)
      {
        smcLog.WriteLog("Failed to update My Films Logo rules: " + e.Message, LogLevel.Error);
      }
    }

    /// <summary>
    /// Apply changes to Skin
    /// </summary>
    private void ApplyConfigurationChanges()
    {
      bool requiresRestart = false;

      SetProperties();

      #region Fonts
      if (UseLargeFonts != btnLargeFontSize.Selected)
      {
        requiresRestart = true;
        UseLargeFonts = btnLargeFontSize.Selected;
        string sourceFile = Path.Combine(SkinInfo.mpPaths.streamedMPpath, UseLargeFonts ? "fonts.large.xml" : "fonts.normal.xml");
        string destinationFile = Path.Combine(SkinInfo.mpPaths.streamedMPpath, "fonts.xml");
        try
        {
          smcLog.WriteLog("Setting Font Size to: {0}", UseLargeFonts ? "Large" : "Normal");
          File.Copy(sourceFile, destinationFile, true);
          StreamedMPConfig.PendingRestartChanges.Add(StreamedMPConfig.PendingChanges.ClearCache);
        }
        catch (Exception ex)
        {
          smcLog.WriteLog("Failed to copy fonts file: {0}", LogLevel.Error, ex.Message);
        }
      }
      #endregion

      #region Unfocused Alpha
      if (MiscConfigGUI.UnfocusedAlphaListItems != int.Parse(UnfocusedAlphaListTemp)) requiresRestart = true;
      if (MiscConfigGUI.UnfocusedAlphaThumbs != int.Parse(UnfocusedAlphaThumbsTemp)) requiresRestart = true;
      MiscConfigGUI.UnfocusedAlphaListItems = int.Parse(UnfocusedAlphaListTemp);
      MiscConfigGUI.UnfocusedAlphaThumbs = int.Parse(UnfocusedAlphaThumbsTemp);
      SetAlphaTransparency();
      #endregion

      #region Colors
      if (MiscConfigGUI.TextColor != TextColorTemp) requiresRestart = true;
      if (MiscConfigGUI.TextColor2 != TextColor2Temp) requiresRestart = true;
      if (MiscConfigGUI.TextColor3 != TextColor3Temp) requiresRestart = true;
      if (MiscConfigGUI.WatchedColor != WatchedColorTemp) requiresRestart = true;
      if (MiscConfigGUI.RemoteColor != RemoteColorTemp) requiresRestart = true;
      MiscConfigGUI.TextColor = TextColorTemp;
      MiscConfigGUI.TextColor2 = TextColor2Temp;
      MiscConfigGUI.TextColor3 = TextColor3Temp;
      MiscConfigGUI.WatchedColor = WatchedColorTemp;
      MiscConfigGUI.RemoteColor = RemoteColorTemp;
      SetColors();
      #endregion

      #region Logos
      // Tvseries / MyFilms Logo Rules
      // Everything else can use skin property #StreamedMP.MediaInfo.Type
      if (TextualLogosTemp != TextualLogos)
      {
        requiresRestart = true;
        UpdateTVSeriesLogos();
        UpdateMyFilmsLogos();
      }
      #endregion

      if (requiresRestart)
      {
        StreamedMPConfig.ShowRestartMessage(GetID, Translation.MiscMenu);
      }
      
    }
    #endregion

    #region Base Overrides
    public override int GetID
    {
      get
      { 
        return (int)StreamedMPConfig.SMPScreenID.SMPMiscSettings; 
      }    
    }

    public override bool Init()
    {      
      return Load(GUIGraphicsContext.Skin + @"\StreamedMPConfig_misc.xml");
    }

    protected override void OnPageLoad()
    {
      // Load Settings
      settings.Load(settings.cXMLSectionMisc);

      // Update Controls
      SetControlStates();

      // Temp Values
      UnfocusedAlphaListTemp = MiscConfigGUI.UnfocusedAlphaListItems.ToString();
      UnfocusedAlphaThumbsTemp = MiscConfigGUI.UnfocusedAlphaThumbs.ToString();

      TextColorTemp = MiscConfigGUI.TextColor;
      TextColor2Temp = MiscConfigGUI.TextColor2;
      TextColor3Temp = MiscConfigGUI.TextColor3;
      WatchedColorTemp = MiscConfigGUI.WatchedColor;
      RemoteColorTemp = MiscConfigGUI.RemoteColor;

      TextualLogosTemp = MiscConfigGUI.TextualLogos;

      base.OnPageLoad();
    }

    protected override void OnPageDestroy(int newWindowId)
    {      
      // Get Current State of controls
      GetControlStates();
 
      // Apply Configuration Changes
      ApplyConfigurationChanges();

      // Save Settings
      settings.Save(settings.cXMLSectionMisc);

      base.OnPageDestroy(GetID);
    }

    protected override void OnClicked(int controlId, GUIControl control, Action.ActionType actionType)
    {
      switch (controlId)
      {
        case (int)GUIControls.UnfocusedAlpha:
          ShowUnfocusedAlphaMenu();
          break;

        case (int)GUIControls.ListColors:
          ShowListColorsMenu();
          break;
      }
      base.OnClicked(controlId, control, actionType);
    }
    #endregion
  }
}
