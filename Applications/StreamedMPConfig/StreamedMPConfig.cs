using System;
using System.Collections.Generic;
using System.Reflection;
using MediaPortal.Configuration;
using MediaPortal.GUI.Library;
using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using MediaPortal.Dialogs;
using Cornerstone.Tools;
using Cornerstone.Database;
using Cornerstone.Database.Tables;
using MediaPortal.Plugins.MovingPictures;
using MediaPortal.Plugins.MovingPictures.Database;

namespace StreamedMPConfig
{
  [PluginIcons("StreamedMPConfig.SMPSettings.png", "StreamedMPConfig.SMPSettingsDisabled.png")]
  public class StreamedMPConfig : GUIWindow, ISetupForm, IPlugin
  {
    #region Variables

    public int mrImageToDisplay = 3;
    public string isImageFanart = null;
    public int isImageCount = 1;
    public string[] mostTVSeriesRecents = new string[3];
    public string[] mostMovPicsRecents = new string[3];
    bool minimiseOnExit = false;
    //Declare Timer for use with Most Recent TVSeries/Movies
    System.Windows.Forms.Timer mrTimer = new System.Windows.Forms.Timer();
    settings smpSettings = new settings();
    SkinInfo skInfo = new SkinInfo();

    private static readonly logger smcLog = logger.GetInstance();

    #endregion

    #region public properties

    public static bool cdCoverOnly { get; set; }
    public static bool showEqGraphic { get; set; }
    public static bool fullVideoOSD { get; set; }
    public static bool checkOnStart { get; set; }
    public static bool checkForUpdateAt { get; set; }
    public static bool udateAvailable { get; set; }
    public static bool manualInstallNeeded { get; set; }
    public static int checkInterval { get; set; }
    public static double hours { get; set; }
    public static string nextUpdateCheck { get; set; }
    public static string theRevisions {get; set;}
    public static DateTime checkTime { get; set; }
    public static bool patchUtilityRunUnattended { get; set; }
    public static bool patchUtilityRestartMP { get; set; }
    public static int mrFanartTimer { get; set; }
    public static System.Threading.Timer updateChkTimer;

    #endregion

    #region Private Properties

    private static Regex _isNumber = new Regex(@"^\d+$");

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
      btSkinUpdate = 43,    // Skin Settings Menu
    }

    public enum SMPScreenID
    {
      SMPMenu = 196200,
      SMPMusicSettings = 196201,
      SMPVideoSettings = 196202,
      SMPMiscSettings = 196203,
      SMPSkinUpdate = 196204
    }

    [SkinControl((int)SkinControlIDs.btMusicScreens)] protected GUIButtonControl btMusicScreens = null;
    [SkinControl((int)SkinControlIDs.btVideoScreens)] protected GUIButtonControl btVideoScreens = null;
    //[SkinControl((int)SkinControlIDs.btMiscScreens)] protected GUIButtonControl btMiscScreens = null;
    [SkinControl((int)SkinControlIDs.btSkinUpdate)] protected GUIButtonControl btSkinUpdate = null;

    #endregion

    #region core functions

    public StreamedMPConfig()
    {
      GetID = GetWindowId();
    }

    public override bool Init()
    {
      Start();
      smcLog.WriteLog(string.Format("StreamedMPConfig GUI {0} starting.", Assembly.GetExecutingAssembly().GetName().Version),LogLevel.Info);
      if (IsAssemblyAvailable("MovingPictures", new Version(1, 0, 6, 1116)))
      {
        getLastThreeAdddedMovies();
        MovingPicturesCore.DatabaseManager.ObjectInserted += new DatabaseManager.ObjectAffectedDelegate(OnMovieAdded);
      }
      smcLog.WriteLog("Set the most recent fanart", LogLevel.Info);
      cycleMostrecentFanart();
      return Load(GUIGraphicsContext.Skin + @"\StreamedMPConfig.xml");
    }

    protected override void OnPageLoad()
    {
      settings.Load();
      GUIControl.SetControlLabel(GetID, 40, Translation.Strings["MusicMenu"]);
      GUIControl.SetControlLabel(GetID, 41, Translation.Strings["VideoMenu"]);
      GUIControl.SetControlLabel(GetID, 43, Translation.Strings["SkinUpdate"]);
    }

