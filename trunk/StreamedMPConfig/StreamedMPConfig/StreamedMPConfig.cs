using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Xml.Serialization;
using MediaPortal.Configuration;
using MediaPortal.Dialogs;
using MediaPortal.GUI.Library;
using MediaPortal.Player;
using MediaPortal.Util;

namespace StreamedMPConfig
{
  public class StreamedMPConfig : GUIWindow, ISetupForm, IPlugin
  {

    #region public properties

    public static bool cdCoverOnly { get; set; }
    public static bool showEqGraphic { get; set; }
    public static bool fullVideoOSD { get; set; }

    #endregion

    #region skin variables

    private enum SkinControlIDs
    {
      CMC_CDCOVER = 2,
      CMC_EQGFX = 3,
      CMC_FULLOSD = 4
    }

    [SkinControl((int)SkinControlIDs.CMC_CDCOVER)]
    protected GUICheckMarkControl cmc_cdcover = null;
    [SkinControl((int)SkinControlIDs.CMC_EQGFX)]
    protected GUICheckMarkControl cmc_eqgfx = null;
    [SkinControl((int)SkinControlIDs.CMC_FULLOSD)]
    protected GUICheckMarkControl cmc_fullOSD = null;

    #endregion

    #region core functions

    public override bool Init()
    {
      Start();
      Log.Info("StreamedMPConfig GUI {0} starting.", Assembly.GetExecutingAssembly().GetName().Version);
      return Load(GUIGraphicsContext.Skin + @"\StreamedMPConfig.xml");
    }

    protected override void OnPageLoad()
    {
      settings.Load();
      cmc_eqgfx.Selected = showEqGraphic;
      cmc_cdcover.Selected = cdCoverOnly;
      cmc_fullOSD.Selected = fullVideoOSD;
    }

    protected override void OnPageDestroy(int new_windowId)
    {
      showEqGraphic = cmc_eqgfx.Selected;
      cdCoverOnly = cmc_cdcover.Selected;
      fullVideoOSD = cmc_fullOSD.Selected;
      settings.Save();
    }

    #endregion

    #region EventHandlers

    private void GUIGraphicsContext_OnVideoWindowChanged()
    {
      Log.Debug("StreamedMPConfig: OnVideoWindowsChanged Event Called");

      if (GUIGraphicsContext.ShowBackground == true)
      {
        Log.Debug("#StreamedMP.VideoWallpaper = false");
        GUIPropertyManager.SetProperty("#StreamedMP.VideoWallpaper", "false");
      }
      else
      {
        Log.Debug("#StreamedMP.VideoWallpaper = true");
        GUIPropertyManager.SetProperty("#StreamedMP.VideoWallpaper", "true");
      }
    }

    private void GUIWindowManager_OnActivateWindow(int windowID)
    {

      // TO DO: Tie the property to the skin file it is used in against setting for all.

      Log.Debug("StreamedMPConfig: Setting all property valuse to initial values");

      GUIPropertyManager.SetProperty("#StreamedMP.cdCoverOnly", "false");
      GUIPropertyManager.SetProperty("#StreamedMP.showEqGraphic", "false");
      GUIPropertyManager.SetProperty("#StreamedMP.fullVideoOSD", "true");

      if (cdCoverOnly)
      {
        Log.Debug("StreamedMPConfig: Setting #StreamedMP.cdCoverOnly = true");
        GUIPropertyManager.SetProperty("#StreamedMP.cdCoverOnly", "true");
      }

      if (showEqGraphic)
      {
        Log.Debug("StreamedMPConfig: Setting #StreamedMP.showEqGraphic = true");
        GUIPropertyManager.SetProperty("#StreamedMP.showEqGraphic", "true");
      }
      if (!fullVideoOSD)
      {
        Log.Debug("StreamedMPConfig: Setting #StreamedMP.fullVideoOSD = false");
        GUIPropertyManager.SetProperty("#StreamedMP.fullVideoOSD", "false");
      }
      else
      {
        Log.Debug("StreamedMPConfig: Setting #StreamedMP.fullVideoOSD = true");
        GUIPropertyManager.SetProperty("#StreamedMP.fullVideoOSD", "true");
      }

    }

