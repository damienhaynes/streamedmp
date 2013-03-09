using System;
using System.Collections;
using MediaPortal.GUI.Library;


namespace StreamedMPConfig
{
  public class VideoOptionsGUI : GUIWindow
  {
    private static readonly logger smcLog = logger.GetInstance();

    #region Skin Connection

    private enum GUIControls
    {
      VideoMinOSD = 2
    }

    [SkinControl((int)GUIControls.VideoMinOSD)] protected GUIToggleButtonControl cmc_MinOSD = null;    

    #endregion

    #region Public Properties

    public static bool FullVideoOSD { get; set; }

    #endregion

    #region Public methods

    public VideoOptionsGUI()
    {
    }
    
    public static void SetProperties()
    {
      smcLog.WriteLog("StreamedMPConfig: Setting #StreamedMP.fullVideoOSD = " + FullVideoOSD.ToString().ToLower(), LogLevel.Debug);
      GUIPropertyManager.SetProperty("#StreamedMP.fullVideoOSD", FullVideoOSD.ToString().ToLower());
    }

    #endregion

    #region Base overrides

    public override int GetID
    {
      get
      {
        return (int)StreamedMPConfig.SMPScreenID.SMPVideoSettings;
      }
    }

    public override bool Init()
    {
      return Load(GUIGraphicsContext.Skin + @"\StreamedMPConfig_video.xml");
    }

    protected override void OnPageLoad()
    {
      settings.Load(settings.cXMLSectionVideo);  
      cmc_MinOSD.Selected = !FullVideoOSD;
      cmc_MinOSD.Label = Translation.MinVideoOSD;

      base.OnPageLoad();
    }

    protected override void OnPageDestroy(int new_windowId)
    {
      FullVideoOSD = !cmc_MinOSD.Selected;
      SetProperties();
      settings.Save(settings.cXMLSectionVideo);

      base.OnPageDestroy(GetID);
    }
    #endregion
  }
}
