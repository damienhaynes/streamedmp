using System;
using System.Collections;
using MediaPortal.GUI.Library;
using Action = MediaPortal.GUI.Library.Action;
using System.IO;
using SMPCheckSum;


namespace StreamedMPConfig
{
  public class MusicOptionsGUI : GUIWindow
  {
    private static readonly logger smcLog = logger.GetInstance();
    private NowPlayingStyles currentStyle = NowPlayingStyles.Default;

    #region Skin Connection

    private enum GUIControls
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

    public enum NowPlayingStyles
    {
      Default,
      FullscreenStyle1,
      FullScreenStyle2,
      EdgeFade,
      EdgeNoFade,
      EdgeNoMask,
      WindowsFade,
      WindowNofade,
      WindowNoMask
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

    #region Public Properties
    public static NowPlayingStyles nowPlayingStyle { get; set; }
    public static bool cdCoverOnly { get; set; }
    public static bool showEqGraphic { get; set; }
    #endregion

    #region Public methods
    
    public static void SetProperties()
    { 
      smcLog.WriteLog("StreamedMPConfig: Setting #StreamedMP.cdCoverOnly = " + cdCoverOnly.ToString().ToLower(), LogLevel.Debug);
      GUIPropertyManager.SetProperty("#StreamedMP.cdCoverOnly", cdCoverOnly.ToString().ToLower());

      smcLog.WriteLog("StreamedMPConfig: Setting #StreamedMP.showEqGraphic = " + showEqGraphic.ToString().ToLower(), LogLevel.Debug);
      GUIPropertyManager.SetProperty("#StreamedMP.showEqGraphic", showEqGraphic.ToString().ToLower());
    }

    public static void SetMusicNowPlayingStyle()
    {
      CheckSum checkSum = new CheckSum();

      // Copy the style files to the main skin directory
      string sourceFiles = Path.Combine(Path.Combine(SkinInfo.mpPaths.streamedMPpath, "NowPlayingScreens"), "style" + ((int)nowPlayingStyle).ToString());
      File.Copy(Path.Combine(sourceFiles, "MyMusicPlayingNow.xml"), Path.Combine(SkinInfo.mpPaths.streamedMPpath, "MyMusicPlayingNow.xml"), true);
      File.Copy(Path.Combine(sourceFiles, "MyMusicPlayingNowAnVU.xml"), Path.Combine(SkinInfo.mpPaths.streamedMPpath, "MyMusicPlayingNowAnVU.xml"), true);
      File.Copy(Path.Combine(sourceFiles, "MyMusicPlayingNowLedVU.xml"), Path.Combine(SkinInfo.mpPaths.streamedMPpath, "MyMusicPlayingNowLedVU.xml"), true);
      // Checksum them
      checkSum.Replace(Path.Combine(SkinInfo.mpPaths.streamedMPpath, "MyMusicPlayingNow.xml"));
      checkSum.Replace(Path.Combine(SkinInfo.mpPaths.streamedMPpath, "MyMusicPlayingNowAnVU.xml"));
      checkSum.Replace(Path.Combine(SkinInfo.mpPaths.streamedMPpath, "MyMusicPlayingNowLedVU.xml"));
    }

    #endregion

    #region Base overrides

    public override bool Init()
    {
      return Load(GUIGraphicsContext.Skin + @"\StreamedMPConfig_music.xml");
    }

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

          if (iControl >= (int)GUIControls.Default && iControl <= (int)GUIControls.WindowNoMask)
            StreamedMPConfig.SetProperty("#StreamedMP.NowPlayingPreview", "npstyle" + (iControl - (int)GUIControls.Default).ToString());

          break;
      }
      return base.OnMessage(message);
    }


    protected override void OnClicked(int controlId, GUIControl control, Action.ActionType actionType)
    {
      switch (controlId)
      {
        case (int)GUIControls.MusicCDCover:
          cdCoverOnly = MusicCDCover.Selected;
          break;
        case (int)GUIControls.MusicGFXeq:
          showEqGraphic = MusicGFXeq.Selected;
          break;
        default:
          {
            // Clear current selected value
            for (int i = (int)GUIControls.Default; i <= (int)GUIControls.WindowNoMask; i++)
            {
              if ((int)control.GetID != i)
                GUIControl.DeSelectControl(GetID, i);
            }
            nowPlayingStyle = (NowPlayingStyles)(controlId - (int)GUIControls.Default);
          }
          break;
      }
      base.OnClicked(controlId, control, actionType);
    }

    protected override void OnPageLoad()
    {
      settings.Load(settings.cXMLSectionMusic);
      MusicGFXeq.Selected = showEqGraphic;
      MusicCDCover.Selected = cdCoverOnly;

      // Load Translations
      MusicCDCover.Label = Translation.CDCover;
      MusicGFXeq.Label = Translation.ShowEQ;

      defaultStyle.Label = "Default";
      fullscreenStyle1.Label = "Fullscreen 1";
      fullscreenStyle2.Label = "Fullscreen 2";
      edgeFadeStyle.Label = "Edge/Fade";
      edgeNoFadeStyle.Label = "Edge/Nofade";
      edgeNoMaskStyle.Label = "Edge Only";
      windowFadeStyle.Label = "Window/Fade";
      windowsNoFadeStyle.Label = "Window/Nofade";
      windowNoMaskStyle.Label = "Window Only";

      GUIControl.SelectControl(GetID, (int)nowPlayingStyle + (int)GUIControls.Default);
      StreamedMPConfig.SetProperty("#StreamedMP.NowPlayingPreview", "npstyle" + ((int)nowPlayingStyle).ToString());
      currentStyle = nowPlayingStyle;

      base.OnPageLoad();
    }

    protected override void OnPageDestroy(int new_windowId)
    {      
      smcLog.WriteLog(string.Format("StreamedMPConfig: Settings.Save({0})", settings.cXMLSectionMusic), LogLevel.Info);

      showEqGraphic = MusicGFXeq.Selected;
      cdCoverOnly = MusicCDCover.Selected;

      if (nowPlayingStyle != currentStyle)
      {
        smcLog.WriteLog("StreamedMPConfig: Copy new Now Playing files", LogLevel.Info);
        SetMusicNowPlayingStyle();
      }
      else
        smcLog.WriteLog("No Changes Made to Now Playing", LogLevel.Info);

      settings.Save(settings.cXMLSectionMusic);
      SetProperties();

      GUIWindowManager.OnResize();

      base.OnPageDestroy(GetID);
    }
    #endregion
  }
}
