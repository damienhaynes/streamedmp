using System;
using System.Reflection;
using MediaPortal.Configuration;
using MediaPortal.GUI.Library;
using System.Threading;

namespace StreamedMPConfig
{
  [PluginIcons("StreamedMPConfig.SMPSettings.png", "StreamedMPConfig.SMPSettingsDisabled.png")]
  public class StreamedMPConfig : GUIWindow, ISetupForm, IPlugin
  {
    #region public properties

    public static bool cdCoverOnly { get; set; }
    public static bool showEqGraphic { get; set; }
    public static bool fullVideoOSD { get; set; }
    public static bool checkOnStart { get; set; }
    public static bool checkForUpdateAt { get; set; }
    public static bool udateAvailable { get; set; }
    public static int checkInterval { get; set; }
    public static double hours { get; set; }
    public static string nextUpdateCheck { get; set; }
    public static DateTime checkTime { get; set; }
    public static Timer updateChkTimer;

    #endregion

    #region Skin Connection

    public enum SkinControlIDs
    {
      cmc_CDCover = 2,      // Music Options
      cmc_EqGfx = 3,        // Music Options
      cmc_MinOSD = 2,       // Video Options 
      cmc_ChangeLog = 2,    // Skin Update
      btDoUpdate = 3,       // Skin Update
      btMusicScreens = 40,  // Skin Settings Menu
      btVideoScreens = 41,  // Skin Settings Menu
      btMiscScreens = 42,   // Skin Settings Menu
      btSkinUpdate = 43     // Skin Settings Menu
    }

    public enum SMPScreenID
    {
      SMPMenu = 196200,
      SMPMusicSettings = 196201,
      SMPVideoSettings = 196202,
      SMPMiscSettings = 196203,
      SMPSkinUpdate = 196204
    }

    [SkinControl((int)SkinControlIDs.btMusicScreens)]
    protected GUIButtonControl btMusicScreens = null;
    [SkinControl((int)SkinControlIDs.btVideoScreens)]
    protected GUIButtonControl btVideoScreens = null;
    [SkinControl((int)SkinControlIDs.btMiscScreens)]
    protected GUIButtonControl btMiscScreens = null;
    [SkinControl((int)SkinControlIDs.btSkinUpdate)]
    protected GUIButtonControl btSkinUpdate = null;

    #endregion

    #region core functions

    public StreamedMPConfig()
    {
      GetID = GetWindowId();
    }

    public override bool Init()
    {
      Start();
      Log.Info("StreamedMPConfig GUI {0} starting.", Assembly.GetExecutingAssembly().GetName().Version);
      return Load(GUIGraphicsContext.Skin + @"\StreamedMPConfig.xml");

    }

    protected override void OnPageLoad()
    {
      settings.Load();
      SkinInfo.GetMediaPortalSkinPath();
    }

    protected override void OnPageDestroy(int new_windowId)
    {
      settings.Save();
    }

    internal static void SetProperty(string property, string value)
    {
      if (property == null)
        return;

      //// If the value is empty always add a space
      //// otherwise the property will keep 
      //// displaying it's previous value
      if (String.IsNullOrEmpty(value))
        value = " ";

      GUIPropertyManager.SetProperty(property, value);
    }

    protected override void OnClicked(int controlId, GUIControl control, Action.ActionType actionType)
    {

      if (control == btMusicScreens)
      {
        GUIWindowManager.ActivateWindow((int)SMPScreenID.SMPMusicSettings);
      }

      if (control == btVideoScreens)
      {
        GUIWindowManager.ActivateWindow((int)SMPScreenID.SMPVideoSettings);
      }

      if (control == btMiscScreens)
      {
        GUIWindowManager.ActivateWindow((int)SMPScreenID.SMPMiscSettings);
      }

      if (control == btSkinUpdate)
      {
        GUIWindowManager.ActivateWindow((int)SMPScreenID.SMPSkinUpdate);
      }
      base.OnClicked(controlId, control, actionType);
    }

    public static DateTime NextAt(TimeSpan time)
    {
      DateTime now = DateTime.Now;
      DateTime result = now.Date + time;

      return (now <= result) ? result : result.AddDays(1);
    }

    public void checkUpdateOnTimer(object source)
    {
      Log.Info("Update timer fired - checking for update");
      if (updateCheck.updateAvailable())
      {
        StreamedMPConfig.udateAvailable = true;

        // This property controls the visibility of the large update icon on the home screens
        StreamedMPConfig.SetProperty("#StreamedMP.UpdateAvailable", "true");

        // This property controls the visibility or the indicator icon displyed next to the clock on the home screens
        StreamedMPConfig.SetProperty("#StreamedMP.ShowUpdateInd", "true");

        // This property controls the visibility or the indicator icon that is displayed next to the skin item in the settings screens
        StreamedMPConfig.SetProperty("#StreamedMP.ShowSettingsUpdateInd", "true");
      }
      else
        StreamedMPConfig.udateAvailable = false;
    }

