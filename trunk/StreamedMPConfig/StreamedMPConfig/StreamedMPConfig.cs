using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using MediaPortal.GUI.Library;
using MediaPortal.GUI.Weather;
using MediaPortal.Configuration;
using MediaPortal.Util;
using System.Text.RegularExpressions;
using System.Timers;

namespace StreamedMPConfig
{
    public class StreamedMPConfigPlugin : IPlugin, ISetupForm
    {
        public StreamedMPConfigPlugin()
        {
        }

        #region ISetupForm Members

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
            return -1;
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

        bool timerSet = false;
        private bool cdCoverOnly = false;
        private bool showEqGraphic = false;

        System.Timers.Timer updateTimerSMP = new System.Timers.Timer();

        #region EventHandlers
        private void GUIWindowManager_OnActivateWindow(int windowID)
        {
          Log.Info("StreamedMPConfig: Setting all property valuse to false");
          GUIPropertyManager.SetProperty("#StreamedMP.cdCoverOnly", "false");
          GUIPropertyManager.SetProperty("#StreamedMP.showEqGraphic", "false");

          if (cdCoverOnly)
          {
            Log.Info("StreamedMPConfig: Setting #StreamedMP.cdCoverOnly = yes");
            GUIPropertyManager.SetProperty("#StreamedMP.cdCoverOnly", "true");
          }

          if (showEqGraphic)
          {
            Log.Info("StreamedMPConfig: Setting #StreamedMP.showEqGraphic = yes");
            GUIPropertyManager.SetProperty("#StreamedMP.showEqGraphic", "true");
          }
        }

        private static void OnTimedEventStreamedMP(object source, System.Timers.ElapsedEventArgs e)
        {


          if (GUIGraphicsContext.ShowBackground == true)
          {
            Log.Debug("#StreamedMP.VideoWallpaper updated on false");
            GUIPropertyManager.SetProperty("#StreamedMP.VideoWallpaper", "false");
          }
          else
          {
            Log.Debug("#StreamedMP.VideoWallpaper updated on true");
            GUIPropertyManager.SetProperty("#StreamedMP.VideoWallpaper", "true");
          }
        }

        #endregion



        public void Start()
        {

          Log.Info("StreamedMPConfig: Process started");

            GUIWindowManager.OnActivateWindow += new GUIWindowManager.WindowActivationHandler(GUIWindowManager_OnActivateWindow);

            using (MediaPortal.Profile.Settings xmlreader = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "StreamedMPConfig.xml")))
            {
                // Read user settings from configuration file

                if (xmlreader.GetValueAsInt("StreamedMPConfig", "cdCoverOnly", 1) != 0)
                    cdCoverOnly = true;
                if (xmlreader.GetValueAsInt("StreamedMPConfig", "showEqGraphic", 1) != 0)
                  showEqGraphic = true;
            }

            if (!timerSet)
            {
              Log.Debug("#StreamedMPConfig: Setting up timer event");
              updateTimerSMP.Elapsed += OnTimedEventStreamedMP;
              updateTimerSMP.Interval = 60000;
              updateTimerSMP.AutoReset = true;
              updateTimerSMP.Enabled = true;
              timerSet = true;
            }


        }

        public void Stop()
        {
            GUIWindowManager.OnActivateWindow -= new GUIWindowManager.WindowActivationHandler(GUIWindowManager_OnActivateWindow);
            updateTimerSMP.Stop();
        }

    }
}