    protected override void OnPageDestroy(int new_windowId)
    {
      settings.Save();
      smcLog.WriteLog("Shutdown of StreamedMPconfig complete", LogLevel.Info);

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

      //if (control == btMiscScreens)
      //{
      //  GUIWindowManager.ActivateWindow((int)SMPScreenID.SMPMiscSettings);
      //}

      if (control == btSkinUpdate)
      {
        GUIWindowManager.ActivateWindow((int)SMPScreenID.SMPSkinUpdate);
      }
      // Pass it on
      base.OnClicked(controlId, control, actionType);
    }

    void restartMediaportal()
    {
      GUIDialogYesNo dlg = (GUIDialogYesNo)GUIWindowManager.GetWindow((int)GUIWindow.Window.WINDOW_DIALOG_YES_NO);
      dlg.Reset();
      dlg.SetHeading("MediaPortal Restart");
      dlg.SetLine(1, String.Empty);
      dlg.SetLine(2, "Are you sure you want to restart MediaPortal?");
      dlg.SetLine(3, String.Empty);
      dlg.DoModal(GUIWindowManager.ActiveWindow);
      if (dlg.IsConfirmed)
      {
        string restartExe = Path.Combine(SkinInfo.mpPaths.sMPbaseDir, "SMPMediaPortalRestart.exe");
        ProcessStartInfo processStart = new ProcessStartInfo(restartExe);
        processStart.Arguments = smpSettings.mpSetAsFullScreen ? "true" : "false";
        processStart.WorkingDirectory = Path.GetDirectoryName(restartExe);
        System.Diagnostics.Process.Start(processStart);
        if (minimiseOnExit)
          Environment.Exit(0);
        else
        {
          Action exitAction = new Action(Action.ActionType.ACTION_EXIT, 0, 0);
          GUIGraphicsContext.OnAction(exitAction);
        }
      }
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

    public static DateTime NextAt(TimeSpan time)
    {
      DateTime now = DateTime.Now;
      DateTime result = now.Date + time;

      return (now <= result) ? result : result.AddDays(1);
    }

    public void checkUpdateOnTimer(object source)
    {
      smcLog.WriteLog("Update timer fired - checking for update",LogLevel.Debug);
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
        smcLog.WriteLog("Next update check at : " + nextCheck.ToLongDateString() + ":" + nextCheck.ToLongTimeString(),LogLevel.Info);
        StreamedMPConfig.nextUpdateCheck = nextCheck.ToString();
        settings.Save();

        TimeSpan setTimerFor = (nextCheck - DateTime.Now).Add(hoursToAdd);
        smcLog.WriteLog("Timer Set for : " + setTimerFor.ToString(),LogLevel.Info);

        return setTimerFor;
      }
    }

    bool IsInteger(string theValue)
    {
      Match m = _isNumber.Match(theValue);
      return m.Success;
    }

    public static void checkAndCopy(string destinationPath)
    {
      String[] Files;
      Files = Directory.GetFileSystemEntries(destinationPath);
      foreach (string patchdir in Files)
      {
        if (patchdir.ToLower().Contains("language"))
          copyDirectory(Path.Combine(destinationPath, "language"), SkinInfo.mpPaths.langBasePath);
        if (patchdir.ToLower().Contains("skin"))
          copyDirectory(Path.Combine(destinationPath, "skin"), SkinInfo.mpPaths.skinBasePath);
        if (patchdir.ToLower().Contains("thumbs"))
          copyDirectory(Path.Combine(destinationPath, "thumbs"), SkinInfo.mpPaths.thumbsPath);
        if (patchdir.ToLower().Contains("database"))
          copyDirectory(Path.Combine(destinationPath, "database"), SkinInfo.mpPaths.databasePath);
      }
    }


    public static void copyDirectory(string patchSource, string patchDestination)
    {
      String[] patchFiles;

      if (patchDestination[patchDestination.Length - 1] != Path.DirectorySeparatorChar)
        patchDestination += Path.DirectorySeparatorChar;

      if (!Directory.Exists(patchDestination)) 
        Directory.CreateDirectory(patchDestination);

      patchFiles = Directory.GetFileSystemEntries(patchSource);

      foreach (string Element in patchFiles)
      {
        if (Directory.Exists(Element))
          copyDirectory(Element, patchDestination + Path.GetFileName(Element));             
        else
        {
          File.Copy(Element, patchDestination + Path.GetFileName(Element), true);
          smcLog.WriteLog("Patching: Copy >" + Element + " to " + patchDestination + Path.GetFileName(Element),LogLevel.Debug);
        }
      }
    }

