using System;
using System.Collections;
using MediaPortal.GUI.Library;


namespace StreamedMPConfig
{
  public class VideoOptionsGUI : GUIWindow
  {
    #region Skin Connection

    [SkinControl((int)StreamedMPConfig.SkinControlIDs.cmc_MinOSD)] protected GUIToggleButtonControl cmc_MinOSD = null;


    public override int GetID
    {
      get
      {
        return (int)StreamedMPConfig.SMPScreenID.SMPVideoSettings;
      }
      set
      {
      }
    }

    #endregion

    #region Public methods

    public VideoOptionsGUI()
    {
    }
    
    #endregion

    #region Base overrides

    public override bool Init()
    {
      return Load(GUIGraphicsContext.Skin + @"\StreamedMPConfig_video.xml");
    }

    protected override void OnPageLoad()
    {
      settings.Load();
      if (StreamedMPConfig.fullVideoOSD)
        cmc_MinOSD.Selected = false;
      else
        cmc_MinOSD.Selected = true;
      // Load Translations
      GUIControl.SetControlLabel(GetID, 2, Translation.Strings["MinVideoOSD"]);
    }

    protected override void OnPageDestroy(int new_windowId)
    {
      if (cmc_MinOSD.Selected)
        StreamedMPConfig.fullVideoOSD = false;
      else
        StreamedMPConfig.fullVideoOSD = true;
      settings.Save();
    }
    #endregion
  }
}