    public static TimeSpan nextCheckAt
    {
      get
      {
        switch (StreamedMPConfig.checkInterval)
        {
          case 1:
            StreamedMPConfig.hours = 24 * 7;
            break;
          case 2:
            StreamedMPConfig.hours = 24 * 14;
            break;
          case 3:
            StreamedMPConfig.hours = 24 * 28;
            break;
        }
        TimeSpan hoursToAdd = TimeSpan.FromHours(StreamedMPConfig.hours);

        DateTime nextCheck = NextAt(new TimeSpan(StreamedMPConfig.checkTime.Hour, 0, 0).Add(hoursToAdd));
        Log.Info("Next update check at : " + nextCheck.ToLongDateString() + ":" + nextCheck.ToLongTimeString());
        StreamedMPConfig.nextUpdateCheck = nextCheck.ToString();
        settings.Save();

        TimeSpan setTimerFor = (nextCheck - DateTime.Now).Add(hoursToAdd);
        Log.Debug("Timer Set for : " + setTimerFor.ToString());

        return setTimerFor;
      }
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


      Log.Debug("StreamedMPConfig: (" + windowID.ToString() + ") Setting all property valuse to initial values");

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

      if (StreamedMPConfig.udateAvailable)
      {
        // If we move off the basichome or Std menu turn off the the Update Available property so the main fade icon
        // is not displayed again
        // When leave the menu also turn off the update indicator displyed next to the clock as this may cause issues in other screens. 
        if (windowID != 35 && windowID != 0)
        {
          SetProperty("#StreamedMP.UpdateAvailable", "false");
          SetProperty("#StreamedMP.ShowUpdateInd", "false");
        }
        else
          SetProperty("#StreamedMP.ShowUpdateInd", "true");
      }
      else
      {
          Log.Debug("StreamedMPConfig: Setting all Indicator Values to false");
          SetProperty("#StreamedMP.UpdateAvailable", "false");
          SetProperty("#StreamedMP.ShowUpdateInd", "false");
          SetProperty("#StreamedMP.ShowSettingsUpdateInd", "false");
      }
    }

    #endregion

    #region <Interface> Implementations

    #region IPlugin Interface

    public void Start()
    {
      Log.Info("StreamedMPConfig Plugin {0} starting.", Assembly.GetExecutingAssembly().GetName().Version);

      string[] wordSplit;
      StreamedMPConfig.udateAvailable = false;
      SkinInfo.GetMediaPortalSkinPath();
      settings.Load();

      GUIWindowManager.OnActivateWindow += new GUIWindowManager.WindowActivationHandler(GUIWindowManager_OnActivateWindow);
      GUIGraphicsContext.OnVideoWindowChanged += new VideoWindowChangedHandler(GUIGraphicsContext_OnVideoWindowChanged);

      try
      {
        foreach (string name in Translation.Strings.Keys)
        {
          if (Translation.Strings[name] != "")
          {
            Log.Debug("Name : " + name + "  Translation : " + Translation.Strings[name]);
            if (name.Contains(":"))
            {
              wordSplit = name.Split(':');
              SetProperty("#StreamedMP." + wordSplit[1], Translation.Strings[name]);
            }
            else
              SetProperty("#StreamedMP." + name, Translation.Strings[name]);
          }
        }
      }
      catch
      {
        Log.Debug("StreamedMPConfig: Translation Exception");
      }

      // Shoud we check for an update on startup, if so check and set skin properties 
      // to control the visibility of the icons indicating and update is available?
      if (StreamedMPConfig.checkOnStart)
      {
        if (updateCheck.updateAvailable())
        {
          StreamedMPConfig.udateAvailable = true;

          // This property controls the visibility of the large update icon on the home screens
          SetProperty("#StreamedMP.UpdateAvailable", "true");

          // This property controls the visibility or the indicator icon displyed next to the clock on the home screens
          SetProperty("#StreamedMP.ShowUpdateInd", "true");

          // This property controls the visibility or the indicator icon that is displayed next to the skin item in the settings screens
          SetProperty("#StreamedMP.ShowSettingsUpdateInd", "true");
        }
      }

      // Should we check for an update at a specific time
      if (StreamedMPConfig.checkForUpdateAt)
      {
        DateTime stored;
        // Setup the callback
        TimerCallback updateCallback = new TimerCallback(checkUpdateOnTimer);

        // Compare the date strored (if any) with the current time
        try
        {
          stored = Convert.ToDateTime(StreamedMPConfig.nextUpdateCheck);
        }
        catch
        {
          stored = DateTime.Now;
        }

        if (stored.CompareTo(DateTime.Now) > 0)
        {
          //We have a save date for an update check that is in the future, we will use that.....
          TimeSpan setStored = stored - DateTime.Now;
          Log.Debug("Stored Timer Set for : " + setStored.ToString());
          updateChkTimer = new Timer(updateCallback, null, setStored, TimeSpan.FromHours(StreamedMPConfig.hours));
        }
        else
        {
          updateChkTimer = new Timer(updateCallback, null, nextCheckAt, TimeSpan.FromHours(StreamedMPConfig.hours));

          // The line below is for testing, comment the above line and uncomment this - the update check will fire 30sec after startup
          // updateChkTimer = new Timer(updateCallback, null, 30000, 60000);
        }
      }
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
      return false;
    }

    /// <summary>
    /// Get Windows-ID
    /// </summary>
    /// <returns>unique id for this plugin</returns>
    public int GetWindowId()
    {
      // WindowID of windowplugin belonging to this setup
      // enter your own unique code
      return (int)SMPScreenID.SMPMenu;
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