    void getLastThreeAdddedMovies()
    {
      smcLog.WriteLog("Get Most Recent Movies", LogLevel.Info);

      List<DBMovieInfo> movies = DBMovieInfo.GetAll();
      smcLog.WriteLog(string.Format("{0} Movies found in database",movies.Count.ToString()), LogLevel.Info);
      
      // Sort list in to most recent first
      movies.Sort(delegate(DBMovieInfo m1, DBMovieInfo m2) { return m2.DateAdded.CompareTo(m1.DateAdded); });

      // Clear the properties first
      for (int i = 3; i == 0; --i)
      {
        SetProperty("#StreamedMP.recentlyAdded.movie" + i.ToString() + ".title", string.Empty);
        SetProperty("#StreamedMP.recentlyAdded.movie" + i.ToString() + ".thumb", string.Empty);
        SetProperty("#StreamedMP.recentlyAdded.movie" + i.ToString() + ".fanart", string.Empty);
        SetProperty("#StreamedMP.recentlyAdded.movie" + i.ToString() + ".runtime", string.Empty);
        SetProperty("#StreamedMP.recentlyAdded.movie" + i.ToString() + ".certification", string.Empty);
        SetProperty("#StreamedMP.recentlyAdded.movie" + i.ToString() + ".score", string.Empty);
      }
      // Now take the first 3 
      int mrMovieNumber = 3;
      foreach (DBMovieInfo movie in movies)
      {
        SetProperty("#StreamedMP.recentlyAdded.movie" + mrMovieNumber.ToString() + ".title", movie.Title);
        SetProperty("#StreamedMP.recentlyAdded.movie" + mrMovieNumber.ToString() + ".thumb", movie.CoverThumbFullPath);
        SetProperty("#StreamedMP.recentlyAdded.movie" + mrMovieNumber.ToString() + ".fanart", movie.BackdropFullPath);
        SetProperty("#StreamedMP.recentlyAdded.movie" + mrMovieNumber.ToString() + ".runtime", movie.Runtime.ToString() + " mins");
        SetProperty("#StreamedMP.recentlyAdded.movie" + mrMovieNumber.ToString() + ".certification", "Rated: " + movie.Certification);
        SetProperty("#StreamedMP.recentlyAdded.movie" + mrMovieNumber.ToString() + ".score", Math.Round(movie.Score, MidpointRounding.AwayFromZero).ToString());

        --mrMovieNumber;
        if (mrMovieNumber == 0)
          break;
      }
    }

    private bool IsAssemblyAvailable(string name, Version ver)
    {
      bool result = false;

      smcLog.WriteLog(string.Format("Checking whether assembly {0} is available and loaded...", name), LogLevel.Info);

      Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
      foreach (Assembly a in assemblies)
      {
        if (a.GetName().Name == name && a.GetName().Version >= ver)
        {
          smcLog.WriteLog(string.Format("Assembly {0} v{1} is available and loaded.", name, a.GetName().Version.ToString()), LogLevel.Info);
          result = true;
          break;
        }
      }

      if (!result)
      {
        smcLog.WriteLog(string.Format("Assembly {0} is not loaded (not available?), trying to load it manually...", name), LogLevel.Info);
        try
        {          
          Assembly assembly = Assembly.ReflectionOnlyLoad(name);
          smcLog.WriteLog(string.Format("Assembly {0} is available and loaded successfully.", name), LogLevel.Info);
          result = true;
        }
        catch (Exception e)
        {
          smcLog.WriteLog(string.Format("Assembly {0} is unavailable, load unsuccessful: {1}:{2}", name, e.GetType(), e.Message), LogLevel.Info);
        }
      }

      return result;
    }

    #endregion
    
    #region EventHandlers

    public void smpAction(Action Action)
    {
      if (Action.wID.ToString() == "196250")
      {
        smcLog.WriteLog("Restarting MediaPortal", LogLevel.Info);
        restartMediaportal();
      }
    }

