using System;
using System.Collections;
using MediaPortal.GUI.Library;
using System.IO;
using SMPCheckSum;


namespace StreamedMPConfig
{
  public class MusicOptionsGUI : GUIWindow
  {
    private static readonly logger smcLog = logger.GetInstance();
    int currentSyle = 0;

    #region Skin Connection

    public enum GUIControls
    {
      MusicCDCover = 2,          // Music Options
      MusicGFXeq = 3,            // Music Options
      Default = 4,
      FullscreenStyle1 = 5,
      FullScreenStyle2 = 6,
      EdgeFade = 7,
      EdgeNoFade = 8,
      EdgeNoMask = 9,
      WindowsFade = 10,
      WindowNofade = 11,
      WindowNoMask = 12
    }

    [SkinControl((int)GUIControls.MusicCDCover)] protected GUIToggleButtonControl MusicCDCover = null;
    [SkinControl((int)GUIControls.MusicGFXeq)] protected GUIToggleButtonControl MusicGFXeq = null;

    [SkinControl((int)GUIControls.Default)] protected GUICheckMarkControl defaultStyle = null;
    [SkinControl((int)GUIControls.FullscreenStyle1)] protected GUICheckMarkControl fullscreenStyle1 = null;
    [SkinControl((int)GUIControls.FullScreenStyle2)] protected GUICheckMarkControl fullscreenStyle2 = null;
    [SkinControl((int)GUIControls.EdgeFade)] protected GUICheckMarkControl edgeFadeStyle = null;
    [SkinControl((int)GUIControls.EdgeNoFade)] protected GUICheckMarkControl edgeNoFadeStyle = null;
    [SkinControl((int)GUIControls.EdgeNoMask)] protected GUICheckMarkControl edgeNoMaskStyle = null;
    [SkinControl((int)GUIControls.WindowsFade)] protected GUICheckMarkControl windowFadeStyle = null;
    [SkinControl((int)GUIControls.WindowNofade)] protected GUICheckMarkControl windowsNoFadeStyle = null;
    [SkinControl((int)GUIControls.WindowNoMask)] protected GUICheckMarkControl windowNoMaskStyle = null;

    #endregion

    #region Constructor

    public MusicOptionsGUI()
    {
    }

    #endregion

    #region Public methods

    public override bool Init()
    {
      return Load(GUIGraphicsContext.Skin + @"\StreamedMPConfig_music.xml");
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
      int iControl = message.TargetControlId;
      switch (message.Message)
      {
        case GUIMessage.MessageType.GUI_MSG_SETFOCUS:

          if (iControl == (int)defaultStyle.GetID)
            StreamedMPConfig.SetProperty("#StreamedMP.NowPlayingPreview", "npstyle0");

          if (iControl == (int)fullscreenStyle1.GetID)
            StreamedMPConfig.SetProperty("#StreamedMP.NowPlayingPreview", "npstyle1");

          if (iControl == (int)fullscreenStyle2.GetID)

            StreamedMPConfig.SetProperty("#StreamedMP.NowPlayingPreview", "npstyle2");
          if (iControl == (int)edgeFadeStyle.GetID)

            StreamedMPConfig.SetProperty("#StreamedMP.NowPlayingPreview", "npstyle3");
          if (iControl == (int)edgeNoFadeStyle.GetID)

            StreamedMPConfig.SetProperty("#StreamedMP.NowPlayingPreview", "npstyle4");
          if (iControl == (int)edgeNoMaskStyle.GetID)

            StreamedMPConfig.SetProperty("#StreamedMP.NowPlayingPreview", "npstyle5");
          if (iControl == (int)windowFadeStyle.GetID)

            StreamedMPConfig.SetProperty("#StreamedMP.NowPlayingPreview", "npstyle6");
          if (iControl == (int)windowsNoFadeStyle.GetID)

            StreamedMPConfig.SetProperty("#StreamedMP.NowPlayingPreview", "npstyle7");

          if (iControl == (int)windowNoMaskStyle.GetID)
            StreamedMPConfig.SetProperty("#StreamedMP.NowPlayingPreview", "npstyle8");

          break;
      }
      return base.OnMessage(message);
    }


    protected override void OnClicked(int controlId, GUIControl control, Action.ActionType actionType)
    {
      switch (controlId)
      {
        case (int)GUIControls.MusicCDCover:
          StreamedMPConfig.cdCoverOnly = MusicCDCover.Selected;
          break;
        case (int)GUIControls.MusicGFXeq:
          StreamedMPConfig.showEqGraphic = MusicGFXeq.Selected;
          break;
        default:
          {
            // Clear current selected value
            for (int i = 4; i < 13; i++)
            {
              if ((int)control.GetID != i)
                GUIControl.DeSelectControl(GetID, i);
            }
            StreamedMPConfig.nowPlayingStyle = control.GetID;
          }
          break;
      }
      base.OnClicked(controlId, control, actionType);
    }

