using System;
using System.Collections;
using MediaPortal.GUI.Library;


namespace StreamedMPConfig
{
  public class MusicOptionsGUI : GUIWindow
  {
    #region Skin Connection


    [SkinControl((int)StreamedMPConfig.SkinControlIDs.cmc_CDCover)] protected GUIToggleButtonControl cmc_CDCover = null;
    [SkinControl((int)StreamedMPConfig.SkinControlIDs.cmc_EqGfx)] protected GUIToggleButtonControl cmc_EqGfx = null;

    
    public override int GetID
    {
      get
      {
        return (int)StreamedMPConfig.SMPScreenID.SMPMusicSettings;
      }
      set
      {
      }
    }

    #endregion

    #region Core Functions

    public override bool Init()
    {
      return Load(GUIGraphicsContext.Skin + @"\StreamedMPConfig_music.xml");
    }

    public MusicOptionsGUI()
    {
      settings.Load();
    }

    protected override void OnPageLoad()
    {
      settings.Load();
      cmc_EqGfx.Selected = StreamedMPConfig.showEqGraphic;
      cmc_CDCover.Selected = StreamedMPConfig.cdCoverOnly;
      // Load Translations
      GUIControl.SetControlLabel(GetID, 2, Translation.Strings["CDCover"]);
      GUIControl.SetControlLabel(GetID, 3, Translation.Strings["ShowEQ"]);
    }

    protected override void OnPageDestroy(int new_windowId)
    {
      StreamedMPConfig.showEqGraphic = cmc_EqGfx.Selected;
      StreamedMPConfig.cdCoverOnly = cmc_CDCover.Selected;
    }

    #endregion

  }
}