    private void OnMovieAdded(DatabaseTable obj)
    {
      // check that object insertion was a movie
      if (obj.GetType() != typeof(DBMovieInfo))
        return;

      smcLog.WriteLog(string.Format("New movie added: {0}", ((DBMovieInfo)obj).Title), LogLevel.Info);
      
      // get all movies again to update recently added list
      // this should be really fast now as its cached
      getLastThreeAdddedMovies();
    }

    private void GUIGraphicsContext_OnVideoWindowChanged()
    {
      if (GUIWindowManager.ActiveWindow == 0)
        return;

      smcLog.WriteLog("StreamedMPConfig: OnVideoWindowsChanged Event Called",LogLevel.Debug);

      if (GUIGraphicsContext.ShowBackground == true)
      {
        smcLog.WriteLog("#StreamedMP.VideoWallpaper = false",LogLevel.Debug);
        GUIPropertyManager.SetProperty("#StreamedMP.VideoWallpaper", "false");
      }
      else
      {
        smcLog.WriteLog("#StreamedMP.VideoWallpaper = true",LogLevel.Debug);
        GUIPropertyManager.SetProperty("#StreamedMP.VideoWallpaper", "true");
      }
    }

    private void GUIWindowManager_OnActivateWindow(int windowID)
    {
      if (GUIWindowManager.ActiveWindow == 0)
        return;

      smcLog.WriteLog("StreamedMPConfig: OnActivateWindow Event Called(" + GUIWindowManager.ActiveWindow.ToString() + ")",LogLevel.Debug);


      // Disable the timer used for most recent fanart images in not on home screen.
      if (GUIWindowManager.ActiveWindow == 35)
      {
        mrTimer.Enabled = true;
        smcLog.WriteLog(string.Format("Most Recent Summary Timer : Enabled ({0} Seconds)",mrFanartTimer), LogLevel.Debug);
      }
      else
      {
        mrTimer.Enabled = false;
        smcLog.WriteLog("Most Recent Summary Timer : Disabled", LogLevel.Debug);
      }


      if (GUIWindowManager.ActiveWindow == 35)
      {
        string seasonNum = null;
        string episodeNum = null;
        string formattedSE = null;

        getMostRecents();
        for (int i = 0; i < 3; i++)
        {
          seasonNum = GUIPropertyManager.GetProperty("#infoservice.recentlyAdded.series" + (i + 1).ToString() + ".season");
          episodeNum = GUIPropertyManager.GetProperty("#infoservice.recentlyAdded.series" + (i + 1).ToString() + ".episodenumber");
          if (mostTVSeriesRecents[i] != null)
          {
            if (smpSettings.mrSeasonEpisodeStyle2)
            {
              formattedSE = "S" + seasonNum.PadLeft(2, '0') + "E" + episodeNum.PadLeft(2, '0');
              SetProperty("#StreamedMP.MostRecent." + (i + 1).ToString() + ".SEFormat", formattedSE);
            }
            else
            {
              formattedSE = seasonNum + "x" + episodeNum;
              SetProperty("#StreamedMP.MostRecent." + (i + 1).ToString() + ".SEFormat", formattedSE);
            }
          }
          else
          {
            formattedSE = seasonNum + "x" + episodeNum;
            SetProperty("#StreamedMP.MostRecent." + (i + 1).ToString() + ".SEFormat", formattedSE);
          }
          smcLog.WriteLog("Set " + "#StreamedMP.MostRecent." + (i + 1).ToString() + ".SEFormat to " + formattedSE, LogLevel.Debug);
        }
      }


      smcLog.WriteLog("StreamedMPConfig: (" + windowID.ToString() + ") Setting all property valuse to initial values",LogLevel.Debug);

      GUIPropertyManager.SetProperty("#StreamedMP.cdCoverOnly", "false");
      GUIPropertyManager.SetProperty("#StreamedMP.showEqGraphic", "false");
      GUIPropertyManager.SetProperty("#StreamedMP.fullVideoOSD", "true");

      if (cdCoverOnly)
      {
        smcLog.WriteLog("StreamedMPConfig: Setting #StreamedMP.cdCoverOnly = true", LogLevel.Debug);
        GUIPropertyManager.SetProperty("#StreamedMP.cdCoverOnly", "true");
      }

      if (showEqGraphic)
      {
        smcLog.WriteLog("StreamedMPConfig: Setting #StreamedMP.showEqGraphic = true", LogLevel.Debug);
        GUIPropertyManager.SetProperty("#StreamedMP.showEqGraphic", "true");
      }
      if (!fullVideoOSD)
      {
        smcLog.WriteLog("StreamedMPConfig: Setting #StreamedMP.fullVideoOSD = false", LogLevel.Debug);
        GUIPropertyManager.SetProperty("#StreamedMP.fullVideoOSD", "false");
      }
      else
      {
        smcLog.WriteLog("StreamedMPConfig: Setting #StreamedMP.fullVideoOSD = true", LogLevel.Debug);
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
        smcLog.WriteLog("StreamedMPConfig: Setting all Indicator Values to false", LogLevel.Debug);
        SetProperty("#StreamedMP.UpdateAvailable", "false");
        SetProperty("#StreamedMP.ShowUpdateInd", "false");
        SetProperty("#StreamedMP.ShowSettingsUpdateInd", "false");
      }
    }