    protected override void OnPageLoad()
    {

      settings.Load("MusicConfigGUI");
      MusicGFXeq.Selected = StreamedMPConfig.showEqGraphic;
      MusicCDCover.Selected = StreamedMPConfig.cdCoverOnly;

      // Load Translations
      GUIControl.SetControlLabel(GetID, (int)GUIControls.MusicCDCover, Translation.Strings["CDCover"]);
      GUIControl.SetControlLabel(GetID, (int)GUIControls.MusicGFXeq, Translation.Strings["ShowEQ"]);

      GUIControl.SetControlLabel(GetID, (int)GUIControls.Default, "Default");
      GUIControl.SetControlLabel(GetID, (int)GUIControls.FullscreenStyle1, "Fullscreen 1");
      GUIControl.SetControlLabel(GetID, (int)GUIControls.FullScreenStyle2, "Fullscreen 2");
      GUIControl.SetControlLabel(GetID, (int)GUIControls.EdgeFade, "Edge/Fade");
      GUIControl.SetControlLabel(GetID, (int)GUIControls.EdgeNoFade, "Edge/Nofade");
      GUIControl.SetControlLabel(GetID, (int)GUIControls.EdgeNoMask, "Edge Only");
      GUIControl.SetControlLabel(GetID, (int)GUIControls.WindowsFade, "Window/Fade");
      GUIControl.SetControlLabel(GetID, (int)GUIControls.WindowNofade, "Window/Nofade");
      GUIControl.SetControlLabel(GetID, (int)GUIControls.WindowNoMask, "Window Only");

      // If no setting then setup default Now Playing Screen
      if (StreamedMPConfig.nowPlayingStyle == 0)
      {
        GUIControl.SelectControl(GetID, (int)GUIControls.Default);
        StreamedMPConfig.SetProperty("#StreamedMP.NowPlayingPreview", "npstyle0");
        currentSyle = (int)GUIControls.Default;
      }
      else
      {
        GUIControl.SelectControl(GetID, StreamedMPConfig.nowPlayingStyle);
        StreamedMPConfig.SetProperty("#StreamedMP.NowPlayingPreview", "npstyle" + (StreamedMPConfig.nowPlayingStyle - 4).ToString());
        currentSyle = StreamedMPConfig.nowPlayingStyle;

      }
    }

    protected override void OnPageDestroy(int new_windowId)
    {
      CheckSum checkSum = new CheckSum();
      smcLog.WriteLog(string.Format("StreamedMPConfig: Settings.Save({0})", "MusicConfigGUI"), LogLevel.Info);

      StreamedMPConfig.showEqGraphic = MusicGFXeq.Selected;
      StreamedMPConfig.cdCoverOnly = MusicCDCover.Selected;

      if (StreamedMPConfig.nowPlayingStyle != currentSyle)
      {
        smcLog.WriteLog("StreamedMPConfig: Copy new Now Playing files", LogLevel.Info);
        // Copy the style files to the main skin directory
        string sourceFiles = Path.Combine(Path.Combine(SkinInfo.mpPaths.streamedMPpath, "NowPlayingScreens"), "style" + (StreamedMPConfig.nowPlayingStyle - 4).ToString());
        File.Copy(Path.Combine(sourceFiles, "MyMusicPlayingNow.xml"), Path.Combine(SkinInfo.mpPaths.streamedMPpath, "MyMusicPlayingNow.xml"), true);
        File.Copy(Path.Combine(sourceFiles, "MyMusicPlayingNowAnVU.xml"), Path.Combine(SkinInfo.mpPaths.streamedMPpath, "MyMusicPlayingNowAnVU.xml"), true);
        File.Copy(Path.Combine(sourceFiles, "MyMusicPlayingNowLedVU.xml"), Path.Combine(SkinInfo.mpPaths.streamedMPpath, "MyMusicPlayingNowLedVU.xml"), true);
        // Checksum them
        checkSum.Replace(Path.Combine(SkinInfo.mpPaths.streamedMPpath, "MyMusicPlayingNow.xml"));
        checkSum.Replace(Path.Combine(SkinInfo.mpPaths.streamedMPpath, "MyMusicPlayingNowAnVU.xml"));
        checkSum.Replace(Path.Combine(SkinInfo.mpPaths.streamedMPpath, "MyMusicPlayingNowLedVU.xml"));
      }
      else
        smcLog.WriteLog("No Changes Made to Now Playing", LogLevel.Info);

      settings.Save("MusicConfigGUI"); 
      
      GUIWindowManager.OnResize();
    }
    #endregion
  }
}
