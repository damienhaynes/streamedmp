using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
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
      UnfocusedAlpha = 7
    }  
    #endregion

    #region Local Variables
    private static readonly logger smcLog = logger.GetInstance();

    private string UnfocusedAlphaListTemp;
    private string UnfocusedAlphaThumbsTemp;
    #endregion

    #region Skin Controls
    [SkinControl((int)GUIControls.HiddenMenuImage)]
    protected GUIToggleButtonControl btnHiddenMenuImage = null;

    [SkinControl((int)GUIControls.RoundedImages)]
    protected GUIToggleButtonControl btnRoundedImages = null;

    [SkinControl((int)GUIControls.IconsInArtwork)]
    protected GUIToggleButtonControl btnIconsInArtwork = null;

    [SkinControl((int)GUIControls.PlayRecents)]
    protected GUIToggleButtonControl btnPlayRecents = null;

    [SkinControl((int)GUIControls.FilterWatchedRecents)]
    protected GUIToggleButtonControl btnFilterWatchedRecents = null;

    [SkinControl((int)GUIControls.UnfocusedAlpha)]
    protected GUIButtonControl btnUnfocusedAlpha = null;
    #endregion

    #region Constructor
    public MiscConfigGUI() {}
    #endregion

    #region Public Properties
    public static bool ShowHiddenMenuImage { get; set; }
    public static bool ShowRoundedImages { get; set; }
    public static bool ShowIconsInArtwork { get; set; }
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
    #endregion

    #region Public Methods
    public static void SetProperties()
    {
      StreamedMPConfig.SetProperty("#StreamedMP.ActionMenu.Image", MiscConfigGUI.ShowHiddenMenuImage ? "Action_menu.png" : "-");
      StreamedMPConfig.SetProperty("#StreamedMP.RoundedPosters.Image", MiscConfigGUI.ShowRoundedImages ? "round.poster.frame.png" : "-");
      // Icons in Artwork require a few image properties
      StreamedMPConfig.SetProperty("#StreamedMP.Icons.Background", MiscConfigGUI.ShowIconsInArtwork ? "overlaywubg.png" : "-");
      StreamedMPConfig.SetProperty("#StreamedMP.Icons.Watched", MiscConfigGUI.ShowIconsInArtwork ? "overlaywatched.png" : "-");
      StreamedMPConfig.SetProperty("#StreamedMP.Icons.UnWatched", MiscConfigGUI.ShowIconsInArtwork ? "overlayunwatched.png" : "-");
      StreamedMPConfig.SetProperty("#StreamedMP.Icons.NAWatched", MiscConfigGUI.ShowIconsInArtwork ? "overlaywatchedna.png" : "-");
      StreamedMPConfig.SetProperty("#StreamedMP.Icons.NAUnWatched", MiscConfigGUI.ShowIconsInArtwork ? "overlayunwatchedna.png" : "-");
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
      dlg.DoModal(GetID);
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

      // Play Most Recents
      btnPlayRecents.Selected = EnablePlayMostRecents;
      btnPlayRecents.Label = Translation.PlayMostRecents;

      // Filter Watched Episodes From RecentlyAdded
      btnFilterWatchedRecents.Selected = FilterWatchedInRecentlyAdded;
      btnFilterWatchedRecents.Label = Translation.FilterWatchedRecents;

      // Unfocused Alpha Settings
      btnUnfocusedAlpha.Label = string.Format(Translation.UnfocusedAlpha, "...");
    }

    private void GetControlStates()
    {
      ShowHiddenMenuImage = btnHiddenMenuImage.Selected;
      ShowRoundedImages = btnRoundedImages.Selected;
      ShowIconsInArtwork = btnIconsInArtwork.Selected;
      EnablePlayMostRecents = btnPlayRecents.Selected;
      
      // Update BasicHome RecentlyAdded with new setting
      if (FilterWatchedInRecentlyAdded != btnFilterWatchedRecents.Selected)
      {
        FilterWatchedInRecentlyAdded = btnFilterWatchedRecents.Selected;
        StreamedMPConfig.getLastThreeAddedTVSeries();
        StreamedMPConfig.getLastThreeAddedMovies();
      }
    }

    /// <summary>
    /// Apply changes to Skin
    /// </summary>
    private void ApplyConfigurationChanges()
    {
      bool requiresRestart = false;

      SetProperties();

      #region Unfocused Alpha
      if (MiscConfigGUI.UnfocusedAlphaListItems != int.Parse(UnfocusedAlphaListTemp)) requiresRestart = true;
      if (MiscConfigGUI.UnfocusedAlphaThumbs != int.Parse(UnfocusedAlphaThumbsTemp)) requiresRestart = true;
      MiscConfigGUI.UnfocusedAlphaListItems = int.Parse(UnfocusedAlphaListTemp);
      MiscConfigGUI.UnfocusedAlphaThumbs = int.Parse(UnfocusedAlphaThumbsTemp);
      SetAlphaTransparency();
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
    }

    protected override void OnPageDestroy(int newWindowId)
    {      
      // Get Current State of controls
      GetControlStates();
 
      // Apply Configuration Changes
      ApplyConfigurationChanges();

      // Save Settings
      settings.Save(settings.cXMLSectionMisc);
    }

    protected override void OnClicked(int controlId, GUIControl control, Action.ActionType actionType)
    {
      switch (controlId)
      {
        case (int)GUIControls.UnfocusedAlpha:
          ShowUnfocusedAlphaMenu();
          break;
      }
      base.OnClicked(controlId, control, actionType);
    }
    #endregion
  }
}
