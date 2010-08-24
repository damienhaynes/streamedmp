using System;
using System.Collections.Generic;
using MediaPortal.GUI.Library;

namespace StreamedMPConfig
{
  public class TVConfig : GUIWindow
  {
    #region Enums
    private enum GUIControls
    {
      TVGuideSize = 2,
      TVMiniGuideSize= 3
    }

    public enum TVGuideRows
    {
      Eight = 8,
      Ten = 10,
      Twelve = 12
    }

    public enum TVMiniGuideRows
    {
      Three = 3,
      Five = 5,
      Seven = 7,
      Nine = 9
    }
    #endregion

    #region Skin Controls
    [SkinControl((int)GUIControls.TVGuideSize)]
    protected GUIButtonControl btnTVGuideSize = null;

    [SkinControl((int)GUIControls.TVMiniGuideSize)]
    protected GUIButtonControl btnTVMiniGuideSize = null;
    #endregion

    #region Constructor
    public TVConfig() { }
    #endregion

    #region Public Properties
    public static TVGuideRows TVGuideRowSize { get; set; }
    public static TVMiniGuideRows TVMiniGuideRowSize { get; set; }
    #endregion

    #region Private Methods
    private void SetControlStates()
    {
      // Set Label for Current TVGuide\TVMiniGuide Size
      btnTVGuideSize.Label = GetTVGuideSizeName(TVGuideRowSize);
      btnTVMiniGuideSize.Label = GetTVMiniGuideSizeName(TVMiniGuideRowSize);
    }

    private void GetControlStates() { }

    private void ShowTVGuideContextMenu()
    {
      IDialogbox dlg = (IDialogbox)GUIWindowManager.GetWindow((int)GUIWindow.Window.WINDOW_DIALOG_MENU);
      if (dlg == null) return;

      dlg.Reset();
      dlg.SetHeading(Translation.TVGuideSize);

      foreach (int value in Enum.GetValues(typeof(TVGuideRows)))
      {
        TVGuideRows rows = (TVGuideRows)Enum.Parse(typeof(TVGuideRows), value.ToString());
        string label = GetTVGuideSizeName(rows);

        // Create new item
        GUIListItem listItem = new GUIListItem(label);
        listItem.ItemId = value;

        // Set selected if current
        if (rows == TVGuideRowSize) listItem.Selected = true;

        // Add new item to context menu
        dlg.Add(listItem);
      }

      dlg.DoModal(GUIWindowManager.ActiveWindow);
      if (dlg.SelectedId <= 0) return;

      // Set new Selection
      TVGuideRowSize = (TVGuideRows)Enum.GetValues(typeof(TVGuideRows)).GetValue(dlg.SelectedLabel);
      btnTVGuideSize.Label = dlg.SelectedLabelText;
    }

    private void ShowTVMiniGuideContextMenu()
    {
      IDialogbox dlg = (IDialogbox)GUIWindowManager.GetWindow((int)GUIWindow.Window.WINDOW_DIALOG_MENU);
      if (dlg == null) return;

      dlg.Reset();
      dlg.SetHeading(Translation.TVMiniGuideSize);

      foreach (int value in Enum.GetValues(typeof(TVMiniGuideRows)))
      {
        TVMiniGuideRows rows = (TVMiniGuideRows)Enum.Parse(typeof(TVMiniGuideRows), value.ToString());
        string label = GetTVMiniGuideSizeName(rows);

        // Create new item
        GUIListItem listItem = new GUIListItem(label);
        listItem.ItemId = value;

        // Set selected if current
        if (rows == TVMiniGuideRowSize) listItem.Selected = true;

        // Add new item to context menu
        dlg.Add(listItem);
      }

      dlg.DoModal(GUIWindowManager.ActiveWindow);
      if (dlg.SelectedId <= 0) return;

      // Set new Selection
      TVMiniGuideRowSize = (TVMiniGuideRows)Enum.GetValues(typeof(TVMiniGuideRows)).GetValue(dlg.SelectedLabel);
      btnTVMiniGuideSize.Label = dlg.SelectedLabelText;
    }

    /// <summary>
    /// Returns a Translated name for selected TVGuide Size
    /// </summary>
    /// <param name="widebanner">TVGuideSize enum to translate</param>
    /// <returns>Translated Name</returns>
    private string GetTVGuideSizeName(TVGuideRows rows)
    {
      switch (rows)
      {
        case TVGuideRows.Eight:
          return Translation.TVGuideEightRows;

        case TVGuideRows.Ten:
          return Translation.TVGuideTenRows;

        case TVGuideRows.Twelve:
          return Translation.TVGuideTwelveRows;

        default:
          return Translation.TVGuideEightRows;
      }
    }

    private string GetTVMiniGuideSizeName(TVMiniGuideRows rows)
    {
      switch (rows)
      {
        case TVMiniGuideRows.Three:
          return Translation.TVMiniGuideThreeRows;

        case TVMiniGuideRows.Five:
          return Translation.TVMiniGuideFiveRows;

        case TVMiniGuideRows.Seven:
          return Translation.TVMiniGuideSevenRows;

        case TVMiniGuideRows.Nine:
          return Translation.TVMiniGuideNineRows;

        default:
          return Translation.TVMiniGuideSevenRows;
      }
    }

    /// <summary>
    /// Apply changes to TVGuide and TVMiniGuide XMLs
    /// </summary>
    private void ApplyConfigurationChanges()
    {
      // TVGuide Imports exist in mytvguide.xml and dialogTvGuide.xml
      string skinFile = GUIGraphicsContext.Skin + @"\mytvguide.xml";
      Helper.SetSkinImport(skinFile, "TVGuideChannelTemplate", string.Format("mytvguide.common.{0}rows.channeltemplate.xml", (int)TVGuideRowSize));

      skinFile = skinFile = GUIGraphicsContext.Skin + @"\dialogTvGuide.xml";
      Helper.SetSkinImport(skinFile, "TVGuideChannelTemplate", string.Format("mytvguide.common.{0}rows.channeltemplate.xml", (int)TVGuideRowSize));

      // TVMiniGuide Imports exist in TVMiniGuide.xml
      skinFile = skinFile = GUIGraphicsContext.Skin + @"\TVMiniGuide.xml";
      Helper.SetSkinImport(skinFile, "TVMiniGuide", string.Format("TVMiniGuide.{0}Rows.xml", (int)TVMiniGuideRowSize));
    }
    #endregion

    #region Base Overrides
    public override int GetID
    {
      get
      { 
        return (int)StreamedMPConfig.SMPScreenID.SMPTVConfig; 
      }    
    }

    public override bool Init()
    {      
      return Load(GUIGraphicsContext.Skin + @"\StreamedMPConfig_tv.xml");
    }

    protected override void OnPageLoad()
    {
      // Load Settings
      settings.Load(settings.cXMLSectionTV);

      // Update Controls
      SetControlStates();
    }

    protected override void OnPageDestroy(int newWindowId)
    {      
      // Get Current State of controls
      GetControlStates();
      
      // Apply Configuration changes
      ApplyConfigurationChanges();

      // Save Settings
      settings.Save(settings.cXMLSectionTV);
    }

    protected override void OnClicked(int controlId, GUIControl control, Action.ActionType actionType)
    {
      switch (controlId)
      {
        case (int)GUIControls.TVGuideSize:
          ShowTVGuideContextMenu();
          break;

        case (int)GUIControls.TVMiniGuideSize:
          ShowTVMiniGuideContextMenu();
          break;
      }
      base.OnClicked(controlId, control, actionType);
    }

    #endregion
  }
}
