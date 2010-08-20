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
      RoundedImages = 3
    }  
    #endregion

    #region Skin Controls
    [SkinControl((int)GUIControls.HiddenMenuImage)]
    protected GUIToggleButtonControl btnHiddenMenuImage = null;

    [SkinControl((int)GUIControls.RoundedImages)]
    protected GUIToggleButtonControl btnRoundedImages = null;
    #endregion

    #region Constructor
    public MiscConfigGUI() { }
    #endregion

    #region Public Properties
    public static bool ShowHiddenMenuImage { get; set; }
    public static bool ShowRoundedImages { get; set; }
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
    }

    private void GetControlStates()
    {
      ShowHiddenMenuImage = btnHiddenMenuImage.Selected;
      ShowRoundedImages = btnRoundedImages.Selected;
    }

    /// <summary>
    /// Apply changes to Skin
    /// </summary>
    private void ApplyConfigurationChanges()
    {
      StreamedMPConfig.SetProperty("#StreamedMP.ActionMenu.Image", MiscConfigGUI.ShowHiddenMenuImage ? "Action_menu.png" : "-");
      StreamedMPConfig.SetProperty("#StreamedMP.RoundedPosters.Image", MiscConfigGUI.ShowRoundedImages ? "round.poster.frame.png" : "-");
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
      settings.Load("Misc");

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
      settings.Save("Misc");
    }
    #endregion
  }
}
