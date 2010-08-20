﻿using System;
using System.Collections.Generic;
using System.Text;
using MediaPortal.GUI.Library;

namespace StreamedMPConfig
{
  public class MovingPicturesConfigGUI : GUIWindow
  {
    #region Enums
    private enum GUIControls
    {
      Style = 2,
      ThumbnailMod = 3
    }

    public enum Thumbnails
    {
      Default,
      TwelveByFourAndEightByThreeGrid,
      TwelveByThreeAndEightByTwoGrid,    
    }
    #endregion

    #region Skin Controls
    [SkinControl((int)GUIControls.Style)]
    protected GUIToggleButtonControl btnStyle = null;

    [SkinControl((int)GUIControls.ThumbnailMod)]
    protected GUIButtonControl btnThumbnailMod = null;
    #endregion

    #region Constructor
    public MovingPicturesConfigGUI() { }
    #endregion

    #region Public Properties
    public static bool IsDefaultStyle { get; set; }
    public static Thumbnails ThumbnailMod { get; set; }
    #endregion

    #region Private Methods
    private void SetControlStates()
    {
      // Set Toggle Button if Default or Fanart Style
      btnStyle.Selected = !IsDefaultStyle;
      btnStyle.Label = Translation.FanartStyle;
      
      // Set Label for Current WideBanner Mod
      btnThumbnailMod.Label = GetThumbnailName(ThumbnailMod);
    }

    private void GetControlStates()
    {
      // We already set Selected Thumbnail Mod when changing

      // Get Default/Fanart Style
      IsDefaultStyle = !btnStyle.Selected;      
    }

    private void ShowThumbnailContextMenu()
    {
      IDialogbox dlg = (IDialogbox)GUIWindowManager.GetWindow((int)GUIWindow.Window.WINDOW_DIALOG_MENU);
      if (dlg == null) return;

      dlg.Reset();
      dlg.SetHeading(Translation.Thumbnails);

      foreach (int value in Enum.GetValues(typeof(Thumbnails)))
      {
        Thumbnails thumb = (Thumbnails)Enum.Parse(typeof(Thumbnails), value.ToString());
        string label = GetThumbnailName(thumb);

        // Create new item
        GUIListItem listItem = new GUIListItem(label);
        listItem.ItemId = value;

        // Set selected if current
        if (thumb == ThumbnailMod) listItem.Selected = true;

        // Add new item to context menu
        dlg.Add(listItem);
      }

      dlg.DoModal(GUIWindowManager.ActiveWindow);
      if (dlg.SelectedId <= 0) return;

      // Set new Selection
      ThumbnailMod = (Thumbnails)Enum.GetValues(typeof(Thumbnails)).GetValue(dlg.SelectedLabel);
      btnThumbnailMod.Label = dlg.SelectedLabelText;
    }

    /// <summary>
    /// Returns a Translated name for selected Thumbnail Mod
    /// </summary>
    /// <param name="widebanner">Thumbnail enum to translate</param>
    /// <returns>Translated Name</returns>
    private string GetThumbnailName(Thumbnails thumb)
    {
      switch (thumb)
      {
        case Thumbnails.Default:
          return Translation.ThumbnailDefault;

        case Thumbnails.TwelveByFourAndEightByThreeGrid:
          return Translation.Thumbnail_12x4_8x3_Grid;

        case Thumbnails.TwelveByThreeAndEightByTwoGrid:
          return Translation.Thumbnail_12x3_8x2_Grid;      

        default:
          return Translation.WideBannerDefault;          
      }
    }

