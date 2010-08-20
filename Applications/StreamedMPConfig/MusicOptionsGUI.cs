using System;
using System.Collections;
using MediaPortal.GUI.Library;
using System.IO;


namespace StreamedMPConfig
{
  public class MusicOptionsGUI : GUIWindow
  {
    private static readonly logger smcLog = logger.GetInstance();

    #region Skin Connection

    public enum GUIControls
    {
      MusicCDCover = 2,          // Music Options
      MusicGFXeq = 3,            // Music Options
      nowPlayingStyle0 = 4,
      nowPlayingStyle1 = 5,
      nowPlayingStyle2 = 6,
      nowPlayingStyle3 = 7,
      nowPlayingStyle4 = 8,
      nowPlayingStyle5 = 9,
      nowPlayingStyle6 = 10,
      nowPlayingStyle7 = 11,
      nowPlayingStyle8 = 12
    }


    [SkinControl((int)GUIControls.MusicCDCover)] protected GUIToggleButtonControl MusicCDCover = null;
    [SkinControl((int)GUIControls.MusicGFXeq)] protected GUIToggleButtonControl MusicGFXeq = null;

    [SkinControl((int)GUIControls.nowPlayingStyle0)] protected GUICheckMarkControl nowPlayingStyle0 = null;
    [SkinControl((int)GUIControls.nowPlayingStyle1)] protected GUICheckMarkControl nowPlayingStyle1 = null;
    [SkinControl((int)GUIControls.nowPlayingStyle2)] protected GUICheckMarkControl nowPlayingStyle2 = null;
    [SkinControl((int)GUIControls.nowPlayingStyle3)] protected GUICheckMarkControl nowPlayingStyle3 = null;
    [SkinControl((int)GUIControls.nowPlayingStyle4)] protected GUICheckMarkControl nowPlayingStyle4 = null;
    [SkinControl((int)GUIControls.nowPlayingStyle5)] protected GUICheckMarkControl nowPlayingStyle5 = null;
    [SkinControl((int)GUIControls.nowPlayingStyle6)] protected GUICheckMarkControl nowPlayingStyle6 = null;
    [SkinControl((int)GUIControls.nowPlayingStyle7)] protected GUICheckMarkControl nowPlayingStyle7 = null;
    [SkinControl((int)GUIControls.nowPlayingStyle8)] protected GUICheckMarkControl nowPlayingStyle8 = null;


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

    public override int GetID
    {
      get
      {
        return (int)StreamedMPConfig.SMPScreenID.SMPMusicSettings;
      }
      set { }
    }

    public override bool OnMessage(GUIMessage message)
    {
      smcLog.WriteLog("Message: " + message.Message.ToString() + " Control: " + message.TargetControlId.ToString(), LogLevel.Info);
      int iControl = message.TargetControlId;
      switch (message.Message)
      {
        case GUIMessage.MessageType.GUI_MSG_SETFOCUS:


          if (iControl == (int)nowPlayingStyle0.GetID)
            StreamedMPConfig.SetProperty("#StreamedMP.NowPlayingPreview", "npstyle0");

          if (iControl == (int)nowPlayingStyle1.GetID)
            StreamedMPConfig.SetProperty("#StreamedMP.NowPlayingPreview", "npstyle1");

          if (iControl == (int)nowPlayingStyle2.GetID)

            StreamedMPConfig.SetProperty("#StreamedMP.NowPlayingPreview", "npstyle2");
          if (iControl == (int)nowPlayingStyle3.GetID)

            StreamedMPConfig.SetProperty("#StreamedMP.NowPlayingPreview", "npstyle3");
          if (iControl == (int)nowPlayingStyle4.GetID)

            StreamedMPConfig.SetProperty("#StreamedMP.NowPlayingPreview", "npstyle4");
          if (iControl == (int)nowPlayingStyle5.GetID)

            StreamedMPConfig.SetProperty("#StreamedMP.NowPlayingPreview", "npstyle5");
          if (iControl == (int)nowPlayingStyle6.GetID)

            StreamedMPConfig.SetProperty("#StreamedMP.NowPlayingPreview", "npstyle6");
          if (iControl == (int)nowPlayingStyle7.GetID)

            StreamedMPConfig.SetProperty("#StreamedMP.NowPlayingPreview", "npstyle7");

          if (iControl == (int)nowPlayingStyle8.GetID)
            StreamedMPConfig.SetProperty("#StreamedMP.NowPlayingPreview", "npstyle8");

          break;
      }
      return base.OnMessage(message);
    }


    protected override void OnClicked(int controlId, GUIControl control, Action.ActionType actionType)
    {
      // Clear current selected value
      for (int i = 4; i < 13; i++)
      {
        if ((int)control.GetID != i)
          GUIControl.DeSelectControl(GetID, i);
      }
      StreamedMPConfig.nowPlayingStyle = control.GetID;
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

      GUIControl.SetControlLabel(GetID, 4, "Default");
      GUIControl.SetControlLabel(GetID, 5, "Fullscreen 1");
      GUIControl.SetControlLabel(GetID, 6, "Fullscreen 2");
      GUIControl.SetControlLabel(GetID, 7, "Edge/Fade");
      GUIControl.SetControlLabel(GetID, 8, "Edge/Nofade");
      GUIControl.SetControlLabel(GetID, 9, "Edge Only");
      GUIControl.SetControlLabel(GetID, 10, "Window/Fade");
      GUIControl.SetControlLabel(GetID, 11, "Window/Nofade");
      GUIControl.SetControlLabel(GetID, 12, "Window Only");
      if (StreamedMPConfig.nowPlayingStyle == 0)
      {
        GUIControl.SelectControl(GetID, (int)GUIControls.nowPlayingStyle0);
        StreamedMPConfig.SetProperty("#StreamedMP.NowPlayingPreview", "npstyle0");
      }
      else
      {
        GUIControl.SelectControl(GetID, StreamedMPConfig.nowPlayingStyle);
        StreamedMPConfig.SetProperty("#StreamedMP.NowPlayingPreview", "npstyle" + (StreamedMPConfig.nowPlayingStyle - 4).ToString());
      }
    }

    protected override void OnPageDestroy(int new_windowId)
    {
      StreamedMPConfig.showEqGraphic = MusicGFXeq.Selected;
      StreamedMPConfig.cdCoverOnly = MusicCDCover.Selected;
      string sourceFiles = Path.Combine(Path.Combine(SkinInfo.mpPaths.streamedMPpath, "NowPlayingScreens"), "style" + (StreamedMPConfig.nowPlayingStyle - 4).ToString());
      File.Copy(Path.Combine(sourceFiles, "MyMusicPlayingNow.xml"), Path.Combine(SkinInfo.mpPaths.streamedMPpath, "MyMusicPlayingNow.xml"), true);
      File.Copy(Path.Combine(sourceFiles, "MyMusicPlayingNowAnVU.xml"), Path.Combine(SkinInfo.mpPaths.streamedMPpath, "MyMusicPlayingNowAnVU.xml"), true);
      File.Copy(Path.Combine(sourceFiles, "MyMusicPlayingNowLedVU.xml"), Path.Combine(SkinInfo.mpPaths.streamedMPpath, "MyMusicPlayingNowLedVU.xml"), true);
      GUIWindowManager.OnResize();
      settings.Save("StreamedMPConfig");
    }
    #endregion
  }
}
