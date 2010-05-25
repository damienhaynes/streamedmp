using System;
using System.Collections;
using System.IO;
using MediaPortal.GUI.Library;


namespace StreamedMPConfig
{
  public class SkinUpdateGUI : GUIWindow
  {
    #region Skin Connection

    [SkinControl((int)StreamedMPConfig.SkinControlIDs.cmc_ChangeLog)] protected GUITextControl cmc_ChangeLog = null;
    [SkinControl((int)StreamedMPConfig.SkinControlIDs.btDoUpdate)] protected GUIButtonControl btDoUpdate = null;

    #endregion

    #region Core Functions

    public override int GetID
    {
      get
      {
        return (int)StreamedMPConfig.SMPScreenID.SMPSkinUpdate;
      }
      set
      {
      }
    }
    public override bool Init()
    {
      return Load(GUIGraphicsContext.Skin + @"\StreamedMPConfig_update.xml");
    }

    public SkinUpdateGUI()
    {
      settings.Load();
    }

    protected override void OnPageLoad()
    {
      settings.Load();

      System.Windows.Forms.RichTextBox rtBox = new System.Windows.Forms.RichTextBox();
      string s = System.IO.File.ReadAllText(Path.Combine(Path.GetTempPath(), "ChangeLog.rtf"));
      rtBox.Rtf = s;
      string plainText = rtBox.Text;
      cmc_ChangeLog.Label = plainText.ToString();
    }

    protected override void OnPageDestroy(int new_windowId)
    {
      settings.Save();
    }

    #endregion
  }
}
