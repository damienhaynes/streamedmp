using System;
using System.Collections;
using MediaPortal.GUI.Library;


namespace StreamedMPConfig
{
 public class MusicOptionsGUI : GUIWindow
  {
   private static readonly logger smcLog = logger.GetInstance();

 
   #region Skin Connection

   public enum GUIControls
   {
     style0 = 4,
     style1 = 5,
     style2 = 6,
     style3 = 7,
     style4 = 8,
     style5 = 9,
     style6 = 10,
     style7 = 11,
     style8 = 12
   }


    [SkinControl((int)StreamedMPConfig.SkinControlIDs.MusicCDCover)] protected GUIToggleButtonControl MusicCDCover = null;
    [SkinControl((int)StreamedMPConfig.SkinControlIDs.MusicGFXeq)] protected GUIToggleButtonControl MusicGFXeq = null;


    [SkinControl((int)GUIControls.style0)] protected GUIToggleButtonControl style0 = null;
    [SkinControl((int)GUIControls.style1)] protected GUIToggleButtonControl style1 = null;
    [SkinControl((int)GUIControls.style2)] protected GUIToggleButtonControl style2 = null;
    [SkinControl((int)GUIControls.style3)] protected GUIToggleButtonControl style3 = null;
    [SkinControl((int)GUIControls.style4)] protected GUIToggleButtonControl style4 = null;
    [SkinControl((int)GUIControls.style5)] protected GUIToggleButtonControl style5 = null;
    [SkinControl((int)GUIControls.style6)] protected GUIToggleButtonControl style6 = null;
    [SkinControl((int)GUIControls.style7)] protected GUIToggleButtonControl style7 = null;
    [SkinControl((int)GUIControls.style8)] protected GUIToggleButtonControl style8 = null;


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

    #region Public methods

    public override bool Init()
    {
      return Load(GUIGraphicsContext.Skin + @"\StreamedMPConfig_music.xml");
    }

    public MusicOptionsGUI()
    {
    }

    #endregion

    #region Base overrides


    public override bool OnMessage(GUIMessage message)
    {
      smcLog.WriteLog("Mesage is : " + message.Message.ToString() + " Ctrl: " + message.TargetControlId.ToString(), LogLevel.Info);

      switch (message.Message)
      {
        case GUIMessage.MessageType.GUI_MSG_SETFOCUS:
          int iControl = message.TargetControlId;



          if (iControl == (int)style0.GetID)
          {
            StreamedMPConfig.SetProperty("#StreamedMP.NowPlayingPreview", "npstyle0");
            smcLog.WriteLog("Setting #StreamedMP.NowPlayingPreview to npstyle0", LogLevel.Info);

          }

          if (iControl == (int)style1.GetID)
          {
            StreamedMPConfig.SetProperty("#StreamedMP.NowPlayingPreview", "npstyle1");
            smcLog.WriteLog("Setting #StreamedMP.NowPlayingPreview to npstyle1", LogLevel.Info);
          }

          if (iControl == (int)style2.GetID)
          {
            StreamedMPConfig.SetProperty("#StreamedMP.NowPlayingPreview", "npstyle2");
            smcLog.WriteLog("Setting #StreamedMP.NowPlayingPreview to npstyle2", LogLevel.Info);
          }

          break;

      }
      return base.OnMessage(message);
    }

    protected override void OnClicked(int controlId, GUIControl control, Action.ActionType actionType)
    {
      switch (controlId)
      {
        case (int)GUIControls.style0:
          break;
        case (int)GUIControls.style1:
          break;
        case (int)GUIControls.style2:
          break;
        case (int)GUIControls.style3:
          break;
        case (int)GUIControls.style4:
          break;
        case (int)GUIControls.style5:
          break;
        case (int)GUIControls.style6:
          break;
        case (int)GUIControls.style7:
          break;


      }
      base.OnClicked(controlId, control, actionType);
    }

    protected override void OnPageLoad()
    {
      settings.Load("StreamedMPConfig");
      MusicGFXeq.Selected = StreamedMPConfig.showEqGraphic;
      MusicCDCover.Selected = StreamedMPConfig.cdCoverOnly;

      // Load Translations
      GUIControl.SetControlLabel(GetID, 2, Translation.Strings["CDCover"]);
      GUIControl.SetControlLabel(GetID, 3, Translation.Strings["ShowEQ"]);

      GUIControl.SetControlLabel(GetID, 4, "Style 1");
      GUIControl.SetControlLabel(GetID, 5, "Style 2");
      GUIControl.SetControlLabel(GetID, 6, "Style 3");
      GUIControl.SetControlLabel(GetID, 7, "Style 4");
      GUIControl.SetControlLabel(GetID, 8, "Style 5");
      GUIControl.SetControlLabel(GetID, 9, "Style 6");
      GUIControl.SetControlLabel(GetID, 10, "Style 7");
      GUIControl.SetControlLabel(GetID, 11, "Style 8");
      GUIControl.SetControlLabel(GetID, 12, "Style 9");
    }

    protected override void OnPageDestroy(int new_windowId)
    {
      StreamedMPConfig.showEqGraphic = MusicGFXeq.Selected;
      StreamedMPConfig.cdCoverOnly = MusicCDCover.Selected;
      settings.Save("StreamedMPConfig");
    }

    #endregion
  }
}
