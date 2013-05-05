using System;
using System.Collections.Generic;
using MediaPortal.GUI.Library;
using Action = MediaPortal.GUI.Library.Action;

namespace StreamedMPConfig
{
  public class TVSeriesConfig : GUIWindow
  {
    #region Enums
    private enum GUIControls
    {
      Style = 2,
      WideBannerMod = 3
    }

    public enum WideBanners
    {
      Default,
      FiveByTwoGridPoster,
      FiveByThreeGrid,
      SevenByThree,
      TenByFour
    }
    #endregion

    #region Local Variables
    private static readonly logger smcLog = logger.GetInstance();
    #endregion

    #region Skin Controls
    [SkinControl((int)GUIControls.Style)]
    protected GUICheckButton btnStyle = null;

    [SkinControl((int)GUIControls.WideBannerMod)]
    protected GUIButtonControl btnWideBannerMod = null;
    #endregion

    #region Constructor
    public TVSeriesConfig() { }
    #endregion

    #region Public Properties
    public static bool IsDefaultStyle { get; set; }
    public static WideBanners WideBannerMod { get; set; }
    #endregion

    #region Private Methods
    private void SetControlStates()
    {
      // Set Toggle Button if Default or Fanart Style
      btnStyle.Selected = !IsDefaultStyle;
      btnStyle.Label = Translation.FanartStyle;
      
      // Set Label for Current WideBanner Mod
      btnWideBannerMod.Label = GetWideBannerName(WideBannerMod);
    }

    private void GetControlStates()
    {
      // We already set Selected WideBanner Mod when changing

      // Get Default/Fanart Style
      IsDefaultStyle = !btnStyle.Selected;      
    }

    private void ShowWideBannerContextMenu()
    {
      IDialogbox dlg = (IDialogbox)GUIWindowManager.GetWindow((int)GUIWindow.Window.WINDOW_DIALOG_MENU);
      if (dlg == null) return;

      dlg.Reset();
      dlg.SetHeading(Translation.WideBanners);

      foreach (int value in Enum.GetValues(typeof(WideBanners)))
      {
        WideBanners banner = (WideBanners)Enum.Parse(typeof(WideBanners), value.ToString());
        string label = GetWideBannerName(banner);

        // Create new item
        GUIListItem listItem = new GUIListItem(label);
        listItem.ItemId = value;

        // Set selected if current
        if (banner == WideBannerMod) listItem.Selected = true;

        // Add new item to context menu
        dlg.Add(listItem);
      }

      dlg.DoModal(GUIWindowManager.ActiveWindow);
      if (dlg.SelectedId <= 0) return;

      // Set new Selection
      WideBannerMod = (WideBanners)Enum.GetValues(typeof(WideBanners)).GetValue(dlg.SelectedLabel);
      btnWideBannerMod.Label = dlg.SelectedLabelText;
    }

    /// <summary>
    /// Returns a Translated name for selected WideBanner Mod
    /// </summary>
    /// <param name="widebanner">Banner enum to translate</param>
    /// <returns>Translated Name</returns>
    private string GetWideBannerName(WideBanners widebanner)
    {
      switch (widebanner)
      {
        case WideBanners.Default:
          return Translation.WideBannerDefault;

        case WideBanners.FiveByTwoGridPoster:
          return Translation.WideBanner5x2Grid;

        case WideBanners.FiveByThreeGrid:
          return Translation.WideBanner5x3Grid;

        case WideBanners.SevenByThree:
          return Translation.WideBanner7x3Grid;

        case WideBanners.TenByFour:
          return Translation.WideBanner10x4Grid;

        default:
          return Translation.WideBannerDefault;          
      }
    }
   
    #endregion

    #region Public Methods

