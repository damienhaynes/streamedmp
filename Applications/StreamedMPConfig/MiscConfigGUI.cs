using System;
using System.Collections.Generic;
using System.Text;
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
      IconsInArtwork = 4
    }  
    #endregion

    #region Skin Controls
    [SkinControl((int)GUIControls.HiddenMenuImage)]
    protected GUIToggleButtonControl btnHiddenMenuImage = null;

    [SkinControl((int)GUIControls.RoundedImages)]
    protected GUIToggleButtonControl btnRoundedImages = null;

    [SkinControl((int)GUIControls.IconsInArtwork)]
    protected GUIToggleButtonControl btnIconsInArtwork = null;
    #endregion

    #region Constructor
    public MiscConfigGUI() { }
    #endregion

    #region Public Properties
    public static bool ShowHiddenMenuImage { get; set; }
    public static bool ShowRoundedImages { get; set; }
    public static bool ShowIconsInArtwork { get; set; }
    public static int MostRecentFanartTimerInt { get; set; }
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
    }

    private void GetControlStates()
    {
      ShowHiddenMenuImage = btnHiddenMenuImage.Selected;
      ShowRoundedImages = btnRoundedImages.Selected;
      ShowIconsInArtwork = btnIconsInArtwork.Selected;
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