    #endregion

    #region <Interface> Implementations

    #region IPlugin Interface

    public void Start()
    {

      Log.Info("StreamedMPConfig Plugin {0} starting.", Assembly.GetExecutingAssembly().GetName().Version);

      GUIWindowManager.OnActivateWindow += new GUIWindowManager.WindowActivationHandler(GUIWindowManager_OnActivateWindow);
      GUIGraphicsContext.OnVideoWindowChanged += new VideoWindowChangedHandler(GUIGraphicsContext_OnVideoWindowChanged);

      settings.Load();


    }
    public void Stop()
    {
      GUIWindowManager.OnActivateWindow -= new GUIWindowManager.WindowActivationHandler(GUIWindowManager_OnActivateWindow);
      GUIGraphicsContext.OnVideoWindowChanged -= new VideoWindowChangedHandler(GUIGraphicsContext_OnVideoWindowChanged);
    }

    #endregion


    #region ISetupForm Members

    /// <summary>
    /// With GetID it will be an window-plugin / otherwise a process-plugin
    /// Enter the id number here again
    /// </summary>
    public override int GetID
    {
      get { return GetWindowId(); }
      set { }
    }

    /// <summary>
    /// Returns the name of the plugin which is shown in the plugin menu
    /// </summary>
    /// <returns>the name of the plugin which is shown in the plugin menu</returns>
    public string PluginName()
    {
      return "StreamedMP Configuration";
    }

    /// <summary>
    /// Returns the description of the plugin which is shown in the plugin menu
    /// </summary>
    /// <returns>the description of the plugin which is shown in the plugin menu</returns>
    public string Description()
    {
      return "StreamedMP configuration control";
    }

    /// <summary>
    /// Returns the author of the plugin which is shown in the plugin menu
    /// </summary>
    /// <returns>the author of the plugin which is shown in the plugin menu</returns>
    public string Author()
    {
      return "trevor";
    }

    /// <summary>
    /// Indicates whether plugin can be enabled/disabled
    /// </summary>
    public void ShowPlugin()
    {
      ConfigurationForm configurationForm = new ConfigurationForm();
      configurationForm.ShowDialog();
    }

    /// <summary>
    /// Indicates whether plugin can be enabled/disabled
    /// </summary>
    /// <returns>true if the plugin can be enabled/disabled</returns>
    public bool CanEnable()
    {
      return true;
    }

    /// <summary>
    /// Get Windows-ID
    /// </summary>
    /// <returns>unique id for this plugin</returns>
    public int GetWindowId()
    {
      // WindowID of windowplugin belonging to this setup
      // enter your own unique code
      return 1962;
    }

    /// <summary>
    /// Indicates if plugin is enabled by default
    /// </summary>
    /// <returns>true if this plugin is enabled by default</returns>
    public bool DefaultEnabled()
    {
      return true;
    }

    /// <summary>
    /// indicates if a plugin has its own setup screen
    /// </summary>
    /// <returns>true if the plugin has its own setup screen</returns>
    public bool HasSetup()
    {
      return true;
    }

    /// <summary>
    /// no Home for this plugin, return false
    /// </summary>
    /// <param name="strButtonText"></param>
    /// <param name="strButtonImage"></param>
    /// <param name="strButtonImageFocus"></param>
    /// <param name="strPictureImage"></param>
    /// <returns></returns>
    public bool GetHome(out string strButtonText, out string strButtonImage,
                        out string strButtonImageFocus, out string strPictureImage)
    {
      strButtonText = String.Empty;
      strButtonImage = String.Empty;
      strButtonImageFocus = String.Empty;
      strPictureImage = String.Empty;
      return false;
    }


    #endregion

    #endregion

  }
}