    /// <summary>
    /// Apply changes to TVSeries.xml
    /// </summary>
    public static void ApplyConfigurationChanges()
    {
      smcLog.WriteLog("Applying TVSeries Configuration Changes...", LogLevel.Info);

      string skinFile = GUIGraphicsContext.Skin + @"\TVSeries.xml";

      string style = IsDefaultStyle ? "Default" : "Fanart";
      smcLog.WriteLog(string.Format("TVSeries Chosen Style is: {0}", style), LogLevel.Info);

      // Set <import> paths
      Helper.SetSkinImport(skinFile, "SeriesAndSeasonListPosters", string.Format("TVSeries.{0}.SeriesAndSeasonListPosters.xml", style));
      Helper.SetSkinImport(skinFile, "SeriesAndSeasonFilmstrip", string.Format("TVSeries.{0}.SeriesAndSeasonFilmstrip.xml", style));
      Helper.SetSkinImport(skinFile, "SeriesListBanners", string.Format("TVSeries.{0}.SeriesListBanners.xml", style));
      Helper.SetSkinImport(skinFile, "EpisodeList", string.Format("TVSeries.{0}.EpisodeList.xml", style));
      Helper.SetSkinImport(skinFile, "GroupList", string.Format("TVSeries.{0}.GroupList.xml", style));

      smcLog.WriteLog(string.Format("TVSeries WideBanner MOD is: {0}", WideBannerMod.ToString()), LogLevel.Info);
      switch (WideBannerMod)
      {
        case WideBanners.Default:
          Helper.SetSkinImport(skinFile, "Facade", string.Format("TVSeries.{0}.Facade.xml", style));
          Helper.SetSkinImport(skinFile, "SeriesWideBanners", string.Format("TVSeries.{0}.SeriesWideBanners.xml", style));
          break;

        case WideBanners.FiveByTwoGridPoster:
          Helper.SetSkinImport(skinFile, "Facade", string.Format("TVSeries.{0}.WideBannerMod.5x2.Facade.xml", style));
          Helper.SetSkinImport(skinFile, "SeriesWideBanners", "TVSeries.Common.WideBannerMod.5x2.SeriesWideBanners.xml");
          break;

        case WideBanners.FiveByThreeGrid:
          Helper.SetSkinImport(skinFile, "Facade", string.Format("TVSeries.{0}.WideBannerMod.5x3.Facade.xml", style));
          Helper.SetSkinImport(skinFile, "SeriesWideBanners", "TVSeries.Common.WideBannerMod.5x3.SeriesWideBanners.xml");
          break;

        case WideBanners.SevenByThree:
          Helper.SetSkinImport(skinFile, "Facade", string.Format("TVSeries.{0}.WideBannerMod.7x3.Facade.xml", style));
          Helper.SetSkinImport(skinFile, "SeriesWideBanners", "TVSeries.Common.WideBannerMod.7x3.SeriesWideBanners.xml");
          break;

        case WideBanners.TenByFour:
          Helper.SetSkinImport(skinFile, "Facade", string.Format("TVSeries.{0}.WideBannerMod.10x4.Facade.xml", style));
          Helper.SetSkinImport(skinFile, "SeriesWideBanners", "TVSeries.Common.WideBannerMod.10x4.SeriesWideBanners.xml");
          break;

        default:
          Helper.SetSkinImport(skinFile, "Facade", string.Format("TVSeries.{0}.Facade.xml", style));
          Helper.SetSkinImport(skinFile, "SeriesWideBanners", string.Format("TVSeries.{0}.SeriesWideBanners.xml", style));
          break;
      }

    }
    #endregion

    #region Base Overrides
    public override int GetID
    {
      get
      { 
        return (int)StreamedMPConfig.SMPScreenID.SMPTVSeriesConfig; 
      }    
    }

    public override bool Init()
    {      
      return Load(GUIGraphicsContext.Skin + @"\StreamedMPConfig_tvseries.xml");
    }

    protected override void OnPageLoad()
    {
      // Load Settings
      settings.Load(settings.cXMLSectionTVSeries);

      // Update Controls
      SetControlStates();

      base.OnPageLoad();
    }

    protected override void OnPageDestroy(int newWindowId)
    {      
      // Get Current State of controls
      GetControlStates();
      
      // Apply Configuration changes
      ApplyConfigurationChanges();

      // Save Settings
      settings.Save(settings.cXMLSectionTVSeries);

      base.OnPageDestroy(GetID);
    }

    protected override void OnClicked(int controlId, GUIControl control, Action.ActionType actionType)
    {
      switch (controlId)
      {
        case (int)GUIControls.Style:
          break;

        case (int)GUIControls.WideBannerMod:
          ShowWideBannerContextMenu();
          break;
      }
      base.OnClicked(controlId, control, actionType);
    }

    #endregion

  }
}