    /// <summary>
    /// Apply changes to MovingPictures.xml
    /// </summary>
    private void ApplyConfigurationChanges()
    {
      string skinFile = GUIGraphicsContext.Skin + @"\MovingPictures.xml";

      string style = btnStyle.Selected ? "fanart" : "default";

      // Set <import> paths
      Helper.SetSkinImport(skinFile, "ListView", string.Format("movingpictures.{0}.listview.xml", style));
      Helper.SetSkinImport(skinFile, "ListViewMediaInfo", string.Format("movingpictures.{0}.listview.mediainfo.xml", style));
      Helper.SetSkinImport(skinFile, "FilmstripView", string.Format("movingpictures.{0}.filmstripview.xml", style));
      Helper.SetSkinImport(skinFile, "FilmstripViewMediaInfo", string.Format("movingpictures.{0}.filmstripview.mediainfo.xml", style));
      Helper.SetSkinImport(skinFile, "TopBar", string.Format("movingpictures.{0}.topbar.xml", style));

      switch (ThumbnailMod)
      {
        case Thumbnails.Default:
          Helper.SetSkinImport(skinFile, "Facade", string.Format("movingpictures.{0}.facade.xml", style));
          Helper.SetSkinImport(skinFile, "BackgroundOverlay", string.Format("movingpictures.{0}.background.overlays.xml", style));
          Helper.SetSkinImport(skinFile, "ThumbView", string.Format("movingpictures.{0}.thumbsview.xml", style));
          Helper.SetSkinImport(skinFile, "ThumbViewMediaInfo", string.Format("movingpictures.{0}.thumbsview.mediainfo.xml", style));
          break;

        case Thumbnails.TwelveByFourAndEightByThreeGrid:
          Helper.SetSkinImport(skinFile, "Facade", string.Format("movingpictures.{0}.thumbsview.12x4.8x3.facade.xml", style));
          Helper.SetSkinImport(skinFile, "BackgroundOverlay", string.Format("movingpictures.{0}.thumbsview.12x4.8x3.background.overlays.xml", style));
          Helper.SetSkinImport(skinFile, "ThumbView", "movingpictures.common.thumbsview.12x4.8x3.xml");
          Helper.SetSkinImport(skinFile, "ThumbViewMediaInfo", "movingpictures.common.thumbsview.12x4.8x3.mediainfo.xml");
          break;

        case Thumbnails.TwelveByThreeAndEightByTwoGrid:
          Helper.SetSkinImport(skinFile, "Facade", string.Format("movingpictures.{0}.thumbsview.12x3.8x2.facade.xml", style));
          Helper.SetSkinImport(skinFile, "BackgroundOverlay", string.Format("movingpictures.{0}.thumbsview.12x3.8x2.background.overlays.xml", style));
          Helper.SetSkinImport(skinFile, "ThumbView", "movingpictures.common.thumbsview.12x3.8x2.xml");
          Helper.SetSkinImport(skinFile, "ThumbViewMediaInfo", "movingpictures.common.thumbsview.12x3.8x2.mediainfo.xml");
          break;

        default:
          Helper.SetSkinImport(skinFile, "Facade", string.Format("movingpictures.{0}.facade.xml", style));
          Helper.SetSkinImport(skinFile, "BackgroundOverlay", string.Format("movingpictures.{0}.background.overlays.xml", style));
          Helper.SetSkinImport(skinFile, "ThumbView", string.Format("movingpictures.{0}.thumbsview.xml", style));
          Helper.SetSkinImport(skinFile, "ThumbViewMediaInfo", string.Format("movingpictures.{0}.thumbsview.mediainfo.xml", style));
          break;
      }

    }
    #endregion

    #region Base Overrides
    public override int GetID
    {
      get
      { 
        return (int)StreamedMPConfig.SMPScreenID.SMPMovPicsConfig; 
      }    
    }

    public override bool Init()
    {      
      return Load(GUIGraphicsContext.Skin + @"\StreamedMPConfig_movingpictures.xml");
    }

    protected override void OnPageLoad()
    {
      // Load Settings
      settings.Load("MovingPicturesConfigGUI");

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
      settings.Save("MovingPicturesConfigGUI");
    }

    protected override void OnClicked(int controlId, GUIControl control, Action.ActionType actionType)
    {
      switch (controlId)
      {
        case (int)GUIControls.Style:
          break;

        case (int)GUIControls.ThumbnailMod:
          ShowThumbnailContextMenu();
          break;
      }
      base.OnClicked(controlId, control, actionType);
    }

    #endregion
  }
}