    void mrTimer_Tick(object sender, EventArgs e)
    {
      cycleMostrecentFanart();
    }

    void cycleMostrecentFanart()
    {
      // Read the InfoService Fanart property every x seconds, check the next in the sequence each time through.
      // Set our own property to match a value if found.
      getMostRecents();
      if (mrImageToDisplay == 1)
        mrImageToDisplay = 2;
      else if (mrImageToDisplay == 2)
        mrImageToDisplay = 3;
      else if (mrImageToDisplay == 3)
        mrImageToDisplay = 1;

      if (mostTVSeriesRecents[mrImageToDisplay - 1] != null)
      {
        smcLog.WriteLog("StreamedMPConfig: TVSeries Img(" + mrImageToDisplay.ToString() + ") File:" + mostTVSeriesRecents[mrImageToDisplay - 1],LogLevel.Debug);
        SetProperty("#StreamedMP.MostRecentImageFanart", mostTVSeriesRecents[mrImageToDisplay - 1]);
      }
      if (mostMovPicsRecents[mrImageToDisplay - 1] != null)
      {
        smcLog.WriteLog("StreamedMPConfig: MovingPictures Img(" + mrImageToDisplay.ToString() + ") File:" + mostMovPicsRecents[mrImageToDisplay - 1], LogLevel.Debug);
        SetProperty("#StreamedMP.MostRecentMovPicsImageFanart", mostMovPicsRecents[mrImageToDisplay - 1]);
      }
    }


    void getMostRecents()
    {
      for (int i = 0; i < 3; i++)
      {
        mostTVSeriesRecents[i] = null;
        mostMovPicsRecents[i] = null;
        try
        {
          mostTVSeriesRecents[i] = GUIPropertyManager.GetProperty("#infoservice.recentlyAdded.series" + (i + 1).ToString() + ".fanart");
        }
        catch { }
        try
        {
          mostMovPicsRecents[i] = GUIPropertyManager.GetProperty("#StreamedMP.recentlyAdded.movie" + (i + 1).ToString() + ".fanart");
        }
        catch { } 
        if (mostTVSeriesRecents[i] == " ")
          mostTVSeriesRecents[i] = null;

        if (mostMovPicsRecents[i] == " ")
          mostMovPicsRecents[i] = null;

      }



    }

    #endregion

    #region <Interface> Implementations

    #region IPlugin Interface

