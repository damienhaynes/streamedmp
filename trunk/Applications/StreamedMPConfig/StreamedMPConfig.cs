using System;
using System.Collections.Generic;
using System.Reflection;
using MediaPortal.Configuration;
using MediaPortal.GUI.Library;
using Action = MediaPortal.GUI.Library.Action;
using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using MediaPortal.Dialogs;
using Microsoft.Win32;
using Cornerstone.Tools;
using Cornerstone.Database;
using Cornerstone.Database.Tables;
using MediaPortal.Plugins.MovingPictures;
using MediaPortal.Plugins.MovingPictures.Database;
using MediaPortal.Plugins.MovingPictures.MainUI;
using SMPCheckSum;
using WindowPlugins.GUITVSeries;
using TVSeriesHelper = WindowPlugins.GUITVSeries.Helper;

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
    public string[] mostTVSeriesRecentsWatched = new string[3];
    public string[] mostMovPicsRecents = new string[3];
    public string[] mostMovPicsRecentsWatched = new string[3];
    bool minimiseOnExit = false;
    //Declare Timer for use with Most Recent TVSeries/Movies
    System.Windows.Forms.Timer mrTimer = new System.Windows.Forms.Timer();
    settings smpSettings = new settings();
    SkinInfo skInfo = new SkinInfo();

    List<DBMovieInfo> recentAddedMovies = null;
    List<DBEpisode> recentAddedEpisodes = null;

    public static List<int> mostRecentEpisodeControlIDs = new List<int>();
    public static List<int> mostRecentMovieControlIDs = new List<int>();

    MoviePlayer moviePlayer = null;
    VideoHandler episodePlayer = null;

    private static readonly logger smcLog = logger.GetInstance();

    #endregion

    #region public properties
        
    public static bool checkOnStart { get; set; }
    public static bool checkForUpdateAt { get; set; }
    public static bool updateAvailable { get; set; }
    public static bool manualInstallNeeded { get; set; }
    public static int checkInterval { get; set; }
    public static double hours { get; set; }
    public static string nextUpdateCheck { get; set; }
    public static string theRevisions { get; set; }
    public static DateTime checkTime { get; set; }
    public static bool patchUtilityRunUnattended { get; set; }
    public static bool patchUtilityRestartMP { get; set; }    
    public static System.Threading.Timer updateChkTimer;

    public static bool movPicRecentAddedEnabled { get; set; }
    public static bool movPicRecentWatchedEnabled { get; set; }
    public static bool tvSeriesRecentAddedEnabled { get; set; }
    public static bool tvSeriesRecentWatchedEnabled { get; set; }

    #endregion

    #region Private Properties

    private static Regex _isNumber = new Regex(@"^\d+$");

    #endregion

    #region Skin Connection

    public enum SkinControlIDs
    {      
      MusicScreens = 40,      // Skin Settings Menu
      VideoScreens = 41,      // Skin Settings Menu
      MiscScreens = 42,       // Skin Settings Menu
      SkinUpdate = 43,        // Skin Settings Menu
      TVSeriesConfig = 44,    // TVSeries Configuration
      MovPicsConfig = 45,     // MovingPictures Configuration
      TVConfig = 46           // TV Configuration
    }

    public enum SMPScreenID
    {
      SMPMenu = 196200,
      SMPMusicSettings = 196201,
      SMPVideoSettings = 196202,
      SMPMiscSettings = 196203,
      SMPSkinUpdate = 196204,
      SMPTVSeriesConfig = 196205,
      SMPMovPicsConfig = 196206,
      SMPTVConfig = 196207
    }

    private enum MediaPortalWindows
    {      
      Home = 0,
      HomePlugins = 34,
      BasicHome = 35
    }

    [SkinControl((int)SkinControlIDs.MusicScreens)]
    protected GUIButtonControl btMusicScreens = null;
    [SkinControl((int)SkinControlIDs.VideoScreens)]
    protected GUIButtonControl btVideoScreens = null;
    [SkinControl((int)SkinControlIDs.MiscScreens)]
    protected GUIButtonControl btMiscScreens = null;
    [SkinControl((int)SkinControlIDs.SkinUpdate)]
    protected GUIButtonControl btSkinUpdate = null;
    [SkinControl((int)SkinControlIDs.TVSeriesConfig)]
    protected GUIButtonControl btTVSeriesConfig = null;
    [SkinControl((int)SkinControlIDs.MovPicsConfig)]
    protected GUIButtonControl btMovPicsConfig = null;
    [SkinControl((int)SkinControlIDs.TVConfig)]
    protected GUIButtonControl btTVConfig = null;

    #endregion

    #region Base overrides

    public override bool Init()
    {
      // Check if the skin is StreamedMP and bail if not.
      // note: user may have multiple streamedmp skins for testing
      if (!GUIGraphicsContext.Skin.Contains("StreamedMP"))
      {
        smcLog.WriteLog("Not Running StreamedMP Skin - do Nothing", LogLevel.Info);
        return true;
      }
      Start();

      //smcLog.WriteLog(string.Format("StreamedMPConfig GUI {0} starting.", Assembly.GetExecutingAssembly().GetName().Version), LogLevel.Info);
      
      // Get Most Recent Options
      settings.LoadEditorProperties();

      if ((tvSeriesRecentAddedEnabled || tvSeriesRecentWatchedEnabled) && Helper.IsAssemblyAvailable("MP-TVSeries", new Version(2, 6, 3, 1239)))
      {
        if (tvSeriesRecentAddedEnabled)
          getLastThreeAddedTVSeries();
        if (tvSeriesRecentWatchedEnabled)
          getLastThreeWatchedTVSeries();
        setTVSeriesEvents();
      }
      else
      {
        // set to false incase enabled after upgrade but plugin not installed
        tvSeriesRecentAddedEnabled = false;
        tvSeriesRecentWatchedEnabled = false;
      }
      if ((movPicRecentAddedEnabled || movPicRecentWatchedEnabled) && Helper.IsAssemblyAvailable("MovingPictures", new Version(1, 0, 6, 1116)))
      {
        if (movPicRecentAddedEnabled)
          getLastThreeAddedMovies();
        if (movPicRecentWatchedEnabled)
          getLastThreeWatchedMovies();
        setMovingPicturesEvents();
      }
      else
      {
        // set to false incase enabled after upgrade but plugin not installed
        movPicRecentAddedEnabled = false;
        movPicRecentWatchedEnabled = false;
      }

      if (smpSettings.timerRequired)
      {
        smcLog.WriteLog("Set the most recent fanart", LogLevel.Info);
        cycleMostrecentFanart();
      }
      return Load(GUIGraphicsContext.Skin + @"\StreamedMPConfig.xml");
    }

    protected override void OnPageLoad()
    {      
      GUIControl.SetControlLabel(GetID, (int)SkinControlIDs.MusicScreens, Translation.MusicMenu);
      GUIControl.SetControlLabel(GetID, (int)SkinControlIDs.VideoScreens, Translation.VideoMenu);
      GUIControl.SetControlLabel(GetID, (int)SkinControlIDs.SkinUpdate, Translation.SkinUpdate);
      GUIControl.SetControlLabel(GetID, (int)SkinControlIDs.MiscScreens, Translation.MiscMenu);
      GUIControl.SetControlLabel(GetID, (int)SkinControlIDs.TVSeriesConfig, Translation.TVSeriesMenu);
      GUIControl.SetControlLabel(GetID, (int)SkinControlIDs.MovPicsConfig, Translation.MovingPicturesMenu);
      GUIControl.SetControlLabel(GetID, (int)SkinControlIDs.TVConfig, Translation.TVMenu);
    }

    protected override void OnPageDestroy(int new_windowId)
    {      
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

      if (control == btTVSeriesConfig)
      {
        GUIWindowManager.ActivateWindow((int)SMPScreenID.SMPTVSeriesConfig);
      }

      if (control == btMovPicsConfig)
      {
        GUIWindowManager.ActivateWindow((int)SMPScreenID.SMPMovPicsConfig);
      }

      if (control == btTVConfig)
      {
        GUIWindowManager.ActivateWindow((int)SMPScreenID.SMPTVConfig);
      }

      if (control == btMiscScreens)
      {
        GUIWindowManager.ActivateWindow((int)SMPScreenID.SMPMiscSettings);
      }

      if (control == btSkinUpdate)
      {
        GUIWindowManager.ActivateWindow((int)SMPScreenID.SMPSkinUpdate);
      }
      // Pass it on
      base.OnClicked(controlId, control, actionType);
    }

    #endregion

    #region Constructor

    public StreamedMPConfig()
    {
      GetID = GetWindowId();
    }

    #endregion

    #region Private Methods

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

    bool IsInteger(string theValue)
    {
      Match m = _isNumber.Match(theValue);
      return m.Success;
    }

    void setMovingPicturesEvents()
    {
      if (movPicRecentAddedEnabled || movPicRecentWatchedEnabled)
      {
        MovingPicturesCore.DatabaseManager.ObjectInserted += new DatabaseManager.ObjectAffectedDelegate(OnObjectInserted);
      }
    }

    void setTVSeriesEvents()
    {
      if (tvSeriesRecentAddedEnabled)
      {
        OnlineParsing.OnlineParsingCompleted += new OnlineParsing.OnlineParsingCompletedHandler(OnTVSeriesParseCompleted);
      }
      if (tvSeriesRecentWatchedEnabled)
      {
        VideoHandler.EpisodeWatched += new VideoHandler.EpisodeWatchedDelegate(OnEpisodeWatched);
      }
    }

    void getLastThreeWatchedMovies()
    {
      smcLog.WriteLog("Get Most Recent Watched Movies", LogLevel.Info);

      // get list of movies in database
      List<DBMovieInfo> movies = DBMovieInfo.GetAll();

      // get filter criteria of movies protected by parental conrols
      bool pcFilterEnabled = MovingPicturesCore.Settings.ParentalControlsEnabled;
      DBFilter<DBMovieInfo> pcFilter = MovingPicturesCore.Settings.ParentalControlsFilter;

      // apply parental control filter to movie list
      List<DBMovieInfo> filteredMovies = pcFilterEnabled ? pcFilter.Filter(movies).ToList() : movies;

      // sort based on most recently watched
      filteredMovies.Sort((m1, m2) =>
        {
          // get watched count for each movie         
          int watchCount1 = m1.WatchedHistory.Count;
          int watchCount2 = m2.WatchedHistory.Count;

          // compare most recently watched dates
          // WatchedHistory stores a list of dates each time the movie was watched
          DateTime lastWatchDate1 = watchCount1 > 0 ? m1.WatchedHistory[watchCount1 - 1].DateWatched : DateTime.MinValue;
          DateTime lastWatchDate2 = watchCount2 > 0 ? m2.WatchedHistory[watchCount2 - 1].DateWatched : DateTime.MinValue;
          return lastWatchDate2.CompareTo(lastWatchDate1);
        });

      // Remove anything not watched in the last 30 days
      filteredMovies.RemoveAll((movie) => 
        {
          int watchCount = movie.WatchedHistory.Count;
          if (watchCount > 0)
          {
            return movie.WatchedHistory[watchCount - 1].DateWatched < DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0, 0));
          }
          else
            return true;
        });

      // Clear the properties first
      for (int i = 3; i == 0; --i)
      {
        SetProperty("#StreamedMP.recentlyWatched.movie" + i.ToString() + ".title", string.Empty);
        SetProperty("#StreamedMP.recentlyWatched.movie" + i.ToString() + ".thumb", string.Empty);
        SetProperty("#StreamedMP.recentlyWatched.movie" + i.ToString() + ".fanart", string.Empty);
        SetProperty("#StreamedMP.recentlyWatched.movie" + i.ToString() + ".runtime", string.Empty);
        SetProperty("#StreamedMP.recentlyWatched.movie" + i.ToString() + ".certification", string.Empty);
        SetProperty("#StreamedMP.recentlyWatched.movie" + i.ToString() + ".score", string.Empty);
        SetProperty("#StreamedMP.recentlyWatched.movie" + i.ToString() + ".watchedcount", string.Empty);
        SetProperty("#StreamedMP.recentlyWatched.movie" + i.ToString() + ".watcheddate", string.Empty);
      }
      // Now take the first 3 
      int rwMovieNumber = 1;
      foreach (DBMovieInfo movie in filteredMovies)
      {
        SetProperty("#StreamedMP.recentlyWatched.movie" + rwMovieNumber.ToString() + ".title", movie.Title);
        SetProperty("#StreamedMP.recentlyWatched.movie" + rwMovieNumber.ToString() + ".thumb", movie.CoverThumbFullPath);
        SetProperty("#StreamedMP.recentlyWatched.movie" + rwMovieNumber.ToString() + ".fanart", movie.BackdropFullPath);
        SetProperty("#StreamedMP.recentlyWatched.movie" + rwMovieNumber.ToString() + ".runtime", GetMovieRuntime(movie) + " mins");
        SetProperty("#StreamedMP.recentlyWatched.movie" + rwMovieNumber.ToString() + ".certification", (string.IsNullOrEmpty(movie.Certification.Trim()) ? string.Empty : " [" + movie.Certification + "]"));
        SetProperty("#StreamedMP.recentlyWatched.movie" + rwMovieNumber.ToString() + ".score", Math.Round(movie.Score, MidpointRounding.AwayFromZero).ToString());
        SetProperty("#StreamedMP.recentlyWatched.movie" + rwMovieNumber.ToString() + ".actualscore", movie.Score.ToString());
        SetProperty("#StreamedMP.recentlyWatched.movie" + rwMovieNumber.ToString() + ".watchedcount", movie.WatchedHistory.Count.ToString());
        SetProperty("#StreamedMP.recentlyWatched.movie" + rwMovieNumber.ToString() + ".watcheddate", movie.WatchedHistory.Count > 0 ? movie.WatchedHistory[movie.WatchedHistory.Count - 1].DateWatched.ToString() : "N/A");
        smcLog.WriteLog(string.Format("Recently Watched Movie {0} is ", rwMovieNumber) + movie.Title, LogLevel.Info);
        ++rwMovieNumber;
        if (rwMovieNumber == 4)
          break;
      }

    }

    void getLastThreeAddedMovies()
    {
      smcLog.WriteLog("Get Most Recent Added Movies", LogLevel.Info);
      
      // get list of movies in database
      List<DBMovieInfo> movies = DBMovieInfo.GetAll();
      
      // get filter criteria of movies protected by parental conrols
      bool pcFilterEnabled = MovingPicturesCore.Settings.ParentalControlsEnabled;
      DBFilter<DBMovieInfo> pcFilter = MovingPicturesCore.Settings.ParentalControlsFilter;     
           
      // apply parental control filter to movie list
      List<DBMovieInfo> filteredMovies = pcFilterEnabled ? pcFilter.Filter(movies).ToList() : movies;

      smcLog.WriteLog(string.Format("{0} Movies found in database", movies.Count.ToString()), LogLevel.Info);      
      smcLog.WriteLog(string.Format("{0} Movies filtered by parental controls", movies.Count - filteredMovies.Count), LogLevel.Info);      

      // Sort list in to most recent first
      filteredMovies.Sort((m1, m2) =>
        {
          return m2.DateAdded.CompareTo(m1.DateAdded); 
        });
      
      // Remove anything older than 30 days
      filteredMovies.RemoveAll(movie => movie.DateAdded < DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0, 0)));

      recentAddedMovies = filteredMovies;

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
      int mrMovieNumber = 1;
      foreach (DBMovieInfo movie in filteredMovies)
      {
        SetProperty("#StreamedMP.recentlyAdded.movie" + mrMovieNumber.ToString() + ".title", movie.Title);
        SetProperty("#StreamedMP.recentlyAdded.movie" + mrMovieNumber.ToString() + ".thumb", movie.CoverThumbFullPath);
        SetProperty("#StreamedMP.recentlyAdded.movie" + mrMovieNumber.ToString() + ".fanart", movie.BackdropFullPath);
        SetProperty("#StreamedMP.recentlyAdded.movie" + mrMovieNumber.ToString() + ".runtime", GetMovieRuntime(movie) + " mins");
        SetProperty("#StreamedMP.recentlyAdded.movie" + mrMovieNumber.ToString() + ".certification", (string.IsNullOrEmpty(movie.Certification.Trim()) ? string.Empty : " [" + movie.Certification + "]"));
        SetProperty("#StreamedMP.recentlyAdded.movie" + mrMovieNumber.ToString() + ".score", Math.Round(movie.Score, MidpointRounding.AwayFromZero).ToString());
        SetProperty("#StreamedMP.recentlyAdded.movie" + mrMovieNumber.ToString() + ".actualscore", movie.Score.ToString());
        smcLog.WriteLog(string.Format("Recently Added Movie {0} is ", mrMovieNumber) + movie.Title, LogLevel.Info);
        ++mrMovieNumber;
        if (mrMovieNumber == 4)
          break;
      }
    }

    string GetMovieRuntime(DBMovieInfo movie)
    {
      string minutes = string.Empty;
      if (movie == null) return minutes;

      if (MovingPicturesCore.Settings.DisplayActualRuntime && movie.ActualRuntime > 0)
      {
        // Actual Runtime or (MediaInfo result) is in milliseconds
        // convert to minutes
        minutes = ((movie.ActualRuntime / 1000) / 60).ToString();
      }
      else
        minutes = movie.Runtime.ToString();

      return minutes;
    }

    void getLastThreeWatchedTVSeries()
    {
      smcLog.WriteLog("Get Most Recent Watched TVSeries", LogLevel.Info);

      // get list of the 3 most recently watched episodes in tvseries database      
      List<DBEpisode> episodes = DBEpisode.GetMostRecent(MostRecentType.Watched, 30, 3);

      // Clear the properties first
      for (int i = 3; i == 0; --i)
      {
        SetProperty("#StreamedMP.recentlyWatched.series" + i.ToString() + ".title", string.Empty);
        SetProperty("#StreamedMP.recentlyWatched.series" + i.ToString() + ".episodetitle", string.Empty);
        SetProperty("#StreamedMP.recentlyWatched.series" + i.ToString() + ".episodenumber", string.Empty);
        SetProperty("#StreamedMP.recentlyWatched.series" + i.ToString() + ".season", string.Empty);
        SetProperty("#StreamedMP.recentlyWatched.series" + i.ToString() + ".thumb", string.Empty);
        SetProperty("#StreamedMP.recentlyWatched.series" + i.ToString() + ".fanart", string.Empty);
      }

      if (episodes.Count == 0)
      {
        smcLog.WriteLog("Found no results for TVseries Recently Watched", LogLevel.Info);
      }

      // Now take the first 3 
      int mrEpisodeNumber = 1;
      foreach (DBEpisode episode in episodes)
      {
        DBSeries series = TVSeriesHelper.getCorrespondingSeries(episode[DBEpisode.cSeriesID]);
        if (series != null)
        {
          SetProperty("#StreamedMP.recentlyWatched.series" + mrEpisodeNumber.ToString() + ".title", series.ToString());
          SetProperty("#StreamedMP.recentlyWatched.series" + mrEpisodeNumber.ToString() + ".episodetitle", episode[DBEpisode.cEpisodeName]);
          SetProperty("#StreamedMP.recentlyWatched.series" + mrEpisodeNumber.ToString() + ".episodenumber", episode[DBEpisode.cEpisodeIndex]);
          SetProperty("#StreamedMP.recentlyWatched.series" + mrEpisodeNumber.ToString() + ".season", episode[DBEpisode.cSeasonIndex]);
          SetProperty("#StreamedMP.recentlyWatched.series" + mrEpisodeNumber.ToString() + ".thumb", series.Poster);
          SetProperty("#StreamedMP.recentlyWatched.series" + mrEpisodeNumber.ToString() + ".fanart", Fanart.getFanart(episode[DBEpisode.cSeriesID]).FanartFilename);
          smcLog.WriteLog(string.Format("Recently Watched Episode {0} is {1}", mrEpisodeNumber, episode.ToString()), LogLevel.Info);
          ++mrEpisodeNumber;
        }
      }

      // Need to sort this - but for now...
      setMostRecents();
    }

    void getLastThreeAddedTVSeries()
    {
      smcLog.WriteLog("Get Most Recent Added TVSeries", LogLevel.Info);

      // get list of the 3 most recently added episodes in tvseries database
      // use file created date rather than added as we dont want to see all episodes for new databases
      recentAddedEpisodes = DBEpisode.GetMostRecent(MostRecentType.Created, 30, 3);
      
      // Clear the properties first
      for (int i = 3; i == 0; --i)
      {
        SetProperty("#StreamedMP.recentlyAdded.series" + i.ToString() + ".title", string.Empty);
        SetProperty("#StreamedMP.recentlyAdded.series" + i.ToString() + ".episodetitle", string.Empty);
        SetProperty("#StreamedMP.recentlyAdded.series" + i.ToString() + ".episodenumber", string.Empty);
        SetProperty("#StreamedMP.recentlyAdded.series" + i.ToString() + ".season", string.Empty);
        SetProperty("#StreamedMP.recentlyAdded.series" + i.ToString() + ".thumb", string.Empty);
        SetProperty("#StreamedMP.recentlyAdded.series" + i.ToString() + ".fanart", string.Empty);
      }

      if (recentAddedEpisodes.Count == 0)
      {
        smcLog.WriteLog("Found no results for TVseries Recently Added", LogLevel.Info);
      }

      // Set properties
      int mrEpisodeNumber = 1;
      foreach (DBEpisode episode in recentAddedEpisodes)
      {
        DBSeries series = TVSeriesHelper.getCorrespondingSeries(episode[DBEpisode.cSeriesID]);
        if (series != null)
        {
          SetProperty("#StreamedMP.recentlyAdded.series" + mrEpisodeNumber.ToString() + ".title", series.ToString());
          SetProperty("#StreamedMP.recentlyAdded.series" + mrEpisodeNumber.ToString() + ".episodetitle", episode[DBEpisode.cEpisodeName]);
          SetProperty("#StreamedMP.recentlyAdded.series" + mrEpisodeNumber.ToString() + ".episodenumber", episode[DBEpisode.cEpisodeIndex]);
          SetProperty("#StreamedMP.recentlyAdded.series" + mrEpisodeNumber.ToString() + ".season", episode[DBEpisode.cSeasonIndex]);
          SetProperty("#StreamedMP.recentlyAdded.series" + mrEpisodeNumber.ToString() + ".thumb", series.Poster);
          SetProperty("#StreamedMP.recentlyAdded.series" + mrEpisodeNumber.ToString() + ".fanart", Fanart.getFanart(episode[DBEpisode.cSeriesID]).FanartFilename);
          smcLog.WriteLog(string.Format("Recently Added Episode {0} is {1}", mrEpisodeNumber, episode.ToString()), LogLevel.Info);
          ++mrEpisodeNumber;
        }
      }

      // Need to sort this - but for now...
      setMostRecents();
    }

    void cycleMostrecentFanart()
    {
      // Read the MostRecent Fanart property every x seconds, check the next in the sequence each time through.
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
        smcLog.WriteLog("StreamedMPConfig: TVSeries Img(" + mrImageToDisplay.ToString() + ") File:" + mostTVSeriesRecents[mrImageToDisplay - 1], LogLevel.Debug);
        SetProperty("#StreamedMP.MostRecentImageFanart", mostTVSeriesRecents[mrImageToDisplay - 1]);
      }
      if (mostTVSeriesRecentsWatched[mrImageToDisplay - 1] != null)
      {
        smcLog.WriteLog("StreamedMPConfig: TVSeries Img(" + mrImageToDisplay.ToString() + ") File:" + mostTVSeriesRecentsWatched[mrImageToDisplay - 1], LogLevel.Debug);
        SetProperty("#StreamedMP.MostRecentImageFanartWatched", mostTVSeriesRecentsWatched[mrImageToDisplay - 1]);
      }
      if (mostMovPicsRecents[mrImageToDisplay - 1] != null)
      {
        smcLog.WriteLog("StreamedMPConfig: MovingPictures Img(" + mrImageToDisplay.ToString() + ") File:" + mostMovPicsRecents[mrImageToDisplay - 1], LogLevel.Debug);
        SetProperty("#StreamedMP.MostRecentMovPicsImageFanart", mostMovPicsRecents[mrImageToDisplay - 1]);
      }
      if (mostMovPicsRecentsWatched[mrImageToDisplay - 1] != null)
      {
        smcLog.WriteLog("StreamedMPConfig: MovingPictures Watched Img(" + mrImageToDisplay.ToString() + ") File:" + mostMovPicsRecentsWatched[mrImageToDisplay - 1], LogLevel.Debug);
        SetProperty("#StreamedMP.MostRecentMovPicsImageFanartWatched", mostMovPicsRecentsWatched[mrImageToDisplay - 1]);
      }
    }

    void getMostRecents()
    {
      for (int i = 0; i < 3; i++)
      {
        mostTVSeriesRecents[i] = null;
        mostTVSeriesRecentsWatched[i] = null;
        mostMovPicsRecents[i] = null;
        mostMovPicsRecentsWatched[i] = null;
        try
        {
          mostTVSeriesRecents[i] = GUIPropertyManager.GetProperty("#StreamedMP.recentlyAdded.series" + (i + 1).ToString() + ".fanart");
        }
        catch { }
        try
        {
          mostTVSeriesRecentsWatched[i] = GUIPropertyManager.GetProperty("#StreamedMP.recentlyWatched.series" + (i + 1).ToString() + ".fanart");
        }
        catch { }
        try
        {
          mostMovPicsRecents[i] = GUIPropertyManager.GetProperty("#StreamedMP.recentlyAdded.movie" + (i + 1).ToString() + ".fanart");
        }
        catch { }
        try
        {
          mostMovPicsRecentsWatched[i] = GUIPropertyManager.GetProperty("#StreamedMP.recentlyWatched.movie" + (i + 1).ToString() + ".fanart");
        }
        catch { }
        if (mostTVSeriesRecents[i] == " ")
          mostTVSeriesRecents[i] = null;

        if (mostTVSeriesRecentsWatched[i] == " ")
          mostTVSeriesRecentsWatched[i] = null;

        if (mostMovPicsRecents[i] == " ")
          mostMovPicsRecents[i] = null;

        if (mostMovPicsRecentsWatched[i] == " ")
          mostMovPicsRecentsWatched[i] = null;

      }
    }

    private void setMostRecents()
    {
      string seasonNum = null;
      string episodeNum = null;
      string formattedSE = null;

      smcLog.WriteLog("In Most Recents Added=" + tvSeriesRecentAddedEnabled.ToString() + "   Watched=" + tvSeriesRecentWatchedEnabled.ToString(), LogLevel.Debug);


      if (tvSeriesRecentAddedEnabled)
      {
        for (int i = 0; i < 3; i++)
        {
          seasonNum = GUIPropertyManager.GetProperty("#StreamedMP.recentlyAdded.series" + (i + 1).ToString() + ".season");
          episodeNum = GUIPropertyManager.GetProperty("#StreamedMP.recentlyAdded.series" + (i + 1).ToString() + ".episodenumber");

          if (seasonNum != null && episodeNum != null)
          {
            if (smpSettings.mrSeasonEpisodeStyle2)
            {
              formattedSE = "S" + seasonNum.PadLeft(2, '0') + "E" + episodeNum.PadLeft(2, '0');
              SetProperty("#StreamedMP.MostRecent.Added." + (i + 1).ToString() + ".SEFormat", formattedSE);
            }
            else
            {
              formattedSE = seasonNum + "x" + episodeNum;
              SetProperty("#StreamedMP.MostRecent.Added." + (i + 1).ToString() + ".SEFormat", formattedSE);
            }

            smcLog.WriteLog("Set " + "#StreamedMP.MostRecent.Added." + (i + 1).ToString() + ".SEFormat to " + formattedSE, LogLevel.Debug);
          }
        }
      }

      if (tvSeriesRecentWatchedEnabled)
      {
        for (int i = 0; i < 3; i++)
        {
          seasonNum = GUIPropertyManager.GetProperty("#StreamedMP.recentlyWatched.series" + (i + 1).ToString() + ".season");
          episodeNum = GUIPropertyManager.GetProperty("#StreamedMP.recentlyWatched.series" + (i + 1).ToString() + ".episodenumber");

          if (seasonNum != null && episodeNum != null)
          {
            if (smpSettings.mrSeasonEpisodeStyle2)
            {
              formattedSE = "S" + seasonNum.PadLeft(2, '0') + "E" + episodeNum.PadLeft(2, '0');
              SetProperty("#StreamedMP.MostRecent.Watched." + (i + 1).ToString() + ".SEFormat", formattedSE);
            }
            else
            {
              formattedSE = seasonNum + "x" + episodeNum;
              SetProperty("#StreamedMP.MostRecent.Watched." + (i + 1).ToString() + ".SEFormat", formattedSE);
            }

            smcLog.WriteLog("Set " + "#StreamedMP.MostRecent.Watched." + (i + 1).ToString() + ".SEFormat to " + formattedSE, LogLevel.Debug);
          }
        }
      }
    }

    /// <summary>
    /// Play most recently added Episode
    /// </summary>
    /// <param name="index">index of episode to be played</param>
    private void PlayEpisode(int index)
    {
      if (recentAddedEpisodes != null && index <= recentAddedEpisodes.Count)
      {
        // send off to tvseries video player
        if (episodePlayer == null)
          episodePlayer = new VideoHandler();

        episodePlayer.ResumeOrPlay(recentAddedEpisodes[index - 1]);
      }
    }

    /// <summary>
    /// Play most recently added movie
    /// </summary>
    /// <param name="index">index of movie to be played</param>
    private void PlayMovie(int index)
    {
      if (recentAddedMovies != null && index <= recentAddedMovies.Count)
      {
        // send off to movingpics video player
        if (moviePlayer == null)
          moviePlayer = new MoviePlayer(new MovingPicturesGUI());

        moviePlayer.Play(recentAddedMovies[index - 1]);
      }
    }

    #endregion

    #region Public methods

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
      smcLog.WriteLog("Update timer fired - checking for update", LogLevel.Debug);
      if (updateCheck.updateAvailable())
      {
        StreamedMPConfig.updateAvailable = true;

        // This property controls the visibility of the large update icon on the home screens
        StreamedMPConfig.SetProperty("#StreamedMP.UpdateAvailable", "true");

        // This property controls the visibility or the indicator icon displyed next to the clock on the home screens
        StreamedMPConfig.SetProperty("#StreamedMP.ShowUpdateInd", "true");

        // This property controls the visibility or the indicator icon that is displayed next to the skin item in the settings screens
        StreamedMPConfig.SetProperty("#StreamedMP.ShowSettingsUpdateInd", "true");
      }
      else
        StreamedMPConfig.updateAvailable = false;
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
        smcLog.WriteLog("Next update check at : " + nextCheck.ToLongDateString() + ":" + nextCheck.ToLongTimeString(), LogLevel.Info);
        StreamedMPConfig.nextUpdateCheck = nextCheck.ToString();
        settings.Save(settings.cXMLSectionUpdate);

        TimeSpan setTimerFor = (nextCheck - DateTime.Now).Add(hoursToAdd);
        smcLog.WriteLog("Timer Set for : " + setTimerFor.ToString(), LogLevel.Info);

        return setTimerFor;
      }
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
      CheckSum checkSum = new CheckSum();

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
          if (Path.GetExtension(patchDestination + Path.GetFileName(Element)).ToLower() == ".xml")        // checkSum on xmlrpc files only
            if (checkSum.Compare(patchDestination + Path.GetFileName(Element)))                         // Only copy if checksum exists and matches
            {
              File.Copy(Element, patchDestination + Path.GetFileName(Element), true);
              smcLog.WriteLog("Patching: Copy >" + Element + " to " + patchDestination + Path.GetFileName(Element), LogLevel.Debug);
            }
        }
      }
    }

    #endregion

    #region EventHandlers

    public void smpAction(Action Action)
    {
      switch (Action.wID)
      {
        case Action.ActionType.REMOTE_1:
        case Action.ActionType.REMOTE_2:
        case Action.ActionType.REMOTE_3:
        case Action.ActionType.ACTION_PLAY:
        case Action.ActionType.ACTION_MUSIC_PLAY:
          // only listen when on BasicHome
          if ((GUIWindowManager.ActiveWindow == (int)MediaPortalWindows.BasicHome) && MiscConfigGUI.EnablePlayMostRecents)
          {
            bool recentAddedEpisodesVisible = false;
            bool recentAddedMoviesVisible = false;

            // check if tvseries recently added is visible
            if (tvSeriesRecentAddedEnabled)
            {
              foreach (int mostRecentEpisodeControlID in mostRecentEpisodeControlIDs)
              {
                recentAddedEpisodesVisible |= GUIWindowManager.GetWindow(GUIWindowManager.ActiveWindow).GetControl(mostRecentEpisodeControlID).IsVisible;
              }
            }

            // check if tvseries recently added is visible
            if (movPicRecentAddedEnabled)
            {
              foreach (int mostRecentMovieControlID in mostRecentMovieControlIDs)
              {
                recentAddedMoviesVisible |= GUIWindowManager.GetWindow(GUIWindowManager.ActiveWindow).GetControl(mostRecentMovieControlID).IsVisible;
              }
            }

            // play time
            if (recentAddedEpisodesVisible)
            {
              switch (Action.wID)
              {
                case Action.ActionType.REMOTE_1:
                  PlayEpisode(1);
                  break;
                case Action.ActionType.REMOTE_2:
                  PlayEpisode(2);
                  break;
                case Action.ActionType.REMOTE_3:
                  PlayEpisode(3);
                  break;
                default:
                  PlayEpisode(1);
                  break;
              }
            }
            else if (recentAddedMoviesVisible)
            {
              switch (Action.wID)
              {
                case Action.ActionType.REMOTE_1:
                  PlayMovie(1);
                  break;
                case Action.ActionType.REMOTE_2:
                  PlayMovie(2);
                  break;
                case Action.ActionType.REMOTE_3:
                  PlayMovie(3);
                  break;
                default:
                  PlayMovie(1);
                  break;
              }
            }
          }
          break;
          
        case (Action.ActionType)196250:
          smcLog.WriteLog("Restarting MediaPortal", LogLevel.Info);
          restartMediaportal();
          break;
      }
    }

    private void OnEpisodeWatched(DBEpisode episode)
    {
      smcLog.WriteLog(string.Format("TVSeries episode '{0}' counts as watched:", episode.ToString()), LogLevel.Info);
      getLastThreeWatchedTVSeries();
    }

    private void OnTVSeriesParseCompleted(bool dataUpdated)
    {
      // if tvseries has new or changed data update recents
      if (dataUpdated)
      {
        smcLog.WriteLog("TVSeries online import complete", LogLevel.Info);
        getLastThreeAddedTVSeries();
      }
    }

    private void OnObjectInserted(DatabaseTable obj)
    {
      // Update Recent Added/Watched lists
      if ((obj.GetType() == typeof(DBMovieInfo)) && movPicRecentAddedEnabled)
      {
        smcLog.WriteLog(string.Format("New movie added: {0}", ((DBMovieInfo)obj).Title), LogLevel.Info);
        getLastThreeAddedMovies();
      }
      else if ((obj.GetType() == typeof(DBWatchedHistory)) && movPicRecentWatchedEnabled)
      {
        smcLog.WriteLog(string.Format("movie watched: {0}", ((DBWatchedHistory)obj).Movie.Title), LogLevel.Info);
        getLastThreeWatchedMovies();
      }      
    }

    private void GUIGraphicsContext_OnVideoWindowChanged()
    {
      if (GUIWindowManager.ActiveWindow == (int)MediaPortalWindows.Home)
        return;

      smcLog.WriteLog("StreamedMPConfig: OnVideoWindowsChanged Event Called", LogLevel.Debug);

      if (GUIGraphicsContext.ShowBackground == true)
      {
        smcLog.WriteLog("#StreamedMP.VideoWallpaper = false", LogLevel.Debug);
        GUIPropertyManager.SetProperty("#StreamedMP.VideoWallpaper", "false");
      }
      else
      {
        smcLog.WriteLog("#StreamedMP.VideoWallpaper = true", LogLevel.Debug);
        GUIPropertyManager.SetProperty("#StreamedMP.VideoWallpaper", "true");
      }
    }

    private void GUIWindowManager_OnActivateWindow(int windowID)
    {
      if (GUIWindowManager.ActiveWindow == (int)MediaPortalWindows.Home)
        return;

      smcLog.WriteLog("StreamedMPConfig: OnActivateWindow Event Called(" + GUIWindowManager.ActiveWindow.ToString() + ")", LogLevel.Debug);

      // Disable the timer used for most recent fanart images in not on home screen.
      if (GUIWindowManager.ActiveWindow == (int)MediaPortalWindows.BasicHome)
      {
        if (smpSettings.timerRequired)
        {
          mrTimer.Enabled = true;
          smcLog.WriteLog(string.Format("Most Recent Summary Timer : Enabled ({0} Seconds)", MiscConfigGUI.MostRecentFanartTimerInt), LogLevel.Debug);
        }
      }
      else
      {
        if (smpSettings.timerRequired)
        {
          mrTimer.Enabled = false;
          smcLog.WriteLog("Most Recent Summary Timer : Disabled", LogLevel.Debug);
        }
      }

      if (GUIWindowManager.ActiveWindow == (int)MediaPortalWindows.BasicHome)
      {
        
        getMostRecents();
        //setMostRecents();        
      }
 
      if (StreamedMPConfig.updateAvailable)
      {
        // If we move off the basichome or Std menu turn off the the Update Available property so the main fade icon
        // is not displayed again
        // When leave the menu also turn off the update indicator displyed next to the clock as this may cause issues in other screens. 
        if (windowID != (int)MediaPortalWindows.BasicHome && windowID != (int)MediaPortalWindows.Home)
        {
          SetProperty("#StreamedMP.UpdateAvailable", "false");
          SetProperty("#StreamedMP.ShowUpdateInd", "false");
        }
        else
          SetProperty("#StreamedMP.ShowUpdateInd", "true");
      }
      else
      {
        smcLog.WriteLog("StreamedMPConfig: Setting all Update Indicator Values to false", LogLevel.Debug);
        SetProperty("#StreamedMP.UpdateAvailable", "false");
        SetProperty("#StreamedMP.ShowUpdateInd", "false");
        SetProperty("#StreamedMP.ShowSettingsUpdateInd", "false");
      }
    }

    void mrTimer_Tick(object sender, EventArgs e)
    {
      cycleMostrecentFanart();
    }

    void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
    {
      if (e.Mode == PowerModes.Resume)
      {
        smcLog.WriteLog("StreamedMP is Resuming from Standby", LogLevel.Info);
        
        // check for updates if enabled
        if (StreamedMPConfig.checkOnStart)
        {
          if (updateCheck.updateAvailable())
          {
            StreamedMPConfig.updateAvailable = true;

            // This property controls the visibility of the large update icon on the home screens
            SetProperty("#StreamedMP.UpdateAvailable", "true");

            // This property controls the visibility or the indicator icon displyed next to the clock on the home screens
            SetProperty("#StreamedMP.ShowUpdateInd", "true");

            // This property controls the visibility or the indicator icon that is displayed next to the skin item in the settings screens
            SetProperty("#StreamedMP.ShowSettingsUpdateInd", "true");
          }
        }
      }
      else if (e.Mode == PowerModes.Suspend)
      {
        smcLog.WriteLog("StreamedMP is entering Standby", LogLevel.Info);
      }
    }

    #endregion

    #region <Interface> Implementations

    #region IPlugin Interface

    public void Start()
    {
      logger.LogFile = Path.Combine(Config.GetFolder(Config.Dir.Log), "StreamedMPConfig.log");
      logger.LogError = smpSettings.logLevelError;
      logger.LogWarning = smpSettings.logLevelWarning;
      logger.LogDebug = smpSettings.logLevelDebug;
      if (skInfo.minimiseMPOnExit.ToLower() == "yes")
        minimiseOnExit = true;

      smcLog.WriteLog(string.Format("StreamedMPConfig Plugin {0} starting.", Assembly.GetExecutingAssembly().GetName().Version), LogLevel.Info);

      StreamedMPConfig.updateAvailable = false;
      
      // if we need to set properties on startup, load corresponding sections
      settings.Load(settings.cXMLSectionUpdate);
      settings.Load(settings.cXMLSectionMusic);      
      settings.Load(settings.cXMLSectionMisc);
      settings.Load(settings.cXMLSectionVideo);      

      MusicOptionsGUI.SetProperties();
      MiscConfigGUI.SetProperties();
      VideoOptionsGUI.SetProperties();
                                         
      if (smpSettings.timerRequired)
      {
        //cycleMostrecentFanart();
        mrTimer.Enabled = true;
        mrTimer.Interval = MiscConfigGUI.MostRecentFanartTimerInt * 1000;
        mrTimer.Tick += new EventHandler(mrTimer_Tick);
        smcLog.WriteLog(string.Format("StreamedMPConfig: Timer Enabled ({0} Seconds)", MiscConfigGUI.MostRecentFanartTimerInt), LogLevel.Info);
      }
      else
        smcLog.WriteLog("StreamedMPConfig: Timer Disabled", LogLevel.Info);


      GUIWindowManager.OnActivateWindow += new GUIWindowManager.WindowActivationHandler(GUIWindowManager_OnActivateWindow);
      GUIGraphicsContext.OnVideoWindowChanged += new VideoWindowChangedHandler(GUIGraphicsContext_OnVideoWindowChanged);
      GUIGraphicsContext.OnNewAction += new OnActionHandler(smpAction);
      SystemEvents.PowerModeChanged += new PowerModeChangedEventHandler(SystemEvents_PowerModeChanged);

      // Set all fixed translation properties 
      // these have defaults defined in the code and are used internally
      try
      {
        foreach (string name in Translation.Strings.Keys)
        {
          if (Translation.Strings[name] != "")
          {
            smcLog.WriteLog("Name : " + name + "  Translation : " + Translation.Strings[name], LogLevel.Info);
            SetProperty("#StreamedMP." + name, Translation.Strings[name]);
          }
        }
      }
      catch
      {
        smcLog.WriteLog("StreamedMPConfig: Translation Exception", LogLevel.Info);
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
            smcLog.WriteLog("Get MediaPortal Tranlation -> Name : " + propName + "  Translation : " + GUILocalizeStrings.Get(int.Parse(propValue)), LogLevel.Debug);
            SetProperty(propName, GUILocalizeStrings.Get(int.Parse(propValue)));
          }
          else
          {
            smcLog.WriteLog("Name : " + propName + "  Translation : " + propValue, LogLevel.Debug);
            SetProperty(propName, propValue);
          }
        }
      }

      // Should we check for an update on startup, if so check and set skin properties 
      // to control the visibility of the icons indicating and update is available?
      if (StreamedMPConfig.checkOnStart)
      {
        if (updateCheck.updateAvailable())
        {
          StreamedMPConfig.updateAvailable = true;

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
          smcLog.WriteLog("Stored Timer Set for : " + setStored.ToString(), LogLevel.Debug);
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
        smcLog.WriteLog("Set #StreamedMP.FullHD = false", LogLevel.Debug);
        StreamedMPConfig.SetProperty("#StreamedMP.FullHD", "false");
      }
      
      // Set Artist Path Property used in music overlays
      string artistPath = Path.Combine(Config.GetFolder(Config.Dir.Thumbs), @"Music\Artists");
      smcLog.WriteLog(string.Format("Set #StreamedMP.ArtistPath = {0}", artistPath), LogLevel.Info);
      StreamedMPConfig.SetProperty("#StreamedMP.ArtistPath", artistPath);

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
      return "StreamedMP Team";
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