﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using MediaPortal.GUI.Library;

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
      FilterWatchedRecents = 6
    }  
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
    #endregion

    #region Constructor
    public MiscConfigGUI() { }
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

      Helper.SetNodeText(file, "/controls/control[type='listcontrol']/textcolor", string.Concat("FF", MiscConfigGUI.TextColor));
      Helper.SetNodeText(file, "/controls/control[type='listcontrol']/textcolor2", string.Concat("FF", MiscConfigGUI.TextColor2));
      Helper.SetNodeText(file, "/controls/control[type='listcontrol']/textcolor3", string.Concat("FF", MiscConfigGUI.TextColor3));
      Helper.SetNodeText(file, "/controls/control[type='listcontrol']/playedColor", string.Concat("FF", MiscConfigGUI.WatchedColor));
      Helper.SetNodeText(file, "/controls/control[type='listcontrol']/remoteColor", string.Concat("FF", MiscConfigGUI.RemoteColor));

      Helper.SetNodeText(file, "/controls/control[type='playlistcontrol']/textcolor", string.Concat("FF", MiscConfigGUI.TextColor));
      Helper.SetNodeText(file, "/controls/control[type='playlistcontrol']/textcolor2", string.Concat("FF", MiscConfigGUI.TextColor2));
      Helper.SetNodeText(file, "/controls/control[type='playlistcontrol']/textcolor3", string.Concat("FF", MiscConfigGUI.TextColor3));
      Helper.SetNodeText(file, "/controls/control[type='playlistcontrol']/playedColor", string.Concat("FF", MiscConfigGUI.WatchedColor));
      Helper.SetNodeText(file, "/controls/control[type='playlistcontrol']/remoteColor", string.Concat("FF", MiscConfigGUI.RemoteColor));
    }

    public static void SetAlphaTransparency()
    {
      string file = GUIGraphicsContext.Skin + @"\references.xml";

      Helper.SetNodeText(file, "/controls/control[type='listcontrol']/unfocusedAlpha", MiscConfigGUI.UnfocusedAlphaListItems.ToString());
      Helper.SetNodeText(file, "/controls/control[type='playlistcontrol']/unfocusedAlpha", MiscConfigGUI.UnfocusedAlphaListItems.ToString());
      Helper.SetNodeText(file, "/controls/control[type='thumbnailpanel']/unfocusedAlpha", MiscConfigGUI.UnfocusedAlphaThumbs.ToString());
    }
    #endregion

    #region Private Methods
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
      SetProperties();
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
    #endregion
  }
}