    public void Start()
    {
      logger.LogFile = Path.Combine(Config.GetFolder(Config.Dir.Log),"StreamedMPConfig.log");
      logger.LogError = smpSettings.logLevelError; 
      logger.LogWarning = smpSettings.logLevelWarning;
      logger.LogDebug = smpSettings.logLevelDebug;
      if (skInfo.minimiseMPOnExit.ToLower() == "yes")
        minimiseOnExit = true;

      smcLog.WriteLog( string.Format("StreamedMPConfig Plugin {0} starting.", Assembly.GetExecutingAssembly().GetName().Version),LogLevel.Info );

      // Check if the skin is StreamedMP and bail if not.
      if (!GUIGraphicsContext.Skin.EndsWith("StreamedMP"))
      {
        smcLog.WriteLog("Not Running StreamedMP Skin - do Nothing", LogLevel.Info);
        return;
      }
      StreamedMPConfig.udateAvailable = false;
      settings.Load();

      if (smpSettings.timerRequired)
      {
        cycleMostrecentFanart();
        mrTimer.Enabled = true;
        mrTimer.Interval = StreamedMPConfig.mrFanartTimer * 1000;
        mrTimer.Tick += new EventHandler(mrTimer_Tick);
        smcLog.WriteLog(string.Format("StreamedMPConfig: Timer Enabled ({0} Seconds)",mrFanartTimer),LogLevel.Info);
      }
      else
        smcLog.WriteLog("StreamedMPConfig: Timer Disabled",LogLevel.Info);    


      //if (smpSettings.isVisEnabled)
      //{
      //  File.Copy(Path.Combine(SkinInfo.mpPaths.streamedMPpath, "common.mymusic.ctrl112Vis.xml"), Path.Combine(SkinInfo.mpPaths.streamedMPpath, "common.mymusic.ctrl112.xml"), true);
      //  Log.Debug("StreamedMPConfig: Copied common.mymusic.ctrl112Vis.xml to common.mymusic.ctrl112.xml");
      //}
      //else
      //{
      //  File.Copy(Path.Combine(SkinInfo.mpPaths.streamedMPpath, "common.mymusic.ctrl112Std.xml"), Path.Combine(SkinInfo.mpPaths.streamedMPpath, "common.mymusic.ctrl112.xml"), true);
      //  Log.Debug("StreamedMPConfig: Copied common.mymusic.ctrl112Std.xml to common.mymusic.ctrl112.xml");
      //}



      GUIWindowManager.OnActivateWindow += new GUIWindowManager.WindowActivationHandler(GUIWindowManager_OnActivateWindow);
      GUIGraphicsContext.OnVideoWindowChanged += new VideoWindowChangedHandler(GUIGraphicsContext_OnVideoWindowChanged);
      GUIGraphicsContext.OnNewAction += new OnActionHandler(smpAction);


      // Set all fixed translation properties 
      // these have defaults defined in the code and are used internally
      try
      {
        foreach (string name in Translation.Strings.Keys)
        {
          if (Translation.Strings[name] != "")
          {
            smcLog.WriteLog("Name : " + name + "  Translation : " + Translation.Strings[name],LogLevel.Info);
            SetProperty("#StreamedMP." + name, Translation.Strings[name]);
          }
        }
      }
      catch
      {
        smcLog.WriteLog("StreamedMPConfig: Translation Exception",LogLevel.Info);
      }

      
      // Set user defined property, this is defined in the langauge file as the full 
      // property name including the the # i.e #StreamedMP.MyDefinedProperty
      foreach (string propName in Translation.FixedTranslations.Keys)
      {
        if (propName != "")
        {
          string propValue;
          Translation.FixedTranslations.TryGetValue(propName, out propValue);
          if (IsInteger(propValue))
          {
            smcLog.WriteLog("Get MediaPortal Tranlation -> Name : " + propName + "  Translation : " + GUILocalizeStrings.Get(int.Parse(propValue)),LogLevel.Debug);
            SetProperty(propName, GUILocalizeStrings.Get(int.Parse(propValue)));
          }
          else
          {
            smcLog.WriteLog("Name : " + propName + "  Translation : " + propValue,LogLevel.Debug);
            SetProperty(propName, propValue);
          }
        }
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
          smcLog.WriteLog("Stored Timer Set for : " + setStored.ToString(),LogLevel.Debug);
          updateChkTimer = new System.Threading.Timer(updateCallback, null, setStored, TimeSpan.FromHours(StreamedMPConfig.hours));
        }
        else
        {
          updateChkTimer = new System.Threading.Timer(updateCallback, null, nextCheckAt, TimeSpan.FromHours(StreamedMPConfig.hours));

          // The line below is for testing, comment the above line and uncomment this - the update check will fire 30sec after startup
          // updateChkTimer = new Timer(updateCallback, null, 30000, 60000);
        }
      }
      if (Screen.PrimaryScreen.Bounds.Width == 1920 && Screen.PrimaryScreen.Bounds.Height == 1080)
      {
        smcLog.WriteLog("Set #StreamedMP.FullHD = true", LogLevel.Debug);
        StreamedMPConfig.SetProperty("#StreamedMP.FullHD", "true");
      }
      else
      {
        smcLog.WriteLog("Set #StreamedMP.FullHD = false",LogLevel.Debug);
        StreamedMPConfig.SetProperty("#StreamedMP.FullHD", "false");
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