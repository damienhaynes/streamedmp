using System;
using System.Collections;
using MediaPortal.GUI.Library;


namespace StreamedMPConfig
{
  public class SkinInfoGUI : GUIWindow
  {
    private static readonly logger smcLog = logger.GetInstance();

    #region Skin Connection

    private enum GUIControls
    {
      exitScreen  = 2
    }

    [SkinControl((int)GUIControls.exitScreen)]
    protected GUIButtonControl skinInfo_exit = null;

    #endregion

    #region Public Properties

    public static bool FullVideoOSD { get; set; }

    #endregion

    #region Public methods

    public SkinInfoGUI()
    {
    }

    public static void SetProperties()
    {

    }

    #endregion

    #region Base overrides

    public override int GetID
    {
      get
      {
        return (int)StreamedMPConfig.SMPScreenID.SMPSkinInfo;
      }
      set
      {
      }
    }

    public override bool Init()
    {
      return Load(GUIGraphicsContext.Skin + @"\MySkinInfo.xml");
    }

    protected override void OnPageLoad()
    {

    }

    protected override void OnPageDestroy(int new_windowId)
    {

    }
    #endregion
  }
}
